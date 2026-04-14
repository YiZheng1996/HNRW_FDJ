using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MetorSignalSimulator.UI.SocketFile
{
    [Table(Name = "Agreement")]
    public class AgreementData
    {
        /// <summary>
        /// ID
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 协议名称（数据名称）
        /// </summary>
        public string signalName { get; set; }

        /// <summary>
        /// 字偏移
        /// </summary>
        public int byteOffset { get; set; }

        /// <summary>
        /// 一般为空，若为0.1，1代表0.1，采集数*0.1
        /// </summary>
        public string dataFormat { get; set; }

        /// <summary>
        /// 原始高
        /// </summary>
        public int RawHight { get; set; }

        /// <summary>
        /// 原始低
        /// </summary>
        public int RawLow { get; set; }

        /// <summary>
        /// 缩放高
        /// </summary>
        public int ScaledHight { get; set; }

        /// <summary>
        /// 缩放低
        /// </summary>
        public int ScaledLow { get; set; }

        /// <summary>
        /// 位偏移
        /// </summary>
        public int binaryOffset { get; set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        public string dataType { get; set; }

        /// <summary>
        /// 车辆名称
        /// </summary>
        public string vhecileNo { get; set; }

        /// <summary>
        /// 车辆号
        /// </summary>
        public string carNo { get; set; }

        /// <summary>
        /// 适用智能工装字段的扩展，端口号
        /// </summary>
        public string yuLiu2 { get; set; } = "";

        /// <summary>
        /// 适用智能工装字段的扩展，端口大小
        /// </summary>
        public string yuLiu3 { get; set; } = "";

        /// <summary>
        /// 适用智能工装字段的扩展，通讯周期
        /// </summary>
        public string yuLiu4 { get; set; } = "";

        /// <summary>
        /// 适用智能工装字段的扩展，通讯类型
        /// </summary>
        public string yuLiu5 { get; set; } = "";
        /// <summary>
        /// 适用智能工装字段的扩展，源宿设备
        /// </summary>
        public string yuLiu6 { get; set; } = "";

        /// <summary>
        /// 适用智能工装字段的扩展，组播地址
        /// </summary>
        public string yuLiu7 { get; set; } = "";


        /// <summary>
        /// 适用智能工装字段的扩展，SMI值
        /// </summary>
        public string yuLiu8 { get; set; } = "";

        /// <summary>
        /// 适用智能工装字段的扩展，本机IP
        /// </summary>
        public string yuLiu9 { get; set; } = "";

        /// <summary>
        /// 适用智能工装字段的扩展，故障分类
        /// </summary>
        public string yuLiu10 { get; set; } = "";

        /// <summary>
        /// 适用智能工装字段的扩展，端口号中文名称
        /// </summary>
        public string yuLiu11 { get; set; } = "";

        /// <summary>
        /// 硬件协议_航插类型
        /// </summary>
        public string yuLiu14 { get; set; } = "";
        /// <summary>
        /// 硬线协议_航插针脚
        /// </summary>
        public string yuLiu15 { get; set; } = "";
        /// <summary>
        /// 硬线协议_显示信息(车辆航插脚位)
        /// </summary>
        public string yuLiu16 { get; set; } = "";
        /// <summary>
        /// 硬线协议_显示信息(信号名)
        /// </summary>
        public string yuLiu17 { get; set; } = "";
        /// <summary>
        /// 硬线协议_是否输出
        /// </summary>
        public string yuLiu18 { get; set; } = "0";
    }
}
