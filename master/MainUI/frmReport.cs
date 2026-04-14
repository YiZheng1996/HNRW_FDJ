using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Reflection;
using System.Diagnostics;
using System.IO;
using RW.UI.Controls.Report;
using MainUI.Model;
using RW.UI;
using Microsoft.Office;
using MainUI.BLL;
using Microsoft.Office.Interop.Excel;
using RW;

namespace MainUI
{
    public partial class frmReport : Form
    {
        public TestViewModel viewMole { get; set; }
        public string saveFilepath { get; set; }
        public string Filename { get; set; }  //报表的文件地址
        public event OpenHandler Opened;
        int rowIndex = 0;

        public frmReport()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 窗体加载
        /// </summary>
        private void frmReport_Load(object sender, EventArgs e)
        {
            try
            {
                Control.CheckForIllegalCrossThreadCalls = false;
                this.rwReport1.Filename = this.Filename;
                this.rwReport1.Init();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 关闭
        /// </summary>
        private void btnClose_Click(object sender, EventArgs e)
        {
            SystemHelper.KillProcess("EXCEL"); //正确参数
            this.Close();
        }
        /// <summary>
        /// 上翻
        /// </summary>
        private void btnPageUp_Click(object sender, EventArgs e)
        {
            int value = (int)this.numericUpDown1.Value;
            rowIndex -= value;
            if (rowIndex < 1)
                rowIndex = 1;
            this.rwReport1.ScrollIndex(rowIndex);
        }
        /// <summary>
        /// 下翻
        /// </summary>
        private void btnPageDown_Click(object sender, EventArgs e)
        {
            int value = (int)this.numericUpDown1.Value;
            rowIndex += value;
            if (rowIndex > 100)
                rowIndex = 100;
            this.rwReport1.ScrollIndex(rowIndex);
        }
        /// <summary>
        /// 打印
        /// </summary>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                this.rwReport1.Print();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// 保存
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (rwReport1 == null)
                    return;

                DateTime now = DateTime.Now;

                string fileName = Common.mTestViewModel.ModelName + "_" + now.ToString("yyyyMMdd_HHmmss") + ".xls";

                saveFilepath = Var.rootRptSave + "\\" + fileName;

                TestRecordBLL recordbll = new TestRecordBLL();
                int ret = recordbll.SaveData(viewMole.ModelTypeID.ToString(), viewMole.ModelName, viewMole.TestNO, RW.UI.RWUser.User.Username, now.ToString("yyyy-MM-dd HH:mm:ss"), saveFilepath);
                if (System.IO.Directory.Exists(Var.rootRptSave) == false)
                    System.IO.Directory.CreateDirectory(Var.rootRptSave);
                this.rwReport1.SaveAS(saveFilepath);

                // rwReport1.SaveAsPDF(saveFilepath);
                Var.MsgBoxSuccess(this, "保存成功");
            }
            catch (Exception ex)
            {
                Var.MsgBoxWarn(this, ex.Message);
            }
        }
        /// <summary>
        /// 读取key中的值，使用第一个worksheet
        /// </summary>
        /// <returns></returns>
        public object Read(string key)
        {
            return this.rwReport1.Read(1, key);
        }
        /// <summary>
        /// 将value写入到key中，使用第一个worksheet
        /// </summary>
        public void Write(string key, object value)
        {
            this.Write(1, key, value);
        }
        /// <summary>
        /// 将value写入到key中，使用指定的worksheet
        /// </summary>
        public void Write(int sheetIndex, string key, object value)
        {
            this.rwReport1.Write(sheetIndex, key, value);
        }
        /// <summary>
        /// 报表打开时
        /// </summary>
        private void rwReport1_Opened(object sender, OpenedReportArgs e)
        {
            if (this.Opened != null)
                this.Opened(this, e);
        }

        /// <summary>
        /// 插入图片
        /// </summary>
        public void InsertPicture(string key, string PicturePath)
        {
            this.rwReport1.InsertPicture(key, PicturePath);
        }
    }
}
