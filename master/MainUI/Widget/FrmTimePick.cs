using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainUI.Widget
{
    /// <summary>
    /// 曲线时间查询
    /// </summary>
    public partial class FrmTimePick : Form
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        public FrmTimePick()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnSelct_Click(object sender, EventArgs e)
        {
            StartTime = this.dtpStartTime.Value;
            EndTime = this.dtpEndTime.Value;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void FrmTimePick_Load(object sender, EventArgs e)
        {
            this.dtpStartTime.Value = DateTime.Now.AddHours(-2);
            this.dtpEndTime.Value = DateTime.Now;
        }
    }
}
