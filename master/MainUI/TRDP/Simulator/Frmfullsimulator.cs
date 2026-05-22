using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using MainUI.Config.Modules;
using MainUI.Equip;
using MainUI.Global;
using MainUI.Simulate;
using MetorSignalSimulator.UI.Model;

namespace MainUI
{
    /// <summary>
    /// 全信号仿真器
    /// 覆盖：AI · AI2 · DI · ET4500 · ZMPT650F · TRDP（内嵌，不再弹窗）
    /// frmTRDPSimulator 一行未改，两者共用 TRDPSimulatorService.Instance。
    /// </summary>
    public partial class frmFullSimulator : Form
    {
        // ════════════════════════════════════════════════════════════════════
        // 布局常量 & 颜色
        // ════════════════════════════════════════════════════════════════════

        private const int ROW_H = 36;
        private const int LBL_W = 210;
        private const int TB_W = 300;
        private const int NUD_W = 90;
        private const int UNIT_W = 55;
        private const int PAD_X = 8;
        private const int PAD_Y = 8;

        private static readonly Color C_HEADER = Color.FromArgb(45, 85, 130);
        private static readonly Color C_ON = Color.FromArgb(40, 160, 80);
        private static readonly Color C_OFF = Color.FromArgb(200, 60, 60);
        private static readonly Color C_FAULT = Color.FromArgb(255, 200, 200);
        private static readonly Color C_NORMAL_D = Color.FromArgb(220, 237, 220);

        // ── 值范围结构体（替代元组，兼容 .NET 4.5）──────────────────────────
        private struct SignalRange
        {
            public double Min, Max;
            public string Unit;
            public SignalRange(double min, double max, string unit)
            { Min = min; Max = max; Unit = unit; }
        }

        // ════════════════════════════════════════════════════════════════════
        // AI / AI2 控件字典
        // ════════════════════════════════════════════════════════════════════

        private readonly Dictionary<string, TrackBar> _aiTb = new Dictionary<string, TrackBar>();
        private readonly Dictionary<string, NumericUpDown> _aiNud = new Dictionary<string, NumericUpDown>();
        private readonly Dictionary<string, TrackBar> _ai2Tb = new Dictionary<string, TrackBar>();
        private readonly Dictionary<string, NumericUpDown> _ai2Nud = new Dictionary<string, NumericUpDown>();

        // ════════════════════════════════════════════════════════════════════
        // DI 控件字典
        // ════════════════════════════════════════════════════════════════════

        private readonly Dictionary<string, Button> _diCtrl = new Dictionary<string, Button>();

        // ════════════════════════════════════════════════════════════════════
        // 串口设备控件
        // ════════════════════════════════════════════════════════════════════

        private NumericUpDown _nudFuelConsumption, _nudRemainingFuel, _nudFuelPct, _nudWeight;
        private Label _lblET4500Status, _lblZMPTStatus;

        // ════════════════════════════════════════════════════════════════════
        // TRDP 内嵌面板专用字段（全部加 _trdp 前缀，与 frmTRDPSimulator 完全隔离）
        // ════════════════════════════════════════════════════════════════════

        private readonly Dictionary<string, TrdpAnalogEntry> _trdpAnalogControls
            = new Dictionary<string, TrdpAnalogEntry>();
        private readonly Dictionary<string, Button> _trdpDigitalButtons
            = new Dictionary<string, Button>();

        private TabPage _trdpTpAnalog, _trdpTpDigital, _trdpTpAxis, _trdpTpStatus;
        private RichTextBox _trdpRtbLog;
        private Label _trdpLblStatus, _trdpLblTick, _trdpLblLifeVal;
        private Button _trdpBtnLifeAuto;
        private System.Windows.Forms.Timer _trdpUiTimer;
        private bool _trdpLifeAuto = true;

        // ════════════════════════════════════════════════════════════════════
        // 全局刷新定时器（串口状态）
        // ════════════════════════════════════════════════════════════════════

        private System.Windows.Forms.Timer _refreshTimer;

        // ════════════════════════════════════════════════════════════════════
        // 构造
        // ════════════════════════════════════════════════════════════════════

        public frmFullSimulator()
        {
            Text = "全信号仿真器";
            Size = new Size(1000, 760);
            MinimumSize = new Size(900, 600);
            StartPosition = FormStartPosition.CenterScreen;
            Font = new Font("微软雅黑", 9f);
            BackColor = Color.FromArgb(248, 248, 252);

            var tab = new TabControl
            {
                Dock = DockStyle.Fill,
                Font = new Font("微软雅黑", 9.5f),
                Padding = new Point(12, 4),
            };
            Controls.Add(tab);

            tab.TabPages.Add(BuildAnalogTab(
                "AI 模拟量（PLC1）", GetAI1Tags(),
                delegate (string k, double v) { Common.AIgrp[k] = v; },
                _aiTb, _aiNud));

            tab.TabPages.Add(BuildAnalogTab(
                "AI2 模拟量（PLC2）", GetAI2Tags(),
                delegate (string k, double v) { Common.AI2Grp[k] = v; },
                _ai2Tb, _ai2Nud));

            tab.TabPages.Add(BuildDITab());
            tab.TabPages.Add(BuildSerialTab());
            tab.TabPages.Add(BuildPresetTab());
            tab.TabPages.Add(BuildTRDPEmbeddedTab());

            var status = new StatusStrip();
            status.Items.Add(new ToolStripStatusLabel(
                "仿真器就绪 · 修改任意值后立即注入到系统")
            { ForeColor = Color.DimGray });
            Controls.Add(status);

            // 串口状态刷新
            _refreshTimer = new System.Windows.Forms.Timer { Interval = 500 };
            _refreshTimer.Tick += RefreshSerialStatus;
            _refreshTimer.Start();

            // 型号切换 → 重建 TRDP 动态 Tab
            EventTriggerModel.OnModelNameChanged += OnTrdpModelNameChanged;

            FormClosed += delegate (object s, FormClosedEventArgs e)
            {
                _refreshTimer.Stop();
                _trdpUiTimer?.Stop();
                EventTriggerModel.OnModelNameChanged -= OnTrdpModelNameChanged;
            };
        }

        // ════════════════════════════════════════════════════════════════════
        // AI / AI2 通用模拟量 Tab
        // ════════════════════════════════════════════════════════════════════

        private TabPage BuildAnalogTab(
            string title, List<string> fullTags,
            Action<string, double> inject,
            Dictionary<string, TrackBar> tbDict,
            Dictionary<string, NumericUpDown> nudDict)
        {
            var tp = new TabPage(title) { Padding = new Padding(0) };
            var scroll = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                Padding = new Padding(PAD_X, PAD_Y, PAD_X, PAD_Y)
            };
            tp.Controls.Add(scroll);

            AddTipLabel(scroll, 0,
                string.Format("共 {0} 个点位  ·  拖动滑块或修改数值框后立即注入", fullTags.Count));

