using DevComponents.Instrumentation;
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
    public partial class ucParamKeyUI : UserControl
    {
        public ucParamKeyUI()
        {
            InitializeComponent();
            //if (string.IsNullOrEmpty(Unit)) 
            //{
            //    this.lblUnit.Visible = false;
            //}
        }

        private string _title = "";
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                lblTitle.Text = value;
            }
        }

        private Font _titleFont = new Font("宋体", 20F, FontStyle.Regular);
        /// <summary>
        /// 标题字体
        /// </summary>
        public Font TitleFont
        {
            get => _titleFont;
            set
            {
                _titleFont = value;
                lblTitle.Font = value;
            }
        }

        private int _titleHeight = 51;
        public int TitleHeight
        {
            get => _titleHeight;
            set
            {
                _titleHeight = value;
                lblTitle.Height = value;
            }
        }

        // 增加背景色属性
        private Color _backgroundColor = Color.Gainsboro;
        public Color BackgroundColor
        {
            get => _backgroundColor;
            set
            {
                _backgroundColor = value;
                lblTitle.BackColor = value;

            }
        }

        private string _key = "";
        /// <summary>
        /// 绑定点位名称
        /// </summary>
        public string Key
        {
            get => _key;
            set { _key = value; }
        }

        private string _unit = "";
        /// <summary>
        /// 单位（显示在数值后面，不覆盖标题）
        /// </summary>
        public string Unit
        {
            get => _unit;
            set
            {
                _unit = value;
                UpdateValueDisplay(); // 只更新单位与颜色，不覆盖单位文本
            }
        }

        private double _gaugeValue;
        public double GaugeValue
        {
            get => _gaugeValue;
            set
            {
                _gaugeValue = value;
                var scale = gaugeControl.CircularScales[0];
                var ptr = scale.Pointers[0];
                ptr.Value = value;

                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(() =>
                    {
                        // 更新数值文本
                        lblValue.Text = value.ToString();

                        // 更新颜色：超过上限阈值变红，否则绿色
                        var hi = scale.Ranges.Count > 1 ? scale.Ranges[1].StartValue : scale.MaxValue;
                        lblValue.ForeColor = value > hi ? Color.Red : Color.Black;

                        // 同步更新单位显示（避免被 GaugeValue 覆盖）
                        UpdateValueDisplay();
                    }));
                }
                else
                {
                    // 更新数值文本
                    lblValue.Text = value.ToString();

                    // 更新颜色：超过上限阈值变红，否则绿色
                    var hi = scale.Ranges.Count > 1 ? scale.Ranges[1].StartValue : scale.MaxValue;
                    lblValue.ForeColor = value > hi ? Color.Red : Color.Black;

                    // 同步更新单位显示（避免被 GaugeValue 覆盖）
                    UpdateValueDisplay();
                }
            }
        }

        /// <summary>
        /// 设置量程范围 
        /// </summary>
        /// <param name="zero">最小值</param>
        /// <param name="max">最大值</param>
        /// <param name="mid">报警值</param>
        /// <returns></returns>
        public bool SetRand(double zero, double max, double mid)
        {
            this.gaugeControl.CircularScales[0].Ranges[0].StartValue = zero;
            this.gaugeControl.CircularScales[0].Ranges[0].EndValue = mid;
            this.gaugeControl.CircularScales[0].Ranges[1].StartValue = mid;
            this.gaugeControl.CircularScales[0].Ranges[1].EndValue = max;
            this.gaugeControl.CircularScales[0].MinValue = zero;
            this.gaugeControl.CircularScales[0].MaxValue = max;
            this.gaugeControl.CircularScales[0].MajorTickMarks.Interval = (max - zero) / 10;
            this.gaugeControl.CircularScales[0].MinorTickMarks.Interval = (max - zero) / 50;
            return true;
        }

        private void UpdateValueDisplay()
        {
            // 只在单位非空时追加一个空格和单位，避免重复追加
            var unitPart = string.IsNullOrWhiteSpace(_unit) ? "" : $" {_unit}";
            // 若需要保留一位小数：lblValue.Text = $"{_gaugeValue:F1}{unitPart}";
            lblValue.Text = $"{lblValue.Text}{unitPart}";
        }
    }
}