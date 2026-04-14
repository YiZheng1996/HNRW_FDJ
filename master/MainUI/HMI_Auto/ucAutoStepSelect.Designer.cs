
namespace MainUI.HMI_Auto
{
    partial class ucAutoStepSelect
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblNo = new System.Windows.Forms.Label();
            this.lblTestName = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblModel = new System.Windows.Forms.Label();
            this.btnSelectModel = new Sunny.UI.UIButton();
            this.uiButton1 = new Sunny.UI.UIButton();
            this.lblLSGKTime = new System.Windows.Forms.Label();
            this.lblLSGKSore = new System.Windows.Forms.Label();
            this.lblLSGKName = new System.Windows.Forms.Label();
            this.lblLSSore = new System.Windows.Forms.Label();
            this.GridViewStepAll = new System.Windows.Forms.DataGridView();
            this.Index = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StepName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn26 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn27 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column107 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridLoopCode = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblTestName2 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.btnExit = new Sunny.UI.UIButton();
            this.btnStart = new Sunny.UI.UIButton();
            this.label13 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.lblGKTime = new System.Windows.Forms.Label();
            this.lblGKSore = new System.Windows.Forms.Label();
            this.lblNodeName = new System.Windows.Forms.Label();
            this.lblSelectSore = new System.Windows.Forms.Label();
            this.pictureBox15 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblLSGKBH = new System.Windows.Forms.Label();
            this.lblGKBH = new System.Windows.Forms.Label();
            this.lblSpeed = new System.Windows.Forms.Label();
            this.lblLSSpeed = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblLC = new System.Windows.Forms.Label();
            this.lblLSLC = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.lblLSPower = new System.Windows.Forms.Label();
            this.lblPower = new System.Windows.Forms.Label();
            this.ucStepTrackBar2 = new MainUI.HMI_Auto.ucStepTrackBar();
            this.ucStepTrackBar1 = new MainUI.HMI_Auto.ucStepTrackBar();
            this.label18 = new System.Windows.Forms.Label();
            this.lblCurrentSpeed = new System.Windows.Forms.Label();
            this.lblCurrentPower = new System.Windows.Forms.Label();
            this.lblCurrentTorque = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridViewStepAll)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridLoopCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox15)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblNo);
            this.groupBox1.Controls.Add(this.lblTestName);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.lblModel);
            this.groupBox1.Controls.Add(this.btnSelectModel);
            this.groupBox1.Controls.Add(this.uiButton1);
            this.groupBox1.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.groupBox1.Location = new System.Drawing.Point(12, 6);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Size = new System.Drawing.Size(541, 78);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "试验信息";
            // 
            // lblNo
            // 
            this.lblNo.AutoSize = true;
            this.lblNo.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblNo.Location = new System.Drawing.Point(409, 32);
            this.lblNo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNo.Name = "lblNo";
            this.lblNo.Size = new System.Drawing.Size(117, 27);
            this.lblNo.TabIndex = 0;
            this.lblNo.Text = "00000-000";
            // 
            // lblTestName
            // 
            this.lblTestName.AutoSize = true;
            this.lblTestName.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTestName.Location = new System.Drawing.Point(111, 32);
            this.lblTestName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTestName.Name = "lblTestName";
            this.lblTestName.Size = new System.Drawing.Size(61, 27);
            this.lblTestName.TabIndex = 9;
            this.lblTestName.Text = "100h";
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(5, 29);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(106, 33);
            this.label9.TabIndex = 8;
            this.label9.Text = "试验类型：";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(192, 29);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 33);
            this.label1.TabIndex = 0;
            this.label1.Text = "型号：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(343, 29);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 33);
            this.label3.TabIndex = 0;
            this.label3.Text = "编号：";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblModel
            // 
            this.lblModel.AutoSize = true;
            this.lblModel.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblModel.Location = new System.Drawing.Point(262, 32);
            this.lblModel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblModel.Name = "lblModel";
            this.lblModel.Size = new System.Drawing.Size(52, 27);
            this.lblModel.TabIndex = 1;
            this.lblModel.Text = "型号";
            // 
            // btnSelectModel
            // 
            this.btnSelectModel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSelectModel.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.btnSelectModel.Location = new System.Drawing.Point(542, 29);
            this.btnSelectModel.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnSelectModel.Name = "btnSelectModel";
            this.btnSelectModel.Size = new System.Drawing.Size(160, 37);
            this.btnSelectModel.StyleCustomMode = true;
            this.btnSelectModel.TabIndex = 686;
            this.btnSelectModel.Text = "重新开始";
            this.btnSelectModel.TipsFont = new System.Drawing.Font("微软雅黑", 13F);
            this.btnSelectModel.Visible = false;
            this.btnSelectModel.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiButton1
            // 
            this.uiButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiButton1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(190)))), ((int)(((byte)(40)))));
            this.uiButton1.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(190)))), ((int)(((byte)(40)))));
            this.uiButton1.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(203)))), ((int)(((byte)(83)))));
            this.uiButton1.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(152)))), ((int)(((byte)(32)))));
            this.uiButton1.FillSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(152)))), ((int)(((byte)(32)))));
            this.uiButton1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiButton1.Location = new System.Drawing.Point(744, 29);
            this.uiButton1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiButton1.Name = "uiButton1";
            this.uiButton1.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(190)))), ((int)(((byte)(40)))));
            this.uiButton1.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(203)))), ((int)(((byte)(83)))));
            this.uiButton1.RectPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(152)))), ((int)(((byte)(32)))));
            this.uiButton1.RectSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(152)))), ((int)(((byte)(32)))));
            this.uiButton1.Size = new System.Drawing.Size(192, 38);
            this.uiButton1.Style = Sunny.UI.UIStyle.Green;
            this.uiButton1.TabIndex = 698;
            this.uiButton1.Text = "开始单个循环代码";
            this.uiButton1.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiButton1.Visible = false;
            this.uiButton1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // lblLSGKTime
            // 
            this.lblLSGKTime.AutoSize = true;
            this.lblLSGKTime.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblLSGKTime.Location = new System.Drawing.Point(717, 619);
            this.lblLSGKTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLSGKTime.Name = "lblLSGKTime";
            this.lblLSGKTime.Size = new System.Drawing.Size(24, 27);
            this.lblLSGKTime.TabIndex = 11;
            this.lblLSGKTime.Text = "1";
            // 
            // lblLSGKSore
            // 
            this.lblLSGKSore.AutoSize = true;
            this.lblLSGKSore.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblLSGKSore.Location = new System.Drawing.Point(716, 444);
            this.lblLSGKSore.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLSGKSore.Name = "lblLSGKSore";
            this.lblLSGKSore.Size = new System.Drawing.Size(24, 27);
            this.lblLSGKSore.TabIndex = 7;
            this.lblLSGKSore.Text = "1";
            // 
            // lblLSGKName
            // 
            this.lblLSGKName.AutoSize = true;
            this.lblLSGKName.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblLSGKName.Location = new System.Drawing.Point(716, 409);
            this.lblLSGKName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLSGKName.Name = "lblLSGKName";
            this.lblLSGKName.Size = new System.Drawing.Size(193, 27);
            this.lblLSGKName.TabIndex = 5;
            this.lblLSGKName.Text = "1-交替突变负荷试验";
            // 
            // lblLSSore
            // 
            this.lblLSSore.AutoSize = true;
            this.lblLSSore.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblLSSore.Location = new System.Drawing.Point(716, 374);
            this.lblLSSore.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLSSore.Name = "lblLSSore";
            this.lblLSSore.Size = new System.Drawing.Size(36, 27);
            this.lblLSSore.TabIndex = 3;
            this.lblLSSore.Text = "00";
            // 
            // GridViewStepAll
            // 
            this.GridViewStepAll.AllowUserToAddRows = false;
            this.GridViewStepAll.AllowUserToDeleteRows = false;
            this.GridViewStepAll.AllowUserToResizeColumns = false;
            this.GridViewStepAll.AllowUserToResizeRows = false;
            this.GridViewStepAll.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.GridViewStepAll.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微软雅黑", 11F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(4);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GridViewStepAll.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.GridViewStepAll.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridViewStepAll.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Index,
            this.StepName,
            this.dataGridViewTextBoxColumn26,
            this.dataGridViewTextBoxColumn27,
            this.Column107});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 11F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.GridViewStepAll.DefaultCellStyle = dataGridViewCellStyle2;
            this.GridViewStepAll.EnableHeadersVisualStyles = false;
            this.GridViewStepAll.Location = new System.Drawing.Point(12, 179);
            this.GridViewStepAll.MultiSelect = false;
            this.GridViewStepAll.Name = "GridViewStepAll";
            this.GridViewStepAll.ReadOnly = true;
            this.GridViewStepAll.RowHeadersVisible = false;
            this.GridViewStepAll.RowTemplate.Height = 23;
            this.GridViewStepAll.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GridViewStepAll.Size = new System.Drawing.Size(541, 220);
            this.GridViewStepAll.TabIndex = 670;
            // 
            // Index
            // 
            this.Index.DataPropertyName = "Index";
            this.Index.FillWeight = 50F;
            this.Index.HeaderText = "步骤";
            this.Index.Name = "Index";
            this.Index.ReadOnly = true;
            this.Index.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // StepName
            // 
            this.StepName.DataPropertyName = "StepName";
            this.StepName.FillWeight = 50F;
            this.StepName.HeaderText = "阶段";
            this.StepName.Name = "StepName";
            this.StepName.ReadOnly = true;
            this.StepName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn26
            // 
            this.dataGridViewTextBoxColumn26.FillWeight = 50F;
            this.dataGridViewTextBoxColumn26.HeaderText = "周期";
            this.dataGridViewTextBoxColumn26.Name = "dataGridViewTextBoxColumn26";
            this.dataGridViewTextBoxColumn26.ReadOnly = true;
            this.dataGridViewTextBoxColumn26.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn27
            // 
            this.dataGridViewTextBoxColumn27.FillWeight = 130F;
            this.dataGridViewTextBoxColumn27.HeaderText = "循环代码";
            this.dataGridViewTextBoxColumn27.Name = "dataGridViewTextBoxColumn27";
            this.dataGridViewTextBoxColumn27.ReadOnly = true;
            this.dataGridViewTextBoxColumn27.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column107
            // 
            this.Column107.FillWeight = 50F;
            this.Column107.HeaderText = "天数";
            this.Column107.Name = "Column107";
            this.Column107.ReadOnly = true;
            this.Column107.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridLoopCode
            // 
            this.dataGridLoopCode.AllowUserToAddRows = false;
            this.dataGridLoopCode.AllowUserToDeleteRows = false;
            this.dataGridLoopCode.AllowUserToResizeColumns = false;
            this.dataGridLoopCode.AllowUserToResizeRows = false;
            this.dataGridLoopCode.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridLoopCode.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("微软雅黑", 11F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(4);
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridLoopCode.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridLoopCode.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridLoopCode.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.Column2,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.Column1});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("微软雅黑", 11F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridLoopCode.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridLoopCode.EnableHeadersVisualStyles = false;
            this.dataGridLoopCode.Location = new System.Drawing.Point(12, 477);
            this.dataGridLoopCode.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridLoopCode.MultiSelect = false;
            this.dataGridLoopCode.Name = "dataGridLoopCode";
            this.dataGridLoopCode.ReadOnly = true;
            this.dataGridLoopCode.RowHeadersVisible = false;
            this.dataGridLoopCode.RowTemplate.Height = 23;
            this.dataGridLoopCode.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridLoopCode.Size = new System.Drawing.Size(541, 273);
            this.dataGridLoopCode.TabIndex = 672;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Index";
            this.dataGridViewTextBoxColumn1.FillWeight = 50F;
            this.dataGridViewTextBoxColumn1.HeaderText = "序号";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "工况编号";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.FillWeight = 80F;
            this.dataGridViewTextBoxColumn2.HeaderText = "转速 %";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "StepName";
            this.dataGridViewTextBoxColumn3.FillWeight = 70F;
            this.dataGridViewTextBoxColumn3.HeaderText = "扭矩 %";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "运行时间 min";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.Cornsilk;
            this.lblTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTitle.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle.Location = new System.Drawing.Point(12, 440);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(541, 36);
            this.lblTitle.TabIndex = 671;
            this.lblTitle.Text = "A 循环代码";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTestName2
            // 
            this.lblTestName2.BackColor = System.Drawing.Color.Cornsilk;
            this.lblTestName2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTestName2.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTestName2.Location = new System.Drawing.Point(12, 142);
            this.lblTestName2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTestName2.Name = "lblTestName2";
            this.lblTestName2.Size = new System.Drawing.Size(541, 36);
            this.lblTestName2.TabIndex = 673;
            this.lblTestName2.Text = "100h 执行步骤";
            this.lblTestName2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label12
            // 
            this.label12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label12.Font = new System.Drawing.Font("微软雅黑", 20F, System.Drawing.FontStyle.Bold);
            this.label12.Location = new System.Drawing.Point(12, 96);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(541, 47);
            this.label12.TabIndex = 674;
            this.label12.Text = "鼠标点击选择步骤、循环代码";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label17
            // 
            this.label17.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.label17.Location = new System.Drawing.Point(575, 406);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(146, 33);
            this.label17.TabIndex = 680;
            this.label17.Text = "循环代码：";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label19
            // 
            this.label19.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.label19.Location = new System.Drawing.Point(579, 371);
            this.label19.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(142, 33);
            this.label19.TabIndex = 678;
            this.label19.Text = "步骤：";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.btnExit.Location = new System.Drawing.Point(580, 710);
            this.btnExit.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnExit.Name = "btnExit";
            this.btnExit.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.btnExit.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(115)))), ((int)(((byte)(115)))));
            this.btnExit.RectPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnExit.RectSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnExit.Size = new System.Drawing.Size(552, 38);
            this.btnExit.Style = Sunny.UI.UIStyle.Red;
            this.btnExit.TabIndex = 683;
            this.btnExit.Text = "返回界面";
            this.btnExit.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnExit.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnStart
            // 
            this.btnStart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStart.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(190)))), ((int)(((byte)(40)))));
            this.btnStart.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(190)))), ((int)(((byte)(40)))));
            this.btnStart.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(203)))), ((int)(((byte)(83)))));
            this.btnStart.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(152)))), ((int)(((byte)(32)))));
            this.btnStart.FillSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(152)))), ((int)(((byte)(32)))));
            this.btnStart.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnStart.Location = new System.Drawing.Point(581, 656);
            this.btnStart.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnStart.Name = "btnStart";
            this.btnStart.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(190)))), ((int)(((byte)(40)))));
            this.btnStart.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(203)))), ((int)(((byte)(83)))));
            this.btnStart.RectPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(152)))), ((int)(((byte)(32)))));
            this.btnStart.RectSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(152)))), ((int)(((byte)(32)))));
            this.btnStart.Size = new System.Drawing.Size(551, 38);
            this.btnStart.Style = Sunny.UI.UIStyle.Green;
            this.btnStart.TabIndex = 682;
            this.btnStart.Text = "开始试验";
            this.btnStart.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnStart.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // label13
            // 
            this.label13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label13.Font = new System.Drawing.Font("微软雅黑", 13F, System.Drawing.FontStyle.Bold);
            this.label13.Location = new System.Drawing.Point(583, 106);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(539, 36);
            this.label13.TabIndex = 687;
            this.label13.Text = "上一次试验工况执行时间(min)";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label21
            // 
            this.label21.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.label21.Location = new System.Drawing.Point(575, 441);
            this.label21.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(146, 33);
            this.label21.TabIndex = 688;
            this.label21.Text = "工况序号：";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label22
            // 
            this.label22.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label22.Font = new System.Drawing.Font("微软雅黑", 13F, System.Drawing.FontStyle.Bold);
            this.label22.Location = new System.Drawing.Point(580, 318);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(192, 43);
            this.label22.TabIndex = 689;
            this.label22.Text = "上一次试验数据";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label16
            // 
            this.label16.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.label16.Location = new System.Drawing.Point(576, 616);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(146, 33);
            this.label16.TabIndex = 690;
            this.label16.Text = "工况执行时间：";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label25
            // 
            this.label25.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label25.Font = new System.Drawing.Font("微软雅黑", 13F, System.Drawing.FontStyle.Bold);
            this.label25.Location = new System.Drawing.Point(939, 318);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(192, 43);
            this.label25.TabIndex = 693;
            this.label25.Text = "重新开始试验参数";
            this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblGKTime
            // 
            this.lblGKTime.AutoSize = true;
            this.lblGKTime.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblGKTime.Location = new System.Drawing.Point(939, 619);
            this.lblGKTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblGKTime.Name = "lblGKTime";
            this.lblGKTime.Size = new System.Drawing.Size(36, 27);
            this.lblGKTime.TabIndex = 697;
            this.lblGKTime.Text = "00";
            // 
            // lblGKSore
            // 
            this.lblGKSore.AutoSize = true;
            this.lblGKSore.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblGKSore.Location = new System.Drawing.Point(938, 444);
            this.lblGKSore.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblGKSore.Name = "lblGKSore";
            this.lblGKSore.Size = new System.Drawing.Size(36, 27);
            this.lblGKSore.TabIndex = 696;
            this.lblGKSore.Text = "00";
            // 
            // lblNodeName
            // 
            this.lblNodeName.AutoSize = true;
            this.lblNodeName.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblNodeName.Location = new System.Drawing.Point(938, 409);
            this.lblNodeName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNodeName.Name = "lblNodeName";
            this.lblNodeName.Size = new System.Drawing.Size(193, 27);
            this.lblNodeName.TabIndex = 695;
            this.lblNodeName.Text = "1-交替突变负荷试验";
            // 
            // lblSelectSore
            // 
            this.lblSelectSore.AutoSize = true;
            this.lblSelectSore.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblSelectSore.Location = new System.Drawing.Point(938, 374);
            this.lblSelectSore.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSelectSore.Name = "lblSelectSore";
            this.lblSelectSore.Size = new System.Drawing.Size(36, 27);
            this.lblSelectSore.TabIndex = 694;
            this.lblSelectSore.Text = "00";
            // 
            // pictureBox15
            // 
            this.pictureBox15.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox15.Image = global::MainUI.Properties.Resources.向下;
            this.pictureBox15.Location = new System.Drawing.Point(221, 400);
            this.pictureBox15.Name = "pictureBox15";
            this.pictureBox15.Size = new System.Drawing.Size(70, 40);
            this.pictureBox15.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox15.TabIndex = 796;
            this.pictureBox15.TabStop = false;
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 13F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(582, 221);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(539, 36);
            this.label2.TabIndex = 798;
            this.label2.Text = "本次试验工况开始时间(min)";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.label4.Location = new System.Drawing.Point(574, 476);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(146, 33);
            this.label4.TabIndex = 800;
            this.label4.Text = "工况编号：";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblLSGKBH
            // 
            this.lblLSGKBH.AutoSize = true;
            this.lblLSGKBH.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblLSGKBH.Location = new System.Drawing.Point(716, 479);
            this.lblLSGKBH.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLSGKBH.Name = "lblLSGKBH";
            this.lblLSGKBH.Size = new System.Drawing.Size(24, 27);
            this.lblLSGKBH.TabIndex = 801;
            this.lblLSGKBH.Text = "1";
            // 
            // lblGKBH
            // 
            this.lblGKBH.AutoSize = true;
            this.lblGKBH.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblGKBH.Location = new System.Drawing.Point(938, 479);
            this.lblGKBH.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblGKBH.Name = "lblGKBH";
            this.lblGKBH.Size = new System.Drawing.Size(36, 27);
            this.lblGKBH.TabIndex = 802;
            this.lblGKBH.Text = "00";
            // 
            // lblSpeed
            // 
            this.lblSpeed.AutoSize = true;
            this.lblSpeed.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblSpeed.Location = new System.Drawing.Point(938, 514);
            this.lblSpeed.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSpeed.Name = "lblSpeed";
            this.lblSpeed.Size = new System.Drawing.Size(36, 27);
            this.lblSpeed.TabIndex = 805;
            this.lblSpeed.Text = "00";
            // 
            // lblLSSpeed
            // 
            this.lblLSSpeed.AutoSize = true;
            this.lblLSSpeed.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblLSSpeed.Location = new System.Drawing.Point(716, 514);
            this.lblLSSpeed.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLSSpeed.Name = "lblLSSpeed";
            this.lblLSSpeed.Size = new System.Drawing.Size(24, 27);
            this.lblLSSpeed.TabIndex = 804;
            this.lblLSSpeed.Text = "1";
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.label7.Location = new System.Drawing.Point(574, 511);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(146, 33);
            this.label7.TabIndex = 803;
            this.label7.Text = "设定转速 rpm：";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblLC
            // 
            this.lblLC.AutoSize = true;
            this.lblLC.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblLC.Location = new System.Drawing.Point(938, 549);
            this.lblLC.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLC.Name = "lblLC";
            this.lblLC.Size = new System.Drawing.Size(36, 27);
            this.lblLC.TabIndex = 808;
            this.lblLC.Text = "00";
            // 
            // lblLSLC
            // 
            this.lblLSLC.AutoSize = true;
            this.lblLSLC.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblLSLC.Location = new System.Drawing.Point(716, 549);
            this.lblLSLC.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLSLC.Name = "lblLSLC";
            this.lblLSLC.Size = new System.Drawing.Size(24, 27);
            this.lblLSLC.TabIndex = 807;
            this.lblLSLC.Text = "1";
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.label11.Location = new System.Drawing.Point(562, 546);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(158, 33);
            this.label11.TabIndex = 806;
            this.label11.Text = "设定励磁电流 A：";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.label6.Location = new System.Drawing.Point(921, 8);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(115, 24);
            this.label6.TabIndex = 811;
            this.label6.Text = "实时功率 kW";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.label10.Location = new System.Drawing.Point(579, 8);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(123, 24);
            this.label10.TabIndex = 809;
            this.label10.Text = "实时转速 rpm";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label14
            // 
            this.label14.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.label14.Location = new System.Drawing.Point(564, 581);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(158, 33);
            this.label14.TabIndex = 813;
            this.label14.Text = "目标功率 kW：";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblLSPower
            // 
            this.lblLSPower.AutoSize = true;
            this.lblLSPower.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblLSPower.Location = new System.Drawing.Point(717, 584);
            this.lblLSPower.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLSPower.Name = "lblLSPower";
            this.lblLSPower.Size = new System.Drawing.Size(24, 27);
            this.lblLSPower.TabIndex = 814;
            this.lblLSPower.Text = "1";
            // 
            // lblPower
            // 
            this.lblPower.AutoSize = true;
            this.lblPower.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPower.Location = new System.Drawing.Point(939, 584);
            this.lblPower.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPower.Name = "lblPower";
            this.lblPower.Size = new System.Drawing.Size(36, 27);
            this.lblPower.TabIndex = 815;
            this.lblPower.Text = "00";
            // 
            // ucStepTrackBar2
            // 
            this.ucStepTrackBar2.CurrentValue = 0D;
            this.ucStepTrackBar2.DecimalNumber = 0;
            this.ucStepTrackBar2.Font = new System.Drawing.Font("宋体", 9F);
            this.ucStepTrackBar2.Location = new System.Drawing.Point(581, 262);
            this.ucStepTrackBar2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ucStepTrackBar2.MaxValue = 8000;
            this.ucStepTrackBar2.MinValue = 0;
            this.ucStepTrackBar2.Name = "ucStepTrackBar2";
            this.ucStepTrackBar2.ReadOnly = false;
            this.ucStepTrackBar2.Size = new System.Drawing.Size(559, 48);
            this.ucStepTrackBar2.TabIndex = 799;
            this.ucStepTrackBar2.OnValueChanged += new MainUI.HMI_Auto.ucStepTrackBar.ValueChangedHandler(this.ucStepTrackBar2_OnValueChanged);
            // 
            // ucStepTrackBar1
            // 
            this.ucStepTrackBar1.CurrentValue = 0D;
            this.ucStepTrackBar1.DecimalNumber = 0;
            this.ucStepTrackBar1.Font = new System.Drawing.Font("宋体", 9F);
            this.ucStepTrackBar1.Location = new System.Drawing.Point(582, 147);
            this.ucStepTrackBar1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ucStepTrackBar1.MaxValue = 8000;
            this.ucStepTrackBar1.MinValue = 0;
            this.ucStepTrackBar1.Name = "ucStepTrackBar1";
            this.ucStepTrackBar1.ReadOnly = true;
            this.ucStepTrackBar1.Size = new System.Drawing.Size(559, 48);
            this.ucStepTrackBar1.TabIndex = 797;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.label18.Location = new System.Drawing.Point(751, 8);
            this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(123, 24);
            this.label18.TabIndex = 816;
            this.label18.Text = "实时扭矩 N·m";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCurrentSpeed
            // 
            this.lblCurrentSpeed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblCurrentSpeed.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCurrentSpeed.Font = new System.Drawing.Font("微软雅黑", 15F);
            this.lblCurrentSpeed.Location = new System.Drawing.Point(584, 35);
            this.lblCurrentSpeed.Name = "lblCurrentSpeed";
            this.lblCurrentSpeed.Size = new System.Drawing.Size(112, 30);
            this.lblCurrentSpeed.TabIndex = 819;
            this.lblCurrentSpeed.Text = "0";
            this.lblCurrentSpeed.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCurrentPower
            // 
            this.lblCurrentPower.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblCurrentPower.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCurrentPower.Font = new System.Drawing.Font("微软雅黑", 15F);
            this.lblCurrentPower.Location = new System.Drawing.Point(922, 35);
            this.lblCurrentPower.Name = "lblCurrentPower";
            this.lblCurrentPower.Size = new System.Drawing.Size(112, 30);
            this.lblCurrentPower.TabIndex = 820;
            this.lblCurrentPower.Text = "0";
            this.lblCurrentPower.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCurrentTorque
            // 
            this.lblCurrentTorque.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblCurrentTorque.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCurrentTorque.Font = new System.Drawing.Font("微软雅黑", 15F);
            this.lblCurrentTorque.Location = new System.Drawing.Point(756, 35);
            this.lblCurrentTorque.Name = "lblCurrentTorque";
            this.lblCurrentTorque.Size = new System.Drawing.Size(112, 30);
            this.lblCurrentTorque.TabIndex = 821;
            this.lblCurrentTorque.Text = "0";
            this.lblCurrentTorque.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // ucAutoStepSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(1271, 763);
            this.ControlBox = false;
            this.Controls.Add(this.lblCurrentTorque);
            this.Controls.Add(this.lblCurrentPower);
            this.Controls.Add(this.lblCurrentSpeed);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.lblPower);
            this.Controls.Add(this.lblLSPower);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.lblLC);
            this.Controls.Add(this.lblLSLC);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.lblSpeed);
            this.Controls.Add(this.lblLSSpeed);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lblGKBH);
            this.Controls.Add(this.lblLSGKBH);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ucStepTrackBar2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblLSGKTime);
            this.Controls.Add(this.ucStepTrackBar1);
            this.Controls.Add(this.pictureBox15);
            this.Controls.Add(this.lblLSGKSore);
            this.Controls.Add(this.lblGKTime);
            this.Controls.Add(this.lblGKSore);
            this.Controls.Add(this.lblNodeName);
            this.Controls.Add(this.lblSelectSore);
            this.Controls.Add(this.lblLSGKName);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.lblLSSore);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.lblTestName2);
            this.Controls.Add(this.dataGridLoopCode);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.GridViewStepAll);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ucAutoStepSelect";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "自动试验 步骤选择";
            this.Load += new System.EventHandler(this.ucAutoStepSelect_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridViewStepAll)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridLoopCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox15)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblModel;
        private System.Windows.Forms.Label lblNo;
        private System.Windows.Forms.Label lblLSSore;
        private System.Windows.Forms.Label lblLSGKName;
        private System.Windows.Forms.Label lblLSGKSore;
        private System.Windows.Forms.Label lblTestName;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DataGridView GridViewStepAll;
        private System.Windows.Forms.DataGridView dataGridLoopCode;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblTestName2;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lblLSGKTime;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label19;
        private Sunny.UI.UIButton btnExit;
        private Sunny.UI.UIButton btnStart;
        private Sunny.UI.UIButton btnSelectModel;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label lblGKTime;
        private System.Windows.Forms.Label lblGKSore;
        private System.Windows.Forms.Label lblNodeName;
        private System.Windows.Forms.Label lblSelectSore;
        private Sunny.UI.UIButton uiButton1;
        private System.Windows.Forms.PictureBox pictureBox15;
        private ucStepTrackBar ucStepTrackBar1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Index;
        private System.Windows.Forms.DataGridViewTextBoxColumn StepName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn26;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn27;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column107;
        private ucStepTrackBar ucStepTrackBar2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblLSGKBH;
        private System.Windows.Forms.Label lblGKBH;
        private System.Windows.Forms.Label lblSpeed;
        private System.Windows.Forms.Label lblLSSpeed;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblLC;
        private System.Windows.Forms.Label lblLSLC;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lblLSPower;
        private System.Windows.Forms.Label lblPower;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label lblCurrentSpeed;
        private System.Windows.Forms.Label lblCurrentPower;
        private System.Windows.Forms.Label lblCurrentTorque;
        private System.Windows.Forms.Timer timer1;
    }
}