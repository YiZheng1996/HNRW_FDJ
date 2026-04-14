using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using RW.Test.UI.BLL;
using MainUI.BLL;

namespace MainUI.Procedure
{
    public partial class ucModelType : UserControl
    {
        public ucModelType()
        {
            InitializeComponent();
        }

        private void ucModelType_Load(object sender, EventArgs e)
        {
            LoadTypes();
        }

        private void LoadTypes()
        {
            this.lstType.DisplayMember = "ModelType";
            this.lstType.ValueMember = "ID";
            this.lstType.DataSource = bll.GetAllModelType();
        }



        ModelTypeBLL bll = new ModelTypeBLL();
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("请先输入型号类别", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (txtName.Text.StartsWith(" "))
            {
                MessageBox.Show("型号类别首字符不能为空格。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if(bll.IsExist(txtName.Text))
            {
                MessageBox.Show("该类别名称已经存在，请重新输入。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return;
            }


            try
            {
                bool isok = bll.Add(txtName.Text);
                if (isok)
                {
                    MessageBox.Show("添加型号类别成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadTypes();
                }
                else
                    MessageBox.Show("添加型号类别失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                MessageBox.Show("添加型号类别出错！" + err, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("请先输入型号类别", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtName.Text.StartsWith(" "))
            {
                MessageBox.Show("型号类别首字符不能为空格。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (bll.IsExist(txtName.Text))
            {
                MessageBox.Show("该类别名称已经存在，请重新输入。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return;
            }

            try
            {
                bool isok = bll.Update(txtID.Text, txtName.Text);
                if (isok)
                {
                    MessageBox.Show("更改型号类别成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadTypes();
                }
                else
                    MessageBox.Show("更改型号类别失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                MessageBox.Show("更改型号类别出错！" + err, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lstType.SelectedItems.Count < 1)
            {
                MessageBox.Show("没有选择型号类别，请先选择类别！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                bool isok = bll.Delete(CurID.ToString());
                if (isok)
                {
                    MessageBox.Show("删除型号类别成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadTypes();
                }
                else
                    MessageBox.Show("删除型号类别失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                MessageBox.Show("删除型号类别出错！" + err, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        int CurID = 0;
        private void lstType_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurID = Convert.ToInt32(this.lstType.SelectedValue);
            string name = this.lstType.Text;

            this.txtID.Text = CurID.ToString();
            this.txtName.Text = name;
        }

       

       







    }
}