            int y = 28;
            foreach (string fullTag in fullTags)
            {
                string key = Regex.Match(fullTag, @"[^.]+$").Value;
                SignalRange r = GetRange(key);

                scroll.Controls.Add(new Label
                {
                    Text = key,
                    Location = new Point(PAD_X, y + 8),
                    Size = new Size(LBL_W, 20),
                    Font = new Font("微软雅黑", 8.5f),
                    ForeColor = Color.FromArgb(50, 50, 70)
                });

                int tbMin = (int)(r.Min * 10);
                int tbMax = (int)(r.Max * 10);
                if (tbMax <= tbMin) tbMax = tbMin + 1;

                var tb = new TrackBar
                {
                    Location = new Point(PAD_X + LBL_W + 4, y + 2),
                    Size = new Size(TB_W, 32),
                    Minimum = tbMin,
                    Maximum = tbMax,
                    TickFrequency = Math.Max(1, (tbMax - tbMin) / 20),
                    SmallChange = 1,
                    LargeChange = Math.Max(1, (tbMax - tbMin) / 10),
                    Value = tbMin
                };

                var nud = new NumericUpDown
                {
                    Location = new Point(PAD_X + LBL_W + TB_W + 8, y + 5),
                    Size = new Size(NUD_W, 24),
                    Minimum = (decimal)r.Min,
                    Maximum = (decimal)r.Max,
                    DecimalPlaces = 1,
                    Increment = 0.5m,
                    Value = (decimal)r.Min
                };

                scroll.Controls.Add(new Label
                {
                    Text = r.Unit,
                    Location = new Point(PAD_X + LBL_W + TB_W + NUD_W + 12, y + 8),
                    Size = new Size(UNIT_W, 20),
                    ForeColor = Color.DimGray,
                    Font = new Font("微软雅黑", 8f)
                });

                scroll.Controls.Add(tb);
                scroll.Controls.Add(nud);

                string capturedKey = key;
                TrackBar capturedTb = tb;
                NumericUpDown capturedNud = nud;
                bool updating = false;

                tb.Scroll += delegate (object s, EventArgs e) {
                    if (updating) return; updating = true;
                    double v = capturedTb.Value / 10.0;
                    capturedNud.Value = (decimal)Math.Round(v, 1);
                    inject(capturedKey, v);
                    updating = false;
                };

                nud.ValueChanged += delegate (object s, EventArgs e) {
                    if (updating) return; updating = true;
                    double v = (double)capturedNud.Value;
                    int tbVal = (int)(v * 10);
                    capturedTb.Value = Math.Max(capturedTb.Minimum, Math.Min(capturedTb.Maximum, tbVal));
                    inject(capturedKey, v);
                    updating = false;
                };

                tbDict[key] = tb;
                nudDict[key] = nud;
                y += ROW_H;
            }
            scroll.AutoScrollMinSize = new Size(0, y + 20);
            return tp;
        }

        // ════════════════════════════════════════════════════════════════════
        // DI Tab
        // ════════════════════════════════════════════════════════════════════

        private TabPage BuildDITab()
        {
            var tp = new TabPage("DI 数字量") { Padding = new Padding(0) };
            var scroll = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                Padding = new Padding(PAD_X, PAD_Y, PAD_X, PAD_Y)
            };
            tp.Controls.Add(scroll);
            AddTipLabel(scroll, 0, "点击切换 0/1  ·  绿色=1(闭合)  红色=0(断开)");

            var defaultOn = new HashSet<string> {
                "盘车连锁开关", "备用电源检测", "发动机DC24V配电", "水阻升降上极限检测" };

            int y = 30, col = 0;
            const int COLS = 2, COL_W = 420, BTN_W = 390;

