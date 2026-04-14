using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RW.UI.Manager;
using MainUI.Model;
using MainUI.BLL;
using MainUI.Config;
using MainUI.Properties;
using MainUI.Config.Test;
using MainUI.Procedure.Test;
using MainUI.Global;
using Sunny.UI;
using MainUI.Helper;
using static MainUI.Config.PubConfig;

namespace MainUI.Procedure
{
    public partial class ucTestParams : ucBaseManagerUI
    {

        // 全局常量定义
        private const double MAX_TORQUE = 110.0; // 最大扭矩
        private const double MAX_RPM = 110.0; // 最大转速
        private const double MAX_RUN_TIME = 9999.0; //最长时间

        // 试验基础参数
        ParaConfig paraconfig = new ParaConfig();

        // 360小时流程配置参数
        Test360hConfig durabilityTestConfig = new Test360hConfig();

        // 360小时 循环代码 配置参数
        DurStepConfig durStepConfig { get; set; }

        public ucTestParams()
        {
            InitializeComponent();
        }

        private void ucTestParams_Load(object sender, EventArgs e)
        {
            uc360hParams1.LoadSysConfig();
        }

        /// <summary>
        /// 产品选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnProductSelection_Click(object sender, EventArgs e)
        {
            frmSpec fs = new frmSpec();
            fs.ShowDialog();
            txtType.Text = fs.SelectModelType;
            txtModel.Text = fs.SelectModel;
            LoadConfig();
        }

        /// <summary>
        /// 数据初始化
        /// </summary>
        private void LoadConfig()
        {
            try
            {
                listTestStep.Text = "A";

                // 加载基础参数
                paraconfig = new ParaConfig();
                paraconfig.SetSectionName(txtModel.Text);
                paraconfig.Load(paraconfig.Filename);

                this.numRatePower.Value = paraconfig.RatedPower.ToDecimal();
                this.numRateTorque.Value = paraconfig.RatedTorque.ToDecimal();
                this.numMinSpeed.Value = paraconfig.MinSpeed.ToDecimal();
                this.numRateSpeed.Value = paraconfig.RatedSpeed.ToDecimal();
                this.numOilPanLong.Value = paraconfig.OilPanLong.ToDecimal();
                this.numOilPanWide.Value = paraconfig.OilPanWide.ToDecimal();
                this.numOilPanHeight.Value = paraconfig.OilPanHeight.ToDecimal();

                this.TorqueChangeTimeValue.Value = paraconfig.IntervalTime;
                this.TorqueChangeMultValue.Value = paraconfig.TorqueChangeMultiple;
                this.RPMChangeMultValue.Value = paraconfig.RPMChangeMultiple;

                // 加载 表格试验参数
                uc100hParams1.LoadGridView(txtModel.Text);

                // 360小时（循环代码）
                uc360hParams1.LoadGridView(txtModel.Text);

                // 励磁调节参数
                ucExcitationRegulationParams1.LoadGridView(txtModel.Text);

                // 加载360小时 循环节点配置
                LoadGridViewNode();

                // 自动试验升功率/降功率配置
                //uc360hParams1.LoadGridView(txtModel.Text);

                // 加载配方参数
                LoadPubConfig();
            }
            catch (Exception ex)
            {
                Var.MsgBoxWarn(this, "数据加载失败：" + ex.Message);
            }
        }

        /// <summary>
        /// 加载360小时 循环代码配置
        /// </summary>
        private void LoadGridViewNode()
        {
            // 加载性能试验表格
            durStepConfig = new DurStepConfig(this.txtModel.Text.ToString(), listTestStep.Text);
            this.dgvNode.Rows.Clear();
            var para = durStepConfig.testBasePara;
            for (int i = 0; i < para.Count; i++)
            {
                this.dgvNode.Rows.Add(para[i].Index, para[i].GKNo, para[i].Torque, para[i].RPM, para[i].RunTime);
            }
        }

