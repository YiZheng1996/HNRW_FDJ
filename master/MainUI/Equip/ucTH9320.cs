using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using System.Threading;
using RW.UI.Controls.Report;
using MainUI.Equip;
using MainUI;

namespace RW.Testbed.UI.Equip
{
    public partial class ucTH9320 : UserControl
    {
        public ucTH9320()
        {
        

            InitializeComponent();
        }

     
        public RWReport Rpt { get; set; }

        private void ucJYNYTest_Load(object sender, EventArgs e)
        {
            rbnJYDZ.Checked = true;

            lblDJS_JY.Visible = false;
            lblDJS_NY.Visible = false;
        }

        public void InitPort()
        {
            if (Var.SysConfig.JYPortName != null)
                this.serialPortTH9320.PortName = Var.SysConfig.JYPortName;
        }
     
      

        private void btnJYStart_Click(object sender, EventArgs e)
        {
            try
            {
                string newLine = Environment.NewLine + Environment.NewLine;
                decimal jydzVol = nudJYDZVol.Value;
                decimal jydzTime = nudJYDZtime.Value;
                decimal jydzLow = nudJYDZlow.Value;

                string tips = "请先确认绝缘电阻试验参数：" + newLine;
                tips += "绝缘电压(DC)(V)：" + jydzVol.ToString() + newLine;
                tips += "绝缘测试时间(s)：" + jydzTime.ToString() + newLine;
                tips += "绝缘电阻下限(MΩ）：" + jydzLow.ToString() + newLine;


                if (MessageBox.Show(tips, "提示：", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                    return;

                TH9320.TH9320_JueYuanTest(serialPortTH9320, jydzVol.ToString(), jydzTime, jydzLow);


                lblJYDZrst.Text = "";
                lblJYDZState.Text = "正在测试…";
                lblJYDZState.BackColor = Color.Tomato;

                btnJYStart.Enabled = false;
                rbnJYNY.Enabled = false;
                grpJYpara.Enabled = false;

                jySecDJS = Convert.ToInt32(nudJYDZtime.Value.ToString()) + 1;
               timerJYDZ.Enabled = true;

               
            }
            catch (Exception ex)
            {
                string err = "开始绝缘电阻测试有误，" + ex.Message;
                MessageBox.Show(err, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Var.LogInfo(err);
            }

        }

    

       

        private void ControlEnable(bool isEnable)
        {
            rbnJYNY.Enabled = isEnable;
            rbnJYDZ.Enabled = isEnable;
            btnJYStart.Enabled = isEnable;
            btnNYStart.Enabled = isEnable;

            grpJYpara.Enabled = isEnable;
            grpNYpara.Enabled = isEnable;
        }

     

        void DIGroup_IsTestingChanged(object sender, bool value)
        {
            if (value)
            {
                if (rbnJYDZ.Checked)
                {
                    lblJYDZState.Text = "正在测试…";
                }
                else
                {
                    lblNYstate.Text = "正在测试…";
                }
            }
            else
            {
                ControlEnable(true);

                if (rbnJYDZ.Checked)
                {
                    lblJYDZState.Text = "测试完成";
                }
                else
                {
                    lblNYstate.Text = "测试完成";
                }
            }
        }



        private void btnJYStop_Click(object sender, EventArgs e)
        {
            try
            {

                timerJYDZ.Enabled = false;

                lblDJS_JY.Text = "倒计时：00:00:00";
                lblDJS_JY.Visible = false;
                lblJYDZState.BackColor = SystemColors.Control;

                ControlEnable(true);

                string rst = TH9320.GetJYDZRst(serialPortTH9320);

                if (rst.ToUpper() == "PASS")
                    lblJYDZrst.Text = "合格";
                else
                    lblJYDZrst.Text = "不合格";

                txtRvalue.Text = TH9320.CurRValue;

                TH9320.TH9320_TestStop(serialPortTH9320);

                lblJYDZState.Text = "停止测试";
            }
            catch (Exception ex)
            {
                string err = "停止绝缘电阻测试有误，" + ex.Message;
                MessageBox.Show(err, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Var.LogInfo(err);
            }
            //timerJY.Stop();
        }

      
        private void btnNYStart_Click(object sender, EventArgs e)
        {
            try
            {
                string newLine = Environment.NewLine + Environment.NewLine;
                decimal jynyVol = nudNYVol.Value;
                decimal jynyTime = nudNYtime.Value;
                decimal jynyUP = nudNYLDLUP.Value;

                string tips = "请先确认绝缘耐压试验参数：" + newLine;
                tips += "绝缘电压(AC)(V)：" + jynyVol.ToString() + newLine;
                tips += "绝缘测试时间(s)：" + jynyTime.ToString() + newLine;
                tips += "绝缘电阻下限(mA)：" + jynyUP.ToString() + newLine;


                if (MessageBox.Show(tips, "提示：", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                    return;

                TH9320.TH9320_NaiYaTest(serialPortTH9320, jynyVol.ToString(), jynyTime.ToString(), jynyUP.ToString());

                lblNYrst.Text = "";
                lblNYstate.Text = "开始测试…";

                lblNYstate.BackColor = Color.Tomato;

                btnNYStart.Enabled = false;
                rbnJYDZ.Enabled = false;
                grpNYpara.Enabled = false;

                nySecDJS = Convert.ToInt32(nudNYtime.Value.ToString()) + 1;
                timerNY.Enabled = true;

            }
            catch (Exception ex)
            {
                string err = "开始耐压测试有误，" + ex.Message;
                MessageBox.Show(err, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Var.LogInfo(err);
            }
        }

        private void btnNYstop_Click(object sender, EventArgs e)
        {
            try
            {
                ControlEnable(true);

                lblNYstate.BackColor = SystemColors.Control;

                timerNY.Enabled = false;
                lblDJS_NY.Text = "";
                lblDJS_NY.Visible = false;

                string rst = TH9320.GetJYDZRst(serialPortTH9320);

                if (rst.ToUpper() == "PASS")
                    lblNYrst.Text = "合格";
                else
                    lblNYrst.Text = "不合格";

                txtIvalue.Text = TH9320.CurIValue;

                TH9320.TH9320_TestStop(serialPortTH9320);
                lblNYstate.Text = "停止测试";
            }
            catch (Exception ex)
            {
                string err = "停止耐压测试有误，" + ex.Message;
                MessageBox.Show(err, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Var.LogInfo(err);
            }
        }

       

        private void rbnJYDZ_CheckedChanged(object sender, EventArgs e)
        {
            if (rbnJYDZ.Checked)
            {
                grpJYDZ.Enabled = true;
                grpNY.Enabled = false;
            }
            else if(rbnJYNY.Checked)
            {
                grpNY.Enabled = true;
                grpJYDZ.Enabled = false;
            }
        }

        private void btnSaveRst_Click(object sender, EventArgs e)
        {
            try
            {
                Rpt.Write("jyVolt", nudJYDZVol.Value.ToString());
                Rpt.Write("jyTime", nudJYDZtime.Value.ToString());
                Rpt.Write("jyLow", nudJYDZlow.Value.ToString());
                Rpt.Write("Rvalue", txtRvalue.Text);
                Rpt.Write("jyRst", lblJYDZrst.Text);

                Rpt.Write("nyVolt", nudNYVol.Value.ToString());
                Rpt.Write("nyTime", nudNYtime.Text);
                Rpt.Write("nyUp", nudNYLDLUP.Text);
                Rpt.Write("Ivalue", txtIvalue.Text);
                Rpt.Write("nyRst", lblNYrst.Text);

                MessageBox.Show("绝缘测试导出结果到Excel完成。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                string err = "绝缘测试导出结果到Excel有误，" + ex.Message;
                MessageBox.Show(err, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Var.LogInfo(err);
            }
        }

        /// <summary>
        /// 绝缘电阻 倒计时 秒
        /// </summary>
        int jySecDJS = 30;
       
        private void timerJYDZ_Tick(object sender, EventArgs e)
        {
            if (lblDJS_JY.Visible == false)
                lblDJS_JY.Visible = true;
            jySecDJS--;
            string strSec = GetDigitalTimeString(jySecDJS);
            lblDJS_JY.Text = "倒计时：" + strSec;
            Application.DoEvents();
            if (jySecDJS <= 0)
            {
                timerJYDZ.Enabled = false;
                
                Thread.Sleep(2000);
                btnJYStop_Click(null, null);
            }


        }

        /// <summary>
        /// 耐压倒计时 秒
        /// </summary>
        int nySecDJS = 30;
        private void timerNY_Tick(object sender, EventArgs e)
        {
            if (lblDJS_NY.Visible == false)
            lblDJS_NY.Visible = true;
            nySecDJS--;
            string strSec = GetDigitalTimeString(nySecDJS);
            lblDJS_NY.Text = "倒计时：" + strSec;

            Application.DoEvents();
            if (nySecDJS <= 0)
            {
                timerNY.Enabled = false;
             
                Thread.Sleep(1000);
                btnNYstop_Click(null, null);
            }
        }


        public static string GetDigitalTimeString(int seconds)
        {
            int total = seconds;
            int digitSec, digitMin, digitHour, digitDay;

            digitSec = total % 60;
            total /= 60; //To Minutes
            digitMin = total % 60;
            total /= 60; //To Hours
            digitHour = total % 24;
            digitDay = total / 24;

            if (digitDay == 0)
            {
                return String.Format("{0:D2}:{1:D2}:{2:D2}", digitHour, digitMin, digitSec);
            }
            else
            {
                return String.Format("{0:D2}:{1:D2}:{2:D2}:{3:D2} ", digitDay, digitHour, digitMin, digitSec);
            }
        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }


    }
}
