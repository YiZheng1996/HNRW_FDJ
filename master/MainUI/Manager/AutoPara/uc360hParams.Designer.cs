namespace MainUI.Procedure
{
    partial class uc360hParams
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.StepNum = new System.Windows.Forms.NumericUpDown();
            this.lbl = new System.Windows.Forms.Label();
            this.btnAdd360hStep = new Sunny.UI.UIButton();
            this.uiGroupBox1 = new Sunny.UI.UIGroupBox();
            this.dgvMHDur = new System.Windows.Forms.DataGridView();
            this.ColIndex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColJD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColZQ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColCode = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.ColDay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.StepNum)).BeginInit();
            this.uiGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMHDur)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox7
            // 
            this.groupBox7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.groupBox7.Controls.Add(this.StepNum);
            this.groupBox7.Controls.Add(this.lbl);
            this.groupBox7.Controls.Add(this.btnAdd360hStep);
            this.groupBox7.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.groupBox7.Location = new System.Drawing.Point(20, 665);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(952, 167);
            this.groupBox7.TabIndex = 441;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "新增步骤";
            // 
            // StepNum
            // 
            this.StepNum.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.StepNum.Location = new System.Drawing.Point(166, 35);
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
            this.lbl.Location = new System.Drawing.Point(31, 40);
            this.lbl.Name = "lbl";
            this.lbl.Size = new System.Drawing.Size(107, 25);
            this.lbl.TabIndex = 417;
            this.lbl.Text = "新增步骤数";
            // 
            // btnAdd360hStep
            // 
            this.btnAdd360hStep.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd360hStep.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.btnAdd360hStep.Location = new System.Drawing.Point(318, 32);
            this.btnAdd360hStep.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnAdd360hStep.Name = "btnAdd360hStep";
            this.btnAdd360hStep.Size = new System.Drawing.Size(120, 40);
            this.btnAdd360hStep.Style = Sunny.UI.UIStyle.Custom;
            this.btnAdd360hStep.TabIndex = 438;
            this.btnAdd360hStep.Text = "新增";
            this.btnAdd360hStep.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAdd360hStep.TipsText = "1";
            this.btnAdd360hStep.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnAdd360hStep.Click += new System.EventHandler(this.btnAdd360hStep_Click);
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Controls.Add(this.dgvMHDur);
            this.uiGroupBox1.Controls.Add(this.label1);
            this.uiGroupBox1.Controls.Add(this.groupBox7);
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
            // dgvMHDur
            // 
            this.dgvMHDur.AllowUserToAddRows = false;
            this.dgvMHDur.AllowUserToDeleteRows = false;
            this.dgvMHDur.AllowUserToResizeColumns = false;
            this.dgvMHDur.AllowUserToResizeRows = false;
            this.dgvMHDur.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMHDur.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvMHDur.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(4);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMHDur.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvMHDur.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMHDur.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColIndex,
            this.ColJD,
            this.ColZQ,
            this.ColCode,
            this.ColDay,
            this.Column1});
            this.dgvMHDur.ContextMenuStrip = this.contextMenuStrip;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvMHDur.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvMHDur.EnableHeadersVisualStyles = false;
            this.dgvMHDur.Location = new System.Drawing.Point(20, 59);
            this.dgvMHDur.Name = "dgvMHDur";
            this.dgvMHDur.RowHeadersVisible = false;
            this.dgvMHDur.RowTemplate.Height = 23;
            this.dgvMHDur.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMHDur.Size = new System.Drawing.Size(952, 600);
            this.dgvMHDur.TabIndex = 654;
            this.dgvMHDur.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgvMHDur_CellBeginEdit);
            this.dgvMHDur.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMHDur_CellContentClick);
            this.dgvMHDur.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMHDur_CellEndEdit);
            this.dgvMHDur.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMHDur_CellEnter);
            this.dgvMHDur.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvMHDur_CellValidating);
            this.dgvMHDur.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvMHDur_DataError);
            this.dgvMHDur.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvMHDur_EditingControlShowing);
            // 
            // ColIndex
            // 
            this.ColIndex.DataPropertyName = "Index";
            this.ColIndex.FillWeight = 40F;
            this.ColIndex.HeaderText = "序号";
            this.ColIndex.Name = "ColIndex";
            // 
            // ColJD
            // 
            this.ColJD.DataPropertyName = "ColJD";
            this.ColJD.FillWeight = 70F;
            this.ColJD.HeaderText = "阶段";
            this.ColJD.Name = "ColJD";
            this.ColJD.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ColZQ
            // 
            this.ColZQ.DataPropertyName = "Index";
            this.ColZQ.FillWeight = 70F;
            this.ColZQ.HeaderText = "周期名";
            this.ColZQ.Name = "ColZQ";
            this.ColZQ.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ColCode
            // 
            this.ColCode.DataPropertyName = "LoopCode";
            this.ColCode.HeaderText = "循环代码";
            this.ColCode.Name = "ColCode";
            // 
            // ColDay
            // 
            this.ColDay.DataPropertyName = "Day";
            this.ColDay.FillWeight = 70F;
            this.ColDay.HeaderText = "天数";
            this.ColDay.Name = "ColDay";
            this.ColDay.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column1
            // 
            this.Column1.FillWeight = 50F;
            this.Column1.HeaderText = "操作";
            this.Column1.Name = "Column1";
            this.Column1.Text = "删除";
            this.Column1.UseColumnTextForButtonValue = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.label1.Font = new System.Drawing.Font("微软雅黑", 16F);
            this.label1.Location = new System.Drawing.Point(15, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(154, 30);
            this.label1.TabIndex = 652;
            this.label1.Text = "360h主流程：";
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
            // uc360hParams
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.Controls.Add(this.uiGroupBox1);
            this.Name = "uc360hParams";
            this.Size = new System.Drawing.Size(1616, 870);
            this.Load += new System.EventHandler(this.ucTestParams_Load);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.StepNum)).EndInit();
            this.uiGroupBox1.ResumeLayout(false);
            this.uiGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMHDur)).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private Sunny.UI.UIGroupBox uiGroupBox1;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.NumericUpDown StepNum;
        private System.Windows.Forms.Label lbl;
        private Sunny.UI.UIButton btnAdd360hStep;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvMHDur;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColIndex;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColJD;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColZQ;
        private System.Windows.Forms.DataGridViewComboBoxColumn ColCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColDay;
        private System.Windows.Forms.DataGridViewButtonColumn Column1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem tsmCopy;
        private System.Windows.Forms.ToolStripMenuItem tsmPaste;
        private System.Windows.Forms.ToolStripMenuItem tsmDelete;
    }
}
