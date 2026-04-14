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
    public partial class AOPara : UserControl
    {
        public AOPara()
        {
            InitializeComponent();
        }
        private string _title = "";
        public string Title
        {
            get { return _title; }
            set { _title = value; lblTitle.Text = value; }
        }

        private double _value;

        public double Value
        {
            get { return _value; }
            set
            {
                _value = value;
                lblValue.Text = value.ToString();

               
            }
        }
    }
}
