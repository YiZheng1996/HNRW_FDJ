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
    ///  硬件较准类
    /// </summary>
    public partial class PLCCalibration : BaseModule
    {
        public PLCCalibration()
        {
            InitializeComponent();
        }

        public PLCCalibration(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
        protected override void InitComponts()
        {
            base.InitComponts();
            this.Driver = Var.opcAIGroup;
        }

        const int chCountAI = 17;

        const int chCountAO = 10;

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
            List<string> lst = new List<string>();
            // 0-3
            lst.Add("AI.大气温度");
            lst.Add("AI.大气湿度");
            lst.Add("AI.大气压力");
            lst.Add("AI.机油流量");
            // 4-7
            lst.Add("AI.清洁油罐来油流量");
            lst.Add("AI.燃油进油流量测量-L30");
            lst.Add("AI.燃油回油流量测量-L31");
            lst.Add("AI.L32");
            // 8-11
            lst.Add("AI.高温水流量测量-L3");
            lst.Add("AI.中冷水流量测量-L8");
            lst.Add("AI.进气流量测量左");
            lst.Add("AI.进气流量测量右");
            // 12 - 16
            lst.Add("AI.水阻箱进水调节阀开度");
            lst.Add("AI.前增压器进气流量计前温度");
            lst.Add("AI.后增压器进气流量计前温度");
            lst.Add("AI.厂房进气压力检测1");
            // 17 - 19
            lst.Add("AI.厂房进气压力检测2");
            return lst;
        }

        /// <summary>
        /// AO 标签
        /// </summary>
        /// <returns></returns>
        public List<string> GetAOTag()
        {
            List<string> lst = new List<string>();
            //0-4
            lst.Add("AO.燃油泵1电动调节阀控制-170");
            lst.Add("AO.燃油泵旁路1电动调节阀控制-194");
            lst.Add("AO.水阻箱进水电动调节阀");
            lst.Add("AO.水泵出口电动调节阀控制-18");

            //5-8
            lst.Add("AO.进气风道左调节阀控制");
            lst.Add("AO.进气风道右调节阀控制");
            lst.Add("AO.排气风道左调节阀控制");
            lst.Add("AO.排气风道右调节阀控制");

            //9-10 （新增）
            lst.Add("AO.励磁调节");
            lst.Add("AO.发动机油门调节");
            return lst;
        }

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

            List<string> lstAOTag = GetAOTag();
            for (int i = 0; i < chCountAO; i++)    //注册AI增溢键值
            {
                int temp = i;
                string opcTag = lstAOTag[i] + "零点";
                this.Register<double>(opcTag, delegate (double value)
                {
                    _plczeroAO[temp] = value;

                });
            }

            for (int i = 0; i < chCountAO; i++)
            {
                int temp = i;
                string opcTag = lstAOTag[i] + "增益";
                this.Register<double>(opcTag, delegate (double value)
                {
                    _plcgainAO[temp] = value;

                });
            }
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
