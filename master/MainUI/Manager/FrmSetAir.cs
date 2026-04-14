using MainUI;
using RW.Modules;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace RW.ModuleTest.UI.Manager
{
    public partial class FrmSetAir : Form
    {
        public FrmSetAir()
        {
            InitializeComponent();
        }

        public event ValueHandler Submited;
        public event Action<double> ValueChanged;

        public FrmSetAir(string title,int maxSetValue)
        {
            InitializeComponent();
            this.Title = title;
            this.MaxValue = maxSetValue;
        }

        public FrmSetAir(string title, int maxSetValue, double curValue)
        {
            InitializeComponent();
            this.Title = title;
            this.MaxValue = maxSetValue;
            numAOValue.Value = curValue.ToDecimal();
        }

        public FrmSetAir(string title, int minSetValue, int maxSetValue, double curValue,string unit="")
        {
            InitializeComponent();
           
            this.Title = title + " (" + minSetValue + "至" + maxSetValue+")" + unit;
            this.MinValue = minSetValue;
            this.MaxValue = maxSetValue;
            if(curValue<MinValue)
            {
                curValue = MinValue;
            }

            if(curValue>MaxValue)
            {
                curValue = MinValue;
            }
            numAOValue.Value = curValue.ToDecimal();
        }

      
        private void FrmSetAir_Load(object sender, EventArgs e)
        {
            UpDownBase updown = this.numAOValue as UpDownBase;
            updown.Select(0, updown.Text.Length);
        }

        private int _mix = 0;
        public int MinValue
        {
            get { return _mix; }
            set
            {
                _mix = value;
                trackBar1.Minimum = value;
                numAOValue.Minimum = value;
            }
        }


        private int _max = 1000;
        [DefaultValue(1000)]
        public int MaxValue
        {
            get { return _max; }
            set
            {
                _max = value;
                trackBar1.Maximum = value;
                numAOValue.Maximum = value;
            }
        }

        private string _lblTitl;

        public string Title
        {
            get { return _lblTitl; }
            set { _lblTitl = value;
                groupBox1.Text = value;
            }
        }

        private double airvalue;
        [Description("返回设定值")]
        [DefaultValue(0)]
        public double AOValue
        {
            get { return airvalue; }
            set { airvalue = Convert.ToDouble(numAOValue.Value); }
        }


        private void Btn_Ok_Click(object sender, EventArgs e)
        {
            AOValue = Convert.ToDouble(numAOValue.Value);


            if (Submited != null)
            {
                Submited(this, this.AOValue);
            }
            if (ValueChanged != null)
                ValueChanged(this.AOValue);

        }



        private void NpdAir_ValueChanged(object sender, EventArgs e)
        {
            if (numAOValue.Value > 1000)
                numAOValue.Value = 1000;

            this.trackBar1.Value = numAOValue.Value.ToInt();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            numAOValue.Value = Convert.ToDecimal(trackBar1.Value);
        }

        private void numAOValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            // if (e.KeyChar == '\r')

            if (e.KeyChar == (char)Keys.Enter)
            {
                Btn_Ok_Click(null, null);
                Close();
            }
        }
    }
}