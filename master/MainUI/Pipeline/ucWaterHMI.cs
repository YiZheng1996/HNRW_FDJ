using MainUI.Modules;
using MainUI.Widget;
using RW.UI.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainUI
{
    public partial class ucWaterHMI : UserControl
    {
        public ucWaterHMI()
        {
            InitializeComponent();
        }

        // 数字量的集合
        Dictionary<string, SwitchPictureBox> dicValve = new Dictionary<string, SwitchPictureBox>();
        // 模拟量的集合
        Dictionary<string, ucPipePara> DoubleDicValve = new Dictionary<string, ucPipePara>();

        public void Init()
        {
            LoadAllValve();

            Common.DOgrp.KeyValueChange += DOgrp_KeyValueChange;
            Common.AOgrp.KeyValueChange += AOgrp_KeyValueChange;
            Common.AIgrp.KeyValueChange += AIgrp_KeyValueChange;
            Common.waterGrp.KeyValueChange += WaterGrp_KeyValueChange; ;

            //登录第1次刷新当前值
            Common.DOgrp.Fresh();
            Common.AOgrp.Fresh();
            Common.waterGrp.Fresh();
            Common.AIgrp.Fresh();
        }

        /// <summary>
        /// 计油数据更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WaterGrp_KeyValueChange(object sender, DoubleValueChangedEventArgs e)
        {
            if (DoubleDicValve.ContainsKey(e.Key))
            {
                DoubleDicValve[e.Key].Value = e.Value;
            }
        }

        /// <summary>
        /// AI数据更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AIgrp_KeyValueChange(object sender, DoubleValueChangedEventArgs e)
        {
            if (DoubleDicValve.ContainsKey(e.Key))
            {
                DoubleDicValve[e.Key].Value = e.Value;
            }
        }

        /// <summary>
        /// AO数据更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AOgrp_KeyValueChange(object sender, DoubleValueChangedEventArgs e)
        {
            if (DoubleDicValve.ContainsKey(e.Key))
            {
                DoubleDicValve[e.Key].Value = e.Value;
            }
        }

        /// <summary>
        /// 泵/阀状态变更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DOgrp_KeyValueChange(object sender, Modules.DIValueChangedEventArgs e)
        {
            if (dicValve.ContainsKey(e.Key))
            {
                dicValve[e.Key].Switch = e.Value;
            }
        }

        /// <summary>
        /// 把所有点位添加到字典
        /// </summary>
        private void LoadAllValve()
        {
            try
            {
                foreach (var item in this.Controls)
                {
                    // 如果为阀/泵
                    if (item is SwitchPictureBox)
                    {
                        SwitchPictureBox sw = item as SwitchPictureBox;
                        if (sw.Tag != null && string.IsNullOrEmpty(sw.Tag.ToString()) == false)
                        {
                            dicValve.Add(sw.Tag.ToString(), sw);
                        }
                    }

                    // 如果为模拟量
                    if (item is ucPipePara)
                    {
                        ucPipePara upp = item as ucPipePara;
                        if (upp.Tag != null && string.IsNullOrEmpty(upp.Tag.ToString()) == false)
                        {
                            DoubleDicValve.Add(upp.Tag.ToString(), upp);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        /// <summary>
        /// 通用点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sw_Valve_Click(object sender, EventArgs e)
        {
            SwitchPictureBox sw = sender as SwitchPictureBox;
            if (sw.Tag == null)
            {
                return;
            }
            if (string.IsNullOrEmpty(sw.Tag.ToString()))
            {
                return;
            }
            Common.DOgrp[sw.Tag.ToString()] = !Common.DOgrp[sw.Tag.ToString()];
        }
    }
}
