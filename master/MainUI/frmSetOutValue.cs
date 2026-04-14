using RW.UI.Controls;
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
    public partial class frmSetOutValue : Form
    {
        private double _OutValue = 0.0;
        /// <summary>
        /// 输出返回值
        /// </summary>
        [Description("返回设定值")]
        [DefaultValue(0)]
        public double OutValue
        {
            get { return _OutValue; }
            set { _OutValue = value; }
        }
        public frmSetOutValue()
        {
            InitializeComponent();
        
        }

        public int X { get; set; }
        public int Y { get; set; }

        private void frmSetOutValue_Load(object sender, EventArgs e)
        {
            // this.Location = new Point(X, Y+53);
            nudOutputValue.Focus();
            nudOutputValue.FocusedSelectAll = true;
            //int h = System.Windows.Forms.Screen.AllScreens[0].Bounds.Height;
            //int w = System.Windows.Forms.Screen.AllScreens[0].Bounds.Width;
            //int w2 = w / 2 + 40;
            //int h2 = h / 2 - 30;
            //Cursor.Position = new Point(w2, h2); // 设置鼠标位置到屏幕坐标 //自动把鼠标移动到输入框位置，才自动选中所有内容

            
        }

        /// <summary>
        /// 初始化模拟量输出窗体
        /// <para>origValue 原始值</para>
        /// <para>title 显示文本</para>
        /// <para>maxValue 最大值</para>
        /// </summary>
        public frmSetOutValue(double origValue, string title,double minValue, double maxValue)
        {
            InitializeComponent();
            nudOutputValue.Minimum = minValue;
            if (origValue < minValue)
                origValue = minValue;
            nudOutputValue.Maximum = maxValue;
            if (origValue > maxValue)
                origValue = maxValue;
            nudOutputValue.Text = origValue.ToString("f0");
            this.Text = title + "  可输入范围：[" + minValue + "至" + maxValue + "]";
            OutValue = origValue;
           
        }

        private void Btn_Ok_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(nudOutputValue.Text))
            {
                nudOutputValue.Text = "0";
            }
            OutValue = Convert.ToDouble(nudOutputValue.Text);
            if (OutValue > nudOutputValue.Maximum)
            {
                nudOutputValue.Text = nudOutputValue.Maximum.ToString();
                OutValue = nudOutputValue.Maximum;
                MessageBox.Show("超出了最大值"+ nudOutputValue.Maximum.ToString()+"。请重新输入。");
                nudOutputValue.Focus();
                return;
            }
          
            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void Btn_Exit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }

        private void nudOutputValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                Btn_Ok_Click(null, null);
             
            }
        }

        private void btnSub_Click(object sender, EventArgs e)
        {
            Sunny.UI.UIButton btn = sender as Sunny.UI.UIButton;
            if (string.IsNullOrEmpty(btn.Tag.ToString()))
                btn.Tag = 1;
            int sub = 1;
            int.TryParse(btn.Tag.ToString(), out sub);
            double curV = nudOutputValue.DoubleValue;
            curV -= sub;
            if (curV >= nudOutputValue.Minimum)
                nudOutputValue.Text = curV.ToString();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            
            Sunny.UI.UIButton btn = sender as Sunny.UI.UIButton;
            if (string.IsNullOrEmpty(btn.Tag.ToString()))
                btn.Tag = 1;
            int add = 1;
            int.TryParse(btn.Tag.ToString(), out add);
            double curV = nudOutputValue.DoubleValue;
            curV += add;
            if (curV <= nudOutputValue.Maximum)
                nudOutputValue.Text = curV.ToString();
        }

        private void btnKey_Click(object sender, EventArgs e)
        {
            NumInputHelper.ShowDialog(nudOutputValue, true);
        }
    }
}
