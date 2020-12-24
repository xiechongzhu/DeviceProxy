using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DeviceProxy
{
    public class DeviceProxy
    {
        public const int ERR_CONNECTION_CLOSE_BY_PEER = 1;
        public const int ERR_RECV_DATA_ERROR = 2;
        public const int ERR_SEND_DATA_ERROR = 3;
        public const int ERR_PROTOCOL_ERROR = 4;
        private const int MAX_CHANNEL_DATA_LENGTH = 10 * 1024 * 1024;

        public delegate void DisconnectCallBack(int reason);
        public delegate void ChannelDataCallBack(int channel, byte[] data);
        private TcpClient socket;
        private DisconnectCallBack disconnectCallBack;
        private ChannelDataCallBack channelDataCallBack;
        private bool bIsConnected = false;
        private byte[] recvBuffer = new byte[100 *1024 * 1024];
        private int recvBufferWritePos = 0;
        private Dictionary<Int32, byte[]> channelDataMap = new Dictionary<int, byte[]>();
        private object channelDataMutex = new object();

        public void SetChannelDataCallBack(ChannelDataCallBack callBack)
        {
            channelDataCallBack = callBack;
        }

        public bool ConnectToHost(String ipAddr, int port, int timeout, DisconnectCallBack callBack)
        {
            try
            {
                channelDataMap.Clear();
                recvBufferWritePos = 0;
                socket = new TcpClient();
                socket.ReceiveBufferSize = socket.SendBufferSize = 100 * 1024 * 1024;
                disconnectCallBack = callBack;
                IAsyncResult result = socket.BeginConnect(ipAddr, port, null, null);
                bool bSuccess = result.AsyncWaitHandle.WaitOne(TimeSpan.FromMilliseconds(timeout));
                if (bSuccess)
                {
                    bIsConnected = true;
                    socket.EndConnect(result);
                    BeginReadNetworkData();
                }
                return bSuccess;
            }
            catch(Exception)
            {
                return false;
            }
        }

        public void DisconnectFromHost()
        {
            channelDataMap.Clear();
            recvBufferWritePos = 0;
            bIsConnected = false;
            socket.Close();
            socket.Dispose();
        }

        public byte[] GetChannelData(Int32 channel, Int32 maxLength)
        {
            lock (channelDataMutex)
            {
                if(!channelDataMap.ContainsKey(channel))
                {
                    return new byte[0];
                }
                byte[] channelData = channelDataMap[channel];
                Int32 len;
                if(-1 == maxLength)
                {
                    len = channelData.Length;
                }
                else
                {
                    len = Math.Min(maxLength, channelData.Length);
                }
                byte[] retData = channelData.Take(len).ToArray();
                channelDataMap[channel] = channelData.Skip(len).ToArray();
                return retData;
            }
        }

        public void SendChannelData(Int32 channel, byte[] data)
        {
            if(bIsConnected)
            {
                try
                {
                    ProtocolHeader protocolHeader = new ProtocolHeader
                    {
                        Channel = channel,
                        DataLength = data.Length
                    };
                    IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf<ProtocolHeader>());
                    Marshal.StructureToPtr(protocolHeader, ptr, true);
                    byte[] header = new byte[Marshal.SizeOf<ProtocolHeader>()];
                    Marshal.Copy(ptr, header, 0, header.Length);
                    Marshal.FreeHGlobal(ptr);
                    byte[] sendData = header.Concat(data).ToArray();
                    socket.GetStream().BeginWrite(sendData, 0, sendData.Length, EndWriteNetworkData, null);
                }
                catch(Exception)
                {
                    DisconnectFromHost();
                    disconnectCallBack?.Invoke(ERR_SEND_DATA_ERROR);
                }
            }
        }

        private void EndWriteNetworkData(IAsyncResult result)
        {
            if(bIsConnected)
            {
                try
                {
                    if(!result.IsCompleted)
                    {
                        DisconnectFromHost();
                        disconnectCallBack?.Invoke(ERR_SEND_DATA_ERROR);
                    }
                    socket.GetStream().EndWrite(result);
                }
                catch(Exception)
                {
                    DisconnectFromHost();
                    disconnectCallBack?.Invoke(ERR_SEND_DATA_ERROR);
                }
            }
        }

        protected void BeginReadNetworkData()
        {
            if(bIsConnected)
            {
                try
                {
                    byte[] socketReadBuffer = new byte[10 * 1024];
                    NetworkStream networkStream = socket.GetStream();
                    networkStream.BeginRead(socketReadBuffer, 0, socketReadBuffer.Length, EndReadNetworkData, socketReadBuffer);
                }
                catch(Exception)
                {
                    DisconnectFromHost();
                    disconnectCallBack?.Invoke(ERR_RECV_DATA_ERROR);
                }
            } 
        }

        protected void EndReadNetworkData(IAsyncResult result)
        {
            if (bIsConnected)
            {
                try
                {
                    byte[] buffer = (byte[])result.AsyncState;
                    NetworkStream networkStream = socket.GetStream();
                    int length = networkStream.EndRead(result);
                    BeginReadNetworkData();
                    if (length == 0)
                    {
                        DisconnectFromHost();
                        disconnectCallBack?.Invoke(ERR_CONNECTION_CLOSE_BY_PEER);
                    }
                    else
                    {
                        if(recvBufferWritePos + length <= recvBuffer.Length)
                        {
                            Array.Copy(buffer, 0, recvBuffer, recvBufferWritePos, length);
                            recvBufferWritePos += length;
                            ParseDeviceData();
                        }
                        else
                        {
                            DisconnectFromHost();
                            disconnectCallBack?.Invoke(ERR_PROTOCOL_ERROR);
                        }
                    }
                }
                catch(Exception)
                {
                    DisconnectFromHost();
                    disconnectCallBack?.Invoke(ERR_RECV_DATA_ERROR);
                }
            }
        }

        protected void ParseDeviceData()
        {
            while(true)
            {
                if(recvBufferWritePos < Marshal.SizeOf<ProtocolHeader>())
                {
                    break;
                }
                IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf<ProtocolHeader>());
                Marshal.Copy(recvBuffer, 0, ptr, Marshal.SizeOf<ProtocolHeader>());
                ProtocolHeader header = Marshal.PtrToStructure<ProtocolHeader>(ptr);
                Marshal.FreeHGlobal(ptr);
                if(recvBufferWritePos < Marshal.SizeOf<ProtocolHeader>() + header.DataLength)
                {
                    break;
                }
                byte[] deviceData = recvBuffer.Skip(Marshal.SizeOf<ProtocolHeader>()).Take(header.DataLength).ToArray();
                Array.Copy(recvBuffer, 0, recvBuffer, 
                    Marshal.SizeOf<ProtocolHeader>() + header.DataLength, recvBufferWritePos - (Marshal.SizeOf<ProtocolHeader>() + header.DataLength));
                recvBufferWritePos -= (Marshal.SizeOf<ProtocolHeader>() + header.DataLength);
                lock (channelDataMutex)
                {
                    if (!channelDataMap.ContainsKey(header.Channel))
                    {
                        channelDataMap[header.Channel] = deviceData;
                    }
                    else
                    {
                        channelDataMap[header.Channel] = channelDataMap[header.Channel].Concat(deviceData).ToArray();
                    }
                    if(channelDataMap[header.Channel].Length > MAX_CHANNEL_DATA_LENGTH)
                    {
                        channelDataMap[header.Channel] = channelDataMap[header.Channel].Skip(channelDataMap[header.Channel].Length - MAX_CHANNEL_DATA_LENGTH).ToArray();
                    }
                }
                channelDataCallBack?.Invoke(header.Channel, deviceData);
            }
        }
    }
}
