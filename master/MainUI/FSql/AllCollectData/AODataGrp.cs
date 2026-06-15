using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainUI.FSql.AllCollectData
{
    public class AODataGrp
    {
        //AO
        /// <summary>
        /// 排气风道右调节阀控制
        /// </summary>
        public double AirOutChannelValve { get; set; }

        /// <summary>
        /// 设置发动机最低转速
        /// </summary>
        public double EngineLowSpeed { get; set; }


        /// <summary>
        /// 水泵出口电动调节阀控制-18
        /// </summary>
        public double WaterOutValve_18 { get; set; }


        /// <summary>
        /// 发动机油门调节
        /// </summary>
        public double EngineOilValve { get; set; }


        /// <summary>
        /// 排气风道左调节阀控制
        /// </summary>
        public double AirOutFlowValveLeft { get; set; }


        /// <summary>
        /// _励磁调节
        /// </summary>
        public double Excitation { get; set; }


        /// <summary>
        /// 燃油泵旁路1电动调节阀控制-194
        /// </summary>
        public double AO_OilBy_Pass1Valve_194 { get; set; }

        /// <summary>
        /// 进气风道左调节阀控制
        /// </summary>
        public double AirInFlowleftValve { get; set; }

        /// <summary>
        /// 燃油泵1电动调节阀控制-170
        /// </summary>
        public double Oil1Vlave_170 { get; set; }

        /// <summary>
        /// 进气风道右调节阀控制
        /// </summary>
        public double AirFlowRightValve { get; set; }

        /// <summary>
        /// 水阻箱进水电动调节阀
        /// </summary>
        public double WaterResistanceBoxValve { get; set; }

    }
}
