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
    public class DOModulesConfig : IniConfig
    {
        public DOModulesConfig()
          : base(Application.StartupPath + "\\config\\DO.ini")
        {
            this.SetSectionName("DOModulesConfig");
            this.Load();
        }


        /// <summary>
        /// 点位列表名称
        /// </summary>
        public List<string> Tags { get; set; } = new List<string>
        {
            "Y03阀控制",
            "Y100阀控制",
            "Y111阀控制",
            "Y115阀控制",
            "Y116阀控制",
            "Y122阀控制",
            "Y133阀控制",
            "Y134阀控制",
            "Y135阀控制",
            "Y136阀控制",
            "Y137阀控制",
            "Y139阀控制",
            "Y15阀控制",
            "Y164阀控制",
            "Y16阀控制",
            "Y179阀控制",
            "Y17阀控制",
            "Y181阀控制",
            "Y182阀控制",
            "Y183阀控制",
            "Y184阀控制",
            "Y190阀控制",
            "Y20阀控制",
            "Y21阀控制",
            "Y22阀控制",
            "Y23阀控制",
            "Y24阀控制",
            "Y26阀控制",
            "Y27阀控制",
            "Y28阀控制",
            "Y29阀控制",
            "Y30阀控制",
            "Y31阀控制",
            "Y41阀控制",
            "Y61阀控制",
            "Y90阀控制",
            "Y91阀控制",
            "Y92阀控制",
            "Y93阀控制",
            "Y95阀控制",
            "Y96阀控制",
            "Y97阀控制",
            "预热水箱加热控制",
            "燃油泵1合闸控制",
            "燃油泵2合闸控制",
            "预热水泵合闸控制",
            "污油排出泵合闸控制",
            "预供机油泵合闸控制",
            "抽油泵合闸控制",
            "主发通风机1合闸控制",
            "主发通风机2合闸控制",
            "水阻上升控制",
            "水阻下降控制",
            "发动机DC24V供电",
            "发动机启停预启动",
            "蜂鸣器控制",
            "水阻箱调节阀开",
            "水阻箱调节阀关",
            "故障复位",
            "1号右排气管阀2",
            "1号右排气管阀1",
            "1号右进气管阀2",
            "1号右进气管阀1",
            "2号左排气管阀2",
            "2号左排气管阀1",
            "2号左进气管阀2",
            "2号左进气管阀1"
        };


    }
}
