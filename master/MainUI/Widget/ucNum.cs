using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainUI.Widget
{
    public partial class ucNum : UserControl
    {
        public ucNum()
        {
            InitializeComponent();
        }

        private string _key = "";
        public string Key
        {
            get { return _key; }
            set { _key = value; }
        }

        private string _title = "";
        public string Title
        {
            get { return _title; }
            set { _title = value; lblTitle.Text = value; }
        }

        private double  _value;

        public double  Value
        {
            get { return _value; }
            set { _value = value;
                txtValue.Text = value.ToString();

                if(value> MaxValue)
                {
                    clrAlarm = Color.Red;
                }
                else
                {
                    clrAlarm = Color.Gold;
                }
            }
        }

        public double MaxValue { get; set; } = 60;

        private Color _clrAlarm;
        public Color clrAlarm
        {
            get { return _clrAlarm; }
            set
            {
                _clrAlarm = value;
                txtValue.ForeColor = value;
            }
        }

    }
}
