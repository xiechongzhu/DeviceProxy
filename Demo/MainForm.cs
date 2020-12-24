using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demo
{
    public partial class MainForm : Form
    {
        private DeviceProxy.DeviceProxy deviceProxy = new DeviceProxy.DeviceProxy();
        private Dictionary<int, StreamWriter> ChannelDataStreamWriters = new Dictionary<int, StreamWriter>();
        private String currentChannelDataDir;

        public MainForm()
        {
            InitializeComponent();
            deviceProxy.SetChannelDataCallBack(ChannelDataCallBack);
        }

        protected void ChannelDataCallBack(int channel, byte[] data)
        {
            if(!ChannelDataStreamWriters.ContainsKey(channel))
            {
                ChannelDataStreamWriters[channel] = new StreamWriter(File.Open(currentChannelDataDir + String.Format(@"\通道{0}.txt", channel),
                    FileMode.Create, FileAccess.Write, FileShare.Read));
            }
            ChannelDataStreamWriters[channel].WriteLine(byteToHexStr(data));
            ChannelDataStreamWriters[channel].Flush();
        }

        protected void DisconnectCallback(int reason)
        {
            Action action = ()=>{
                String strMessage;
                switch(reason)
                {
                    case DeviceProxy.DeviceProxy.ERR_CONNECTION_CLOSE_BY_PEER:
                        strMessage = "服务器主动断开连接";
                        break;
                    case DeviceProxy.DeviceProxy.ERR_PROTOCOL_ERROR:
                        strMessage = "协议错误";
                        break;
                    case DeviceProxy.DeviceProxy.ERR_RECV_DATA_ERROR:
                        strMessage = "数据接收错误";
                        break;
                    case DeviceProxy.DeviceProxy.ERR_SEND_DATA_ERROR:
                        strMessage = "数据发送错误";
                        break;
                    default:
                        strMessage = "未知错误";
                        break;
                }
                MessageBox.Show(this, strMessage);
                editIpAddr.Enabled = true;
                editPort.Enabled = true;
                btnConnect.Enabled = true;
                btnDisconnect.Enabled = false;
            };
            Invoke(action);
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            foreach (StreamWriter streamWriter in ChannelDataStreamWriters.Values)
            {
                streamWriter.Close();
            }
            ChannelDataStreamWriters.Clear();
            if (deviceProxy.ConnectToHost(editIpAddr.Text, int.Parse(editPort.Text), 1000, DisconnectCallback))
            {
                editIpAddr.Enabled = false;
                editPort.Enabled = false;
                btnConnect.Enabled = false;
                btnDisconnect.Enabled = true;
                currentChannelDataDir = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + DateTime.Now.ToString("yyyyMMddHHmmss");
                Directory.CreateDirectory(currentChannelDataDir);

            }
            else
            {
                MessageBox.Show("连接失败");
            }
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            deviceProxy.DisconnectFromHost();
            editIpAddr.Enabled = true;
            editPort.Enabled = true;
            btnConnect.Enabled = true;
            btnDisconnect.Enabled = false;
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            byte[] recvData = deviceProxy.GetChannelData(int.Parse(editReadChannel.Text), int.Parse(editReadCount.Text));
            String str = String.Format("[通道{0}]读取{1}字节,内容:{2}", int.Parse(editReadChannel.Text), recvData.Length,
                byteToHexStr(recvData));
            editReadOutput.Text += String.Format("\r\n{0}", str);
            editReadOutput.SelectionStart = editReadOutput.Text.Length;
            editReadOutput.ScrollToCaret();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            deviceProxy.SendChannelData(int.Parse(editSendChannel.Text), StrToHexByte(editSendData.Text));
        }

        public static string byteToHexStr(byte[] bytes)
        {
            string returnStr = "";
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    returnStr += (bytes[i].ToString("X2") + " ");
                }
            }
            return returnStr;
        }

        public static byte[] StrToHexByte(string hexString)
        {
            hexString = hexString.Replace(" ", "").Replace("\t", "").Replace("\r", "").Replace("\n", "");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }
    }
}
