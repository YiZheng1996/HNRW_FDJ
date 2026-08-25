using MainUI.FSql.Model;
using System;
using System.Reflection;

namespace MainUI.Report
{

    /// <summary>
    /// 测试数据列字段
    /// pym
    /// </summary>
    public class ColumnDefinition
    {
        public string PropertyName { get; set; }
        public string DisplayName { get; set; }
        /// <summary>所属数据模块名（如 TRDPDataGrp），用于表头分组着色与 JSON 取值</summary>
        public string GroupName { get; set; }
        /// <summary>
        /// 表头颜色编号：0=基础列不着色；&gt;0 时从 HeaderColorPalette 取 (Tag_num-1) 对应颜色
        /// </summary>
        public int Tag_num { get; set; }
        public Type SourceType { get; set; }
        public PropertyInfo PropertyInfo { get; set; }

        public ColumnDefinition(string propertyName, string displayName)
        {
            PropertyName = propertyName;
            DisplayName = displayName;
            SourceType = typeof(TestParaAllData);
        }
    }
}
