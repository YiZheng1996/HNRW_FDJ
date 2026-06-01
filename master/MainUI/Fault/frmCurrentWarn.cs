using MainUI.Fault.Engine;
using MainUI.Fault.Model;
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

        private void ucFaultList_Load(object sender, EventArgs e)
        {
            // 可以在这里开始模拟数据变化来测试

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region 动态报警墙扩展

        private readonly List<ucWarn> _dynamicEcmWarns = new List<ucWarn>();

        /// <summary>gpWarn1 绝对定位：动态控件按列堆叠的起始位置与行距。</summary>
        private int _dynStartX = 11;
        private int _dynStartY = 700;   // 放在固定控件下方；如有重叠按实际界面调此值
        private const int DYN_ROW_H = 42;

        public void BuildDynamicEcmWarns()
        {
            try
            {
                if (this.DesignMode) return;

                string model = Var.SysConfig?.LastModel;
                if (!EcmProfileStore.Exists(model)) return;

                var profile = EcmProfileStore.Load(model);
                if (profile == null || profile.Rules == null) return;

                this.gpWarn1.SuspendLayout();

                int idx = 0;
                foreach (var rule in profile.Rules)
                {
                    string key = rule.Name;
                    if (string.IsNullOrEmpty(key)) continue;
                    if (_faultWarnMap.ContainsKey(key)) continue;

                    var w = NewEcmWarn(key, _dynStartX, _dynStartY + idx * DYN_ROW_H);
                    this.gpWarn1.Controls.Add(w);
                    _faultWarnMap.Add(key, w);
                    _dynamicEcmWarns.Add(w);
                    idx++;
                }

                this.gpWarn1.ResumeLayout();
            }
            catch (Exception ex)
            {
                try { Var.LogInfo("frmCurrentWarn.BuildDynamicEcmWarns 失败: " + ex.Message); } catch { }
            }
        }

        private ucWarn NewEcmWarn(string key, int x, int y)
        {
            var w = new ucWarn
            {
                Key = key,
                Title = key,
                Font = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(134))),
                Margin = new Padding(5),
                Size = new Size(561, 37),
                Location = new Point(x, y),
                ShowStopButton = false
            };
            return w;
        }

        #endregion
    }
}
