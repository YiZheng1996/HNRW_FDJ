using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.CompilerServices;
using System.Collections;
using System.Data;

namespace MainUI.Helper
{
    public class CSVHelper
    {
        /// <summary>
        /// 创建文件。目录不存在则创建目录。
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string CreateFile(string fileName)
        {
            FileStream fs = null;
            try
            {
                //目录文件夹不存在，则创建文件夹
                string root = Path.GetDirectoryName(fileName);
                if (Directory.Exists(root) == false)
                    Directory.CreateDirectory(root);

                if (File.Exists(fileName) == false)
                    fs = File.Create(fileName);
            }
            catch (Exception ex)
            { }
            finally
            {
                if (fs != null)
                {
                    fs.Dispose();
                }
            }
            return fileName;
        }

        /// <summary>
        ///  保存数据到csv文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool Save(string path, object[] data)
        {
            bool ok = true;
            StreamWriter sw = null;

            try
            {
                //此处的true代表续写，false代表覆盖
                sw = new StreamWriter(path, true, Encoding.GetEncoding("GB2312"));
                string txt = "";
                for (int i = 0; i < data.Length; i++)
                {
                    txt += data[i] + ",";
                }
                txt = txt.TrimEnd(',');
                sw.WriteLine(txt);
            }
            catch (Exception ex)
            {
                //如果写文件时，文件被打开则无法正常写入数据。
                ok = false;
            }
            finally
            {
                if (sw != null)
                    sw.Dispose();
            }
            return ok;


        }

        public static List<string> ReadAsList(string path)
        {
            if (!File.Exists(path))
                return new List<string>();
            List<string> lstObj = new List<string>();
            StreamReader sr = null;
            try
            {
                sr = new StreamReader(path, Encoding.GetEncoding("GB2312"));
                string str = string.Empty;
                while ((str = sr.ReadLine()) != null)
                {
                    lstObj.Add(str);
                }
            }
            catch (Exception ex)
            {
                return lstObj;
            }
            finally
            {
                if (sr != null)
                    sr.Dispose();
            }
            return lstObj;
        }

       

        public static DataTable ReadAsDatatable(string path)
        {
            DataTable dt = new DataTable();
            if (!File.Exists(path))
                return null;
            List<string> lstObj = new List<string>();
            StreamReader sr = null;
            try
            {
                sr = new StreamReader(path, Encoding.GetEncoding("GB2312"));
                string firstRow = sr.ReadLine();
                string[] colAry = firstRow.Split(',');
                for (int i = 0; i < colAry.Length; i++)
                {
                    dt.Columns.Add(new DataColumn(colAry[i]));
                }
              
                string str = string.Empty;
                while ((str = sr.ReadLine()) != null)
                {
                    DataRow row = dt.NewRow();
                    string[] contentAry = str.Split(',');
                    for (int i = 0; i < contentAry.Length; i++)
                    {
                        row[i] = contentAry[i];
                    }
                    dt.Rows.Add(row);
                }
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                throw new Exception(err);
            }
            finally
            {
                if (sr != null)
                    sr.Dispose();
            }

            return dt;
        }


        //  dicFault = MiniExcelLibs.MiniExcel.Query<FaultView>("故障解析表.xlsx").ToDictionary(x => x.FaultCode);

    }



}
