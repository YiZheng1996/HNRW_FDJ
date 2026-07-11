
namespace MainUI.Report
{
    partial class ucAutoRecord
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.txtNumber = new Sunny.UI.UITextBox();
            this.uiLabel3 = new Sunny.UI.UILabel();
            this.uiLabel2 = new Sunny.UI.UILabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgvRecord = new System.Windows.Forms.DataGridView();
            this.uiLabel4 = new Sunny.UI.UILabel();
            this.cboModel = new Sunny.UI.UITextBox();
            this.dtpStartTime = new Sunny.UI.UIDatetimePicker();
            this.uiLabel1 = new Sunny.UI.UILabel();
            this.dtpEndTime = new Sunny.UI.UIDatetimePicker();
            this.btnSelctModel = new RW.UI.Controls.RButton();
            this.btnSearch = new RW.UI.Controls.RButton();
            this.btnUpPage = new RW.UI.Controls.RButton();
            this.btnNextPage = new RW.UI.Controls.RButton();
            this.lblTotalNum = new Sunny.UI.UILabel();
            this.btnOut = new RW.UI.Controls.RButton();
            this.rdoManual = new Sunny.UI.UIRadioButton();
            this.rdoAuto = new Sunny.UI.UIRadioButton();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecord)).BeginInit();
            this.SuspendLayout();
            // 
            // txtNumber
            // 
            this.txtNumber.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtNumber.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtNumber.Location = new System.Drawing.Point(94, 52);
            this.txtNumber.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtNumber.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtNumber.Name = "txtNumber";
            this.txtNumber.Padding = new System.Windows.Forms.Padding(5);
            this.txtNumber.ShowText = false;
            this.txtNumber.Size = new System.Drawing.Size(141, 29);
            this.txtNumber.TabIndex = 392;
            this.txtNumber.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtNumber.Watermark = "请输入";
            // 
            // uiLabel3
            // 
            this.uiLabel3.BackColor = System.Drawing.Color.Transparent;
            this.uiLabel3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.uiLabel3.Location = new System.Drawing.Point(6, 55);
            this.uiLabel3.Name = "uiLabel3";
            this.uiLabel3.Size = new System.Drawing.Size(90, 23);
            this.uiLabel3.TabIndex = 391;
            this.uiLabel3.Text = "发动机编号";
            this.uiLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // uiLabel2
            // 
            this.uiLabel2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.uiLabel2.Location = new System.Drawing.Point(6, 12);
            this.uiLabel2.Name = "uiLabel2";
            this.uiLabel2.Size = new System.Drawing.Size(97, 23);
            this.uiLabel2.TabIndex = 390;
            this.uiLabel2.Text = "发动机型号";
            this.uiLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dgvRecord);
            this.panel1.Location = new System.Drawing.Point(10, 92);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1815, 902);
            this.panel1.TabIndex = 393;
            // 
            // dgvRecord
            // 
            this.dgvRecord.AllowUserToAddRows = false;
            this.dgvRecord.AllowUserToDeleteRows = false;
            this.dgvRecord.BackgroundColor = System.Drawing.Color.White;
            this.dgvRecord.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRecord.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRecord.EnableHeadersVisualStyles = false;
            this.dgvRecord.Location = new System.Drawing.Point(0, 0);
            this.dgvRecord.Name = "dgvRecord";
            this.dgvRecord.ReadOnly = true;
            this.dgvRecord.RowTemplate.Height = 23;
            this.dgvRecord.Size = new System.Drawing.Size(1815, 902);
            this.dgvRecord.TabIndex = 845;
            // 
            // uiLabel4
            // 
            this.uiLabel4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.uiLabel4.Location = new System.Drawing.Point(383, 12);
            this.uiLabel4.Name = "uiLabel4";
            this.uiLabel4.Size = new System.Drawing.Size(75, 23);
            this.uiLabel4.TabIndex = 394;
            this.uiLabel4.Text = "开始时间";
            this.uiLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cboModel
            // 
            this.cboModel.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.cboModel.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboModel.Location = new System.Drawing.Point(94, 9);
            this.cboModel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cboModel.MinimumSize = new System.Drawing.Size(1, 16);
            this.cboModel.Name = "cboModel";
            this.cboModel.Padding = new System.Windows.Forms.Padding(5);
            this.cboModel.ShowText = false;
            this.cboModel.Size = new System.Drawing.Size(141, 29);
            this.cboModel.TabIndex = 400;
            this.cboModel.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.cboModel.Watermark = "请输入";
            // 
            // dtpStartTime
            // 
            this.dtpStartTime.CanEmpty = true;
            this.dtpStartTime.DateCultureInfo = new System.Globalization.CultureInfo("zh-CN");
            this.dtpStartTime.FillColor = System.Drawing.Color.White;
            this.dtpStartTime.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dtpStartTime.Location = new System.Drawing.Point(465, 9);
            this.dtpStartTime.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dtpStartTime.MaxLength = 19;
            this.dtpStartTime.MinimumSize = new System.Drawing.Size(63, 0);
            this.dtpStartTime.Name = "dtpStartTime";
            this.dtpStartTime.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.dtpStartTime.Size = new System.Drawing.Size(199, 29);
            this.dtpStartTime.SymbolDropDown = 61555;
            this.dtpStartTime.SymbolNormal = 61555;
            this.dtpStartTime.SymbolSize = 24;
            this.dtpStartTime.TabIndex = 401;
            this.dtpStartTime.Text = "2020-06-02 17:57:28";
            this.dtpStartTime.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.dtpStartTime.Value = new System.DateTime(2020, 6, 2, 17, 57, 28, 203);
            this.dtpStartTime.Watermark = "";
            // 
            // uiLabel1
            // 
            this.uiLabel1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.uiLabel1.Location = new System.Drawing.Point(383, 55);
            this.uiLabel1.Name = "uiLabel1";
            this.uiLabel1.Size = new System.Drawing.Size(75, 23);
            this.uiLabel1.TabIndex = 650;
            this.uiLabel1.Text = "结束时间";
            this.uiLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtpEndTime
            // 
            this.dtpEndTime.CanEmpty = true;
            this.dtpEndTime.DateCultureInfo = new System.Globalization.CultureInfo("zh-CN");
            this.dtpEndTime.FillColor = System.Drawing.Color.White;
            this.dtpEndTime.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dtpEndTime.Location = new System.Drawing.Point(465, 52);
            this.dtpEndTime.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dtpEndTime.MaxLength = 19;
            this.dtpEndTime.MinimumSize = new System.Drawing.Size(63, 0);
            this.dtpEndTime.Name = "dtpEndTime";
            this.dtpEndTime.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.dtpEndTime.Size = new System.Drawing.Size(199, 29);
            this.dtpEndTime.SymbolDropDown = 61555;
            this.dtpEndTime.SymbolNormal = 61555;
            this.dtpEndTime.SymbolSize = 24;
            this.dtpEndTime.TabIndex = 651;
            this.dtpEndTime.Text = "2020-06-02 17:57:28";
            this.dtpEndTime.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.dtpEndTime.Value = new System.DateTime(2020, 6, 2, 17, 57, 28, 203);
            this.dtpEndTime.Watermark = "";
            // 
            // btnSelctModel
            // 
            this.btnSelctModel.BackColor = System.Drawing.Color.Silver;
            this.btnSelctModel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSelctModel.FalseColor = System.Drawing.Color.Silver;
            this.btnSelctModel.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSelctModel.Location = new System.Drawing.Point(242, 9);
            this.btnSelctModel.Name = "btnSelctModel";
            this.btnSelctModel.Size = new System.Drawing.Size(90, 73);
            this.btnSelctModel.SwitchType = RW.UI.Controls.SwitchStyleEnums.Switch;
            this.btnSelctModel.TabIndex = 675;
            this.btnSelctModel.Tag = "";
            this.btnSelctModel.Text = "型号选择";
            this.btnSelctModel.TrueColor = System.Drawing.Color.Lime;
            this.btnSelctModel.Click += new System.EventHandler(this.btnSelctModel_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.Silver;
            this.btnSearch.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSearch.FalseColor = System.Drawing.Color.Silver;
            this.btnSearch.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSearch.Location = new System.Drawing.Point(782, 9);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(120, 72);
            this.btnSearch.SwitchType = RW.UI.Controls.SwitchStyleEnums.Switch;
            this.btnSearch.TabIndex = 676;
            this.btnSearch.Tag = "";
            this.btnSearch.Text = "查询";
            this.btnSearch.TrueColor = System.Drawing.Color.Lime;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnUpPage
            // 
            this.btnUpPage.BackColor = System.Drawing.Color.Silver;
            this.btnUpPage.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnUpPage.FalseColor = System.Drawing.Color.Silver;
            this.btnUpPage.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnUpPage.Location = new System.Drawing.Point(1582, 9);
            this.btnUpPage.Name = "btnUpPage";
            this.btnUpPage.Size = new System.Drawing.Size(106, 72);
            this.btnUpPage.SwitchType = RW.UI.Controls.SwitchStyleEnums.Switch;
            this.btnUpPage.TabIndex = 677;
            this.btnUpPage.Tag = "";
            this.btnUpPage.Text = "上一页";
            this.btnUpPage.TrueColor = System.Drawing.Color.Lime;
            this.btnUpPage.Click += new System.EventHandler(this.btnUpPage_Click);
            // 
            // btnNextPage
            // 
            this.btnNextPage.BackColor = System.Drawing.Color.Silver;
            this.btnNextPage.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnNextPage.FalseColor = System.Drawing.Color.Silver;
            this.btnNextPage.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnNextPage.Location = new System.Drawing.Point(1709, 9);
            this.btnNextPage.Name = "btnNextPage";
            this.btnNextPage.Size = new System.Drawing.Size(106, 72);
            this.btnNextPage.SwitchType = RW.UI.Controls.SwitchStyleEnums.Switch;
            this.btnNextPage.TabIndex = 679;
            this.btnNextPage.Tag = "";
            this.btnNextPage.Text = "下一页";
            this.btnNextPage.TrueColor = System.Drawing.Color.Lime;
            this.btnNextPage.Click += new System.EventHandler(this.btnNextPage_Click);
            // 
            // lblTotalNum
            // 
            this.lblTotalNum.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTotalNum.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblTotalNum.Location = new System.Drawing.Point(1439, 55);
            this.lblTotalNum.Name = "lblTotalNum";
            this.lblTotalNum.Size = new System.Drawing.Size(116, 23);
            this.lblTotalNum.TabIndex = 680;
            this.lblTotalNum.Text = "共 0 条";
            this.lblTotalNum.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnOut
            // 
            this.btnOut.BackColor = System.Drawing.Color.Silver;
            this.btnOut.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnOut.FalseColor = System.Drawing.Color.Silver;
            this.btnOut.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOut.Location = new System.Drawing.Point(946, 8);
            this.btnOut.Name = "btnOut";
            this.btnOut.Size = new System.Drawing.Size(122, 73);
            this.btnOut.SwitchType = RW.UI.Controls.SwitchStyleEnums.Switch;
            this.btnOut.TabIndex = 681;
            this.btnOut.Tag = "";
            this.btnOut.Text = "导出数据";
            this.btnOut.TrueColor = System.Drawing.Color.Lime;
            this.btnOut.Click += new System.EventHandler(this.btnOut_Click);
            // 
            // rdoManual
            // 
            this.rdoManual.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rdoManual.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.rdoManual.Location = new System.Drawing.Point(671, 52);
            this.rdoManual.MinimumSize = new System.Drawing.Size(1, 1);
            this.rdoManual.Name = "rdoManual";
            this.rdoManual.Padding = new System.Windows.Forms.Padding(26, 0, 0, 0);
            this.rdoManual.Size = new System.Drawing.Size(104, 29);
            this.rdoManual.TabIndex = 849;
            this.rdoManual.Text = "手动";
            // 
            // rdoAuto
            // 
            this.rdoAuto.Checked = true;
            this.rdoAuto.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rdoAuto.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.rdoAuto.Location = new System.Drawing.Point(671, 9);
            this.rdoAuto.MinimumSize = new System.Drawing.Size(1, 1);
            this.rdoAuto.Name = "rdoAuto";
            this.rdoAuto.Padding = new System.Windows.Forms.Padding(26, 0, 0, 0);
            this.rdoAuto.Size = new System.Drawing.Size(104, 29);
            this.rdoAuto.TabIndex = 848;
            this.rdoAuto.Text = "自动";
            // 
            // ucAutoRecord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.Controls.Add(this.rdoManual);
            this.Controls.Add(this.rdoAuto);
            this.Controls.Add(this.btnOut);
            this.Controls.Add(this.lblTotalNum);
            this.Controls.Add(this.btnUpPage);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.btnNextPage);
            this.Controls.Add(this.btnSelctModel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dtpEndTime);
            this.Controls.Add(this.uiLabel1);
            this.Controls.Add(this.dtpStartTime);
            this.Controls.Add(this.cboModel);
            this.Controls.Add(this.uiLabel4);
            this.Controls.Add(this.txtNumber);
            this.Controls.Add(this.uiLabel3);
            this.Controls.Add(this.uiLabel2);
            this.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ucAutoRecord";
            this.Size = new System.Drawing.Size(1828, 997);
            this.Load += new System.EventHandler(this.ucAutoRecord_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecord)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UITextBox txtNumber;
        private Sunny.UI.UILabel uiLabel3;
        private Sunny.UI.UILabel uiLabel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgvRecord;
        private Sunny.UI.UILabel uiLabel4;
        private Sunny.UI.UITextBox cboModel;
        private Sunny.UI.UIDatetimePicker dtpStartTime;
        private Sunny.UI.UILabel uiLabel1;
        private Sunny.UI.UIDatetimePicker dtpEndTime;
        private RW.UI.Controls.RButton btnSelctModel;
        private RW.UI.Controls.RButton btnSearch;
        private RW.UI.Controls.RButton btnUpPage;
        private RW.UI.Controls.RButton btnNextPage;
        private Sunny.UI.UILabel lblTotalNum;
        private RW.UI.Controls.RButton btnOut;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn14;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn15;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn16;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn17;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn18;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn19;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn20;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn21;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn22;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn23;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn24;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn25;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn26;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn27;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn28;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn29;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn30;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn31;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn32;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn33;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn34;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn35;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn36;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn37;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn38;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn39;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn40;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn41;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn42;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn43;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn44;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn45;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn46;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn47;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn48;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn49;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn50;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn51;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn52;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn53;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn54;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn55;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn56;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn57;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn58;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn59;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn60;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn61;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn62;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn63;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn64;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn65;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn66;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn67;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn68;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn69;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn70;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn71;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn72;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn73;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn74;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn75;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn76;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn77;
        private Sunny.UI.UIRadioButton rdoManual;
        private Sunny.UI.UIRadioButton rdoAuto;
    }
}
