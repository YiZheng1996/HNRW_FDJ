namespace MainUI.Widget
{
    partial class FrmGKChangeInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmGKChangeInfo));
            this.lblTips = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Btn_Ok = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblTestStep = new System.Windows.Forms.Label();
            this.lblTestTime = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblOldTorque = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblNewTorque = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblOldRPM = new System.Windows.Forms.Label();
            this.lblNewRPM = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblChangeTime = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTips
            // 
            resources.ApplyResources(this.lblTips, "lblTips");
            this.lblTips.Name = "lblTips";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.Btn_Ok);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // Btn_Ok
            // 
            this.Btn_Ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            resources.ApplyResources(this.Btn_Ok, "Btn_Ok");
            this.Btn_Ok.Name = "Btn_Ok";
            this.Btn_Ok.UseVisualStyleBackColor = true;
            this.Btn_Ok.Click += new System.EventHandler(this.Btn_Ok_Click);
            // 
            // pictureBox1
            // 
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Image = global::MainUI.Properties.Resources.information;
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // lblTestStep
            // 
            resources.ApplyResources(this.lblTestStep, "lblTestStep");
            this.lblTestStep.Name = "lblTestStep";
            // 
            // lblTestTime
            // 
            resources.ApplyResources(this.lblTestTime, "lblTestTime");
            this.lblTestTime.Name = "lblTestTime";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // lblOldTorque
            // 
            resources.ApplyResources(this.lblOldTorque, "lblOldTorque");
            this.lblOldTorque.Name = "lblOldTorque";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // lblNewTorque
            // 
            resources.ApplyResources(this.lblNewTorque, "lblNewTorque");
            this.lblNewTorque.Name = "lblNewTorque";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // lblOldRPM
            // 
            resources.ApplyResources(this.lblOldRPM, "lblOldRPM");
            this.lblOldRPM.Name = "lblOldRPM";
            // 
            // lblNewRPM
            // 
            resources.ApplyResources(this.lblNewRPM, "lblNewRPM");
            this.lblNewRPM.Name = "lblNewRPM";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // lblChangeTime
            // 
            resources.ApplyResources(this.lblChangeTime, "lblChangeTime");
            this.lblChangeTime.ForeColor = System.Drawing.Color.DarkRed;
            this.lblChangeTime.Name = "lblChangeTime";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // FrmGKChangeInfo
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.ControlBox = false;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblTestTime);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.lblNewRPM);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblChangeTime);
            this.Controls.Add(this.lblOldRPM);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblNewTorque);
            this.Controls.Add(this.lblOldTorque);
            this.Controls.Add(this.lblTestStep);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblTips);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FrmGKChangeInfo";
            this.TopMost = true;
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTips;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button Btn_Ok;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblTestStep;
        private System.Windows.Forms.Label lblTestTime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblOldTorque;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblNewTorque;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblOldRPM;
        private System.Windows.Forms.Label lblNewRPM;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblChangeTime;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Timer timer1;
    }
}