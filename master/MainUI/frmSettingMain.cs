using MainUI.Procedure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MainUI
{
    public partial class frmSettingMain : Form
    {
        public frmSettingMain()
        {
            InitializeComponent();
            //默认展开全部
            this.treeView1.ExpandAll();
            //默认打开第一个
            if (this.treeView1.Nodes.Count > 0)
            {
                //this.ShowView(this.treeView1.Nodes[0]);
            }
        }
        public delegate void SelectedNodeHandler(object sender, SettingNode node);
        public event SelectedNodeHandler SelectedChanged;
        private List<TreeNode> nodes = new List<TreeNode>();
        public void AddNodes(SettingNode snode)
        {
            TreeNode node = new TreeNode(snode.Text);
            node.Name = snode.Name;
            node.Tag = snode.UI;
            nodes.Add(node);
        }
        public void AddNodes(string text, UserControl control)
        {
            this.AddNodes(new SettingNode(text, control));
        }
        private TreeNode InitNode(SettingNode snode)
        {
            TreeNode tnode = new TreeNode(snode.Text);
            tnode.Name = snode.Name;
            tnode.Tag = snode.UI;
            foreach (SettingNode n in snode.Nodes)
            {
                TreeNode node = InitNode(n);
                tnode.Nodes.Add(node);
            }
            return tnode;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            this.treeView1.Nodes.AddRange(nodes.ToArray());
            this.treeView1.ExpandAll();
        }

        private void treeView1_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            //正常打开时，返回true，无法打开时，返回false，并取消选中
            e.Cancel = !ShowView(e.Node);
        }

        private bool ShowView(TreeNode node)
        {
            if (node != null)
            {
                if (node.Tag is UserControl)
                {
                    UserControl uc = node.Tag as UserControl;
                    this.AddItem(uc);
                }
                else if (node.Nodes.Count > 0)
                {
                    ShowView(node.Nodes[0]);
                }
            }
            return true;
        }

        private void AddItem(UserControl uc)
        {
            bool exists = false;
            foreach (Control c in this.pnlMain.Controls)
            {
                if (c != null && c.GetType().FullName == uc.GetType().FullName)
                {
                    exists = true;
                    if (c is ucBaseManagerUI)
                        (c as ucBaseManagerUI).Reload();
                    c.Show();
                }
                else
                {
                    c.Hide();
                }
            }
            if (!exists)
            {
                uc.Dock = DockStyle.Fill;
                this.pnlMain.Controls.Add(uc);
                uc.BringToFront();
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            Control c = e.Node.Tag as Control;
            if (c is ucBaseManagerUI)
                (c as ucBaseManagerUI).Reload();
        }
    }

    //public class SettingNode
    //{

    //    public SettingNode()
    //    {

    //    }

    //    public SettingNode(string text)
    //    {
    //        this.Text = text;
    //    }

    //    public SettingNode(string text, UserControl uc)
    //    {
    //        this.text = text;
    //        this.UI = uc;
    //    }

    //    private int id;

    //    public int ID
    //    {
    //        get { return id; }
    //        set { id = value; }
    //    }

    //    private string text;

    //    public string Text
    //    {
    //        get { return text; }
    //        set { text = value; }
    //    }

    //    private string name;

    //    public string Name
    //    {
    //        get { return name; }
    //        set { name = value; }
    //    }


    //    private Control ui;

    //    public Control UI
    //    {
    //        get { return ui; }
    //        set { ui = value; }
    //    }

    //    private List<SettingNode> nodes = new List<SettingNode>();

    //    public List<SettingNode> Nodes
    //    {
    //        get { return nodes; }
    //        set { nodes = value; }
    //    }

    //}
}