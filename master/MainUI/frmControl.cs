using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using RW.Driver;
using RW.UI.Manager.User;
//using RW.UI.BLL;
using MainUI.BLL;

namespace MainUI
{
    public partial class frmControl : Form
    {
        public frmControl()
        {
            InitializeComponent();
        }


        private void frmChangePwd_Load(object sender, EventArgs e)
        {
        
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}