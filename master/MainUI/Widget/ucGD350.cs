using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RW.Modules;

namespace MainUI.Widget
{
    public partial class ucGD350 : UserControl
    {
        public ucGD350()
        {
            InitializeComponent();
            timer1.Start();
            
        }

        private void ucGD350_Load(object sender, EventArgs e)
        {
            if (this.DesignMode)
                return;
            rbnCW.Checked = true;
            timer1.Enabled = true;
        }

        private void Init()
        {
          
        }

      

        private Sunny.UI.UILightState GetOnOff(int status)
        {
            if (status == 0)
                return Sunny.UI.UILightState.On;
            else
                return Sunny.UI.UILightState.Off;
        }

        Color clrGreen = Color.Green;
        Color clrControl = SystemColors.Control;

        private void timer1_Tick(object sender, EventArgs e)
        {
            ltReady.State = GetOnOff(Common.gd350_1.Ready);

            //int faultCode = Common.gd350_1.FaultCode;
            //lblFault.Visible = faultCode > 0;
            //lblFault.Text = $"故障代码：{faultCode},请查看变频器手册。";

            var status = Common.gd350_1.RunStatusAI;

            if (Common.gd350_1.LineVoltage > 500 && Common.gd350_1.Ready == 0)
            {
                
            }
            //var status = Common.gd350_1.RunStatusAI; 
            //var cw = status == 1;
            //var ccw = status == 2;
            //var running = cw || ccw;
            //var fault = status == 4;

            //ltCW.State = GetOnOff(cw);
            //ltCCW.State = GetOnOff(ccw);
            //ltRun.State = GetOnOff(running);
            //if(running)
            //{
            //    btnRun.BackColor = clrGreen;
            //    btnRun.Enabled = false;
            //    rbnCW.Enabled = false;
            //    rbnCCW.Enabled = false;
            //}
            //else
            //{
            //    btnRun.BackColor = clrControl;
            //    btnRun.Enabled = true;
            //    rbnCW.Enabled = true;
            //    rbnCCW.Enabled = true;
            //}

            //ltFault.State = GetOnOff(fault);

            //double spd = Math.Abs(Common.gd350_1.Speed);
            //lblSpeed.Text = spd.ToString();

            switch (Common.gd350_1.RunStatusAI)
            {
                case 1:
                    ltCW.State = Sunny.UI.UILightState.On;
                    ltCCW.State = Sunny.UI.UILightState.Off;
                    ltFault.State = Sunny.UI.UILightState.Off;
                    break;
                case 2:
                    ltCCW.State = Sunny.UI.UILightState.On;
                    ltCW.State = Sunny.UI.UILightState.Off;
                    ltFault.State = Sunny.UI.UILightState.Off;
                    break;
                case 3:
                    ltFault.State = Sunny.UI.UILightState.On;
                    ltCCW.State = Sunny.UI.UILightState.Off;
                    ltCW.State = Sunny.UI.UILightState.Off;
                    break;
                default:
                    break;
            }
            if(ltCW.State == Sunny.UI.UILightState.On || ltCCW.State == Sunny.UI.UILightState.On)
            {
                ltRun.State = Sunny.UI.UILightState.On;

            }else
            {
                ltRun.State = Sunny.UI.UILightState.Off;
            }
            if(Common.gd350_1.SetRunStatus == 0)
            {
                
            }
            double freq = Math.Abs(Common.gd350_1.FrequencyAI);
            lblFreq.Text = freq.ToString();
            lblSpeed.Text = (freq * 3).ToString();
            lblVoltage.Text = Common.gd350_1.OutputVoltage.ToString();
            lblCurrent.Text = Common.gd350_1.OutputCurrent.ToString();
            lblOutputPower.Text = Common.gd350_1.OutputPower.ToString();

        }

        /// <summary>
        /// 1：正转运行; 2：反转运行; 5：停机; 6：紧急停机; 7：故障复位;
        /// </summary>
        int isCW = 1;
        private void rbnCW_CheckedChanged(object sender, EventArgs e)
        {
            if(rbnCW.Checked)
            {
                isCW = 1;
            }
            if(rbnCCW.Checked)
            {
                isCW = 2;
            }

        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            string fx = isCW == 1 ? "正转" : "反转";
            string txt = $"确定要给定{fx}运行吗？";

            bool ok = Var.MsgBoxYesNo(this, txt);
            if (ok == false)
            {
                return;
            }

            try
            {
                Common.gd350_1.RunStatusAI = isCW;

            }
            catch(Exception ex)
            {
                string err = "启动变频器有误；原因:" + ex.Message;
                MessageBox.Show(err, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            bool ok = Var.MsgBoxYesNo(this, "确定要停止运行吗？");
            if(ok==false)
            {

                return;
            }

            Stop();
        }

        private void Stop()
        {
            try
            {
                Common.gd350_1.RunStatusAI = 3;
                Common.gd350_1.SetRunStatus = 0;
                
            }
            catch (Exception ex)
            {
                string err = "停止变频器有误；原因:" + ex.Message;
                MessageBox.Show(err, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void btnVolt_Click(object sender, EventArgs e)
        {

        }

        private void btnSpeed_Click(object sender, EventArgs e)
        {

        }

        private void btnFreq_Click(object sender, EventArgs e)
        {
            int freq = (this.numFreq.Value.ToInt());
            Common.gd350_1.SetFrequency = freq;
        }

      
    }
}