            foreach (string tag in new DIModulesConfig().Tags)
            {
                bool defVal = defaultOn.Contains(tag);
                var btn = new Button
                {
                    Location = new Point(PAD_X + col * COL_W, y),
                    Size = new Size(BTN_W, 28),
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("微软雅黑", 8.5f),
                    Tag = defVal,
                    Cursor = Cursors.Hand
                };
                btn.FlatAppearance.BorderColor = Color.Silver;
                SetDIStyle(btn, tag, defVal);
                try { Common.DIgrp[tag] = defVal; } catch { }

                string ct = tag; Button cb = btn;
                btn.Click += delegate (object s, EventArgs e) {
                    bool next = !(bool)cb.Tag; cb.Tag = next;
                    SetDIStyle(cb, ct, next);
                    try { Common.DIgrp[ct] = next; }
                    catch (Exception ex)
                    {
                        MessageBox.Show(string.Format("注入失败：{0}\n{1}", ct, ex.Message),
                            "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                };

                scroll.Controls.Add(btn);
                _diCtrl[tag] = btn;
                col++; if (col >= COLS) { col = 0; y += 34; }
            }
            if (col != 0) y += 34;
            y += 10;

            var b1 = MakeBtn("全部置 1（闭合）", new Point(PAD_X, y), C_ON, 160);
            b1.Click += delegate (object s, EventArgs e) { SetAllDI(true); };
            scroll.Controls.Add(b1);

            var b0 = MakeBtn("全部置 0（断开）", new Point(PAD_X + 170, y), C_OFF, 160);
            b0.Click += delegate (object s, EventArgs e) { SetAllDI(false); };
            scroll.Controls.Add(b0);

            scroll.AutoScrollMinSize = new Size(0, y + 50);
            return tp;
        }

        private void SetAllDI(bool val)
        {
            foreach (KeyValuePair<string, Button> kv in _diCtrl)
            {
                kv.Value.Tag = val;
                SetDIStyle(kv.Value, kv.Key, val);
                try { Common.DIgrp[kv.Key] = val; } catch { }
            }
        }

        private void SetDIStyle(Button btn, string tagName, bool val)
        {
            btn.BackColor = val ? C_ON : C_OFF;
            btn.ForeColor = Color.White;
            btn.Text = val
                ? string.Format("● {0}  [1]", tagName)
                : string.Format("○ {0}  [0]", tagName);
        }

        // ════════════════════════════════════════════════════════════════════
        // 串口设备 Tab
        // ════════════════════════════════════════════════════════════════════

        private TabPage BuildSerialTab()
        {
            var tp = new TabPage("串口设备") { Padding = new Padding(0) };
            var pnl = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                Padding = new Padding(PAD_X, PAD_Y, PAD_X, PAD_Y)
            };
            tp.Controls.Add(pnl);
            int y = 0;

            y = AddGroupHeader(pnl, y, "燃油耗仪 ET4500");
            _nudFuelConsumption = AddSerialRow(pnl, ref y, "油耗量", 0, 500, 45.5m, "kg/h",
                delegate (decimal v) {
                    ET4500.Instance.fuelConsumption = (double)v;
                    Common.opcExChangeSendGrp.SetDouble("油耗仪_NoError", 1);
                });
            _nudRemainingFuel = AddSerialRow(pnl, ref y, "剩余油量", 0, 2000, 350m, "kg",
                delegate (decimal v) { ET4500.Instance.remainingFuel = (double)v; });
            _nudFuelPct = AddSerialRow(pnl, ref y, "油量百分比", 0, 100, 70m, "%",
                delegate (decimal v) { ET4500.Instance.fuelPercentage = (double)v; });

            y += 8;
            var btnET = MakeBtn("启用 ET4500 仿真模式（跳过串口）", new Point(PAD_X, y), C_HEADER, 280);
            btnET.Click += delegate (object s, EventArgs e) {
                ET4500.Instance.SimulateMode();
                _lblET4500Status.Text = "ET4500：仿真模式 ✓";
                _lblET4500Status.ForeColor = Color.ForestGreen;
            };
            pnl.Controls.Add(btnET);

            _lblET4500Status = new Label
            {
                Text = "ET4500：未启用仿真",
                Location = new Point(PAD_X + 290, y + 6),
                Size = new Size(280, 20),
                ForeColor = Color.Gray,
                Font = new Font("微软雅黑", 8.5f)
            };
            pnl.Controls.Add(_lblET4500Status);
            y += 38;

            y += 8;
            y = AddGroupHeader(pnl, y, "称重仪 ZMPT650F（机油消耗磅秤）");
            _nudWeight = AddSerialRow(pnl, ref y, "当前重量", 0, 9999, 25m, "kg",
                delegate (decimal v) {
                    ZMPT650F.Instance.Weight = (double)v;
                    MiddleData.instnce.PTFWeight = (double)v;
                    Common.opcExChangeSendGrp.SetDouble("重量", (double)v);
                    Common.opcExChangeSendGrp.SetDouble("称重仪_NoError", 1);
                });

            y += 8;
            var btnZM = MakeBtn("启用 ZMPT650F 仿真模式（跳过串口）", new Point(PAD_X, y), C_HEADER, 280);
            btnZM.Click += delegate (object s, EventArgs e) {
                ZMPT650F.Instance.SimulateMode();
                _lblZMPTStatus.Text = "ZMPT650F：仿真模式 ✓";
                _lblZMPTStatus.ForeColor = Color.ForestGreen;
            };
            pnl.Controls.Add(btnZM);

            _lblZMPTStatus = new Label
            {
                Text = "ZMPT650F：未启用仿真",
                Location = new Point(PAD_X + 290, y + 6),
                Size = new Size(280, 20),
                ForeColor = Color.Gray,
                Font = new Font("微软雅黑", 8.5f)
            };
            pnl.Controls.Add(_lblZMPTStatus);

            return tp;
        }

        // ════════════════════════════════════════════════════════════════════
        // 工况预设 Tab
        // ════════════════════════════════════════════════════════════════════

        private TabPage BuildPresetTab()
        {
            var tp = new TabPage("工况预设") { Padding = new Padding(0) };
            var pnl = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                Padding = new Padding(16, 12, 16, 12),
            };
            tp.Controls.Add(pnl);

            int y = 0;

            // ── 说明 ─────────────────────────────────────────────────────────
            pnl.Controls.Add(new Label
            {
                Text = "一键注入标准工况模拟值，所有数据源同时更新（AIgrp / AI2Grp / waterGrp / engineOilGrp / TRDP / ET4500 / ZMPT650F）",
                Location = new Point(0, y),
                Size = new Size(820, 36),
                Font = new Font("微软雅黑", 8.5f),
                ForeColor = Color.DimGray,
            });
            y += 44;

            // ── 状态栏 ───────────────────────────────────────────────────────
            var lblStatus = new Label
            {
                Text = "尚未注入任何预设",
                Location = new Point(0, y),
                Size = new Size(820, 24),
                Font = new Font("微软雅黑", 9f, FontStyle.Bold),
                ForeColor = Color.DimGray,
            };
            pnl.Controls.Add(lblStatus);
            y += 32;

            // ── 预设按钮组 ───────────────────────────────────────────────────
            y = AddPresetGroup(pnl, y, lblStatus, "100h 性能试验", new[]
            {
                new PresetBtnDef("标定功率工况\n100% load · 1000rpm · 3000N.m",
                    Color.FromArgb(30, 100, 60),
                    delegate { SimulatePresetService.Instance.ApplyRatedPower(); }),

                new PresetBtnDef("超负荷工况\n110% load · 1000rpm · 3300N.m",
                    Color.FromArgb(140, 60, 20),
                    delegate { SimulatePresetService.Instance.ApplyOverload(); }),

                new PresetBtnDef("部分负荷工况\n75% load · 1000rpm · 2250N.m",
                    Color.FromArgb(30, 80, 140),
                    delegate { SimulatePresetService.Instance.ApplyPartialLoad(); }),

                new PresetBtnDef("交变负荷 - 高载\n标定功率 1000rpm",
                    Color.FromArgb(80, 50, 130),
                    delegate { SimulatePresetService.Instance.ApplyAlternatingLoad(true); }),

                new PresetBtnDef("交变负荷 - 空载\n700rpm · 0N.m",
                    Color.FromArgb(80, 50, 130),
                    delegate { SimulatePresetService.Instance.ApplyAlternatingLoad(false); }),
            });

            y += 8;
            y = AddPresetGroup(pnl, y, lblStatus, "360h 耐久性试验", new[]
            {
                new PresetBtnDef("第Ⅰ阶段 典型工况\n630rpm · 50%扭矩",
                    Color.FromArgb(40, 90, 100),
                    delegate { SimulatePresetService.Instance.ApplyEndurance1(); }),

                new PresetBtnDef("第Ⅱ阶段 典型工况\n520rpm · 18%扭矩",
                    Color.FromArgb(40, 90, 100),
                    delegate { SimulatePresetService.Instance.ApplyEndurance2(); }),

                new PresetBtnDef("第Ⅲ阶段 长时标定\n1000rpm · 100%扭矩",
                    Color.FromArgb(40, 90, 100),
                    delegate { SimulatePresetService.Instance.ApplyEndurance3(); }),
            });

            y += 8;
            y = AddPresetGroup(pnl, y, lblStatus, "其他", new[]
            {
                new PresetBtnDef("怠速工况\n启机成功后稳定状态 450rpm",
                    Color.FromArgb(90, 90, 90),
                    delegate { SimulatePresetService.Instance.ApplyIdle(); }),
            });

            // ── 注入后实时显示计算值 ─────────────────────────────────────────
            y += 16;
            pnl.Controls.Add(new Label
            {
                Text = "注入后预计记录值（AutoRecordPara）",
                Location = new Point(0, y),
                Size = new Size(400, 20),
                Font = new Font("微软雅黑", 9f, FontStyle.Bold),
                ForeColor = Color.FromArgb(45, 85, 130),
            });
            y += 26;

            var lblCalc = new Label
            {
                Text = "（点击预设按钮后在此显示）",
                Location = new Point(0, y),
                Size = new Size(820, 80),
                Font = new Font("Consolas", 9f),
                ForeColor = Color.FromArgb(40, 100, 40),
            };
            pnl.Controls.Add(lblCalc);

            // 每隔 500ms 刷新计算值显示
            var calcTimer = new System.Windows.Forms.Timer { Interval = 500 };
            calcTimer.Tick += delegate (object s, EventArgs e)
            {
                try
                {
                    double rpm = MiddleData.instnce.EngineSpeed;
                    double torque = MiddleData.instnce.EngineTorque;
                    double power = MiddleData.instnce.EnginePower;
                    double fuel = ET4500.Instance.fuelConsumption;
                    double weight = ZMPT650F.Instance.Weight;
                    lblCalc.Text = string.Format(
                        "RPM={0:F0}  Torque={1:F1}N.m  Power={2:F1}kW  燃油耗={3:F1}kg/h  磅秤={4:F1}kg",
                        rpm, torque, power, fuel, weight);
                }
                catch { }
            };
            calcTimer.Start();

            FormClosed += delegate (object s, FormClosedEventArgs e) { calcTimer.Stop(); };

            pnl.AutoScrollMinSize = new Size(0, y + 120);
            return tp;
        }

        // ── 预设按钮组辅助方法 ────────────────────────────────────────────────

        private int AddPresetGroup(Panel parent, int y, Label lblStatus,
            string groupTitle, PresetBtnDef[] defs)
        {
            parent.Controls.Add(new Label
            {
                Text = groupTitle,
                Location = new Point(0, y),
                Size = new Size(600, 22),
                Font = new Font("微软雅黑", 9.5f, FontStyle.Bold),
                ForeColor = Color.FromArgb(45, 85, 130),
            });
            y += 28;

            int col = 0;
            const int BTN_W = 196;
            const int BTN_H = 58;
            const int GAP = 8;

            foreach (PresetBtnDef def in defs)
            {
                int x = col * (BTN_W + GAP);

                var btn = new Button
                {
                    Text = def.Label,
                    Location = new Point(x, y),
                    Size = new Size(BTN_W, BTN_H),
                    FlatStyle = FlatStyle.Flat,
                    BackColor = def.Color,
                    ForeColor = Color.White,
                    Font = new Font("微软雅黑", 8.5f),
                    Cursor = Cursors.Hand,
                    TextAlign = ContentAlignment.MiddleCenter,
                };
                btn.FlatAppearance.BorderSize = 0;

                Action captured = def.Action;
                Label capturedLbl = lblStatus;
                string capturedTitle = def.Label.Split('\n')[0];

                btn.Click += delegate (object s, EventArgs e)
                {
                    try
                    {
                        captured();
                        SyncControlsToCurrentValues();
                        capturedLbl.Text = string.Format("✓ 已注入：{0}  [{1}]",
                            capturedTitle, DateTime.Now.ToString("HH:mm:ss"));
                        capturedLbl.ForeColor = Color.ForestGreen;
                    }
                    catch (Exception ex)
                    {
                        capturedLbl.Text = "✗ 注入失败：" + ex.Message;
                        capturedLbl.ForeColor = Color.Firebrick;
                    }
                };

                parent.Controls.Add(btn);
                col++;
                if (col >= 4) { col = 0; y += BTN_H + GAP; }
            }

            if (col != 0) y += BTN_H + GAP;
            return y;
        }

