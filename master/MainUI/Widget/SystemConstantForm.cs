using MainUI;
using MainUI.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainUI
{
    public partial class SystemConstantForm : Form
    {
        SysConstantParas sysConstantParas = new SysConstantParas();

        // 构造函数，接受窗体标题和背景色
        public SystemConstantForm()
        {
            InitializeComponent();

            sysConstantParas.Save();
        }


        private Label label6;
        private NumericUpDown nudLiquidLevel;
        private Label label4;
        private Label label5;
        private Sunny.UI.UIButton btnSelectModel;

        #region Windows 窗体设计器生成的代码
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.label6 = new System.Windows.Forms.Label();
            this.nudLiquidLevel = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnSelectModel = new Sunny.UI.UIButton();
            ((System.ComponentModel.ISupportInitialize)(this.nudLiquidLevel)).BeginInit();
            this.SuspendLayout();
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(12, 88);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(126, 24);
            this.label6.TabIndex = 706;
            this.label6.Text = "目标液位(mm)";
            // 
            // nudLiquidLevel
            // 
            this.nudLiquidLevel.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nudLiquidLevel.Location = new System.Drawing.Point(143, 85);
            this.nudLiquidLevel.Name = "nudLiquidLevel";
            this.nudLiquidLevel.Size = new System.Drawing.Size(80, 29);
            this.nudLiquidLevel.TabIndex = 707;
            this.nudLiquidLevel.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudLiquidLevel.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 16F);
            this.label4.Location = new System.Drawing.Point(12, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(164, 22);
            this.label4.TabIndex = 708;
            this.label4.Text = "预热水箱水加热";
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(12, 57);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(418, 21);
            this.label5.TabIndex = 709;
            this.label5.Text = "高于目标液位时,启动加热器对预热水箱的水加热";
            // 
            // btnSelectModel
            // 
            this.btnSelectModel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSelectModel.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.btnSelectModel.Location = new System.Drawing.Point(16, 236);
            this.btnSelectModel.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnSelectModel.Name = "btnSelectModel";
            this.btnSelectModel.Size = new System.Drawing.Size(439, 34);
            this.btnSelectModel.StyleCustomMode = true;
            this.btnSelectModel.TabIndex = 710;
            this.btnSelectModel.Text = "保存";
            this.btnSelectModel.TipsFont = new System.Drawing.Font("微软雅黑", 13F);
            this.btnSelectModel.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnSelectModel.Click += new System.EventHandler(this.btnSelectModel_Click);
            // 
            // SystemConstantForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(467, 282);
            this.Controls.Add(this.btnSelectModel);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.nudLiquidLevel);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SystemConstantForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "系统参数设置";
            ((System.ComponentModel.ISupportInitialize)(this.nudLiquidLevel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private void btnSelectModel_Click(object sender, EventArgs e)
        {
            sysConstantParas.WaterLiquidLevel = this.nudLiquidLevel.Value.ToInt();
            sysConstantParas.Save();
        }
    }
}
