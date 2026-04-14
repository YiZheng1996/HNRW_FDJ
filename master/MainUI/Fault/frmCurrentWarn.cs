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

        private void ucFaultList_Load(object sender, EventArgs e)
        {
            // 可以在这里开始模拟数据变化来测试

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
