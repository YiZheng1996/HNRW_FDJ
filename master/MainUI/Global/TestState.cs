using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainUI.Global
{
    public static class TestState
    {

        // 静态属性
        private static string _TestName = "NULL";
        private static string _TestCycle = "NULL";
        private static string _TestStep = "NULL";

        // 静态事件，用于通知属性变更
        public static event EventHandler<PropertyChangedEventArgs> StaticPropertyChanged;

        public static string TestName
        {
            get => _TestName;
            set
            {
                if (_TestName != value)
                {
                    _TestName = value;
                    OnStaticPropertyChanged(nameof(TestName), value);
                }
            }
        }

        public static string TestCycle
        {
            get => _TestCycle;
            set
            {
                if (_TestCycle != value)
                {
                    _TestCycle = value;
                    OnStaticPropertyChanged(nameof(TestCycle), value);
                }
            }
        }

        public static string TestStep
        {
            get => _TestStep;
            set
            {

                _TestStep = value;
                OnStaticPropertyChanged(nameof(TestStep), value);

            }
        }

        // 触发静态属性变更事件的方法
        private static void OnStaticPropertyChanged(string propertyName, object newValue)
        {
            StaticPropertyChanged?.Invoke(null, new PropertyChangedEventArgs(propertyName, newValue));
        }

        // 批量更新方法
        public static void UpdateAll(string val1, string val2, string val3)
        {
            TestName = val1;
            TestCycle = val2;
            TestStep = val3;
        }
    }

    // 自定义事件参数类
    public class PropertyChangedEventArgs : EventArgs
    {
        public string PropertyName { get; }
        public object NewValue { get; }

        public PropertyChangedEventArgs(string propertyName, object newValue)
        {
            PropertyName = propertyName;
            NewValue = newValue;
        }
    }


}

