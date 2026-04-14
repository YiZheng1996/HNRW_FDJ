using MainUI.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainUI.Graph
{
    public partial class frmGraphEdit : Form
    {
        string _model;
        string _point;
        GraphConfig _graph;

        public frmGraphEdit(GraphConfig graph, string model, string point)
        {
            _model = model;
            _point = point;
            _graph = graph;

            InitializeComponent();

            // 查询并赋值
            if (point != null)
            {
                InitConfig();
            }
            else
            {
                // 新增时默认给单选框赋值
                this.rdo0.Checked = true;
            }
        }

        /// <summary>
        /// 编辑参数
        /// </summary>
        public void InitConfig()
        {
            // 不允许更改key值
            this.btnselect.Enabled = false;

            //// 编辑参数，先把参数查询出来
            //DashboardConfig dashboardConfig = new DashboardConfig(_model);
            //dashboardConfig.Load();

            // 查询的数据
            var garphData = _graph.graphDatas.FirstOrDefault(d => d.Point == _point);

            this.txtPoint.Text = garphData.Point;
            this.txtUnit.Text = garphData.Unit;
            this.txtName.Text = garphData.Name;
            this.nudMin.Text = garphData.MinVal.ToString();
            this.nudMax.Text = garphData.MaxVal.ToString();
            this.nudScarm.Text = garphData.ScarmVal.ToString();
            if (garphData.ScarmTodo == 0)
            {
                this.rdo0.Checked = true;
            }
            else
            {
                this.rdo1.Checked = true;
            }
        }

        /// <summary>
        /// 保存测试
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_point == null)
            {
                if (string.IsNullOrEmpty(this.txtPoint.Text) || string.IsNullOrEmpty(this.txtName.Text))
                {
                    Var.MsgBoxWarn(this, "点位/名称不能为空。");
                    return;
                }

                //新增参数
                GraphData para1 = new GraphData();

                para1.Point = this.txtPoint.Text;
                para1.Unit = this.txtUnit.Text;
                para1.Name = this.txtName.Text;
                para1.MaxVal = this.nudMax.Value;
                para1.MinVal = this.nudMin.Value;
                para1.ScarmVal = this.nudScarm.Value;
                if (this.rdo0.Checked == true)
                {
                    para1.ScarmTodo = 0;
                }
                else
                {
                    para1.ScarmTodo = 1;
                }

                //DashboardConfig dashboardConfig = new DashboardConfig(_model);
                //dashboardConfig.Load();
                _graph.graphDatas.Add(para1);
                _graph.Save();
            }
            else
            {
                // 编辑参数
                //DashboardConfig dashboardConfig = new DashboardConfig(_model);
                //dashboardConfig.Load();
                var graphData = _graph.graphDatas.FirstOrDefault(d => d.Point == _point);
                graphData.Name = this.txtName.Text;
                graphData.MinVal = Convert.ToDouble(this.nudMin.Text);
                graphData.MaxVal = Convert.ToDouble(this.nudMax.Text);
                graphData.ScarmVal = Convert.ToDouble(this.nudScarm.Text);
                if (this.rdo0.Checked == true)
                {
                    graphData.ScarmTodo = 0;
                }
                else
                {
                    graphData.ScarmTodo = 1;
                }
                _graph.Save();
            }
            this.Close();
        }


        /// <summary>
        /// 返回
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 点击选择，出现需要绑定的点位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnselect_Click(object sender, EventArgs e)
        {
            frmGraphPointSelect frmPointSelect = new frmGraphPointSelect(_model);
            DialogResult dr = frmPointSelect.ShowDialog();

            // 获取返回的点位和单位
            string selectedPoint = frmPointSelect.SelectedPoint;
            string selectedUnit = frmPointSelect.SelectedUnit;

            // 将点位名称赋值给 txtPoint
            this.txtPoint.Text = selectedPoint;
            this.txtUnit.Text = selectedUnit;

        }
    }
}
