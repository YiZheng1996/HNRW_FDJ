using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RW.Modules;
using RW.Driver;
using RW.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using RW.Data;
using MainUI.Config;
using System.IO;
using System.Drawing;
using System.Management;
using MainUI.BLL;
using Newtonsoft.Json;
using MetorSignalSimulator.UI.SocketFile;
using MetorSignalSimulator.UI.Model;
using MainUI.Services;
using BogieIdling.UI.Model;
using BogieIdling.UI.TRDP;
using MainUI.Equip;
using MainUI.Driver;
using System.Threading;
using MainUI.FSql;
using MainUI.Fault;

namespace MainUI
{
    public static class Var
    {
        public static string SoftName = "发动机性能试验台";

        public static string Version = "GJCS25042 VA.1.20260206";

        /// <summary>
        /// OPC交互-发送端
        /// </summary>
        public static OPCDriver opcExChangeSend = new OPCDriver();

        /// <summary>
        /// OPC交互-接收端
        /// </summary>
        public static OPCDriver opcExChangeReceive = new OPCDriver();

        public static OPCDriver opcDIGroup = new OPCDriver();
        public static OPCDriver opcDOGroup = new OPCDriver();
        public static OPCDriver opcAOGroup = new OPCDriver();
        public static OPCDriver opcAIGroup = new OPCDriver();
        public static OPCDriver opcStatus = new OPCDriver();

        // 数据交互
        public static OPCDriver opcExChangeGroup = new OPCDriver();

        // 油水管路故障
        public static OPCDriver opcPipelineFaultGroup = new OPCDriver();

        // 零点增益界面
        public static frmHardWare hardWare = new frmHardWare();

        /// <summary>
        /// 变频器
        /// </summary>
        public static OPCDriver opcInverterModbus = new OPCDriver();

        /// <summary>
        /// 使用Modbus协议的测试电量传感器
        /// </summary>
        public static OPCDriver opcThreePhaseElectric = new OPCDriver();

        /// <summary>
        /// 燃油系统数据采集
        /// </summary>
        public static OPCDriver opcFuelModbus = new OPCDriver();

        /// <summary>
        /// 机油系统数据采集
        /// </summary>
        public static OPCDriver opcEngineOilModbus = new OPCDriver();

        /// <summary>
        /// 水系统数据采集
        /// </summary>
        public static OPCDriver opcWaterModbus = new OPCDriver();

        /// <summary>
        /// PLC2AI数据
        /// </summary>
        public static OPCDriver opcAI2Modbus = new OPCDriver();
        /// <summary>
        /// 启动PLC采集
        /// </summary>
        public static OPCDriver opcStartPLCModbus = new OPCDriver();

        /// <summary>
        /// 转速模块采集
        /// </summary>
        public static OPCDriver opcSpeedModbus = new OPCDriver();

        /// <summary>
        /// 励磁系统数据采集
        /// </summary>
        public static OPCDriver opcExcitationModbus = new OPCDriver();

        /// <summary>
        /// 进气风道加热模块数据采集
        /// </summary>
        public static OPCDriver opcAirDuctModbus = new OPCDriver();


        public static string rootRptSave = Application.StartupPath + "\\save";  //报表保存路径

        /// <summary>
        ///登录用户权限级别： 1 管理员；2 工艺员；3 操作员
        /// </summary>
        public static int UserPrivate = 3;

        /// <summary>
        /// 暂停刷新
        /// </summary>
        public static bool NotFresh = false;

        //public static string ConnectionString = @"provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\db.mdb;jet oledb:database password=ok";  //数据库连接字符串
        //public static OleDB Database = new OleDB();

        public static SQLiteDB Database = new SQLiteDB();
        public static string ConnectionString = @"Data Source=DataBase.db;Version=3;";

        /// <summary>
        /// 程序配置文件
        /// </summary>
        public static SysParas SysConfig = new SysParas();

        /// <summary>
        /// 报警的配置文件
        /// </summary>
        public static FaultConfig FaultConfig = new FaultConfig();

        /// <summary>
        /// 生命信号字节
        /// </summary>
        public static int lifeport = 0;

        public static CRC16_FALSEHelper crc16 = new CRC16_FALSEHelper();

