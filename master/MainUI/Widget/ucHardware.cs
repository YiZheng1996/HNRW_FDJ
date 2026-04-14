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
    public partial class ucHardware : UserControl
    {
        public ucHardware()
        {
            InitializeComponent();
        }
        private bool _state;
        public bool LightState
        {
            get { return _state; }
            set { _state = value; light.State = value ? Sunny.UI.UILightState.On : Sunny.UI.UILightState.Off; }
        }
        private string _title;
        public string Title { get { return _title; } set { lblTitle.Text = value; _title = value; } }
    }
}
