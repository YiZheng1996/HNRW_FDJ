using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainUI.Manager
{


    public partial class frmPointSelect : Form
    {
        public string SelectedPoint { get; private set; } = "";
        public string SelectedUnit { get; private set; } = "";

        // 标题的集合
        Dictionary<string, Label> Labels = new Dictionary<string, Label>();

        List<DashboardPoint> DashboardPoints = new List<DashboardPoint>();

        string _model;

        // 选中的类型
        string _lblType = "";

        public frmPointSelect(string model)
        {
            _model = model;

            InitializeComponent();

            Labels.Add("温度", lblTemp);
            Labels.Add("压力", lblPress);
            Labels.Add("功率", lblPower);
            Labels.Add("扭矩", lblTorque);
            Labels.Add("转速", lblSpeed);
        }


        /// <summary>
        /// 加载所有点位到类中
        /// </summary>
        private void InitPoint()
        {
            // 温度
            List<string> TempList = new List<string>();
            TempList.Add("T1高温水出机温度");
            TempList.Add("中冷器进口水温");
            TempList.Add("中冷器出口水温");
            TempList.Add("前中冷前空气温度");
            TempList.Add("前中冷后空气温度");
            TempList.Add("前增压器进气温度");
            foreach (var item in TempList)
            {
                DashboardPoints.Add(new DashboardPoint { Point = item, Type = "温度", Unit = "℃" });
            }

            // 压力
            List<string> PressureList = new List<string>();
            PressureList.Add("P1高温水出机压力");
            PressureList.Add("P21主油道进口油压");
            PressureList.Add("主油道末端油压");
            PressureList.Add("中冷水泵出口压力");
            PressureList.Add("前中冷前空气压力");
            PressureList.Add("前中冷后空气压力");
            PressureList.Add("前增压器排气背压");
            PressureList.Add("前增压器进气真空度");
            PressureList.Add("后增压器排气背压");
            PressureList.Add("后增压器进气真空度"); 
            PressureList.Add("P38燃油供油压力");
            foreach (var item in PressureList)
            {
                DashboardPoints.Add(new DashboardPoint { Point = item, Type = "压力", Unit = "kPa" });
            }

            // 功率
            List<string> PowerList = new List<string>();
            PowerList.Add("发动机功率");
            foreach (var item in PowerList)
            {
                DashboardPoints.Add(new DashboardPoint { Point = item, Type = "功率", Unit = "kW" });
            }

            // 扭矩
            List<string> TorqueList = new List<string>();
            TorqueList.Add("发动机扭矩");
            foreach (var item in TorqueList)
            {
                DashboardPoints.Add(new DashboardPoint { Point = item, Type = "扭矩", Unit = "kN" });
            }

            // 转速
            List<string> SpeedList = new List<string>();
            SpeedList.Add("增压器转速");
            SpeedList.Add("发动机转速");
            foreach (var item in SpeedList)
            {
                DashboardPoints.Add(new DashboardPoint { Point = item, Type = "转速", Unit = "r/min" });
            }

            Labels["温度"].BackColor = Color.FromArgb(80, 160, 255);
            _lblType = "温度";

            // 默认只先加载温度的数据
            InitTableData();
        }

        /// <summary>
        /// 图片统一点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblTemp_Click(object sender, EventArgs e)
        {
            var button = sender as Label;

            foreach (var item in Labels)
            {
                if (item.Key == button.Text)
                {
                    _lblType = button.Text;
                    Labels[item.Key].BackColor = Color.FromArgb(80, 160, 255);
                }
                else
                {
                    Labels[item.Key].BackColor = Color.FromArgb(169, 169, 169);
                }
            }

            InitTableData();
        }

        /// <summary>
        /// 加载表格的数据
        /// </summary>
        public void InitTableData()
        {
            dgvMH.Rows.Clear();

            var list = DashboardPoints.Where(d => d.Type == _lblType).ToList();
            for (int i = 0; i < list.Count(); i++)
            {
                dgvMH.Rows.Add(list[i].Type, list[i].Point, list[i].Unit);
            }
        }

        /// <summary>
        /// 双击选中数据，返回数据给上一层级赋值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvMH_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Cells[1] = 点位名称, Cells[2] = 单位
                SelectedPoint = dgvMH.Rows[e.RowIndex].Cells[1].Value?.ToString() ?? "";
                SelectedUnit = dgvMH.Rows[e.RowIndex].Cells[2].Value?.ToString() ?? "";

                // 关闭窗体，并返回 OK
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void frmPointSelect_Load(object sender, EventArgs e)
        {
            // 添加所有的
            InitPoint();
        }
    }

    /// <summary>
    /// 仪表盘数据
    /// </summary>
    public class DashboardPoint
    {
        /// <summary>
        /// 类别
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 点位名称
        /// </summary>
        public string Point { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; }
    }
}
