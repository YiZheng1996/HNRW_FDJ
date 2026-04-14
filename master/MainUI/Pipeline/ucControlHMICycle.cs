using MainUI.Modules;
using MainUI.Widget;
using RW;
using RW.UI.Controls;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MainUI.Config.Modules;
using System.IO;
using System.Threading;
using MainUI.Config;
using RW.Modules;
using static MainUI.Modules.EventArgsModel;

namespace MainUI
{
    public partial class ucControlHMICycle : UserControl
    {
        SysConstantParas SysConstantParasConfig = new SysConstantParas();

        // 预热水箱温度
        int WaterTempValue = 0;

        /// <summary>
        /// 开/关到位，泵控制 字典
        /// </summary>
        Dictionary<string, List<RButton>> dicBtn = new Dictionary<string, List<RButton>>();
        /// <summary>
        /// 故障灯状态
        /// </summary>
        Dictionary<string, List<UILedBulb>> dicLight = new Dictionary<string, List<UILedBulb>>();
        /// <summary>
        /// 数值状态
        /// </summary>
        Dictionary<string, List<Label>> dicLabel = new Dictionary<string, List<Label>>();

        /// <summary>
        /// 通用测试列表
        /// </summary>
        public Dictionary<string, List<ContronlPoint>> _contronlMap = new Dictionary<string, List<ContronlPoint>>();

        /// <summary>
        /// 是否正在测试
        /// </summary>
        public Dictionary<string, bool> _deviceCommandMap = new Dictionary<string, bool>();

        /// <summary>
        /// 一键合闸开始点位
        /// </summary>
        Dictionary<string, RButton> dicTestBtn = new Dictionary<string, RButton>();

        public ucControlHMICycle()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            // 绑定数值
            EachValueControl();

            Common.DIgrp.KeyValueChange += DIgrp_KeyValueChange;
            Common.AOgrp.KeyValueChange += AOgrp_KeyValueChange;
            Common.waterGrp.KeyValueChange += WaterGrp_KeyValueChange;
            Common.engineOilGrp.KeyValueChange += EngineOilGrp_KeyValueChange;
            Common.fuelGrp.KeyValueChange += FuelGrp_KeyValueChange;
            Common.PipelineFaultGrp.KeyValueChange += PipelineFaultGrp_KeyValueChange;
            //Common.ExChangeGrp.KeyValueChange += ExChangeGrp_KeyValueChange1;

            timer1.Enabled = true;
            timer1.Start();

            // 目标温度数值
            var wValue = Common.ExChangeGrp.GetDouble("预热水箱加热温度设定");
            if (wValue != 0)
            {
                this.nudSetTemp.Value = wValue;
            }
            else
            {
                Common.ExChangeGrp.SetDouble("预热水箱加热温度设定", 60);
                this.nudSetTemp.Value = 60;
            }

        }

 

