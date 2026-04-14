using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MainUI.Modules;
using RW.Modules;
using System.Threading;
using RW.EventLog;
using MainUI.Config;
using System.Diagnostics;
using MainUI.Procedure;

namespace MainUI
{
    public partial class frmHardWare : Form
    {
        //// 配电柜
        //Dictionary<string, UCCalibration> dicAI1 = new Dictionary<string, UCCalibration>();
        //Dictionary<string, UCCalibration> dicAO1 = new Dictionary<string, UCCalibration>();
        Dictionary<string, UCCalibration> dicAI2 = new Dictionary<string, UCCalibration>();

        Dictionary<int, UCCalibration> dicAI1 = new Dictionary<int, UCCalibration>();
        Dictionary<int, UCCalibration> dicAO1 = new Dictionary<int, UCCalibration>();

        // 不进入plc的部分
        // 水
        Dictionary<string, UCCalibration> dicWater = new Dictionary<string, UCCalibration>();

        // 机油
        Dictionary<string, UCCalibration> dicEngine = new Dictionary<string, UCCalibration>();

        // 燃油
        Dictionary<string, UCCalibration> dicFuel = new Dictionary<string, UCCalibration>();

        public frmHardWare()
        {
            InitializeComponent();

        }
        /// <summary>
        /// 窗体加载
        /// </summary>
        private void frmHardWare_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void AIgrp_AIvalueGrpChanged(object sender, int index, double value)
        {
            //try
            //{
            //    if (dicAI.ContainsKey(index))
            //    {
            //        dicAI[index].Value = value;
            //    }
            //    throw new NotImplementedException();
            //}
            //catch (Exception ex)
            //{
            //    string err = "Error at frmHardWare AIgrp_ValueGroupChanged;" + ex.Message;
            //    Debug.WriteLine(err);
            //}
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            // 下标赋值
            //foreach (var item in Common.AIgrp.AIListData)
            //{
            //    if (dicAI1.ContainsKey(item.Key))
            //    {
            //        dicAI1[item.Key].Value = Common.AIgrp.AIListData[item.Key];
            //    }
            //}

            //foreach (var item in Common.AOgrp.AOListData)
            //{
            //    if (dicAO1.ContainsKey(item.Key))
            //    {
            //        dicAO1[item.Key].Value = Common.AOgrp.AOListData[item.Key];
            //    }
            //}

            foreach (var item in Common.AI2Grp.AIListData)
            {
                if (dicAI2.ContainsKey(item.Key))
                {
                    dicAI2[item.Key].Value = Common.AI2Grp.AIListData[item.Key];
                }
            }

            for (int i = 0; i < Common.AIgrp.AIList.Length; i++)
            {
                if (dicAI1.ContainsKey(i))
                {
                    dicAI1[i].Value = Common.AIgrp.AIList[i];
                }
            }

            for (int i = 0; i < Common.AOgrp.AOList.Length; i++)
            {
                if (dicAO1.ContainsKey(i))
                {
                    dicAO1[i].Value = Common.AOgrp.AOList[i];
                }
            }

            // 不进PLC的部分

            // 水系统
            foreach (var item in Common.waterGrp.NewDataValue)
            {
                if (dicWater.ContainsKey(item.Key))
                {
                    dicWater[item.Key].Value = Common.waterGrp.NewDataValue[item.Key];
                }
            }

            // 燃油系统
            foreach (var item in Common.fuelGrp.NewDataValue)
            {
                if (dicFuel.ContainsKey(item.Key))
                {
                    dicFuel[item.Key].Value = Common.fuelGrp.NewDataValue[item.Key];
                }
            }

            // 机油系统
            foreach (var item in Common.engineOilGrp.NewDataValue)
            {
                if (dicEngine.ContainsKey(item.Key))
                {
                    dicEngine[item.Key].Value = Common.engineOilGrp.NewDataValue[item.Key];
                }
            }
        }

