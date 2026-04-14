using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainUI.Widget
{
    public partial class ucValueLabel : UserControl
    {
        public ucValueLabel()
        {
            InitializeComponent();
        }

        private string _key = "";
        public string Key
        {
            get { return _key; }
            set { _key = value; }
        }

        private string _title = "";
        public string Title
        {
            get { return _title; }
            set { _title = value; lblName.Text = value; }
        }

        private double _value;
        public double Value
        {
            get { return _value; }
            set
            {
                _value = value;
                if (!this.DesignMode)
                {
                    if (this.InvokeRequired)
                    {
                        this.Invoke(new Action(() =>
                        {
                            lblValue.Text = value.ToString();
                        }));
                    }
                    else
                    {
                        lblValue.Text = value.ToString();
                    }
                }
            }
        }

        private string _unit = "℃";
        public string Unit
        {
            get { return _unit; }
            set
            {
                _unit = value;
                this.lblUnit.Text = value;
                this.Refresh();
            }
        }

        // 新增：标题文本颜色属性
        private Color _titleColor = Color.Black;
        public Color TitleColor
        {
            get { return _titleColor; }
            set
            {
                _titleColor = value;
                if (!this.DesignMode)
                {
                    if (this.InvokeRequired)
                    {
                        this.Invoke(new Action(() =>
                        {
                            lblName.ForeColor = value;
                        }));
                    }
                    else
                    {
                        lblName.ForeColor = value;
                    }
                }
                else
                {
                    lblName.ForeColor = value;
                }
            }
        }

        // 新增：数值文本颜色属性
        private Color _valueColor = Color.White;
        public Color ValueColor
        {
            get { return _valueColor; }
            set
            {
                _valueColor = value;
                if (!this.DesignMode)
                {
                    if (this.InvokeRequired)
                    {
                        this.Invoke(new Action(() =>
                        {
                            lblValue.ForeColor = value;
                        }));
                    }
                    else
                    {
                        lblValue.ForeColor = value;
                    }
                }
                else
                {
                    lblValue.ForeColor = value;
                }
            }
        }

        // 新增：数值标签宽度属性
        private int _valueWidth = 114;
        public int ValueWidth
        {
            get { return _valueWidth; }
            set
            {
                _valueWidth = value;
                if (!this.DesignMode)
                {
                    if (this.InvokeRequired)
                    {
                        this.Invoke(new Action(() =>
                        {
                            lblValue.Size = new Size(value, lblValue.Height);
                            panel1.Size = new Size(value, panel1.Height);
                            AdjustLayout();
                        }));
                    }
                    else
                    {
                        lblValue.Size = new Size(value, lblValue.Height);
                        panel1.Size = new Size(value, panel1.Height);
                        AdjustLayout();
                    }
                }
                else
                {
                    lblValue.Size = new Size(value, lblValue.Height);
                    panel1.Size = new Size(value, panel1.Height);
                    AdjustLayout();
                }
            }
        }

        // 新增：单位标签宽度属性
        private int _unitWidth = 52;
        public int UnitWidth
        {
            get { return _unitWidth; }
            set
            {
                _unitWidth = value;
                if (!this.DesignMode)
                {
                    if (this.InvokeRequired)
                    {
                        this.Invoke(new Action(() =>
                        {
                            lblUnit.Size = new Size(value, lblUnit.Height);
                            AdjustLayout();
                        }));
                    }
                    else
                    {
                        lblUnit.Size = new Size(value, lblUnit.Height);
                        AdjustLayout();
                    }
                }
                else
                {
                    lblUnit.Size = new Size(value, lblUnit.Height);
                    AdjustLayout();
                }
            }
        }

        // 新增：标题标签宽度属性
        private int _titleWidth = 108;
        public int TitleWidth
        {
            get { return _titleWidth; }
            set
            {
                _titleWidth = value;
                if (!this.DesignMode)
                {
                    if (this.InvokeRequired)
                    {
                        this.Invoke(new Action(() =>
                        {
                            lblName.Size = new Size(value, lblName.Height);
                            AdjustLayout();
                        }));
                    }
                    else
                    {
                        lblName.Size = new Size(value, lblName.Height);
                        AdjustLayout();
                    }
                }
                else
                {
                    lblName.Size = new Size(value, lblName.Height);
                    AdjustLayout();
                }
            }
        }

        // 新增：标题字体属性
        private Font _titleFont = new Font("宋体", 15F, FontStyle.Bold);
        public Font TitleFont
        {
            get { return _titleFont; }
            set
            {
                _titleFont = value;
                if (!this.DesignMode)
                {
                    if (this.InvokeRequired)
                    {
                        this.Invoke(new Action(() =>
                        {
                            lblName.Font = value;
                            AdjustLayout();
                        }));
                    }
                    else
                    {
                        lblName.Font = value;
                        AdjustLayout();
                    }
                }
                else
                {
                    lblName.Font = value;
                    AdjustLayout();
                }
            }
        }

        // 新增：数值字体属性
        private Font _valueFont = new Font("宋体", 17F, FontStyle.Bold);
        public Font ValueFont
        {
            get { return _valueFont; }
            set
            {
                _valueFont = value;
                if (!this.DesignMode)
                {
                    if (this.InvokeRequired)
                    {
                        this.Invoke(new Action(() =>
                        {
                            lblValue.Font = value;
                            AdjustLayout();
                        }));
                    }
                    else
                    {
                        lblValue.Font = value;
                        AdjustLayout();
                    }
                }
                else
                {
                    lblValue.Font = value;
                    AdjustLayout();
                }
            }
        }

        // 新增：单位字体属性
        private Font _unitFont = new Font("宋体", 15F, FontStyle.Bold);
        public Font UnitFont
        {
            get { return _unitFont; }
            set
            {
                _unitFont = value;
                if (!this.DesignMode)
                {
                    if (this.InvokeRequired)
                    {
                        this.Invoke(new Action(() =>
                        {
                            lblUnit.Font = value;
                            AdjustLayout();
                        }));
                    }
                    else
                    {
                        lblUnit.Font = value;
                        AdjustLayout();
                    }
                }
                else
                {
                    lblUnit.Font = value;
                    AdjustLayout();
                }
            }
        }

        // 辅助方法：调整布局
        private void AdjustLayout()
        {
            // 调整 panel1 的位置，确保不会与 lblUnit 重叠
            if (panel1 != null && lblUnit != null)
            {
                //panel1.Width = this.Width - lblUnit.Width - lblName.Width - 20; // 留出一些边距
                //lblName.Location = new Point(10, (this.Height - lblName.Height) / 2);
            }
        }

        // 重写 OnSizeChanged 方法，确保布局自适应
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            AdjustLayout();
        }

    }
}
