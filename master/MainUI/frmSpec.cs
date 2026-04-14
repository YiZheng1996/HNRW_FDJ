using MainUI.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MainUI
{
    public partial class frmSpec : Form
    {
        /// <summary>
        /// 是否为主界面选择
        /// </summary>
        public bool IsMainSelect { get; set; }

        /// <summary>
        /// 选择的型号
        /// </summary>
        public string SelectModel { get; set; }

        /// <summary>
        /// 选择的型号
        /// </summary>
        public string SelectModelType { get; set; }

        public frmSpec(bool isMainSelect = false)
        {
            IsMainSelect = isMainSelect;
            InitializeComponent();
        }
        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmSpec_Load(object sender, EventArgs e)
        {
            uiDataGridView1.AutoGenerateColumns = false;
            uiDataGridView1.ReadOnly = true;
            BindModels();

            btnGet_Click(null, null);
        }
        // ModelTypeBLL pbll = new ModelTypeBLL(Var.Database, Var.ConnectionString, "ModelType");
        ModelTypeBLL pbll = new ModelTypeBLL();


        int[] TypeIDAry = null;

        /// <summary>
        /// 获取被试品类别列表
        /// </summary>
        private void BindModels()
        {
            try
            {
                cboType.Items.Clear();
                DataTable dt = pbll.GetAllModelType();
                int cnt = dt.Rows.Count;
                TypeIDAry = new int[cnt];
                foreach (DataRow item in dt.Rows)
                {
                    int idx = this.cboType.Items.Add(item[1].ToString());
                    TypeIDAry[idx] = Convert.ToInt32(item[0]);
                }

                //此种方式赋值，对cboType_SelectedValueChanged值变化事件不安全，弃用
                //this.cboType.DataSource = dt;
                //this.cboType.DisplayMember = "ModelType";
                //this.cboType.ValueMember = "ID";

                if (cboType.Items.Count > 0)
                    cboType.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("类别加载异常；原因：" + ex.Message);
            }
        }

        private void cboType_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (TypeIDAry.Length < 1)
                    return;
                //ModelBLL bModel = new ModelBLL(Var.Database, Var.ConnectionString, "Model");
                ModelBLL bModel = new ModelBLL();

                DataTable dt = bModel.GetAllKindByCon(" and b.ID =" + TypeIDAry[cboType.SelectedIndex]);
                cboModel.ValueMember = "ID";
                cboModel.DisplayMember = "Name";
                cboModel.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        /// <summary>
        /// 上翻
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (uiDataGridView1.Rows.Count > 0)
            {
                if (this.uiDataGridView1.CurrentRow.Index > 0)
                {
                    int i = uiDataGridView1.Rows.GetPreviousRow(uiDataGridView1.CurrentRow.Index, DataGridViewElementStates.None);//获取原选定上一行索引
                    uiDataGridView1.Rows[i].Selected = true; //选中整行
                    uiDataGridView1.CurrentCell = uiDataGridView1[1, i];//指针上移
                }
            }
        }
        /// <summary>
        /// 下翻
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            if (uiDataGridView1.Rows.Count > 0)
            {
                if (this.uiDataGridView1.CurrentRow.Index < this.uiDataGridView1.Rows.Count - 1)
                {
                    int i = uiDataGridView1.Rows.GetNextRow(uiDataGridView1.CurrentRow.Index, DataGridViewElementStates.None);//获取原选定下一行索引
                    uiDataGridView1.Rows[i].Selected = true; //选中整行
                    uiDataGridView1.CurrentCell = uiDataGridView1[1, i];//指针下移
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (uiDataGridView1.Rows.Count == 0)
            {
                return;
            }
            GetModel();
        }

        public void GetModel()
        {
            if (IsMainSelect)
            {
                Common.mTestViewModel.ModelID = Convert.ToInt32(uiDataGridView1.Rows[uiDataGridView1.CurrentRow.Index].Cells["colID"].Value);//得到当前选择的型号ID
                Common.mTestViewModel.ModelType = uiDataGridView1.Rows[uiDataGridView1.CurrentRow.Index].Cells["TypeName"].Value.ToString();//得到当前选择的型号类别
                Common.mTestViewModel.ModelName = uiDataGridView1.Rows[uiDataGridView1.CurrentRow.Index].Cells["colUsername"].Value.ToString();//得到当前选择的型号名称
                Common.mTestViewModel.Mark = uiDataGridView1.Rows[uiDataGridView1.CurrentRow.Index].Cells["colPassword"].Value.ToString();//得到当前选择的备注
                Common.mTestViewModel.ModelTypeID = Convert.ToInt32(uiDataGridView1.Rows[uiDataGridView1.CurrentRow.Index].Cells["colTypeid"].Value);//得到当前选择的备注
            }

            SelectModel = uiDataGridView1.Rows[uiDataGridView1.CurrentRow.Index].Cells["colUsername"].Value.ToString();
            SelectModelType = uiDataGridView1.Rows[uiDataGridView1.CurrentRow.Index].Cells["TypeName"].Value.ToString();

            this.DialogResult = DialogResult.Yes;
            this.Close();
            this.Dispose();
        }

        public DataTable GetData(int TypeId, string ModelName)
        {
            // ModelBLL modelBll = new ModelBLL(Var.Database, Var.ConnectionString, "Model");
            ModelBLL modelBll = new ModelBLL();

            DataTable dt = new DataTable();
            StringBuilder sb = new StringBuilder();
            if (TypeId >= 0)
            {
                sb.AppendFormat(" and b.ID ={0}", TypeId);
            }
            if (ModelName != "")
            {
                sb.AppendFormat(" and Name like '%" + ModelName + "%'");
            }
            dt = modelBll.GetAllKindByCon(sb.ToString());
            return dt;
        }

        private void btnGet_Click(object sender, EventArgs e)
        {

            try
            {
                int typeid = -1;
                string model = cboModel.Text;
                if (cboType.SelectedValue == null)
                {
                    typeid = -1;
                }
                else
                    typeid = TypeIDAry[cboType.SelectedIndex];

                if (cboModel.SelectedValue == null)
                {
                    model = "";
                }

                DataTable dt = GetData(typeid, model);
                uiDataGridView1.DataSource = dt;

                //DataTable dt = GetData(dicType[cboType.SelectedValue.ToString()], cboModel.Text);
                //uiDataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView_Spec_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            GetModel();
        }




    }
}