        /// <summary>
        /// 创建OPC连接地址
        /// </summary>
        static Var()
        {
            string kepServerName = "KEPware.KEPServerEx.V6";

            opcDOGroup.ServerName = kepServerName;
            opcDOGroup.Prefix = "PLC.S71500.";
            opcDIGroup.ServerName = kepServerName;
            opcDIGroup.Prefix = "PLC.S71500.";
            opcAIGroup.ServerName = kepServerName;
            opcAIGroup.Prefix = "PLC.S71500.";
            opcAOGroup.ServerName = kepServerName;
            opcAOGroup.Prefix = "PLC.S71500.";
            opcStatus.ServerName = kepServerName;
            opcStatus.Prefix = "PLC.S71500.";
            opcThreePhaseElectric.ServerName = kepServerName;
            opcThreePhaseElectric.Prefix = "被试品端电量测量.";
            opcFuelModbus.ServerName = kepServerName;
            opcFuelModbus.Prefix = "燃油系统数据采集.";
            opcEngineOilModbus.ServerName = kepServerName;
            opcEngineOilModbus.Prefix = "机油系统数据采集.";
            opcWaterModbus.ServerName = kepServerName;
            opcWaterModbus.Prefix = "水系统数据采集.";
            opcInverterModbus.ServerName = kepServerName;
            opcInverterModbus.Prefix = "PLC.S71500.InvertDO.";
            opcStartPLCModbus.ServerName = kepServerName;
            opcStartPLCModbus.Prefix = "StartPLC.S7200.";
            opcAI2Modbus.ServerName = kepServerName;
            opcAI2Modbus.Prefix = "PLC2.S71500.";
            opcSpeedModbus.ServerName = kepServerName;
            opcSpeedModbus.Prefix = "转速模块.";
            opcPipelineFaultGroup.ServerName = kepServerName;
            opcPipelineFaultGroup.Prefix = "PLC.S71500.";
            opcExChangeGroup.ServerName = kepServerName;
            opcExChangeGroup.Prefix = "PLC.S71500.";
            opcExcitationModbus.ServerName = kepServerName;
            opcExcitationModbus.Prefix = "PLC2.S71500.AI.";
            opcExChangeSend.ServerName = kepServerName;
            opcExChangeSend.Prefix = "ExChangeData.设备 1.";
            opcExChangeReceive.ServerName = kepServerName;
            opcExChangeReceive.Prefix = "控制台位通讯.设备1.ExChangeData.";
            opcAirDuctModbus.ServerName = kepServerName;
            opcAirDuctModbus.Prefix = "风道加热器PLC.S7200.";
            //opcDianliu.ServerName = kepServerName;
            //opcDianliu.Prefix = "DianLiu.";
        }

        /// <summary>
        /// OPC打开
        /// </summary>
        public static void Connect()
        {
            opcDOGroup.Connect();
            opcDIGroup.Connect();
            opcAIGroup.Connect();
            opcAOGroup.Connect();
            opcStatus.Connect();
            opcInverterModbus.Connect();
            opcThreePhaseElectric.Connect();
            opcFuelModbus.Connect();
            opcEngineOilModbus.Connect();
            opcWaterModbus.Connect();
            opcAI2Modbus.Connect();
            opcStartPLCModbus.Connect();
            opcSpeedModbus.Connect();
            opcPipelineFaultGroup.Connect();
            opcExChangeGroup.Connect();
            opcExcitationModbus.Connect();
            opcExChangeSend.Connect(); // 连接模块 2选1
            opcExChangeReceive.Connect(); //接收模块 2选1
        }

        /// <summary>
        /// OPC关闭
        /// </summary>
        public static void Close()
        {
            opcDOGroup.Close();
            opcDIGroup.Close();
            opcAIGroup.Close();
            opcAOGroup.Close();
            opcStatus.Close();
            opcInverterModbus.Close();
            opcThreePhaseElectric.Close();
            opcFuelModbus.Close();
            opcEngineOilModbus.Close();
            opcWaterModbus.Close();
            opcAI2Modbus.Close();
            opcStartPLCModbus.Close();
            opcSpeedModbus.Close();
            opcPipelineFaultGroup.Close();
            opcExChangeGroup.Close();
            opcExcitationModbus.Close();
            opcExChangeSend.Close();
            opcExChangeReceive.Close();
            opcAirDuctModbus.Close();
        }

