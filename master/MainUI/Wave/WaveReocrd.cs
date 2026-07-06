using System;
using System.Collections.Generic;

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

        /// <summary>
        /// 本段曲线的X轴起始时间（工况开始时记录，用于将Timestamp还原为分钟数）
        /// </summary>
        public DateTime WaveStartTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 历史工况存档（每次工况切换时把当前数据快照到这里）
        /// </summary>
        public List<WaveSegment> HistorySegments { get; set; } = new List<WaveSegment>();
    }

    /// <summary>
    /// 一段工况的历史快照
    /// </summary>
    public class WaveSegment
    {
        /// <summary>
        /// 循环代码，如 A / B / C
        /// </summary>
        public string CycleCode { get; set; }

        /// <summary>
        /// 这段工况的X轴起始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 数据点快照（深拷贝，与实时线解耦）
        /// </summary>
        public List<DataPoint> DataPoints { get; set; } = new List<DataPoint>();
    }

    /// <summary>
    /// 单个数据点
    /// </summary>
    public class WaveData
    {
        /// <summary>
        /// 曲线名称
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