        /// <summary>
        /// 把当前 AIgrp/AI2Grp 的实际值同步回界面滑块和数值框
        /// </summary>
        private void SyncControlsToCurrentValues()
        {
            // ── AI（PLC1）────────────────────────────────────────────────────────
            foreach (KeyValuePair<string, TrackBar> kv in _aiTb)
            {
                try
                {
                    double val = Common.AIgrp[kv.Key];
                    int tbVal = (int)(val * 10);
                    kv.Value.Value = Math.Max(kv.Value.Minimum,
                                     Math.Min(kv.Value.Maximum, tbVal));
                    if (_aiNud.ContainsKey(kv.Key))
                        _aiNud[kv.Key].Value = (decimal)Math.Round(val, 1);
                }
                catch { }
            }

            // ── AI2（PLC2）───────────────────────────────────────────────────────
            foreach (KeyValuePair<string, TrackBar> kv in _ai2Tb)
            {
                try
                {
                    double val = Common.AI2Grp[kv.Key];
                    int tbVal = (int)(val * 10);
                    kv.Value.Value = Math.Max(kv.Value.Minimum,
                                     Math.Min(kv.Value.Maximum, tbVal));
                    if (_ai2Nud.ContainsKey(kv.Key))
                        _ai2Nud[kv.Key].Value = (decimal)Math.Round(val, 1);
                }
                catch { }
            }

            // ── TRDP 模拟量控件（_trdpAnalogControls）────────────────────────────
            foreach (KeyValuePair<string, TrdpAnalogEntry> kv in _trdpAnalogControls)
            {
                try
                {
                    // 从 TRDP 字典读当前注入值
                    decimal val = Var.TRDP.trdpValue.ContainsKey(kv.Key)
                        ? Var.TRDP.trdpValue[kv.Key]
                        : 0m;

                    var entry = kv.Value;
                    int tbVal = (int)val;
                    entry.TrackBar.Value = Math.Max(entry.TrackBar.Minimum,
                                           Math.Min(entry.TrackBar.Maximum, tbVal));
                    entry.ValueLabel.Text = val.ToString("F1") + " ";   // 单位已在 label 里
                    if (val >= entry.Nud.Minimum && val <= entry.Nud.Maximum)
                        entry.Nud.Value = val;
                }
                catch { }
            }
        }

        // ── 预设按钮描述（内部结构体）─────────────────────────────────────────

        private class PresetBtnDef
        {
            public string Label { get; private set; }
            public Color Color { get; private set; }
            public Action Action { get; private set; }

            public PresetBtnDef(string label, Color color, Action action)
            {
                Label = label;
                Color = color;
                Action = action;
            }
        }

        // ════════════════════════════════════════════════════════════════════
        // TRDP 内嵌 Tab（完整功能，与 frmTRDPSimulator 并存互不干扰）
        // ════════════════════════════════════════════════════════════════════

        private TabPage BuildTRDPEmbeddedTab()
        {
            var tp = new TabPage("TRDP（柴油机控制器）") { Padding = new Padding(0) };

            // ── 顶部工具栏 ───────────────────────────────────────────────────
            var pnlTop = new Panel
            {
                Dock = DockStyle.Top,
                Height = 50,
                BackColor = Color.FromArgb(30, 56, 100),
                Padding = new Padding(10, 0, 10, 0)
            };

            var lblTitle = new Label
            {
                Text = "TRDP 模拟控制台",
                AutoSize = true,
                Font = new Font("微软雅黑", 12f, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(12, 13)
            };

            _trdpLblStatus = new Label
            {
                Text = "● 未启动",
                AutoSize = true,
                Font = new Font("微软雅黑", 9f),
                ForeColor = Color.FromArgb(180, 200, 180),
                Location = new Point(220, 17)
            };

            _trdpLblTick = new Label
            {
                Text = "帧: 0",
                AutoSize = true,
                Font = new Font("Consolas", 9f),
                ForeColor = Color.FromArgb(160, 200, 160),
                Location = new Point(330, 17)
            };

            var btnStart = TrdpTopBtn("▶ 启动", Color.FromArgb(40, 130, 60), new Point(430, 10));
            var btnStop = TrdpTopBtn("■ 停止", Color.FromArgb(160, 50, 50), new Point(518, 10));
            var btnSync = TrdpTopBtn("⟳ 同步", Color.FromArgb(60, 100, 160), new Point(606, 10));
            btnSync.Width = 90;

            btnStart.Click += delegate (object s, EventArgs e) {
                TRDPSimulatorService.Instance.Start(); _trdpUiTimer.Start();
            };
            btnStop.Click += delegate (object s, EventArgs e) {
                TRDPSimulatorService.Instance.Stop(); _trdpUiTimer.Stop();
            };
            btnSync.Click += delegate (object s, EventArgs e) { TrdpRebuildDynamicTabs(); };

            pnlTop.Controls.AddRange(new Control[] {
                lblTitle, _trdpLblStatus, _trdpLblTick, btnStart, btnStop, btnSync });

            // ── 底部日志 ─────────────────────────────────────────────────────
            var pnlLog = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 140,
                BackColor = Color.FromArgb(25, 25, 30)
            };

            _trdpRtbLog = new RichTextBox
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                Font = new Font("Consolas", 8.5f),
                BackColor = Color.FromArgb(30, 30, 35),
                ForeColor = Color.FromArgb(200, 200, 200),
                BorderStyle = BorderStyle.None,
                ScrollBars = RichTextBoxScrollBars.Vertical
            };

            var lblLogHdr = new Label
            {
                Text = "  运行日志",
                Dock = DockStyle.Top,
                Height = 20,
                Font = new Font("微软雅黑", 8f, FontStyle.Bold),
                ForeColor = Color.FromArgb(140, 160, 180)
            };

            var btnClrLog = new Button
            {
                Text = "清空",
                Dock = DockStyle.Right,
                Width = 50,
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.Gray,
                Font = new Font("微软雅黑", 8f)
            };
            btnClrLog.Click += delegate (object s, EventArgs e) { _trdpRtbLog.Clear(); };

            pnlLog.Controls.Add(_trdpRtbLog);
            pnlLog.Controls.Add(lblLogHdr);
            pnlLog.Controls.Add(btnClrLog);

            // ── 中间 SubTabControl ───────────────────────────────────────────
            _trdpTpAnalog = new TabPage("模拟量");
            _trdpTpDigital = new TabPage("数字量（故障位）");
            _trdpTpAxis = new TabPage("轴温");
            _trdpTpStatus = new TabPage("通讯状态");

            // 通讯状态 Tab 静态内容
            var pnlSt = new Panel { Dock = DockStyle.Fill, AutoScroll = true };
            pnlSt.Controls.Add(new Label
            {
                AutoSize = true,
                Location = new Point(8, 10),
                Font = new Font("微软雅黑", 9f, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 56, 100),
                Text = "设备生命信号（D01）  [U16, 0-255, 周期递增]"
            });

            _trdpLblLifeVal = new Label
            {
                AutoSize = true,
                Location = new Point(8, 40),
                Font = new Font("微软雅黑", 9f, FontStyle.Bold),
                ForeColor = Color.FromArgb(20, 80, 160),
                Text = "生命信号值：0"
            };

