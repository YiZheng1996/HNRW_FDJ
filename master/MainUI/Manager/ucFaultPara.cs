using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MainUI.Widgets;
using MainUI.Config;

namespace MainUI.Manager
{
    public partial class ucFaultPara : UserControl
    {
        public ucFaultPara()
        {
            InitializeComponent();
        }


        private void ucFaultPara_Load(object sender, EventArgs e)
        {
            try
            {
                LoadFaultInfo();
            }
            catch (Exception ex)
            {
                string err = "加载故障信息有误；原因：" + ex.Message;
                Var.MsgBoxWarn(this,err);
            }
        }

        // 启动机
        Dictionary<string, FaultEntity> QDdicUC1 = new Dictionary<string, FaultEntity>();
        Dictionary<string, FaultEntity> QDdicUC2 = new Dictionary<string, FaultEntity>();
        Dictionary<string, FaultEntity> QDdicUC3 = new Dictionary<string, FaultEntity>();

        // 配电柜-管路
        Dictionary<string, FaultEntity> PDdicUC1 = new Dictionary<string, FaultEntity>();

        public void LoadFaultInfo()
        {
            ConfigManager.FaultConfig.Load();

            flowLayoutPanel1.Controls.Clear();
            QDdicUC1.Clear();

            ConfigManager.FaultConfig.Save();

            foreach (var item in ConfigManager.FaultConfig.QDdicFault1)
            {
                uclblTextBox lbl = new uclblTextBox();
                string index =  item.Value.Index;
                lbl.Index =  index.ToInt();
                lbl.Title = "故障序号" + index;
                lbl.FaultName = item.Value.FaultName;
                flowLayoutPanel1.Controls.Add(lbl);
            }

            flowLayoutPanel2.Controls.Clear();
            QDdicUC2.Clear();
            foreach (var item in ConfigManager.FaultConfig.QDdicFault2)
            {
                uclblTextBox lbl = new uclblTextBox();
                string index =  item.Value.Index ;
                lbl.Index = index.ToInt();
                lbl.Title = "故障序号" + index;
                lbl.FaultName = item.Value.FaultName;
                flowLayoutPanel2.Controls.Add(lbl);
            }

            flowLayoutPanel3.Controls.Clear();
            QDdicUC3.Clear();
            foreach (var item in ConfigManager.FaultConfig.QDdicFault3)
            {
                uclblTextBox lbl = new uclblTextBox();
                string index =  item.Value.Index;
                lbl.Index = index.ToInt();
                lbl.Title = "故障序号" + index;
                lbl.FaultName = item.Value.FaultName;
                flowLayoutPanel3.Controls.Add(lbl);
            }

            flowLayoutPanel4.Controls.Clear();
            PDdicUC1.Clear();
            foreach (var item in ConfigManager.FaultConfig.PDdicFault1)
            {
                uclblTextBox lbl = new uclblTextBox();
                string index = item.Value.Index;
                lbl.Index = index.ToInt();
                lbl.Title = "故障序号" + index;
                lbl.FaultName = item.Value.FaultName;
                flowLayoutPanel4.Controls.Add(lbl);
            }
        }



