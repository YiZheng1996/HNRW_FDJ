using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using RW.Modules;
using static MainUI.Modules.EventArgsModel;

namespace MainUI.Modules
{
    /// <summary>
    /// 3102Q智能称重（KEPServer：励磁信息.Value1，Modbus TCP，独立OPCDriver）
    /// </summary>
    public class Weight3102QGrp : BaseSensorGroup
    {
        public ConcurrentDictionary<string, double> DataValue { get; }
            = new ConcurrentDictionary<string, double>();

        public ConcurrentDictionary<string, bool> StatusValue { get; }
            = new ConcurrentDictionary<string, bool>();

        public Weight3102QGrp()
        {
            this.Driver = Var.opcWeight3102Q;
        }

        public Weight3102QGrp(IContainer container) : base(container) { }

        protected override void InitComponts()
        {
            this.Driver = Var.opcWeight3102Q;
        }

        [Description("数值点变化后触发（含换算后的显示重量kg）")]
        public event EventHandler<DoubleValueChangedEventArgs> KeyValueChange;

        /// <summary>true 正常；false 异常</summary>
        public bool NoError { get; private set; } = true;

        /// <summary>true 模拟；false 非模拟</summary>
        public bool Simulated { get; private set; } = false;

        public bool IsNoError => NoError;
        public bool IsSimulated => Simulated;

        /// <summary>显示重量工程值 kg（OPC「显示重量16位原始值」单位为 g，÷1000）</summary>
        public double WeightKg
        {
            get { return GetRaw("显示重量16位原始值") / 1000.0; }
        }

        /// <summary>皮重工程值 kg</summary>
        public double TareWeightKg
        {
            get { return ScaleRaw(GetRaw("皮重重量原始值"), GetDecimalPlaces()); }
        }

        /// <summary>实际重量工程值 kg</summary>
        public double ActualWeightKg
        {
            get { return ScaleRaw(GetRaw("实际重量原始值"), GetDecimalPlaces()); }
        }

        public double this[string key]
        {
            get
            {
                if (string.Equals(key, "显示重量", StringComparison.Ordinal)
                    || string.Equals(key, "WeightKg", StringComparison.Ordinal))
                {
                    return WeightKg;
                }
                return GetRaw(key);
            }
        }

        public void Fresh()
        {
            RefreshDerivedWeight();
            foreach (var item in DataValue)
            {
                KeyValueChange?.Invoke(this, new DoubleValueChangedEventArgs(item.Key, item.Value));
            }
        }

        public override void Init()
        {
            // 前缀 Var.opcWeight3102Q = 励磁信息.Value1.
            string[] numericTags = new string[]
            {
                "显示重量16位原始值",
                "小数点位值",
                "显示重量32位原始值",
                "皮重重量原始值",
                "实际重量原始值",
                "状态输出字",
                "累计次数",
                "继电器参数A",
                "继电器参数B",
                "继电器参数C",
                "继电器参数D",
                "零区重量E",
                "允差F",
                "允差U",
            };

            foreach (var tag in numericTags)
            {
                DataValue.AddOrUpdate(tag, 0, (k, oldValue) => 0);
                string opcTag = tag;
                this.Register<double>(opcTag, value =>
                {
                    DataValue.AddOrUpdate(opcTag, value, (k, oldValue) => value);
                    if (opcTag == "显示重量16位原始值"
                        || opcTag == "显示重量32位原始值"
                        || opcTag == "小数点位值"
                        || opcTag == "皮重重量原始值"
                        || opcTag == "实际重量原始值")
                    {
                        RefreshDerivedWeight();
                    }
                    else
                    {
                        KeyValueChange?.Invoke(this, new DoubleValueChangedEventArgs(opcTag, value));
                    }
                });
            }

            string[] statusTags = new string[]
            {
                "状态_显示重量去皮",
                "状态_显示重量稳定",
                "状态_显示重量零区",
                "状态_放料进程",
                "状态_继电器1输出",
                "状态_继电器2输出",
                "状态_实际重量输出",
                "状态_运行输出",
                "状态_标定使能",
                "状态_零点标定进行",
                "状态_量程标定进行",
                "报警_传感器信号太小",
                "报警_量程标定重量小",
            };

            foreach (var tag in statusTags)
            {
                StatusValue.AddOrUpdate(tag, false, (k, oldValue) => false);
                string opcTag = tag;
                this.Register<bool>(opcTag, value =>
                {
                    StatusValue.AddOrUpdate(opcTag, value, (k, oldValue) => value);
                    KeyValueChange?.Invoke(this, new DoubleValueChangedEventArgs(opcTag, value ? 1 : 0));
                });
            }

            this.Register<bool>("_System._NoError", value => { NoError = value; });
            this.Register<bool>("_System._Simulated", value => { Simulated = value; });

            DataValue.AddOrUpdate("显示重量", 0, (k, oldValue) => 0);
            base.Init();
        }

        #region 写命令

        /// <summary>控制命令字：1清零、2去皮、4取消去皮、8启停</summary>
        public void WriteControlCommand(ushort command)
        {
            this.Write("控制命令字", (double)command);
        }

        public void ClearZero() => WriteControlCommand(1);
        public void Tare() => WriteControlCommand(2);
        public void CancelTare() => WriteControlCommand(4);
        public void StartStop() => WriteControlCommand(8);

        /// <summary>标定使能：写136有效，结束后写0</summary>
        public void SetCalibrationEnable(bool enable)
        {
            this.Write("标定使能", enable ? 136d : 0d);
        }

        public void WriteZeroCalibration()
        {
            this.Write("零点标定", 0d);
        }

        public void WriteSpanCalibration(short weight)
        {
            this.Write("量程标定", (double)weight);
        }

        public void WriteRelayParamA(int raw) => this.Write("继电器参数A", (double)raw);
        public void WriteRelayParamB(int raw) => this.Write("继电器参数B", (double)raw);
        public void WriteRelayParamC(int raw) => this.Write("继电器参数C", (double)raw);
        public void WriteRelayParamD(int raw) => this.Write("继电器参数D", (double)raw);
        public void WriteZeroZoneE(short raw) => this.Write("零区重量E", (double)raw);
        public void WriteToleranceF(short raw) => this.Write("允差F", (double)raw);
        public void WriteToleranceU(short raw) => this.Write("允差U", (double)raw);

        #endregion

        private double GetRaw(string key)
        {
            return DataValue.TryGetValue(key, out var v) ? v : 0;
        }

        private int GetDecimalPlaces()
        {
            int dp = (int)Math.Round(GetRaw("小数点位值"));
            if (dp < 0) dp = 0;
            if (dp > 3) dp = 3;
            return dp;
        }

        private static double ScaleRaw(double raw, int decimalPlaces)
        {
            double divisor = Math.Pow(10, decimalPlaces);
            if (divisor <= 0) return raw;
            return Math.Round(raw / divisor, decimalPlaces + 1);
        }

        private void RefreshDerivedWeight()
        {
            double kg = WeightKg;
            DataValue.AddOrUpdate("显示重量", kg, (k, oldValue) => kg);
            KeyValueChange?.Invoke(this, new DoubleValueChangedEventArgs("显示重量", kg));
            KeyValueChange?.Invoke(this,
                new DoubleValueChangedEventArgs("显示重量16位原始值", GetRaw("显示重量16位原始值")));
            KeyValueChange?.Invoke(this,
                new DoubleValueChangedEventArgs("显示重量32位原始值", GetRaw("显示重量32位原始值")));
            KeyValueChange?.Invoke(this,
                new DoubleValueChangedEventArgs("小数点位值", GetRaw("小数点位值")));
        }
    }
}