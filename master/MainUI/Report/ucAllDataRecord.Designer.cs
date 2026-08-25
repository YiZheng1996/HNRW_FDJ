
using System.Windows.Forms;

namespace MainUI.Report
{
    partial class ucAllDataRecord
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
            this.allDataRecord = new System.Windows.Forms.DataGridView();
            this.uiLabel4 = new Sunny.UI.UILabel();
            this.cboModel = new Sunny.UI.UITextBox();
            this.dtpStartTime = new Sunny.UI.UIDatetimePicker();
            this.uiLabel1 = new Sunny.UI.UILabel();
            this.dtpEndTime = new Sunny.UI.UIDatetimePicker();
            this.btnSelctModel = new RW.UI.Controls.RButton();
            this.btnSearch = new RW.UI.Controls.RButton();
            this.btnUpPage = new RW.UI.Controls.RButton();
            this.btnNextPage = new RW.UI.Controls.RButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.BaseDataGrp = new System.Windows.Forms.CheckBox();
            this.ChoiceAll = new System.Windows.Forms.CheckBox();
            this.EngineOilDataGrp = new System.Windows.Forms.CheckBox();
            this.GD350_1Data = new System.Windows.Forms.CheckBox();
            this.AirDuctData1Grp = new System.Windows.Forms.CheckBox();
            this.AirDuctData2Grp = new System.Windows.Forms.CheckBox();
            this.PipelineFaultDataGrp = new System.Windows.Forms.CheckBox();
            this.ExChangeDataGrpBool = new System.Windows.Forms.CheckBox();
            this.ExChangeDataGrpDouble = new System.Windows.Forms.CheckBox();
            this.SpeedDataGrp = new System.Windows.Forms.CheckBox();
            this.DODataGrp = new System.Windows.Forms.CheckBox();
            this.StartPLCDataGrp = new System.Windows.Forms.CheckBox();
            this.DIDataGrp = new System.Windows.Forms.CheckBox();
            this.PLC2AIDataGrp = new System.Windows.Forms.CheckBox();
            this.AODataGrp = new System.Windows.Forms.CheckBox();
            this.WaterDataGrp = new System.Windows.Forms.CheckBox();
            this.AIDataGrp = new System.Windows.Forms.CheckBox();
            this.ThreePhaseElectricData = new System.Windows.Forms.CheckBox();
            this.TRDPDataGrp = new System.Windows.Forms.CheckBox();
            this.FuelDataGrp = new System.Windows.Forms.CheckBox();
            this.lblTotalNum = new Sunny.UI.UITextBox();
            this.pageNO = new Sunny.UI.UITextBox();
            this.pageSize = new Sunny.UI.UITextBox();
            this.uiLabel5 = new Sunny.UI.UILabel();
            this.reportSave = new RW.UI.Controls.RButton();
            this.reportSaveAll = new RW.UI.Controls.RButton();
            this.tabDataRecord = new System.Windows.Forms.TabControl();
            this.tabPageAllData = new System.Windows.Forms.TabPage();
            this.panelPagingAllData = new System.Windows.Forms.Panel();
            this.tabPageStartup = new System.Windows.Forms.TabPage();
            this.panelStartup = new System.Windows.Forms.Panel();
            this.dgvStartupRecord = new System.Windows.Forms.DataGridView();
            this.colStartupIndex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStartupRecordTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStartupType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStartupRPM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStartupTorque = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStartupPower = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStartupExcitationVoltage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStartupExcitationCurrent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStartupInvertVoltage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStartupInvertCurrent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStartupInvertRPM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStartupInvertPower = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStartupInvertFaultCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelPagingStartup = new System.Windows.Forms.Panel();
            this.pageSize1 = new Sunny.UI.UITextBox();
            this.uiLabel6 = new Sunny.UI.UILabel();
            this.lblTotalNum1 = new Sunny.UI.UITextBox();
            this.pageNO1 = new Sunny.UI.UITextBox();
            this.btnUpPage1 = new RW.UI.Controls.RButton();
            this.btnDownPag1 = new RW.UI.Controls.RButton();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.allDataRecord)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tabDataRecord.SuspendLayout();
            this.tabPageAllData.SuspendLayout();
            this.panelPagingAllData.SuspendLayout();
            this.tabPageStartup.SuspendLayout();
            this.panelStartup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStartupRecord)).BeginInit();
            this.panelPagingStartup.SuspendLayout();
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
            this.panel1.Controls.Add(this.allDataRecord);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1786, 724);
            this.panel1.TabIndex = 393;
            // 
            // allDataRecord
            // 
            this.allDataRecord.AllowUserToAddRows = false;
            this.allDataRecord.AllowUserToDeleteRows = false;
            this.allDataRecord.BackgroundColor = System.Drawing.Color.White;
            this.allDataRecord.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.allDataRecord.Dock = System.Windows.Forms.DockStyle.Fill;
            this.allDataRecord.EnableHeadersVisualStyles = false;
            this.allDataRecord.Location = new System.Drawing.Point(0, 0);
            this.allDataRecord.Name = "allDataRecord";
            this.allDataRecord.ReadOnly = true;
            this.allDataRecord.RowTemplate.Height = 23;
            this.allDataRecord.Size = new System.Drawing.Size(1786, 724);
            this.allDataRecord.TabIndex = 849;
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
            this.btnSearch.Location = new System.Drawing.Point(686, 9);
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
            this.btnUpPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpPage.BackColor = System.Drawing.Color.Silver;
            this.btnUpPage.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnUpPage.FalseColor = System.Drawing.Color.Silver;
            this.btnUpPage.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnUpPage.Location = new System.Drawing.Point(1553, 9);
            this.btnUpPage.Name = "btnUpPage";
            this.btnUpPage.Size = new System.Drawing.Size(106, 30);
            this.btnUpPage.SwitchType = RW.UI.Controls.SwitchStyleEnums.Switch;
            this.btnUpPage.TabIndex = 851;
            this.btnUpPage.Tag = "";
            this.btnUpPage.Text = "上一页";
            this.btnUpPage.TrueColor = System.Drawing.Color.Lime;
            this.btnUpPage.Click += new System.EventHandler(this.btnUpPage_Click);
            // 
            // btnNextPage
            // 
            this.btnNextPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNextPage.BackColor = System.Drawing.Color.Silver;
            this.btnNextPage.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnNextPage.FalseColor = System.Drawing.Color.Silver;
            this.btnNextPage.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnNextPage.Location = new System.Drawing.Point(1671, 9);
            this.btnNextPage.Name = "btnNextPage";
            this.btnNextPage.Size = new System.Drawing.Size(106, 30);
            this.btnNextPage.SwitchType = RW.UI.Controls.SwitchStyleEnums.Switch;
            this.btnNextPage.TabIndex = 850;
            this.btnNextPage.Tag = "";
            this.btnNextPage.Text = "下一页";
            this.btnNextPage.TrueColor = System.Drawing.Color.Lime;
            this.btnNextPage.Click += new System.EventHandler(this.btnNextPage_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.BaseDataGrp);
            this.groupBox1.Controls.Add(this.ChoiceAll);
            this.groupBox1.Controls.Add(this.EngineOilDataGrp);
            this.groupBox1.Controls.Add(this.GD350_1Data);
            this.groupBox1.Controls.Add(this.AirDuctData1Grp);
            this.groupBox1.Controls.Add(this.AirDuctData2Grp);
            this.groupBox1.Controls.Add(this.PipelineFaultDataGrp);
            this.groupBox1.Controls.Add(this.ExChangeDataGrpBool);
            this.groupBox1.Controls.Add(this.ExChangeDataGrpDouble);
            this.groupBox1.Controls.Add(this.SpeedDataGrp);
            this.groupBox1.Controls.Add(this.DODataGrp);
            this.groupBox1.Controls.Add(this.StartPLCDataGrp);
            this.groupBox1.Controls.Add(this.DIDataGrp);
            this.groupBox1.Controls.Add(this.PLC2AIDataGrp);
            this.groupBox1.Controls.Add(this.AODataGrp);
            this.groupBox1.Controls.Add(this.WaterDataGrp);
            this.groupBox1.Controls.Add(this.AIDataGrp);
            this.groupBox1.Controls.Add(this.ThreePhaseElectricData);
            this.groupBox1.Controls.Add(this.TRDPDataGrp);
            this.groupBox1.Controls.Add(this.FuelDataGrp);
            this.groupBox1.Location = new System.Drawing.Point(10, 918);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1860, 79);
            this.groupBox1.TabIndex = 852;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "选择项点";
            // 
            // BaseDataGrp
            // 
            this.BaseDataGrp.AutoSize = true;
            this.BaseDataGrp.Location = new System.Drawing.Point(6, 23);
            this.BaseDataGrp.Name = "BaseDataGrp";
            this.BaseDataGrp.Size = new System.Drawing.Size(93, 25);
            this.BaseDataGrp.TabIndex = 18;
            this.BaseDataGrp.Text = "基础数据";
            this.BaseDataGrp.UseVisualStyleBackColor = true;
            // 
            // ChoiceAll
            // 
            this.ChoiceAll.AutoSize = true;
            this.ChoiceAll.Location = new System.Drawing.Point(1723, 51);
            this.ChoiceAll.Name = "ChoiceAll";
            this.ChoiceAll.Size = new System.Drawing.Size(61, 25);
            this.ChoiceAll.TabIndex = 17;
            this.ChoiceAll.Text = "全选";
            this.ChoiceAll.UseVisualStyleBackColor = true;
            this.ChoiceAll.CheckedChanged += new System.EventHandler(this.Choice_All);
            // 
            // EngineOilDataGrp
            // 
            this.EngineOilDataGrp.AutoSize = true;
            this.EngineOilDataGrp.Location = new System.Drawing.Point(6, 54);
            this.EngineOilDataGrp.Name = "EngineOilDataGrp";
            this.EngineOilDataGrp.Size = new System.Drawing.Size(141, 25);
            this.EngineOilDataGrp.TabIndex = 16;
            this.EngineOilDataGrp.Text = "发动机机油数据";
            this.EngineOilDataGrp.UseVisualStyleBackColor = true;
            // 
            // GD350_1Data
            // 
            this.GD350_1Data.AutoSize = true;
            this.GD350_1Data.Location = new System.Drawing.Point(1330, 54);
            this.GD350_1Data.Name = "GD350_1Data";
            this.GD350_1Data.Size = new System.Drawing.Size(109, 25);
            this.GD350_1Data.TabIndex = 15;
            this.GD350_1Data.Text = "启动柜数据";
            this.GD350_1Data.UseVisualStyleBackColor = true;
            // 
            // AirDuctData1Grp
            // 
            this.AirDuctData1Grp.AutoSize = true;
            this.AirDuctData1Grp.Location = new System.Drawing.Point(1519, 23);
            this.AirDuctData1Grp.Name = "AirDuctData1Grp";
            this.AirDuctData1Grp.Size = new System.Drawing.Size(157, 25);
            this.AirDuctData1Grp.TabIndex = 19;
            this.AirDuctData1Grp.Text = "一号风道加热数据";
            this.AirDuctData1Grp.UseVisualStyleBackColor = true;
            // 
            // AirDuctData2Grp
            // 
            this.AirDuctData2Grp.AutoSize = true;
            this.AirDuctData2Grp.Location = new System.Drawing.Point(1519, 51);
            this.AirDuctData2Grp.Name = "AirDuctData2Grp";
            this.AirDuctData2Grp.Size = new System.Drawing.Size(157, 25);
            this.AirDuctData2Grp.TabIndex = 20;
            this.AirDuctData2Grp.Text = "二号风道加热数据";
            this.AirDuctData2Grp.UseVisualStyleBackColor = true;
            // 
            // PipelineFaultDataGrp
            // 
            this.PipelineFaultDataGrp.AutoSize = true;
            this.PipelineFaultDataGrp.Location = new System.Drawing.Point(1723, 23);
            this.PipelineFaultDataGrp.Name = "PipelineFaultDataGrp";
            this.PipelineFaultDataGrp.Size = new System.Drawing.Size(61, 25);
            this.PipelineFaultDataGrp.TabIndex = 14;
            this.PipelineFaultDataGrp.Text = "故障";
            this.PipelineFaultDataGrp.UseVisualStyleBackColor = true;
            // 
            // ExChangeDataGrpBool
            // 
            this.ExChangeDataGrpBool.AutoSize = true;
            this.ExChangeDataGrpBool.Location = new System.Drawing.Point(1330, 23);
            this.ExChangeDataGrpBool.Name = "ExChangeDataGrpBool";
            this.ExChangeDataGrpBool.Size = new System.Drawing.Size(141, 25);
            this.ExChangeDataGrpBool.TabIndex = 13;
            this.ExChangeDataGrpBool.Text = "交互开关量输入";
            this.ExChangeDataGrpBool.UseVisualStyleBackColor = true;
            // 
            // ExChangeDataGrpDouble
            // 
            this.ExChangeDataGrpDouble.AutoSize = true;
            this.ExChangeDataGrpDouble.Location = new System.Drawing.Point(1136, 23);
            this.ExChangeDataGrpDouble.Name = "ExChangeDataGrpDouble";
            this.ExChangeDataGrpDouble.Size = new System.Drawing.Size(141, 25);
            this.ExChangeDataGrpDouble.TabIndex = 12;
            this.ExChangeDataGrpDouble.Text = "交互模拟量输入";
            this.ExChangeDataGrpDouble.UseVisualStyleBackColor = true;
            // 
            // SpeedDataGrp
            // 
            this.SpeedDataGrp.AutoSize = true;
            this.SpeedDataGrp.Location = new System.Drawing.Point(1136, 54);
            this.SpeedDataGrp.Name = "SpeedDataGrp";
            this.SpeedDataGrp.Size = new System.Drawing.Size(125, 25);
            this.SpeedDataGrp.TabIndex = 11;
            this.SpeedDataGrp.Text = "转速模块数据";
            this.SpeedDataGrp.UseVisualStyleBackColor = true;
            // 
            // DODataGrp
            // 
            this.DODataGrp.AutoSize = true;
            this.DODataGrp.Location = new System.Drawing.Point(942, 23);
            this.DODataGrp.Name = "DODataGrp";
            this.DODataGrp.Size = new System.Drawing.Size(157, 25);
            this.DODataGrp.TabIndex = 10;
            this.DODataGrp.Text = "实验台开关量输出";
            this.DODataGrp.UseVisualStyleBackColor = true;
            // 
            // StartPLCDataGrp
            // 
            this.StartPLCDataGrp.AutoSize = true;
            this.StartPLCDataGrp.Location = new System.Drawing.Point(942, 54);
            this.StartPLCDataGrp.Name = "StartPLCDataGrp";
            this.StartPLCDataGrp.Size = new System.Drawing.Size(109, 25);
            this.StartPLCDataGrp.TabIndex = 9;
            this.StartPLCDataGrp.Text = "变频器数据";
            this.StartPLCDataGrp.UseVisualStyleBackColor = true;
            // 
            // DIDataGrp
            // 
            this.DIDataGrp.AutoSize = true;
            this.DIDataGrp.Location = new System.Drawing.Point(736, 23);
            this.DIDataGrp.Name = "DIDataGrp";
            this.DIDataGrp.Size = new System.Drawing.Size(157, 25);
            this.DIDataGrp.TabIndex = 8;
            this.DIDataGrp.Text = "实验台开关量输入";
            this.DIDataGrp.UseVisualStyleBackColor = true;
            // 
            // PLC2AIDataGrp
            // 
            this.PLC2AIDataGrp.AutoSize = true;
            this.PLC2AIDataGrp.Location = new System.Drawing.Point(736, 54);
            this.PLC2AIDataGrp.Name = "PLC2AIDataGrp";
            this.PLC2AIDataGrp.Size = new System.Drawing.Size(166, 25);
            this.PLC2AIDataGrp.TabIndex = 7;
            this.PLC2AIDataGrp.Text = "实验台模拟量输出2";
            this.PLC2AIDataGrp.UseVisualStyleBackColor = true;
            // 
            // AODataGrp
            // 
            this.AODataGrp.AutoSize = true;
            this.AODataGrp.Location = new System.Drawing.Point(535, 23);
            this.AODataGrp.Name = "AODataGrp";
            this.AODataGrp.Size = new System.Drawing.Size(166, 25);
            this.AODataGrp.TabIndex = 6;
            this.AODataGrp.Text = "实验台模拟量输出1";
            this.AODataGrp.UseVisualStyleBackColor = true;
            // 
            // WaterDataGrp
            // 
            this.WaterDataGrp.AutoSize = true;
            this.WaterDataGrp.Location = new System.Drawing.Point(535, 51);
            this.WaterDataGrp.Name = "WaterDataGrp";
            this.WaterDataGrp.Size = new System.Drawing.Size(109, 25);
            this.WaterDataGrp.TabIndex = 5;
            this.WaterDataGrp.Text = "水系统数据";
            this.WaterDataGrp.UseVisualStyleBackColor = true;
            // 
            // AIDataGrp
            // 
            this.AIDataGrp.AutoSize = true;
            this.AIDataGrp.Location = new System.Drawing.Point(347, 23);
            this.AIDataGrp.Name = "AIDataGrp";
            this.AIDataGrp.Size = new System.Drawing.Size(157, 25);
            this.AIDataGrp.TabIndex = 4;
            this.AIDataGrp.Text = "实验台模拟量输入";
            this.AIDataGrp.UseVisualStyleBackColor = true;
            // 
            // ThreePhaseElectricData
            // 
            this.ThreePhaseElectricData.AutoSize = true;
            this.ThreePhaseElectricData.Location = new System.Drawing.Point(347, 54);
            this.ThreePhaseElectricData.Name = "ThreePhaseElectricData";
            this.ThreePhaseElectricData.Size = new System.Drawing.Size(125, 25);
            this.ThreePhaseElectricData.TabIndex = 3;
            this.ThreePhaseElectricData.Text = "发动机电数据";
            this.ThreePhaseElectricData.UseVisualStyleBackColor = true;
            // 
            // TRDPDataGrp
            // 
            this.TRDPDataGrp.AutoSize = true;
            this.TRDPDataGrp.Location = new System.Drawing.Point(181, 23);
            this.TRDPDataGrp.Name = "TRDPDataGrp";
            this.TRDPDataGrp.Size = new System.Drawing.Size(141, 25);
            this.TRDPDataGrp.TabIndex = 2;
            this.TRDPDataGrp.Text = "发动机数据输出";
            this.TRDPDataGrp.UseVisualStyleBackColor = true;
            // 
            // FuelDataGrp
            // 
            this.FuelDataGrp.AutoSize = true;
            this.FuelDataGrp.Location = new System.Drawing.Point(181, 54);
            this.FuelDataGrp.Name = "FuelDataGrp";
            this.FuelDataGrp.Size = new System.Drawing.Size(93, 25);
            this.FuelDataGrp.TabIndex = 1;
            this.FuelDataGrp.Text = "燃油数据";
            this.FuelDataGrp.UseVisualStyleBackColor = true;
            // 
            // lblTotalNum
            // 
            this.lblTotalNum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotalNum.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTotalNum.Location = new System.Drawing.Point(1301, 9);
            this.lblTotalNum.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lblTotalNum.MinimumSize = new System.Drawing.Size(1, 16);
            this.lblTotalNum.Name = "lblTotalNum";
            this.lblTotalNum.Padding = new System.Windows.Forms.Padding(5);
            this.lblTotalNum.ReadOnly = true;
            this.lblTotalNum.ShowText = false;
            this.lblTotalNum.Size = new System.Drawing.Size(112, 30);
            this.lblTotalNum.TabIndex = 853;
            this.lblTotalNum.Text = "共0条";
            this.lblTotalNum.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTotalNum.Watermark = "";
            // 
            // pageNO
            // 
            this.pageNO.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pageNO.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pageNO.Location = new System.Drawing.Point(1429, 9);
            this.pageNO.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pageNO.MinimumSize = new System.Drawing.Size(1, 16);
            this.pageNO.Name = "pageNO";
            this.pageNO.Padding = new System.Windows.Forms.Padding(5);
            this.pageNO.ReadOnly = true;
            this.pageNO.ShowText = false;
            this.pageNO.Size = new System.Drawing.Size(112, 30);
            this.pageNO.TabIndex = 854;
            this.pageNO.Text = "第0页/共0页";
            this.pageNO.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.pageNO.Watermark = "";
            // 
            // pageSize
            // 
            this.pageSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pageSize.DoubleValue = 200D;
            this.pageSize.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pageSize.IntValue = 200;
            this.pageSize.Location = new System.Drawing.Point(1182, 9);
            this.pageSize.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pageSize.MinimumSize = new System.Drawing.Size(1, 16);
            this.pageSize.Name = "pageSize";
            this.pageSize.Padding = new System.Windows.Forms.Padding(5);
            this.pageSize.ShowText = false;
            this.pageSize.Size = new System.Drawing.Size(43, 30);
            this.pageSize.TabIndex = 855;
            this.pageSize.Text = "200";
            this.pageSize.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.pageSize.Watermark = "";
            this.pageSize.TextChanged += new System.EventHandler(this.pageSize_Change);
            // 
            // uiLabel5
            // 
            this.uiLabel5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.uiLabel5.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.uiLabel5.Location = new System.Drawing.Point(1232, 5);
            this.uiLabel5.Name = "uiLabel5";
            this.uiLabel5.Size = new System.Drawing.Size(62, 38);
            this.uiLabel5.TabIndex = 856;
            this.uiLabel5.Text = "条/页";
            this.uiLabel5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // reportSave
            // 
            this.reportSave.BackColor = System.Drawing.Color.Silver;
            this.reportSave.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.reportSave.FalseColor = System.Drawing.Color.Silver;
            this.reportSave.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.reportSave.Location = new System.Drawing.Point(1481, 10);
            this.reportSave.Name = "reportSave";
            this.reportSave.Size = new System.Drawing.Size(120, 72);
            this.reportSave.SwitchType = RW.UI.Controls.SwitchStyleEnums.Switch;
            this.reportSave.TabIndex = 857;
            this.reportSave.Tag = "";
            this.reportSave.Text = "导出本页";
            this.reportSave.TrueColor = System.Drawing.Color.Lime;
            this.reportSave.Click += new System.EventHandler(this.Report_Save);
            // 
            // reportSaveAll
            // 
            this.reportSaveAll.BackColor = System.Drawing.Color.Silver;
            this.reportSaveAll.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.reportSaveAll.FalseColor = System.Drawing.Color.Silver;
            this.reportSaveAll.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.reportSaveAll.Location = new System.Drawing.Point(1638, 10);
            this.reportSaveAll.Name = "reportSaveAll";
            this.reportSaveAll.Size = new System.Drawing.Size(120, 72);
            this.reportSaveAll.SwitchType = RW.UI.Controls.SwitchStyleEnums.Switch;
            this.reportSaveAll.TabIndex = 858;
            this.reportSaveAll.Tag = "";
            this.reportSaveAll.Text = "导出全部";
            this.reportSaveAll.TrueColor = System.Drawing.Color.Lime;
            this.reportSaveAll.Click += new System.EventHandler(this.Report_Save_All);
            // 
            // tabDataRecord
            // 
            this.tabDataRecord.Controls.Add(this.tabPageAllData);
            this.tabDataRecord.Controls.Add(this.tabPageStartup);
            this.tabDataRecord.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabDataRecord.ItemSize = new System.Drawing.Size(160, 36);
            this.tabDataRecord.Location = new System.Drawing.Point(10, 92);
            this.tabDataRecord.Name = "tabDataRecord";
            this.tabDataRecord.SelectedIndex = 0;
            this.tabDataRecord.Size = new System.Drawing.Size(1800, 820);
            this.tabDataRecord.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabDataRecord.TabIndex = 859;
            // 
            // tabPageAllData
            // 
            this.tabPageAllData.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.tabPageAllData.Controls.Add(this.panel1);
            this.tabPageAllData.Controls.Add(this.panelPagingAllData);
            this.tabPageAllData.Location = new System.Drawing.Point(4, 40);
            this.tabPageAllData.Name = "tabPageAllData";
            this.tabPageAllData.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAllData.Size = new System.Drawing.Size(1792, 776);
            this.tabPageAllData.TabIndex = 0;
            this.tabPageAllData.Text = "总数据记录表";
            // 
            // panelPagingAllData
            // 
            this.panelPagingAllData.Controls.Add(this.pageSize);
            this.panelPagingAllData.Controls.Add(this.uiLabel5);
            this.panelPagingAllData.Controls.Add(this.lblTotalNum);
            this.panelPagingAllData.Controls.Add(this.pageNO);
            this.panelPagingAllData.Controls.Add(this.btnUpPage);
            this.panelPagingAllData.Controls.Add(this.btnNextPage);
            this.panelPagingAllData.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelPagingAllData.Location = new System.Drawing.Point(3, 727);
            this.panelPagingAllData.Name = "panelPagingAllData";
            this.panelPagingAllData.Size = new System.Drawing.Size(1786, 46);
            this.panelPagingAllData.TabIndex = 860;
            // 
            // tabPageStartup
            // 
            this.tabPageStartup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.tabPageStartup.Controls.Add(this.panelStartup);
            this.tabPageStartup.Controls.Add(this.panelPagingStartup);
            this.tabPageStartup.Location = new System.Drawing.Point(4, 40);
            this.tabPageStartup.Name = "tabPageStartup";
            this.tabPageStartup.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageStartup.Size = new System.Drawing.Size(1792, 776);
            this.tabPageStartup.TabIndex = 1;
            this.tabPageStartup.Text = "启动柜数据表";
            // 
            // panelStartup
            // 
            this.panelStartup.Controls.Add(this.dgvStartupRecord);
            this.panelStartup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelStartup.Location = new System.Drawing.Point(3, 3);
            this.panelStartup.Name = "panelStartup";
            this.panelStartup.Size = new System.Drawing.Size(1786, 724);
            this.panelStartup.TabIndex = 0;
            // 
            // dgvStartupRecord
            // 
            this.dgvStartupRecord.AllowUserToAddRows = false;
            this.dgvStartupRecord.AllowUserToDeleteRows = false;
            this.dgvStartupRecord.BackgroundColor = System.Drawing.Color.White;
            this.dgvStartupRecord.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStartupRecord.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colStartupIndex,
            this.colStartupRecordTime,
            this.colStartupType,
            this.colStartupRPM,
            this.colStartupTorque,
            this.colStartupPower,
            this.colStartupExcitationVoltage,
            this.colStartupExcitationCurrent,
            this.colStartupInvertVoltage,
            this.colStartupInvertCurrent,
            this.colStartupInvertRPM,
            this.colStartupInvertPower,
            this.colStartupInvertFaultCode});
            this.dgvStartupRecord.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvStartupRecord.EnableHeadersVisualStyles = false;
            this.dgvStartupRecord.Location = new System.Drawing.Point(0, 0);
            this.dgvStartupRecord.Name = "dgvStartupRecord";
            this.dgvStartupRecord.ReadOnly = true;
            this.dgvStartupRecord.RowHeadersVisible = false;
            this.dgvStartupRecord.RowTemplate.Height = 23;
            this.dgvStartupRecord.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvStartupRecord.Size = new System.Drawing.Size(1786, 724);
            this.dgvStartupRecord.TabIndex = 0;
            // 
            // colStartupIndex
            // 
            this.colStartupIndex.HeaderText = "序号";
            this.colStartupIndex.Name = "colStartupIndex";
            this.colStartupIndex.ReadOnly = true;
            this.colStartupIndex.Width = 70;
            // 
            // colStartupRecordTime
            // 
            this.colStartupRecordTime.HeaderText = "记录时间";
            this.colStartupRecordTime.Name = "colStartupRecordTime";
            this.colStartupRecordTime.ReadOnly = true;
            this.colStartupRecordTime.Width = 160;
            // 
            // colStartupType
            // 
            this.colStartupType.HeaderText = "类型";
            this.colStartupType.Name = "colStartupType";
            this.colStartupType.ReadOnly = true;
            this.colStartupType.Width = 80;
            // 
            // colStartupRPM
            // 
            this.colStartupRPM.HeaderText = "转速";
            this.colStartupRPM.Name = "colStartupRPM";
            this.colStartupRPM.ReadOnly = true;
            // 
            // colStartupTorque
            // 
            this.colStartupTorque.HeaderText = "扭矩";
            this.colStartupTorque.Name = "colStartupTorque";
            this.colStartupTorque.ReadOnly = true;
            // 
            // colStartupPower
            // 
            this.colStartupPower.HeaderText = "功率";
            this.colStartupPower.Name = "colStartupPower";
            this.colStartupPower.ReadOnly = true;
            // 
            // colStartupExcitationVoltage
            // 
            this.colStartupExcitationVoltage.HeaderText = "励磁电压";
            this.colStartupExcitationVoltage.Name = "colStartupExcitationVoltage";
            this.colStartupExcitationVoltage.ReadOnly = true;
            // 
            // colStartupExcitationCurrent
            // 
            this.colStartupExcitationCurrent.HeaderText = "励磁电流";
            this.colStartupExcitationCurrent.Name = "colStartupExcitationCurrent";
            this.colStartupExcitationCurrent.ReadOnly = true;
            // 
            // colStartupInvertVoltage
            // 
            this.colStartupInvertVoltage.HeaderText = "变频电压";
            this.colStartupInvertVoltage.Name = "colStartupInvertVoltage";
            this.colStartupInvertVoltage.ReadOnly = true;
            // 
            // colStartupInvertCurrent
            // 
            this.colStartupInvertCurrent.HeaderText = "变频电流";
            this.colStartupInvertCurrent.Name = "colStartupInvertCurrent";
            this.colStartupInvertCurrent.ReadOnly = true;
            // 
            // colStartupInvertRPM
            // 
            this.colStartupInvertRPM.HeaderText = "变频转速";
            this.colStartupInvertRPM.Name = "colStartupInvertRPM";
            this.colStartupInvertRPM.ReadOnly = true;
            // 
            // colStartupInvertPower
            // 
            this.colStartupInvertPower.HeaderText = "变频功率";
            this.colStartupInvertPower.Name = "colStartupInvertPower";
            this.colStartupInvertPower.ReadOnly = true;
            // 
            // colStartupInvertFaultCode
            // 
            this.colStartupInvertFaultCode.HeaderText = "故障代码";
            this.colStartupInvertFaultCode.Name = "colStartupInvertFaultCode";
            this.colStartupInvertFaultCode.ReadOnly = true;
            // 
            // panelPagingStartup
            // 
            this.panelPagingStartup.Controls.Add(this.pageSize1);
            this.panelPagingStartup.Controls.Add(this.uiLabel6);
            this.panelPagingStartup.Controls.Add(this.lblTotalNum1);
            this.panelPagingStartup.Controls.Add(this.pageNO1);
            this.panelPagingStartup.Controls.Add(this.btnUpPage1);
            this.panelPagingStartup.Controls.Add(this.btnDownPag1);
            this.panelPagingStartup.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelPagingStartup.Location = new System.Drawing.Point(3, 727);
            this.panelPagingStartup.Name = "panelPagingStartup";
            this.panelPagingStartup.Size = new System.Drawing.Size(1786, 46);
            this.panelPagingStartup.TabIndex = 861;
            // 
            // pageSize1
            // 
            this.pageSize1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pageSize1.DoubleValue = 200D;
            this.pageSize1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pageSize1.IntValue = 200;
            this.pageSize1.Location = new System.Drawing.Point(1182, 8);
            this.pageSize1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pageSize1.MinimumSize = new System.Drawing.Size(1, 16);
            this.pageSize1.Name = "pageSize1";
            this.pageSize1.Padding = new System.Windows.Forms.Padding(5);
            this.pageSize1.ShowText = false;
            this.pageSize1.Size = new System.Drawing.Size(43, 30);
            this.pageSize1.TabIndex = 861;
            this.pageSize1.Text = "200";
            this.pageSize1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.pageSize1.Watermark = "";
            this.pageSize1.TextChanged += new System.EventHandler(this.pageSize1_Change);
            // 
            // uiLabel6
            // 
            this.uiLabel6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.uiLabel6.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.uiLabel6.Location = new System.Drawing.Point(1232, 4);
            this.uiLabel6.Name = "uiLabel6";
            this.uiLabel6.Size = new System.Drawing.Size(62, 38);
            this.uiLabel6.TabIndex = 862;
            this.uiLabel6.Text = "条/页";
            this.uiLabel6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTotalNum1
            // 
            this.lblTotalNum1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotalNum1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTotalNum1.Location = new System.Drawing.Point(1301, 8);
            this.lblTotalNum1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lblTotalNum1.MinimumSize = new System.Drawing.Size(1, 16);
            this.lblTotalNum1.Name = "lblTotalNum1";
            this.lblTotalNum1.Padding = new System.Windows.Forms.Padding(5);
            this.lblTotalNum1.ReadOnly = true;
            this.lblTotalNum1.ShowText = false;
            this.lblTotalNum1.Size = new System.Drawing.Size(112, 30);
            this.lblTotalNum1.TabIndex = 859;
            this.lblTotalNum1.Text = "共0条";
            this.lblTotalNum1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTotalNum1.Watermark = "";
            // 
            // pageNO1
            // 
            this.pageNO1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pageNO1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pageNO1.Location = new System.Drawing.Point(1429, 8);
            this.pageNO1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pageNO1.MinimumSize = new System.Drawing.Size(1, 16);
            this.pageNO1.Name = "pageNO1";
            this.pageNO1.Padding = new System.Windows.Forms.Padding(5);
            this.pageNO1.ReadOnly = true;
            this.pageNO1.ShowText = false;
            this.pageNO1.Size = new System.Drawing.Size(112, 30);
            this.pageNO1.TabIndex = 860;
            this.pageNO1.Text = "第0页/共0页";
            this.pageNO1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.pageNO1.Watermark = "";
            // 
            // btnUpPage1
            // 
            this.btnUpPage1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpPage1.BackColor = System.Drawing.Color.Silver;
            this.btnUpPage1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnUpPage1.FalseColor = System.Drawing.Color.Silver;
            this.btnUpPage1.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnUpPage1.Location = new System.Drawing.Point(1553, 8);
            this.btnUpPage1.Name = "btnUpPage1";
            this.btnUpPage1.Size = new System.Drawing.Size(106, 30);
            this.btnUpPage1.SwitchType = RW.UI.Controls.SwitchStyleEnums.Switch;
            this.btnUpPage1.TabIndex = 858;
            this.btnUpPage1.Tag = "";
            this.btnUpPage1.Text = "上一页";
            this.btnUpPage1.TrueColor = System.Drawing.Color.Lime;
            this.btnUpPage1.Click += new System.EventHandler(this.btnUpPage1_Click);
            // 
            // btnDownPag1
            // 
            this.btnDownPag1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDownPag1.BackColor = System.Drawing.Color.Silver;
            this.btnDownPag1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnDownPag1.FalseColor = System.Drawing.Color.Silver;
            this.btnDownPag1.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnDownPag1.Location = new System.Drawing.Point(1671, 8);
            this.btnDownPag1.Name = "btnDownPag1";
            this.btnDownPag1.Size = new System.Drawing.Size(106, 30);
            this.btnDownPag1.SwitchType = RW.UI.Controls.SwitchStyleEnums.Switch;
            this.btnDownPag1.TabIndex = 857;
            this.btnDownPag1.Tag = "";
            this.btnDownPag1.Text = "下一页";
            this.btnDownPag1.TrueColor = System.Drawing.Color.Lime;
            this.btnDownPag1.Click += new System.EventHandler(this.btnDownPag1_Click);
            // 
            // ucAllDataRecord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.Controls.Add(this.reportSaveAll);
            this.Controls.Add(this.reportSave);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.btnSelctModel);
            this.Controls.Add(this.tabDataRecord);
            this.Controls.Add(this.groupBox1);
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
            this.Name = "ucAllDataRecord";
            this.Size = new System.Drawing.Size(1828, 997);
            this.Load += new System.EventHandler(this.ucAutoRecord_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.allDataRecord)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabDataRecord.ResumeLayout(false);
            this.tabPageAllData.ResumeLayout(false);
            this.panelPagingAllData.ResumeLayout(false);
            this.tabPageStartup.ResumeLayout(false);
            this.panelStartup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvStartupRecord)).EndInit();
            this.panelPagingStartup.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UITextBox txtNumber;
        private Sunny.UI.UILabel uiLabel3;
        private Sunny.UI.UILabel uiLabel2;
        private System.Windows.Forms.Panel panel1;
        private Sunny.UI.UILabel uiLabel4;
        private Sunny.UI.UITextBox cboModel;
        private Sunny.UI.UIDatetimePicker dtpStartTime;
        private Sunny.UI.UILabel uiLabel1;
        private Sunny.UI.UIDatetimePicker dtpEndTime;
        private RW.UI.Controls.RButton btnSelctModel;
        private RW.UI.Controls.RButton btnSearch;
        private RW.UI.Controls.RButton btnOut;
        private DataGridView allDataRecord;
        private RW.UI.Controls.RButton btnUpPage;
        private RW.UI.Controls.RButton btnNextPage;
        private GroupBox groupBox1;
        private CheckBox FuelDataGrp;
        private CheckBox BaseGrp;
        private CheckBox PLC2AIDataGrp;
        private CheckBox AODataGrp;
        private CheckBox WaterDataGrp;
        private CheckBox AIDataGrp;
        private CheckBox ThreePhaseElectricData;
        private CheckBox TRDPDataGrp;
        private CheckBox GD350_1Data;
        private CheckBox AirDuctData1Grp;
        private CheckBox AirDuctData2Grp;
        private CheckBox PipelineFaultDataGrp;
        private CheckBox ExChangeDataGrpBool;
        private CheckBox ExChangeDataGrpDouble;
        private CheckBox SpeedDataGrp;
        private CheckBox DODataGrp;
        private CheckBox StartPLCDataGrp;
        private CheckBox DIDataGrp;
        private CheckBox EngineOilDataGrp;
        private CheckBox ChoiceAll;
        private CheckBox BaseDataGrp;
        private Sunny.UI.UITextBox lblTotalNum;
        private Sunny.UI.UITextBox pageNO;
        private Sunny.UI.UITextBox pageSize;
        private Sunny.UI.UILabel uiLabel5;
        private RW.UI.Controls.RButton reportSave;
        private RW.UI.Controls.RButton reportSaveAll;
        private System.Windows.Forms.TabControl tabDataRecord;
        private System.Windows.Forms.TabPage tabPageAllData;
        private System.Windows.Forms.Panel panelPagingAllData;
        private System.Windows.Forms.TabPage tabPageStartup;
        private System.Windows.Forms.Panel panelPagingStartup;
        private System.Windows.Forms.Panel panelStartup;
        private DataGridView dgvStartupRecord;
        private DataGridViewTextBoxColumn colStartupIndex;
        private DataGridViewTextBoxColumn colStartupRecordTime;
        private DataGridViewTextBoxColumn colStartupType;
        private DataGridViewTextBoxColumn colStartupRPM;
        private DataGridViewTextBoxColumn colStartupTorque;
        private DataGridViewTextBoxColumn colStartupPower;
        private DataGridViewTextBoxColumn colStartupExcitationVoltage;
        private DataGridViewTextBoxColumn colStartupExcitationCurrent;
        private DataGridViewTextBoxColumn colStartupInvertVoltage;
        private DataGridViewTextBoxColumn colStartupInvertCurrent;
        private DataGridViewTextBoxColumn colStartupInvertRPM;
        private DataGridViewTextBoxColumn colStartupInvertPower;
        private DataGridViewTextBoxColumn colStartupInvertFaultCode;
        private Sunny.UI.UITextBox pageSize1;
        private Sunny.UI.UILabel uiLabel6;
        private Sunny.UI.UITextBox lblTotalNum1;
        private Sunny.UI.UITextBox pageNO1;
        private RW.UI.Controls.RButton btnUpPage1;
        private RW.UI.Controls.RButton btnDownPag1;
    }
}
