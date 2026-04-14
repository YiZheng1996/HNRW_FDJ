namespace MainUI.Procedure
{
    partial class uc100hParams
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lbl100hStepName = new System.Windows.Forms.Label();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.StepNum = new System.Windows.Forms.NumericUpDown();
            this.lbl = new System.Windows.Forms.Label();
            this.btnAdd100hStep = new Sunny.UI.UIButton();
            this.dgv100hMH = new System.Windows.Forms.DataGridView();
            this.StepIndex100 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StepName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GK = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Torque = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RPM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RunTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvMH = new System.Windows.Forms.DataGridView();
            this.Index = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TestName = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.CollectIntervalTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ForeachNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Col100Day = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.uiGroupBox1 = new Sunny.UI.UIGroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.nud100hNum = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.btnAdd100h = new Sunny.UI.UIButton();
            this.ucGKParams1 = new MainUI.Procedure.ucGKParams();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl100hSore = new System.Windows.Forms.Label();
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.StepNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv100hMH)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMH)).BeginInit();
            this.uiGroupBox1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud100hNum)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl100hStepName
            // 
            this.lbl100hStepName.AutoSize = true;
            this.lbl100hStepName.Font = new System.Drawing.Font("微软雅黑", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl100hStepName.Location = new System.Drawing.Point(205, 407);
            this.lbl100hStepName.Name = "lbl100hStepName";
            this.lbl100hStepName.Size = new System.Drawing.Size(23, 30);
            this.lbl100hStepName.TabIndex = 442;
            this.lbl100hStepName.Text = "-";
            // 
            // groupBox7
            // 
            this.groupBox7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.groupBox7.Controls.Add(this.StepNum);
            this.groupBox7.Controls.Add(this.lbl);
            this.groupBox7.Controls.Add(this.btnAdd100hStep);
            this.groupBox7.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.groupBox7.Location = new System.Drawing.Point(3, 778);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(842, 89);
            this.groupBox7.TabIndex = 441;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "新增步骤";
            // 
            // StepNum
            // 
            this.StepNum.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.StepNum.Location = new System.Drawing.Point(150, 35);
            this.StepNum.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.StepNum.Name = "StepNum";
            this.StepNum.Size = new System.Drawing.Size(120, 34);
            this.StepNum.TabIndex = 423;
            this.StepNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.StepNum.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lbl
            // 
            this.lbl.AutoSize = true;
            this.lbl.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.lbl.Location = new System.Drawing.Point(15, 40);
            this.lbl.Name = "lbl";
            this.lbl.Size = new System.Drawing.Size(107, 25);
            this.lbl.TabIndex = 417;
            this.lbl.Text = "新增步骤数";
            // 
            // btnAdd100hStep
            // 
            this.btnAdd100hStep.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd100hStep.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.btnAdd100hStep.Location = new System.Drawing.Point(302, 32);
            this.btnAdd100hStep.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnAdd100hStep.Name = "btnAdd100hStep";
            this.btnAdd100hStep.Size = new System.Drawing.Size(120, 40);
            this.btnAdd100hStep.Style = Sunny.UI.UIStyle.Custom;
            this.btnAdd100hStep.TabIndex = 438;
            this.btnAdd100hStep.Text = "新增";
            this.btnAdd100hStep.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAdd100hStep.TipsText = "1";
            this.btnAdd100hStep.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnAdd100hStep.Click += new System.EventHandler(this.btnAdd100hStep_Click);
            // 
            // dgv100hMH
            // 
            this.dgv100hMH.AllowUserToAddRows = false;
            this.dgv100hMH.AllowUserToDeleteRows = false;
            this.dgv100hMH.AllowUserToResizeColumns = false;
            this.dgv100hMH.AllowUserToResizeRows = false;
            this.dgv100hMH.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv100hMH.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgv100hMH.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.Padding = new System.Windows.Forms.Padding(4);
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv100hMH.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.dgv100hMH.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv100hMH.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.StepIndex100,
            this.StepName,
            this.Column3,
            this.Column1,
            this.GK,
            this.Torque,
            this.RPM,
            this.RunTime,
            this.Column2});
            this.dgv100hMH.ContextMenuStrip = this.contextMenuStrip;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv100hMH.DefaultCellStyle = dataGridViewCellStyle12;
            this.dgv100hMH.EnableHeadersVisualStyles = false;
            this.dgv100hMH.Location = new System.Drawing.Point(3, 445);
            this.dgv100hMH.Name = "dgv100hMH";
            this.dgv100hMH.RowHeadersVisible = false;
            this.dgv100hMH.RowTemplate.Height = 23;
            this.dgv100hMH.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv100hMH.Size = new System.Drawing.Size(842, 327);
            this.dgv100hMH.TabIndex = 436;
            this.dgv100hMH.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgv100hStep_CellBeginEdit);
            this.dgv100hMH.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv100hMH_CellContentClick);
            this.dgv100hMH.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv100hMH_CellEndEdit);
            this.dgv100hMH.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgv100hMH_CellValidating);
            this.dgv100hMH.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgv100hMH_EditingControlShowing);
            // 
            // StepIndex100
            // 
            this.StepIndex100.DataPropertyName = "Index";
            this.StepIndex100.FillWeight = 40F;
            this.StepIndex100.HeaderText = "步骤号";
            this.StepIndex100.Name = "StepIndex100";
            this.StepIndex100.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.StepIndex100.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // StepName
            // 
            this.StepName.DataPropertyName = "StepName";
            this.StepName.HeaderText = "阶段名称";
            this.StepName.Name = "StepName";
            this.StepName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.StepName.Visible = false;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "周期";
            this.Column3.Name = "Column3";
            this.Column3.Visible = false;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "节点";
            this.Column1.Name = "Column1";
            this.Column1.Visible = false;
            // 
            // GK
            // 
            this.GK.FillWeight = 50F;
            this.GK.HeaderText = "工况编号";
            this.GK.Name = "GK";
            this.GK.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Torque
            // 
            this.Torque.DataPropertyName = "Torque";
            dataGridViewCellStyle9.Format = "F0";
            this.Torque.DefaultCellStyle = dataGridViewCellStyle9;
            this.Torque.FillWeight = 60F;
            this.Torque.HeaderText = "扭矩（%）";
            this.Torque.Name = "Torque";
            this.Torque.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // RPM
            // 
            this.RPM.DataPropertyName = "RPM";
            dataGridViewCellStyle10.Format = "F0";
            this.RPM.DefaultCellStyle = dataGridViewCellStyle10;
            this.RPM.FillWeight = 60F;
            this.RPM.HeaderText = "转速（%）";
            this.RPM.Name = "RPM";
            this.RPM.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // RunTime
            // 
            this.RunTime.DataPropertyName = "RunTime";
            dataGridViewCellStyle11.Format = "F0";
            this.RunTime.DefaultCellStyle = dataGridViewCellStyle11;
            this.RunTime.FillWeight = 70F;
            this.RunTime.HeaderText = "运行时间(min)";
            this.RunTime.Name = "RunTime";
            this.RunTime.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column2
            // 
            this.Column2.FillWeight = 30F;
            this.Column2.HeaderText = "操作";
            this.Column2.Name = "Column2";
            this.Column2.Text = "删除";
            this.Column2.UseColumnTextForButtonValue = true;
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmCopy,
            this.tsmPaste,
            this.tsmDelete});
            this.contextMenuStrip.Name = "contextMenuStrip360h";
            this.contextMenuStrip.Size = new System.Drawing.Size(147, 70);
            // 
            // tsmCopy
            // 
            this.tsmCopy.Name = "tsmCopy";
            this.tsmCopy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.tsmCopy.Size = new System.Drawing.Size(146, 22);
            this.tsmCopy.Text = "复制";
            this.tsmCopy.Click += new System.EventHandler(this.tsmCopy_Click);
            // 
            // tsmPaste
            // 
            this.tsmPaste.Name = "tsmPaste";
            this.tsmPaste.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.tsmPaste.Size = new System.Drawing.Size(146, 22);
            this.tsmPaste.Text = "粘贴";
            this.tsmPaste.Click += new System.EventHandler(this.tsmPaste_Click);
            // 
            // tsmDelete
            // 
            this.tsmDelete.Name = "tsmDelete";
            this.tsmDelete.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.tsmDelete.Size = new System.Drawing.Size(146, 22);
            this.tsmDelete.Text = "删除";
            this.tsmDelete.Click += new System.EventHandler(this.tsmDelete_Click);
            // 
            // dgvMH
            // 
            this.dgvMH.AllowUserToAddRows = false;
            this.dgvMH.AllowUserToDeleteRows = false;
            this.dgvMH.AllowUserToResizeColumns = false;
            this.dgvMH.AllowUserToResizeRows = false;
            this.dgvMH.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMH.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvMH.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(4);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMH.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvMH.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMH.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Index,
            this.TestName,
            this.CollectIntervalTime,
            this.ForeachNum,
            this.Col100Day,
            this.Column4});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvMH.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgvMH.EnableHeadersVisualStyles = false;
            this.dgvMH.Location = new System.Drawing.Point(3, 35);
            this.dgvMH.MultiSelect = false;
            this.dgvMH.Name = "dgvMH";
            this.dgvMH.RowHeadersVisible = false;
            this.dgvMH.RowTemplate.Height = 23;
            this.dgvMH.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMH.Size = new System.Drawing.Size(842, 260);
            this.dgvMH.TabIndex = 401;
            this.dgvMH.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgvMH_CellBeginEdit);
            this.dgvMH.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMH_CellContentClick);
            this.dgvMH.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMH_CellEndEdit);
            this.dgvMH.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMH_CellEnter);
            this.dgvMH.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvMH_DataError);
            this.dgvMH.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvMH_EditingControlShowing);
            this.dgvMH.SelectionChanged += new System.EventHandler(this.dgvMH_SelectionChanged);
            // 
            // Index
            // 
            this.Index.DataPropertyName = "Index";
            this.Index.FillWeight = 50F;
            this.Index.HeaderText = "序号";
            this.Index.Name = "Index";
            this.Index.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // TestName
            // 
            this.TestName.DataPropertyName = "TestName";
            this.TestName.FillWeight = 150F;
            this.TestName.HeaderText = "阶段名称";
            this.TestName.Items.AddRange(new object[] {
            "标定功率试验, ",
            "超负荷试验, ",
            "部分负荷试验, ",
            "交替突变负荷试验"});
            this.TestName.Name = "TestName";
            this.TestName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // CollectIntervalTime
            // 
            this.CollectIntervalTime.DataPropertyName = "CollectIntervalTime";
            this.CollectIntervalTime.HeaderText = "间隔时间";
            this.CollectIntervalTime.Name = "CollectIntervalTime";
            this.CollectIntervalTime.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CollectIntervalTime.Visible = false;
            // 
            // ForeachNum
            // 
            this.ForeachNum.DataPropertyName = "ForeachNum";
            this.ForeachNum.HeaderText = "循环次数";
            this.ForeachNum.Name = "ForeachNum";
            this.ForeachNum.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ForeachNum.Visible = false;
            // 
            // Col100Day
            // 
            this.Col100Day.HeaderText = "天数";
            this.Col100Day.Name = "Col100Day";
            this.Col100Day.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column4
            // 
            this.Column4.FillWeight = 50F;
            this.Column4.HeaderText = "操作";
            this.Column4.Name = "Column4";
            this.Column4.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column4.Text = "删除";
            this.Column4.UseColumnTextForButtonValue = true;
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Controls.Add(this.groupBox1);
            this.uiGroupBox1.Controls.Add(this.ucGKParams1);
            this.uiGroupBox1.Controls.Add(this.lbl100hStepName);
            this.uiGroupBox1.Controls.Add(this.label2);
            this.uiGroupBox1.Controls.Add(this.label1);
            this.uiGroupBox1.Controls.Add(this.lbl100hSore);
            this.uiGroupBox1.Controls.Add(this.groupBox7);
            this.uiGroupBox1.Controls.Add(this.dgvMH);
            this.uiGroupBox1.Controls.Add(this.dgv100hMH);
            this.uiGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiGroupBox1.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.uiGroupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiGroupBox1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Padding = new System.Windows.Forms.Padding(0, 32, 0, 0);
            this.uiGroupBox1.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.uiGroupBox1.Size = new System.Drawing.Size(1616, 870);
            this.uiGroupBox1.Style = Sunny.UI.UIStyle.Custom;
            this.uiGroupBox1.TabIndex = 400;
            this.uiGroupBox1.Text = null;
            this.uiGroupBox1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.uiGroupBox1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.groupBox1.Controls.Add(this.nud100hNum);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.btnAdd100h);
            this.groupBox1.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.groupBox1.Location = new System.Drawing.Point(3, 301);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(842, 89);
            this.groupBox1.TabIndex = 658;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "新增阶段";
            // 
            // nud100hNum
            // 
            this.nud100hNum.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nud100hNum.Location = new System.Drawing.Point(150, 35);
            this.nud100hNum.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nud100hNum.Name = "nud100hNum";
            this.nud100hNum.Size = new System.Drawing.Size(120, 34);
            this.nud100hNum.TabIndex = 423;
            this.nud100hNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nud100hNum.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.label3.Location = new System.Drawing.Point(15, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 25);
            this.label3.TabIndex = 417;
            this.label3.Text = "新增阶段数";
            // 
            // btnAdd100h
            // 
            this.btnAdd100h.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd100h.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.btnAdd100h.Location = new System.Drawing.Point(302, 32);
            this.btnAdd100h.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnAdd100h.Name = "btnAdd100h";
            this.btnAdd100h.Size = new System.Drawing.Size(120, 40);
            this.btnAdd100h.Style = Sunny.UI.UIStyle.Custom;
            this.btnAdd100h.TabIndex = 438;
            this.btnAdd100h.Text = "新增";
            this.btnAdd100h.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAdd100h.TipsText = "1";
            this.btnAdd100h.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnAdd100h.Click += new System.EventHandler(this.btnAdd100h_Click);
            // 
            // ucGKParams1
            // 
            this.ucGKParams1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.ucGKParams1.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ucGKParams1.Key = "100h";
            this.ucGKParams1.Location = new System.Drawing.Point(908, 19);
            this.ucGKParams1.Margin = new System.Windows.Forms.Padding(5, 9, 5, 9);
            this.ucGKParams1.Name = "ucGKParams1";
            this.ucGKParams1.Size = new System.Drawing.Size(703, 848);
            this.ucGKParams1.TabIndex = 657;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.label2.Font = new System.Drawing.Font("微软雅黑", 16F);
            this.label2.Location = new System.Drawing.Point(3, 407);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(211, 30);
            this.label2.TabIndex = 653;
            this.label2.Text = "当前选中试验阶段：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.label1.Font = new System.Drawing.Font("微软雅黑", 16F);
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(154, 30);
            this.label1.TabIndex = 652;
            this.label1.Text = "100h主流程：";
            // 
            // lbl100hSore
            // 
            this.lbl100hSore.AutoSize = true;
            this.lbl100hSore.Font = new System.Drawing.Font("微软雅黑", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl100hSore.Location = new System.Drawing.Point(308, 407);
            this.lbl100hSore.Name = "lbl100hSore";
            this.lbl100hSore.Size = new System.Drawing.Size(23, 30);
            this.lbl100hSore.TabIndex = 445;
            this.lbl100hSore.Text = "-";
            this.lbl100hSore.Visible = false;
            // 
            // uc100hParams
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.Controls.Add(this.uiGroupBox1);
            this.Name = "uc100hParams";
            this.Size = new System.Drawing.Size(1616, 870);
            this.Load += new System.EventHandler(this.ucTestParams_Load);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.StepNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv100hMH)).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMH)).EndInit();
            this.uiGroupBox1.ResumeLayout(false);
            this.uiGroupBox1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud100hNum)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Sunny.UI.UIGroupBox uiGroupBox1;
        private System.Windows.Forms.DataGridView dgvMH;
        private System.Windows.Forms.Label lbl100hStepName;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.NumericUpDown StepNum;
        private System.Windows.Forms.Label lbl;
        private Sunny.UI.UIButton btnAdd100hStep;
        private System.Windows.Forms.DataGridView dgv100hMH;
        private System.Windows.Forms.Label lbl100hSore;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem tsmCopy;
        private System.Windows.Forms.ToolStripMenuItem tsmPaste;
        private System.Windows.Forms.ToolStripMenuItem tsmDelete;
        private System.Windows.Forms.DataGridViewTextBoxColumn StepIndex100;
        private System.Windows.Forms.DataGridViewTextBoxColumn StepName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn GK;
        private System.Windows.Forms.DataGridViewTextBoxColumn Torque;
        private System.Windows.Forms.DataGridViewTextBoxColumn RPM;
        private System.Windows.Forms.DataGridViewTextBoxColumn RunTime;
        private System.Windows.Forms.DataGridViewButtonColumn Column2;
        private ucGKParams ucGKParams1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Index;
        private System.Windows.Forms.DataGridViewComboBoxColumn TestName;
        private System.Windows.Forms.DataGridViewTextBoxColumn CollectIntervalTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn ForeachNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn Col100Day;
        private System.Windows.Forms.DataGridViewButtonColumn Column4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown nud100hNum;
        private System.Windows.Forms.Label label3;
        private Sunny.UI.UIButton btnAdd100h;
    }
}