        static SysLogBLL sysBll = new SysLogBLL();


        public static readonly object objLock = new object();
        public static void LogInfo(string txt)
        {
            //按月份，新建文件夹
            string pathRoot = Application.StartupPath + "\\SysLog\\" + System.DateTime.Now.ToString("yyyy-MM");
            if (Directory.Exists(pathRoot) == false)
                Directory.CreateDirectory(pathRoot);

            //按日期，建立txt文件；
            string fileTxtName = System.DateTime.Now.ToString("yyyy-MM-dd") + ".log";

            string nowStr = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string pathFull = pathRoot + "\\" + fileTxtName;
            string myLogInfo = string.Format("{0}：{1}\r\n", nowStr, txt);

            lock (objLock)
            {
                System.IO.File.AppendAllText(pathFull, myLogInfo);

                sysBll.Add(txt, nowStr);
            }
        }

        /// <summary>
        /// 量程转换
        /// </summary>
        /// <param name="input"></param>
        /// <param name="inputL">4</param>
        /// <param name="inputH">20</param>
        /// <param name="outL">0</param>
        /// <param name="outH">1000</param>
        /// <returns></returns>
        public static double AIAO_Convert(double input, double inputL, double inputH, double outL, double outH)
        {
            double rst = (outH - outL) * (input - inputL) / (inputH - inputL) + outL;
            rst = Math.Round(rst, 3);
            return rst;
        }


        /// <summary>
        /// 文件名的非法字符替换为下划线
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static string ReplaceFileName(string filename)
        {
            string rst = filename;
            rst = rst.Replace("\\", "_");
            rst = rst.Replace("/", "_");
            rst = rst.Replace("?", "_");
            rst = rst.Replace("*", "_");
            rst = rst.Replace(":", "_");
            rst = rst.Replace("|", "_");
            rst = rst.Replace("\"", "_");
            rst = rst.Replace("<", "_");
            rst = rst.Replace(">", "_");

            // rst = rst.Replace("-", "_"); 中划线是允许的

            return rst;
        }

        #region 弹窗提示

        /// <summary>
        /// 信息提示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="msg"></param>
        public static void MsgBoxInfo(Control sender, string msg)
        {
            FrmMessage frmMsg = new FrmMessage();
            if (sender != null)
            {
                sender.Invoke((MethodInvoker)delegate ()
                {
                    frmMsg.Msg = msg;
                    frmMsg.ShowDialog(sender);
                });
            }
            else
            {
                frmMsg.Msg = msg;
                frmMsg.ShowDialog();
            }
        }

        /// <summary>
        /// 成功
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="msg"></param>
        public static void MsgBoxSuccess(Control sender, string msg)
        {
            FrmMessageSuccess frmMsg = new FrmMessageSuccess();
            if (sender != null)
            {
                sender.Invoke((MethodInvoker)delegate ()
                {
                    frmMsg.Msg = msg;
                    frmMsg.ShowDialog(sender);
                });
            }
            else
            {
                frmMsg.Msg = msg;
                frmMsg.ShowDialog();
            }
        }

        /// <summary>
        /// 确定/取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static bool MsgBoxYesNo(Control sender, string msg)
        {
            bool isYes = false;
            frmMessageYesNO frmMsgYesNO = new frmMessageYesNO();
            if (sender != null)
            {
                sender.Invoke((MethodInvoker)delegate ()
                {
                    frmMsgYesNO.Msg = msg;
                    frmMsgYesNO.ShowDialog(sender);
                    isYes = frmMsgYesNO.IsYes;
                });
            }
            else
            {
                frmMsgYesNO.Msg = msg;
                frmMsgYesNO.ShowDialog();
                isYes = frmMsgYesNO.IsYes;
            }
            return isYes;

        }