        /// <summary>
        /// 初始化零点增益
        /// </summary>
        public void InitZeroGain()
        {
            try
            {
                string plc1Str = "";
                string plc2Str = "";
                if (!Common.opcStatus.NoError)
                {
                    plc1Str = "台位控制部分";
                }
                if (!Common.AI2Grp.NoError)
                {
                    plc2Str = "台发动机检测部分";
                }

                if (plc1Str != "" || plc2Str != "")
                {
                    throw new InvalidOperationException($"{plc1Str} {plc2Str} PLC连接出现异常！");
                }

                SetDic();
                InitZeroGainDic();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("硬件校准界面，初始化零点增益有误" + ex.ToString());
                //MessageBox.Show("硬件校准界面，初始化零点增益有误。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 初始化零点增益
        /// </summary>
        private void InitZeroGainDic()
        {
            try
            {
                foreach (var item in dicAI1)
                {
                    item.Value.InitData();
                    item.Value.Submit();
                }

                foreach (var item in dicAO1)
                {
                    item.Value.InitData();
                    item.Value.Submit();
                }

                foreach (var item in dicAI2)
                {
                    item.Value.InitData();
                    item.Value.Submit();
                }

                // 不进plc，需要特殊处理
                foreach (var item in dicWater)
                {
                    item.Value.InitData();
                    item.Value.Submit();

                    Common.waterGrp.AddOrUpdateCalibrationParams(item.Value.SectionName, new CalibrationParams { Zero = item.Value.ZeroValue, Gain = item.Value.GainValue });
                }

                foreach (var item in dicEngine)
                {
                    item.Value.InitData();
                    item.Value.Submit();

                    Common.engineOilGrp.AddOrUpdateCalibrationParams(item.Value.SectionName, new CalibrationParams { Zero = item.Value.ZeroValue, Gain = item.Value.GainValue });
                }

                foreach (var item in dicFuel)
                {
                    item.Value.InitData();
                    item.Value.Submit();

                    Common.fuelGrp.AddOrUpdateCalibrationParams(item.Value.SectionName, new CalibrationParams { Zero = item.Value.ZeroValue, Gain = item.Value.GainValue });
                }

                //var tempData = Common.engineParaGrp.AIListData.Where(d => d.Key.Contains("温度")).ToList();
                //// 使用代码生成
                //foreach (var item in tempData)
                //{
                //    UCCalibration cCalibration = new UCCalibration();
                //    cCalibration.Size = new Size(710, 37);
                //    cCalibration.Key = cCalibration.CustomName = item.Key;
                //    cCalibration.NameTypes = SectionNameTypes.Custom;

                //    cCalibration.InitData();
                //    cCalibration.Submit();

                //    dicAI2.Add(item.Key, cCalibration);
                //}
            }
            catch (Exception ex)
            {
                Var.MsgBoxWarn(this, "初始化零点增益控件出现异常" + ex.ToString());

            }
        }


        /// <summary>
        /// 绑定键值对
        /// </summary>
        private void SetDic()
        {
            try
            {
                //AI
                dicAI1.Clear();
                foreach (Control item in panel7.Controls)
                {
                    if (item is UCCalibration)
                    {
                        UCCalibration tmp = item as UCCalibration;
                        dicAI1.Add(tmp.Index, tmp);
                    }
                }

                //AO
                dicAO1.Clear();
                foreach (Control item in grp2.Controls)
                {
                    if (item is UCCalibration)
                    {
                        UCCalibration tmp = item as UCCalibration;
                        dicAO1.Add(tmp.Index, tmp);
                    }
                }

                // PLC2 （测量柜）
                dicAI2.Clear();
                foreach (Control item in panel4.Controls)
                {
                    if (item is UCCalibration)
                    {
                        UCCalibration tmp = item as UCCalibration;
                        dicAI2.Add(tmp.Key, tmp);
                    }
                }
                foreach (Control item in panel5.Controls)
                {
                    if (item is UCCalibration)
                    {
                        UCCalibration tmp = item as UCCalibration;
                        dicAI2.Add(tmp.Key, tmp);
                    }
                }

                // 不写到PLC的部分
                // 机油
                foreach (Control item in panelEngine.Controls)
                {
                    if (item is UCCalibration)
                    {
                        UCCalibration tmp = item as UCCalibration;
                        dicEngine.Add(tmp.Key, tmp);
                    }
                }

                // 燃油
                foreach (Control item in panelFuel.Controls)
                {
                    if (item is UCCalibration)
                    {
                        UCCalibration tmp = item as UCCalibration;
                        dicFuel.Add(tmp.Key, tmp);
                    }
                }

                // 水
                foreach (Control item in panelWater.Controls)
                {
                    if (item is UCCalibration)
                    {
                        UCCalibration tmp = item as UCCalibration;
                        dicWater.Add(tmp.Key, tmp);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "硬件较准页面，绑定点位出现异常" + ex.ToString());
            }

        }

        /// <summary>
        /// AI的零点增益
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucCalibration_2_Submited(object sender, SubmitArgs e)
        {
            try
            {
                // 检查是否需要跨线程调用
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action<object, SubmitArgs>(ucCalibration_2_Submited), sender, e);
                    return;
                }

                UCCalibration cuCur = sender as UCCalibration;
                cuCur.GainValue = e.Gain;
                cuCur.ZeroValue = e.Zero;

                Common.plcc2.SetAIGain(e.Key, e.Gain);
                Common.plcc2.SetAIZero(e.Key, e.Zero);


            }
            catch (Exception ex)
            {
                string err = "Error at frmHardWare ucCalibration_Submited;" + ex.Message;
                Debug.WriteLine(err);
            }
        }

        /// <summary>
        /// AI的零点增益
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucCalibration_1_Submited(object sender, SubmitArgs e)
        {
            try
            {
                // 检查是否需要跨线程调用
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action<object, SubmitArgs>(ucCalibration_1_Submited), sender, e);
                    return;
                }

                UCCalibration cuCur = sender as UCCalibration;
                cuCur.GainValue = e.Gain;
                cuCur.ZeroValue = e.Zero;

                Common.plcc.SetAIGain(e.Key, e.Gain);
                Common.plcc.SetAIZero(e.Key, e.Zero);

            }
            catch (Exception ex)
            {
                string err = "Error at frmHardWare ucCalibration_Submited;" + ex.Message;
                Debug.WriteLine(err);
            }
        }

