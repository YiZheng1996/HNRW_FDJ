namespace MainUI
{
    partial class frmProcessDataManager
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnExit = new Sunny.UI.UIButton();
            this.btnRemove = new Sunny.UI.UIButton();
            this.btnView = new Sunny.UI.UIButton();
            this.cboType = new Sunny.UI.UIComboBox();
            this.grpDI = new Sunny.UI.UIGroupBox();
            this.lblCnt = new Sunny.UI.UILabel();
            this.uiLabel5 = new Sunny.UI.UILabel();
            this.dtpStartEnd = new Sunny.UI.UIDatePicker();
            this.dtpStartBig = new Sunny.UI.UIDatePicker();
            this.uiLabel4 = new Sunny.UI.UILabel();
            this.txtNumber = new Sunny.UI.UITextBox();
            this.btnSearch = new Sunny.UI.UIButton();
            this.uiLabel3 = new Sunny.UI.UILabel();
            this.uiLabel2 = new Sunny.UI.UILabel();
            this.cboModel = new Sunny.UI.UIComboBox();
            this.uiLabel1 = new Sunny.UI.UILabel();
            this.uiDataGridView1 = new Sunny.UI.UIDataGridView();
            this.colid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colmodeltype = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colModel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTestID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTester = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTestTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colReportPath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnReportOut = new Sunny.UI.UIButton();
            this.panel2.SuspendLayout();
            this.grpDI.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiDataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnReportOut);
            this.panel2.Controls.Add(this.btnExit);
            this.panel2.Controls.Add(this.btnRemove);
            this.panel2.Controls.Add(this.btnView);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panel2.Location = new System.Drawing.Point(0, 655);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1174, 62);
            this.panel2.TabIndex = 3;
            // 
            // btnExit
            // 
            this.btnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExit.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.btnExit.Location = new System.Drawing.Point(868, 12);
            this.btnExit.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(120, 40);
            this.btnExit.TabIndex = 390;
            this.btnExit.Text = "退出";
            this.btnExit.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnExit.TipsText = "1";
            this.btnExit.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRemove.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.btnRemove.Location = new System.Drawing.Point(430, 12);
            this.btnRemove.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(120, 40);
            this.btnRemove.TabIndex = 389;
            this.btnRemove.Text = "删除";
            this.btnRemove.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnRemove.TipsText = "1";
            this.btnRemove.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnView
            // 
            this.btnView.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnView.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.btnView.Location = new System.Drawing.Point(29, 12);
            this.btnView.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(120, 40);
            this.btnView.TabIndex = 388;
            this.btnView.Text = "查看详情";
            this.btnView.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnView.TipsText = "1";
            this.btnView.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // cboType
            // 
            this.cboType.DataSource = null;
            this.cboType.FillColor = System.Drawing.Color.White;
            this.cboType.FilterMaxCount = 50;
            this.cboType.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.cboType.Location = new System.Drawing.Point(107, 28);
            this.cboType.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cboType.MinimumSize = new System.Drawing.Size(63, 0);
            this.cboType.Name = "cboType";
            this.cboType.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.cboType.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.cboType.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom;
            this.cboType.ShowFilter = true;
            this.cboType.Size = new System.Drawing.Size(165, 29);
            this.cboType.TabIndex = 71;
            this.cboType.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.cboType.Watermark = "请选择";
            this.cboType.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.cboType.SelectedValueChanged += new System.EventHandler(this.cboType_SelectedValueChanged);
            // 
            // grpDI
            // 
            this.grpDI.Controls.Add(this.lblCnt);
            this.grpDI.Controls.Add(this.uiLabel5);
            this.grpDI.Controls.Add(this.dtpStartEnd);
            this.grpDI.Controls.Add(this.dtpStartBig);
            this.grpDI.Controls.Add(this.uiLabel4);
            this.grpDI.Controls.Add(this.txtNumber);
            this.grpDI.Controls.Add(this.btnSearch);
            this.grpDI.Controls.Add(this.uiLabel3);
            this.grpDI.Controls.Add(this.uiLabel2);
            this.grpDI.Controls.Add(this.cboModel);
            this.grpDI.Controls.Add(this.uiLabel1);
            this.grpDI.Controls.Add(this.cboType);
            this.grpDI.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpDI.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.grpDI.Location = new System.Drawing.Point(0, 0);
            this.grpDI.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpDI.MinimumSize = new System.Drawing.Size(1, 1);
            this.grpDI.Name = "grpDI";
            this.grpDI.Padding = new System.Windows.Forms.Padding(0, 32, 0, 0);
            this.grpDI.Size = new System.Drawing.Size(1174, 108);
            this.grpDI.TabIndex = 390;
            this.grpDI.Text = "数据检索";
            this.grpDI.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.grpDI.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // lblCnt
            // 
            this.lblCnt.AutoSize = true;
            this.lblCnt.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCnt.Location = new System.Drawing.Point(799, 82);
            this.lblCnt.Name = "lblCnt";
            this.lblCnt.Size = new System.Drawing.Size(74, 21);
            this.lblCnt.TabIndex = 393;
            this.lblCnt.Text = "记录数量";
            this.lblCnt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblCnt.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiLabel5
            // 
            this.uiLabel5.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel5.Location = new System.Drawing.Point(304, 70);
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
            this.dtpStartEnd.Location = new System.Drawing.Point(361, 67);
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
            this.dtpStartBig.Location = new System.Drawing.Point(107, 67);
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
            this.uiLabel4.Location = new System.Drawing.Point(25, 70);
            this.uiLabel4.Name = "uiLabel4";
            this.uiLabel4.Size = new System.Drawing.Size(75, 23);
            this.uiLabel4.TabIndex = 389;
            this.uiLabel4.Text = "测试时间";
            this.uiLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiLabel4.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // txtNumber
            // 
            this.txtNumber.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtNumber.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.txtNumber.Location = new System.Drawing.Point(611, 28);
            this.txtNumber.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtNumber.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtNumber.Name = "txtNumber";
            this.txtNumber.ShowText = false;
            this.txtNumber.Size = new System.Drawing.Size(169, 29);
            this.txtNumber.TabIndex = 388;
            this.txtNumber.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtNumber.Watermark = "请输入";
            this.txtNumber.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // btnSearch
            // 
            this.btnSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearch.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.btnSearch.Location = new System.Drawing.Point(1029, 35);
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
            // uiLabel3
            // 
            this.uiLabel3.BackColor = System.Drawing.Color.Transparent;
            this.uiLabel3.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.uiLabel3.Location = new System.Drawing.Point(537, 31);
            this.uiLabel3.Name = "uiLabel3";
            this.uiLabel3.Size = new System.Drawing.Size(75, 23);
            this.uiLabel3.TabIndex = 75;
            this.uiLabel3.Text = "生产号";
            this.uiLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.uiLabel3.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiLabel2
            // 
            this.uiLabel2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel2.Location = new System.Drawing.Point(279, 31);
            this.uiLabel2.Name = "uiLabel2";
            this.uiLabel2.Size = new System.Drawing.Size(75, 23);
            this.uiLabel2.TabIndex = 74;
            this.uiLabel2.Text = "产品型号";
            this.uiLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiLabel2.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // cboModel
            // 
            this.cboModel.DataSource = null;
            this.cboModel.FillColor = System.Drawing.Color.White;
            this.cboModel.FilterMaxCount = 50;
            this.cboModel.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.cboModel.Location = new System.Drawing.Point(361, 28);
            this.cboModel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cboModel.MinimumSize = new System.Drawing.Size(63, 0);
            this.cboModel.Name = "cboModel";
            this.cboModel.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.cboModel.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.cboModel.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom;
            this.cboModel.ShowFilter = true;
            this.cboModel.Size = new System.Drawing.Size(165, 29);
            this.cboModel.TabIndex = 73;
            this.cboModel.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.cboModel.Watermark = "请选择";
            this.cboModel.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiLabel1
            // 
            this.uiLabel1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel1.Location = new System.Drawing.Point(25, 31);
            this.uiLabel1.Name = "uiLabel1";
            this.uiLabel1.Size = new System.Drawing.Size(75, 23);
            this.uiLabel1.TabIndex = 72;
            this.uiLabel1.Text = "产品类型";
            this.uiLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiLabel1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiDataGridView1
            // 
            this.uiDataGridView1.AllowUserToAddRows = false;
            this.uiDataGridView1.AllowUserToDeleteRows = false;
            this.uiDataGridView1.AllowUserToResizeColumns = false;
            this.uiDataGridView1.AllowUserToResizeRows = false;
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.uiDataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle11;
            this.uiDataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.uiDataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.uiDataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle12.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.uiDataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle12;
            this.uiDataGridView1.ColumnHeadersHeight = 32;
            this.uiDataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.uiDataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colid,
            this.colmodeltype,
            this.colModel,
            this.colTestID,
            this.colTester,
            this.colTestTime,
            this.colReportPath});
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle13.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.uiDataGridView1.DefaultCellStyle = dataGridViewCellStyle13;
            this.uiDataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiDataGridView1.EnableHeadersVisualStyles = false;
            this.uiDataGridView1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiDataGridView1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.uiDataGridView1.Location = new System.Drawing.Point(0, 108);
            this.uiDataGridView1.Name = "uiDataGridView1";
            this.uiDataGridView1.ReadOnly = true;
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle14.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle14.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.uiDataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle14;
            dataGridViewCellStyle15.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiDataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle15;
            this.uiDataGridView1.RowTemplate.Height = 23;
            this.uiDataGridView1.SelectedIndex = -1;
            this.uiDataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.uiDataGridView1.Size = new System.Drawing.Size(1174, 547);
            this.uiDataGridView1.StripeOddColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.uiDataGridView1.TabIndex = 391;
            this.uiDataGridView1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.uiDataGridView1.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseDoubleClick);
            // 
            // colid
            // 
            this.colid.DataPropertyName = "id";
            this.colid.HeaderText = "ID";
            this.colid.Name = "colid";
            this.colid.ReadOnly = true;
            this.colid.Visible = false;
            // 
            // colmodeltype
            // 
            this.colmodeltype.DataPropertyName = "modeltype";
            this.colmodeltype.HeaderText = "产品类型";
            this.colmodeltype.Name = "colmodeltype";
            this.colmodeltype.ReadOnly = true;
            // 
            // colModel
            // 
            this.colModel.DataPropertyName = "Model";
            this.colModel.HeaderText = "产品型号";
            this.colModel.Name = "colModel";
            this.colModel.ReadOnly = true;
            // 
            // colTestID
            // 
            this.colTestID.DataPropertyName = "TestID";
            this.colTestID.HeaderText = "生产号";
            this.colTestID.Name = "colTestID";
            this.colTestID.ReadOnly = true;
            // 
            // colTester
            // 
            this.colTester.DataPropertyName = "Tester";
            this.colTester.HeaderText = "测试人员";
            this.colTester.Name = "colTester";
            this.colTester.ReadOnly = true;
            // 
            // colTestTime
            // 
            this.colTestTime.DataPropertyName = "TestTime";
            this.colTestTime.HeaderText = "测试时间";
            this.colTestTime.Name = "colTestTime";
            this.colTestTime.ReadOnly = true;
            // 
            // colReportPath
            // 
            this.colReportPath.DataPropertyName = "ReportPath";
            this.colReportPath.HeaderText = "报表路径";
            this.colReportPath.Name = "colReportPath";
            this.colReportPath.ReadOnly = true;
            this.colReportPath.Visible = false;
            // 
            // btnReportOut
            // 
            this.btnReportOut.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReportOut.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.btnReportOut.Location = new System.Drawing.Point(177, 12);
            this.btnReportOut.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnReportOut.Name = "btnReportOut";
            this.btnReportOut.Size = new System.Drawing.Size(120, 40);
            this.btnReportOut.TabIndex = 391;
            this.btnReportOut.Text = "导出";
            this.btnReportOut.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnReportOut.TipsText = "1";
            this.btnReportOut.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // frmProcessDataManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(1174, 717);
            this.Controls.Add(this.uiDataGridView1);
            this.Controls.Add(this.grpDI);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmProcessDataManager";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "试验数据管理";
            this.Load += new System.EventHandler(this.frmDataManager_Load);
            this.panel2.ResumeLayout(false);
            this.grpDI.ResumeLayout(false);
            this.grpDI.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiDataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel2;
        private Sunny.UI.UIComboBox cboType;
        private Sunny.UI.UIGroupBox grpDI;
        private Sunny.UI.UILabel uiLabel1;
        private Sunny.UI.UIComboBox cboModel;
        private Sunny.UI.UILabel uiLabel2;
        private Sunny.UI.UILabel uiLabel3;
        private Sunny.UI.UIButton btnSearch;
        private Sunny.UI.UITextBox txtNumber;
        private Sunny.UI.UILabel uiLabel4;
        private Sunny.UI.UIDatePicker dtpStartBig;
        private Sunny.UI.UIDatePicker dtpStartEnd;
        private Sunny.UI.UIButton btnExit;
        private Sunny.UI.UIButton btnRemove;
        private Sunny.UI.UIButton btnView;
        private Sunny.UI.UIDataGridView uiDataGridView1;
        private Sunny.UI.UILabel uiLabel5;
        private Sunny.UI.UILabel lblCnt;
        private System.Windows.Forms.DataGridViewTextBoxColumn colid;
        private System.Windows.Forms.DataGridViewTextBoxColumn colmodeltype;
        private System.Windows.Forms.DataGridViewTextBoxColumn colModel;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTestID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTester;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTestTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colReportPath;
        private Sunny.UI.UIButton btnReportOut;
    }
}