namespace MainUI.Procedure.Test
{
    partial class frmTestStep
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTestStep));
            this.dgvMH = new System.Windows.Forms.DataGridView();
            this.label7 = new System.Windows.Forms.Label();
            this.lblAdd = new System.Windows.Forms.Label();
            this.btnStepSave = new Sunny.UI.UIButton();
            this.btnAddStep = new Sunny.UI.UIButton();
            this.btnStepEdit = new Sunny.UI.UIButton();
            this.btnStepDelete = new Sunny.UI.UIButton();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.RPMValue = new System.Windows.Forms.NumericUpDown();
            this.TorqueValue = new System.Windows.Forms.NumericUpDown();
            this.TimeValue = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbTestName = new System.Windows.Forms.Label();
            this.Index = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StepName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RPM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Torque = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RunTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMH)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RPMValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TorqueValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TimeValue)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvMH
            // 
            this.dgvMH.AllowUserToAddRows = false;
            this.dgvMH.AllowUserToDeleteRows = false;
            this.dgvMH.AllowUserToResizeColumns = false;
            this.dgvMH.AllowUserToResizeRows = false;
            this.dgvMH.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMH.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(4);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMH.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvMH.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMH.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Index,
            this.StepName,
            this.Column3,
            this.Column1,
            this.RPM,
            this.Torque,
            this.RunTime});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvMH.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvMH.EnableHeadersVisualStyles = false;
            this.dgvMH.Location = new System.Drawing.Point(12, 54);
            this.dgvMH.Name = "dgvMH";
            this.dgvMH.ReadOnly = true;
            this.dgvMH.RowHeadersVisible = false;
            this.dgvMH.RowTemplate.Height = 23;
            this.dgvMH.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMH.Size = new System.Drawing.Size(873, 321);
            this.dgvMH.TabIndex = 402;
            this.dgvMH.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMH_CellEnter);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(8, 13);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(115, 21);
            this.label7.TabIndex = 22;
            this.label7.Text = "试验阶段：";
            // 
            // lblAdd
            // 
            this.lblAdd.AutoSize = true;
            this.lblAdd.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblAdd.ForeColor = System.Drawing.Color.Red;
            this.lblAdd.Location = new System.Drawing.Point(819, 9);
            this.lblAdd.Name = "lblAdd";
            this.lblAdd.Size = new System.Drawing.Size(66, 25);
            this.lblAdd.TabIndex = 408;
            this.lblAdd.Text = "☆新增";
            this.lblAdd.Visible = false;
            // 
            // btnStepSave
            // 
            this.btnStepSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStepSave.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.btnStepSave.Location = new System.Drawing.Point(11, 560);
            this.btnStepSave.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnStepSave.Name = "btnStepSave";
            this.btnStepSave.Size = new System.Drawing.Size(874, 40);
            this.btnStepSave.TabIndex = 412;
            this.btnStepSave.Text = "返回";
            this.btnStepSave.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnStepSave.TipsText = "1";
            this.btnStepSave.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnStepSave.Click += new System.EventHandler(this.btnStepSave_Click);
            // 
            // btnAddStep
            // 
            this.btnAddStep.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddStep.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.btnAddStep.Location = new System.Drawing.Point(11, 496);
            this.btnAddStep.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnAddStep.Name = "btnAddStep";
            this.btnAddStep.Size = new System.Drawing.Size(120, 40);
            this.btnAddStep.TabIndex = 414;
            this.btnAddStep.Text = "添加";
            this.btnAddStep.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAddStep.TipsText = "1";
            this.btnAddStep.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnAddStep.Click += new System.EventHandler(this.btnAddStep_Click);
            // 
            // btnStepEdit
            // 
            this.btnStepEdit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStepEdit.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.btnStepEdit.Location = new System.Drawing.Point(166, 496);
            this.btnStepEdit.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnStepEdit.Name = "btnStepEdit";
            this.btnStepEdit.Size = new System.Drawing.Size(120, 40);
            this.btnStepEdit.TabIndex = 415;
            this.btnStepEdit.Text = "编辑";
            this.btnStepEdit.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnStepEdit.TipsText = "1";
            this.btnStepEdit.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnStepEdit.Click += new System.EventHandler(this.btnStepEdit_Click);
            // 
            // btnStepDelete
            // 
            this.btnStepDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStepDelete.FillColor = System.Drawing.Color.Tomato;
            this.btnStepDelete.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnStepDelete.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.btnStepDelete.Location = new System.Drawing.Point(765, 494);
            this.btnStepDelete.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnStepDelete.Name = "btnStepDelete";
            this.btnStepDelete.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnStepDelete.Size = new System.Drawing.Size(120, 40);
            this.btnStepDelete.Style = Sunny.UI.UIStyle.Custom;
            this.btnStepDelete.TabIndex = 416;
            this.btnStepDelete.Text = "删除";
            this.btnStepDelete.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnStepDelete.TipsText = "1";
            this.btnStepDelete.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnStepDelete.Click += new System.EventHandler(this.btnStepDelete_Click);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.numericUpDown1.Location = new System.Drawing.Point(687, 56);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            110,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(120, 34);
            this.numericUpDown1.TabIndex = 430;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(652, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(211, 21);
            this.label4.TabIndex = 429;
            this.label4.Text = "采集间隔时间（min）";
            // 
            // RPMValue
            // 
            this.RPMValue.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.RPMValue.Location = new System.Drawing.Point(20, 56);
            this.RPMValue.Maximum = new decimal(new int[] {
            110,
            0,
            0,
            0});
            this.RPMValue.Name = "RPMValue";
            this.RPMValue.Size = new System.Drawing.Size(120, 34);
            this.RPMValue.TabIndex = 423;
            // 
            // TorqueValue
            // 
            this.TorqueValue.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TorqueValue.Location = new System.Drawing.Point(224, 56);
            this.TorqueValue.Maximum = new decimal(new int[] {
            110,
            0,
            0,
            0});
            this.TorqueValue.Name = "TorqueValue";
            this.TorqueValue.Size = new System.Drawing.Size(121, 34);
            this.TorqueValue.TabIndex = 424;
            // 
            // TimeValue
            // 
            this.TimeValue.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TimeValue.Location = new System.Drawing.Point(436, 57);
            this.TimeValue.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.TimeValue.Name = "TimeValue";
            this.TimeValue.Size = new System.Drawing.Size(120, 34);
            this.TimeValue.TabIndex = 425;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(35, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 21);
            this.label3.TabIndex = 417;
            this.label3.Text = "转速（%）";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(240, 31);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(105, 21);
            this.label5.TabIndex = 418;
            this.label5.Text = "扭矩（%）";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(419, 32);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(169, 21);
            this.label10.TabIndex = 422;
            this.label10.Text = "运行时间（min）";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.TimeValue);
            this.groupBox1.Controls.Add(this.numericUpDown1);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.RPMValue);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.TorqueValue);
            this.groupBox1.Font = new System.Drawing.Font("宋体", 15.75F);
            this.groupBox1.Location = new System.Drawing.Point(12, 381);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(873, 100);
            this.groupBox1.TabIndex = 431;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "编辑";
            // 
            // cbTestName
            // 
            this.cbTestName.AutoSize = true;
            this.cbTestName.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbTestName.Location = new System.Drawing.Point(114, 11);
            this.cbTestName.Name = "cbTestName";
            this.cbTestName.Size = new System.Drawing.Size(27, 27);
            this.cbTestName.TabIndex = 432;
            this.cbTestName.Text = "-";
            // 
            // Index
            // 
            this.Index.DataPropertyName = "Index";
            this.Index.FillWeight = 50F;
            this.Index.HeaderText = "步骤号";
            this.Index.Name = "Index";
            this.Index.ReadOnly = true;
            this.Index.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // StepName
            // 
            this.StepName.DataPropertyName = "StepName";
            this.StepName.HeaderText = "阶段名称";
            this.StepName.Name = "StepName";
            this.StepName.ReadOnly = true;
            this.StepName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.StepName.Visible = false;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "周期";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Visible = false;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "节点";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Visible = false;
            // 
            // RPM
            // 
            this.RPM.DataPropertyName = "RPM";
            this.RPM.FillWeight = 70F;
            this.RPM.HeaderText = "转速（%）";
            this.RPM.Name = "RPM";
            this.RPM.ReadOnly = true;
            this.RPM.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Torque
            // 
            this.Torque.DataPropertyName = "Torque";
            this.Torque.FillWeight = 70F;
            this.Torque.HeaderText = "扭矩（%）";
            this.Torque.Name = "Torque";
            this.Torque.ReadOnly = true;
            this.Torque.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // RunTime
            // 
            this.RunTime.DataPropertyName = "RunTime";
            this.RunTime.FillWeight = 90F;
            this.RunTime.HeaderText = "运行时间(min)";
            this.RunTime.Name = "RunTime";
            this.RunTime.ReadOnly = true;
            this.RunTime.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // frmTestStep
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(905, 612);
            this.Controls.Add(this.cbTestName);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnStepDelete);
            this.Controls.Add(this.btnStepEdit);
            this.Controls.Add(this.btnAddStep);
            this.Controls.Add(this.btnStepSave);
            this.Controls.Add(this.lblAdd);
            this.Controls.Add(this.dgvMH);
            this.Controls.Add(this.label7);
            this.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmTestStep";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "100h 性能试验参数新增/编辑界面";
            ((System.ComponentModel.ISupportInitialize)(this.dgvMH)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RPMValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TorqueValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TimeValue)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridView dgvMH;
        private System.Windows.Forms.Label lblAdd;
        private Sunny.UI.UIButton btnStepSave;
        private Sunny.UI.UIButton btnAddStep;
        private Sunny.UI.UIButton btnStepEdit;
        private Sunny.UI.UIButton btnStepDelete;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown RPMValue;
        private System.Windows.Forms.NumericUpDown TorqueValue;
        private System.Windows.Forms.NumericUpDown TimeValue;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label10;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label cbTestName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Index;
        private System.Windows.Forms.DataGridViewTextBoxColumn StepName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn RPM;
        private System.Windows.Forms.DataGridViewTextBoxColumn Torque;
        private System.Windows.Forms.DataGridViewTextBoxColumn RunTime;
    }
}