            _trdpBtnLifeAuto = new Button
            {
                Location = new Point(200, 36),
                Size = new Size(170, 28),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(40, 120, 60),
                ForeColor = Color.White,
                Font = new Font("微软雅黑", 9f),
                Text = "● 自动递增（已启用）",
                Cursor = Cursors.Hand
            };
            _trdpBtnLifeAuto.Click += delegate (object s, EventArgs e) {
                _trdpLifeAuto = !_trdpLifeAuto;
                TRDPSimulatorService.Instance.SetLifeAuto(_trdpLifeAuto);
                _trdpBtnLifeAuto.BackColor = _trdpLifeAuto
                    ? Color.FromArgb(40, 120, 60) : Color.FromArgb(160, 80, 20);
                _trdpBtnLifeAuto.Text = _trdpLifeAuto
                    ? "● 自动递增（已启用）" : "○ 自动递增（已停止）";
            };

            pnlSt.Controls.Add(_trdpLblLifeVal);
            pnlSt.Controls.Add(_trdpBtnLifeAuto);
            _trdpTpStatus.Controls.Add(pnlSt);

            var subTab = new TabControl
            {
                Dock = DockStyle.Fill,
                Font = new Font("微软雅黑", 9f)
            };
            subTab.TabPages.Add(_trdpTpAnalog);
            subTab.TabPages.Add(_trdpTpDigital);
            subTab.TabPages.Add(_trdpTpAxis);
            subTab.TabPages.Add(_trdpTpStatus);

            // ── 控件添加顺序：Fill → Bottom → Top（WinForms Dock 布局规则）──
            // Fill 最先加（索引最低，最后处理，填充剩余空间）
            // Bottom 次之
            // Top 最后加（索引最高，最先处理，优先占顶部）
            tp.Controls.Add(subTab);
            tp.Controls.Add(pnlLog);
            tp.Controls.Add(pnlTop);

            // ── 定时器 ───────────────────────────────────────────────────────
            _trdpUiTimer = new System.Windows.Forms.Timer { Interval = 1000 };
            _trdpUiTimer.Tick += delegate (object s, EventArgs e) {
                var svc = TRDPSimulatorService.Instance;
                _trdpLblTick.Text = string.Format("帧: {0}", svc.TickCount);
                _trdpLblStatus.Text = svc.IsRunning ? "● 运行中" : "● 已停止";
                _trdpLblStatus.ForeColor = svc.IsRunning
                    ? Color.FromArgb(100, 230, 120) : Color.FromArgb(220, 100, 100);
                _trdpLblLifeVal.Text = string.Format("生命信号值：{0}", svc.LifeCounter);
            };

            // ── 绑定日志回调 ─────────────────────────────────────────────────
            TRDPSimulatorService.Instance.OnLog += delegate (string msg, TRDPSimulatorService.LogLevel level) {
                if (IsDisposed || !IsHandleCreated) return;
                try { Invoke(new Action(delegate { TrdpAppendLog(msg, level); })); } catch { }
            };

            // ── 初次构建动态 Tab ─────────────────────────────────────────────
            TrdpRebuildDynamicTabs();

