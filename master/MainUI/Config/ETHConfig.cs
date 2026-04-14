using RW.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainUI.Config
{
    /// <summary>
    /// 连接TRDP配置
    /// </summary>
    public class ETHConfig : IniConfig
    {
        /// <summary>
        /// TRDP使用
        /// </summary>
        public ETHConfig()
            : base(Application.StartupPath + "\\config\\trdp.ini")
        {

            this.Load();
        }


        public ETHConfig(string ininame)
        {
            iniName = ininame;
            this.Name = ininame;
        }

        /// <summary>
        /// 根据ini名称加载不同 ini文件
        /// </summary>
        public static string iniName = "trdp";


        private string _Name;

        /// <summary>
        /// Name
        /// </summary>
        protected string Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
                this.Filename = Application.StartupPath + "\\config\\" + iniName + ".ini";
                this.Load();
            }
        }

        /// <summary>
        /// 发送数据时 发送端口 0：ETH0  1:ETH1
        /// </summary>
        public int ETH { get; set; }



    }
}
