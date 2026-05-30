using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BogieIdling.UI.TRDP;
using MainUI.Equip;
using MainUI.Global;
using MainUI.Helper;
using MainUI.Properties;
using MainUI.Widget;
using MetorSignalSimulator.UI.Model;
using RW.Components.User;
using Sunny.UI;
using static MainUI.Modules.EventArgsModel;

namespace MainUI
{
    public partial class ucForm2 : UserControl
    {
        Dictionary<string, ucValueLabel> dicValueLabel = new Dictionary<string, ucValueLabel>();
        Dictionary<string, UILight> dicLight = new Dictionary<string, UILight>();

        //管路数据显示条字典
        Dictionary<string, ucValueLabel> dicPipeLabel = new Dictionary<string, ucValueLabel>();

        // 按钮点击事件
        Dictionary<string, UIButton> ButtonDic = new Dictionary<string, UIButton>();
        Dictionary<string, Panel> PilpDic = new Dictionary<string, Panel>();

        // 状态栏处理类
        private DeviceStatusProcessor _statusProcessor;

        public ucForm2()
        {
            InitializeComponent();

        }

        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            // 初始化设备状态处理器
            _statusProcessor = new DeviceStatusProcessor(false);

            tslblUser.Text = "登录用户：" + RW.UI.RWUser.User.Username;
            tslblVersion.Text = Var.Version;
            statusStrip1.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
            tslblVersion.Alignment = ToolStripItemAlignment.Right;

            EachControl(this);

            Var.TRDP.KeyValueChange += TRDP_KeyValueChange;
            Common.DIgrp.KeyValueChange += DIgrp_KeyValueChange;
            //管路数据点位注册和初始化
            Common.AI2Grp.KeyValueChange += AI2Grp_KeyValueChange;
            Common.waterGrp.KeyValueChange += WaterGrp_KeyValueChange;
            Common.engineOilGrp.KeyValueChange += EngineOilGrp_KeyValueChange;
            Common.fuelGrp.KeyValueChange += FuelGrp_KeyValueChange;

            //Common.DIgrp.Fresh();
            //Common.waterGrp.Fresh();
            //Common.engineOilGrp.Fresh();
            //Common.fuelGrp.Fresh();

            // 初始化字典
            ButtonDic.Add("ECM数据", this.btnECM);
            ButtonDic.Add("管路数据", this.btnPipeline);

            PilpDic.Add("ECM数据", panelTRDP);
            PilpDic.Add("管路数据", panelPipeline);

            // 默认显示TRDP系统
            var key = "ECM数据";
            foreach (var item in ButtonDic)
            {
                if (item.Key == key)
                {
                    ButtonDic[item.Key].FillColor = Color.FromArgb(80, 160, 255);
                    PilpDic[item.Key].Visible = true;
                }
                else
                {
                    PilpDic[item.Key].Visible = false;
                    ButtonDic[item.Key].FillColor = Color.FromArgb(173, 178, 181);
                }
            }

            this.timer1.Enabled = true;
            this.timer1.Start();

            this.timerPLC.Enabled = true;
            this.timerPLC.Start();

            InitDynamicTRDP();
        }


        #region 管路数据的值改变事件
        private void AI2Grp_KeyValueChange(object sender, DoubleValueChangedEventArgs e)
        {
            if (dicPipeLabel.ContainsKey(e.Key))
            {
                dicPipeLabel[e.Key].Value = e.Value;
            }
        }

        private void FuelGrp_KeyValueChange(object sender, DoubleValueChangedEventArgs e)
        {
            if (dicPipeLabel.ContainsKey(e.Key))
            {
                dicPipeLabel[e.Key].Value = e.Value;
            }
        }

        private void EngineOilGrp_KeyValueChange(object sender, DoubleValueChangedEventArgs e)
        {
            if (dicPipeLabel.ContainsKey(e.Key))
            {
                dicPipeLabel[e.Key].Value = e.Value;
            }
        }

        private void WaterGrp_KeyValueChange(object sender, DoubleValueChangedEventArgs e)
        {
            if (dicPipeLabel.ContainsKey(e.Key))
            {
                dicPipeLabel[e.Key].Value = e.Value;
            }
        }
        #endregion

