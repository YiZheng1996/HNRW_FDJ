using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MainUI
{
    public partial class frmMessageYesNO : Form
    {
        public frmMessageYesNO()
        {
            InitializeComponent();
            this.Load += frmMessageYesNO_Load; // 添加Load事件
        }

        private void frmMessageYesNO_Load(object sender, EventArgs e)
        {
            this.btnYes.Focus();
        }

        private string _title = "";
        /// <summary>
        /// 提示框左上角的标题文字。
        /// </summary>
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                this.Text = value;
            }
        }

        //2019-8-28 09:03:38，增加字体、颜色设置==============================
        private Font _TipsFont;
        [DefaultValue(typeof(Font), "宋体, 15pt")]
        public Font TipsFont
        {
            get { return this.lblTips.Font; }
            set
            {
                this.lblTips.Font = value;
                _TipsFont = value;
            }
        }


        [DefaultValue(typeof(Color), "Black")]
        [Description("用于提示文字的前景色")]
        public Color TipsForeColor
        {
            get { return this.lblTips.ForeColor; }
            set { this.lblTips.ForeColor = value; }
        }
        //2019-8-28 09:03:38，增加字体、颜色设置==============================

        private string _msg = "";
        /// <summary>
        /// 提示内容
        /// </summary>
        public string Msg
        {
            get
            {
                return _msg;
            }
            set
            {
                _msg = value;
                lblTips.Text = value;
            }
        }

        /// <summary>
        /// yes :true  no:false
        /// </summary>
        public bool IsYes { get; set; }

        private void btnYes_Click(object sender, EventArgs e)
        {
            IsYes = true;
            Close();
        }

        private void btnNO_Click(object sender, EventArgs e)
        {
            IsYes = false;
            Close();
        }

        private void lblTips_Click(object sender, EventArgs e)
        {

        }



    }
}
