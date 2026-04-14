using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainUI
{
    public partial class frmDispReport : Form
    {
        private string filename;
        public frmDispReport(string file)
        {
            InitializeComponent();
            filename = file;
        }
        private void frmDispReport_Load(object sender, EventArgs e)
        {
            rwReport1.Filename = filename;
            rwReport1.SetWrite(true);
            rwReport1.Init();
        }
        private void BtnPrint_Click(object sender, EventArgs e)
        {
            rwReport1.Print();
        }
        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        } 
    }
}
