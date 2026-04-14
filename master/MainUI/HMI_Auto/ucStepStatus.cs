using MainUI.Config.Test;
using RW.UI.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MainUI.HMI_Auto
{
    public partial class ucStepStatus : UserControl
    {
        List<ucStepItem> StepItems { get; set; } = new List<ucStepItem>();

        /// <summary>
        /// 360h 循环代码具体参数
        /// </summary>
        private List<DurStepConfig> currentStepConfigs { get; set; } = new List<DurStepConfig>();

        /// <summary>
        /// 100h 循环代码具体参数
        /// </summary>
        private Test100hConfig testConfigs { get; set; }

        /// <summary>
        /// 导航栏
        /// </summary>
        private List<RButton> navigationButtons { get; set; } = new List<RButton>();

        public ucStepStatus()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 加载 360h 表格和导航按钮
        /// </summary>
        public void LoadItem(List<DurStepConfig> list)
        {
            currentStepConfigs = list;

            // 清空现有控件
            this.flowLayoutPanel1.Controls.Clear();
            this.flowLayoutPanelTitle.Controls.Clear();
            StepItems.Clear();
            navigationButtons.Clear();

            // 创建导航按钮
            CreateNavigationButtons(list);

            // 加载步骤项
            foreach (var item in list)
            {
                ucStepItem step = new ucStepItem();
                step.SizeChanged += ucStepItem1_SizeChanged;
                step.InitItem(item.GetSectionName(), item.testBasePara);
                StepItems.Add(step);
                this.flowLayoutPanel1.Controls.Add(step);
                ResizeHeight(step);
            }

            // 默认选中第一个
            if (navigationButtons.Count > 0)
            {
                HighlightSelectedStep(0);
                navigationButtons[0].Switch = true;

                // 确保第一个项可见
                ScrollToStepItem(0);
            }
        }

        /// <summary>
        /// 创建360导航按钮
        /// </summary>
        private void CreateNavigationButtons(List<DurStepConfig> list)
        {
            // 优先使用系统配置的步骤名称
            var stepNames = Var.SysConfig?.TestStep360 ?? new List<string>();

            for (int i = 0; i < list.Count; i++)
            {
                RButton rButton = new RButton();

                // 设置按钮文本：优先使用TestStep360的名称，如果没有则使用DurStepConfig的名称
                string buttonText = i < stepNames.Count ? stepNames[i] : list[i].GetSectionName();
                rButton.Text = buttonText;

                rButton.FalseColor = Color.Silver;
                rButton.TrueColor = Color.Lime;
                rButton.Size = new Size(61, 33);
                rButton.SwitchType = SwitchStyleEnums.Switch;
                rButton.Tag = i; // 存储索引

                // 添加点击事件
                rButton.Click += (sender, e) =>
                {
                    var button = sender as RButton;
                    if (button != null && button.Tag != null)
                    {
                        int index = (int)button.Tag;
                        NavigateToStep(index);
                    }
                };

                navigationButtons.Add(rButton);
                this.flowLayoutPanelTitle.Controls.Add(rButton);
            }
        }

        /// <summary>
        /// 加载 100h 表格和导航按钮
        /// </summary>
        public void Load100hItem(Test100hConfig list)
        {
            testConfigs = list;

            // 清空现有控件
            this.flowLayoutPanel1.Controls.Clear();
            this.flowLayoutPanelTitle.Controls.Clear();
            StepItems.Clear();
            navigationButtons.Clear();

            // 创建导航按钮
            CreateNavigation100hButtons(list);

            // 加载步骤项
            int i = 1;
            foreach (var item in list.testStepLists)
            {
                ucStepItem step = new ucStepItem();
                step.SizeChanged += ucStepItem1_SizeChanged;
                step.InitItem($"{i}-{item.TestName}", item.testBasePara);
                StepItems.Add(step);
                this.flowLayoutPanel1.Controls.Add(step);
                ResizeHeight(step);
                i++;
            }

            // 默认选中第一个
            if (navigationButtons.Count > 0)
            {
                HighlightSelectedStep(0);
                navigationButtons[0].Switch = true;

                // 确保第一个项可见
                ScrollToStepItem(0);
            }
        }

        /// <summary>
        /// 创建100导航按钮
        /// </summary>
        private void CreateNavigation100hButtons(Test100hConfig list)
        {
            // 优先使用系统配置的步骤名称
            for (int i = 0; i < list.testStepLists.Count; i++)
            {
                // 阶段（循环代码）
                var stepData = list.testStepLists[i];

                RButton rButton = new RButton();

                // 设置按钮文本：优先使用TestStep360的名称，如果没有则使用DurStepConfig的名称
                string buttonText = $"{(i + 1)}-{stepData.TestName}";
                rButton.Text = buttonText;

                rButton.FalseColor = Color.Silver;
                rButton.TrueColor = Color.Lime;
                rButton.Size = new Size(200, 33);
                rButton.SwitchType = SwitchStyleEnums.Switch;
                rButton.Tag = i; // 存储索引

                // 添加点击事件
                rButton.Click += (sender, e) =>
                {
                    var button = sender as RButton;
                    if (button != null && button.Tag != null)
                    {
                        int index = (int)button.Tag;
                        NavigateToStep(index);
                    }
                };

                navigationButtons.Add(rButton);
                this.flowLayoutPanelTitle.Controls.Add(rButton);
            }
        }

        /// <summary>
        /// 导航到指定步骤
        /// </summary>
        private void NavigateToStep(int index)
        {
            if (index >= 0 && index < StepItems.Count)
            {
                // 高亮选中的步骤
                HighlightSelectedStep(index);

                // 滚动到对应的步骤项
                ScrollToStepItem(index);

                // 更新按钮状态
                UpdateButtonStates(index);
            }
        }

        /// <summary>
        /// 滚动到指定的步骤项（通过滚动条移动）
        /// </summary>
        private void ScrollToStepItem(int index)
        {
            if (index < StepItems.Count)
            {
                // 计算目标项在流布局面板中的位置
                int targetY = 0;
                for (int i = 0; i < index; i++)
                {
                    targetY += StepItems[i].Height + flowLayoutPanel1.Margin.Vertical;
                }

                //为了让正在试验保持在最中间
                this.flowLayoutPanel1.SetAutoScrollMargin(this.flowLayoutPanel1.Width / 2 - StepItems[index].Width / 2, 0);
                this.flowLayoutPanel1.ScrollControlIntoView(StepItems[index]);
            }
        }

        /// <summary>
        /// 更新按钮选中状态
        /// </summary>
        private void UpdateButtonStates(int selectedIndex)
        {
            for (int i = 0; i < navigationButtons.Count; i++)
            {
                navigationButtons[i].Switch = (i == selectedIndex);
            }
        }

        /// <summary>
        /// 高亮选中的步骤
        /// </summary>
        private void HighlightSelectedStep(int selectedIndex)
        {
            for (int i = 0; i < StepItems.Count; i++)
            {
                if (i == selectedIndex)
                {
                    // 选中项的高亮样式
                    StepItems[i].BackColor = Color.LightYellow;
                    StepItems[i].BorderStyle = BorderStyle.FixedSingle;
                }
                else
                {
                    // 非选中项的默认样式
                    StepItems[i].BackColor = Color.FromArgb(243, 249, 255);
                    StepItems[i].BorderStyle = BorderStyle.None;
                }
            }
        }

        private void ucStepItem1_SizeChanged(object sender, EventArgs e)
        {
            var con = sender as Control;
            ResizeHeight(con);
        }

        /// <summary>
        /// 自定义控制高度
        /// </summary>
        private void ResizeHeight(Control con)
        {
            //this.Height = con.Height + 9 + 14;
        }

        /// <summary>
        /// 高亮指定步骤的指定行（单行高亮）
        /// </summary>
        /// <param name="stepIndex">步骤索引（从0开始）</param>
        /// <param name="rowIndex">行索引（从0开始）</param>
        public void HighlightStepRow(int stepIndex, int rowIndex)
        {
            if (stepIndex >= 0 && stepIndex < StepItems.Count)
            {
                // 先更新导航栏到指定步骤
                NavigateToStep(stepIndex);

                StepItems[stepIndex].HighlightRow(rowIndex);
            }
        }

        /// <summary>
        /// 高亮当前选中步骤的指定行
        /// </summary>
        /// <param name="rowIndex">行索引（从0开始）</param>
        public void HighlightCurrentStepRow(int rowIndex)
        {
            int currentStepIndex = GetCurrentStepIndex();
            if (currentStepIndex >= 0)
            {
                HighlightStepRow(currentStepIndex, rowIndex);
            }
        }

        /// <summary>
        /// 清除所有步骤的高亮
        /// </summary>
        public void ClearAllHighlights()
        {
            foreach (var stepItem in StepItems)
            {
                stepItem.ClearHighlight();
            }
        }

        /// <summary>
        /// 清除指定步骤的高亮
        /// </summary>
        /// <param name="stepIndex">步骤索引（从0开始）</param>
        public void ClearStepHighlight(int stepIndex)
        {
            if (stepIndex >= 0 && stepIndex < StepItems.Count)
            {
                StepItems[stepIndex].ClearHighlight();
            }
        }

        /// <summary>
        /// 获取指定步骤的高亮行索引
        /// </summary>
        public int GetStepHighlightedRow(int stepIndex)
        {
            if (stepIndex >= 0 && stepIndex < StepItems.Count)
            {
                return StepItems[stepIndex].GetHighlightedRow();
            }
            return -1;
        }

        /// <summary>
        /// 获取当前步骤的高亮行索引
        /// </summary>
        public int GetCurrentStepHighlightedRow()
        {
            int currentStepIndex = GetCurrentStepIndex();
            if (currentStepIndex >= 0)
            {
                return GetStepHighlightedRow(currentStepIndex);
            }
            return -1;
        }

        /// <summary>
        /// 检查指定步骤是否有高亮行
        /// </summary>
        public bool HasStepHighlightedRow(int stepIndex)
        {
            if (stepIndex >= 0 && stepIndex < StepItems.Count)
            {
                return StepItems[stepIndex].HasHighlightedRow();
            }
            return false;
        }

        /// <summary>
        /// 提供外部导航方法
        /// </summary>
        public void NavigateToStepByIndex(int index)
        {
            NavigateToStep(index);
        }

        /// <summary>
        /// 提供外部导航方法（通过名称）
        /// </summary>
        public void NavigateToStepByName(string stepName)
        {
            for (int i = 0; i < StepItems.Count; i++)
            {
                if (StepItems[i].Title.Contains(stepName))
                {
                    NavigateToStep(i);
                    break;
                }
            }
        }

        /// <summary>
        /// 获取当前选中的步骤索引
        /// </summary>
        public int GetCurrentStepIndex()
        {
            for (int i = 0; i < navigationButtons.Count; i++)
            {
                if (navigationButtons[i].Switch)
                {
                    return i;
                }
            }
            return -1;
        }

        /// <summary>
        /// 导航到下一个步骤
        /// </summary>
        public void NavigateToNextStep()
        {
            int currentIndex = GetCurrentStepIndex();
            if (currentIndex >= 0 && currentIndex < StepItems.Count - 1)
            {
                NavigateToStep(currentIndex + 1);
            }
        }

        /// <summary>
        /// 导航到上一个步骤
        /// </summary>
        public void NavigateToPreviousStep()
        {
            int currentIndex = GetCurrentStepIndex();
            if (currentIndex > 0)
            {
                NavigateToStep(currentIndex - 1);
            }
        }
    }
}