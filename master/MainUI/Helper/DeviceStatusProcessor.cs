using MainUI.Fault;
using MainUI.Fault.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainUI.Helper
{
    /// <summary>
    /// 设备状态处理器
    /// </summary>
    public class DeviceStatusProcessor
    {
        /// <summary>
        /// 是否更新故障状态(默认不更新，主界面更新一次即可)
        /// </summary>
        public bool IsUpdateFaultStatus;

        // 状态栏的状态 颜色
        private readonly Color clrOK = Color.LightGreen;
        private readonly Color clrNG = Color.Salmon;
        private readonly Color clrSimulate = Color.Orange;
        private readonly string txtSim = "仿真模式";
        private readonly string txtOK = "OK";
        private readonly string txtNG = "NG";


        public DeviceStatusProcessor(bool updateFaultStatus)
        {
            IsUpdateFaultStatus = updateFaultStatus;
        }

        /// <summary>
        /// 设备状态
        /// </summary>
        public class DeviceStatus
        {
            /// <summary>
            /// 是否存在OK状态
            /// </summary>
            public bool HasOK { get; set; }

            /// <summary>
            /// 是否存在NG状态
            /// </summary>
            public bool HasNG { get; set; }

            /// <summary>
            /// 是否存在模拟状态
            /// </summary>
            public bool HasSim { get; set; }

            public List<string> OKDevices { get; } = new List<string>();
            public List<string> NGDevices { get; } = new List<string>();
            public List<string> SimDevices { get; } = new List<string>();
        }

        /// <summary>
        /// 分析设备状态
        /// </summary>
        /// <param name="simulatedArray">模拟状态数组</param>
        /// <param name="noErrorArray">无错误状态数组</param>
        /// <param name="systemName">系统名称</param>
        /// <returns>设备状态</returns>
        public DeviceStatus AnalyzeDeviceStatus(bool[] simulatedArray, bool[] noErrorArray, string systemName, WarnTypeEnum warnType = WarnTypeEnum.Alarm)
        {
            var status = new DeviceStatus();

            for (int i = 0; i < simulatedArray.Length; i++)
            {
                int deviceNumber = i + 1;
                string deviceName = $"{systemName}{deviceNumber}";

                if (simulatedArray[i])
                {
                    status.HasSim = true;
                    status.SimDevices.Add(deviceNumber.ToString());
                    if (IsUpdateFaultStatus)
                        Var.FaultService.FaultStatusChange(FaultTypeEnum.communication, WarnTypeEnum.None, deviceName);
                }
                else
                {
                    if (noErrorArray[i])
                    {
                        status.HasOK = true;
                        status.OKDevices.Add(deviceNumber.ToString());
                        if (IsUpdateFaultStatus)
                            Var.FaultService.FaultStatusChange(FaultTypeEnum.communication, WarnTypeEnum.None, deviceName);
                    }
                    else
                    {
                        status.HasNG = true;
                        status.NGDevices.Add(deviceNumber.ToString());
                        if (IsUpdateFaultStatus)
                            Var.FaultService.FaultStatusChange(FaultTypeEnum.communication, warnType, deviceName);
                    }
                }
            }

            return status;
        }

        /// <summary>
        /// 更新标签显示
        /// </summary>
        public void UpdateStatusLabel(
            DeviceStatus status,
            ToolStripStatusLabel lblOK,
            ToolStripStatusLabel lblNG,
            ToolStripStatusLabel lblSim,
            string systemName,
            Func<bool> isAllSimulated = null,
            Func<bool> isAllNoError = null)
        {
            // 更新OK标签
            if (status.OKDevices.Count > 0)
            {
                if (status.HasOK && !status.HasNG && !status.HasSim)
                {
                    lblOK.Text = $"{systemName}:{txtOK}";
                }
                else
                {
                    lblOK.Text = $"{systemName}:{txtOK} " + string.Join(",", status.OKDevices);
                }
                lblOK.BackColor = clrOK;
                lblOK.Visible = true;
            }
            else
            {
                lblOK.Visible = false;
            }

            // 更新NG标签
            if (status.NGDevices.Count > 0)
            {
                if (isAllNoError?.Invoke() == true)
                {
                    lblNG.Text = $"{systemName}:{txtNG}";
                }
                else
                {
                    lblNG.Text = $"{systemName}:{txtNG} " + string.Join(",", status.NGDevices);
                }
                lblNG.BackColor = clrNG;
                lblNG.Visible = true;
            }
            else
            {
                lblNG.Visible = false;
            }

            // 更新Sim标签
            if (status.SimDevices.Count > 0)
            {
                if (isAllSimulated?.Invoke() == true)
                {
                    lblSim.Text = $"{systemName}:{txtSim}";
                }
                else
                {
                    lblSim.Text = $"{systemName}:{txtSim} " + string.Join(",", status.SimDevices);
                }
                lblSim.BackColor = clrSimulate;
                lblSim.Visible = true;
            }
            else
            {
                lblSim.Visible = false;
            }
        }

        /// <summary>
        /// 刷新通讯模块的通讯状态（正常:false 故障：true）
        /// </summary>
        /// <param name="lbl"></param>
        /// <param name="moduleName"></param>
        /// <param name="Simulate"></param>
        /// <param name="noError"></param>
        public bool FreshCommStatus(ToolStripStatusLabel lbl, string moduleName, bool Simulate, bool noError)
        {
            bool isNg = false;
            if (Simulate)
            {
                lbl.Text = moduleName + ":" + txtSim;
                lbl.BackColor = clrSimulate;
            }
            else
            {
                isNg = !noError; // noError为false时，isNg为true
                lbl.Text = moduleName + ":" + (noError ? txtOK : txtNG);
                lbl.BackColor = noError ? clrOK : clrNG;
            }
            return isNg;
        }

    }
}