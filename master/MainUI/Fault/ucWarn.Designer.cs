
namespace MainUI.Fault
{
    partial class ucWarn
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbtAlarm = new RW.UI.Controls.RButton();
            this.rbtShedding = new RW.UI.Controls.RButton();
            this.rbtStop = new RW.UI.Controls.RButton();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle.Location = new System.Drawing.Point(-4, 8);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(209, 20);
            this.lblTitle.TabIndex = 9;
            this.lblTitle.Text = "后增压器进口油压>330";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rbtStop);
            this.panel1.Controls.Add(this.rbtShedding);
            this.panel1.Controls.Add(this.rbtAlarm);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(296, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(303, 37);
            this.panel1.TabIndex = 13;
            // 
            // rbtAlarm
            // 
            this.rbtAlarm.BackColor = System.Drawing.Color.White;
            this.rbtAlarm.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.rbtAlarm.FalseColor = System.Drawing.Color.White;
            this.rbtAlarm.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rbtAlarm.InputDriverName = "";
            this.rbtAlarm.InputTagName = "";
            this.rbtAlarm.Location = new System.Drawing.Point(7, 2);
            this.rbtAlarm.Name = "rbtAlarm";
            this.rbtAlarm.OutputTagName = "";
            this.rbtAlarm.Size = new System.Drawing.Size(86, 33);
            this.rbtAlarm.SwitchType = RW.UI.Controls.SwitchStyleEnums.Switch;
            this.rbtAlarm.TabIndex = 760;
            this.rbtAlarm.Tag = "1";
            this.rbtAlarm.TrueColor = System.Drawing.Color.Red;
            // 
            // rbtShedding
            // 
            this.rbtShedding.BackColor = System.Drawing.Color.White;
            this.rbtShedding.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.rbtShedding.FalseColor = System.Drawing.Color.White;
            this.rbtShedding.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rbtShedding.InputDriverName = "";
            this.rbtShedding.InputTagName = "";
            this.rbtShedding.Location = new System.Drawing.Point(107, 2);
            this.rbtShedding.Name = "rbtShedding";
            this.rbtShedding.OutputTagName = "";
            this.rbtShedding.Size = new System.Drawing.Size(86, 33);
            this.rbtShedding.SwitchType = RW.UI.Controls.SwitchStyleEnums.Switch;
            this.rbtShedding.TabIndex = 761;
            this.rbtShedding.Tag = "1";
            this.rbtShedding.TrueColor = System.Drawing.Color.Red;
            // 
            // rbtStop
            // 
            this.rbtStop.BackColor = System.Drawing.Color.White;
            this.rbtStop.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.rbtStop.FalseColor = System.Drawing.Color.White;
            this.rbtStop.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rbtStop.InputDriverName = "";
            this.rbtStop.InputTagName = "";
            this.rbtStop.Location = new System.Drawing.Point(207, 2);
            this.rbtStop.Name = "rbtStop";
            this.rbtStop.OutputTagName = "";
            this.rbtStop.Size = new System.Drawing.Size(86, 33);
            this.rbtStop.SwitchType = RW.UI.Controls.SwitchStyleEnums.Switch;
            this.rbtStop.TabIndex = 762;
            this.rbtStop.Tag = "1";
            this.rbtStop.TrueColor = System.Drawing.Color.Red;
            // 
            // ucWarn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblTitle);
            this.Name = "ucWarn";
            this.Size = new System.Drawing.Size(599, 37);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panel1;
        private RW.UI.Controls.RButton rbtStop;
        private RW.UI.Controls.RButton rbtShedding;
        private RW.UI.Controls.RButton rbtAlarm;
    }
}
