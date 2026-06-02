using MainUI.Fault.Engine;
using MainUI.Fault.Model;
using MainUI.Global;
using MainUI.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainUI.Fault
{
    /// <summary>
    /// 动态报警墙扩展
    /// 当前型号若由引擎接管(存在 JSON)，按规则名补建缺失的 ucWarn，
    /// 加入 gpWarn1 并注册进 _faultWarnMap，使 另外型号 故障也能在“实时报警”窗亮灯。
    /// 240 无 JSON 时不动。
    /// </summary>
    public partial class frmCurrentWarn : Form
    {
        /// <summary>
        /// 所有故障的控件
        /// </summary>
        private Dictionary<string, ucWarn> _faultWarnMap = new Dictionary<string, ucWarn>();

        public frmCurrentWarn()
        {
            InitializeComponent();

            if (this.DesignMode) return;

            InitializeFaultDetection();

            // 重新刷新一次故障（用于程序初始化）
            Var.FaultService.FaultCheckResend();
        }

        [Description("当点击故障复位时触发该事件")]
        public event Action FaultReset;

        /// <summary>
        /// 初始化加载
        /// </summary>
        private void InitializeFaultDetection()
        {
            _faultWarnMap.Clear();
            // 建立故障ID到ucWarn控件的映射
            foreach (var item in this.gpWarn1.Controls)
            {
                if (item is ucWarn)
                {
                    ucWarn warn = item as ucWarn;
                    _faultWarnMap.Add(warn.Key, warn);
                }
            }
            // 订阅故障检测事件
            Var.FaultService.FaultDetected += OnFaultDetected;

            // 首次构建动态报警灯，并在此安装型号切换钩子（缺这一句切型号不会刷新）
            BuildDynamicEcmWarns();
        }

        /// <summary>
        /// 刷新故障状态
        /// </summary>
        /// <param name="faultId"></param>
        /// <param name="faultState"></param>
        /// <param name="warnType"></param>
        private void OnFaultDetected(string faultId, FaultState faultState, WarnTypeEnum warnType)
        {
            // 确保在UI线程上更新
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<string, FaultState, WarnTypeEnum>(OnFaultDetected), faultId, faultState, warnType);
                return;
            }

            if (_faultWarnMap.TryGetValue(faultId, out var warnControl))
            {
                warnControl.CurrentFault = warnType;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region “实时报警”弹窗的动态报警墙扩展（型号驱动）

        /// <summary>本类补建的动态控件（型号切换时定点清理），与固定的 _faultWarnMap 并存。</summary>
        private readonly List<ucWarn> _dynamicEcmWarns = new List<ucWarn>();

        /// <summary>gpWarn1 绝对定位：动态控件按列堆叠的起始位置与行距。</summary>
        private int _dynStartX = 11;
        private int _dynStartY = 700;   // 放在固定控件下方；如有重叠按实际界面调此值
        private const int DYN_ROW_H = 42;

        /// <summary>型号切换事件钩子是否已安装（惰性安装，确保只订阅一次）。</summary>
        private bool _ecmModelHookInstalled;

        /// <summary>
        /// 按当前型号的引擎判据补建缺失的 ucWarn。
        /// 仅补“现有 _faultWarnMap 里没有的 Key”，已有的固定控件原样复用。
        /// </summary>
        public void BuildDynamicEcmWarns()
        {
            // 钩子必须无条件先装好：即使当前是 240(无JSON)，也要能在切到 280 时收到通知刷新。
            EnsureEcmModelHook();

            try
            {
                if (this.DesignMode) return;

                string model = Var.SysConfig != null ? Var.SysConfig.LastModel : null;
                if (!EcmProfileStore.Exists(model)) return;   // 无 JSON(如240) → 不动

                var profile = EcmProfileStore.Load(model);
                if (profile == null || profile.Rules == null) return;

                this.gpWarn1.SuspendLayout();
                try
                {
                    int idx = 0;
                    foreach (var rule in profile.Rules)
                    {
                        string key = rule.Name;
                        if (string.IsNullOrEmpty(key)) continue;
                        if (_faultWarnMap.ContainsKey(key)) continue;   // 已有固定控件 → 复用

                        var w = NewEcmWarn(key, _dynStartX, _dynStartY + idx * DYN_ROW_H);
                        this.gpWarn1.Controls.Add(w);
                        _faultWarnMap.Add(key, w);
                        _dynamicEcmWarns.Add(w);
                        idx++;
                    }
                }
                finally
                {
                    this.gpWarn1.ResumeLayout();
                }
            }
            catch (Exception ex)
            {
                try { Var.LogInfo("frmCurrentWarn.BuildDynamicEcmWarns 失败: " + ex.Message); } catch { }
            }
        }

        /// <summary>
        /// 型号切换时调用：先清掉上一型号补建的控件，再按新型号重建。
        /// 设计期固定控件(_faultWarnMap 里那批)不动。
        /// </summary>
        public void RebuildDynamicEcmWarns()
        {
            try
            {
                if (this.DesignMode) return;

                this.gpWarn1.SuspendLayout();
                try
                {
                    foreach (var w in _dynamicEcmWarns)
                    {
                        if (w == null) continue;
                        string k = w.Key;
                        if (!string.IsNullOrEmpty(k) && _faultWarnMap.ContainsKey(k))
                            _faultWarnMap.Remove(k);
                        this.gpWarn1.Controls.Remove(w);
                        w.Dispose();
                    }
                    _dynamicEcmWarns.Clear();
                }
                finally
                {
                    this.gpWarn1.ResumeLayout();
                }

                BuildDynamicEcmWarns();
            }
            catch (Exception ex)
            {
                try { Var.LogInfo("frmCurrentWarn.RebuildDynamicEcmWarns 失败: " + ex.Message); } catch { }
            }
        }

        /// <summary>按现有固定 ucWarn 的样式新建一个（绝对定位），保证视觉一致。</summary>
        private ucWarn NewEcmWarn(string key, int x, int y)
        {
            return new ucWarn
            {
                Key = key,
                Title = key,
                Font = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(134))),
                Margin = new Padding(5),
                Size = new Size(561, 37),
                Location = new Point(x, y),
                ShowStopButton = false
            };
        }

        // 型号切换即时刷新 

        /// <summary>
        /// 惰性安装型号切换钩子（只装一次）。订阅 EventTriggerModel.OnModelNameChanged，
        /// 并在窗体销毁时反订阅，避免静态事件持有已释放窗体造成泄漏/跨线程异常。
        /// </summary>
        private void EnsureEcmModelHook()
        {
            if (_ecmModelHookInstalled) return;
            _ecmModelHookInstalled = true;

            EventTriggerModel.OnModelNameChanged += OnEcmModelChanged;
            this.Disposed += (s, e) =>
            {
                EventTriggerModel.OnModelNameChanged -= OnEcmModelChanged;
            };
        }

        /// <summary>
        /// 型号切换回调。可能在后台线程触发，统一切到 UI 线程后再重建控件。
        /// 本弹窗可能尚未显示(无句柄)，此时直接在当前线程重建内存中的控件集合。
        /// </summary>
        private void OnEcmModelChanged(string modelName)
        {
            if (this.IsDisposed) return;
            try
            {
                if (this.IsHandleCreated && this.InvokeRequired)
                    this.BeginInvoke(new Action(RefreshAfterModelChanged));
                else
                    RefreshAfterModelChanged();
            }
            catch { /* 窗体已销毁，忽略 */ }
        }

        /// <summary>重建动态控件，并立即按新型号当前故障状态重绘。</summary>
        private void RefreshAfterModelChanged()
        {
            RebuildDynamicEcmWarns();

            // 让弹窗立刻反映新型号的当前故障态；FaultCheckResend 幂等、无副作用。
            try { Var.FaultService?.FaultCheckResend(); } catch { }
        }

        #endregion
    }
}