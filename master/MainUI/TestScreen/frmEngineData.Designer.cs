
namespace MainUI
{
    partial class frmEngineData
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
            this.ucForm21 = new MainUI.ucForm2();
            this.SuspendLayout();
            // 
            // ucForm21
            // 
            this.ucForm21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.ucForm21.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucForm21.Location = new System.Drawing.Point(0, 0);
            this.ucForm21.Name = "ucForm21";
            this.ucForm21.Size = new System.Drawing.Size(1920, 1080);
            this.ucForm21.TabIndex = 0;
            // 
            // frmEngineData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(1920, 1080);
            this.Controls.Add(this.ucForm21);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmEngineData";
            this.Text = "发动机ECM模块/管路数据监控界面";
            this.ResumeLayout(false);

        }

        #endregion

        private ucForm2 ucForm21;
    }
}