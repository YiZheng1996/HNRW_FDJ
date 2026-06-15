using MainUI.FSql.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