        /// <summary>
        /// 急停
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DIgrp_KeyValueChange(object sender, DIValueChangedEventArgs e)
        {
            if (e.Key == "紧急停止")
            {
                if (e.Value)
                {
                    this.picRunStatus.Image = Resources.normal;
                }
                else
                {
                    this.picRunStatus.Image = Resources.scram;
                }
            }
        }

        /// <summary>
        /// 切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectPage_Click(object sender, EventArgs e)
        {
            var button = sender as UIButton;
            var key = button.Text;
            foreach (var item in ButtonDic)
            {
                if (item.Key == key)
                {
                    PilpDic[item.Key].Visible = true;
                    ButtonDic[item.Key].FillColor = Color.FromArgb(80, 160, 255);
                }
                else
                {
                    PilpDic[item.Key].Visible = false;
                    ButtonDic[item.Key].FillColor = Color.FromArgb(173, 178, 181);
                }
            }
        }

        /// <summary>
        /// 监控TRDP数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TRDP_KeyValueChange(object sender, TRDPValueChangedEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => TRDP_KeyValueChange(sender, e)));
                return;
            }

            if (dicValueLabel.ContainsKey(e.Key))
            {
                dicValueLabel[e.Key].Value = e.Value.ToDouble();
            }
            if (dicLight.ContainsKey(e.Key))
            {
                dicLight[e.Key].State = e.Value == 0 ? UILightState.Off : UILightState.On;
            }
            if (e.Key == "同步状态")
            {
                this.lblConState.Text = e.Value == 0 ? "未同步" : "已同步";
            }
        }

        /// <summary>
        /// 添加控件到字典
        /// </summary>
        /// <param name="con"></param>
        private void EachControl(Control con)
        {
            foreach (Control item in con.Controls)
            {
                EachControl(item);
            }
            if (con is ucValueLabel)
            {
                //添加TRDP数值显示条
                ucValueLabel valueLabel = con as ucValueLabel;
                if (valueLabel.Tag != null && valueLabel.Tag.ToString() != string.Empty)
                {
                    dicValueLabel.Add(valueLabel.Tag.ToString(), valueLabel);
                }
                //添加Pipe数值显示条
                if (valueLabel.Key != null && valueLabel.Key.ToString() != string.Empty)
                {
                    dicPipeLabel.Add(valueLabel.Key.ToString(), valueLabel);
                }
            }
            if (con is UILight)
            {
                //添加TRDP状态灯
                UILight light = con as UILight;
                if (light.Tag != null && light.Tag.ToString() != string.Empty)
                {
                    dicLight.Add(light.Tag.ToString(), light);
                }
            }
        }

        /// <summary>
        /// 时间刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            this.lblDateTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }


        /// <summary>
        /// 底部状态栏刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerPLC_Tick(object sender, EventArgs e)
        {
            try
            {
                // 1. 台位控制OPC
                bool IsPlcNg = _statusProcessor.FreshCommStatus(tslblPLC, "台位控制", Common.opcStatus.Simulated, Common.opcStatus.NoError);

                // 2. PLC测量OPC
                bool IsCkPlcNg = _statusProcessor.FreshCommStatus(tslblCKPLC, "测量柜", Common.AI2Grp.Simulated, Common.AI2Grp.NoError);

                // 3. 启动柜OPC
                bool IsStartNg = _statusProcessor.FreshCommStatus(tslblStart, "启动柜", Common.startPLCGrp.Simulated, Common.startPLCGrp.NoError);

                // 4. 发动机电参数
                bool IsElectricalNg = _statusProcessor.FreshCommStatus(tslblElectrical, "发动机电参数", Common.threePhaseElectric.Simulated, Common.threePhaseElectric.NoError);

                // 5-7. 机油箱系统 - 统一处理
                ProcessEngineOilSystem();

                // 燃油
                ProcessFeulSystem();

                // 水系统
                ProcessWaterSystem();

                // 11. 柴油机控制器 (注意：此处第二个参数固定为false，表示非仿真模式)
                bool IsTrdpNg = _statusProcessor.FreshCommStatus(tslblTRDP, "柴油机控制器", false, Var.TRDP.IsConnected);

                var IszcNg = false;
                var lsRYNg = false;
                var lsWeightNg = false;
                var IsBC = false;
                if (Var.SysConfig.ExeType == 1)
                {
                    // 如果是控制端
                    IszcNg = _statusProcessor.FreshCommStatus(tslblCommunication, "台位主从通讯", false, true);
                    lsRYNg = _statusProcessor.FreshCommStatus(tslblRYHY, "燃油耗仪", false, ET4500.Instance.IsConnected);
                    lsWeightNg = _statusProcessor.FreshCommStatus(tslblWeight, "称重仪", false, ZMPT650F.Instance.IsConnected);
                    IsBC = _statusProcessor.FreshCommStatus(tslblJYBC, "机油耗磅秤", false, YHA27.Instance.IsConnected);
                }
                else
                {
                    // 检测
                    IszcNg = _statusProcessor.FreshCommStatus(tslblCommunication, "台位主从通讯", Common.opcExChangeReceiveGrp.Simulated, Common.opcExChangeReceiveGrp.NoError);
                    lsRYNg = _statusProcessor.FreshCommStatus(tslblRYHY, "燃油耗仪", false, Common.opcExChangeReceiveGrp.GetDouble("油耗仪_NoError") == 1);
                    lsWeightNg = _statusProcessor.FreshCommStatus(tslblWeight, "称重仪", false, Common.opcExChangeReceiveGrp.GetDouble("称重仪_NoError") == 1);
                    IsBC = _statusProcessor.FreshCommStatus(tslblJYBC, "机油耗磅秤", false, Common.opcExChangeReceiveGrp.GetDouble("磅秤_NoError") == 1);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 处理机油箱系统
        /// </summary>
        private void ProcessEngineOilSystem()
        {
            var status = _statusProcessor.AnalyzeDeviceStatus(
                Common.engineOilGrp.Simulated,
                Common.engineOilGrp.NoError,
                "机油系统"
            );

            _statusProcessor.UpdateStatusLabel(
                status,
                tslblJYOK,
                tslblJYNG,
                tslblJYSim,
                "机油系统",
                () => Common.engineOilGrp.IsSimulated,
                () => Common.engineOilGrp.IsNoError
            );
        }

        /// <summary>
        /// 处理燃油箱系统
        /// </summary>
        private void ProcessFeulSystem()
        {
            var status = _statusProcessor.AnalyzeDeviceStatus(
                Common.fuelGrp.Simulated,
                Common.fuelGrp.NoError,
                "燃油系统"
            );

            _statusProcessor.UpdateStatusLabel(
                status,
                tslblRYOK,
                tslblRYNG,
                tslblRYSim,
                "燃油系统",
                () => Common.fuelGrp.IsSimulated,
                () => Common.fuelGrp.IsNoError
            );
        }

        /// <summary>
        /// 处理水箱系统
        /// </summary>
        private void ProcessWaterSystem()
        {
            var status = _statusProcessor.AnalyzeDeviceStatus(
                Common.waterGrp.Simulated,
                Common.waterGrp.NoError,
                "水系统"
            );

            _statusProcessor.UpdateStatusLabel(
                status,
                tslblWaterOK,
                tslblWaterNG,
                tslblWaterSim,
                "水系统",
                () => Common.waterGrp.IsSimulated,
                () => Common.waterGrp.IsNoError
            );
        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            bool ok = Var.MsgBoxYesNo(this, "确定要退出软件吗？");
            if (ok)
            {
                Var.LogInfo("用户" + RWUser.User.Username + "退出登录。");

                try
                {
                    Var.Close();
                    // 关trdp连接
                    Var.TRDP.Timestop();
                    // 关闭燃油耗仪通讯
                    ET4500.Instance.Close();
                }
                catch (Exception ex)
                {
                    Var.LogInfo($"程序退出 OPC关闭失败 {ex.ToString()}");
                }
                Application.Exit();
            }
        }

        #region ECM数据自动生成

        // ── 总开关：默认 false，保持现有功能不变 ────────────────────────────
        /// <summary>
        /// 是否启用 TRDP 动态界面生成。默认 false（沿用静态界面）。
        /// 置 true 后，panelTRDP 内容将由 Var.TRDP.tags 自动生成。
        /// </summary>
        public bool EnableDynamicTRDP { get; set; } = true;

        // ── 标记是否已订阅型号事件，避免重复订阅 ──────────────────────────
        private bool _dynTRDPSubscribed = false;

        // ── 布局常量（可按实际界面微调） ───────────────────────────────────
        private const int DYN_PAD_X = 12;   // 左右内边距
        private const int DYN_PAD_TOP = 12;  // 顶部内边距
        private const int DYN_COL_W = 360;  // 每个数值条/灯行宽度
        private const int DYN_ROW_H = 40;   // 行高（含间距）
        private const int DYN_COL_GAP = 16;  // 列间距

        /// <summary>
        /// 启用并首次构建动态 TRDP 界面。
        /// 在 ucForm2.Init() 末尾调用：若 EnableDynamicTRDP 为 true 则生效。
        /// </summary>
        public void InitDynamicTRDP()
        {
            if (!EnableDynamicTRDP) return;

            // 订阅型号切换（只订阅一次）
            if (!_dynTRDPSubscribed)
            {
                EventTriggerModel.OnModelNameChanged += OnDynTRDPModelChanged;
                this.Disposed += (s, e) =>
                {
                    EventTriggerModel.OnModelNameChanged -= OnDynTRDPModelChanged;
                };
                _dynTRDPSubscribed = true;
            }

            // 首次构建（若已加载过型号 Excel，则立即铺满）
            RebuildDynamicTRDP();
        }

        // ── 型号切换回调 ───────────────────────────────────────────────────
        private void OnDynTRDPModelChanged(string modelName)
        {
            if (!EnableDynamicTRDP) return;
            if (this.IsDisposed || !this.IsHandleCreated) return;
            try
            {
                if (this.InvokeRequired)
                    this.Invoke(new Action(RebuildDynamicTRDP));
                else
                    RebuildDynamicTRDP();
            }
            catch { /* 窗体已关闭，忽略 */ }
        }

        // ── 核心：重建 panelTRDP 内容 ──────────────────────────────────────
        /// <summary>
        /// 从 Var.TRDP.tags 读取当前型号信号，重建 panelTRDP 内的数值条/状态灯，
        /// 并重建 dicValueLabel / dicLight 字典。必须在 UI 线程调用。
        /// </summary>
        private void RebuildDynamicTRDP()
        {
            if (panelTRDP == null) return;

            panelTRDP.SuspendLayout();
            try
            {
                // 1. 仅清空 panelTRDP 内的动态控件（管路 panel 不动）
                ClearDynamicTRDPControls();

                // 2. 从 dicValueLabel / dicLight 中移除属于 TRDP 的键，避免残留旧型号控件
                //    （只移除由本方法生成、Tag 形式的键；管路键存在 dicPipeLabel，本就不在这两个字典里）
                dicValueLabel.Clear();
                dicLight.Clear();

                // 3. 读取当前信号
                var tags = GetDynamicTRDPTags();
                if (tags.Count == 0)
                {
                    var tip = MakeDynTipLabel("当前未加载任何 ECM 信号，请先在主界面选择型号。");
                    panelTRDP.Controls.Add(tip);
                    return;
                }

                // 4. 分类：模拟量(数值条) / 数字量B1(状态灯)
                var analogTags = tags.Where(t => IsDynAnalogType(t.DataType)).ToList();
                var digitalTags = tags.Where(t => t.DataType == "B1").ToList();

                // 5. 自动多列网格布局
                int areaWidth = panelTRDP.ClientSize.Width > 0
                    ? panelTRDP.ClientSize.Width
                    : panelTRDP.Width;
                int cols = Math.Max(1, (areaWidth - DYN_PAD_X) / (DYN_COL_W + DYN_COL_GAP));

                int idx = 0;

                // 5a. 模拟量数值条
                foreach (var tag in analogTags)
                {
                    var ctrl = BuildDynValueLabel(tag);
                    PlaceDynControl(ctrl, idx, cols);
                    panelTRDP.Controls.Add(ctrl);

                    // 注册到刷新字典（键 = 信号名 = DataLabel）
                    if (!string.IsNullOrEmpty(tag.DataLabel) &&
                        !dicValueLabel.ContainsKey(tag.DataLabel))
                    {
                        dicValueLabel.Add(tag.DataLabel, ctrl);
                    }
                    idx++;
                }

                // 5b. 数字量状态灯
                foreach (var tag in digitalTags)
                {
                    var lightRow = BuildDynLightRow(tag, out UILight light);
                    PlaceDynControl(lightRow, idx, cols);
                    panelTRDP.Controls.Add(lightRow);

                    if (!string.IsNullOrEmpty(tag.DataLabel) &&
                        !dicLight.ContainsKey(tag.DataLabel))
                    {
                        dicLight.Add(tag.DataLabel, light);
                    }
                    idx++;
                }
            }
            finally
            {
                panelTRDP.ResumeLayout();
            }
        }

        // ── 清空 panelTRDP 内由本类生成的控件 ──────────────────────────────
        private void ClearDynamicTRDPControls()
        {
            // 拷贝一份再移除，避免遍历时修改集合
            var toRemove = new List<Control>();
            foreach (Control c in panelTRDP.Controls)
            {
                toRemove.Add(c);
            }
            foreach (var c in toRemove)
            {
                panelTRDP.Controls.Remove(c);
                c.Dispose();
            }
        }

        // ── 生成一个模拟量数值条 ───────────────────────────────────────────
        private ucValueLabel BuildDynValueLabel(FullTags tag)
        {
            var vl = new ucValueLabel
            {
                Width = DYN_COL_W,
                Height = 34,
                Tag = tag.DataLabel,          // ★ Tag = 信号名，供 TRDP_KeyValueChange 匹配
                Title = tag.DataLabel,        // 标题显示信号名
                Unit = string.IsNullOrEmpty(tag.DataUnit) ? "" : tag.DataUnit,
                Value = 0
            };
            return vl;
        }

        // ── 生成一行“信号名 + 状态灯” ─────────────────────────────────────
        private Panel BuildDynLightRow(FullTags tag, out UILight light)
        {
            var row = new Panel
            {
                Width = DYN_COL_W,
                Height = 30,
                BackColor = Color.Transparent
            };

            var lbl = new UILabel
            {
                Text = tag.DataLabel,
                AutoSize = false,
                Width = DYN_COL_W - 60,
                Height = 28,
                Location = new Point(0, 1),
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("宋体", 11F)
            };

            light = new UILight
            {
                Tag = tag.DataLabel,          // Tag = 信号名
                Width = 24,
                Height = 24,
                Location = new Point(DYN_COL_W - 32, 3),
                State = UILightState.Off
            };

            row.Controls.Add(lbl);
            row.Controls.Add(light);
            return row;
        }

        // ── 网格定位 ───────────────────────────────────────────────────────
        private void PlaceDynControl(Control ctrl, int index, int cols)
        {
            int r = index / cols;
            int c = index % cols;
            int x = DYN_PAD_X + c * (DYN_COL_W + DYN_COL_GAP);
            int y = DYN_PAD_TOP + r * DYN_ROW_H;
            ctrl.Location = new Point(x, y);
        }

        // ── 提示标签 ───────────────────────────────────────────────────────
        private Label MakeDynTipLabel(string text)
        {
            return new Label
            {
                Text = text,
                AutoSize = true,
                ForeColor = Color.Gray,
                Font = new Font("宋体", 11F),
                Location = new Point(DYN_PAD_X, DYN_PAD_TOP)
            };
        }

        // ── 工具：取当前信号列表 ───────────────────────────────────────────
        private static List<FullTags> GetDynamicTRDPTags()
        {
            try
            {
                if (Var.TRDP == null || Var.TRDP.tags == null)
                    return new List<FullTags>();
                return Var.TRDP.tags
                    .Where(t => t != null && !string.IsNullOrEmpty(t.DataLabel))
                    .ToList();
            }
            catch
            {
                return new List<FullTags>();
            }
        }

        // ── 工具：判断是否模拟量类型 ───────────────────────────────────────
        private static bool IsDynAnalogType(string dataType)
        {
            return dataType == "U16" || dataType == "I16"
                || dataType == "U32" || dataType == "I32"
                || dataType == "F32" || dataType == "U8"
                || dataType == "I8";
        }


        #endregion
    }
}
