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
        public string GroupName { get; set; }
        /// <summary>
        /// 查询时按勾选模块顺序动态赋值，用于表头着色
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
