
namespace Demo
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.editIpAddr = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.editPort = new System.Windows.Forms.TextBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.editReadCount = new System.Windows.Forms.TextBox();
            this.editReadOutput = new System.Windows.Forms.TextBox();
            this.editReadChannel = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnRead = new System.Windows.Forms.Button();
            this.btnSend = new System.Windows.Forms.Button();
            this.editSendChannel = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.editSendData = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP:";
            // 
            // editIpAddr
            // 
            this.editIpAddr.Location = new System.Drawing.Point(64, 28);
            this.editIpAddr.Name = "editIpAddr";
            this.editIpAddr.Size = new System.Drawing.Size(180, 35);
            this.editIpAddr.TabIndex = 1;
            this.editIpAddr.Text = "127.0.0.1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(273, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 24);
            this.label2.TabIndex = 2;
            this.label2.Text = "PORT:";
            // 
            // editPort
            // 
            this.editPort.Location = new System.Drawing.Point(349, 28);
            this.editPort.Name = "editPort";
            this.editPort.Size = new System.Drawing.Size(180, 35);
            this.editPort.TabIndex = 3;
            this.editPort.Text = "1111";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(575, 31);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 40);
            this.btnConnect.TabIndex = 4;
            this.btnConnect.Text = "连接";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Enabled = false;
            this.btnDisconnect.Location = new System.Drawing.Point(670, 31);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(75, 40);
            this.btnDisconnect.TabIndex = 5;
            this.btnDisconnect.Text = "断开";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 24);
            this.label3.TabIndex = 6;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 98);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1442, 773);
            this.tabControl1.TabIndex = 7;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnRead);
            this.tabPage1.Controls.Add(this.editReadChannel);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.editReadOutput);
            this.tabPage1.Controls.Add(this.editReadCount);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Location = new System.Drawing.Point(8, 39);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1426, 726);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "数据读取";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.editSendData);
            this.tabPage2.Controls.Add(this.btnSend);
            this.tabPage2.Controls.Add(this.editSendChannel);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Location = new System.Drawing.Point(8, 39);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1426, 726);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "数据发送";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(118, 24);
            this.label4.TabIndex = 0;
            this.label4.Text = "读取长度:";
            // 
            // editReadCount
            // 
            this.editReadCount.Location = new System.Drawing.Point(144, 24);
            this.editReadCount.Name = "editReadCount";
            this.editReadCount.Size = new System.Drawing.Size(201, 35);
            this.editReadCount.TabIndex = 1;
            this.editReadCount.Text = "1000";
            // 
            // editReadOutput
            // 
            this.editReadOutput.Location = new System.Drawing.Point(24, 82);
            this.editReadOutput.Multiline = true;
            this.editReadOutput.Name = "editReadOutput";
            this.editReadOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.editReadOutput.Size = new System.Drawing.Size(1378, 623);
            this.editReadOutput.TabIndex = 2;
            // 
            // editReadChannel
            // 
            this.editReadChannel.Location = new System.Drawing.Point(491, 24);
            this.editReadChannel.Name = "editReadChannel";
            this.editReadChannel.Size = new System.Drawing.Size(201, 35);
            this.editReadChannel.TabIndex = 4;
            this.editReadChannel.Text = "1";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(391, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(94, 24);
            this.label5.TabIndex = 3;
            this.label5.Text = "通道号:";
            // 
            // btnRead
            // 
            this.btnRead.Location = new System.Drawing.Point(722, 24);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(102, 40);
            this.btnRead.TabIndex = 5;
            this.btnRead.Text = "读取";
            this.btnRead.UseVisualStyleBackColor = true;
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(359, 25);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(102, 40);
            this.btnSend.TabIndex = 8;
            this.btnSend.Text = "发送";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // editSendChannel
            // 
            this.editSendChannel.Location = new System.Drawing.Point(132, 25);
            this.editSendChannel.Name = "editSendChannel";
            this.editSendChannel.Size = new System.Drawing.Size(201, 35);
            this.editSendChannel.TabIndex = 7;
            this.editSendChannel.Text = "1";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 28);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(94, 24);
            this.label6.TabIndex = 6;
            this.label6.Text = "通道号:";
            // 
            // editSendData
            // 
            this.editSendData.Location = new System.Drawing.Point(21, 99);
            this.editSendData.Multiline = true;
            this.editSendData.Name = "editSendData";
            this.editSendData.Size = new System.Drawing.Size(1386, 621);
            this.editSendData.TabIndex = 9;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1466, 883);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnDisconnect);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.editPort);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.editIpAddr);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "DeviceProxy";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox editIpAddr;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox editPort;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox editReadChannel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox editReadOutput;
        private System.Windows.Forms.TextBox editReadCount;
        private System.Windows.Forms.Button btnRead;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox editSendChannel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox editSendData;
    }
}

