using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainUI.FSql.AllCollectData
{
    public class ExChangeDataGrpBool
    {
        [JsonProperty("ExChangeGrpBool_高温水预热循环")]
        public bool HighTempWaterPreheatCycle { get; set; }

        [JsonProperty("ExChangeGrpBool_燃油耗测量油泵选择")]
        public bool FuelConsumptionMeasurePumpSelect { get; set; }

        [JsonProperty("ExChangeGrpBool_油底壳抽油选择油箱")]
        public bool OilSumpExtractSelectTank { get; set; }

        [JsonProperty("ExChangeGrpBool_燃油循环")]
        public bool FuelCycle { get; set; }

        [JsonProperty("ExChangeGrpBool_油底壳抽油")]
        public bool OilSumpExtract { get; set; }

        [JsonProperty("ExChangeGrpBool_高温水中冷水回抽")]
        public bool HighTempWaterColdWaterBackExtract { get; set; }

        [JsonProperty("ExChangeGrpBool_机油箱加油")]
        public bool OilTankRefuel { get; set; }

        [JsonProperty("ExChangeGrpBool_上位机停机控制")]
        public bool UpperComputerShutdownCtrl { get; set; }

        [JsonProperty("ExChangeGrpBool_预供机油循环")]
        public bool PreSupplyOilCycle { get; set; }

        [JsonProperty("ExChangeGrpBool_预热水箱加热")]
        public bool PreHeatTankHeating { get; set; }

        [JsonProperty("ExChangeGrpBool_燃油耗测量")]
        public bool FuelConsumptionMeasure { get; set; }

        [JsonProperty("ExChangeGrpBool_油底壳加油")]
        public bool OilSumpRefuel { get; set; }

        [JsonProperty("ExChangeGrpBool_机油回抽")]
        public bool OilBackExtract { get; set; }

        [JsonProperty("ExChangeGrpBool_预热水箱加水")]
        public bool PreHeatTankWaterAdd { get; set; }

        [JsonProperty("ExChangeGrpBool_燃油箱回油冷却")]
        public bool FuelTankReturnOilCool { get; set; }

        [JsonProperty("ExChangeGrpBool_燃油循环油泵选择")]
        public bool FuelCyclePumpSelect { get; set; }

        [JsonProperty("ExChangeGrpBool_机油加热处理循环")]
        public bool OilHeatProcessCycle { get; set; }
    }
    
}
