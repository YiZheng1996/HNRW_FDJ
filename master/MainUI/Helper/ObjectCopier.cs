using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MainUI.Helper
{
    /// <summary>
    /// 对象帮助类
    /// </summary>
    public static class ObjectCopier
    {
        /// <summary>
        /// 浅拷贝集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static T DeepCopy<T>(T data)
        {
            if (data == null) return default(T);
            string json = JsonConvert.SerializeObject(data);
            return JsonConvert.DeserializeObject<T>(json);
        }

        /// <summary>
        /// 验证输入是否为有效数字
        /// </summary>
        public static bool IsValidNumber(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return false;

            // 检查是否包含中文字符
            foreach (char c in input)
            {
                if (c >= 0x4E00 && c <= 0x9FA5) // Unicode中文字符范围
                {
                    return false;
                }
            }

            // 尝试转换为double
            double result;
            return double.TryParse(input, out result);
        }
    }
}