        /// <summary>
        /// 确定
        /// </summary>
        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtModel.Text))
                {
                    Var.MsgBoxWarn(this, "请选择型号后再保存");
                    return;
                }

                // 保存发动机基础数据
                paraconfig.SetSectionName(txtModel.Text);
                paraconfig.RatedPower = this.numRatePower.Value.ToInt();
                paraconfig.RatedTorque = this.numRateTorque.Value.ToInt();
                paraconfig.RatedSpeed = this.numRateSpeed.Value.ToInt();
                paraconfig.MinSpeed = this.numMinSpeed.Value.ToInt();
                paraconfig.OilPanLong = this.numOilPanLong.Value.ToInt();
                paraconfig.OilPanWide = this.numOilPanWide.Value.ToInt();
                paraconfig.OilPanHeight = this.numOilPanHeight.Value.ToInt();

                paraconfig.IntervalTime = this.TorqueChangeTimeValue.Value.ToInt();
                paraconfig.TorqueChangeMultiple = this.TorqueChangeMultValue.Value.ToInt();
                paraconfig.RPMChangeMultiple = this.RPMChangeMultValue.Value.ToInt();

                paraconfig.Save();

                btnSavePub_Click(null, null);

                Var.MsgBoxSuccess(this, "保存成功。");

                // 型号一致时刷新
                if (Var.SysConfig.LastModel == txtModel.Text) 
                {
                    EventTriggerModel.ParaModelChanged(txtModel.Text);
                }
            }
            catch (Exception ex)
            {
                Var.MsgBoxWarn(this, "保存载失败：" + ex.Message);
            }
        }

        /// <summary>
        /// 重置
        /// </summary>
        private void btnReset_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtModel.Text))
                LoadConfig();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.InitialDirectory = Application.StartupPath + "\\配置文件\\";
            openFile.Filter = "Excel2007+|*.xlsx";
            openFile.ShowDialog();
            if (string.IsNullOrEmpty(openFile.FileName) == false)
            {
                string path = openFile.FileName;
                string[] str = path.Split('\\');
                //txtConfigFile.Text = str[str.Length - 1];
            }
        }

        // 在 ucTestParams 类中添加以下方法

        #region 360小时循环代码
        /// <summary>
        /// 获取下一个节点序号
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        private int GetNextNodeIndex(DurStepConfig config)
        {
            if (config.testBasePara.Count == 0)
                return 1;

            return config.testBasePara.Max(p => p.Index) + 1;
        }

        /// <summary>
        /// 360小时 具体循环代码 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn360Add_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtModel.Text))
            {
                Var.MsgBoxWarn(this, "请先选择型号");
                return;
            }

            if (listTestStep.SelectedItem == null)
            {
                Var.MsgBoxWarn(this, "请先选择循环代码");
                return;
            }

            string nodeName = listTestStep.SelectedItem.ToString();
            double speedPercent = uiDoubleUpDown1.Value;
            double torquePercent = uiDoubleUpDown2.Value;
            double runTime = uiDoubleUpDown3.Value;

            if (runTime <= 0)
            {
                Var.MsgBoxWarn(this, "运行时间不能为空");
                return;
            }

            try
            {
                DurStepConfig config = new DurStepConfig(txtModel.Text, nodeName);

                TestBasePara newPara = new TestBasePara
                {
                    Index = GetNextNodeIndex(config),
                    RPM = speedPercent,
                    Torque = torquePercent,
                    RunTime = runTime,
                    StepName = nodeName
                };

                config.testBasePara.Add(newPara);
                config.Save();

                LoadGridViewNode();
                Var.MsgBoxSuccess(this, "添加成功！");

                // 清空输入框
                uiDoubleUpDown1.Value = 0;
                uiDoubleUpDown2.Value = 0;
                uiDoubleUpDown3.Value = 0;
            }
            catch (Exception ex)
            {
                Var.MsgBoxWarn(this, "添加失败：" + ex.Message);
            }
        }

        /// <summary>
        /// 360小时 具体循环代码 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn360Update_Click(object sender, EventArgs e)
        {
            if (dgvNode.SelectedRows.Count == 0)
            {
                Var.MsgBoxWarn(this, "请选择要编辑的数据行");
                return;
            }

            if (listTestStep.SelectedItem == null)
            {
                Var.MsgBoxWarn(this, "请先选择循环代码");
                return;
            }

            string nodeName = listTestStep.SelectedItem.ToString();

            try
            {
                DataGridViewRow selectedRow = dgvNode.SelectedRows[0];
                int index = Convert.ToInt32(selectedRow.Cells["NodeIndex"].Value);

                DurStepConfig config = new DurStepConfig(txtModel.Text, nodeName);
                TestBasePara editPara = config.testBasePara.FirstOrDefault(p => p.Index == index);

                if (editPara == null)
                {
                    Var.MsgBoxWarn(this, "未找到要编辑的数据");
                    return;
                }

                // 更新数据
                editPara.RPM = uiDoubleUpDown1.Value;
                editPara.Torque = uiDoubleUpDown2.Value;
                editPara.RunTime = uiDoubleUpDown3.Value;

                config.Save();
                LoadGridViewNode();

                Var.MsgBoxSuccess(this, "编辑成功！");
            }
            catch (Exception ex)
            {
                Var.MsgBoxWarn(this, "编辑失败：" + ex.Message);
            }
        }

        /// <summary>
        /// 360小时 具体循环代码 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn360Delete_Click(object sender, EventArgs e)
        {
            if (dgvNode.SelectedRows.Count == 0)
            {
                Var.MsgBoxWarn(this, "请选择要删除的数据行");
                return;
            }

            if (listTestStep.SelectedItem == null)
            {
                Var.MsgBoxWarn(this, "请先选择循环代码");
                return;
            }

            var result = Var.MsgBoxYesNo(this, "确定要删除选中的数据吗？");
            if (!result) return;

            string nodeName = listTestStep.SelectedItem.ToString();

            try
            {
                DataGridViewRow selectedRow = dgvNode.SelectedRows[0];
                int index = Convert.ToInt32(selectedRow.Cells["NodeIndex"].Value);

                DurStepConfig config = new DurStepConfig(txtModel.Text, nodeName);
                TestBasePara deletePara = config.testBasePara.FirstOrDefault(p => p.Index == index);

                if (deletePara != null)
                {
                    config.testBasePara.Remove(deletePara);

                    // 重新排序序号
                    for (int i = 0; i < config.testBasePara.Count; i++)
                    {
                        config.testBasePara[i].Index = i + 1;
                    }

                    config.Save();
                    LoadGridViewNode();

                    Var.MsgBoxSuccess(this, "删除成功！");
                }
            }
            catch (Exception ex)
            {
                Var.MsgBoxWarn(this, "删除失败：" + ex.Message);
            }
        }

        /// <summary>
        /// 点击表格
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvNode_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvNode.Rows.Count)
            {
                DataGridViewRow selectedRow = dgvNode.Rows[e.RowIndex];

                double speed = Convert.ToDouble(selectedRow.Cells["NodeSpeed"].Value ?? "0");
                double torque = Convert.ToDouble(selectedRow.Cells["NodeTorque"].Value ?? "0");
                double runTime = Convert.ToDouble(selectedRow.Cells["NodeTime"].Value ?? "0");

                uiDoubleUpDown1.Value = speed.ToDouble();
                uiDoubleUpDown2.Value = torque.ToDouble();
                uiDoubleUpDown3.Value = runTime.ToDouble();
            }
        }

        private void listTestStep_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadGridViewNode();
        }


        #endregion


        #region 360小时流程表格右键菜单功能

        // 存储剪切板数据（使用具体业务类）
        private List<DurabilityData> copiedDataList = null;
        private bool isCutOperation = false;

        /// <summary>
        /// 复制选中行数据（支持多选，保持选中顺序）
        /// </summary>
        private void tsmCopy_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    if (dgvMHDur.SelectedRows.Count == 0)
            //    {
            //        Var.MsgBoxWarn(this, "请先选择要复制的数据行");
            //        return;
            //    }

            //    // 清空之前的复制数据
            //    copiedDataList = new List<DurabilityData>();

            //    // 获取当前配置以确保数据最新
            //    durabilityTestConfig = new DurabilityTestConfig(txtModel.Text);
            //    durabilityTestConfig.Load();

            //    // 按选中顺序（从上到下）复制数据
            //    var selectedRows = dgvMHDur.SelectedRows.Cast<DataGridViewRow>()
            //        .OrderBy(r => r.Index)  // 按界面显示顺序排序
            //        .ToList();

            //    foreach (DataGridViewRow selectedRow in selectedRows)
            //    {
            //        int index = Convert.ToInt32(selectedRow.Cells["ColIndex"].Value);

            //        // 从配置中查找对应的业务数据
            //        var data = durabilityTestConfig.DurabilityDatas.FirstOrDefault(d => d.Index == index);
            //        if (data != null)
            //        {
            //            // 创建新的业务对象（深拷贝）
            //            DurabilityData copiedData = new DurabilityData
            //            {
            //                Index = data.Index,
            //                PhaseName = data.PhaseName,
            //                CycleName = data.CycleName,
            //                NodeName = data.NodeName,
            //                DayNum = data.DayNum
            //            };
            //            copiedDataList.Add(copiedData);
            //        }
            //    }

            //    isCutOperation = false;
            //    Var.MsgBoxSuccess(this, $"已复制 {copiedDataList.Count} 条数据到剪贴板");
            //}
            //catch (Exception ex)
            //{
            //    Var.MsgBoxWarn(this, "复制失败：" + ex.Message);
            //}
        }

        /// <summary>
        /// 剪切选中行数据（支持多选）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmCut_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    if (dgvMHDur.SelectedRows.Count == 0)
            //    {
            //        Var.MsgBoxWarn(this, "请先选择要剪切的数据行");
            //        return;
            //    }

            //    // 清空之前的复制数据
            //    copiedDataList = new List<DurabilityData>();

            //    // 获取当前配置以确保数据最新
            //    durabilityTestConfig = new DurabilityTestConfig(txtModel.Text);
            //    durabilityTestConfig.Load();

            //    // 复制所有选中的行对应的业务数据
            //    foreach (DataGridViewRow selectedRow in dgvMHDur.SelectedRows)
            //    {
            //        int index = Convert.ToInt32(selectedRow.Cells["ColIndex"].Value);

            //        // 从配置中查找对应的业务数据
            //        var data = durabilityTestConfig.DurabilityDatas.FirstOrDefault(d => d.Index == index);
            //        if (data != null)
            //        {
            //            // 创建新的业务对象（深拷贝）
            //            DurabilityData copiedData = new DurabilityData
            //            {
            //                Index = data.Index,
            //                PhaseName = data.PhaseName,
            //                CycleName = data.CycleName,
            //                NodeName = data.NodeName,
            //                DayNum = data.DayNum
            //            };
            //            copiedDataList.Add(copiedData);
            //        }
            //    }

            //    isCutOperation = true;
            //    Var.MsgBoxSuccess(this, $"已剪切 {copiedDataList.Count} 条数据到剪贴板");
            //}
            //catch (Exception ex)
            //{
            //    Var.MsgBoxWarn(this, "剪切失败：" + ex.Message);
            //}
        }


        /// 粘贴数据到最下面（支持多条数据，不检查重复）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmPaste_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    if (copiedDataList == null || copiedDataList.Count == 0)
            //    {
            //        Var.MsgBoxWarn(this, "剪贴板中没有数据可粘贴");
            //        return;
            //    }

            //    if (string.IsNullOrEmpty(txtModel.Text))
            //    {
            //        Var.MsgBoxWarn(this, "请先选择产品型号");
            //        return;
            //    }

            //    // 获取当前配置
            //    durabilityTestConfig = new DurabilityTestConfig(txtModel.Text);
            //    durabilityTestConfig.Load();

            //    if (isCutOperation)
            //    {
            //        // 剪切操作：先删除原数据，再追加到末尾
            //        var result = Var.MsgBoxYesNo(this, $"确定要移动 {copiedDataList.Count} 条数据到末尾吗？");
            //        if (!result) return;

            //        // 获取当前最大序号作为起始点
            //        int startIndex = GetNext360Index();
            //        int currentIndex = startIndex;

            //        // 先删除原数据
            //        foreach (var copiedData in copiedDataList)
            //        {
            //            var existingData = durabilityTestConfig.DurabilityDatas.FirstOrDefault(d =>
            //                d.Index == copiedData.Index);

            //            if (existingData != null)
            //            {
            //                durabilityTestConfig.DurabilityDatas.Remove(existingData);
            //            }
            //        }

            //        // 再追加新数据到末尾
            //        foreach (var copiedData in copiedDataList)
            //        {
            //            DurabilityData newData = new DurabilityData
            //            {
            //                Index = currentIndex++,
            //                PhaseName = copiedData.PhaseName,
            //                CycleName = copiedData.CycleName,
            //                NodeName = copiedData.NodeName,
            //                DayNum = copiedData.DayNum
            //            };
            //            durabilityTestConfig.DurabilityDatas.Add(newData);
            //        }

            //        // 重新排序所有数据
            //        ReorderDurabilityData();

            //        // 保存配置
            //        durabilityTestConfig.Save();

            //        // 刷新显示
            //        LoadGridViewDur();

            //        // 清除剪切板
            //        copiedDataList = null;
            //        isCutOperation = false;

            //        Var.MsgBoxSuccess(this, $"已移动 {copiedDataList.Count} 条数据到末尾");
            //    }
            //    else
            //    {
            //        // 复制操作：直接追加到末尾
            //        var result = Var.MsgBoxYesNo(this, $"确定要粘贴 {copiedDataList.Count} 条数据到末尾吗？");
            //        if (!result) return;

            //        int startIndex = GetNext360Index();
            //        int currentIndex = startIndex;

            //        // 直接追加所有数据到末尾
            //        foreach (var copiedData in copiedDataList)
            //        {
            //            DurabilityData newData = new DurabilityData
            //            {
            //                Index = currentIndex++,
            //                PhaseName = copiedData.PhaseName,
            //                CycleName = copiedData.CycleName,
            //                NodeName = copiedData.NodeName,
            //                DayNum = copiedData.DayNum
            //            };
            //            durabilityTestConfig.DurabilityDatas.Add(newData);
            //        }

            //        // 重新排序所有数据
            //        ReorderDurabilityData();

            //        // 保存配置
            //        durabilityTestConfig.Save();

            //        // 刷新显示
            //        LoadGridViewDur();

            //        Var.MsgBoxSuccess(this, $"已粘贴 {copiedDataList.Count} 条数据到末尾");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Var.MsgBoxWarn(this, "粘贴失败：" + ex.Message);
            //}
        }

        /// <summary>
        /// 重新排序耐久性数据
        /// </summary>
        private void ReorderDurabilityData()
        {
            if (durabilityTestConfig.DurabilityDatas == null || durabilityTestConfig.DurabilityDatas.Count == 0)
                return;

            // 按当前序号排序后重新编号
            var sortedData = durabilityTestConfig.DurabilityDatas.OrderBy(d => d.Index).ToList();

            for (int i = 0; i < sortedData.Count; i++)
            {
                sortedData[i].Index = i + 1;
            }

            durabilityTestConfig.DurabilityDatas = sortedData;
        }

        /// <summary>
        /// 获取最大序号（用于新增数据）
        /// </summary>
        /// <returns></returns>
        private int GetNext360Index()
        {
            if (durabilityTestConfig.DurabilityDatas == null || durabilityTestConfig.DurabilityDatas.Count == 0)
                return 1;

            return durabilityTestConfig.DurabilityDatas.Max(d => d.Index) + 1;
        }

        /// <summary>
        /// 右键菜单打开前事件（控制菜单项可用状态）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextMenuStrip360h_Opening(object sender, CancelEventArgs e)
        {
            //// 根据是否有选中行和剪贴板数据来设置菜单项可用状态
            //bool hasSelection = dgvMHDur.SelectedRows.Count > 0;
            //bool hasClipboardData = copiedDataList != null && copiedDataList.Count > 0;

            //tsmCopy.Enabled = hasSelection;
            //tsmCut.Enabled = hasSelection;
            //tsmPaste.Enabled = hasClipboardData;
        }


        #endregion

        /// <summary>
        /// 加载配方参数
        /// </summary>
        private void LoadPubConfig()
        {
            try
            {
                if (string.IsNullOrEmpty(txtModel.Text))
                {
                    return;
                }

                PubConfig pubConfig = new PubConfig(txtModel.Text);
                var pubPara = pubConfig.PubParaList;

                if (pubPara.Count == 0)
                {
                    // 如果没有保存过的配方，设置默认值或清空控件
                    ClearAllControls();
                    return;
                }

                // 加载第一个配方的数据到控件
                var para = pubPara[0];

                // 励磁电流参数
                DefaultExcitationCurrent.Value = para.DefaultExcitationCurrent;
                MinExcitationCurrent.Value = para.MinExcitationCurrent;
                MaxExcitationCurrent.Value = para.MaxExcitationCurrent;

                // 转速参数
                DefaultRotationSpeed.Value = para.DefaultRotationSpeed;
                MinRotationSpeed.Value = para.MinRotationSpeed;
                MaxRotationSpeed.Value = para.MaxRotationSpeed;

                // 油温参数
                DefaultOilTemperature.Value = para.DefaultOilTemperature;
                MinOilTemperature.Value = para.MinOilTemperature;
                MaxOilTemperature.Value = para.MaxOilTemperature;

                // 高温水参数
                DefaultHighTemperatureWater.Value = para.DefaultHighTemperatureWater;
                MinHighTemperatureWater.Value = para.MinHighTemperatureWater;
                MaxHighTemperatureWater.Value = para.MaxHighTemperatureWater;

                // 中冷水温度参数
                DefaultMediumColdWaterTemperature.Value = para.DefaultMediumColdWaterTemperature;
                MinMediumColdWaterTemperature.Value = para.MinMediumColdWaterTemperature;
                MaxMediumColdWaterTemperature.Value = para.MaxMediumColdWaterTemperature;

                // 水泵出口流量参数
                DefaultWaterPumpOutletFlow.Value = para.DefaultWaterPumpOutletFlow;
                MinWaterPumpOutletFlow.Value = para.MinWaterPumpOutletFlow;
                MaxWaterPumpOutletFlow.Value = para.MaxWaterPumpOutletFlow;

                // 燃油泵1流量参数
                DefaultFuelPump1Flow.Value = para.DefaultFuelPump1Flow;
                MinFuelPump1Flow.Value = para.MinFuelPump1Flow;
                MaxFuelPump1Flow.Value = para.MaxFuelPump1Flow;

                // 燃油泵2流量参数
                DefaultFuelPump2Flow.Value = para.DefaultFuelPump2Flow;
                MinFuelPump2Flow.Value = para.MinFuelPump2Flow;
                MaxFuelPump2Flow.Value = para.MaxFuelPump2Flow;

                // 进气管右流量参数
                DefaultIntakeDuctRightFlow.Value = para.DefaultIntakeDuctRightFlow;
                MinIntakeDuctRightFlow.Value = para.MinIntakeDuctRightFlow;
                MaxIntakeDuctRightFlow.Value = para.MaxIntakeDuctRightFlow;

                // 进气管左流量参数
                DefaultIntakeDuctLeftFlow.Value = para.DefaultIntakeDuctLeftFlow;
                MinIntakeDuctLeftFlow.Value = para.MinIntakeDuctLeftFlow;
                MaxIntakeDuctLeftFlow.Value = para.MaxIntakeDuctLeftFlow;

                // 排气管右参数
                DefaultExhaustDuctRight.Value = para.DefaultExhaustDuctRight;
                MinExhaustDuctRight.Value = para.MinExhaustDuctRight;
                MaxExhaustDuctRight.Value = para.MaxExhaustDuctRight;

                // 排气管左参数
                DefaultExhaustDuctLeft.Value = para.DefaultExhaustDuctLeft;
                MinExhaustDuctLeft.Value = para.MinExhaustDuctLeft;
                MaxExhaustDuctLeft.Value = para.MaxExhaustDuctLeft;

                // 水阻箱进口参数
                DefaultWaterResistanceTankInlet.Value = para.DefaultWaterResistanceTankInlet;
                MinWaterResistanceTankInlet.Value = para.MinWaterResistanceTankInlet;
                MaxWaterResistanceTankInlet.Value = para.MaxWaterResistanceTankInlet;
            }
            catch (Exception ex)
            {
                Var.MsgBoxWarn(this, "加载配方出现异常");
                Var.LogInfo($"加载配方出现异常:{ex.Message}");
            }
        }

        /// <summary>
        /// 清空所有控件值
        /// </summary>
        private void ClearAllControls()
        {
            // 将所有数值控件的值设置为0或默认值
            var numericControls = this.Controls.OfType<NumericUpDown>();
            foreach (var control in numericControls)
            {
                control.Value = 0;
            }
        }

        /// <summary>
        /// 保存配方
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSavePub_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtModel.Text))
            {
                Var.MsgBoxWarn(this, "试验参数请选择产品后保存");
                return;
            }
            try
            {
                PubConfig pubConfig = new PubConfig(txtModel.Text);
                var pubPara = pubConfig.PubParaList;
                if (pubPara.Count == 0)
                {
                    //新增
                    PubPara pubParaNew = new PubPara();
                    pubParaNew.DefaultExcitationCurrent = DefaultExcitationCurrent.Value.ToInt();
                    pubParaNew.MinExcitationCurrent = MinExcitationCurrent.Value.ToInt();
                    pubParaNew.MaxExcitationCurrent = MaxExcitationCurrent.Value.ToInt();

                    pubParaNew.DefaultRotationSpeed = DefaultRotationSpeed.Value.ToInt();
                    pubParaNew.MinRotationSpeed = MinRotationSpeed.Value.ToInt();
                    pubParaNew.MaxRotationSpeed = MaxRotationSpeed.Value.ToInt();

                    pubParaNew.DefaultOilTemperature = DefaultOilTemperature.Value.ToInt();
                    pubParaNew.MinOilTemperature = MinOilTemperature.Value.ToInt();
                    pubParaNew.MaxOilTemperature = MaxOilTemperature.Value.ToInt();

                    pubParaNew.DefaultHighTemperatureWater = DefaultHighTemperatureWater.Value.ToInt();
                    pubParaNew.MinHighTemperatureWater = MinHighTemperatureWater.Value.ToInt();
                    pubParaNew.MaxHighTemperatureWater = MaxHighTemperatureWater.Value.ToInt();

                    pubParaNew.DefaultMediumColdWaterTemperature = DefaultMediumColdWaterTemperature.Value.ToInt();
                    pubParaNew.MinMediumColdWaterTemperature = MinMediumColdWaterTemperature.Value.ToInt();
                    pubParaNew.MaxMediumColdWaterTemperature = MaxMediumColdWaterTemperature.Value.ToInt();

                    pubParaNew.DefaultWaterPumpOutletFlow = DefaultWaterPumpOutletFlow.Value.ToInt();
                    pubParaNew.MinWaterPumpOutletFlow = MinWaterPumpOutletFlow.Value.ToInt();
                    pubParaNew.MaxWaterPumpOutletFlow = MaxWaterPumpOutletFlow.Value.ToInt();

                    pubParaNew.DefaultFuelPump1Flow = DefaultFuelPump1Flow.Value.ToInt();
                    pubParaNew.MinFuelPump1Flow = MinFuelPump1Flow.Value.ToInt();
                    pubParaNew.MaxFuelPump1Flow = MaxFuelPump1Flow.Value.ToInt();

                    pubParaNew.DefaultFuelPump2Flow = DefaultFuelPump2Flow.Value.ToInt();
                    pubParaNew.MinFuelPump2Flow = MinFuelPump2Flow.Value.ToInt();
                    pubParaNew.MaxFuelPump2Flow = MaxFuelPump2Flow.Value.ToInt();

                    pubParaNew.DefaultIntakeDuctRightFlow = DefaultIntakeDuctRightFlow.Value.ToInt();
                    pubParaNew.MinIntakeDuctRightFlow = MinIntakeDuctRightFlow.Value.ToInt();
                    pubParaNew.MaxIntakeDuctRightFlow = MaxIntakeDuctRightFlow.Value.ToInt();

                    pubParaNew.DefaultIntakeDuctLeftFlow = DefaultIntakeDuctLeftFlow.Value.ToInt();
                    pubParaNew.MinIntakeDuctLeftFlow = MinIntakeDuctLeftFlow.Value.ToInt();
                    pubParaNew.MaxIntakeDuctLeftFlow = MaxIntakeDuctLeftFlow.Value.ToInt();

                    pubParaNew.DefaultExhaustDuctRight = DefaultExhaustDuctRight.Value.ToInt();
                    pubParaNew.MinExhaustDuctRight = MinExhaustDuctRight.Value.ToInt();
                    pubParaNew.MaxExhaustDuctRight = MaxExhaustDuctRight.Value.ToInt();

                    pubParaNew.DefaultExhaustDuctLeft = DefaultExhaustDuctLeft.Value.ToInt();
                    pubParaNew.MinExhaustDuctLeft = MinExhaustDuctLeft.Value.ToInt();
                    pubParaNew.MaxExhaustDuctLeft = MaxExhaustDuctLeft.Value.ToInt();

                    pubParaNew.DefaultWaterResistanceTankInlet = DefaultWaterResistanceTankInlet.Value.ToInt();
                    pubParaNew.MinWaterResistanceTankInlet = MinWaterResistanceTankInlet.Value.ToInt();
                    pubParaNew.MaxWaterResistanceTankInlet = MaxWaterResistanceTankInlet.Value.ToInt();
                    pubPara.Add(pubParaNew);
                    pubConfig.Save();
                }
                pubPara[0].DefaultExcitationCurrent = DefaultExcitationCurrent.Value.ToInt();
                pubPara[0].MinExcitationCurrent = MinExcitationCurrent.Value.ToInt();
                pubPara[0].MaxExcitationCurrent = MaxExcitationCurrent.Value.ToInt();

                pubPara[0].DefaultRotationSpeed = DefaultRotationSpeed.Value.ToInt();
                pubPara[0].MinRotationSpeed = MinRotationSpeed.Value.ToInt();
                pubPara[0].MaxRotationSpeed = MaxRotationSpeed.Value.ToInt();

                pubPara[0].DefaultOilTemperature = DefaultOilTemperature.Value.ToInt();
                pubPara[0].MinOilTemperature = MinOilTemperature.Value.ToInt();
                pubPara[0].MaxOilTemperature = MaxOilTemperature.Value.ToInt();

                pubPara[0].DefaultHighTemperatureWater = DefaultHighTemperatureWater.Value.ToInt();
                pubPara[0].MinHighTemperatureWater = MinHighTemperatureWater.Value.ToInt();
                pubPara[0].MaxHighTemperatureWater = MaxHighTemperatureWater.Value.ToInt();

                pubPara[0].DefaultMediumColdWaterTemperature = DefaultMediumColdWaterTemperature.Value.ToInt();
                pubPara[0].MinMediumColdWaterTemperature = MinMediumColdWaterTemperature.Value.ToInt();
                pubPara[0].MaxMediumColdWaterTemperature = MaxMediumColdWaterTemperature.Value.ToInt();

                pubPara[0].DefaultWaterPumpOutletFlow = DefaultWaterPumpOutletFlow.Value.ToInt();
                pubPara[0].MinWaterPumpOutletFlow = MinWaterPumpOutletFlow.Value.ToInt();
                pubPara[0].MaxWaterPumpOutletFlow = MaxWaterPumpOutletFlow.Value.ToInt();

                pubPara[0].DefaultFuelPump1Flow = DefaultFuelPump1Flow.Value.ToInt();
                pubPara[0].MinFuelPump1Flow = MinFuelPump1Flow.Value.ToInt();
                pubPara[0].MaxFuelPump1Flow = MaxFuelPump1Flow.Value.ToInt();

                pubPara[0].DefaultFuelPump2Flow = DefaultFuelPump2Flow.Value.ToInt();
                pubPara[0].MinFuelPump2Flow = MinFuelPump2Flow.Value.ToInt();
                pubPara[0].MaxFuelPump2Flow = MaxFuelPump2Flow.Value.ToInt();

                pubPara[0].DefaultIntakeDuctRightFlow = DefaultIntakeDuctRightFlow.Value.ToInt();
                pubPara[0].MinIntakeDuctRightFlow = MinIntakeDuctRightFlow.Value.ToInt();
                pubPara[0].MaxIntakeDuctRightFlow = MaxIntakeDuctRightFlow.Value.ToInt();

                pubPara[0].DefaultIntakeDuctLeftFlow = DefaultIntakeDuctLeftFlow.Value.ToInt();
                pubPara[0].MinIntakeDuctLeftFlow = MinIntakeDuctLeftFlow.Value.ToInt();
                pubPara[0].MaxIntakeDuctLeftFlow = MaxIntakeDuctLeftFlow.Value.ToInt();

                pubPara[0].DefaultExhaustDuctRight = DefaultExhaustDuctRight.Value.ToInt();
                pubPara[0].MinExhaustDuctRight = MinExhaustDuctRight.Value.ToInt();
                pubPara[0].MaxExhaustDuctRight = MaxExhaustDuctRight.Value.ToInt();

                pubPara[0].DefaultExhaustDuctLeft = DefaultExhaustDuctLeft.Value.ToInt();
                pubPara[0].MinExhaustDuctLeft = MinExhaustDuctLeft.Value.ToInt();
                pubPara[0].MaxExhaustDuctLeft = MaxExhaustDuctLeft.Value.ToInt();

                pubPara[0].DefaultWaterResistanceTankInlet = DefaultWaterResistanceTankInlet.Value.ToInt();
                pubPara[0].MinWaterResistanceTankInlet = MinWaterResistanceTankInlet.Value.ToInt();
                pubPara[0].MaxWaterResistanceTankInlet = MaxWaterResistanceTankInlet.Value.ToInt();
                pubConfig.Save();
            }
            catch (Exception ex)
            {
                Var.MsgBoxWarn(this, "保存配方出现异常");
                Var.LogInfo($"保存配方出现异常:{ex.Message}");
            }
        }

        /// <summary>
        /// 验证输入是否为有效数字
        /// </summary>
        private bool IsValidNumber(string input)
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

        /// <summary>
        /// 文本框按键事件处理
        /// </summary>
        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 允许退格键
            if (e.KeyChar == (char)8)
                return;

            // 允许小数点（只能有一个）
            if (e.KeyChar == '.')
            {
                TextBox textBox = sender as TextBox;
                if (textBox != null && textBox.Text.Contains("."))
                {
                    e.Handled = true; // 阻止输入多个小数点
                }
                return;
            }

            // 允许数字
            if (char.IsDigit(e.KeyChar))
                return;

            // 阻止其他所有字符
            e.Handled = true;
        }

        #region 360小时循环代码

        /// <summary>
        /// 360循环代码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd360hStep_Click(object sender, EventArgs e)
        {
            if (StepNum360.Value <= 0)
            {
                Var.MsgBoxWarn(this, "请设置有效的步骤数量");
                return;
            }

            try
            {
                // 获取当前最大步骤序号
                int currentMaxIndex = durStepConfig.testBasePara.Count > 0 ?
                    durStepConfig.testBasePara.Max(p => p.Index) : 0;
                string nodeName = listTestStep.SelectedItem.ToString();

                // 新增指定数量的步骤
                int stepsToAdd = (int)StepNum360.Value;
                for (int i = 1; i <= stepsToAdd; i++)
                {
                    TestBasePara newStep = new TestBasePara
                    {
                        Index = currentMaxIndex + i,
                        RPM = 0,
                        Torque = 0,
                        GKNo = "0",
                        RunTime = 1,
                        StepName = nodeName
                    };

                    durStepConfig.testBasePara.Add(newStep);
                    durStepConfig.Save();
                }
                // 刷新显示
                LoadGridViewNode();
            }
            catch (Exception ex)
            {
                Var.MsgBoxWarn(this, "添加详细步骤失败：" + ex.Message);
            }
        }

        /// <summary>
        /// 开始编辑360小时循环代码 流程表格单元格
        /// </summary>
        private void dgvNode_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            // 只允许编辑转速、扭矩、运行时间列
            if (e.ColumnIndex < 1)
            {
                e.Cancel = true;
                return;
            }

            // 允许编辑，清空输入法状态（防止中文输入）
            this.ImeMode = ImeMode.Disable;
        }

        /// <summary>
        /// 删除按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvNode_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // 确保点击的是删除按钮（假设第7列是删除按钮）
            if (e.RowIndex < 0 || e.ColumnIndex != 5) return;

            // 防止事件冒泡
            dgvNode.EndEdit();

            // 调用删除方法
            Delete360StepByRowIndex(e.RowIndex);
        }

        /// <summary>
        /// 根据行索引删除详细步骤
        /// </summary>
        private void Delete360StepByRowIndex(int rowIndex)
        {
            try
            {
                DataGridViewRow detailRow = dgvNode.Rows[rowIndex];

                if (detailRow.Cells["NodeIndex"].Value == null)
                {
                    Var.MsgBoxWarn(this, "该行为空行，无法删除");
                    return;
                }

                int detailIndex = Convert.ToInt32(detailRow.Cells["NodeIndex"].Value);

                var result = Var.MsgBoxYesNo(this, $"确定要删除步骤{detailIndex}吗？");
                if (!result) return;

                // 再找具体工况步骤
                var detailData = durStepConfig.testBasePara.FirstOrDefault(d => d.Index == detailIndex);
                if (detailData == null)
                {
                    Var.MsgBoxWarn(this, $"未找到步骤{detailIndex}的数据");
                    return;
                }

                durStepConfig.testBasePara.Remove(detailData);

                // 重新排序
                for (int i = 0; i < durStepConfig.testBasePara.Count; i++)
                {
                    durStepConfig.testBasePara[i].Index = i + 1;
                }
                durStepConfig.Save();

                // 重新加载360循环代码表格
                LoadGridViewNode();

                // 如果删除后还有行，自动选中上一行或第一行
                if (dgvNode.Rows.Count > 0)
                {
                    int selectRowIndex = rowIndex - 1;
                    if (selectRowIndex < 0) selectRowIndex = 0;

                    if (selectRowIndex < dgvNode.Rows.Count)
                    {
                        dgvNode.CurrentCell = dgvNode.Rows[selectRowIndex].Cells[0];
                    }
                }
            }
            catch (Exception ex)
            {
                Var.MsgBoxWarn(this, "删除失败：" + ex.Message);
            }
        }

        /// <summary>
        /// 360小时循环代码详细步骤表格单元格值编辑结束后触发
        /// </summary>
        private void dgvNode_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvNode.Rows.Count)
            {
                try
                {
                    // 恢复输入法状态
                    this.ImeMode = ImeMode.NoControl;

                    DataGridViewRow detailRow = dgvNode.Rows[e.RowIndex];
                    // 获取单元格
                    DataGridViewCell cell = detailRow.Cells[e.ColumnIndex];
                    string columnName = dgvNode.Columns[e.ColumnIndex].Name;
                    int detailIndex = Convert.ToInt32(detailRow.Cells["NodeIndex"].Value);

                    // 验证输入
                    string inputValue = cell.Value?.ToString() ?? "";

                    if (columnName == "NodeSpeed" || columnName == "NodeTorque" || columnName == "NodeTime" || columnName == "GK")
                    {
                        // 验证是否为数字（防止中文输入）
                        if (!IsValidNumber(inputValue))
                        {
                            Var.MsgBoxWarn(this, $"请输入有效的数字！当前输入：{inputValue}");
                            cell.Value = 0;
                            //LoadGridViewNode(); // 重新加载恢复原值
                            return;
                        }

                        double value = Convert.ToDouble(inputValue);

                        // 验证范围
                        if (columnName == "NodeSpeed" && (value < 0 || value > MAX_RPM))
                        {
                            Var.MsgBoxWarn(this, $"转速范围：0-{MAX_RPM}！当前输入：{value}");
                            cell.Value = 0;
                            //LoadGridViewNode();
                            return;
                        }
                        else if (columnName == "NodeTorque" && (value < 0 || value > MAX_TORQUE))
                        {
                            Var.MsgBoxWarn(this, $"扭矩范围：0-{MAX_TORQUE}！当前输入：{value}");
                            cell.Value = 0;
                            //LoadGridViewNode();
                            return;
                        }
                        else if (columnName == "NodeTime" && (value < 0 || value > MAX_RUN_TIME))
                        {
                            Var.MsgBoxWarn(this, $"运行时间范围：0-{MAX_RUN_TIME}！当前输入：{value}");
                            cell.Value = 1;
                            //LoadGridViewNode();
                            return;
                        }
                        else if (columnName == "GK")
                        {
                            // todo 是否检测工况编号是否存在
                            //return;
                        }
                    }

                    // 获取修改后的值
                    double rpm = Convert.ToDouble(detailRow.Cells["NodeSpeed"].Value);
                    double torque = Convert.ToDouble(detailRow.Cells["NodeTorque"].Value);
                    double runTime = Convert.ToDouble(detailRow.Cells["NodeTime"].Value);
                    string gkNo = detailRow.Cells["GK"].Value.ToString();

                    // 更新配置数据
                    var detailData = durStepConfig.testBasePara.FirstOrDefault(d => d.Index == detailIndex);
                    if (detailData != null)
                    {
                        // 更新修改的值
                        detailData.RPM = rpm;
                        detailData.Torque = torque;
                        detailData.RunTime = runTime;
                        detailData.GKNo = gkNo;

                        durStepConfig.Save();
                    }
                }
                catch (FormatException)
                {
                    Var.MsgBoxWarn(this, "输入格式错误，请输入有效的数字！");
                    //LoadGridViewNode(); // 重新加载恢复原值
                }
                catch (Exception ex)
                {
                    Var.MsgBoxWarn(this, "保存失败：" + ex.Message);
                }
            }
        }

        /// <summary>
        /// 单元格验证事件
        /// </summary>
        private void dgvNode_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            //if (e.RowIndex >= 0 && e.RowIndex < dgvNode.Rows.Count)
            //{
            //    string columnName = dgvNode.Columns[e.ColumnIndex].Name;

            //    // 只验证RPM、Torque、RunTime列
            //    if (columnName == "NodeSpeed" || columnName == "NodeTorque" || columnName == "NodeTime" || columnName == "GK")
            //    {
            //        string value = e.FormattedValue?.ToString() ?? "";

            //        if (string.IsNullOrWhiteSpace(value))
            //        {
            //            dgvNode.Rows[e.RowIndex].ErrorText = "不能为空！";
            //            e.Cancel = true;
            //            return;
            //        }

            //        // 验证是否为数字
            //        if (!IsValidNumber(value))
            //        {
            //            dgvNode.Rows[e.RowIndex].ErrorText = "请输入有效的数字！";
            //            e.Cancel = true;
            //            return;
            //        }

            //        double doubleValue;
            //        if (!double.TryParse(value, out doubleValue))
            //        {
            //            dgvNode.Rows[e.RowIndex].ErrorText = "请输入有效的数字！";
            //            e.Cancel = true;
            //            return;
            //        }

            //        // 验证范围
            //        if (columnName == "NodeSpeed")
            //        {
            //            if (doubleValue < 0 || doubleValue > MAX_RPM)
            //            {
            //                dgvNode.Rows[e.RowIndex].ErrorText = $"转速范围：0-{MAX_RPM} %";
            //                Var.MsgBoxWarn(this, dgvNode.Rows[e.RowIndex].ErrorText);
            //                e.Cancel = true;
            //            }
            //        }
            //        else if (columnName == "NodeTorque")
            //        {
            //            if (doubleValue < 0 || doubleValue > MAX_TORQUE)
            //            {
            //                dgvNode.Rows[e.RowIndex].ErrorText = $"扭矩范围：0-{MAX_TORQUE} %";
            //                Var.MsgBoxWarn(this, dgvNode.Rows[e.RowIndex].ErrorText);
            //                e.Cancel = true;
            //            }
            //        }
            //        else if (columnName == "NodeTime")
            //        {
            //            if (doubleValue < 0 || doubleValue > MAX_RUN_TIME)
            //            {
            //                dgvNode.Rows[e.RowIndex].ErrorText = $"运行时间范围：0-{MAX_RUN_TIME} min";
            //                Var.MsgBoxWarn(this, dgvNode.Rows[e.RowIndex].ErrorText);
            //                e.Cancel = true;
            //            }
            //        }

            //        // 清除错误提示
            //        dgvNode.Rows[e.RowIndex].ErrorText = "";
            //    }
            //}
        }

        /// <summary>
        /// 键盘输入验证（防止输入非数字字符）
        /// </summary>
        private void dgvNode_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            // 获取当前编辑的单元格
            if (dgvNode.CurrentCell == null) return;

            int columnIndex = dgvNode.CurrentCell.ColumnIndex;
            string columnName = dgvNode.Columns[columnIndex].Name;

            // 只对RPM、Torque、RunTime列进行输入限制
            if (columnName == "NodeSpeed" || columnName == "NodeTorque" || columnName == "NodeTime")
            {
                TextBox textBox = e.Control as TextBox;
                if (textBox != null)
                {
                    // 移除之前的事件处理器（防止重复添加）
                    textBox.KeyPress -= new KeyPressEventHandler(TextBox_KeyPress);
                    // 添加新的事件处理器
                    textBox.KeyPress += new KeyPressEventHandler(TextBox_KeyPress);

                    // 设置输入法为关闭状态
                    textBox.ImeMode = ImeMode.Disable;
                }
            }
        }

        #endregion

        /// <summary>
        /// 上移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUP_Click(object sender, EventArgs e)
        {
            MoveSelectedRow(-1);
        }

        /// <summary>
        /// 下移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDown_Click(object sender, EventArgs e)
        {
            MoveSelectedRow(1);
        }

        /// <summary>
        /// 移动选中行
        /// </summary>
        /// <param name="direction">移动方向：-1上移，1下移</param>
        private void MoveSelectedRow(int direction)
        {
            if (dgvNode.SelectedRows.Count == 0)
            {
                Var.MsgBoxWarn(this, "请选择要移动的数据行");
                return;
            }

            if (listTestStep.SelectedItem == null)
            {
                Var.MsgBoxWarn(this, "请先选择循环代码");
                return;
            }

            try
            {
                string nodeName = listTestStep.SelectedItem.ToString();
                DataGridViewRow selectedRow = dgvNode.SelectedRows[0];
                int currentIndex = selectedRow.Index;

                // 检查边界
                if ((direction == -1 && currentIndex == 0) ||
                    (direction == 1 && currentIndex == dgvNode.Rows.Count - 1))
                {
                    Var.MsgBoxWarn(this, direction == -1 ? "已经是第一行，无法上移" : "已经是最后一行，无法下移");
                    return;
                }

                int targetIndex = currentIndex + direction;

                // 获取当前行和目标行的数据
                int currentDataIndex = Convert.ToInt32(selectedRow.Cells["NodeIndex"].Value);
                int targetDataIndex = Convert.ToInt32(dgvNode.Rows[targetIndex].Cells["NodeIndex"].Value);

                // 在配置数据中查找对应的项
                TestBasePara currentPara = durStepConfig.testBasePara.FirstOrDefault(p => p.Index == currentDataIndex);
                TestBasePara targetPara = durStepConfig.testBasePara.FirstOrDefault(p => p.Index == targetDataIndex);

                if (currentPara != null && targetPara != null)
                {
                    // 交换数据内容（保持Index不变）
                    SwapTestBaseParaData(currentPara, targetPara);

                    // 保存配置
                    durStepConfig.Save();

                    // 重新加载显示
                    LoadGridViewNode();

                    // 重新选中移动后的行
                    if (targetIndex >= 0 && targetIndex < dgvNode.Rows.Count)
                    {
                        dgvNode.ClearSelection();
                        dgvNode.Rows[targetIndex].Selected = true;
                        dgvNode.CurrentCell = dgvNode.Rows[targetIndex].Cells[0];
                    }
                }
            }
            catch (Exception ex)
            {
                Var.MsgBoxWarn(this, $"移动失败：{ex.Message}");
            }
        }

        /// <summary>
        /// 交换两个TestBasePara对象的数据内容（保持Index不变）
        /// </summary>
        private void SwapTestBaseParaData(TestBasePara para1, TestBasePara para2)
        {
            // 保存临时数据
            string tempGKNo = para1.GKNo;
            double tempRPM = para1.RPM;
            double tempTorque = para1.Torque;
            double tempRunTime = para1.RunTime;
            string tempStepName = para1.StepName;

            // 将para2的数据赋给para1
            para1.GKNo = para2.GKNo;
            para1.RPM = para2.RPM;
            para1.Torque = para2.Torque;
            para1.RunTime = para2.RunTime;
            para1.StepName = para2.StepName;

            // 将临时数据（原para1）赋给para2
            para2.GKNo = tempGKNo;
            para2.RPM = tempRPM;
            para2.Torque = tempTorque;
            para2.RunTime = tempRunTime;
            para2.StepName = tempStepName;
        }
    }
}

