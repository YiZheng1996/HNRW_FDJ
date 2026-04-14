using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MainUI.Widgets
{
    [DefaultProperty("FaultName")]
    public partial class uclblTextBox : UserControl
    {
        public uclblTextBox()
        {
            InitializeComponent();
        }



        /// <summary>
        /// 序号
        /// </summary>
        [DefaultValue(0)]
        public int Index { get; set; }


        private string _fTitle = "";
        /// <summary>
        /// 标题
        /// </summary>
        [Description("标题")]
        [DefaultValue("")]
        public string Title { get { return _fTitle; }
            set { lblTitle.Text = value;
                _fTitle = value; } }


        private string _FaultName = "";

        /// <summary>
        /// 故障名称
        /// </summary>
        [Description("故障名称")]
        [DefaultValue("")]
        public string FaultName { get { return _FaultName; } set { txtFault.Text = value; _FaultName = value; } }

        private void txtFault_TextChanged(object sender, EventArgs e)
        {
            _FaultName = txtFault.Text;
        }

    }
}
