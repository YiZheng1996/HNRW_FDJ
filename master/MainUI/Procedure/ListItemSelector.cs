using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MainUI.Procedure
{
    public partial class ListItemSelector : Form
    {
        public ListItemSelector()
        {
            InitializeComponent();
            this.ItemSize = new Size(230, 35);
            this.Columns = 1;
        }

        private void ListItemSelector_Load(object sender, EventArgs e)
        {
            if (DisplayCancelButton)
            {
                this.Add("取消", delegate { });
            }
        }

        private void SetSize()
        {
            if (columns >= 0)
            {
                this.flowLayoutPanel1.MaximumSize = new Size((this.ItemSize.Width + 6) * columns, 0);
            }
        }

        private Size itemSize;
        [DefaultValue(typeof(Size), "230,35")]
        public Size ItemSize { get { return itemSize; } set { itemSize = value; } }

        private bool cancelButton = false;
        [DefaultValue(false)]
        public bool DisplayCancelButton { get { return cancelButton; } set { cancelButton = value; } }

        private int columns;
        /// <summary>
        /// 指定显示的列数量，为0表示不指定
        /// </summary>
        [DefaultValue(1)]
        public int Columns { get { return columns; } set { columns = value; SetSize(); } }

        [Obsolete("此方法不建议使用，建议使用Add(object text, object value)方法")]
        public Button Add(object item, string displayMember, string valueMember)
        {
            Button btn = new Button();
            if (item is DataRowView)
            {
                btn.Name = ((DataRowView)item).Row[valueMember].ToString();
                btn.Text = ((DataRowView)item).Row[displayMember].ToString();
            }
            else
            {
                btn.Name = item.ToString();
                btn.Text = item.ToString();
            }
            btn.Tag = item;
            btn.Size = ItemSize;
            btn.Click += new EventHandler(btn_Click);
            btn.DialogResult = DialogResult.OK;

            this.flowLayoutPanel1.Controls.Add(btn);
            this.Height = this.flowLayoutPanel1.Height + 26;
            return btn;
        }

        public Button Add(object text, object value)
        {
            Button btn = new Button();
            btn.Text = text.ToString();
            btn.Tag = value;
            btn.Size = ItemSize;
            btn.Click += new EventHandler(btn_Click);
            btn.DialogResult = DialogResult.OK;

            this.flowLayoutPanel1.Controls.Add(btn);
            this.Height = this.flowLayoutPanel1.Height + 26;
            return btn;
        }

        public Button Add(string text, EventHandler click)
        {
            Button btn = new Button();
            btn.Text = text;
            btn.Size = ItemSize;
            btn.Click += click;
            btn.DialogResult = DialogResult.Cancel;
            this.CancelButton = btn;
            this.flowLayoutPanel1.Controls.Add(btn);
            this.Height = this.flowLayoutPanel1.Height + 26;
            return btn;
        }

        public void Add(object item)
        {
            Add(item, null, null);
        }

        public void AddRange(object[] items)
        {
            foreach (object item in items)
            {
                Add(item);
            }
        }

        void btn_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            this.SelectedItem = btn.Tag;
            OnItemSelector();
        }

        private object selectedItem;
        [DefaultValue(null)]
        public object SelectedItem
        {
            get { return selectedItem; }
            protected set { selectedItem = value; }
        }

        protected virtual void OnItemSelector()
        {
            if (ItemSelector != null) ItemSelector(this, EventArgs.Empty);
            //this.Close();
        }

        public event EventHandler ItemSelector;
    }

}
