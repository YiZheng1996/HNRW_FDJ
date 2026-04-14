using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainUI.Wave
{
    /// <summary>
    /// 曲线记录类
    /// </summary>
    public class WaveReocrd
    {
        /// <summary>
        /// 整张曲线图名称
        /// </summary>
        public string ReocrdName { get; set; }

        /// <summary>
        /// 当前曲线类型（false：历史曲线  true：实时曲线）
        /// </summary>
        public bool CurrentType { get; set; }

        /// <summary>
        /// 曲线的条数数据
        /// </summary>
        public List<WaveData> WaveDataPoints { get; set; } = new List<WaveData>();
    }

    /// <summary>
    /// 单个数据点
    /// </summary>
    public class WaveData
    {
        /// <summary>
        /// 整张曲线图名称
        /// </summary>
        public string CurveName { get; set; }

        /// <summary>
        /// 数据点集合 (时间-数值)
        /// </summary>
        public List<DataPoint> DataPoints { get; set; } = new List<DataPoint>();
    }

    /// <summary>
    /// 单个数据点
    /// </summary>
    public class DataPoint
    {
        /// <summary>
        /// X轴时间
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// 数值
        /// </summary>
        public double Value { get; set; }
    }
}
