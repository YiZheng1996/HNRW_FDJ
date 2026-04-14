
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MainUI.FSql
{
    public class TestParaService
    {
        /// <summary>
        /// 保存试验记录
        /// </summary>
        /// <param name="modelType"></param>
        /// <returns>受影响的行数</returns>
        /// <exception cref="Exception"></exception>
        public int Save(TestPara testPara)
        {
            List<TestPara> listTestPara = new List<TestPara>();
            int result = 0;
            try
            {

                result = DB.mysql.Insert<TestPara>(testPara).ExecuteAffrows();
                return result;
            }
            catch (Exception ex)
            {

                throw new Exception("试验记录保存失败！原因： " + ex.Message);
            }
        }
        /// <summary>
        /// 查询所有试验记录
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllKind()
        {
            DataTable dt = new DataTable();
            string sql = "SELECT `Index`,`TestName`,`TestStage`,`TestCycle`,`TestStep`, `DataTime`, `Time`, `HourNum`, `AT`, `AP`, `AH`, `RPM`, `Torque`, `Power`, `WaterTemp1`, `WaterTemp2`, `WaterTemp3`, `WaterTemp4`, `WaterTemp5`, `WaterTemp6`, `Pressure1`, `Pressure2`, `Pressure3`, `Flow1`, `Flow2`, `EOTemp1`, `EOTemp2`, `EOTemp3`, `EOPressure1`, `EOPressure2`, `EOAnalysis`, `EOConsumption`, `AirTemp1`, `AirTemp2`, `AirPressure1`, `AirPressure2`, `AirPressure3`, `RPM2`, `EGTemp1`, `EGTemp2`, `EGTemp3`, `EGTemp4`, `EGPressure1`, `EGPressure2`, `EGPressure3`, `Smoke`, `ECOTime`, `ECOQuantity`, `ECORate`, `OilTemp`, `Para`FROM TestPara;";
            dt = DB.mysql.Select<TestPara>()
                            .WithSql(sql)
                            .ToDataTable("*");
            return dt;
                
        }
        /// <summary>
        /// 通过试验阶段查询记录
        /// </summary>
        /// <returns></returns>
        public DataTable GetDataByTestStage(string TestStage)
        {
            DataTable dt = new DataTable();
            string sql = $"SELECT `Index`, `TestName`, `TestStage`, `TestCycle`, `TestStep`, `DataTime`, `Time`, `HourNum`, `AT`, `AP`, `AH`, `RPM`, `Torque`, `Power`, `WaterTemp1`, `WaterTemp2`, `WaterTemp3`, `WaterTemp4`, `WaterTemp5`, `WaterTemp6`, `Pressure1`, `Pressure2`, `Pressure3`, `Flow1`, `Flow2`, `EOTemp1`, `EOTemp2`, `EOTemp3`, `EOPressure1`, `EOPressure2`, `EOAnalysis`, `EOConsumption`, `AirTemp1`, `AirTemp2`, `AirPressure1`, `AirPressure2`, `AirPressure3`, `RPM2`, `EGTemp1`, `EGTemp2`, `EGTemp3`, `EGTemp4`, `EGPressure1`, `EGPressure2`, `EGPressure3`, `Smoke`, `ECOTime`, `ECOQuantity`, `ECORate`, `OilTemp`, `Para` FROM TestPara WHERE `TestStage` = '{TestStage}'";
            dt = DB.mysql.Select<TestPara>()
                            .WithSql(sql)
                            .ToDataTable("*");
            return dt;
        }
        /// <summary>
        /// 通过试验阶段和周期查找记录
        /// </summary>
        /// <param name="TestStage">试验阶段</param>
        /// <param name="TestCycle">试验周期</param>
        /// <returns></returns>
        public DataTable GetDataByTestCycle(string TestStage,string TestCycle)
        {
            DataTable dt = new DataTable();
            string sql = $"SELECT `Index`, `TestName`, `TestStage`, `TestCycle`, `TestStep`, `DataTime`, `Time`, `HourNum`, `AT`, `AP`, `AH`, `RPM`, `Torque`, `Power`, `WaterTemp1`, `WaterTemp2`, `WaterTemp3`, `WaterTemp4`, `WaterTemp5`, `WaterTemp6`, `Pressure1`, `Pressure2`, `Pressure3`, `Flow1`, `Flow2`, `EOTemp1`, `EOTemp2`, `EOTemp3`, `EOPressure1`, `EOPressure2`, `EOAnalysis`, `EOConsumption`, `AirTemp1`, `AirTemp2`, `AirPressure1`, `AirPressure2`, `AirPressure3`, `RPM2`, `EGTemp1`, `EGTemp2`, `EGTemp3`, `EGTemp4`, `EGPressure1`, `EGPressure2`, `EGPressure3`, `Smoke`, `ECOTime`, `ECOQuantity`, `ECORate`, `OilTemp`, `Para` FROM TestPara WHERE `TestStage` = '{TestStage}' AND `TestCycle` = '{TestCycle}'";
            dt = DB.mysql.Select<TestPara>()
                            .WithSql(sql)
                            .ToDataTable("*");
            return dt;
        }
        /// <summary>
        /// 通过试验阶段，试验周期，和循环节点查找记录
        /// </summary>
        /// <param name="TestStage">试验阶段</param>
        /// <param name="TestCycle">试验周期</param>
        /// <param name="TestStep">循环节点</param>
        /// <returns></returns>
        public DataTable GetDataByTestStep(string TestStage, string TestCycle,string TestStep)
        {
            DataTable dt = new DataTable();
            string sql = $"SELECT `Index`, `TestName`, `TestStage`, `TestCycle`, `TestStep`, `DataTime`, `Time`, `HourNum`, `AT`, `AP`, `AH`, `RPM`, `Torque`, `Power`, `WaterTemp1`, `WaterTemp2`, `WaterTemp3`, `WaterTemp4`, `WaterTemp5`, `WaterTemp6`, `Pressure1`, `Pressure2`, `Pressure3`, `Flow1`, `Flow2`, `EOTemp1`, `EOTemp2`, `EOTemp3`, `EOPressure1`, `EOPressure2`, `EOAnalysis`, `EOConsumption`, `AirTemp1`, `AirTemp2`, `AirPressure1`, `AirPressure2`, `AirPressure3`, `RPM2`, `EGTemp1`, `EGTemp2`, `EGTemp3`, `EGTemp4`, `EGPressure1`, `EGPressure2`, `EGPressure3`, `Smoke`, `ECOTime`, `ECOQuantity`, `ECORate`, `OilTemp`, `Para` FROM TestPara WHERE `TestStage` = '{TestStage}' AND `TestCycle` = '{TestCycle}' AND `TestStep` = '{TestCycle}'";
            dt = DB.mysql.Select<TestPara>()
                            .WithSql(sql)
                            .ToDataTable("*");
            return dt;
        }
        ///// <summary>
        ///// 修改型号类别
        ///// </summary>
        ///// <param name="oldModelTypeName">原来的类别名称</param>
        ///// <param name="newModelTypeName">类别名称将修改为</param>
        ///// <returns>受影响的行数</returns>
        ///// <exception cref="Exception"></exception>
        //public int Update(string oldModelTypeName, string newModelTypeName)
        //{
        //    int result = 0;
        //    try
        //    {
        //        result = DB.mysql.Update<ModelType>().Set(d => d.ModelTypeName, newModelTypeName).Where(d => d.ModelTypeName == oldModelTypeName).ExecuteAffrows();
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {

        //        throw new Exception("型号类别信息更新失败" + ex.Message);
        //    }
        //}
        //public int Delete(string modelTypeName)
        //{
        //    int result = 0;
        //    try
        //    {
        //        result = DB.mysql.Delete<ModelType>().Where(d =>d.ModelTypeName == modelTypeName).ExecuteAffrows();
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {

        //        throw new Exception("型号类别信息删除失败" + ex.Message);
        //    }

        //}
    }
}