        /// <summary>
        /// 警告
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="msg"></param>
        public static void MsgBoxWarn(Control sender, string msg)
        {
            frmMessageWarning frmwarning = new frmMessageWarning();
            if (sender != null)
            {
                sender.Invoke((MethodInvoker)delegate ()
                {
                    frmwarning.Msg = msg;
                    frmwarning.ShowDialog(sender);
                });
            }
            else
            {
                frmwarning.Msg = msg;
                frmwarning.ShowDialog();
            }
        }

        #endregion


        #region 初始化调用
        private static frmLoading _loading = null;
        /// <summary>
        /// 加载窗体
        /// </summary>
        public static frmLoading Loading
        {
            get
            {
                if (_loading == null)
                {
                    _loading = new frmLoading();

                    _loading.AddInitInvoke("台位控制部分", () =>
                    {
                        opcDOGroup.Connect();
                        opcDIGroup.Connect();
                        opcAIGroup.Connect();
                        opcAOGroup.Connect();
                        opcStatus.Connect();
                        opcPipelineFaultGroup.Connect();
                        opcExChangeGroup.Connect();

                        Common.AIgrp.Init();
                        Common.AOgrp.Init();
                        Common.DIgrp.Init();
                        Common.DOgrp.Init();
                        Common.PipelineFaultGrp.Init();
                        Common.ExChangeGrp.Init();

                        // 零点增益
                        Common.plcc.Init();
                        Common.opcStatus.Init();
                    });

                    _loading.AddInitInvoke("发动机检测部分", () =>
                    {
                        opcAI2Modbus.Connect();

                        Common.AI2Grp.Init();
                        // 零点增益
                        Common.plcc2.Init();
                    });

                    _loading.AddInitInvoke("双台位通讯", () =>
                    {
                        if (Var.SysConfig.ExeType == 1)
                        {
                            opcExChangeSend.Connect();
                            Common.opcExChangeSendGrp.Init();
                        }
                        else
                        {
                            opcExChangeReceive.Connect();
                            Common.opcExChangeReceiveGrp.Init();
                        }
                    });

                    _loading.AddInitInvoke("发电机模块", () =>
                    {
                        opcStartPLCModbus.Connect();
                        opcInverterModbus.Connect();

                        Common.startPLCGrp.Init();
                        Common.gd350_1.Init();
                    });

                    _loading.AddInitInvoke("励磁柜", () =>
                    {
                        opcExcitationModbus.Connect();

                        Common.excitationGrp.Init();
                    });
                    
                    _loading.AddInitInvoke("电参数仪器", () =>
                    {
                        opcThreePhaseElectric.Connect();

                        Common.threePhaseElectric.Init();
                    });

                    if (Var.SysConfig.ExeType == 1) 
                    {
                        _loading.AddInitInvoke("燃油耗仪", () =>
                        {
                            var result = ET4500.Instance.Init();
                            if (!result)
                            {
                                throw new InvalidOperationException("燃油耗仪连接失败！！");
                            }
                        });
                    }

                    _loading.AddInitInvoke("机油系统通讯模块", () =>
                    {
                        opcEngineOilModbus.Connect();

                        Common.engineOilGrp.Init();
                    });

                    _loading.AddInitInvoke("燃油系统通讯模块", () =>
                    {
                        opcFuelModbus.Connect();

                        Common.fuelGrp.Init();
                    });

                    _loading.AddInitInvoke("高温/低温水系统通讯模块", () =>
                    {
                        opcWaterModbus.Connect();

                        Common.waterGrp.Init();
                    });

                    _loading.AddInitInvoke("转速模块", () =>
                    {
                        opcSpeedModbus.Connect();

                        Common.speedGrp.Init();
                    });

                    // 控制部分才需要连接
                    if (Var.SysConfig.ExeType == 1) 
                    {
                        _loading.AddInitInvoke("称重仪", () =>
                        {
                            // 加载串口
                            var pt650Result = ZMPT650F.Instance.Init();
                            if (!pt650Result)
                            {
                                throw new InvalidOperationException("称重仪（扭矩显示）连接失败！！");
                            }
                        });
                    }
     
                    _loading.AddInitInvoke("零点增益模块", () =>
                    {
                        // 初始化零点增益界面
                        hardWare.InitZeroGain();
                    });

                    _loading.AddInitInvoke("进气风道加热模块", () =>
                    {
                        opcAirDuctModbus.Connect();

                        Common.AirDuctGrp.Init();
                    });

                    _loading.AddInitInvoke("故障模块", () =>
                    {
                        FaultService.Init();
                    });

                    _loading.AddInitInvoke("连接TRDP模块", () =>
                    {
                        // 连接TRDP
                        Var.TRDP.InitTag();

                        if (Var.SysConfig.ExeType == 1)
                        {

                            //Var.TRDP.LoadData(data.data);
                            var trdpConnect = Var.TRDP.TRDPstart();
                            if (!trdpConnect)
                            {
                                throw new InvalidOperationException("发动机控制器（ECM模块）连接失败！！");
                            }
                            else
                            {
                                // udp 发动打开
                                UDPSend.Start();
                            }
                        }
                        else
                        {
                            // 启动接收端
                            UDPReceive.Start();
                            // 手动打开trdp的心跳计时
                            Var.TRDP.CheckConnect();

                            // TRDP 只监听
                            //Var.TRDPRecive.LoadData(data.data);
                            //Var.TRDPRecive.Connect();
                        }

                        //Var.TRDP.tags.Clear();
                        //Var.TRDP.ports.Clear();
                        //foreach (var item in data.data)
                        //{
                        //    if (item.signalName == null)
                        //    {
                        //        continue;
                        //    }

                        //    FullTags ft = new FullTags();
                        //    ft.ID = item.id;
                        //    if (item.dataFormat == "" || item.dataFormat == "0" || item.dataFormat == null)
                        //    {
                        //        ft.dataFormat = 1;
                        //    }
                        //    else
                        //    {
                        //        ft.dataFormat = decimal.Parse(item.dataFormat);
                        //    }
                        //    ft.RawLow = item.RawLow;
                        //    ft.RawHight = item.RawHight;
                        //    ft.ScaledLow = item.ScaledLow;
                        //    ft.ScaledHight = item.ScaledHight;
                        //    ft.DataLabel = item.signalName;
                        //    ft.DataType = item.dataType;
                        //    ft.DataRange = item.vhecileNo;
                        //    ft.Description = item.carNo;
                        //    ft.guzhangfenlei = item.yuLiu10;
                        //    ft.Identity = item.signalName.Contains("生命信号");
                        //    ft.TxType = item.yuLiu5.Contains("以太网") ? "以太网" : "MVB";
                        //    ft.comID = Convert.ToInt32(item.yuLiu2);
                        //    COMMData cd = new COMMData();
                        //    cd.Port = Convert.ToInt32(item.yuLiu2, 16);
                        //    cd.Offset = item.byteOffset;
                        //    cd.Bit = item.binaryOffset;
                        //    ft.COMMData = cd;
                        //    if (item.yuLiu5.Contains("以太网"))
                        //        Var.TRDP.tags.Add(ft);

                        //    AddPorts(item);
                        //}
                        //if (!Var.TRDP.isc)
                        //{
                        //    Var.TRDP.LoadData(data.data);
                        //    Var.TRDP.Connect();
                        //}
                    });
                }
                return _loading;
            }
        }

