
namespace MainUI.HMI_Auto
{
    partial class ucFuelConsumptionMeasurement
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
            this.lblOilTime = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.lblStopOilTime = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lblfuelConsumptionMeter = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lblCurrentWeight = new System.Windows.Forms.Label();
            this.lblCurrentLiquidLevel = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.lblMeasureReplenishment = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.lblCurrentPressure = new System.Windows.Forms.Label();
            this.lblStartOilTime = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.lblMeasureFuelConsumption = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.btnForceStop = new RW.UI.Controls.RButton();
            this.btnStartOil = new RW.UI.Controls.RButton();
            this.lblOilBeginP = new System.Windows.Forms.Label();
            this.btnStopOil = new RW.UI.Controls.RButton();
            this.lblBCWeight = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.lblOilBeginH = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.timerEngineOilConsumption = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // lblOilTime
            // 
            this.lblOilTime.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblOilTime.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblOilTime.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblOilTime.ForeColor = System.Drawing.Color.Black;
            this.lblOilTime.Location = new System.Drawing.Point(20, 429);
            this.lblOilTime.Name = "lblOilTime";
            this.lblOilTime.Size = new System.Drawing.Size(294, 28);
            this.lblOilTime.TabIndex = 1000;
            this.lblOilTime.Tag = "";
            this.lblOilTime.Text = "-";
            this.lblOilTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("宋体", 14F);
            this.label20.Location = new System.Drawing.Point(20, 404);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(133, 19);
            this.label20.TabIndex = 999;
            this.label20.Text = "测量时间（h）";
            // 
            // lblStopOilTime
            // 
            this.lblStopOilTime.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblStopOilTime.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblStopOilTime.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblStopOilTime.ForeColor = System.Drawing.Color.Black;
            this.lblStopOilTime.Location = new System.Drawing.Point(20, 341);
            this.lblStopOilTime.Name = "lblStopOilTime";
            this.lblStopOilTime.Size = new System.Drawing.Size(294, 28);
            this.lblStopOilTime.TabIndex = 998;
            this.lblStopOilTime.Tag = "";
            this.lblStopOilTime.Text = "-";
            this.lblStopOilTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("宋体", 14F);
            this.label11.Location = new System.Drawing.Point(20, 316);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(123, 19);
            this.label11.TabIndex = 997;
            this.label11.Text = "结束测量时间";
            // 
            // lblfuelConsumptionMeter
            // 
            this.lblfuelConsumptionMeter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblfuelConsumptionMeter.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblfuelConsumptionMeter.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblfuelConsumptionMeter.ForeColor = System.Drawing.Color.Black;
            this.lblfuelConsumptionMeter.Location = new System.Drawing.Point(682, 64);
            this.lblfuelConsumptionMeter.Name = "lblfuelConsumptionMeter";
            this.lblfuelConsumptionMeter.Size = new System.Drawing.Size(120, 28);
            this.lblfuelConsumptionMeter.TabIndex = 996;
            this.lblfuelConsumptionMeter.Tag = "";
            this.lblfuelConsumptionMeter.Text = "0.0";
            this.lblfuelConsumptionMeter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("宋体", 14F);
            this.label12.Location = new System.Drawing.Point(389, 116);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(289, 19);
            this.label12.TabIndex = 995;
            this.label12.Text = "燃油质量流量计消耗量 g/(kW*h)";
            // 
            // lblCurrentWeight
            // 
            this.lblCurrentWeight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblCurrentWeight.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCurrentWeight.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCurrentWeight.ForeColor = System.Drawing.Color.Black;
            this.lblCurrentWeight.Location = new System.Drawing.Point(251, 111);
            this.lblCurrentWeight.Name = "lblCurrentWeight";
            this.lblCurrentWeight.Size = new System.Drawing.Size(91, 28);
            this.lblCurrentWeight.TabIndex = 992;
            this.lblCurrentWeight.Tag = "";
            this.lblCurrentWeight.Text = "0.0";
            this.lblCurrentWeight.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCurrentLiquidLevel
            // 
            this.lblCurrentLiquidLevel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblCurrentLiquidLevel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCurrentLiquidLevel.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCurrentLiquidLevel.ForeColor = System.Drawing.Color.Black;
            this.lblCurrentLiquidLevel.Location = new System.Drawing.Point(251, 64);
            this.lblCurrentLiquidLevel.Name = "lblCurrentLiquidLevel";
            this.lblCurrentLiquidLevel.Size = new System.Drawing.Size(91, 28);
            this.lblCurrentLiquidLevel.TabIndex = 988;
            this.lblCurrentLiquidLevel.Tag = "";
            this.lblCurrentLiquidLevel.Text = "0.0";
            this.lblCurrentLiquidLevel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Font = new System.Drawing.Font("宋体", 14F);
            this.label31.Location = new System.Drawing.Point(20, 69);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(220, 19);
            this.label31.TabIndex = 987;
            this.label31.Text = "实时机油耗测量液位(mm)";
            // 
            // lblMeasureReplenishment
            // 
            this.lblMeasureReplenishment.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblMeasureReplenishment.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblMeasureReplenishment.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblMeasureReplenishment.ForeColor = System.Drawing.Color.Black;
            this.lblMeasureReplenishment.Location = new System.Drawing.Point(682, 109);
            this.lblMeasureReplenishment.Name = "lblMeasureReplenishment";
            this.lblMeasureReplenishment.Size = new System.Drawing.Size(120, 28);
            this.lblMeasureReplenishment.TabIndex = 983;
            this.lblMeasureReplenishment.Tag = "";
            this.lblMeasureReplenishment.Text = "0.0";
            this.lblMeasureReplenishment.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 14F);
            this.label7.Location = new System.Drawing.Point(20, 116);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(163, 19);
            this.label7.TabIndex = 990;
            this.label7.Text = "实时磅秤重量(kg)";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("宋体", 14F);
            this.label22.Location = new System.Drawing.Point(389, 69);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(194, 19);
            this.label22.TabIndex = 981;
            this.label22.Text = "燃油消耗量 g/(kW*h)";
            // 
            // lblCurrentPressure
            // 
            this.lblCurrentPressure.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblCurrentPressure.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCurrentPressure.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCurrentPressure.ForeColor = System.Drawing.Color.Black;
            this.lblCurrentPressure.Location = new System.Drawing.Point(251, 18);
            this.lblCurrentPressure.Name = "lblCurrentPressure";
            this.lblCurrentPressure.Size = new System.Drawing.Size(91, 28);
            this.lblCurrentPressure.TabIndex = 986;
            this.lblCurrentPressure.Tag = "";
            this.lblCurrentPressure.Text = "0.0";
            this.lblCurrentPressure.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblStartOilTime
            // 
            this.lblStartOilTime.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblStartOilTime.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblStartOilTime.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblStartOilTime.ForeColor = System.Drawing.Color.Black;
            this.lblStartOilTime.Location = new System.Drawing.Point(20, 252);
            this.lblStartOilTime.Name = "lblStartOilTime";
            this.lblStartOilTime.Size = new System.Drawing.Size(294, 28);
            this.lblStartOilTime.TabIndex = 994;
            this.lblStartOilTime.Tag = "";
            this.lblStartOilTime.Text = "-";
            this.lblStartOilTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("宋体", 14F);
            this.label29.Location = new System.Drawing.Point(20, 22);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(230, 19);
            this.label29.TabIndex = 985;
            this.label29.Text = "实时机油耗测量压力(kPa)";
            // 
            // lblMeasureFuelConsumption
            // 
            this.lblMeasureFuelConsumption.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblMeasureFuelConsumption.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblMeasureFuelConsumption.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblMeasureFuelConsumption.ForeColor = System.Drawing.Color.Black;
            this.lblMeasureFuelConsumption.Location = new System.Drawing.Point(682, 15);
            this.lblMeasureFuelConsumption.Name = "lblMeasureFuelConsumption";
            this.lblMeasureFuelConsumption.Size = new System.Drawing.Size(120, 28);
            this.lblMeasureFuelConsumption.TabIndex = 980;
            this.lblMeasureFuelConsumption.Tag = "";
            this.lblMeasureFuelConsumption.Text = "0.0";
            this.lblMeasureFuelConsumption.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("宋体", 14F);
            this.label19.Location = new System.Drawing.Point(389, 20);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(194, 19);
            this.label19.TabIndex = 979;
            this.label19.Text = "机油消耗量 g/(kW*h)";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("宋体", 14F);
            this.label10.Location = new System.Drawing.Point(20, 227);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(123, 19);
            this.label10.TabIndex = 993;
            this.label10.Text = "开始测量时间";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("宋体", 14F);
            this.label17.Location = new System.Drawing.Point(357, 404);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(239, 19);
            this.label17.TabIndex = 982;
            this.label17.Text = "机油耗磅秤重量初始值(kg)";
            // 
            // btnForceStop
            // 
            this.btnForceStop.BackColor = System.Drawing.Color.Silver;
            this.btnForceStop.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnForceStop.FalseColor = System.Drawing.Color.Silver;
            this.btnForceStop.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnForceStop.InputDriverName = "";
            this.btnForceStop.InputTagName = "";
            this.btnForceStop.Location = new System.Drawing.Point(441, 165);
            this.btnForceStop.Name = "btnForceStop";
            this.btnForceStop.OutputTagName = "";
            this.btnForceStop.Size = new System.Drawing.Size(146, 33);
            this.btnForceStop.SwitchType = RW.UI.Controls.SwitchStyleEnums.Switch;
            this.btnForceStop.TabIndex = 991;
            this.btnForceStop.Tag = "1";
            this.btnForceStop.Text = "强制停止补油";
            this.btnForceStop.TrueColor = System.Drawing.Color.Lime;
            this.btnForceStop.Click += new System.EventHandler(this.btnForceStop_Click);
            // 
            // btnStartOil
            // 
            this.btnStartOil.BackColor = System.Drawing.Color.Silver;
            this.btnStartOil.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnStartOil.FalseColor = System.Drawing.Color.Silver;
            this.btnStartOil.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnStartOil.InputDriverName = "";
            this.btnStartOil.InputTagName = "";
            this.btnStartOil.Location = new System.Drawing.Point(24, 165);
            this.btnStartOil.Name = "btnStartOil";
            this.btnStartOil.OutputTagName = "";
            this.btnStartOil.Size = new System.Drawing.Size(159, 33);
            this.btnStartOil.SwitchType = RW.UI.Controls.SwitchStyleEnums.Switch;
            this.btnStartOil.TabIndex = 974;
            this.btnStartOil.Tag = "1";
            this.btnStartOil.Text = "开始机油耗测量";
            this.btnStartOil.TrueColor = System.Drawing.Color.Lime;
            this.btnStartOil.Click += new System.EventHandler(this.btnStartOil_Click);
            // 
            // lblOilBeginP
            // 
            this.lblOilBeginP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblOilBeginP.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblOilBeginP.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblOilBeginP.ForeColor = System.Drawing.Color.Black;
            this.lblOilBeginP.Location = new System.Drawing.Point(357, 252);
            this.lblOilBeginP.Name = "lblOilBeginP";
            this.lblOilBeginP.Size = new System.Drawing.Size(182, 28);
            this.lblOilBeginP.TabIndex = 976;
            this.lblOilBeginP.Tag = "";
            this.lblOilBeginP.Text = "0.0";
            this.lblOilBeginP.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnStopOil
            // 
            this.btnStopOil.BackColor = System.Drawing.Color.Silver;
            this.btnStopOil.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnStopOil.FalseColor = System.Drawing.Color.Silver;
            this.btnStopOil.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnStopOil.InputDriverName = "";
            this.btnStopOil.InputTagName = "";
            this.btnStopOil.Location = new System.Drawing.Point(222, 165);
            this.btnStopOil.Name = "btnStopOil";
            this.btnStopOil.OutputTagName = "";
            this.btnStopOil.Size = new System.Drawing.Size(188, 33);
            this.btnStopOil.SwitchType = RW.UI.Controls.SwitchStyleEnums.Switch;
            this.btnStopOil.TabIndex = 989;
            this.btnStopOil.Tag = "1";
            this.btnStopOil.Text = "结束测量（补油）";
            this.btnStopOil.TrueColor = System.Drawing.Color.Lime;
            this.btnStopOil.Click += new System.EventHandler(this.btnStopOil_Click);
            // 
            // lblBCWeight
            // 
            this.lblBCWeight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblBCWeight.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblBCWeight.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblBCWeight.ForeColor = System.Drawing.Color.Black;
            this.lblBCWeight.Location = new System.Drawing.Point(357, 429);
            this.lblBCWeight.Name = "lblBCWeight";
            this.lblBCWeight.Size = new System.Drawing.Size(182, 28);
            this.lblBCWeight.TabIndex = 984;
            this.lblBCWeight.Tag = "";
            this.lblBCWeight.Text = "0.0";
            this.lblBCWeight.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("宋体", 14F);
            this.label15.Location = new System.Drawing.Point(357, 228);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(211, 19);
            this.label15.TabIndex = 975;
            this.label15.Text = "机油耗压力初始值(kPa)";
            // 
            // lblOilBeginH
            // 
            this.lblOilBeginH.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblOilBeginH.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblOilBeginH.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblOilBeginH.ForeColor = System.Drawing.Color.Black;
            this.lblOilBeginH.Location = new System.Drawing.Point(357, 341);
            this.lblOilBeginH.Name = "lblOilBeginH";
            this.lblOilBeginH.Size = new System.Drawing.Size(182, 28);
            this.lblOilBeginH.TabIndex = 978;
            this.lblOilBeginH.Tag = "";
            this.lblOilBeginH.Text = "0.0";
            this.lblOilBeginH.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("宋体", 14F);
            this.label18.Location = new System.Drawing.Point(357, 316);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(201, 19);
            this.label18.TabIndex = 977;
            this.label18.Text = "机油耗液位初始值(mm)";
            // 
            // timerEngineOilConsumption
            // 
            this.timerEngineOilConsumption.Interval = 1000;
            this.timerEngineOilConsumption.Tick += new System.EventHandler(this.timerEngineOilConsumption_Tick);
            // 
            // ucFuelConsumptionMeasurement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.Controls.Add(this.lblOilTime);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.lblStopOilTime);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.lblfuelConsumptionMeter);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.lblCurrentWeight);
            this.Controls.Add(this.lblCurrentLiquidLevel);
            this.Controls.Add(this.label31);
            this.Controls.Add(this.lblMeasureReplenishment);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.lblCurrentPressure);
            this.Controls.Add(this.lblStartOilTime);
            this.Controls.Add(this.label29);
            this.Controls.Add(this.lblMeasureFuelConsumption);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.btnForceStop);
            this.Controls.Add(this.btnStartOil);
            this.Controls.Add(this.lblOilBeginP);
            this.Controls.Add(this.btnStopOil);
            this.Controls.Add(this.lblBCWeight);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.lblOilBeginH);
            this.Controls.Add(this.label18);
            this.Name = "ucFuelConsumptionMeasurement";
            this.Size = new System.Drawing.Size(1180, 621);
            this.Load += new System.EventHandler(this.ucFuelConsumptionMeasurement_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblOilTime;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label lblStopOilTime;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblfuelConsumptionMeter;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lblCurrentWeight;
        private System.Windows.Forms.Label lblCurrentLiquidLevel;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label lblMeasureReplenishment;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label lblCurrentPressure;
        private System.Windows.Forms.Label lblStartOilTime;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label lblMeasureFuelConsumption;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label17;
        private RW.UI.Controls.RButton btnForceStop;
        private RW.UI.Controls.RButton btnStartOil;
        private System.Windows.Forms.Label lblOilBeginP;
        private RW.UI.Controls.RButton btnStopOil;
        private System.Windows.Forms.Label lblBCWeight;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label lblOilBeginH;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Timer timerEngineOilConsumption;
    }
}
