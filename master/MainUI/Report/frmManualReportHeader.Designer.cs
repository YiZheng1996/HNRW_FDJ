namespace MainUI.Report
{
    partial class frmManualReportHeader
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private System.Windows.Forms.Label lblTestProject;
        private System.Windows.Forms.TextBox txtTestProject;
        private System.Windows.Forms.Label lblSuperchargerModel;
        private System.Windows.Forms.TextBox txtSuperchargerModel;
        private System.Windows.Forms.Label lblSuperchargerSN;
        private System.Windows.Forms.TextBox txtSuperchargerSN;
        private System.Windows.Forms.Label lblTestBenchNo;
        private System.Windows.Forms.TextBox txtTestBenchNo;
        private System.Windows.Forms.Label lblMainGeneratorNo;
        private System.Windows.Forms.TextBox txtMainGeneratorNo;
        private System.Windows.Forms.Label lblAvgOutsideTemp;
        private System.Windows.Forms.TextBox txtAvgOutsideTemp;
        private System.Windows.Forms.Label lblAvgAtmPressure;
        private System.Windows.Forms.TextBox txtAvgAtmPressure;
        private System.Windows.Forms.Label lblHumidity;
        private System.Windows.Forms.TextBox txtHumidity;
        private System.Windows.Forms.Label lblOilGrade;
        private System.Windows.Forms.TextBox txtOilGrade;
        private System.Windows.Forms.Label lblFuelGrade;
        private System.Windows.Forms.TextBox txtFuelGrade;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;

        private void InitializeComponent()
        {
            this.lblTestProject = new System.Windows.Forms.Label();
            this.txtTestProject = new System.Windows.Forms.TextBox();
            this.lblSuperchargerModel = new System.Windows.Forms.Label();
            this.txtSuperchargerModel = new System.Windows.Forms.TextBox();
            this.lblSuperchargerSN = new System.Windows.Forms.Label();
            this.txtSuperchargerSN = new System.Windows.Forms.TextBox();
            this.lblTestBenchNo = new System.Windows.Forms.Label();
            this.txtTestBenchNo = new System.Windows.Forms.TextBox();
            this.lblMainGeneratorNo = new System.Windows.Forms.Label();
            this.txtMainGeneratorNo = new System.Windows.Forms.TextBox();
            this.lblAvgOutsideTemp = new System.Windows.Forms.Label();
            this.txtAvgOutsideTemp = new System.Windows.Forms.TextBox();
            this.lblAvgAtmPressure = new System.Windows.Forms.Label();
            this.txtAvgAtmPressure = new System.Windows.Forms.TextBox();
            this.lblHumidity = new System.Windows.Forms.Label();
            this.txtHumidity = new System.Windows.Forms.TextBox();
            this.lblOilGrade = new System.Windows.Forms.Label();
            this.txtOilGrade = new System.Windows.Forms.TextBox();
            this.lblFuelGrade = new System.Windows.Forms.Label();
            this.txtFuelGrade = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblTestProject
            // 
            this.lblTestProject.AutoSize = true;
            this.lblTestProject.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lblTestProject.Location = new System.Drawing.Point(30, 36);
            this.lblTestProject.Name = "lblTestProject";
            this.lblTestProject.Size = new System.Drawing.Size(74, 21);
            this.lblTestProject.TabIndex = 0;
            this.lblTestProject.Text = "试验项目";
            // 
            // txtTestProject
            // 
            this.txtTestProject.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.txtTestProject.Location = new System.Drawing.Point(200, 30);
            this.txtTestProject.Name = "txtTestProject";
            this.txtTestProject.Size = new System.Drawing.Size(320, 29);
            this.txtTestProject.TabIndex = 1;
            // 
            // lblSuperchargerModel
            // 
            this.lblSuperchargerModel.AutoSize = true;
            this.lblSuperchargerModel.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lblSuperchargerModel.Location = new System.Drawing.Point(30, 84);
            this.lblSuperchargerModel.Name = "lblSuperchargerModel";
            this.lblSuperchargerModel.Size = new System.Drawing.Size(90, 21);
            this.lblSuperchargerModel.TabIndex = 2;
            this.lblSuperchargerModel.Text = "增压器型号";
            // 
            // txtSuperchargerModel
            // 
            this.txtSuperchargerModel.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.txtSuperchargerModel.Location = new System.Drawing.Point(200, 78);
            this.txtSuperchargerModel.Name = "txtSuperchargerModel";
            this.txtSuperchargerModel.Size = new System.Drawing.Size(320, 29);
            this.txtSuperchargerModel.TabIndex = 3;
            // 
            // lblSuperchargerSN
            // 
            this.lblSuperchargerSN.AutoSize = true;
            this.lblSuperchargerSN.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lblSuperchargerSN.Location = new System.Drawing.Point(30, 132);
            this.lblSuperchargerSN.Name = "lblSuperchargerSN";
            this.lblSuperchargerSN.Size = new System.Drawing.Size(122, 21);
            this.lblSuperchargerSN.TabIndex = 4;
            this.lblSuperchargerSN.Text = "增压器出厂编号";
            // 
            // txtSuperchargerSN
            // 
            this.txtSuperchargerSN.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.txtSuperchargerSN.Location = new System.Drawing.Point(200, 126);
            this.txtSuperchargerSN.Name = "txtSuperchargerSN";
            this.txtSuperchargerSN.Size = new System.Drawing.Size(320, 29);
            this.txtSuperchargerSN.TabIndex = 5;
            // 
            // lblTestBenchNo
            // 
            this.lblTestBenchNo.AutoSize = true;
            this.lblTestBenchNo.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lblTestBenchNo.Location = new System.Drawing.Point(30, 180);
            this.lblTestBenchNo.Name = "lblTestBenchNo";
            this.lblTestBenchNo.Size = new System.Drawing.Size(90, 21);
            this.lblTestBenchNo.TabIndex = 6;
            this.lblTestBenchNo.Text = "试验台位号";
            // 
            // txtTestBenchNo
            // 
            this.txtTestBenchNo.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.txtTestBenchNo.Location = new System.Drawing.Point(200, 174);
            this.txtTestBenchNo.Name = "txtTestBenchNo";
            this.txtTestBenchNo.Size = new System.Drawing.Size(320, 29);
            this.txtTestBenchNo.TabIndex = 7;
            // 
            // lblMainGeneratorNo
            // 
            this.lblMainGeneratorNo.AutoSize = true;
            this.lblMainGeneratorNo.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lblMainGeneratorNo.Location = new System.Drawing.Point(30, 228);
            this.lblMainGeneratorNo.Name = "lblMainGeneratorNo";
            this.lblMainGeneratorNo.Size = new System.Drawing.Size(106, 21);
            this.lblMainGeneratorNo.TabIndex = 8;
            this.lblMainGeneratorNo.Text = "主发电机编号";
            // 
            // txtMainGeneratorNo
            // 
            this.txtMainGeneratorNo.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.txtMainGeneratorNo.Location = new System.Drawing.Point(200, 222);
            this.txtMainGeneratorNo.Name = "txtMainGeneratorNo";
            this.txtMainGeneratorNo.Size = new System.Drawing.Size(320, 29);
            this.txtMainGeneratorNo.TabIndex = 9;
            // 
            // lblAvgOutsideTemp
            // 
            this.lblAvgOutsideTemp.AutoSize = true;
            this.lblAvgOutsideTemp.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lblAvgOutsideTemp.Location = new System.Drawing.Point(30, 276);
            this.lblAvgOutsideTemp.Name = "lblAvgOutsideTemp";
            this.lblAvgOutsideTemp.Size = new System.Drawing.Size(105, 21);
            this.lblAvgOutsideTemp.TabIndex = 10;
            this.lblAvgOutsideTemp.Text = "平均外温 (℃)";
            // 
            // txtAvgOutsideTemp
            // 
            this.txtAvgOutsideTemp.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.txtAvgOutsideTemp.Location = new System.Drawing.Point(200, 270);
            this.txtAvgOutsideTemp.Name = "txtAvgOutsideTemp";
            this.txtAvgOutsideTemp.Size = new System.Drawing.Size(320, 29);
            this.txtAvgOutsideTemp.TabIndex = 11;
            // 
            // lblAvgAtmPressure
            // 
            this.lblAvgAtmPressure.AutoSize = true;
            this.lblAvgAtmPressure.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lblAvgAtmPressure.Location = new System.Drawing.Point(30, 324);
            this.lblAvgAtmPressure.Name = "lblAvgAtmPressure";
            this.lblAvgAtmPressure.Size = new System.Drawing.Size(140, 21);
            this.lblAvgAtmPressure.TabIndex = 12;
            this.lblAvgAtmPressure.Text = "平均大气压力 (Pa)";
            // 
            // txtAvgAtmPressure
            // 
            this.txtAvgAtmPressure.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.txtAvgAtmPressure.Location = new System.Drawing.Point(200, 318);
            this.txtAvgAtmPressure.Name = "txtAvgAtmPressure";
            this.txtAvgAtmPressure.Size = new System.Drawing.Size(320, 29);
            this.txtAvgAtmPressure.TabIndex = 13;
            // 
            // lblHumidity
            // 
            this.lblHumidity.AutoSize = true;
            this.lblHumidity.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lblHumidity.Location = new System.Drawing.Point(30, 372);
            this.lblHumidity.Name = "lblHumidity";
            this.lblHumidity.Size = new System.Drawing.Size(103, 21);
            this.lblHumidity.TabIndex = 14;
            this.lblHumidity.Text = "相对湿度 (%)";
            // 
            // txtHumidity
            // 
            this.txtHumidity.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.txtHumidity.Location = new System.Drawing.Point(200, 366);
            this.txtHumidity.Name = "txtHumidity";
            this.txtHumidity.Size = new System.Drawing.Size(320, 29);
            this.txtHumidity.TabIndex = 15;
            // 
            // lblOilGrade
            // 
            this.lblOilGrade.AutoSize = true;
            this.lblOilGrade.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lblOilGrade.Location = new System.Drawing.Point(30, 420);
            this.lblOilGrade.Name = "lblOilGrade";
            this.lblOilGrade.Size = new System.Drawing.Size(74, 21);
            this.lblOilGrade.TabIndex = 16;
            this.lblOilGrade.Text = "机油牌号";
            // 
            // txtOilGrade
            // 
            this.txtOilGrade.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.txtOilGrade.Location = new System.Drawing.Point(200, 414);
            this.txtOilGrade.Name = "txtOilGrade";
            this.txtOilGrade.Size = new System.Drawing.Size(320, 29);
            this.txtOilGrade.TabIndex = 17;
            // 
            // lblFuelGrade
            // 
            this.lblFuelGrade.AutoSize = true;
            this.lblFuelGrade.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lblFuelGrade.Location = new System.Drawing.Point(30, 468);
            this.lblFuelGrade.Name = "lblFuelGrade";
            this.lblFuelGrade.Size = new System.Drawing.Size(74, 21);
            this.lblFuelGrade.TabIndex = 18;
            this.lblFuelGrade.Text = "燃油牌号";
            // 
            // txtFuelGrade
            // 
            this.txtFuelGrade.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.txtFuelGrade.Location = new System.Drawing.Point(200, 462);
            this.txtFuelGrade.Name = "txtFuelGrade";
            this.txtFuelGrade.Size = new System.Drawing.Size(320, 29);
            this.txtFuelGrade.TabIndex = 19;
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnOk.Location = new System.Drawing.Point(160, 530);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(140, 44);
            this.btnOk.TabIndex = 20;
            this.btnOk.Text = "确定导出";
            this.btnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnCancel.Location = new System.Drawing.Point(320, 530);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(140, 44);
            this.btnCancel.TabIndex = 21;
            this.btnCancel.Text = "取消";
            // 
            // frmManualReportHeader
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(560, 610);
            this.ControlBox = false;
            this.Controls.Add(this.lblTestProject);
            this.Controls.Add(this.txtTestProject);
            this.Controls.Add(this.lblSuperchargerModel);
            this.Controls.Add(this.txtSuperchargerModel);
            this.Controls.Add(this.lblSuperchargerSN);
            this.Controls.Add(this.txtSuperchargerSN);
            this.Controls.Add(this.lblTestBenchNo);
            this.Controls.Add(this.txtTestBenchNo);
            this.Controls.Add(this.lblMainGeneratorNo);
            this.Controls.Add(this.txtMainGeneratorNo);
            this.Controls.Add(this.lblAvgOutsideTemp);
            this.Controls.Add(this.txtAvgOutsideTemp);
            this.Controls.Add(this.lblAvgAtmPressure);
            this.Controls.Add(this.txtAvgAtmPressure);
            this.Controls.Add(this.lblHumidity);
            this.Controls.Add(this.txtHumidity);
            this.Controls.Add(this.lblOilGrade);
            this.Controls.Add(this.txtOilGrade);
            this.Controls.Add(this.lblFuelGrade);
            this.Controls.Add(this.txtFuelGrade);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmManualReportHeader";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "出厂试验报表信息填写";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}