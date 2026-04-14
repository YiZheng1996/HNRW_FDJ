
namespace MainUI
{
    partial class FrmPipeControl
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
            this.ucALLPipelineHMI1 = new MainUI.ucALLPipelineHMI();
            this.SuspendLayout();
            // 
            // ucALLPipelineHMI1
            // 
            this.ucALLPipelineHMI1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.ucALLPipelineHMI1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucALLPipelineHMI1.Font = new System.Drawing.Font("宋体", 11F);
            this.ucALLPipelineHMI1.Location = new System.Drawing.Point(0, 0);
            this.ucALLPipelineHMI1.Margin = new System.Windows.Forms.Padding(4);
            this.ucALLPipelineHMI1.Name = "ucALLPipelineHMI1";
            this.ucALLPipelineHMI1.Size = new System.Drawing.Size(1920, 1080);
            this.ucALLPipelineHMI1.TabIndex = 0;
            // 
            // FrmPipeControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(1920, 1080);
            this.Controls.Add(this.ucALLPipelineHMI1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmPipeControl";
            this.Text = "油、水管路系统控制界面";
            this.ResumeLayout(false);

        }

        #endregion

        private ucALLPipelineHMI ucALLPipelineHMI1;
    }
}