namespace MainUI.Report.Entity
{
    public class ManualReportHeaderInfo
    {
        public string TestProject { get; set; }        // 试验项目
        public string SuperchargerModel { get; set; }   // 增压器型号
        public string SuperchargerSN { get; set; }       // 增压器出厂编号
        public string TestBenchNo { get; set; }          // 试验台位号
        public string MainGeneratorNo { get; set; }       // 主发电机编号
        public double AvgOutsideTemp { get; set; }        // 平均外温
        public double AvgAtmPressure { get; set; }        // 平均大气压力
        public double Humidity { get; set; }               // 相对湿度
        public string OilGrade { get; set; }                 // 机油牌号
        public string FuelGrade { get; set; }                 // 燃油牌号
    }
}