        private void ExChangeGrp_KeyValueChange1(object sender, DIValueChangedEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<object, DIValueChangedEventArgs>(ExChangeGrp_KeyValueChange1), sender, e);
                return;
            }

            if (e.Key == "预热水箱加水")
            {
                this.rButton42.Switch = e.Value;
                //this.rButton41.Switch = !e.Value;
            }
            else if (e.Key == "预热水箱加热")
            {
                this.rButton44.Switch = e.Value;
                //this.rButton43.Switch = !e.Value;
            }
            else if (e.Key == "高温水预热循环")
            {
                this.rButton78.Switch = e.Value;
                //this.rButton77.Switch = !e.Value;
            }
            else if (e.Key == "高温水中冷水回抽")
            {
                this.rButton76.Switch = e.Value;
                //this.rButton75.Switch = !e.Value;
            }
            // 机油
            else if (e.Key == "预供机油循环")
            {
                this.rButton52.Switch = e.Value;
                //this.rButton51.Switch = !e.Value;
            }
            else if (e.Key == "机油加热处理循环")
            {
                this.rButton53.Switch = e.Value;
                //this.rButton54.Switch = !e.Value;
            }
            else if (e.Key == "油底壳加油")
            {
                this.rButton24.Switch = e.Value;
                //this.rButton19.Switch = !e.Value;
            }
            else if (e.Key == "油底壳抽油")
            {
                this.rButton56.Switch = e.Value;
                //this.rButton55.Switch = !e.Value;
            }
            // 回抽
            else if (e.Key == "机油回抽")
            {
                this.rButton80.Switch = e.Value;
                //this.rButton79.Switch = !e.Value;
            }
            // 燃油
            else if (e.Key == "燃油循环")
            {
                this.rButton58.Switch = e.Value;
                //this.rButton57.Switch = !e.Value;
            }
            else if (e.Key == "燃油耗测量")
            {
                this.rButton37.Switch = e.Value;
                //this.rButton25.Switch = !e.Value;
            }
            else if (e.Key == "燃油箱回油冷却")
            {
                this.rButton64.Switch = e.Value;
                //this.rButton63.Switch = !e.Value;
            }
            // 选择
            else if (e.Key == "燃油循环油泵选择")
            {
                this.rButton28.Switch = !e.Value;
                this.rButton40.Switch = e.Value;
            }
            else if (e.Key == "燃油耗测量油泵选择")
            {
                this.rButton66.Switch = !e.Value;
                this.rButton65.Switch = e.Value;
            }
            else if (e.Key == "油底壳抽油选择油箱")
            {
                this.rButton68.Switch = !e.Value;
                this.rButton67.Switch = e.Value;
            }
        }

        /// <summary>
        /// 故障灯的状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PipelineFaultGrp_KeyValueChange(object sender, DIValueChangedEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<object, DIValueChangedEventArgs>(PipelineFaultGrp_KeyValueChange), sender, e);
                return;
            }

            if (dicLight.ContainsKey(e.Key))
            {
                if (e.Value)
                {
                    // todo 为 true时，需要记录到故障中
                }

                var list = dicLight[e.Key];
                foreach (var item in list)
                {
                    item.Visible = e.Value;
                }
            }
        }

        /// <summary>
        /// 机油数据值改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EngineOilGrp_KeyValueChange(object sender, DoubleValueChangedEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<object, DoubleValueChangedEventArgs>(EngineOilGrp_KeyValueChange), sender, e);
                return;
            }

            if (dicLabel.ContainsKey(e.Key))
            {
                var list = dicLabel[e.Key];
                foreach (var item in list)
                {
                    item.Text = e.Value.ToString();
                }
            }
        }

        /// <summary>
        /// 燃油数值调整
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FuelGrp_KeyValueChange(object sender, DoubleValueChangedEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<object, DoubleValueChangedEventArgs>(WaterGrp_KeyValueChange), sender, e);
                return;
            }

            if (dicLabel.ContainsKey(e.Key))
            {
                var list = dicLabel[e.Key];
                foreach (var item in list)
                {
                    item.Text = e.Value.ToString();
                }
            }
        }

        /// <summary>
        /// 水数据值改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WaterGrp_KeyValueChange(object sender, DoubleValueChangedEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<object, DoubleValueChangedEventArgs>(WaterGrp_KeyValueChange), sender, e);
                return;
            }

            if (dicLabel.ContainsKey(e.Key))
            {
                var list = dicLabel[e.Key];
                foreach (var item in list)
                {
                    item.Text = e.Value.ToString();
                }
            }
        }

        /// <summary>
        /// AO值改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AOgrp_KeyValueChange(object sender, DoubleValueChangedEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<object, DoubleValueChangedEventArgs>(AOgrp_KeyValueChange), sender, e);
                return;
            }

            if (dicLabel.ContainsKey(e.Key))
            {
                var list = dicLabel[e.Key];
                foreach (var item in list)
                {
                    item.Text = e.Value.ToString();
                }
            }
        }

        /// <summary>
        /// 值改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DIgrp_KeyValueChange(object sender, DIValueChangedEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<object, DIValueChangedEventArgs>(DIgrp_KeyValueChange), sender, e);
                return;
            }
         
            if (dicBtn.ContainsKey(e.Key))
            {
                foreach (var item in dicBtn[e.Key])
                {
                    item.Switch = e.Value;
                }
            }
        }

        /// <summary>
        /// 添加界面所有灯和按钮
        /// </summary>
        /// <param name="con"></param>
        private void EachControl(Control con)
        {
            foreach (Control item in con.Controls)
            {
                EachControl(item);
            }
            if (con is UILedBulb)
            {
                //添加灯
                UILedBulb light = con as UILedBulb;
                if (light.Tag != null && light.Tag.ToString() != string.Empty)
                {
                    List<UILedBulb> UILightList;
                    dicLight.TryGetValue(light.Tag.ToString(), out UILightList);
                    if (UILightList == null)
                    {
                        UILightList = new List<UILedBulb>() { };
                        UILightList.Add(light);
                        dicLight.Add(light.Tag.ToString(), UILightList);
                    }
                    else
                    {
                        dicLight[light.Tag.ToString()].Add(light);
                    }
                }
            }
            if (con is RButton)
            {
                //添加按钮
                RButton btn = con as RButton;
                if (btn.InputTagName != null && btn.InputTagName.ToString() != string.Empty)
                {
                    List<RButton> ButtonList;
                    dicBtn.TryGetValue(btn.InputTagName.ToString(), out ButtonList);
                    if (ButtonList == null)
                    {
                        ButtonList = new List<RButton>() { };
                        ButtonList.Add(btn);
                        dicBtn.Add(btn.InputTagName.ToString(), ButtonList);
                    }
                    else
                    {
                        dicBtn[btn.InputTagName.ToString()].Add(btn);
                    }

                }
            }
        }

        /// <summary>
        /// 添加数值绑定
        /// </summary>
        /// <param name="con"></param>
        private void EachValueControl()
        {
            // 清空字典以确保重新初始化时不会重复
            dicLabel.Clear();

            // 每个键对应一个包含单个Label的列表
            dicLabel.Add("预热水箱液位检测", new List<Label> { lblWaterFlow });
            dicLabel.Add("预热水箱温度检测-T12", new List<Label> { lblWaterTemp });
            dicLabel.Add("T2高温水进机温度", new List<Label> { lblWaterHInTemp });
            dicLabel.Add("T1高温水出机温度", new List<Label> { lblWaterHOutTemp });
            dicLabel.Add("柴油箱液位检测-L29", new List<Label> { lblFlueFolw });
            dicLabel.Add("T31燃油泵进口油温", new List<Label> { lblFeulHInTemp });

            dicLabel.Add("P21主油道进口油压", new List<Label> { lblEngineP });
            dicLabel.Add("机油箱温度检测-T23", new List<Label> { lblEngineT });
            dicLabel.Add("机油箱液位检测-L18", new List<Label> { lblEngineFlow});
            dicLabel.Add("待处理机油箱温度检测-T24", new List<Label> { lblEngineDT });
            dicLabel.Add("待处理机油箱液位检测-L19", new List<Label> { lblEngineDF });
        }

        ///// <summary>
        ///// 添加界面的一键控制按钮
        ///// </summary>
        ///// <param name="con"></param>
        //private void EachControlTest(Control con)
        //{
        //    foreach (Control item in con.Controls)
        //    {
        //        EachControlTest(item);
        //    }
        //    if (con is RButton)
        //    {
        //        //添加按钮
        //        RButton btn = con as RButton;
        //        if (btn.Tag != null && btn.Tag.ToString() != "-")
        //        {
        //            RButton ButtonList;
        //            dicTestBtn.TryGetValue($"{btn.Tag.ToString()}_{btn.Text}", out ButtonList);
        //            if (ButtonList == null)
        //            {
        //                ButtonList = btn;
        //            }
        //        }
        //    }
        //}

        /// <summary>
        /// 通用点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sw_Valve_Click(object sender, EventArgs e)
        {
            RButton sw = sender as RButton;
            if (sw.Tag == null)
            {
                Var.MsgBoxWarn(this, "程序异常，未绑定Tag值。");
                return;
            }
            if (string.IsNullOrEmpty(sw.Tag.ToString()))
            {
                Var.MsgBoxWarn(this, "程序异常，未绑定Tag值。");
                return;
            }
            if (string.IsNullOrEmpty(sw.OutputTagName.ToString()))
            {
                Var.MsgBoxWarn(this, "程序异常，未绑定OutputTagName值。");
                return;
            }
            // 如果已经打开/关闭 则过滤
            if (sw.Switch == true)
            {
                return;
            }

            string str = sw.Text.Contains("开") ? "打开" : "关闭";
            string th = sw.OutputTagName.ToString().Replace("控制", "").Replace("合闸", "");
            string strMessage = $"是否要{str}{th}?";
            bool result = Var.MsgBoxYesNo(this, strMessage);
            if (result == false)
            {
                return;
            }
            else
            {
                try
                {
                    Common.DOgrp[sw.OutputTagName.ToString()] = Convert.ToBoolean(sw.Tag.ToInt()); //!Common.DOgrp[sw.Tag.ToString()];
                }
                catch (Exception ex)
                {

                    throw new Exception($"{th}+{str}失败！原因：" + ex.Message);
                }
            }
        }

        /// <summary>
        /// 模式切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRYXHSelect_Click(object sender, EventArgs e)
        {
            RButton btn = sender as RButton;
            if (btn.Switch) return;

            // 如果燃油循环正打开
            if (btn.OutputTagName == "燃油循环油泵选择")
            {
                if (rButton58.Switch)
                {
                    var result = Var.MsgBoxYesNo(this, "确定要切换燃油输送泵吗？");
                    if (!result) return;
                }
                Common.ExChangeGrp.SetBool("燃油循环油泵选择", (btn.Tag.ToString() == "1"));
            }
            // 油耗打开
            else if (btn.OutputTagName == "燃油耗测量油泵选择")
            {
                if (rButton37.Switch)
                {
                    var result = Var.MsgBoxYesNo(this, "确定要切换燃油输送泵吗？");
                    if (!result) return;
                }
                Common.ExChangeGrp.SetBool("燃油耗测量油泵选择", (btn.Tag.ToString() == "1"));
            }
            else
            {
                // 油底壳抽油
                if (rButton56.Switch)
                {
                    Var.MsgBoxWarn(this, "正在进行油底壳抽油，不能进行切换。");
                    return;
                }
                Common.ExChangeGrp.SetBool("油底壳抽油选择油箱", (btn.Tag.ToString() == "1"));
            }
        }

        /// <summary>
        /// 通用的开始试验逻辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStart_Click(object sender, EventArgs e)
        {
            RButton btn = sender as RButton;
            if (btn.Tag == null)
            {
                Var.MsgBoxWarn(this, "程序异常，未绑定Tag值。");
                return;
            }
            if (string.IsNullOrEmpty(btn.Tag.ToString()))
            {
                Var.MsgBoxWarn(this, "程序异常，未绑定Tag值。");
                return;
            }
            if (string.IsNullOrEmpty(btn.OutputTagName.ToString()))
            {
                Var.MsgBoxWarn(this, "程序异常，未绑定OutputTagName值。");
                return;
            }
            var Pressure1 = Common.AIgrp["厂房进气压力检测1"];
            var Pressure2 = Common.AIgrp["厂房进气压力检测2"];
            if (Pressure1 < 200 && Pressure2 < 200)
            {
                Var.MsgBoxWarn(this, "厂房压力低，不能进行一键操作。");
                return;
            }

            // 如果已经打开/关闭 则过滤
            if (btn.Switch == true)
            {
                return;
            }

            var setResult = btn.Tag.ToString() == "1";

            // 水系统判定
            if (setResult && btn.OutputTagName == "预热水箱加水")
            {
                if (Convert.ToDouble(this.lblWaterFlow.Text) >= 1500)
                {
                    Var.MsgBoxWarn(this, "预热水箱加水开启条件：预热水箱液位<1500mm。");
                    return;
                }
            }
            else if (setResult && btn.OutputTagName == "预热水箱加热")
            {
                if (Convert.ToDouble(this.lblWaterFlow.Text) <= 600)
                {
                    Var.MsgBoxWarn(this, "预热水箱加热开启条件：预热水箱液位>600mm。");
                    return;
                }
            }
            else if (setResult && btn.OutputTagName == "高温水预热循环")
            {
                StringBuilder errorMessages = new StringBuilder();
                int msgIndex = 0;

                if (Convert.ToDouble(this.lblWaterTemp.Text) <= 60)
                {
                    errorMessages.AppendLine($"不满足开启条件{++msgIndex}：预热水箱温度>60℃。");
                }
                if (Convert.ToDouble(this.lblWaterFlow.Text) <= 600)
                {
                    errorMessages.AppendLine($"不满足开启条件{++msgIndex}.预热水箱液位>600mm。");
                }
                if (msgIndex > 0) 
                {
                    Var.MsgBoxWarn(this, errorMessages.ToString());
                    return;
                }
            }
            else if (setResult && btn.OutputTagName == "高温水中冷水回抽")
            {
                if (Convert.ToDouble(this.lblWaterFlow.Text) >= 1500)
                {
                    Var.MsgBoxWarn(this, "高温水/中冷水回抽开启条件：预热水箱液位<1500mm。");
                    return;
                }
            }
            // 燃油系统
            else if (setResult && btn.OutputTagName == "燃油循环")
            {
                if (Convert.ToDouble(this.lblFlueFolw.Text) <= 300)
                {
                    Var.MsgBoxWarn(this, "燃油循环开启条件：燃油箱液位＞300mm。");
                    return;
                }
            }
            // 机油系统
            else if (setResult && btn.OutputTagName == "机油箱加油")
            {
                if (Convert.ToDouble(this.lblEngineFlow.Text) >= 1500)
                {
                    Var.MsgBoxWarn(this, "机油箱加油开启条件：机油箱液位＜1500mm。");
                    return;
                }
            }
            else if (setResult && btn.OutputTagName == "机油加热处理循环")
            {
                StringBuilder errorMessages = new StringBuilder();
                int msgIndex = 0;

                if (Convert.ToDouble(this.lblEngineFlow.Text) <= 600)
                {
                    errorMessages.AppendLine($"不满足开启条件{++msgIndex}：机油箱液位>600mm。");
                }
                if (Convert.ToDouble(this.lblEngineT.Text) >= 30)
                {
                    errorMessages.AppendLine($"不满足开启条件{++msgIndex}：机油箱温度<30℃。");
                }
                if (msgIndex > 0)
                {
                    Var.MsgBoxWarn(this, errorMessages.ToString());
                    return;
                }
            }
            else if (setResult && btn.OutputTagName == "油底壳加油")
            {
                if (Convert.ToDouble(this.lblEngineFlow.Text) <= 600)
                {
                    Var.MsgBoxWarn(this, "油底壳加油开启条件：机油箱液位>600mm。");
                    return;
                }
            }
            else if (setResult && btn.OutputTagName == "油底壳抽油")
            {
                if (Convert.ToDouble(this.lblEngineFlow.Text) >= 1500)
                {
                    Var.MsgBoxWarn(this, "油底壳抽油开启条件：机油箱液位<1500mm。");
                    return;
                }
            }
            else if (setResult && btn.OutputTagName == "机油回抽")
            {
                if (Convert.ToDouble(this.lblEngineFlow.Text) >= 1500)
                {
                    Var.MsgBoxWarn(this, "机油回抽开启条件：机油箱液位<1500mm。");
                    return;
                }
            }

            string th = btn.OutputTagName.ToString().Replace("控制", "").Replace("合闸", "");
            string strMessage = $"是否要{ btn.Text }{th}?";
            bool mesResult = Var.MsgBoxYesNo(this, strMessage);
            if (mesResult == false)
            {
                return;
            }
            else 
            {
                try
                {
                    if (btn.OutputTagName == "预热水箱加热")
                    {
                        Common.ExChangeGrp.SetDouble("预热水箱加热温度设定", this.nudSetTemp.Value);
                    }

                    Common.ExChangeGrp.SetBool(btn.OutputTagName, setResult);
                }
                catch (Exception ex)
                {
                    Var.MsgBoxWarn(this, $"{btn.Text}+{th}失败！原因：" + ex.Message);
                }
            }
            //RButton btn = sender as RButton;
            //if (btn != null)
            //{
            //    var tag = btn.Tag.ToString();
            //    List<ContronlPoint> contronlList;
            //    if (_contronlMap.TryGetValue(tag, out contronlList))
            //    {

            //        if (btn.Switch)
            //        {
            //            Var.MsgBoxWarn(this, $"正在进行【{tag}】，请勿重复操作。");
            //            return;
            //        }

            //        var result = Var.MsgBoxYesNo(this, $"确定要开始 {tag} 吗");
            //        if (!result) return;

            //        // 获取所有的测试逻辑
            //        btn.Switch = true;
            //        _deviceCommandMap[tag] = true;

            //        if (contronlList.Count() == 0)
            //        {
            //            // 需要特殊处理
            //            if (tag == "预热水箱水加热")
            //            {
            //                var value = Common.waterGrp["预热水箱温度检测-T12"];
            //                var liquidLevel = Common.waterGrp["预热水箱液位检测"];

            //                // 液位低于目标值，禁止开始
            //                if (liquidLevel < SysConstantParasConfig.WaterLiquidLevel)
            //                {
            //                    Var.MsgBoxInfo(this, "当前预热水箱液位低于最小加热液位，禁止水加热。");
            //                    return;
            //                }
            //                // 液位大于目标值，但是温度达到了，也禁止开始
            //                if (liquidLevel >= SysConstantParasConfig.WaterLiquidLevel && value >= WaterTempValue)
            //                {
            //                    Var.MsgBoxInfo(this, "当前预热水箱温度已经到达目标值，禁止水加热。");
            //                    return;
            //                }

            //                Thread th = new Thread(() => btnWaterStart_Click(tag));
            //                th.Name = $"{tag} 线程";
            //                th.IsBackground = true;
            //                th.Start();
            //            }
            //            else if (tag == "机油预热")
            //            {
            //                Thread th = new Thread(() => btnWaterStart_Click(tag));
            //                th.Name = $"{tag} 线程";
            //                th.IsBackground = true;
            //                th.Start();
            //            }
            //            else if (tag == "燃油箱加油")
            //            {
            //                Thread th = new Thread(() => btnFeulStart_Click(tag));
            //                th.Name = $"{tag} 线程";
            //                th.IsBackground = true;
            //                th.Start();
            //            }
            //        }
            //        else
            //        {
            //            Thread th = new Thread(() => Start(tag, contronlList));
            //            th.Name = $"{tag} 一键合闸线程";
            //            th.IsBackground = true;
            //            th.Start();
            //        }
            //    }
            //    else
            //    {
            //        // 如果不存在则说明软件存在问题
            //        Var.MsgBoxWarn(this, "一键合闸 程序出现异常，未找到 Tag");
            //    }
            //}
        }

        /// <summary>
        /// 通用的停止试验逻辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStop_Click(object sender, EventArgs e)
        {
            RButton btn = sender as RButton;
            if (btn != null)
            {
                var tag = btn.Tag.ToString();

                _deviceCommandMap[tag] = false;
            }
        }

        /// <summary>
        /// 预热水箱水加热开始试验逻辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnWaterStart_Click(string tag)
        {
            bool levelResult = true;
            try
            {
                var value = Common.waterGrp["预热水箱温度检测-T12"];
                if (value < WaterTempValue)
                {
                    this.Invoke(new Action(() =>
                    {
                        this.nudSetTemp.Enabled = false;
                    }));

                    // 如果预热水箱加热开关是关闭的
                    if (!YRsxCheck())
                    {
                        Common.DOgrp["预热水箱加热控制"] = true;
                    }

                    while (_deviceCommandMap[tag])
                    {
                        value = Common.waterGrp["预热水箱温度检测-T12"];
                        if (value >= WaterTempValue)
                        {
                            break;
                        }

                        // 液位低于设置值后
                        var level = Common.waterGrp["预热水箱液位检测"];
                        if (level <= SysConstantParasConfig.WaterLiquidLevel)
                        {
                            levelResult = false;
                            break;
                        }
                        Thread.Sleep(1000);
                    }
                }
            }
            catch (Exception ex)
            {
                Var.LogInfo($"预热水箱加热线程出现异常：" + ex.ToString());
            }
            finally
            {
                // 如果打开了预热水箱加热开关
                if (YRsxCheck())
                {
                    Common.DOgrp["预热水箱加热控制"] = true;
                }
                _deviceCommandMap[tag] = false;

                this.Invoke(new Action(() =>
                {
                    this.nudSetTemp.Enabled = true;

                    // 异常时
                    if (!levelResult)
                    {
                        Var.MsgBoxWarn(this, "预热水箱液位低于目标值，水加热退出！");
                    }
                }));
            }
        }

        /// <summary>
        /// 预热水箱水加热开始试验逻辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFeulStart_Click(string tag)
        {
            try
            {

                while (_deviceCommandMap[tag])
                {
                    var value = Common.waterGrp["柴油箱液位检测-L29"];
                    if (value <= 400)
                    {
                        Common.DOgrp["Y164阀控制"] = true;
                    }
                    if (value >= 2000)
                    {
                        Common.DOgrp["Y164阀控制"] = false;
                    }
                    Thread.Sleep(1000);
                }
            }
            catch (Exception ex)
            {
                Var.LogInfo($"燃油箱加油线程出现异常：" + ex.ToString());
            }
            finally
            {
                // 如果打开了预热水箱加热开关
                if (YRsxCheck())
                {
                    Common.DOgrp["预热水箱加热控制"] = false;
                }
                _deviceCommandMap[tag] = false;
            }
        }

        /// <summary>
        /// 实时检测
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            this.rButton42.Switch = Common.ExChangeGrp.GetBool("预热水箱加水");
            this.rButton44.Switch = Common.ExChangeGrp.GetBool("预热水箱加热");
            this.rButton78.Switch = Common.ExChangeGrp.GetBool("高温水预热循环");
            this.rButton76.Switch = Common.ExChangeGrp.GetBool("高温水中冷水回抽");
            this.rButton52.Switch = Common.ExChangeGrp.GetBool("预供机油循环");
            this.rButton53.Switch = Common.ExChangeGrp.GetBool("机油加热处理循环");
            this.rButton24.Switch = Common.ExChangeGrp.GetBool("油底壳加油");
            this.rButton56.Switch = Common.ExChangeGrp.GetBool("油底壳抽油");
            this.rButton80.Switch = Common.ExChangeGrp.GetBool("机油回抽");
            this.rButton58.Switch = Common.ExChangeGrp.GetBool("燃油循环");
            this.rButton37.Switch = Common.ExChangeGrp.GetBool("燃油耗测量");
            this.rButton64.Switch = Common.ExChangeGrp.GetBool("燃油箱回油冷却");

            var ry1 = Common.ExChangeGrp.GetBool("燃油循环油泵选择");
            this.rButton28.Switch = !ry1; // 
            this.rButton40.Switch = ry1;

            var ry2 = Common.ExChangeGrp.GetBool("燃油耗测量油泵选择");
            this.rButton66.Switch = !ry2; // 燃油泵1
            this.rButton65.Switch = ry2;

            var ydk = Common.ExChangeGrp.GetBool("油底壳抽油选择油箱");
            this.rButton68.Switch = !ydk; // 机油箱
            this.rButton67.Switch = ydk;
        }

        /// <summary>
        /// 预热水箱检测
        /// </summary>
        /// <returns></returns>
        public bool YRsxCheck()
        {
            return (Common.DIgrp["预热水箱加热器控制1检测"]
             || Common.DIgrp["预热水箱加热器控制2检测"]
             || Common.DIgrp["预热水箱加热器控制3检测"]
             || Common.DIgrp["预热水箱加热器控制4检测"]
             || Common.DIgrp["预热水箱加热器控制5检测"]
             || Common.DIgrp["预热水箱加热器控制6检测"]);
        }

        /// <summary>
        /// 温度调节
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SetTemp_Click(object sender, EventArgs e)
        {
            var pipePara = sender as RButton;
            RW.Components.Widgets.frmSetValue set = new RW.Components.Widgets.frmSetValue();
            set.DecimalNumber = 1;
            set.MaxValue = 150;
            set.Unit = UnitEnum.celsius;
            set.Text = "温度设置";

            if (pipePara.Tag.ToString().Contains("机油温度"))
            {
                set.Value = Common.engineOilGrp[pipePara.Tag.ToString()];
                var dr = set.ShowDialog(this);
                if (dr == DialogResult.OK)
                    Common.engineOilGrp.EngineOilPID = set.Value;
            }
            else
            {
                set.Value = Common.waterGrp[pipePara.Tag.ToString()];
                var dr = set.ShowDialog(this);
                if (dr == DialogResult.OK)
                    if (pipePara.Tag.ToString().Contains("高温水"))
                    {
                        Common.waterGrp.HeightTempWaterPID = set.Value;
                    }
                    else
                    {
                        Common.waterGrp.MediumColdPID = set.Value;
                    }
            }
        }

        /// <summary>
        /// 流量调节
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SetFlow_Click(object sender, EventArgs e)
        {
            var pipePara = sender as RButton;
            RW.Components.Widgets.frmSetValue set = new RW.Components.Widgets.frmSetValue();
            set.DecimalNumber = 1;
            set.MaxValue = 100;
            set.Unit = UnitEnum.percent;
            set.Text = "流量设置";
            set.Value = Common.AOgrp[pipePara.Tag.ToString()];
            var dr = set.ShowDialog(this);
            if (dr == DialogResult.OK)
                Common.AOgrp[pipePara.Tag.ToString()] = set.Value;
        }

        /// <summary>
        /// 重新更新  预热水箱水加热 的目标温度
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetTemp_Click(object sender, EventArgs e)
        {
            var result = Var.MsgBoxYesNo(this, $"是否要更新预热水箱水加热的温度为 {this.nudSetTemp.Value} ℃？");
            if (!result) return;
            Common.ExChangeGrp.SetDouble("预热水箱加热温度设定", this.nudSetTemp.Value);
        }

        private void nudSetTemp_ValueChanged(object sender, double value)
        {
            WaterTempValue = this.nudSetTemp.Value.ToInt() + 2;
        }
    }


  

}
