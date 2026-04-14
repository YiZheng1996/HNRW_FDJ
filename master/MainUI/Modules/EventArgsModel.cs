using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainUI.Modules
{
    /// <summary>
    /// 模块事件类
    /// </summary>
    public class EventArgsModel
    {

        // 自定义Bool类型事件
        public class DIValueChangedEventArgs : EventArgs
        {
            public string Key { get; }
            public bool Value { get; }

            public DIValueChangedEventArgs(string key, bool value)
            {
                Key = key;
                Value = value;
            }
        }

        // 自定义Doube类型事件
        public class DoubleValueChangedEventArgs : EventArgs
        {
            public string Key { get; }
            public double Value { get; }

            public DoubleValueChangedEventArgs(string key, double value)
            {
                Key = key;
                Value = value;
            }
        }

        // 自定义字符串更改事件
        public class StringValueChangedEventArgs : EventArgs
        {
            public string Key { get; }
            public string Value { get; }

            public StringValueChangedEventArgs(string key, string value)
            {
                Key = key;
                Value = value;
            }
        }

    }
}