        /// <summary>
        /// 添加端口
        /// </summary>
        /// <param name="item"></param>
        //public static void AddPorts(AgreementData item)
        //{
        //    Ports pt = new Ports();
        //    pt.ID = item.id;
        //    pt.Port = item.yuLiu2;
        //    pt.IsRead = !item.yuLiu6.Contains("源");
        //    pt.IsUse = true;
        //    pt.Rate = int.Parse(item.yuLiu4);
        //    pt.DataSize = int.Parse(item.yuLiu3);
        //    pt.SMIValue = string.IsNullOrEmpty(item.yuLiu8) ? 0 : int.Parse(item.yuLiu8);
        //    pt.duankoumingcheng = string.IsNullOrEmpty(item.yuLiu11) ? null : item.yuLiu11;

        //    if (item.yuLiu5.Contains("以太网"))
        //    {
        //        if (Var.TRDP.ports.FirstOrDefault(x => x.Port.Equals(item.yuLiu2)) == null)
        //        {
        //            pt.MulticastAddress = pt.MulticastAddress;
        //            Var.TRDP.ports.Add(pt);
        //            Var.TRDP.fullData[pt.PortNum1] = new byte[pt.DataSize];
        //        }
        //    }
        //}

        /// <summary>
        /// 初始化所有模块
        /// </summary>
        public static bool Init()
        {
            var dr = Loading.ShowDialog();
            return dr == DialogResult.OK;
        }
        #endregion


