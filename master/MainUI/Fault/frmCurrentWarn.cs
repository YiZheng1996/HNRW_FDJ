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
    }
}