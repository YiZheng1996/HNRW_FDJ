using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BogieIdling.UI
{
    /// <summary>
    /// 数据的范围
    /// 类型1：有效,无效
    /// 类型1：1=有效,0=无效
    /// 类型2：1=通风模式,2=全自动模式,3=手动冷模式,4=手动暖模式,5=停机
    /// 类型3：0~65535
    /// 类型4：empty
    /// </summary>
    public class DataRange
    {
        public DataRange()
        {
            Init();
        }

        public DataRange(string text)
        {
            this.Text = text;
            Init();

            string[] arr = text.Split(',');
            if (arr.Length > 1)
            {
                for (int i = 0; i < arr.Length; i++)
                {
                    string[] items = arr[i].Split('=');

                    int key = i;
                    string value = "";

                    if (items.Length > 1)
                    {
                        key = Convert.ToInt32(items[0]);
                        value = items[1];
                    }
                    else
                    {
                        value = items[0];
                        key = i;
                    }
                    this.Items[key] = value;
                }
            }
            else
            {
                try
                {
                    string[] r = arr[0].Split('~');
                    if (r.Length > 1)
                    {
                        this.Min = Convert.ToInt32(r[0]);
                        this.Max = Convert.ToInt32(r[1]);
                    }
                }
                catch (Exception ex)
                {
                    
                    throw ex;
                }

            }

        }

        public void Init()
        {
            this.Items = new Dictionary<int, string>();
        }

        public string Text { get; set; }

        public int Min { get; set; }
        public int Max { get; set; }

        public Dictionary<int, string> Items { get; set; }
    }
}
