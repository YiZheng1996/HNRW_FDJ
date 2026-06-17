namespace MainUI.Global
{

    public static class TrialTypeExtensions
    {
        /// <summary>
        /// 显示名称，用于UI和日志
        /// </summary>
        /// <param name="trialType"></param>
        /// <returns></returns>
        public static string DisplayName(this TrialTypeEnum trialType)
        {
            switch (trialType)
            {
                case TrialTypeEnum.RoutineTest: return "例行试验";
                case TrialTypeEnum.TypeTest: return "型式试验";
                default: return trialType.ToString();
            }
        }

        /// <summary>
        /// section 后缀，用于读取 {model}_例行 / {model}_型式
        /// </summary>
        /// <param name="trialType"></param>
        /// <returns></returns>
        public static string SectionSuffix(this TrialTypeEnum trialType)
        {
            return trialType == TrialTypeEnum.RoutineTest ? "例行" : "型式";
        }
    }


    /// <summary>
    /// 试验类型
    /// </summary>
    public enum TrialTypeEnum
    {
        /// <summary>
        /// 例行试验
        /// </summary>
        RoutineTest = 0,

        /// <summary>
        /// 型式试验
        /// </summary>
        TypeTest = 1
    }
}