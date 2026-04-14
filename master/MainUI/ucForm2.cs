using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BogieIdling.UI.TRDP;
using MainUI.Widget;
using Sunny.UI;

namespace MainUI
{
    public partial class ucForm2 : UserControl
    {
        Dictionary<string, ucValueLabel> dicValueLabel = new Dictionary<string, ucValueLabel>();
        Dictionary<string, UILight> dicLight = new Dictionary<string, UILight>();
        public ucForm2()
        {
            InitializeComponent();
            EachControl(this);
            Var.TRDP.KeyValueChange += TRDP_KeyValueChange;
            
        }
        private void TRDP_KeyValueChange(object sender, TRDPValueChangedEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => TRDP_KeyValueChange(sender, e)));
                return;
            }

            if (dicValueLabel.ContainsKey(e.Key))
            {
                dicValueLabel[e.Key].Value = e.Value.ToDouble();
            }
            if (dicLight.ContainsKey(e.Key))
            {
                dicLight[e.Key].State = e.Value == 0 ? UILightState.Off : UILightState.On;
            }
            if(e.Key == "同步状态")
            {
                this.lblConState.Text = e.Value == 0 ? "未同步" : "已同步";
            }
        }
        /// <summary>
        /// 添加控件到字典
        /// </summary>
        /// <param name="con"></param>
        private void EachControl(Control con)
        {
            foreach (Control item in con.Controls)
            {
                EachControl(item);
            }
            if (con is ucValueLabel)
            {
                //添加数值显示条
                ucValueLabel valueLabel = con as ucValueLabel;
                if (valueLabel.Tag != null && valueLabel.Tag.ToString() != string.Empty)
                {
                    dicValueLabel.Add(valueLabel.Tag.ToString(), valueLabel);
                }
            }
            if (con is UILight)
            {
                //添加状态灯
                UILight light = con as UILight;
                if (light.Tag != null && light.Tag.ToString() != string.Empty)
                {
                    dicLight.Add(light.Tag.ToString(), light);
                }
            }
        }
    }
}
