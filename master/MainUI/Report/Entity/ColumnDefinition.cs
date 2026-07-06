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
        //增加一个条件给列第一行加背景颜色，区分项点
        public int Tag_num { get; set; }
        public Type SourceType { get; set; }
        public PropertyInfo PropertyInfo { get; set; }

        public ColumnDefinition(string propertyName, string displayName)
        {
            PropertyName = propertyName;
            DisplayName = displayName;
            SourceType = typeof(TestParaAllData);
        } 
        

        public ColumnDefinition(string propertyName, string displayName,int tag_num)
        {
            PropertyName = propertyName;
            DisplayName = displayName;
            Tag_num = tag_num;
            SourceType = typeof(TestParaAllData);
        }
    }
}
