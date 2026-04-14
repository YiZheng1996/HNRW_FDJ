using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RW.Configuration;
using System.Windows.Forms;
using System.ComponentModel;

namespace MainUI.Config.Modules
{
    public class ValveConfig : IniConfig
    {
        public ValveConfig()
          : base(Application.StartupPath + "\\config\\Valve.ini")
        {
            this.SetSectionName("ValveInfo");
            this.Load();
        }

        [IniKeyName("阀门信息列表")]
        public List<ValveInfo> valveInfo { get; set; } = new List<ValveInfo>();



        public class ValveInfo
        {
            /// <summary>
            /// 阀门号
            /// </summary>
            [DisplayName("阀门号")]
            public string ValveNum { get; set; }

            /// <summary>
            /// 阀门点位
            /// </summary>
            [DisplayName("阀门点位")]
            public string ValvePoint { get; set; }

            /// <summary>
            /// 阀门开到位点位
            /// </summary>
            [DisplayName("阀门开到位点位")]
            public string ValveOpenPoint { get; set; }

            /// <summary>
            /// 阀门关到位点位
            /// </summary>
            [DisplayName("阀门关到位点位")]
            public string ValveClosePoint { get; set; }
        }

    }
}