        private void btnAdd_Click(object sender, EventArgs e)
        {
            // 启动机
            if (tabControl1.SelectedTab == tabPage1)
            {
                if(flowLayoutPanel1.Controls.Count>=32)
                {
                    Var.MsgBoxInfo(this,"最多允许添加32个故障代号。");
                    return;
                }
                uclblTextBox lbl = new uclblTextBox();
                int index =1000+ flowLayoutPanel1.Controls.Count + 1;
                lbl.Index = index;
                lbl.Title = "故障序号" + index.ToString().PadLeft(3,'0');
                lbl.FaultName = "";
                flowLayoutPanel1.Controls.Add(lbl);
            }

            if (tabControl1.SelectedTab == tabPage2)
            {
                if (flowLayoutPanel2.Controls.Count >= 32)
                {
                    Var.MsgBoxInfo(this,"最多允许添加32个故障代号。");
                    return;
                }

                uclblTextBox lbl = new uclblTextBox();
                int index = 2000 + flowLayoutPanel2.Controls.Count + 1;
                lbl.Index = index;
                lbl.Title = "故障序号" + index.ToString().PadLeft(3, '0');
                lbl.FaultName = "";
                flowLayoutPanel2.Controls.Add(lbl);
            }

            if (tabControl1.SelectedTab == tabPage3)
            {
                if (flowLayoutPanel3.Controls.Count >= 32)
                {
                    Var.MsgBoxInfo(this,"最多允许添加32个故障代号。");
                    return;
                }
                uclblTextBox lbl = new uclblTextBox();
                int index = 3000 + flowLayoutPanel3.Controls.Count + 1;
                lbl.Index = index;
                lbl.Title = "故障序号" + index.ToString().PadLeft(3, '0');
                lbl.FaultName = "";
                flowLayoutPanel3.Controls.Add(lbl);
            }

            // 配电柜
            if (tabControl1.SelectedTab == tabPage4)
            {
                if (flowLayoutPanel4.Controls.Count >= 32)
                {
                    Var.MsgBoxInfo(this,"最多允许添加32个故障代号。");
                    return;
                }
                uclblTextBox lbl = new uclblTextBox();
                int index = 1000 + flowLayoutPanel4.Controls.Count + 1;
                lbl.Index = index;
                lbl.Title = "故障序号" + index.ToString().PadLeft(3, '0');
                lbl.FaultName = "";
                flowLayoutPanel4.Controls.Add(lbl);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

            if (tabControl1.SelectedTab == tabPage1)
            {
                if (flowLayoutPanel1.Controls.Count < 1)
                {
                    Var.MsgBoxInfo(this,"一级故障没有故障信息可以删除。");
                    return;
                }

                flowLayoutPanel1.Controls.RemoveAt(flowLayoutPanel1.Controls.Count - 1);
            }

            if (tabControl1.SelectedTab == tabPage2)
            {
                if (flowLayoutPanel2.Controls.Count < 1)
                {
                    Var.MsgBoxInfo(this,"二级故障没有故障信息可以删除。");
                    return;
                }
                flowLayoutPanel2.Controls.RemoveAt(flowLayoutPanel2.Controls.Count - 1);
            }

            if (tabControl1.SelectedTab == tabPage3)
            {
                if (flowLayoutPanel3.Controls.Count < 1)
                {
                    Var.MsgBoxInfo(this,"三级故障没有故障信息可以删除。");
                    return;
                }
                flowLayoutPanel3.Controls.RemoveAt(flowLayoutPanel3.Controls.Count - 1);
            }

            if (tabControl1.SelectedTab == tabPage4)
            {
                if (flowLayoutPanel4.Controls.Count < 1)
                {
                    Var.MsgBoxInfo(this,"一级故障没有故障信息可以删除。");
                    return;
                }
                flowLayoutPanel4.Controls.RemoveAt(flowLayoutPanel4.Controls.Count - 1);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //一级故障
                QDdicUC1.Clear();
                foreach (var item in flowLayoutPanel1.Controls)
                {
                    uclblTextBox lbl = item as uclblTextBox;

                    FaultEntity fe = new FaultEntity();
                    fe.Index =  lbl.Index.ToString();
                    fe.Titile = lbl.Title;
                    fe.FaultName = lbl.FaultName;
                    QDdicUC1.Add(fe.Index, fe);
                }
                ConfigManager.FaultConfig.QDdicFault1 = QDdicUC1;
                ConfigManager.FaultConfig.Save();

                //二级故障
                QDdicUC2.Clear();
                foreach (var item in flowLayoutPanel2.Controls)
                {
                    uclblTextBox lbl = item as uclblTextBox;

                    FaultEntity fe = new FaultEntity();
                    fe.Index = lbl.Index.ToString() ;
                    fe.Titile = lbl.Title;
                    fe.FaultName = lbl.FaultName;
                    QDdicUC2.Add(fe.Index, fe);
                }
                ConfigManager.FaultConfig.QDdicFault2 = QDdicUC2;
                ConfigManager.FaultConfig.Save();

                //三级故障
                QDdicUC3.Clear();
                foreach (var item in flowLayoutPanel3.Controls)
                {
                    uclblTextBox lbl = item as uclblTextBox;

                    FaultEntity fe = new FaultEntity();
                    fe.Index =  lbl.Index.ToString();
                    fe.Titile = lbl.Title;
                    fe.FaultName = lbl.FaultName;
                    QDdicUC3.Add(fe.Index, fe);
                }
                ConfigManager.FaultConfig.QDdicFault3 = QDdicUC3;
                ConfigManager.FaultConfig.Save();
                Var.MsgBoxInfo(this, "故障信息保存成功。");


                //三级故障
                PDdicUC1.Clear();
                foreach (var item in flowLayoutPanel1.Controls)
                {
                    uclblTextBox lbl = item as uclblTextBox;

                    FaultEntity fe = new FaultEntity();
                    fe.Index = lbl.Index.ToString();
                    fe.Titile = lbl.Title;
                    fe.FaultName = lbl.FaultName;
                    QDdicUC3.Add(fe.Index, fe);
                }
                ConfigManager.FaultConfig.PDdicFault1 = PDdicUC1;
                ConfigManager.FaultConfig.Save();
                Var.MsgBoxInfo(this, "故障信息保存成功。");
            }
            catch (Exception ex)
            {
                string err = "保存故障信息有误；原因：" + ex.Message;
                Var.MsgBoxWarn(this,err);

            }
        }

       
    }
}
