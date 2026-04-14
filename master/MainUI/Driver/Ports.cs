using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;

namespace MetorSignalSimulator.UI.Model
{
    /// <summary>
    /// 端口的数据模型
    /// </summary>
    public class Ports : BaseModel
    {
        public Ports()
        {

        }

        public Ports(DataRow row)
        {
            Init(row);
        }
        /// <summary>
        /// 显示ID
        /// </summary>
        public long ID { get; set; }
        /// <summary>
        /// 端口显示名称
        /// </summary>
        public string PortName { get; set; }
        /// <summary>
        /// 扫描周期（ms）
        /// </summary>
        public int Rate { get; set; }
        /// <summary>
        /// 端口
        /// </summary>
        public string Port { get; set; }

        public int PortNum { get { return Convert.ToInt32(Port, 16); } }

        public int PortNum1 { get { return int.Parse(Port); } }
        /// <summary>
        /// 通讯方式，0为以太网，1为MVB
        /// </summary>
        public int CommMode { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        /// 是否是读命令
        /// </summary>
        public bool IsRead { get; set; }

        /// <summary>
        /// 是否使用
        /// </summary>
        public bool IsUse { get; set; }

        /// <summary>
        /// 数据长度，一般MVB限值最大为32字节，网卡不限
        /// </summary>
        public int DataSize { get; set; }

        //以太网组播地址
        public string MulticastAddress { get; set; }

        public int SMIValue { get; set; }
        public string duankoumingcheng { get; set; }
        }

    /// <summary>
    /// 基础模块类型
    /// </summary>
    public class BaseModel
    {
        /// <summary>
        /// 将DataRow的名称，反射到实体
        /// </summary>
        /// <param name="row"></param>
        public virtual void Init(DataRow row)
        {
            PropertyInfo[] members = this.GetType().GetProperties();
            foreach (var item in members)
            {
                if (row.Table.Columns.Contains(item.Name))
                {
                    object value = Convert.ChangeType(row[item.Name], item.PropertyType);
                    item.SetValue(this, value, null);
                }
            }
        }
    }
}
