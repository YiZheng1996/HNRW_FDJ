using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace MainUI
{
    /// <summary>
    /// 导出全部时的“保存中”提示窗。
    /// 显示后在后台写文件；完成前不可关闭，完成后可点关闭。
    /// </summary>
    public partial class frmExportSaving : Form
    {
        private readonly Action _work;
        private bool _finished;

        public frmExportSaving(Action work)
        {
            _work = work;
            InitializeComponent();
        }

        private void frmExportSaving_Shown(object sender, EventArgs e)
        {
            btnClose.Enabled = false;
            _finished = false;

            ThreadPool.QueueUserWorkItem(_ =>
            {
                try
                {
                    _work?.Invoke();
                    CompleteOnUi(true, "保存完成");
                }
                catch (Exception ex)
                {
                    CompleteOnUi(false, "保存失败：" + ex.Message);
                }
            });
        }

        private void CompleteOnUi(bool success, string message)
        {
            if (IsDisposed)
            {
                return;
            }

            try
            {
                if (InvokeRequired)
                {
                    BeginInvoke(new Action(() => SetCompleted(success, message)));
                    return;
                }

                SetCompleted(success, message);
            }
            catch (ObjectDisposedException)
            {
            }
        }

        private void SetCompleted(bool success, string message)
        {
            if (IsDisposed)
            {
                return;
            }

            lblStatus.Text = message;
            lblStatus.ForeColor = success
                ? Color.FromArgb(0, 128, 0)
                : Color.FromArgb(192, 0, 0);
            _finished = true;
            btnClose.Enabled = true;
            btnClose.Focus();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (!_finished)
            {
                e.Cancel = true;
            }
            base.OnFormClosing(e);
        }
    }
}
