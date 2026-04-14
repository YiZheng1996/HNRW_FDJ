using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainUI.Global
{
    public static class EventTriggerModel
    {
        // 定义事件
        public static event Action<string> OnModelNameChanged;

        /// <summary>
        /// 型号更新时
        /// </summary>
        /// <param name="modelName"></param>
        public static void RaiseOnModelNameChanged(string modelName)
        {
            OnModelNameChanged?.Invoke(modelName);
        }

 
        public static event Action<string> OnParaNameChanged;
        /// <summary>
        /// 参数管理型号更新时
        /// </summary>
        /// <param name="modelName"></param>
        public static void ParaModelChanged(string modelName)
        {
            OnParaNameChanged?.Invoke(modelName);
        }

        public static event Action<bool> OnScramChanged;
        /// <summary>
        /// 急停状态更新时
        /// </summary>
        /// <param name="modelName"></param>
        public static void ScramChanged(bool status)
        {
            OnScramChanged?.Invoke(status);
        }


        public static event Action<string> OnModelNumberChanged;
        /// <summary>
        /// 发动机编号更新时
        /// </summary>
        /// <param name="modelName"></param>
        public static void ModelNumberChanged(string number)
        {
            OnModelNumberChanged?.Invoke(number);
        }

        public static event Action<bool> OnStartupChanged;
        /// <summary>
        /// 启机触发
        /// </summary>
        /// <param name="modelName"></param>
        public static void StartupChanged(bool status)
        {
            OnStartupChanged?.Invoke(status);
        }

        public static event Action<double> OnTimngChanged;
        /// <summary>
        /// 计时触发
        /// </summary>
        /// <param name="modelName"></param>
        public static void TimngChanged(double timer)
        {
            OnTimngChanged?.Invoke(timer);
        }
    }
}
