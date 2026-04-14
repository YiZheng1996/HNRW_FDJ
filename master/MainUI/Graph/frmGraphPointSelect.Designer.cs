
namespace MainUI.Graph
{
    partial class frmGraphPointSelect
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.grpDI = new Sunny.UI.UIGroupBox();
            this.uiGroupBox1 = new Sunny.UI.UIGroupBox();
            this.lblSpeed = new System.Windows.Forms.Label();
            this.lblTorque = new System.Windows.Forms.Label();
            this.lblPower = new System.Windows.Forms.Label();
            this.lblTemp = new System.Windows.Forms.Label();
            this.lblPress = new System.Windows.Forms.Label();
            this.dgvMH = new System.Windows.Forms.DataGridView();
            this.Index = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Unit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblFlow = new System.Windows.Forms.Label();
            this.grpDI.SuspendLayout();
            this.uiGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMH)).BeginInit();
            this.SuspendLayout();
            // 
            // grpDI
            // 
            this.grpDI.Controls.Add(this.uiGroupBox1);
            this.grpDI.Controls.Add(this.dgvMH);
            this.grpDI.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpDI.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grpDI.Location = new System.Drawing.Point(0, 0);
            this.grpDI.Margin = new System.Windows.Forms.Padding(9, 13, 9, 13);
            this.grpDI.MinimumSize = new System.Drawing.Size(1, 1);
            this.grpDI.Name = "grpDI";
            this.grpDI.Padding = new System.Windows.Forms.Padding(0, 88, 0, 0);
            this.grpDI.Size = new System.Drawing.Size(695, 525);
            this.grpDI.TabIndex = 392;
            this.grpDI.Text = "鼠标双击表格中共的点位";
            this.grpDI.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.grpDI.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Controls.Add(this.lblSpeed);
            this.uiGroupBox1.Controls.Add(this.lblTorque);
            this.uiGroupBox1.Controls.Add(this.lblFlow);
            this.uiGroupBox1.Controls.Add(this.lblPower);
            this.uiGroupBox1.Controls.Add(this.lblTemp);
            this.uiGroupBox1.Controls.Add(this.lblPress);
            this.uiGroupBox1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiGroupBox1.Location = new System.Drawing.Point(12, 28);
            this.uiGroupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiGroupBox1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Padding = new System.Windows.Forms.Padding(0, 32, 0, 0);
            this.uiGroupBox1.Size = new System.Drawing.Size(668, 87);
            this.uiGroupBox1.TabIndex = 396;
            this.uiGroupBox1.Text = "点位类别";
            this.uiGroupBox1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.uiGroupBox1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // lblSpeed
            // 
            this.lblSpeed.BackColor = System.Drawing.Color.DarkGray;
            this.lblSpeed.Font = new System.Drawing.Font("宋体", 16F);
            this.lblSpeed.Location = new System.Drawing.Point(558, 27);
            this.lblSpeed.Name = "lblSpeed";
            this.lblSpeed.Size = new System.Drawing.Size(106, 48);
            this.lblSpeed.TabIndex = 398;
            this.lblSpeed.Tag = "";
            this.lblSpeed.Text = "转速";
            this.lblSpeed.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblSpeed.Click += new System.EventHandler(this.lblTemp_Click);
            // 
            // lblTorque
            // 
            this.lblTorque.BackColor = System.Drawing.Color.DarkGray;
            this.lblTorque.Font = new System.Drawing.Font("宋体", 16F);
            this.lblTorque.Location = new System.Drawing.Point(447, 27);
            this.lblTorque.Name = "lblTorque";
            this.lblTorque.Size = new System.Drawing.Size(106, 48);
            this.lblTorque.TabIndex = 397;
            this.lblTorque.Tag = "";
            this.lblTorque.Text = "扭矩";
            this.lblTorque.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTorque.Click += new System.EventHandler(this.lblTemp_Click);
            // 
            // lblPower
            // 
            this.lblPower.BackColor = System.Drawing.Color.DarkGray;
            this.lblPower.Font = new System.Drawing.Font("宋体", 16F);
            this.lblPower.Location = new System.Drawing.Point(336, 27);
            this.lblPower.Name = "lblPower";
            this.lblPower.Size = new System.Drawing.Size(106, 48);
            this.lblPower.TabIndex = 396;
            this.lblPower.Tag = "";
            this.lblPower.Text = "功率";
            this.lblPower.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblPower.Click += new System.EventHandler(this.lblTemp_Click);
            // 
            // lblTemp
            // 
            this.lblTemp.BackColor = System.Drawing.Color.DarkGray;
            this.lblTemp.Font = new System.Drawing.Font("宋体", 16F);
            this.lblTemp.Location = new System.Drawing.Point(3, 27);
            this.lblTemp.Name = "lblTemp";
            this.lblTemp.Size = new System.Drawing.Size(106, 48);
            this.lblTemp.TabIndex = 394;
            this.lblTemp.Tag = "";
            this.lblTemp.Text = "温度";
            this.lblTemp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTemp.Click += new System.EventHandler(this.lblTemp_Click);
            // 
            // lblPress
            // 
            this.lblPress.BackColor = System.Drawing.Color.DarkGray;
            this.lblPress.Font = new System.Drawing.Font("宋体", 16F);
            this.lblPress.Location = new System.Drawing.Point(114, 27);
            this.lblPress.Name = "lblPress";
            this.lblPress.Size = new System.Drawing.Size(106, 48);
            this.lblPress.TabIndex = 395;
            this.lblPress.Tag = "";
            this.lblPress.Text = "压力";
            this.lblPress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblPress.Click += new System.EventHandler(this.lblTemp_Click);
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
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(4);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMH.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvMH.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMH.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Index,
            this.Time,
            this.Unit});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvMH.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvMH.EnableHeadersVisualStyles = false;
            this.dgvMH.Location = new System.Drawing.Point(12, 123);
            this.dgvMH.Name = "dgvMH";
            this.dgvMH.ReadOnly = true;
            this.dgvMH.RowHeadersVisible = false;
            this.dgvMH.RowTemplate.Height = 23;
            this.dgvMH.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMH.Size = new System.Drawing.Size(671, 390);
            this.dgvMH.TabIndex = 393;
            this.dgvMH.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMH_CellContentDoubleClick);
            // 
            // Index
            // 
            this.Index.DataPropertyName = "Type";
            this.Index.FillWeight = 60.13713F;
            this.Index.HeaderText = "类别";
            this.Index.Name = "Index";
            this.Index.ReadOnly = true;
            // 
            // Time
            // 
            this.Time.DataPropertyName = "Point";
            this.Time.FillWeight = 88.83249F;
            this.Time.HeaderText = "点位名称";
            this.Time.Name = "Time";
            this.Time.ReadOnly = true;
            // 
            // Unit
            // 
            this.Unit.DataPropertyName = "Unit";
            this.Unit.HeaderText = "单位";
            this.Unit.Name = "Unit";
            this.Unit.ReadOnly = true;
            this.Unit.Visible = false;
            // 
            // lblFlow
            // 
            this.lblFlow.BackColor = System.Drawing.Color.DarkGray;
            this.lblFlow.Font = new System.Drawing.Font("宋体", 16F);
            this.lblFlow.Location = new System.Drawing.Point(225, 27);
            this.lblFlow.Name = "lblFlow";
            this.lblFlow.Size = new System.Drawing.Size(106, 48);
            this.lblFlow.TabIndex = 396;
            this.lblFlow.Tag = "";
            this.lblFlow.Text = "流量";
            this.lblFlow.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblFlow.Click += new System.EventHandler(this.lblTemp_Click);
            // 
            // frmGraphPointSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(695, 525);
            this.Controls.Add(this.grpDI);
            this.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frmGraphPointSelect";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmPointSelect_Load);
            this.grpDI.ResumeLayout(false);
            this.uiGroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMH)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UIGroupBox grpDI;
        private System.Windows.Forms.DataGridView dgvMH;
        private System.Windows.Forms.Label lblTemp;
        private System.Windows.Forms.Label lblPress;
        private Sunny.UI.UIGroupBox uiGroupBox1;
        private System.Windows.Forms.Label lblPower;
        private System.Windows.Forms.Label lblTorque;
        private System.Windows.Forms.Label lblSpeed;
        private System.Windows.Forms.DataGridViewTextBoxColumn Index;
        private System.Windows.Forms.DataGridViewTextBoxColumn Time;
        private System.Windows.Forms.DataGridViewTextBoxColumn Unit;
        private System.Windows.Forms.Label lblFlow;
    }
}