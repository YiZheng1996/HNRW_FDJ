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
                // 异步投递，不阻塞后台 TRDP 推送线程；窗体已关闭则忽略
                try { this.BeginInvoke(new Action(() => TRDP_KeyValueChange(sender, e))); }
                catch { /* 窗体已销毁，忽略 */ }
                return;
            }

            // 重建期间字典处于清空/重填中间态，本帧跳过，重建完下一帧自然补上
            if (_rebuildingTRDP) return;

            if (dicValueLabel.ContainsKey(e.Key))
            {
                dicValueLabel[e.Key].Value = e.Value.ToDouble();
            }
            if (dicLight.ContainsKey(e.Key))
            {
                // 普通报警位：值≠0→报警亮；反极性位(同步状态)：值==0→异常亮
                bool on = IsActiveLowSignal(e.Key) ? (e.Value == 0) : (e.Value != 0);
                dicLight[e.Key].State = on ? UILightState.On : UILightState.Off;
            }
            if (e.Key == "同步状态" && lblConState != null && !lblConState.IsDisposed)
            {
                lblConState.Text = e.Value == 0 ? "未同步" : "已同步";
            }
        }

        /// <summary>
        /// 反极性信号：1=正常、0=异常（与普通报警位 0=正常、1=报警 相反）。
        /// 目前仅"同步状态"。如有其它同类状态位，在此追加关键字即可。
        /// </summary>
        private static bool IsActiveLowSignal(string key)
        {
            return !string.IsNullOrEmpty(key) && key.Contains("同步状态");
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

        // 总开关：默认 false，保持现有功能不变
        /// <summary>
        /// 是否启用 TRDP 动态界面生成。默认 false（沿用静态界面）。
        /// 置 true 后，panelTRDP 内容将由 Var.TRDP.tags 自动生成。
        /// </summary>
        public bool EnableDynamicTRDP { get; set; } = true;

        // 标记是否已订阅型号事件，避免重复订阅
        private bool _dynTRDPSubscribed = false;

        // 重建期间置 true，刷新回调据此跳帧，避免读到中间态字典。volatile 保证跨线程可见
        private volatile bool _rebuildingTRDP = false;


        // 布局常量（可按实际界面微调）
        private const int DYN_PAD_X = 12;   // 左右内边距
        private const int DYN_PAD_TOP = 12;  // 顶部内边距
        private const int DYN_COL_W = 430;  // 每个数值条/灯行宽度
        private const int DYN_ROW_H = 40;   // 行高（含间距）
        private const int DYN_COL_GAP = 50;  // 列间距
        private const int DYN_DIGITAL_GAP = 24;  // 数字量列间距/起始间距，独立可调

        private static readonly Font DYN_TITLE_FONT = new Font("宋体", 13F, FontStyle.Bold);
        private const int DYN_VALUE_W = 110;
        private const int DYN_UNIT_W = 70;
        private const int DYN_GAP = 6;

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

        // ── 型号切换回调
        private void OnDynTRDPModelChanged(string modelName)
        {
            if (!EnableDynamicTRDP) return;
            if (this.IsDisposed || !this.IsHandleCreated) return;
            try
            {
                if (this.InvokeRequired)
                    this.BeginInvoke(new Action(RebuildDynamicTRDP));
                else
                    RebuildDynamicTRDP();
            }
            catch { /* 窗体已关闭，忽略 */ }
        }

        /// <summary>
        /// 从 Var.TRDP.tags 读取当前型号信号，重建 panelTRDP 内的数值条/状态灯，
        /// 并重建 dicValueLabel / dicLight 字典。必须在 UI 线程调用。
        /// </summary>
        private void RebuildDynamicTRDP()
        {
            if (panelTRDP == null) return;

            _rebuildingTRDP = true;
            panelTRDP.SuspendLayout();
            try
            {
                // 先断开字典引用，再销毁控件
                dicValueLabel.Clear();
                dicLight.Clear();
                ClearDynamicTRDPControls();

                var tags = GetDynamicTRDPTags();
                if (tags.Count == 0)
                {
                    var tip = MakeDynTipLabel("当前未加载任何 ECM 信号，请先在主界面选择型号。");
                    panelTRDP.Controls.Add(tip);
                    return;
                }

                // 分类
                // 数字量灯：B1 + Excel 标了 IsBool 的整型报警量
                var digitalTags = tags.Where(t => t.DataType == "B1" || IsDynBooleanAlarm(t)).ToList();
                // 模拟量数值条：真模拟量，排除 0/1 报警量
                var analogTags = tags.Where(t => IsDynAnalogType(t.DataType) && !IsDynBooleanAlarm(t)).ToList();

                int areaWidth = panelTRDP.ClientSize.Width > 0
                    ? panelTRDP.ClientSize.Width : panelTRDP.Width;

                const int analogCols = 3;   // 左：模拟量 3 列
                const int digitalCols = 2;  // 右：数字量 2 列

                // ── 模拟量：列优先分列，每列独立算标题宽度，逐列紧凑排布 ──
                int analogRows = Math.Max(1, (analogTags.Count + analogCols - 1) / analogCols);

                // 先按"列优先"把信号分到各列
                var analogByCol = new List<List<FullTags>>();
                for (int c = 0; c < analogCols; c++) analogByCol.Add(new List<FullTags>());
                for (int i = 0; i < analogTags.Count; i++)
                    analogByCol[Math.Min(analogCols - 1, i / analogRows)].Add(analogTags[i]);

                // 累加式列起点：每列宽度按自身最长名称决定 → 名称与值贴紧
                int colX = DYN_PAD_X;
                for (int c = 0; c < analogCols; c++)
                {
                    var colTags = analogByCol[c];
                    if (colTags.Count == 0) continue;

                    int colTitleW = ComputeAnalogTitleWidth(colTags);   // 每列独立
                    int colCtrlW = colTitleW + DYN_VALUE_W + DYN_UNIT_W + DYN_GAP;

                    for (int r = 0; r < colTags.Count; r++)
                    {
                        var tag = colTags[r];
                        var ctrl = BuildDynValueLabel(tag, colTitleW);
                        ctrl.Location = new Point(colX, DYN_PAD_TOP + r * DYN_ROW_H);
                        panelTRDP.Controls.Add(ctrl);

                        if (!string.IsNullOrEmpty(tag.DataLabel) &&
                            !dicValueLabel.ContainsKey(tag.DataLabel))
                            dicValueLabel.Add(tag.DataLabel, ctrl);
                    }

                    colX += colCtrlW + DYN_COL_GAP;   // 下一列紧跟（DYN_COL_GAP 控制列间距）
                }

                // ── 数字量：紧跟模拟量右侧，列宽按最长名称定宽（不再平分剩余宽度）──
                if (digitalTags.Count > 0)
                {
                    // 数字量行宽 = 灯+间隙(30) + 最长名称 + 余量(16)
                    int digTitleW = 0;
                    using (var f = new Font("宋体", 11F))
                    {
                        foreach (var t in digitalTags)
                        {
                            int w = TextRenderer.MeasureText(t.DataLabel ?? "", f).Width;
                            if (w > digTitleW) digTitleW = w;
                        }
                    }
                    int digitalColW = 50 + digTitleW + 30;     // 单列实际宽度

                    int digitalStartX = colX + DYN_DIGITAL_GAP;   // 与模拟量整体留点距离
                    int digitalTop = DYN_PAD_TOP;
                    int digitalRows = Math.Max(1, (digitalTags.Count + digitalCols - 1) / digitalCols);
                    for (int i = 0; i < digitalTags.Count; i++)
                    {
                        var tag = digitalTags[i];
                        var lightRow = BuildDynLightRow(tag, digitalColW, out UILight light);

                        int col = i / digitalRows;   // 列优先
                        int row = i % digitalRows;
                        lightRow.Location = new Point(
                            digitalStartX + col * (digitalColW + DYN_DIGITAL_GAP),  // 列间距独立
                            digitalTop + row * DYN_ROW_H);
                        panelTRDP.Controls.Add(lightRow);

                        if (!string.IsNullOrEmpty(tag.DataLabel) &&
                            !dicLight.ContainsKey(tag.DataLabel))
                            dicLight.Add(tag.DataLabel, light);
                    }
                }
            }
            finally
            {
                panelTRDP.ResumeLayout();
                _rebuildingTRDP = false;
            }
        }

        // 统一标题列宽 value 框对齐
        private static int ComputeAnalogTitleWidth(List<FullTags> analogTags)
        {
            int maxTitleAllowed = DYN_COL_W - DYN_VALUE_W - DYN_UNIT_W - DYN_GAP;
            int titleW = 110;
            foreach (var t in analogTags)
            {
                int w = TextRenderer.MeasureText(t.DataLabel ?? "", DYN_TITLE_FONT).Width + 12;
                if (w > titleW) titleW = w;
            }
            return Math.Min(titleW, maxTitleAllowed);
        }

        // 查看 Excel 标记
        private static bool IsDynBooleanAlarm(FullTags t)
        {
            return t != null && t.IsBoolAlarm;
        }

        // 清空 panelTRDP 内由本类生成的控件
        private void ClearDynamicTRDPControls()
        {
            // 拷贝一份再移除，避免遍历时修改集合
            var toRemove = new List<Control>();
            foreach (Control c in panelTRDP.Controls)
            {
                // 仅保护同步状态固定控件，其余照旧全清
                if (ReferenceEquals(c, lblConState)) continue;
                if (ReferenceEquals(c, label3)) continue;   // "同步状态："前缀，可选
                toRemove.Add(c);
            }
            foreach (var c in toRemove)
            {
                panelTRDP.Controls.Remove(c);
                c.Dispose();
            }
        }

        // 生成一个模拟量数值条
        private ucValueLabel BuildDynValueLabel(FullTags tag, int titleW)
        {
            var vl = new ucValueLabel
            {
                Width = titleW + DYN_VALUE_W + DYN_UNIT_W + DYN_GAP,
                Height = 34,
                Tag = tag.DataLabel,
                Title = tag.DataLabel,
                TitleFont = DYN_TITLE_FONT,
                TitleWidth = titleW,            // 所有控件相同 value 框对齐
                TitleColor = Color.Black,
                ValueWidth = DYN_VALUE_W,
                ValueColor = Color.Black,
                Unit = string.IsNullOrEmpty(tag.DataUnit) ? "" : tag.DataUnit,
                UnitWidth = DYN_UNIT_W,
                BackColor = Color.Transparent,
                Value = 0
            };
            if (vl.Unit == "mm3")
            {
                vl.Unit = "mm³";
            }
            return vl;
        }


        // 生成一行“信号名 + 状态灯” 
        private Panel BuildDynLightRow(FullTags tag, int rowWidth, out UILight light)
        {
            var row = new Panel
            {
                Width = rowWidth,
                Height = DYN_ROW_H - 6,
                BackColor = Color.Transparent
            };

            light = new UILight
            {
                Tag = tag.DataLabel,
                Width = 30,
                Height = 30,
                Location = new Point(2, (row.Height - 30) / 2),
                OffColor = SystemColors.ControlText,
                OffCenterColor = Color.Silver,
                OnColor = Color.Red,
                OnCenterColor = Color.FromArgb(255, 188, 128),
                State = UILightState.Off
            };

            var lbl = new UILabel
            {
                Text = tag.DataLabel,
                AutoSize = false,
                Width = rowWidth - 32,
                Height = row.Height,
                Location = new Point(30, 0),
                TextAlign = ContentAlignment.MiddleLeft,
                Font = DYN_TITLE_FONT,
                ForeColor = Color.Black
            };

            row.Controls.Add(light);
            row.Controls.Add(lbl);
            return row;
        }

        //  提示标签
        private Label MakeDynTipLabel(string text)
        {
            return new Label
            {
                Text = text,
                AutoSize = true,
                ForeColor = Color.Gray,
                Font = new Font("宋体", 13F),
                Location = new Point(DYN_PAD_X, DYN_PAD_TOP)
            };
        }

        // 取当前信号列表
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

        // 判断是否模拟量类型
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
