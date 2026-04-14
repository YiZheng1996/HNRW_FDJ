using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MainUI.Config;

namespace MainUI.Fault
{
    public partial class frmWarnParaConfig : Form
    {
        // 型号对应报警参数配置
        //FaultConfig fc = null;

        Dictionary<string, NumericUpDown> dicValue = new Dictionary<string, NumericUpDown>();
        public frmWarnParaConfig()
        {
            InitializeComponent();
            if (this.DesignMode) return;
            BuildValue();
        }

        // 修改 BuildValue 方法
        void BuildValue()
        {
            // 确保 FaultDataLists 至少有一个元素
            if (Var.FaultConfig.FaultDataLists == null || Var.FaultConfig.FaultDataLists.Count == 0)
            {
                // 如果没有数据，初始化一个默认的 FaultData
                Var.FaultConfig.FaultDataLists.Add(new FaultData());
            }

            FaultData data = Var.FaultConfig.FaultDataLists[0];

            // 故障1: 高温水出水温度
            this.F1V1.Value = data.F1V1;
            this.F1V2.Value = data.F1V2;

            // 故障2: 中冷水进水温度
            this.F2V1.Value = data.F2V1;

            // 故障3: 中冷水出水温度
            this.F3V1.Value = data.F3V1;

            // 故障6: 后中冷后空气温度
            this.F6V1.Value = data.F6V1;

            // 故障7: 主油道进口油温
            this.F7V1.Value = data.F7V1;

            // 故障8: 前压气机出口空气温度
            this.F8V1.Value = data.F8V1;

            // 故障9: 后压气机出口空气温度
            this.F9V1.Value = data.F9V1;

            // 故障11: 主油道进口油压
            this.F11V1.Value = data.F11V1;
            this.F11V2.Value = data.F11V2;

            // 故障14.1: 燃油精滤器后油压
            this.F141V1.Value = data.F141V1;
            this.F141V2.Value = data.F141V2;
            this.F141V3.Value = data.F141V3;
            this.F141V4.Value = data.F141V4;

            // 故障14.2: 燃油精滤器后前后压差大于100 kPa
            this.F142V1.Value = data.F142V1;
            this.F142V2.Value = data.F142V2;

            // 故障17: 机油泵出口油温
            this.F17V1.Value = data.F17V1;
            this.F17V2.Value = data.F17V2;

            // 故障18: 主油道末端油压
            this.F18V1.Value = data.F18V1;
            this.F18V2.Value = data.F18V2;
            this.F18V3.Value = data.F18V3;
            this.F18V4.Value = data.F18V4;
            this.F18V5.Value = data.F18V5;
            this.F18V6.Value = data.F18V6;
            this.F18V7.Value = data.F18V7;
            this.F18V8.Value = data.F18V8;
            this.F18V9.Value = data.F18V9;
            this.F18V10.Value = data.F18V10;
            this.F18V11.Value = data.F18V11;
            this.F18V12.Value = data.F18V12;
            this.F18V13.Value = data.F18V13;

            // 故障20: 后增压器进油压
            this.F20V1.Value = data.F20V1;
            this.F20V2.Value = data.F20V2;

            // 故障22: 前增压器转速
            this.F22V1.Value = data.F22V1;
            this.F22V2.Value = data.F22V2;

            // 故障23: 后增压器转速
            this.F23V1.Value = data.F23V1;
            this.F23V2.Value = data.F23V2;

            // 故障24: AI-A6缸排气温度
            this.F24V1.Value = data.F24V1;
            this.F24V2.Value = data.F24V2;
            this.F24V3.Value = data.F24V3;
            this.F24V4.Value = data.F24V4;

            // 故障25: BI-B6缸排气温度
            this.F25V1.Value = data.F25V1;
            this.F25V2.Value = data.F25V2;
            this.F25V3.Value = data.F25V3;
            this.F25V4.Value = data.F25V4;

            // 故障26: A涡前排气温度
            this.F26V1.Value = data.F26V1;

            // 故障27: B涡前排气温度
            this.F27V1.Value = data.F27V1;

            // 故障28: I-7档轴温
            this.F28V1.Value = data.F28V1;
            this.F28V2.Value = data.F28V2;
            this.F28V3.Value = data.F28V3;

            // 故障29: 轴温检测装置通讯异常
            this.F29V1.Value = data.F29V1;

            // 故障30: 电喷转速1
            this.F30V1.Value = data.F30V1;

            // 故障31: 电喷转速2
            this.F31V1.Value = data.F31V1;

            // 故障32: 电喷故障
            this.F32V1.Value = data.F32V1;

            // 故障33: 发动机转速1 飞轮
            this.F33V1.Value = data.F33V1;

            // 故障34: 发动机转速2 飞轮
            this.F34V1.Value = data.F34V1;

            // 故障35: 后增进油压卸载开关
            this.F35V1.Value = data.F35V1;

            // 故障36: 后增进油压停机开关
            this.F36V1.Value = data.F36V1;

            // 故障37: 曲轴箱差压开关
            this.F37V1.Value = data.F37V1;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.btnSave.Focus();

            try
            {
                // 确保 FaultDataLists 不为空
                if (Var.FaultConfig.FaultDataLists == null)
                {
                    Var.FaultConfig.FaultDataLists = new List<FaultData>();
                }

                // 如果列表为空，先添加一个 FaultData 对象
                if (Var.FaultConfig.FaultDataLists.Count == 0)
                {
                    Var.FaultConfig.FaultDataLists.Add(new FaultData());
                }

                // 创建新的 FaultData 对象并赋值
                FaultData data = new FaultData
                {
                    F1V1 = this.F1V1.Value.ToInt(),
                    F1V2 = this.F1V2.Value.ToInt(),
                    F2V1 = this.F2V1.Value.ToInt(),
                    F3V1 = this.F3V1.Value.ToInt(),
                    F6V1 = this.F6V1.Value.ToInt(),
                    F7V1 = this.F7V1.Value.ToInt(),
                    F8V1 = this.F8V1.Value.ToInt(),
                    F9V1 = this.F9V1.Value.ToInt(),
                    F11V1 = this.F11V1.Value.ToInt(),
                    F11V2 = this.F11V2.Value.ToInt(),
                    F141V1 = this.F141V1.Value.ToInt(),
                    F141V2 = this.F141V2.Value.ToInt(),
                    F141V3 = this.F141V3.Value.ToInt(),
                    F141V4 = this.F141V4.Value.ToInt(),
                    F142V1 = this.F142V1.Value.ToInt(),
                    F142V2 = this.F142V2.Value.ToInt(),
                    F17V1 = this.F17V1.Value.ToInt(),
                    F17V2 = this.F17V2.Value.ToInt(),
                    F18V1 = this.F18V1.Value.ToInt(),
                    F18V2 = this.F18V2.Value.ToInt(),
                    F18V3 = this.F18V3.Value.ToInt(),
                    F18V4 = this.F18V4.Value.ToInt(),
                    F18V5 = this.F18V5.Value.ToInt(),
                    F18V6 = this.F18V6.Value.ToInt(),
                    F18V7 = this.F18V7.Value.ToInt(),
                    F18V8 = this.F18V8.Value.ToInt(),
                    F18V9 = this.F18V9.Value.ToInt(),
                    F18V10 = this.F18V10.Value.ToInt(),
                    F18V11 = this.F18V11.Value.ToInt(),
                    F18V12 = this.F18V12.Value.ToInt(),
                    F18V13 = this.F18V13.Value.ToInt(),
                    F20V1 = this.F20V1.Value.ToInt(),
                    F20V2 = this.F20V2.Value.ToInt(),
                    F22V1 = this.F22V1.Value.ToInt(),
                    F22V2 = this.F22V2.Value.ToInt(),
                    F23V1 = this.F23V1.Value.ToInt(),
                    F23V2 = this.F23V2.Value.ToInt(),
                    F24V1 = this.F24V1.Value.ToInt(),
                    F24V2 = this.F24V2.Value.ToInt(),
                    F24V3 = this.F24V3.Value.ToInt(),
                    F24V4 = this.F24V4.Value.ToInt(),
                    F25V1 = this.F25V1.Value.ToInt(),
                    F25V2 = this.F25V2.Value.ToInt(),
                    F25V3 = this.F25V3.Value.ToInt(),
                    F25V4 = this.F25V4.Value.ToInt(),
                    F26V1 = this.F26V1.Value.ToInt(),
                    F27V1 = this.F27V1.Value.ToInt(),
                    F28V1 = this.F28V1.Value.ToInt(),
                    F28V2 = this.F28V2.Value.ToInt(),
                    F28V3 = this.F28V3.Value.ToInt(),
                    F29V1 = this.F29V1.Value.ToInt(),
                    F30V1 = this.F30V1.Value.ToInt(),
                    F31V1 = this.F31V1.Value.ToInt(),
                    F32V1 = this.F32V1.Value.ToInt(),
                    F33V1 = this.F33V1.Value.ToInt(),
                    F34V1 = this.F34V1.Value.ToInt(),
                    F35V1 = this.F35V1.Value.ToInt(),
                    F36V1 = this.F36V1.Value.ToInt(),
                    F37V1 = this.F37V1.Value.ToInt()
                };

                // 替换第一个元素
                Var.FaultConfig.FaultDataLists[0] = data;

                // 保存配置
                Var.FaultConfig.Save();

                // 更新故障检测
                Var.FaultService.GetCurrentFaultData();
            }
            catch (Exception ex)
            {
                Var.LogInfo($"保存报警参数配置时出错: {ex.Message}");
                Var.MsgBoxWarn(this, $"保存失败: {ex.Message}");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
