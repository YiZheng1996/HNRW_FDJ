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
    public partial class ucPipePara : UserControl
    {
        public ucPipePara()
        {
            InitializeComponent();
            // 初始化单位高度为设计器中的高度（22）
            _unitHeight = lblUnit.Height;
        }

        private string _title = "";
        public string Title
        {
            get { return _title; }
            set { _title = value; lblTitle.Text = value; }
        }

        private double _value;
        public double Value
        {
            get { return _value; }
            set
            {
                _value = value;
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(() =>
                    {
                        lblValue.Text = value.ToString("F1"); // 保留一位小数
                    }));
                }
                else
                {
                    lblValue.Text = value.ToString("F1");
                }
            }
        }

        private int _titleHeight = 32;
        public int TitleHeight
        {
            get { return _titleHeight; }
            set
            {
                _titleHeight = value;
                UpdateTitleHeight();
            }
        }

        private string _unit = "℃";
        public string Unit
        {
            get { return _unit; }
            set
            {
                _unit = value;
                if (string.IsNullOrEmpty(value))
                {
                    // 不设置单位则隐藏
                    if (this.InvokeRequired)
                    {
                        this.Invoke(new Action(() =>
                        {
                            lblUnit.Height = 0;
                            lblUnit.Text = "";
                        }));
                    }
                    else
                    {
                        lblUnit.Height = 0;
                        lblUnit.Text = "";
                    }
                }
                else
                {
                    // 存在单位则显示
                    if (this.InvokeRequired)
                    {
                        this.Invoke(new Action(() =>
                        {
                            lblUnit.Text = value;
                            lblUnit.Height = _unitHeight;
                        }));
                    }
                    else
                    {
                        lblUnit.Text = value;
                        lblUnit.Height = _unitHeight;
                    }
                }
            }
        }

        private int _unitHeight = 22;
        public int UnitHeight
        {
            get { return _unitHeight; }
            set
            {
                _unitHeight = value;
                if (!string.IsNullOrEmpty(_unit))
                {
                    // 如果当前有单位，则更新高度
                    if (this.InvokeRequired)
                    {
                        this.Invoke(new Action(() =>
                        {
                            lblUnit.Height = _unitHeight;
                        }));
                    }
                    else
                    {
                        lblUnit.Height = _unitHeight;
                    }
                }
            }
        }

        private void UpdateTitleHeight()
        {
            if (lblTitle != null)
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(() =>
                    {
                        lblTitle.Height = _titleHeight;
                    }));
                }
                else
                {
                    lblTitle.Height = _titleHeight;
                }
            }
        }
    }
}