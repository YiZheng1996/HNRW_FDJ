using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainUI.HMI_Auto
{
    public partial class ucStepTrackBar : UserControl
    {
        /// <summary>
        /// 值改变事件委托
        /// </summary>
        public event ValueChangedHandler OnValueChanged;

        public delegate void ValueChangedHandler(double currentValue);

        public ucStepTrackBar()
        {
            InitializeComponent();
        }


        private bool _readOnly = false;
        public bool ReadOnly
        {
            get { return _readOnly; }
            set
            {
                _readOnly = value;
                // 根据只读状态启用或禁用控件
                this.Enabled = !value;
            }
        }

        private double currentValue;

        /// <summary>
        /// 实时数据
        /// </summary>
        public double CurrentValue
        {
            get { return currentValue; }
            set
            {
                currentValue = value;
                this.txtValue.Text = value.ToString();

                double v = getNumber(value);
                if (v <= this.MaxValue)
                    this.trackBar1.Value = getNumber(value);
            }
        }

        int decimalNumber = 0;
        public int DecimalNumber
        {
            get { return decimalNumber; }
            set { decimalNumber = value; }
        }

        private int getNumber(double value)
        {
            int v = (int)(value * Math.Pow(10, DecimalNumber));
            return v;
        }


        public int MinValue
        {
            get { return this.trackBar1.Minimum; }
            set
            {
                this.trackBar1.Minimum = getNumber(value);
                this.trackBar1.TickFrequency = (this.trackBar1.Maximum - this.trackBar1.Minimum) / 10;
                this.trackBar1.LargeChange = (int)Math.Pow(10, (this.trackBar1.Maximum - this.trackBar1.Minimum).ToString().Length - 2);
            }
        }

        public int MaxValue
        {
            get { return this.trackBar1.Maximum; }
            set
            {
                this.trackBar1.Maximum = getNumber(value);
                this.trackBar1.TickFrequency = (this.trackBar1.Maximum - this.trackBar1.Minimum) / 10;
                this.trackBar1.LargeChange = (int)Math.Pow(10, (this.trackBar1.Maximum - this.trackBar1.Minimum).ToString().Length - 2);
            }
        }



        private void textBox1_Leave(object sender, EventArgs e)
        {
            //int value;
            //if (!int.TryParse(this.txtValue.Text, out value))
            //{
            //    this.trackBar1.Focus();
            //    this.currentValue = this.trackBar1.Value;
            //}
        }


        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            double v = this.trackBar1.Value / Math.Pow(10, DecimalNumber);
            this.txtValue.Text = v.ToString();
            this.currentValue = v;
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            double v = this.trackBar1.Value / Math.Pow(10, DecimalNumber);
            this.txtValue.Text = v.ToString();
            this.currentValue = v;

            // 触发自定义的值改变事件，传递当前值
            OnValueChanged?.Invoke(this.currentValue);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            double value;
            if (double.TryParse(this.txtValue.Text, out value) && this.trackBar1.Maximum >= getNumber(value))
            {
                this.trackBar1.Value = getNumber(value);
            }
        }

        private void textBox1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            double value;
            double.TryParse(this.txtValue.Text, out value);
            if (this.trackBar1.Maximum < value)
            {
                //e.IsInputKey = false;
            }
        }
    }


}