        // 让控件处在不可用的情况下（控件.Enabled = false;），字体任然效果效果不变灰黑。
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int wndproc);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        public const int GWL_STYLE = -16;
        public const int WS_DISABLED = 0x8000000;
        public static void SetControlEnabled(Control c, bool enabled)
        {
            if (enabled)
            { SetWindowLong(c.Handle, GWL_STYLE, (~WS_DISABLED) & GetWindowLong(c.Handle, GWL_STYLE)); }
            else
            { SetWindowLong(c.Handle, GWL_STYLE, WS_DISABLED | GetWindowLong(c.Handle, GWL_STYLE)); }
        }

        /// <summary>
        /// 设定datagridView控件的通用属性
        /// </summary>
        /// <param name="dataGridView1"></param>
        public static void SetDgv(System.Windows.Forms.DataGridView dataGridView1)
        {
            try
            {
                //部分属性要在dataGridView1加载数据之前设置属性才有效。
                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    dataGridView1.Columns[i].ReadOnly = true;    //内容为只读
                    dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;//能否排序列
                }

                dataGridView1.MultiSelect = false;
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;  // 列标题居中
                dataGridView1.RowTemplate.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;  // 每行内容居中

                //列标题高度
                dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                // 列标题高度固定
                dataGridView1.ColumnHeadersHeight = 40;   // 高度固定为30 

                //每行内容高度
                dataGridView1.RowTemplate.Height = 26;

                dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = SystemColors.GradientInactiveCaption; // 奇数行设置背景色
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                //设置单元格颜色
                dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.Single;
                dataGridView1.GridColor = Color.FromArgb(80, 160, 255);

                //设置列标题边框颜色
                dataGridView1.EnableHeadersVisualStyles = false;
                dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;

                dataGridView1.RowHeadersVisible = false; //是否显示每行之前的小箭头
                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.AllowUserToDeleteRows = false;
                dataGridView1.AllowUserToResizeColumns = false;
                dataGridView1.AllowUserToResizeRows = false;
            }
            catch (Exception ex)
            {
                string err = "设置表格控件通用属性有误；原因：" + ex.Message;
                Var.LogInfo(err);
            }

        }


        static string root = "D:\\";
        public static void SaveScreen()
        {
            try
            {
                Rectangle bounds = Screen.GetBounds(Point.Empty);
                using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
                {
                    using (Graphics g = Graphics.FromImage(bitmap))
                    {
                        g.CopyFromScreen(Point.Empty, Point.Empty, bounds.Size);
                    }


                    if (System.IO.Directory.Exists(root) == false)
                        System.IO.Directory.CreateDirectory(root);

                    string saveFilePath = root + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".png";
                    bitmap.Save(saveFilePath, System.Drawing.Imaging.ImageFormat.Png);
                }
            }
            catch (Exception ex)
            {

            }
        }


        public static string GetCPUName()
        {
            //1、添加引用库文件 System.Management.dll。 它通常位于.NET Framework的安装目录下。
            //2、添加命名空间 using System.Management; 
            ManagementObjectSearcher ma = new ManagementObjectSearcher("select * from Win32_processor");
            string cpuName = "";
            foreach (ManagementObject item in ma.Get())
            {
                cpuName = item["name"].ToString().Trim();
            }
            return cpuName;
        }


        public static string GetCPUSerialNumber()
        {
            //1、添加引用库文件 System.Management.dll。 它通常位于.NET Framework的安装目录下。
            //2、添加命名空间 using System.Management; 
            ManagementClass mycpu = new ManagementClass("Win32_processor");
            ManagementObjectCollection myconn = mycpu.GetInstances();
            string cpuNum = "";
            foreach (ManagementObject item in myconn)
            {
                cpuNum = item.Properties["Processorid"].Value.ToString().Trim();
            }
            return cpuNum;
        }

        #region TRDP的初始化

        /// <summary>
        /// TRDP程序用参数
        /// </summary>
        public static TRDPConfig tRDPConfig = new TRDPConfig();

        private static TRDPClass m1 = null;
        public static object locker = new object();
        public static TRDPClass TRDP
        {
            get
            {
                if (m1 == null)
                {
                    lock (locker)
                    {
                        if (m1 == null)
                            m1 = new TRDPClass();
                    }
                }
                return m1;
            }
        }

        private static UDPBaseSend udpsender = null;
        public static object locker2 = new object();
        /// <summary>
        /// 发送的UDP数据
        /// </summary>
        public static UDPBaseSend UDPSend
        {
            get
            {
                if (udpsender == null)
                {
                    lock (locker)
                    {
                        if (udpsender == null)
                            udpsender = new UDPBaseSend(tRDPConfig.UDPReceiveIP, 17226, 17226);
                    }
                }
                return udpsender;
            }
        }

        private static UDPBaseSend udpreceiver = null;
        public static object locker3 = new object();
        /// <summary>
        /// 接收的UDP数据
        /// </summary>
        public static UDPBaseSend UDPReceive
        {
            get
            {
                if (udpreceiver == null)
                {
                    lock (locker)
                    {
                        if (udpreceiver == null)
                            udpreceiver = new UDPBaseSend(tRDPConfig.UDPSendIP, 17226, 17226);
                    }
                }
                return udpreceiver;
            }
        }

        #endregion


        #region 故障的模块
        private static UnifiedFaultDetectionService fault1 = null;
        /// <summary>
        /// 所有故障
        /// </summary>
        public static UnifiedFaultDetectionService FaultService
        {
            get
            {
                if (fault1 == null)
                {
                    fault1 = new UnifiedFaultDetectionService();
                }
                return fault1;
            }
        }

        //private static TestBedFaultDetectionService testBedfault = null;
        ///// <summary>
        ///// 试验台内部故障
        ///// </summary>
        //public static TestBedFaultDetectionService testBedFaultService
        //{
        //    get
        //    {
        //        if (testBedfault == null)
        //        {
        //            testBedfault = new TestBedFaultDetectionService();
        //        }
        //        return testBedfault;
        //    }
        //}
        #endregion


        #region 拓展方法
        /// <summary>
        /// 设置字节某两位置的值
        /// </summary>
        /// <param name="data"></param>
        /// <param name="index"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public static byte SetbyteBitToU2(this byte data, int beginIndex, byte val)
        {

            if (val > 3)
            {
                throw new ArgumentOutOfRangeException($"U2的值不能大于3； value:{val}");
            }
            var bit1 = val.GetbyteBit(0);
            var bit2 = val.GetbyteBit(1);
            if (beginIndex > 6)
            {
                throw new ArgumentOutOfRangeException($"位索引不能大于6， bitIndex:{beginIndex}");
            }
            int endIndex = beginIndex + 1;

            data = bit1 ? (byte)(data | (1 << beginIndex)) : (byte)(data & ~(1 << beginIndex));
            data = bit2 ? (byte)(data | (1 << endIndex)) : (byte)(data & ~(1 << endIndex));
            return data;
            // return double.TryParse(str, out output);
        }

        /// <summary>
        /// 获取字节的某位
        /// </summary>
        /// <param name="data"></param>
        /// <param name="offbit"></param>
        /// <returns></returns>
        public static bool GetbyteBit(this byte data, int offbit)
        {
            int tmpInt = 1 << offbit;
            return (data & tmpInt) / tmpInt == 0 ? false : true;
        }

        public static int ToInt(this object obj, int ret = 0)
        {
            int.TryParse(obj + "", out ret);
            return ret;
        }

        public static decimal ToDecimal(this object obj, decimal ret = 0)
        {
            decimal.TryParse(obj + "", out ret);
            return ret;
        }

        public static double ToDouble(this object obj, double ret = 0)
        {
            double.TryParse(obj + "", out ret);
            ret = Math.Round(ret, 1);
            return ret;
        }
        #endregion

    }



}
