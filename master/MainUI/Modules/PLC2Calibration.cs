using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RW.Modules;

namespace MainUI.Modules
{
    /// <summary>
    ///  PLC2(控制柜) 硬件较准类
    /// </summary>
    public partial class PLCCalibration2 : BaseModule
    {
        public PLCCalibration2()
        {
            InitializeComponent();
        }

        public PLCCalibration2(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
        protected override void InitComponts()
        {
            base.InitComponts();
            this.Driver = Var.opcAI2Modbus;
        }

        const int chCountAI = 71;

        const int chCountAO = 0;

        private double[] _plczeroAI = new double[chCountAI];
        private double[] _plcgainAI = new double[chCountAI];
        private double[] _plczeroAO = new double[chCountAO];
        private double[] _plcgainAO = new double[chCountAO];

        /// <summary>
        /// 返回零点值
        /// </summary>
        public double[] PlcZero
        {
            get
            { return _plczeroAI; }
        }

        public double[] Plcgain
        {
            get
            { return _plcgainAI; }
        }

        /// <summary>
        /// AI 标签
        /// </summary>
        /// <returns></returns>
        public List<string> GetAITag()
        {
            List<string> items = new List<string>() {
                "AI.A1缸排气温度",
                "AI.A2缸排气温度",
                "AI.A3缸排气温度",
                "AI.A4缸排气温度",
                "AI.A5缸排气温度",
                "AI.A6缸排气温度",
                "AI.A7缸排气温度",
                "AI.A8缸排气温度",
                "AI.B1缸排气温度",
                "AI.B2缸排气温度",
                "AI.B3缸排气温度",
                "AI.B4缸排气温度",
                "AI.B5缸排气温度",
                "AI.B6缸排气温度",
                "AI.B7缸排气温度",
                "AI.B8缸排气温度",
                "AI.P1高温水出机压力",
                "AI.P20机油泵出口压力",
                "AI.P21主油道进口油压",
                "AI.P2高温水泵进口压力",
                "AI.P38燃油供油压力",
                "AI.P3中冷水泵进口压力",
                "AI.P5中冷水出机压力",
                "AI.T1高温水出机温度",
                "AI.T20机油泵出口油温",
                "AI.T21主油道进口油温",
                "AI.T2高温水进机温度",
                "AI.T30燃油回油温度",
                "AI.T31燃油泵进口油温",
                "AI.T3中冷水进机温度",
                "AI.T5中冷水出机温度",
                "AI.高温水泵出口压力",
                "AI.后中冷前空气温度",
                "AI.后涡轮出口废气温度",
                "AI.后涡轮进口废气温度",
                "AI.后涡轮进口废气压力",
                "AI.后增压器机油出口温度",
                "AI.后增压器机油进口温度",
                "AI.后增压器机油进口压力",
                "AI.后增压器进气温度",
                "AI.后增压器进气真空度",
                "AI.后增压器排气背压",
                "AI.后中冷后空气温度",
                "AI.后中冷后空气压力",
                "AI.后中冷前空气压力",
                "AI.机油耗测量压力",
                "AI.机油耗测量液位",
                "AI.前涡轮出口废气温度",
                "AI.前涡轮进口废气温度",
                "AI.前涡轮进口废气压力",
                "AI.前增压器机油出口温度",
                "AI.前增压器机油进口温度",
                "AI.前增压器机油进口压力",
                "AI.前增压器进气温度",
                "AI.前增压器进气真空度",
                "AI.前增压器排气背压",
                "AI.前中冷后空气温度",
                "AI.前中冷后空气压力",
                "AI.前中冷前空气温度",
                "AI.前中冷前空气压力",
                "AI.中冷器出口水温",
                "AI.中冷器进口水温",
                "AI.中冷水泵出口压力",
                "AI.主油道末端油压",
                "AI.测功机U相温度",
                "AI.测功机V相温度",
                "AI.测功机W相温度",
                "AI.测功机D相温度",
                "AI.测功机N相温度",
                "AI.励磁电压检测",
                "AI.励磁电流检测",
            };
            return items;
        }

        ///// <summary>
        ///// AO 标签
        ///// </summary>
        ///// <returns></returns>
        //public List<string> GetAOTag()
        //{
        //    List<string> lst = new List<string>();
        //    //0-4
        //    lst.Add("AO.燃油泵1电动调节阀控制-170");
        //    lst.Add("AO.燃油泵旁路1电动调节阀控制-194");
        //    lst.Add("AO.水泵出口电动调节阀控制-18");
        //    lst.Add("AO.进气风道左调节阀控制");
        //    //5-7
        //    lst.Add("AO.进气风道右调节阀控制");
        //    lst.Add("AO.排气风道左调节阀控制");
        //    lst.Add("AO.排气风道右调节阀控制");
        //    return lst;
        //}

        public override void Init()
        {
            List<string> lstTag = GetAITag();
            for (int i = 0; i < chCountAI; i++)    //注册AI增溢键值
            {
                int temp = i;
                string opcTag = lstTag[i] + "零点";
                this.Register<double>(opcTag, delegate (double value)
                {
                    _plczeroAI[temp] = value;
                });
            }

            for (int i = 0; i < chCountAI; i++)
            {
                int temp = i;
                string opcTag = lstTag[i] + "增益";
                this.Register<double>(opcTag, delegate (double value)
                {
                    _plcgainAI[temp] = value;

                });
            }

            //List<string> lstAOTag = GetAOTag();
            //for (int i = 0; i < chCountAO; i++)    //注册AI增溢键值
            //{
            //    int temp = i;
            //    string opcTag = lstAOTag[i] + "零点";
            //    this.Register<double>(opcTag, delegate (double value)
            //    {
            //        _plczeroAO[temp] = value;

            //    });
            //}

            //for (int i = 0; i < chCountAO; i++)
            //{
            //    int temp = i;
            //    string opcTag = lstAOTag[i] + "增益";
            //    this.Register<double>(opcTag, delegate (double value)
            //    {
            //        _plcgainAO[temp] = value;

            //    });
            //}
            base.Init();
        }

        /// <summary>
        /// AI零点写值操作
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
        public void SetAIZero(string key, double value)
        {
            string opcTag = "AI." + key + "零点";
            this.Write(opcTag, value);
        }


        /// <summary>
        /// AI增溢写值操作
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
        public void SetAIGain(string key, double value)
        {
            string opcTag = "AI." + key + "增益";
            this.Write(opcTag, value);
        }

        /// <summary>
        /// AO零点写值操作
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
        public void SetAOZero(string key, double value)
        {
            string opcTag = "AO." + key + "零点";
            this.Write(opcTag, value);
        }

        /// <summary>
        /// AO增溢写值操作
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
        public void SetAOGain(string key, double value)
        {
            string opcTag = "AO." + key + "增益";
            this.Write(opcTag, value);
        }
    }
}
