namespace MainUI.Procedure
{
    partial class ucGKParams
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.btnExcelIn = new Sunny.UI.UIButton();
            this.StepNum = new System.Windows.Forms.NumericUpDown();
            this.lbl = new System.Windows.Forms.Label();
            this.btnAdd100hStep = new Sunny.UI.UIButton();
            this.dgvMH = new System.Windows.Forms.DataGridView();
            this.StepIndex100 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GK = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RPM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Torque = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ExcitationCurrent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Power = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.StepNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMH)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox7
            // 
            this.groupBox7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.groupBox7.Controls.Add(this.btnExcelIn);
            this.groupBox7.Controls.Add(this.StepNum);
            this.groupBox7.Controls.Add(this.lbl);
            this.groupBox7.Controls.Add(this.btnAdd100hStep);
            this.groupBox7.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.groupBox7.Location = new System.Drawing.Point(5, 744);
            this.groupBox7.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Padding = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.groupBox7.Size = new System.Drawing.Size(693, 95);
            this.groupBox7.TabIndex = 442;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "新增工况";
            // 
            // btnExcelIn
            // 
            this.btnExcelIn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExcelIn.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.btnExcelIn.Location = new System.Drawing.Point(520, 44);
            this.btnExcelIn.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btnExcelIn.MinimumSize = new System.Drawing.Size(2, 1);
            this.btnExcelIn.Name = "btnExcelIn";
            this.btnExcelIn.Size = new System.Drawing.Size(142, 45);
            this.btnExcelIn.TabIndex = 443;
            this.btnExcelIn.Text = "从工况表进行导入";
            this.btnExcelIn.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnExcelIn.TipsText = "1";
            this.btnExcelIn.Visible = false;
            this.btnExcelIn.Click += new System.EventHandler(this.btnExcelIn_Click);
            // 
            // StepNum
            // 
            this.StepNum.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.StepNum.Location = new System.Drawing.Point(148, 37);
            this.StepNum.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.StepNum.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.StepNum.Name = "StepNum";
            this.StepNum.Size = new System.Drawing.Size(128, 34);
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
            this.lbl.Location = new System.Drawing.Point(19, 42);
            this.lbl.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lbl.Name = "lbl";
            this.lbl.Size = new System.Drawing.Size(107, 25);
            this.lbl.TabIndex = 417;
            this.lbl.Text = "新增工况数";
            // 
            // btnAdd100hStep
            // 
            this.btnAdd100hStep.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd100hStep.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.btnAdd100hStep.Location = new System.Drawing.Point(311, 34);
            this.btnAdd100hStep.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btnAdd100hStep.MinimumSize = new System.Drawing.Size(2, 1);
            this.btnAdd100hStep.Name = "btnAdd100hStep";
            this.btnAdd100hStep.Size = new System.Drawing.Size(153, 41);
            this.btnAdd100hStep.Style = Sunny.UI.UIStyle.Custom;
            this.btnAdd100hStep.TabIndex = 438;
            this.btnAdd100hStep.Text = "新增";
            this.btnAdd100hStep.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAdd100hStep.TipsText = "1";
            this.btnAdd100hStep.Click += new System.EventHandler(this.btnAdd100hStep_Click);
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
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微软雅黑", 14F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(4);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMH.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvMH.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMH.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.StepIndex100,
            this.GK,
            this.RPM,
            this.Torque,
            this.ExcitationCurrent,
            this.Power,
            this.Column2});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("微软雅黑", 14F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvMH.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvMH.EnableHeadersVisualStyles = false;
            this.dgvMH.Location = new System.Drawing.Point(3, 42);
            this.dgvMH.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.dgvMH.Name = "dgvMH";
            this.dgvMH.RowHeadersVisible = false;
            this.dgvMH.RowTemplate.Height = 23;
            this.dgvMH.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMH.Size = new System.Drawing.Size(695, 697);
            this.dgvMH.TabIndex = 437;
            this.dgvMH.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgvMH_CellBeginEdit);
            this.dgvMH.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMH_CellContentClick);
            this.dgvMH.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMH_CellEndEdit);
            this.dgvMH.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMH_CellEnter);
            this.dgvMH.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvMH_CellValidating);
            this.dgvMH.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvMH_DataError);
            // 
            // StepIndex100
            // 
            this.StepIndex100.DataPropertyName = "Index";
            this.StepIndex100.FillWeight = 50F;
            this.StepIndex100.HeaderText = "序号";
            this.StepIndex100.Name = "StepIndex100";
            this.StepIndex100.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.StepIndex100.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // GK
            // 
            this.GK.FillWeight = 50F;
            this.GK.HeaderText = "工况编号";
            this.GK.Name = "GK";
            this.GK.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // RPM
            // 
            this.RPM.DataPropertyName = "RPM";
            dataGridViewCellStyle2.Format = "F0";
            this.RPM.DefaultCellStyle = dataGridViewCellStyle2;
            this.RPM.FillWeight = 80F;
            this.RPM.HeaderText = "转速值(rpm)";
            this.RPM.Name = "RPM";
            this.RPM.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Torque
            // 
            this.Torque.DataPropertyName = "Torque";
            dataGridViewCellStyle3.Format = "F0";
            this.Torque.DefaultCellStyle = dataGridViewCellStyle3;
            this.Torque.FillWeight = 80F;
            this.Torque.HeaderText = "扭矩值(N.m)";
            this.Torque.Name = "Torque";
            this.Torque.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ExcitationCurrent
            // 
            this.ExcitationCurrent.DataPropertyName = "ExcitationCurrent";
            this.ExcitationCurrent.FillWeight = 80F;
            this.ExcitationCurrent.HeaderText = "励磁电流(A)";
            this.ExcitationCurrent.Name = "ExcitationCurrent";
            this.ExcitationCurrent.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Power
            // 
            this.Power.DataPropertyName = "Power";
            this.Power.FillWeight = 80F;
            this.Power.HeaderText = "功率(kW)";
            this.Power.Name = "Power";
            this.Power.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column2
            // 
            this.Column2.FillWeight = 60F;
            this.Column2.HeaderText = "操作";
            this.Column2.Name = "Column2";
            this.Column2.Text = "删除";
            this.Column2.UseColumnTextForButtonValue = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblTitle);
            this.panel1.Controls.Add(this.dgvMH);
            this.panel1.Controls.Add(this.groupBox7);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(703, 848);
            this.panel1.TabIndex = 443;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(3, 11);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(120, 25);
            this.lblTitle.TabIndex = 443;
            this.lblTitle.Text = "360h 工况表";
            // 
            // ucGKParams
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(5, 9, 5, 9);
            this.Name = "ucGKParams";
            this.Size = new System.Drawing.Size(703, 848);
            this.Load += new System.EventHandler(this.ucModelManage_Load);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.StepNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMH)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView dgvMH;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.NumericUpDown StepNum;
        private System.Windows.Forms.Label lbl;
        private Sunny.UI.UIButton btnAdd100hStep;
        private Sunny.UI.UIButton btnExcelIn;
        private System.Windows.Forms.DataGridViewTextBoxColumn StepIndex100;
        private System.Windows.Forms.DataGridViewTextBoxColumn GK;
        private System.Windows.Forms.DataGridViewTextBoxColumn RPM;
        private System.Windows.Forms.DataGridViewTextBoxColumn Torque;
        private System.Windows.Forms.DataGridViewTextBoxColumn ExcitationCurrent;
        private System.Windows.Forms.DataGridViewTextBoxColumn Power;
        private System.Windows.Forms.DataGridViewButtonColumn Column2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblTitle;
    }
}
