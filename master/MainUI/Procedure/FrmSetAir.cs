using MainUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MainUI.Procedure
{
    public partial class FrmSetAir : Form
    {
        public FrmSetAir()
        {
            InitializeComponent();
        }


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

        public FrmSetAir(string title,int minSetValue,  int maxSetValue, double curValue)
        {
            InitializeComponent();
            this.Title = title;
            this.MinValue = minSetValue;
            this.MaxValue = maxSetValue;
            numAOValue.Value = curValue.ToDecimal();
        }


        private int _min = 0;
        [DefaultValue(0)]
        public int MinValue
        {
            get { return _min; }
            set
            {
                _min = value;
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
        }

        private void NpdAir_ValueChanged(object sender, EventArgs e)
        {
            //if (numAOValue.Value > 1000)
            //    numAOValue.Value = 1000;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            numAOValue.Value = Convert.ToDecimal(trackBar1.Value);
        }
    }
}