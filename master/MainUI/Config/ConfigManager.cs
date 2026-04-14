using System;
using System.Collections.Generic;
using System.Text;

namespace MainUI.Config
{
    public class ConfigManager
    {
        private static string model;
        public static string Model { get { return model; } set { model = value; } }

        /// <summary>
        /// 故障列表
        /// </summary>
        private static FaultConfig faultConfig;
        public static FaultConfig FaultConfig
        {
            get 
            {
                return null;
            }
        
            //get
            //{
            //    if (faultConfig == null)
            //        //faultConfig = new FaultConfig();
            //    return faultConfig;
            //}
        }

    }
}