        /// <summary>
        /// AO 的零点增益
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucCalibration24_Submited(object sender, SubmitArgs e)
        {
            try
            {
                // 检查是否需要跨线程调用
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action<object, SubmitArgs>(ucCalibration24_Submited), sender, e);
                    return;
                }

                UCCalibration cuCur = sender as UCCalibration;
                cuCur.GainValue = e.Gain;
                cuCur.ZeroValue = e.Zero;

                Common.plcc.SetAOGain(e.Key, e.Gain);
                Common.plcc.SetAOZero(e.Key, e.Zero);

            }
            catch (Exception ex)
            {
                string err = "Error at frmHardWare ucCalibration_Submited;" + ex.Message;
                Debug.WriteLine(err);
            }
        }

        /// <summary>
        /// 燃油系统 不进入PLC的零点增益
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucCalibrationFuelNoPlC_Submited(object sender, SubmitArgs e)
        {
            try
            {
                // 检查是否需要跨线程调用
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action<object, SubmitArgs>(ucCalibrationFuelNoPlC_Submited), sender, e);
                    return;
                }

                UCCalibration cuCur = sender as UCCalibration;
                cuCur.GainValue = e.Gain;
                cuCur.ZeroValue = e.Zero;

                Common.fuelGrp.AddOrUpdateCalibrationParams(cuCur.SectionName, new CalibrationParams { Zero = cuCur.ZeroValue, Gain = cuCur.GainValue });
            }
            catch (Exception ex)
            {
                string err = "Error at frmHardWare ucCalibration_Submited;" + ex.Message;
                Debug.WriteLine(err);
                Var.MsgBoxWarn(this, $"零点增益下发异常 {ex.ToString()}");
            }
        }

        /// <summary>
        /// 水系统 不进入PLC的零点增益
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucCalibrationWaterNoPlC_Submited(object sender, SubmitArgs e)
        {
            try
            {
                // 检查是否需要跨线程调用
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action<object, SubmitArgs>(ucCalibrationWaterNoPlC_Submited), sender, e);
                    return;
                }

                UCCalibration cuCur = sender as UCCalibration;
                cuCur.GainValue = e.Gain;
                cuCur.ZeroValue = e.Zero;

                Common.waterGrp.AddOrUpdateCalibrationParams(cuCur.SectionName, new CalibrationParams { Zero = cuCur.ZeroValue, Gain = cuCur.GainValue });
            }
            catch (Exception ex)
            {
                string err = "Error at frmHardWare ucCalibration_Submited;" + ex.Message;
                Debug.WriteLine(err);
                Var.MsgBoxWarn(this, $"零点增益下发异常 {ex.ToString()}");
            }
        }

        /// <summary>
        /// 机油系统 不进入PLC的零点增益
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucCalibrationEngineNoPlC_Submited(object sender, SubmitArgs e)
        {
            try
            {
                // 检查是否需要跨线程调用
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action<object, SubmitArgs>(ucCalibrationEngineNoPlC_Submited), sender, e);
                    return;
                }

                UCCalibration cuCur = sender as UCCalibration;
                cuCur.GainValue = e.Gain;
                cuCur.ZeroValue = e.Zero;

                Common.engineOilGrp.AddOrUpdateCalibrationParams(cuCur.SectionName, new CalibrationParams { Zero = cuCur.ZeroValue, Gain = cuCur.GainValue });
            }
            catch (Exception ex)
            {
                string err = "Error at frmHardWare ucCalibration_Submited;" + ex.Message;
                Debug.WriteLine(err);
                Var.MsgBoxWarn(this, $"零点增益下发异常 {ex.ToString()}");
            }
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
