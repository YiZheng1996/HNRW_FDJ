using RW.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainUI.Config
{
   public class SysConstantParas : IniConfig
    {
        public SysConstantParas()
            : base(Application.StartupPath + "\\config\\SysConstantParas.ini")
        {
            this.SetSectionName("SysConstantParas");
            this.Load();
        }

        /// <summary>
        ///水加热最低液位mm
        /// </summary>
        [IniKeyName("水加热最低液位")]
        public int WaterLiquidLevel { get; set; } = 5000;


    }



}
