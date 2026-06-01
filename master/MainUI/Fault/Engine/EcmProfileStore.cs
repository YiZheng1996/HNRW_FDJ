using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web.Script.Serialization;   // .NET Framework 自带(System.Web.Extensions)
using System.Windows.Forms;

namespace MainUI.Fault.Engine
{
    /// <summary>
    /// 型号故障配置(JSON)的读写仓库。
    /// 职责：
    ///   1) 给参数编辑器/引擎提供同一份反序列化后的 EcmFaultProfile（唯一真相源）。
    ///   2) 仅把工艺员改动的“阈值数字”写回 JSON，不改判据结构。
    /// 设计原则：与现有代码完全解耦——240 不会用到本类；只有放了 {型号}.faults.json 的型号才走这里。
    /// 不引入第三方库：用 .NET 自带的 JavaScriptSerializer。
    /// </summary>
    public static class EcmProfileStore
    {
        private static readonly JavaScriptSerializer _ser = new JavaScriptSerializer();

        /// <summary>配置目录：与 TRDPConfig 同级的 FaultProfiles。</summary>
        public static string ProfileDir
        {
            get { return Path.Combine(Application.StartupPath, "FaultProfiles"); }
        }

        /// <summary>某型号 JSON 的完整路径。</summary>
        public static string PathOf(string model)
        {
            return Path.Combine(ProfileDir, model + ".faults.json");
        }

        /// <summary>该型号是否存在 JSON 配置（存在=由数据驱动引擎接管）。</summary>
        public static bool Exists(string model)
        {
            if (string.IsNullOrEmpty(model)) return false;
            return File.Exists(PathOf(model));
        }

        /// <summary>读取并反序列化某型号配置；不存在或解析失败返回 null。</summary>
        public static EcmFaultProfile Load(string model)
        {
            try
            {
                string p = PathOf(model);
                if (!File.Exists(p)) return null;
                string json = File.ReadAllText(p, Encoding.UTF8);
                return _ser.Deserialize<EcmFaultProfile>(json);
            }
            catch (Exception ex)
            {
                try { Var.LogInfo("加载故障配置失败 " + model + ": " + ex.Message); } catch { }
                return null;
            }
        }

        /// <summary>
        /// 把内存中的 profile 序列化写回该型号 JSON（带缩进，便于人工查看/版本管理）。
        /// 仅在参数编辑器“保存”时调用。写前自动备份原文件为 .bak。
        /// </summary>
        public static bool Save(EcmFaultProfile profile)
        {
            if (profile == null || string.IsNullOrEmpty(profile.Model)) return false;
            try
            {
                if (!Directory.Exists(ProfileDir)) Directory.CreateDirectory(ProfileDir);
                string p = PathOf(profile.Model);

                // 备份
                if (File.Exists(p))
                {
                    string bak = p + ".bak";
                    File.Copy(p, bak, true);
                }

                string raw = _ser.Serialize(profile);
                string pretty = JsonPrettify(raw);
                File.WriteAllText(p, pretty, new UTF8Encoding(false)); // 无BOM
                return true;
            }
            catch (Exception ex)
            {
                try { Var.LogInfo("保存故障配置失败 " + profile.Model + ": " + ex.Message); } catch { }
                return false;
            }
        }

        /// <summary>把 JavaScriptSerializer 输出的紧凑 JSON 缩进美化。</summary>
        private static string JsonPrettify(string json)
        {
            var sb = new StringBuilder();
            int indent = 0;
            bool inStr = false;
            Action<int> newline = (n) =>
            {
                sb.Append('\n');
                sb.Append(' ', n * 2);
            };
            for (int i = 0; i < json.Length; i++)
            {
                char c = json[i];
                if (inStr)
                {
                    sb.Append(c);
                    if (c == '\\' && i + 1 < json.Length) { sb.Append(json[++i]); }
                    else if (c == '"') inStr = false;
                    continue;
                }
                switch (c)
                {
                    case '"': inStr = true; sb.Append(c); break;
                    case '{':
                    case '[':
                        sb.Append(c); indent++; newline(indent); break;
                    case '}':
                    case ']':
                        indent--; newline(indent); sb.Append(c); break;
                    case ',':
                        sb.Append(c); newline(indent); break;
                    case ':':
                        sb.Append(": "); break;
                    default:
                        if (!char.IsWhiteSpace(c)) sb.Append(c);
                        break;
                }
            }
            return sb.ToString();
        }
    }
}