            return tp;
        }

        // ════════════════════════════════════════════════════════════════════
        // TRDP 动态 Tab 重建（对应 frmTRDPSimulator.RebuildDynamicTabs）
        // ════════════════════════════════════════════════════════════════════

        private void OnTrdpModelNameChanged(string modelName)
        {
            if (IsDisposed || !IsHandleCreated) return;
            try { Invoke(new Action(TrdpRebuildDynamicTabs)); } catch { }
        }

        private void TrdpRebuildDynamicTabs()
        {
            var tags = TrdpGetCurrentTags();

            TrdpClearTabContent(_trdpTpAnalog);
            TrdpClearTabContent(_trdpTpDigital);
            TrdpClearTabContent(_trdpTpAxis);
            _trdpAnalogControls.Clear();
            _trdpDigitalButtons.Clear();

            if (tags.Count == 0)
            {
                TrdpAppendLog("当前未加载任何 TRDP 信号，请先在主界面选择型号。",
                    TRDPSimulatorService.LogLevel.Warn);
                return;
            }

            var analogTags = tags.Where(t => TrdpIsAnalogType(t.DataType)).ToList();
            var digitalTags = tags.Where(t => t.DataType == "B1").ToList();
            var axisTags = tags.Where(t => t.DataLabel != null
                                           && t.DataLabel.Contains("轴温")).ToList();

            TrdpBuildAnalogTab(analogTags);
            TrdpBuildDigitalTab(digitalTags);
            TrdpBuildAxisTab(axisTags);

            TrdpAppendLog(
                string.Format("已同步 {0} 个信号（模拟量:{1} / 数字量:{2} / 轴温:{3}）",
                    tags.Count, analogTags.Count, digitalTags.Count, axisTags.Count),
                TRDPSimulatorService.LogLevel.OK);
        }

        // ── 模拟量 Tab ───────────────────────────────────────────────────────

        private void TrdpBuildAnalogTab(List<FullTags> tags)
        {
            var panel = TrdpMakeScrollPanel(); int y = 8;
            if (tags.Count == 0)
            {
                panel.Controls.Add(TrdpMakeLabel("当前型号无模拟量信号。", 9, Color.Gray, pt: new Point(8, 8)));
                _trdpTpAnalog.Controls.Add(panel); return;
            }

            TrdpAddSectionHeader(panel, ref y,
                string.Format("共 {0} 个模拟量信号（物理值范围来自协议定义）", tags.Count));

            foreach (var tag in tags)
            {
                if (tag.DataLabel != null && tag.DataLabel.Contains("轴温")) continue;
                TrdpAddAnalogRow(panel, ref y, tag, tag.DataUnit ?? "");
            }
            _trdpTpAnalog.Controls.Add(panel);
        }

        private void TrdpAddAnalogRow(Panel parent, ref int y, FullTags tag, string unit)
        {
            string sigName = tag.DataLabel;
            decimal scale = tag.dataFormat <= 0m ? 1m : tag.dataFormat;
            int decPlaces = scale >= 1m ? 0 : (scale >= 0.1m ? 1 : 2);
            decimal engMax = TrdpGetPhysicalMax(sigName, unit, scale);
            decimal engMin = 0m;
            int tbMax = (int)engMax; if (tbMax < 1) tbMax = 1;

            var lblName = TrdpMakeLabel(
                string.Format("{0}  [offset:{1} {2} ×{3}  max:{4}]",
                    sigName, tag.Offset, tag.DataType, scale, engMax),
                8.5f, Color.FromArgb(60, 60, 80), pt: new Point(8, y + 4));
            lblName.Size = new Size(290, 18);

            var tb = new TrackBar
            {
                Location = new Point(302, y),
                Size = new Size(340, 30),
                Minimum = 0,
                Maximum = tbMax,
                Value = 0,
                TickFrequency = Math.Max(1, tbMax / 20),
                SmallChange = 1,
                LargeChange = Math.Max(1, tbMax / 10)
            };

            var lblVal = TrdpMakeLabel(string.Format("0 {0}", unit),
                9f, Color.FromArgb(20, 80, 160), bold: true, pt: new Point(650, y + 5));
            lblVal.Size = new Size(95, 18);
            lblVal.TextAlign = ContentAlignment.MiddleRight;

            var nud = new NumericUpDown
            {
                Location = new Point(752, y + 4),
                Size = new Size(88, 22),
                Minimum = engMin,
                Maximum = engMax,
                DecimalPlaces = decPlaces,
                Increment = scale < 1m ? scale : 1m,
                Value = engMin,
                Font = new Font("Consolas", 9f)
            };

            bool syncing = false;
            string captured = sigName;
            Label capturedLbl = lblVal;
            TrackBar capturedTb = tb;
            NumericUpDown capturedNud = nud;

            tb.ValueChanged += delegate (object s, EventArgs e) {
                if (syncing) return; syncing = true;
                try
                {
                    decimal v = capturedTb.Value;
                    capturedLbl.Text = string.Format("{0} {1}", v.ToString("F" + decPlaces), unit);
                    if (v >= capturedNud.Minimum && v <= capturedNud.Maximum) capturedNud.Value = v;
                    TRDPSimulatorService.Instance.InjectValue(captured, v);
                }
                finally { syncing = false; }
            };

            nud.ValueChanged += delegate (object s, EventArgs e) {
                if (syncing) return; syncing = true;
                try
                {
                    decimal v = capturedNud.Value;
                    capturedLbl.Text = string.Format("{0} {1}", v.ToString("F" + decPlaces), unit);
                    int tbV = (int)v;
                    if (tbV >= capturedTb.Minimum && tbV <= capturedTb.Maximum) capturedTb.Value = tbV;
                    TRDPSimulatorService.Instance.InjectValue(captured, v);
                }
                finally { syncing = false; }
            };

            parent.Controls.AddRange(new Control[] { lblName, tb, lblVal, nud });
            _trdpAnalogControls[sigName] = new TrdpAnalogEntry
            { TrackBar = tb, ValueLabel = lblVal, Nud = nud };
            y += 34;
        }

        // ── 数字量 Tab ───────────────────────────────────────────────────────

        private void TrdpBuildDigitalTab(List<FullTags> tags)
        {
            var panel = TrdpMakeScrollPanel(); int y = 8;
            if (tags.Count == 0)
            {
                panel.Controls.Add(TrdpMakeLabel("当前型号无数字量（B1）信号。", 9, Color.Gray, pt: new Point(8, 8)));
                _trdpTpDigital.Controls.Add(panel); return;
            }

            TrdpAddSectionHeader(panel, ref y,
                string.Format("共 {0} 个数字量信号（B1），点击按钮切换0/1注入", tags.Count));

            foreach (var tag in tags) TrdpAddDigitalRow(panel, ref y, tag);

            y += 6;
            var btnReset = new Button
            {
                Text = "全部清零（置0）",
                Location = new Point(8, y),
                Size = new Size(160, 28),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(140, 140, 140),
                ForeColor = Color.White,
                Font = new Font("微软雅黑", 9f),
                Cursor = Cursors.Hand
            };
            btnReset.Click += delegate (object s, EventArgs e) { TrdpResetAllDigital(); };
            panel.Controls.Add(btnReset);

            _trdpTpDigital.Controls.Add(panel);
        }

        private void TrdpAddDigitalRow(Panel parent, ref int y, FullTags tag)
        {
            string sigName = tag.DataLabel;
            bool isSyncSig = sigName != null && sigName.Contains("同步状态");
            bool defState = isSyncSig;

            var lblName = TrdpMakeLabel(
                string.Format("{0}  [offset:{1} bit:{2}]", sigName, tag.Offset, tag.Bit),
                8.5f, Color.FromArgb(60, 60, 80), pt: new Point(8, y + 6));
            lblName.Size = new Size(280, 18);

            var btn = new Button
            {
                Location = new Point(296, y),
                Size = new Size(160, 26),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("微软雅黑", 8.5f),
                Cursor = Cursors.Hand,
                Tag = defState ? 1 : 0
            };
            btn.FlatAppearance.BorderColor = Color.Gray;
            TrdpApplyDigitalStyle(btn, defState ? 1 : 0, sigName);
            TRDPSimulatorService.Instance.InjectValue(sigName, defState ? 1m : 0m);

            string cs = sigName; Button cb = btn;
            btn.Click += delegate (object s, EventArgs e) {
                int next = (int)cb.Tag == 0 ? 1 : 0;
                cb.Tag = next;
                TrdpApplyDigitalStyle(cb, next, cs);
                TRDPSimulatorService.Instance.InjectValue(cs, (decimal)next);
            };

            _trdpDigitalButtons[sigName] = btn;
            parent.Controls.AddRange(new Control[] { lblName, btn });
            y += 34;
        }

        private void TrdpApplyDigitalStyle(Button btn, int val, string sigName)
        {
            bool isSyncSig = sigName != null && sigName.Contains("同步状态");
            if (isSyncSig)
            {
                btn.BackColor = val == 1 ? Color.FromArgb(40, 130, 60) : Color.FromArgb(160, 50, 50);
                btn.ForeColor = Color.White;
                btn.Text = val == 1 ? "1  已同步（正常）" : "0  未同步（异常）";
            }
            else
            {
                bool isFault = val == 1;
                btn.BackColor = isFault ? C_FAULT : C_NORMAL_D;
                btn.ForeColor = isFault ? Color.FromArgb(160, 0, 0) : Color.FromArgb(30, 80, 30);
                btn.Text = isFault ? "1  ▲ 故障" : "0  正常";
            }
        }

        private void TrdpResetAllDigital()
        {
            foreach (KeyValuePair<string, Button> kv in _trdpDigitalButtons)
            {
                bool isSyncSig = kv.Key != null && kv.Key.Contains("同步状态");
                int defVal = isSyncSig ? 1 : 0;
                kv.Value.Tag = defVal;
                TrdpApplyDigitalStyle(kv.Value, defVal, kv.Key);
                TRDPSimulatorService.Instance.InjectValue(kv.Key, (decimal)defVal);
            }
        }

        // ── 轴温 Tab ─────────────────────────────────────────────────────────

        private void TrdpBuildAxisTab(List<FullTags> tags)
        {
            var panel = TrdpMakeScrollPanel(); int y = 8;
            if (tags.Count == 0)
            {
                panel.Controls.Add(TrdpMakeLabel(
                    "当前型号未检测到包含 [轴温] 的信号。", 9, Color.Gray, pt: new Point(8, 8)));
                _trdpTpAxis.Controls.Add(panel); return;
            }

            TrdpAddSectionHeader(panel, ref y,
                string.Format("共 {0} 个轴温信号（来自当前型号 Excel）", tags.Count));
            TrdpAddSectionHeader(panel, ref y,
                "降载阈值≥108℃  停机阈值≥125℃",
                headerColor: Color.Gray, fontSize: 8f);

            foreach (var tag in tags) TrdpAddAxisRow(panel, ref y, tag);
            _trdpTpAxis.Controls.Add(panel);
        }

        private void TrdpAddAxisRow(Panel parent, ref int y, FullTags tag)
        {
            string sigName = tag.DataLabel;
            int defTemp = 70;
            decimal scale = tag.dataFormat == 0 ? 0.1m : tag.dataFormat;

            var lblName = TrdpMakeLabel(
                string.Format("{0}  [offset:{1}  ×{2}]", sigName, tag.Offset, scale),
                9f, Color.FromArgb(30, 56, 100), bold: true, pt: new Point(8, y + 8));
            lblName.AutoSize = true;

            var tb = new TrackBar
            {
                Location = new Point(240, y),
                Size = new Size(450, 30),
                Minimum = 20,
                Maximum = 160,
                Value = defTemp,
                TickFrequency = 10,
                SmallChange = 1,
                LargeChange = 5
            };

            var lblVal = TrdpMakeLabel(defTemp + ".0 ℃", 10f, Color.FromArgb(20, 80, 160),
                bold: true, pt: new Point(698, y + 5));
            lblVal.Size = new Size(90, 20);
            lblVal.TextAlign = ContentAlignment.MiddleLeft;

            var lblWarn = TrdpMakeLabel("", 8f, Color.FromArgb(160, 40, 40), pt: new Point(794, y + 7));
            lblWarn.AutoSize = true;

            var nud = new NumericUpDown
            {
                Location = new Point(890, y + 4),
                Size = new Size(75, 22),
                Minimum = 20,
                Maximum = 160,
                DecimalPlaces = 1,
                Increment = 0.5m,
                Value = defTemp,
                Font = new Font("Consolas", 9f)
            };

            bool syncing = false;
            string captured = sigName;
            TrackBar capturedTb = tb;
            NumericUpDown capturedNud = nud;
            Label capturedLbl = lblVal;
            Label capturedWarn = lblWarn;

            Action<decimal> updateColor = delegate (decimal v) {
                bool isStop = v >= 125; bool isShed = v >= 108 && v < 125;
                capturedLbl.ForeColor = isStop ? Color.FromArgb(180, 20, 20)
                    : isShed ? Color.FromArgb(160, 90, 0) : Color.FromArgb(20, 80, 160);
                capturedWarn.Text = isStop ? "▲ 超停机阈值" : isShed ? "△ 超降载阈值" : "";
            };

            tb.ValueChanged += delegate (object s, EventArgs e) {
                if (syncing) return; syncing = true;
                decimal v = capturedTb.Value;
                capturedLbl.Text = v.ToString("F1") + " ℃";
                if (v >= capturedNud.Minimum && v <= capturedNud.Maximum) capturedNud.Value = v;
                updateColor(v);
                TRDPSimulatorService.Instance.InjectValue(captured, v);
                syncing = false;
            };

            nud.ValueChanged += delegate (object s, EventArgs e) {
                if (syncing) return; syncing = true;
                int tbV = (int)capturedNud.Value;
                if (tbV >= capturedTb.Minimum && tbV <= capturedTb.Maximum) capturedTb.Value = tbV;
                capturedLbl.Text = capturedNud.Value.ToString("F1") + " ℃";
                updateColor(capturedNud.Value);
                TRDPSimulatorService.Instance.InjectValue(captured, capturedNud.Value);
                syncing = false;
            };

            parent.Controls.AddRange(new Control[] { lblName, tb, lblVal, lblWarn, nud });
            y += 38;
        }

        // ── TRDP 日志 ────────────────────────────────────────────────────────

        private void TrdpAppendLog(string msg, TRDPSimulatorService.LogLevel level)
        {
            if (_trdpRtbLog == null || _trdpRtbLog.IsDisposed) return;
            Color col = level == TRDPSimulatorService.LogLevel.OK
                ? Color.FromArgb(80, 200, 120)
                : level == TRDPSimulatorService.LogLevel.Fault
                    ? Color.FromArgb(255, 100, 100)
                    : level == TRDPSimulatorService.LogLevel.Warn
                        ? Color.FromArgb(255, 210, 80)
                        : Color.FromArgb(180, 180, 180);

            string line = string.Format("[{0}] {1}\n", DateTime.Now.ToString("HH:mm:ss"), msg);
            _trdpRtbLog.SelectionStart = _trdpRtbLog.TextLength;
            _trdpRtbLog.SelectionLength = 0;
            _trdpRtbLog.SelectionColor = col;
            _trdpRtbLog.AppendText(line);
            _trdpRtbLog.ScrollToCaret();
            if (_trdpRtbLog.Lines.Length > 500)
            {
                _trdpRtbLog.Select(0, _trdpRtbLog.GetFirstCharIndexFromLine(100));
                _trdpRtbLog.SelectedText = "";
            }
        }

        // ── TRDP 静态辅助方法（均加 Trdp 前缀，不与 frmTRDPSimulator 冲突）──

        private static List<FullTags> TrdpGetCurrentTags()
        {
            try
            {
                if (Var.TRDP == null || Var.TRDP.tags == null) return new List<FullTags>();
                return Var.TRDP.tags.Where(t => t != null && !string.IsNullOrEmpty(t.DataLabel)).ToList();
            }
            catch { return new List<FullTags>(); }
        }

        private static bool TrdpIsAnalogType(string dataType)
        {
            return dataType == "U16" || dataType == "I16" || dataType == "U32"
                || dataType == "I32" || dataType == "F32" || dataType == "U8" || dataType == "I8";
        }

        private static decimal TrdpGetPhysicalMax(string sigName, string unit, decimal scale)
        {
            if (sigName == null) sigName = "";
            if (unit == null) unit = "";
            if (sigName.Contains("增压器转速")) return 60000m;
            if (sigName.Contains("转速") || sigName.Contains("相位传感器")) return 1500m;
            if (sigName.Contains("排气温度") || sigName.Contains("涡前排气")) return 1000m;
            if (sigName.Contains("温度") || unit.Contains("℃")) return 350m;
            if (sigName.Contains("电源")) return 36m;
            if (sigName.Contains("压力") || unit.Contains("kPa")) return 2500m;
            if (sigName.Contains("燃油量")) return 120m;
            if (sigName.Contains("提前角")) return 25m;
            if (sigName.Contains("运行时间")) return 9999m;
            if (scale >= 1m) return 2000m;
            if (scale >= 0.1m) return 500m;
            return 100m;
        }

        private static void TrdpClearTabContent(TabPage tp)
        {
            foreach (Control c in tp.Controls) { c.Controls.Clear(); c.Dispose(); }
            tp.Controls.Clear();
        }

        private static Panel TrdpMakeScrollPanel()
            => new Panel { Dock = DockStyle.Fill, AutoScroll = true };

        private static Label TrdpMakeLabel(string text, float fontSize,
            Color color, bool bold = false, Point? pt = null)
        {
            var lbl = new Label
            {
                Text = text,
                AutoSize = true,
                Font = new Font("微软雅黑", fontSize, bold ? FontStyle.Bold : FontStyle.Regular),
                ForeColor = color
            };
            if (pt.HasValue) lbl.Location = pt.Value;
            return lbl;
        }

        private static void TrdpAddSectionHeader(Panel panel, ref int y, string title,
            Color? headerColor = null, float fontSize = 8f)
        {
            panel.Controls.Add(new Label
            {
                Text = title,
                AutoSize = true,
                Font = new Font("微软雅黑", fontSize, FontStyle.Bold),
                ForeColor = headerColor ?? Color.FromArgb(30, 56, 100),
                Location = new Point(6, y)
            });
            y += 22;
        }

        private static Button TrdpTopBtn(string text, Color back, Point loc)
        {
            var btn = new Button
            {
                Text = text,
                Location = loc,
                Size = new Size(80, 30),
                FlatStyle = FlatStyle.Flat,
                BackColor = back,
                ForeColor = Color.White,
                Font = new Font("微软雅黑", 9f, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderColor = Color.FromArgb(80, 120, 160);
            return btn;
        }

        // ════════════════════════════════════════════════════════════════════
        // 串口设备状态刷新
        // ════════════════════════════════════════════════════════════════════

        private void RefreshSerialStatus(object sender, EventArgs e)
        {
            if (_lblET4500Status != null && !_lblET4500Status.Text.Contains("仿真"))
            {
                bool ok = ET4500.Instance.IsConnected;
                _lblET4500Status.Text = ok ? "ET4500：已连接" : "ET4500：未连接";
                _lblET4500Status.ForeColor = ok ? Color.ForestGreen : Color.Firebrick;
            }
            if (_lblZMPTStatus != null && !_lblZMPTStatus.Text.Contains("仿真"))
            {
                bool ok = ZMPT650F.Instance.IsConnected;
                _lblZMPTStatus.Text = ok ? "ZMPT650F：已连接" : "ZMPT650F：未连接";
                _lblZMPTStatus.ForeColor = ok ? Color.ForestGreen : Color.Firebrick;
            }
        }

        // ════════════════════════════════════════════════════════════════════
        // AI1 / AI2 点位列表（硬编码，与 Grp.Init() 保持一致）
        // ════════════════════════════════════════════════════════════════════

        private static List<string> GetAI1Tags()
        {
            return new List<string> {
                "AI.大气温度","AI.大气湿度","AI.大气压力","AI.机油流量",
                "AI.清洁油罐来油流量","AI.燃油进油流量测量-L30","AI.燃油回油流量测量-L31","AI.L32",
                "AI.高温水流量测量-L3","AI.中冷水流量测量-L8","AI.进气流量测量左","AI.进气流量测量右",
                "AI.水阻箱进水调节阀开度","AI.前增压器进气流量计前温度","AI.后增压器进气流量计前温度",
                "AI.厂房进气压力检测1","AI.厂房进气压力检测2" };
        }

        private static List<string> GetAI2Tags()
        {
            return new List<string> {
                "AI.A1缸排气温度","AI.A2缸排气温度","AI.A3缸排气温度","AI.A4缸排气温度",
                "AI.A5缸排气温度","AI.A6缸排气温度","AI.A7缸排气温度","AI.A8缸排气温度",
                "AI.B1缸排气温度","AI.B2缸排气温度","AI.B3缸排气温度","AI.B4缸排气温度",
                "AI.B5缸排气温度","AI.B6缸排气温度","AI.B7缸排气温度","AI.B8缸排气温度",
                "AI.P1高温水出机压力","AI.P20机油泵出口压力","AI.P21主油道进口油压",
                "AI.P2高温水泵进口压力","AI.P38燃油供油压力","AI.P3中冷水泵进口压力",
                "AI.P5中冷水出机压力","AI.T1高温水出机温度","AI.T20机油泵出口油温",
                "AI.T21主油道进口油温","AI.T2高温水进机温度","AI.T30燃油回油温度",
                "AI.T31燃油泵进口油温","AI.T3中冷水进机温度","AI.T5中冷水出机温度",
                "AI.高温水泵出口压力","AI.后中冷前空气温度","AI.后涡轮出口废气温度",
                "AI.后涡轮进口废气温度","AI.后涡轮进口废气压力","AI.后增压器机油出口温度",
                "AI.后增压器机油进口温度","AI.后增压器机油进口压力","AI.后增压器进气温度",
                "AI.后增压器进气真空度","AI.后增压器排气背压","AI.后中冷后空气温度",
                "AI.后中冷后空气压力","AI.后中冷前空气压力","AI.前涡轮出口废气温度",
                "AI.前涡轮进口废气温度","AI.前涡轮进口废气压力","AI.前增压器机油出口温度",
                "AI.前增压器机油进口温度","AI.前增压器机油进口压力","AI.前增压器进气温度",
                "AI.前增压器进气真空度","AI.前增压器排气背压","AI.前中冷后空气温度",
                "AI.前中冷后空气压力","AI.前中冷前空气温度","AI.前中冷前空气压力",
                "AI.中冷器出口水温","AI.中冷器进口水温","AI.中冷水泵出口压力","AI.主油道末端油压",
                "AI.机油耗测量压力","AI.机油耗测量液位","AI.测功机U相温度","AI.测功机V相温度",
                "AI.测功机W相温度","AI.测功机D相温度","AI.测功机N相温度",
                "AI.励磁电流检测","AI.励磁电压检测" };
        }

        // ════════════════════════════════════════════════════════════════════
        // AI 信号范围映射
        // ════════════════════════════════════════════════════════════════════

        private static SignalRange GetRange(string tag)
        {
            if (tag.Contains("排气温度") || tag.Contains("废气温度") || tag.Contains("涡轮出口"))
                return new SignalRange(0, 800, "℃");
            if (tag.Contains("真空度")) return new SignalRange(-100, 0, "kPa");
            if (tag.Contains("增压器转速") || tag.Contains("涡轮转速")) return new SignalRange(0, 60000, "rpm");
            if (tag.Contains("转速")) return new SignalRange(0, 1200, "rpm");
            if (tag.Contains("温度") || tag.Contains("油温") || tag.Contains("水温") || tag.Contains("气温"))
                return new SignalRange(0, 200, "℃");
            if (tag.Contains("背压")) return new SignalRange(0, 100, "kPa");
            if (tag.Contains("压力") || tag.Contains("油压") || tag.Contains("水压") || tag.Contains("气压"))
                return new SignalRange(0, 2500, "kPa");
            if (tag.Contains("流量")) return new SignalRange(0, 500, "kg/h");
            if (tag.Contains("开度") || tag.Contains("液位")) return new SignalRange(0, 100, "%");
            if (tag.Contains("湿度")) return new SignalRange(0, 100, "%");
            if (tag.Contains("大气压")) return new SignalRange(80, 110, "kPa");
            if (tag.Contains("电压")) return new SignalRange(0, 1000, "V");
            if (tag.Contains("电流")) return new SignalRange(0, 1000, "A");
            return new SignalRange(0, 100, "");
        }

        // ════════════════════════════════════════════════════════════════════
        // 通用辅助控件方法
        // ════════════════════════════════════════════════════════════════════

        private void AddTipLabel(Panel parent, int y, string text)
        {
            parent.Controls.Add(new Label
            {
                Text = text,
                Location = new Point(PAD_X, y),
                Size = new Size(800, 22),
                Font = new Font("微软雅黑", 8f),
                ForeColor = Color.DimGray
            });
        }

        private int AddGroupHeader(Panel parent, int y, string title)
        {
            parent.Controls.Add(new Label
            {
                Text = title,
                Location = new Point(PAD_X, y),
                Size = new Size(500, 26),
                Font = new Font("微软雅黑", 10f, FontStyle.Bold),
                ForeColor = C_HEADER
            });
            return y + 30;
        }

        private NumericUpDown AddSerialRow(Panel parent, ref int y,
            string name, decimal min, decimal max, decimal defVal, string unit,
            Action<decimal> onChanged)
        {
            parent.Controls.Add(new Label
            {
                Text = name,
                Location = new Point(PAD_X + 20, y + 6),
                Size = new Size(120, 20),
                Font = new Font("微软雅黑", 9f),
                ForeColor = Color.FromArgb(50, 50, 70)
            });

            var nud = new NumericUpDown
            {
                Location = new Point(PAD_X + 150, y + 3),
                Size = new Size(110, 24),
                Minimum = min,
                Maximum = max,
                DecimalPlaces = 1,
                Increment = 0.5m,
                Value = defVal,
                Font = new Font("微软雅黑", 9.5f)
            };

            parent.Controls.Add(new Label
            {
                Text = unit,
                Location = new Point(PAD_X + 268, y + 6),
                Size = new Size(50, 20),
                ForeColor = Color.DimGray
            });

            Action<decimal> captured = onChanged;
            nud.ValueChanged += delegate (object s, EventArgs e)
            { captured(((NumericUpDown)s).Value); };

            parent.Controls.Add(nud);
            y += ROW_H;
            return nud;
        }

        private Button MakeBtn(string text, Point loc, Color back, int width)
        {
            var btn = new Button
            {
                Text = text,
                Location = loc,
                Size = new Size(width, 28),
                FlatStyle = FlatStyle.Flat,
                BackColor = back,
                ForeColor = Color.White,
                Font = new Font("微软雅黑", 8.5f),
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderSize = 0;
            return btn;
        }

        // 在类的字段区加这一行
        private static frmFullSimulator _instance;

        /// <summary>
        /// 打开仿真器（全局唯一窗口）
        /// </summary>
        public static void ShowInstance()
        {
            if (_instance == null || _instance.IsDisposed)
            {
                _instance = new frmFullSimulator();
                _instance.FormClosed += delegate (object s, FormClosedEventArgs e)
                {
                    _instance = null;
                };
            }
            _instance.Show();
            _instance.BringToFront();
            _instance.WindowState = FormWindowState.Normal;
        }

        // ── TRDP 内部数据结构 ────────────────────────────────────────────────
        private class TrdpAnalogEntry
        {
            public TrackBar TrackBar { get; set; }
            public Label ValueLabel { get; set; }
            public NumericUpDown Nud { get; set; }
        }
    }
}