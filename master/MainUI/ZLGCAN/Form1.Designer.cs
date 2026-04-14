namespace ZLGCAN
{
    partial class Form1
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
            this.button_close = new System.Windows.Forms.Button();
            this.btn_init = new System.Windows.Forms.Button();
            this.button_reset = new System.Windows.Forms.Button();
            this.btn_start = new System.Windows.Forms.Button();
            this.btn_open = new System.Windows.Forms.Button();
            this.comboBox_index = new System.Windows.Forms.ComboBox();
            this.label_index = new System.Windows.Forms.Label();
            this.label_baud = new System.Windows.Forms.Label();
            this.comboBox_baud = new System.Windows.Forms.ComboBox();
            this.comboBox_channel = new System.Windows.Forms.ComboBox();
            this.label_channel = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.listBox = new System.Windows.Forms.ListBox();
            this.button_clear = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnSendData = new System.Windows.Forms.Button();
            this.comboBox_frametype = new System.Windows.Forms.ComboBox();
            this.label_frametype = new System.Windows.Forms.Label();
            this.label_senddata = new System.Windows.Forms.Label();
            this.textBox_senddata = new System.Windows.Forms.TextBox();
            this.label_ID = new System.Windows.Forms.Label();
            this.textBox_ID = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_close
            // 
            this.button_close.Location = new System.Drawing.Point(717, 87);
            this.button_close.Margin = new System.Windows.Forms.Padding(4);
            this.button_close.Name = "button_close";
            this.button_close.Size = new System.Drawing.Size(127, 54);
            this.button_close.TabIndex = 16;
            this.button_close.Text = "关闭设备";
            this.button_close.UseVisualStyleBackColor = true;
            this.button_close.Click += new System.EventHandler(this.button_close_Click);
            // 
            // btn_init
            // 
            this.btn_init.Location = new System.Drawing.Point(187, 87);
            this.btn_init.Margin = new System.Windows.Forms.Padding(4);
            this.btn_init.Name = "btn_init";
            this.btn_init.Size = new System.Drawing.Size(127, 54);
            this.btn_init.TabIndex = 15;
            this.btn_init.Text = "2.初始化CAN";
            this.btn_init.UseVisualStyleBackColor = true;
            this.btn_init.Click += new System.EventHandler(this.button_init_Click);
            // 
            // button_reset
            // 
            this.button_reset.Location = new System.Drawing.Point(557, 87);
            this.button_reset.Margin = new System.Windows.Forms.Padding(4);
            this.button_reset.Name = "button_reset";
            this.button_reset.Size = new System.Drawing.Size(127, 54);
            this.button_reset.TabIndex = 14;
            this.button_reset.Text = "复位";
            this.button_reset.UseVisualStyleBackColor = true;
            this.button_reset.Click += new System.EventHandler(this.button_reset_Click);
            // 
            // btn_start
            // 
            this.btn_start.Location = new System.Drawing.Point(339, 87);
            this.btn_start.Margin = new System.Windows.Forms.Padding(4);
            this.btn_start.Name = "btn_start";
            this.btn_start.Size = new System.Drawing.Size(127, 54);
            this.btn_start.TabIndex = 13;
            this.btn_start.Text = "3.启动CAN";
            this.btn_start.UseVisualStyleBackColor = true;
            this.btn_start.Click += new System.EventHandler(this.button_start_Click);
            // 
            // btn_open
            // 
            this.btn_open.Location = new System.Drawing.Point(19, 87);
            this.btn_open.Margin = new System.Windows.Forms.Padding(4);
            this.btn_open.Name = "btn_open";
            this.btn_open.Size = new System.Drawing.Size(127, 54);
            this.btn_open.TabIndex = 12;
            this.btn_open.Text = "1.打开设备";
            this.btn_open.UseVisualStyleBackColor = true;
            this.btn_open.Click += new System.EventHandler(this.button_open_Click);
            // 
            // comboBox_index
            // 
            this.comboBox_index.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_index.FormattingEnabled = true;
            this.comboBox_index.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3"});
            this.comboBox_index.Location = new System.Drawing.Point(153, 20);
            this.comboBox_index.Margin = new System.Windows.Forms.Padding(4);
            this.comboBox_index.Name = "comboBox_index";
            this.comboBox_index.Size = new System.Drawing.Size(63, 23);
            this.comboBox_index.TabIndex = 33;
            // 
            // label_index
            // 
            this.label_index.AutoSize = true;
            this.label_index.Location = new System.Drawing.Point(59, 25);
            this.label_index.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_index.Name = "label_index";
            this.label_index.Size = new System.Drawing.Size(82, 15);
            this.label_index.TabIndex = 32;
            this.label_index.Text = "设备索引：";
            // 
            // label_baud
            // 
            this.label_baud.AutoSize = true;
            this.label_baud.Location = new System.Drawing.Point(434, 23);
            this.label_baud.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_baud.Name = "label_baud";
            this.label_baud.Size = new System.Drawing.Size(67, 15);
            this.label_baud.TabIndex = 35;
            this.label_baud.Text = "波特率：";
            // 
            // comboBox_baud
            // 
            this.comboBox_baud.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_baud.FormattingEnabled = true;
            this.comboBox_baud.Items.AddRange(new object[] {
            "1Mbps",
            "800kbps",
            "500kbps",
            "250kbps",
            "125kbps",
            "100kbps",
            "50kbps",
            "20kbps",
            "10kbps",
            "5kbps"});
            this.comboBox_baud.Location = new System.Drawing.Point(510, 17);
            this.comboBox_baud.Margin = new System.Windows.Forms.Padding(4);
            this.comboBox_baud.Name = "comboBox_baud";
            this.comboBox_baud.Size = new System.Drawing.Size(160, 23);
            this.comboBox_baud.TabIndex = 34;
            // 
            // comboBox_channel
            // 
            this.comboBox_channel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_channel.FormattingEnabled = true;
            this.comboBox_channel.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3"});
            this.comboBox_channel.Location = new System.Drawing.Point(312, 20);
            this.comboBox_channel.Name = "comboBox_channel";
            this.comboBox_channel.Size = new System.Drawing.Size(44, 23);
            this.comboBox_channel.TabIndex = 37;
            this.comboBox_channel.SelectedIndexChanged += new System.EventHandler(this.comboBox_channel_SelectedIndexChanged);
            // 
            // label_channel
            // 
            this.label_channel.AutoSize = true;
            this.label_channel.Location = new System.Drawing.Point(263, 24);
            this.label_channel.Name = "label_channel";
            this.label_channel.Size = new System.Drawing.Size(52, 15);
            this.label_channel.TabIndex = 36;
            this.label_channel.Text = "通道：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.listBox);
            this.groupBox1.Location = new System.Drawing.Point(11, 339);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(889, 309);
            this.groupBox1.TabIndex = 38;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "数据接收";
            // 
            // listBox
            // 
            this.listBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox.FormattingEnabled = true;
            this.listBox.ItemHeight = 15;
            this.listBox.Location = new System.Drawing.Point(3, 20);
            this.listBox.Name = "listBox";
            this.listBox.Size = new System.Drawing.Size(883, 286);
            this.listBox.TabIndex = 2;
            // 
            // button_clear
            // 
            this.button_clear.Location = new System.Drawing.Point(801, 324);
            this.button_clear.Name = "button_clear";
            this.button_clear.Size = new System.Drawing.Size(99, 42);
            this.button_clear.TabIndex = 1;
            this.button_clear.Text = "清空";
            this.button_clear.UseVisualStyleBackColor = true;
            this.button_clear.Click += new System.EventHandler(this.button_clear_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnSendData);
            this.groupBox2.Controls.Add(this.comboBox_frametype);
            this.groupBox2.Controls.Add(this.label_frametype);
            this.groupBox2.Controls.Add(this.label_senddata);
            this.groupBox2.Controls.Add(this.textBox_senddata);
            this.groupBox2.Controls.Add(this.label_ID);
            this.groupBox2.Controls.Add(this.textBox_ID);
            this.groupBox2.Location = new System.Drawing.Point(11, 175);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(886, 143);
            this.groupBox2.TabIndex = 39;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "数据发送";
            // 
            // btnSendData
            // 
            this.btnSendData.Location = new System.Drawing.Point(546, 77);
            this.btnSendData.Name = "btnSendData";
            this.btnSendData.Size = new System.Drawing.Size(99, 42);
            this.btnSendData.TabIndex = 33;
            this.btnSendData.Text = "发送";
            this.btnSendData.UseVisualStyleBackColor = true;
            this.btnSendData.Click += new System.EventHandler(this.btnSendData_Click);
            // 
            // comboBox_frametype
            // 
            this.comboBox_frametype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_frametype.FormattingEnabled = true;
            this.comboBox_frametype.Items.AddRange(new object[] {
            "标准帧",
            "扩展帧"});
            this.comboBox_frametype.Location = new System.Drawing.Point(415, 17);
            this.comboBox_frametype.Name = "comboBox_frametype";
            this.comboBox_frametype.Size = new System.Drawing.Size(92, 23);
            this.comboBox_frametype.TabIndex = 32;
            // 
            // label_frametype
            // 
            this.label_frametype.AutoSize = true;
            this.label_frametype.Location = new System.Drawing.Point(352, 22);
            this.label_frametype.Name = "label_frametype";
            this.label_frametype.Size = new System.Drawing.Size(67, 15);
            this.label_frametype.TabIndex = 31;
            this.label_frametype.Text = "帧类型：";
            // 
            // label_senddata
            // 
            this.label_senddata.AutoSize = true;
            this.label_senddata.Location = new System.Drawing.Point(22, 52);
            this.label_senddata.Name = "label_senddata";
            this.label_senddata.Size = new System.Drawing.Size(175, 15);
            this.label_senddata.TabIndex = 30;
            this.label_senddata.Text = "数据(0x, 以空格隔开)：";
            // 
            // textBox_senddata
            // 
            this.textBox_senddata.Location = new System.Drawing.Point(203, 47);
            this.textBox_senddata.Name = "textBox_senddata";
            this.textBox_senddata.Size = new System.Drawing.Size(630, 24);
            this.textBox_senddata.TabIndex = 29;
            this.textBox_senddata.Text = "11 22 33 44 55 66 77 88";
            // 
            // label_ID
            // 
            this.label_ID.AutoSize = true;
            this.label_ID.Location = new System.Drawing.Point(127, 21);
            this.label_ID.Name = "label_ID";
            this.label_ID.Size = new System.Drawing.Size(70, 15);
            this.label_ID.TabIndex = 22;
            this.label_ID.Text = "ID(0x)：";
            // 
            // textBox_ID
            // 
            this.textBox_ID.Location = new System.Drawing.Point(203, 17);
            this.textBox_ID.MaxLength = 8;
            this.textBox_ID.Name = "textBox_ID";
            this.textBox_ID.Size = new System.Drawing.Size(100, 24);
            this.textBox_ID.TabIndex = 21;
            this.textBox_ID.Text = "01";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(928, 676);
            this.Controls.Add(this.button_clear);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.comboBox_channel);
            this.Controls.Add(this.label_channel);
            this.Controls.Add(this.comboBox_index);
            this.Controls.Add(this.label_index);
            this.Controls.Add(this.label_baud);
            this.Controls.Add(this.comboBox_baud);
            this.Controls.Add(this.button_close);
            this.Controls.Add(this.btn_init);
            this.Controls.Add(this.button_reset);
            this.Controls.Add(this.btn_start);
            this.Controls.Add(this.btn_open);
            this.Font = new System.Drawing.Font("宋体", 11F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ZLG USBCAN_II";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_close;
        private System.Windows.Forms.Button btn_init;
        private System.Windows.Forms.Button button_reset;
        private System.Windows.Forms.Button btn_start;
        private System.Windows.Forms.Button btn_open;
        private System.Windows.Forms.ComboBox comboBox_index;
        private System.Windows.Forms.Label label_index;
        private System.Windows.Forms.Label label_baud;
        private System.Windows.Forms.ComboBox comboBox_baud;
        private System.Windows.Forms.ComboBox comboBox_channel;
        private System.Windows.Forms.Label label_channel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox listBox;
        private System.Windows.Forms.Button button_clear;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label_senddata;
        private System.Windows.Forms.TextBox textBox_senddata;
        private System.Windows.Forms.Label label_ID;
        private System.Windows.Forms.TextBox textBox_ID;
        private System.Windows.Forms.ComboBox comboBox_frametype;
        private System.Windows.Forms.Label label_frametype;
        private System.Windows.Forms.Button btnSendData;
    }
}

