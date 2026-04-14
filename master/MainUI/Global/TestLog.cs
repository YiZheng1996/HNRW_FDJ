using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainUI.Global
{
    public static class TestLog
    {
        private static string _testLog = string.Empty;
        public static event EventHandler<LogChangedEventArgs> LogChanged;
        public static string WritetLog
        {
            get => _testLog;
            set
            {
                if (_testLog != value)
                {
                    _testLog = value;
                    OnStaticPropertyChanged(nameof(WritetLog), value);
                }
            }
        }
        public static void UpdateTestPara(string newValue)
        {
            WritetLog = newValue;
        }
        private static void OnStaticPropertyChanged(string propertyName, object newValue)
        {
            LogChanged?.Invoke(null, new LogChangedEventArgs(propertyName, newValue));
        }
        
    }
    // 自定义事件参数类
    public class LogChangedEventArgs : EventArgs
    {
        public string PropertyName { get; }
        public object NewValue { get; }

        public LogChangedEventArgs(string propertyName, object newValue)
        {
            PropertyName = propertyName;
            NewValue = newValue;
        }
    }
}
