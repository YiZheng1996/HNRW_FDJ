using MainUI.Procedure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainUI
{
    public partial class ucParaManger : UserControl
    {
        public ucParaManger()
        {
            InitializeComponent();
            this.treeView1.ExpandAll();
            //默认打开第一个
            if (this.treeView1.Nodes.Count > 0)
            {
                this.ShowView(this.treeView1.Nodes[0]);
            }
        }

        public delegate void SelectedNodeHandler(object sender, SettingNode node);
        public event SelectedNodeHandler SelectedChanged;

        private List<TreeNode> treeNodes = new List<TreeNode>();  // 改个更清晰的名字

        // 私有字段存储节点
        private List<SettingNode> settingNodes = new List<SettingNode>();

        // 添加这个公共属性 - 与 SettingNode 的 Nodes 不同
        public List<SettingNode> SettingNodes
        {
            get { return settingNodes; }
            set { settingNodes = value; }
        }

        public void AddNodes(SettingNode snode)
        {
            // 关键修复：将节点添加到 settingNodes 列表
            if (settingNodes == null)
                settingNodes = new List<SettingNode>();

            settingNodes.Add(snode);

            // 原有逻辑：创建 TreeNode 并添加到 treeNodes 列表
            TreeNode treeNode = new TreeNode(snode.Text);
            treeNode.Name = snode.Name;
            treeNode.Tag = snode.UI;
            treeNodes.Add(treeNode);

            // 如果有子节点，也需要处理
            if (snode.Nodes != null && snode.Nodes.Count > 0)
            {
                foreach (var childNode in snode.Nodes)
                {
                    AddChildTreeNode(treeNode, childNode);
                }
            }
            if (this.treeView1 != null)
            {
                TreeNode nodeCopy = new TreeNode(snode.Text);
                nodeCopy.Name = snode.Name;
                nodeCopy.Tag = snode.UI;
                this.treeView1.Nodes.Add(nodeCopy);
                this.treeView1.ExpandAll();
                if (this.treeView1.Nodes.Count == 1)
                {
                    this.treeView1.SelectedNode = nodeCopy; // 选中当前添加的节点
                }
            }
           
        }

        // 添加子节点到 TreeView 的方法
        private void AddChildTreeNode(TreeNode parentTreeNode, SettingNode childSettingNode)
        {
            TreeNode childTreeNode = new TreeNode(childSettingNode.Text);
            childTreeNode.Name = childSettingNode.Name;
            childTreeNode.Tag = childSettingNode.UI;
            parentTreeNode.Nodes.Add(childTreeNode);

            // 递归处理更深的子节点
            if (childSettingNode.Nodes != null && childSettingNode.Nodes.Count > 0)
            {
                foreach (var grandChild in childSettingNode.Nodes)
                {
                    AddChildTreeNode(childTreeNode, grandChild);
                }
            }
        }

        public void AddNodes(string text, UserControl control)
        {
            this.AddNodes(new SettingNode(text, control));
        }

        // 添加一个方法来重新加载节点到 TreeView（如果需要在 UI 中显示）
        public void ReloadTreeView()
        {
            this.treeView1.Nodes.Clear();

            // 将所有 SettingNode 转换为 TreeNode
            foreach (var settingNode in settingNodes)
            {
                TreeNode treeNode = InitNode(settingNode);
                this.treeView1.Nodes.Add(treeNode);
            }

            this.treeView1.ExpandAll();
            if (this.treeView1.Nodes.Count > 0)
            {
                this.ShowView(this.treeView1.Nodes[0]);
            }
        }

        // 原有的 InitNode 方法保持不变
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
            // 修改为：将 treeNodes 中的所有 TreeNode 添加到 TreeView
            this.treeView1.Nodes.AddRange(treeNodes.ToArray());
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
            foreach (Control c in this.panelMain.Controls)
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
                this.panelMain.Controls.Add(uc);
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

    public class SettingNode
    {

        public SettingNode()
        {

        }

        public SettingNode(string text)
        {
            this.Text = text;
        }

        public SettingNode(string text, UserControl uc)
        {
            this.text = text;
            this.UI = uc;
        }

        private int id;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        private string text;

        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }


        private Control ui;

        public Control UI
        {
            get { return ui; }
            set { ui = value; }
        }

        private List<SettingNode> nodes = new List<SettingNode>();

        public List<SettingNode> Nodes
        {
            get { return nodes; }
            set { nodes = value; }
        }
    }
}
