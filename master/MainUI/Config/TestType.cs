namespace MainUI.Config
{
    /// <summary>
    /// 试验类型与 段名 / 持久化字符串 的互转
    /// </summary>
    public static class TestTypeHelper
    {
        /// <summary>
        ///  {model}.ini 段名
        /// </summary>
        /// <param name="type">枚举类型</param>
        /// <returns></returns>
        public static string ToSection(this TestType type)
        {
            return type == TestType.型式 ? "型式试验" : "例行试验";
        }

        /// <summary>
        /// 持久化字符串（存进 SysParas）
        /// </summary>
        /// <param name="type">枚举</param>
        /// <returns></returns>
        public static string ToConfigString(this TestType type)
        {
            return type == TestType.型式 ? "型式" : "例行";
        }

        /// <summary>
        /// 空或无法识别一律回退“例行”（= 现状）
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static TestType Parse(string s)
        {
            return s == "型式" ? TestType.型式 : TestType.例行;
        }
    }

    /// <summary>
    /// 试验类型：例行试验 / 型式试验
    /// </summary>
    public enum TestType
    {
        例行 = 0,
        型式 = 1
    }

}