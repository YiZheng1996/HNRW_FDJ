namespace MainUI.Procedure
{
    partial class UCCalibration
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCCalibration));
            this.uiPanel1 = new Sunny.UI.UIPanel();
            this.nudGainValue = new Sunny.UI.UITextBox();
            this.nudZeroValue = new Sunny.UI.UITextBox();
            this.btnReset = new Sunny.UI.UIButton();
            this.btnOK = new Sunny.UI.UIButton();
            this.txtValue = new Sunny.UI.UITextBox();
            this.cboChannel = new System.Windows.Forms.ComboBox();
            this.lblText = new Sunny.UI.UILabel();
            this.uiPanel1.SuspendLayout();
            this.txtValue.SuspendLayout();
            this.SuspendLayout();
            // 
            // uiPanel1
            // 
            this.uiPanel1.Controls.Add(this.nudGainValue);
            this.uiPanel1.Controls.Add(this.nudZeroValue);
            this.uiPanel1.Controls.Add(this.btnReset);
            this.uiPanel1.Controls.Add(this.btnOK);
            this.uiPanel1.Controls.Add(this.txtValue);
            resources.ApplyResources(this.uiPanel1, "uiPanel1");
            this.uiPanel1.Name = "uiPanel1";
            this.uiPanel1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.uiPanel1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // nudGainValue
            // 
            this.nudGainValue.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.nudGainValue.DecimalPlaces = 3;
            resources.ApplyResources(this.nudGainValue, "nudGainValue");
            this.nudGainValue.Name = "nudGainValue";
            this.nudGainValue.ShowText = false;
            this.nudGainValue.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.nudGainValue.Type = Sunny.UI.UITextBox.UIEditType.Double;
            this.nudGainValue.Watermark = "请输入";
            this.nudGainValue.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.nudGainValue.Click += new System.EventHandler(this.cboChannel_Click);
            this.nudGainValue.TextChanged += new System.EventHandler(this.nudGainValue_ValueChanged);
            // 
            // nudZeroValue
            // 
            this.nudZeroValue.Cursor = System.Windows.Forms.Cursors.IBeam;
            resources.ApplyResources(this.nudZeroValue, "nudZeroValue");
            this.nudZeroValue.Name = "nudZeroValue";
            this.nudZeroValue.ShowText = false;
            this.nudZeroValue.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.nudZeroValue.Type = Sunny.UI.UITextBox.UIEditType.Double;
            this.nudZeroValue.Watermark = "请输入";
            this.nudZeroValue.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.nudZeroValue.Click += new System.EventHandler(this.cboChannel_Click);
            this.nudZeroValue.TextChanged += new System.EventHandler(this.nudZeroDisplacement_ValueChanged);
            // 
            // btnReset
            // 
            this.btnReset.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.btnReset, "btnReset");
            this.btnReset.Name = "btnReset";
            this.btnReset.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnReset.TipsText = "1";
            this.btnReset.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnReset.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnOK
            // 
            this.btnOK.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.Name = "btnOK";
            this.btnOK.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOK.TipsText = "1";
            this.btnOK.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // txtValue
            // 
            this.txtValue.Controls.Add(this.cboChannel);
            this.txtValue.Cursor = System.Windows.Forms.Cursors.Default;
            resources.ApplyResources(this.txtValue, "txtValue");
            this.txtValue.Name = "txtValue";
            this.txtValue.ReadOnly = true;
            this.txtValue.ShowText = false;
            this.txtValue.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtValue.Watermark = "请选择";
            this.txtValue.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.txtValue.TextChanged += new System.EventHandler(this.txtValue_TextChanged);
            // 
            // cboChannel
            // 
            resources.ApplyResources(this.cboChannel, "cboChannel");
            this.cboChannel.Name = "cboChannel";
            this.cboChannel.Click += new System.EventHandler(this.cboChannel_Click);
            // 
            // lblText
            // 
            this.lblText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            resources.ApplyResources(this.lblText, "lblText");
            this.lblText.Name = "lblText";
            this.lblText.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // UCCalibration
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblText);
            this.Controls.Add(this.uiPanel1);
            this.Name = "UCCalibration";
            this.Load += new System.EventHandler(this.UCCalibration_Load);
            this.uiPanel1.ResumeLayout(false);
            this.txtValue.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UIPanel uiPanel1;
        private Sunny.UI.UILabel lblText;
        private Sunny.UI.UITextBox txtValue;
        protected System.Windows.Forms.ComboBox cboChannel;
        private Sunny.UI.UITextBox nudGainValue;
        private Sunny.UI.UITextBox nudZeroValue;
        private Sunny.UI.UIButton btnReset;
        private Sunny.UI.UIButton btnOK;
    }
}
