using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MainUI.BLL;
using RW.UI.Manager;

namespace MainUI.Procedure
{
    public partial class ucModelManage : ucBaseManagerUI
    {
        //ModelBLL bll = new ModelBLL(Var.Database, Var.ConnectionString, "Model");
        ModelBLL bll = new ModelBLL();

        string grpTxt = "";

        public ucModelManage()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
        }

        private void ucModelManage_Load(object sender, EventArgs e)
        {
            try
            {
                grpTxt = groupBox1.Text;
                LoadModelType();
                LoadModel();
            }
            catch (Exception ex)
            {
                Var.MsgBoxWarn(this, $"型号管理界面初始化失败；原因：{ex.Message}");
            }
        }
        private void LoadModelType()
        {
            // ModelTypeBLL BLLmodelType = new ModelTypeBLL(Var.Database, Var.ConnectionString, "ModelType");
            ModelTypeBLL BLLmodelType = new ModelTypeBLL();

            DataTable dt = BLLmodelType.GetAllModelType();
            if (dt == null)
                return;
            cboModelType.DataSource = dt;
            cboModelType.DisplayMember = "ModelType";
            cboModelType.ValueMember = "ID";

        }
        private void LoadModel()
        {
            DataTable dt = bll.GetAllKind();


            dataGridView1.DataSource = dt;
            if (dt == null)
                return;
            int cnt = dt.Rows.Count;
            groupBox1.Text = grpTxt + "(" + cnt + ")";

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string curModel = txtModelName.Text.Trim();
                if (string.IsNullOrEmpty(curModel))
                    return;
                int typeid = Convert.ToInt32(cboModelType.SelectedValue.ToString());
                string mark = txtChexing.Text;
                if (bll.IsExist(curModel))
                {
                    Var.MsgBoxInfo(this, $"型号【" + curModel + "】已存在");
                    return;
                }
                if (bll.Add(curModel, typeid, mark))
                {
                    Var.MsgBoxSuccess(this, "新增成功");
                    LoadModel();
                }
                else
                {
                    Var.MsgBoxWarn(this, "新增失败");
                }
            }
            catch (Exception)
            {
                Var.MsgBoxWarn(this, "发生错误，新增失败");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string curModel = txtModelName.Text.Trim();
            string modelID = txtID.Text;
            if (string.IsNullOrEmpty(curModel))
                return;

            if (bll.Delete(modelID))
            {
                Var.MsgBoxSuccess(this, "删除成功");
                LoadModel();
            }
            else
            {
                Var.MsgBoxWarn(this, "删除失败");


            }
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            try
            {
                string curModel = txtModelName.Text.Trim();
                if (string.IsNullOrEmpty(curModel))
                    return;
                int typeid = Convert.ToInt32(cboModelType.SelectedValue.ToString());
                string mark = txtChexing.Text;

                string curID = txtID.Text;
                if (bll.Update(curID, curModel, typeid, mark))
                {

                    Var.MsgBoxSuccess(this, "修改成功");
                    LoadModel();
                }
                else
                {
                    Var.MsgBoxWarn(this, "修改失败");
                }
            }
            catch (Exception)
            {
                Var.MsgBoxWarn(this, "发生错误，修改失败");
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ShowCurRecord();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            ShowCurRecord();
        }
        private void ShowCurRecord()
        {
            if (dataGridView1.DataSource == null)
                return;
            if (dataGridView1.Rows.Count < 1)
                return;
            string id = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["id"].Value.ToString();
            //string id = dataGridView1.SelectedRows[0].Cells["id"].Value.ToString();
            string name = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["name"].Value.ToString();
            string typeid = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["typeid"].Value.ToString();
            string modeltype = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["modeltype"].Value.ToString();
            string mark = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["mark"].Value.ToString();
            txtID.Text = id;
            txtModelName.Text = name;
            cboModelType.Text = modeltype;
            txtChexing.Text = mark;
        }
    }
}
