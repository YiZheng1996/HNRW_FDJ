using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainUI.FSql.AllCollectData
{
    public class ExChangeDataGrpDouble
    {
        [JsonProperty("ExChangeGrpDouble_预热水箱温度")]
        public double PreHeatTankTemperature { get; set; }

        [JsonProperty("ExChangeGrpDouble_预热水箱液位")]
        public double PreHeatTankLevel { get; set; }

        [JsonProperty("ExChangeGrpDouble_高温水出机温度")]
        public double HighTempWaterOutMachineTemperature { get; set; }

        [JsonProperty("ExChangeGrpDouble_机油出机压力")]
        public double OilOutMachinePressure { get; set; }

        [JsonProperty("ExChangeGrpDouble_柴油机转速")]
        public double DieselEngineRotateSpeed { get; set; }

        [JsonProperty("ExChangeGrpDouble_预热水箱加热温度设定")]
        public double PreHeatTankHeatTempSet { get; set; }

        [JsonProperty("ExChangeGrpDouble_机油箱温度")]
        public double OilTankTemperature { get; set; }

        [JsonProperty("ExChangeGrpDouble_燃油进机压力")]
        public double FuelInMachinePressure { get; set; }

        [JsonProperty("ExChangeGrpDouble_机油箱液位")]
        public double OilTankLevel { get; set; }

        [JsonProperty("ExChangeGrpDouble_内循环水箱液位")]
        public double InnerCircWaterTankLevel { get; set; }

        [JsonProperty("ExChangeGrpDouble_待处理机油箱温度")]
        public double PendingOilTankTemperature { get; set; }

        [JsonProperty("ExChangeGrpDouble_高温水进机温度")]
        public double HighTempWaterInMachineTemperature { get; set; }

        [JsonProperty("ExChangeGrpDouble_燃油进机温度")]
        public double FuelInMachineTemperature { get; set; }

        [JsonProperty("ExChangeGrpDouble_待处理机油箱液位")]
        public double PendingOilTankLevel { get; set; }

        [JsonProperty("ExChangeGrpDouble_机油进机压力")]
        public double OilInMachinePressure { get; set; }

        [JsonProperty("ExChangeGrpDouble_燃油箱液位")]
        public double FuelTankLevel { get; set; }
    }
}
