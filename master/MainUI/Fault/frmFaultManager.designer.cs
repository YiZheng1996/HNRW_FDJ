namespace MainUI.Fault
{
    partial class frmFaultManager
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnExit = new Sunny.UI.UIButton();
            this.grpDI = new Sunny.UI.UIGroupBox();
            this.txtFaultDesc = new Sunny.UI.UITextBox();
            this.uiLabel2 = new Sunny.UI.UILabel();
            this.uiLabel5 = new Sunny.UI.UILabel();
            this.dtpStartEnd = new Sunny.UI.UIDatePicker();
            this.dtpStartBig = new Sunny.UI.UIDatePicker();
            this.uiLabel4 = new Sunny.UI.UILabel();
            this.btnSearch = new Sunny.UI.UIButton();
            this.uiLabel1 = new Sunny.UI.UILabel();
            this.uiDataGridView1 = new Sunny.UI.UIDataGridView();
            this.colid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFaultType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSeverity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColFaultCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOccurTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colResetTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblRecordCount = new Sunny.UI.UILabel();
            this.cboType = new Sunny.UI.UIComboBox();
            this.panel2.SuspendLayout();
            this.grpDI.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiDataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnExit);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panel2.Location = new System.Drawing.Point(0, 760);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1339, 53);
            this.panel2.TabIndex = 3;
            // 
            // btnExit
            // 
            this.btnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExit.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.btnExit.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.btnExit.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(115)))), ((int)(((byte)(115)))));
            this.btnExit.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnExit.FillSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnExit.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnExit.Location = new System.Drawing.Point(15, 8);
            this.btnExit.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnExit.Name = "btnExit";
            this.btnExit.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.btnExit.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(115)))), ((int)(((byte)(115)))));
            this.btnExit.RectPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnExit.RectSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnExit.Size = new System.Drawing.Size(1301, 38);
            this.btnExit.Style = Sunny.UI.UIStyle.Red;
            this.btnExit.TabIndex = 610;
            this.btnExit.Text = "退出";
            this.btnExit.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnExit.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // grpDI
            // 
            this.grpDI.Controls.Add(this.cboType);
            this.grpDI.Controls.Add(this.txtFaultDesc);
            this.grpDI.Controls.Add(this.uiLabel2);
            this.grpDI.Controls.Add(this.uiLabel5);
            this.grpDI.Controls.Add(this.dtpStartEnd);
            this.grpDI.Controls.Add(this.dtpStartBig);
            this.grpDI.Controls.Add(this.uiLabel4);
            this.grpDI.Controls.Add(this.btnSearch);
            this.grpDI.Controls.Add(this.uiLabel1);
            this.grpDI.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpDI.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.grpDI.Location = new System.Drawing.Point(0, 0);
            this.grpDI.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpDI.MinimumSize = new System.Drawing.Size(1, 1);
            this.grpDI.Name = "grpDI";
            this.grpDI.Padding = new System.Windows.Forms.Padding(0, 32, 0, 0);
            this.grpDI.Size = new System.Drawing.Size(1339, 78);
            this.grpDI.TabIndex = 390;
            this.grpDI.Text = null;
            this.grpDI.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.grpDI.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // txtFaultDesc
            // 
            this.txtFaultDesc.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtFaultDesc.DecimalPlaces = 0;
            this.txtFaultDesc.Font = new System.Drawing.Font("宋体", 14F);
            this.txtFaultDesc.Location = new System.Drawing.Point(370, 33);
            this.txtFaultDesc.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtFaultDesc.Maximum = 5000D;
            this.txtFaultDesc.MaxLength = 500;
            this.txtFaultDesc.Minimum = 0D;
            this.txtFaultDesc.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtFaultDesc.Name = "txtFaultDesc";
            this.txtFaultDesc.ShowText = false;
            this.txtFaultDesc.Size = new System.Drawing.Size(213, 29);
            this.txtFaultDesc.TabIndex = 681;
            this.txtFaultDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtFaultDesc.Watermark = "";
            this.txtFaultDesc.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiLabel2
            // 
            this.uiLabel2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel2.Location = new System.Drawing.Point(291, 36);
            this.uiLabel2.Name = "uiLabel2";
            this.uiLabel2.Size = new System.Drawing.Size(75, 23);
            this.uiLabel2.TabIndex = 394;
            this.uiLabel2.Text = "故障描述";
            this.uiLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiLabel2.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiLabel5
            // 
            this.uiLabel5.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel5.Location = new System.Drawing.Point(991, 35);
            this.uiLabel5.Name = "uiLabel5";
            this.uiLabel5.Size = new System.Drawing.Size(25, 23);
            this.uiLabel5.TabIndex = 392;
            this.uiLabel5.Text = "至";
            this.uiLabel5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiLabel5.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // dtpStartEnd
            // 
            this.dtpStartEnd.FillColor = System.Drawing.Color.White;
            this.dtpStartEnd.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dtpStartEnd.Location = new System.Drawing.Point(1024, 32);
            this.dtpStartEnd.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dtpStartEnd.MaxLength = 10;
            this.dtpStartEnd.MinimumSize = new System.Drawing.Size(63, 0);
            this.dtpStartEnd.Name = "dtpStartEnd";
            this.dtpStartEnd.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.dtpStartEnd.Size = new System.Drawing.Size(165, 29);
            this.dtpStartEnd.SymbolDropDown = 61555;
            this.dtpStartEnd.SymbolNormal = 61555;
            this.dtpStartEnd.TabIndex = 391;
            this.dtpStartEnd.Text = "2023-02-01";
            this.dtpStartEnd.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.dtpStartEnd.Value = new System.DateTime(2023, 2, 1, 16, 20, 20, 721);
            this.dtpStartEnd.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // dtpStartBig
            // 
            this.dtpStartBig.FillColor = System.Drawing.Color.White;
            this.dtpStartBig.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dtpStartBig.Location = new System.Drawing.Point(817, 32);
            this.dtpStartBig.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dtpStartBig.MaxLength = 10;
            this.dtpStartBig.MinimumSize = new System.Drawing.Size(63, 0);
            this.dtpStartBig.Name = "dtpStartBig";
            this.dtpStartBig.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.dtpStartBig.Size = new System.Drawing.Size(165, 29);
            this.dtpStartBig.SymbolDropDown = 61555;
            this.dtpStartBig.SymbolNormal = 61555;
            this.dtpStartBig.TabIndex = 390;
            this.dtpStartBig.Text = "2023-02-01";
            this.dtpStartBig.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.dtpStartBig.Value = new System.DateTime(2023, 2, 1, 16, 20, 20, 721);
            this.dtpStartBig.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiLabel4
            // 
            this.uiLabel4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel4.Location = new System.Drawing.Point(735, 35);
            this.uiLabel4.Name = "uiLabel4";
            this.uiLabel4.Size = new System.Drawing.Size(75, 23);
            this.uiLabel4.TabIndex = 389;
            this.uiLabel4.Text = "时间范围";
            this.uiLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiLabel4.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // btnSearch
            // 
            this.btnSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearch.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.btnSearch.Location = new System.Drawing.Point(1199, 26);
            this.btnSearch.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(120, 40);
            this.btnSearch.TabIndex = 387;
            this.btnSearch.Text = "搜索";
            this.btnSearch.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSearch.TipsText = "1";
            this.btnSearch.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // uiLabel1
            // 
            this.uiLabel1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel1.Location = new System.Drawing.Point(15, 36);
            this.uiLabel1.Name = "uiLabel1";
            this.uiLabel1.Size = new System.Drawing.Size(75, 23);
            this.uiLabel1.TabIndex = 72;
            this.uiLabel1.Text = "故障类型";
            this.uiLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiLabel1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiDataGridView1
            // 
            this.uiDataGridView1.AllowUserToAddRows = false;
            this.uiDataGridView1.AllowUserToDeleteRows = false;
            this.uiDataGridView1.AllowUserToResizeColumns = false;
            this.uiDataGridView1.AllowUserToResizeRows = false;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.uiDataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle6;
            this.uiDataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.uiDataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle7.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.uiDataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.uiDataGridView1.ColumnHeadersHeight = 32;
            this.uiDataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.uiDataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colid,
            this.colFaultType,
            this.colSeverity,
            this.ColFaultCode,
            this.ColDescription,
            this.colOccurTime,
            this.colStatus,
            this.colResetTime});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.uiDataGridView1.DefaultCellStyle = dataGridViewCellStyle8;
            this.uiDataGridView1.EnableHeadersVisualStyles = false;
            this.uiDataGridView1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiDataGridView1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.uiDataGridView1.Location = new System.Drawing.Point(3, 3);
            this.uiDataGridView1.Name = "uiDataGridView1";
            this.uiDataGridView1.ReadOnly = true;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle9.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.uiDataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiDataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle10;
            this.uiDataGridView1.RowTemplate.Height = 23;
            this.uiDataGridView1.SelectedIndex = -1;
            this.uiDataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.uiDataGridView1.Size = new System.Drawing.Size(1333, 624);
            this.uiDataGridView1.StripeOddColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.uiDataGridView1.TabIndex = 391;
            this.uiDataGridView1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // colid
            // 
            this.colid.DataPropertyName = "id";
            this.colid.HeaderText = "ID";
            this.colid.Name = "colid";
            this.colid.ReadOnly = true;
            this.colid.Visible = false;
            // 
            // colFaultType
            // 
            this.colFaultType.DataPropertyName = "fault_type";
            this.colFaultType.HeaderText = "报警归属";
            this.colFaultType.Name = "colFaultType";
            this.colFaultType.ReadOnly = true;
            this.colFaultType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colFaultType.Width = 120;
            // 
            // colSeverity
            // 
            this.colSeverity.DataPropertyName = "severity";
            this.colSeverity.HeaderText = "报警类型";
            this.colSeverity.Name = "colSeverity";
            this.colSeverity.ReadOnly = true;
            this.colSeverity.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colSeverity.Width = 160;
            // 
            // ColFaultCode
            // 
            this.ColFaultCode.DataPropertyName = "fault_code";
            this.ColFaultCode.HeaderText = "报警名称";
            this.ColFaultCode.Name = "ColFaultCode";
            this.ColFaultCode.ReadOnly = true;
            this.ColFaultCode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColFaultCode.Visible = false;
            // 
            // ColDescription
            // 
            this.ColDescription.DataPropertyName = "description";
            this.ColDescription.HeaderText = "报警描述";
            this.ColDescription.Name = "ColDescription";
            this.ColDescription.ReadOnly = true;
            this.ColDescription.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColDescription.Width = 500;
            // 
            // colOccurTime
            // 
            this.colOccurTime.DataPropertyName = "occur_time";
            this.colOccurTime.HeaderText = "报警时间";
            this.colOccurTime.Name = "colOccurTime";
            this.colOccurTime.ReadOnly = true;
            this.colOccurTime.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colOccurTime.Width = 200;
            // 
            // colStatus
            // 
            this.colStatus.DataPropertyName = "status";
            this.colStatus.HeaderText = "是否复位";
            this.colStatus.Name = "colStatus";
            this.colStatus.ReadOnly = true;
            this.colStatus.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colResetTime
            // 
            this.colResetTime.DataPropertyName = "reset_time";
            this.colResetTime.HeaderText = "复位时间";
            this.colResetTime.Name = "colResetTime";
            this.colResetTime.ReadOnly = true;
            this.colResetTime.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colResetTime.Width = 200;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblRecordCount);
            this.panel1.Controls.Add(this.uiDataGridView1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 86);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1339, 674);
            this.panel1.TabIndex = 392;
            // 
            // lblRecordCount
            // 
            this.lblRecordCount.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblRecordCount.Location = new System.Drawing.Point(1057, 636);
            this.lblRecordCount.Name = "lblRecordCount";
            this.lblRecordCount.Size = new System.Drawing.Size(253, 23);
            this.lblRecordCount.TabIndex = 395;
            this.lblRecordCount.Text = "故障记录： 共 0 条记录";
            this.lblRecordCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblRecordCount.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // cboType
            // 
            this.cboType.DataSource = null;
            this.cboType.DropDownStyle = Sunny.UI.UIDropDownStyle.DropDownList;
            this.cboType.DropDownWidth = 300;
            this.cboType.FillColor = System.Drawing.Color.White;
            this.cboType.FilterMaxCount = 50;
            this.cboType.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.cboType.Items.AddRange(new object[] {
            "全部",
            "通讯",
            "OPC检测",
            "逻辑判断",
            "发动机控制器"});
            this.cboType.Location = new System.Drawing.Point(87, 33);
            this.cboType.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cboType.MinimumSize = new System.Drawing.Size(63, 0);
            this.cboType.Name = "cboType";
            this.cboType.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.cboType.Size = new System.Drawing.Size(170, 29);
            this.cboType.TabIndex = 682;
            this.cboType.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.cboType.Watermark = "";
            this.cboType.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // frmFaultManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(1339, 813);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.grpDI);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmFaultManager";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "报警日志管理";
            this.Load += new System.EventHandler(this.frmDataManager_Load);
            this.panel2.ResumeLayout(false);
            this.grpDI.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uiDataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel2;
        private Sunny.UI.UIGroupBox grpDI;
        private Sunny.UI.UILabel uiLabel1;
        private Sunny.UI.UIButton btnSearch;
        private Sunny.UI.UILabel uiLabel4;
        private Sunny.UI.UIDatePicker dtpStartBig;
        private Sunny.UI.UIDatePicker dtpStartEnd;
        private Sunny.UI.UIDataGridView uiDataGridView1;
        private Sunny.UI.UILabel uiLabel5;
        private Sunny.UI.UILabel uiLabel2;
        private Sunny.UI.UIButton btnExit;
        private Sunny.UI.UITextBox txtFaultDesc;
        private System.Windows.Forms.Panel panel1;
        private Sunny.UI.UILabel lblRecordCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colid;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFaultType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSeverity;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColFaultCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOccurTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn colResetTime;
        private Sunny.UI.UIComboBox cboType;
    }
}