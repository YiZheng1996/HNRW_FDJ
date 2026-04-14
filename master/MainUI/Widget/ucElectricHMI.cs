using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MainUI.Procedure;

namespace MainUI
{
    public partial class ucElectricHMI : UserControl
    {
        public ucElectricHMI()
        {
            InitializeComponent();
        }

        private void aiNumericalDisplay13_Click(object sender, EventArgs e)
        {
            
        }

        private void btnExciter_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = new FrmSetAir().ShowDialog();
                if(result == DialogResult.OK)
                {

                }
            }
            catch (Exception ex)
            {

                throw new Exception("励磁电源启动失败"+ex.Message);
            }
        }
    }
}
