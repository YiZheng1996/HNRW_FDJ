namespace MainUI.Widget
{
    partial class ucParamKeyUI
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            DevComponents.Instrumentation.GaugeCircularScale gaugeCircularScale1 = new DevComponents.Instrumentation.GaugeCircularScale();
            DevComponents.Instrumentation.GaugePointer gaugePointer1 = new DevComponents.Instrumentation.GaugePointer();
            DevComponents.Instrumentation.GaugeRange gaugeRange1 = new DevComponents.Instrumentation.GaugeRange();
            DevComponents.Instrumentation.GaugeRange gaugeRange2 = new DevComponents.Instrumentation.GaugeRange();
            DevComponents.Instrumentation.GaugeSection gaugeSection1 = new DevComponents.Instrumentation.GaugeSection();
            DevComponents.Instrumentation.GradientFillColor gradientFillColor1 = new DevComponents.Instrumentation.GradientFillColor();
            DevComponents.Instrumentation.GradientFillColor gradientFillColor2 = new DevComponents.Instrumentation.GradientFillColor();
            this.cbName = new Sunny.UI.UIComboBox();
            this.lblValue = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.gaugeControl = new DevComponents.Instrumentation.GaugeControl();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gaugeControl)).BeginInit();
            this.SuspendLayout();
            // 
            // cbName
            // 
            this.cbName.DataSource = null;
            this.cbName.FillColor = System.Drawing.Color.White;
            this.cbName.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbName.ItemHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this.cbName.ItemSelectForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.cbName.Location = new System.Drawing.Point(218, 72);
            this.cbName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbName.MinimumSize = new System.Drawing.Size(63, 0);
            this.cbName.Name = "cbName";
            this.cbName.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.cbName.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.cbName.Size = new System.Drawing.Size(244, 29);
            this.cbName.Style = Sunny.UI.UIStyle.Custom;
            this.cbName.TabIndex = 3;
            this.cbName.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.cbName.Visible = false;
            this.cbName.Watermark = "";
            this.cbName.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // lblValue
            // 
            this.lblValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblValue.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblValue.Font = new System.Drawing.Font("宋体", 21F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblValue.Location = new System.Drawing.Point(0, 285);
            this.lblValue.Name = "lblValue";
            this.lblValue.Size = new System.Drawing.Size(243, 33);
            this.lblValue.TabIndex = 6;
            this.lblValue.Text = "0.0";
            this.lblValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.gaugeControl);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 51);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(243, 234);
            this.panel1.TabIndex = 10;
            // 
            // gaugeControl
            // 
            this.gaugeControl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            gaugeCircularScale1.MaxPin.Name = "MaxPin";
            gaugeCircularScale1.MinPin.Name = "MinPin";
            gaugeCircularScale1.Name = "Scale1";
            gaugePointer1.CapFillColor.BorderColor = System.Drawing.Color.DimGray;
            gaugePointer1.CapFillColor.BorderWidth = 1;
            gaugePointer1.CapFillColor.Color1 = System.Drawing.Color.WhiteSmoke;
            gaugePointer1.CapFillColor.Color2 = System.Drawing.Color.DimGray;
            gaugePointer1.FillColor.BorderColor = System.Drawing.Color.DimGray;
            gaugePointer1.FillColor.BorderWidth = 1;
            gaugePointer1.FillColor.Color1 = System.Drawing.Color.Red;
            gaugePointer1.FillColor.Color2 = System.Drawing.Color.Red;
            gaugePointer1.Length = 0.358F;
            gaugePointer1.Name = "Pointer1";
            gaugePointer1.Style = DevComponents.Instrumentation.PointerStyle.Needle;
            gaugePointer1.Width = 0.2F;
            gaugeCircularScale1.Pointers.AddRange(new DevComponents.Instrumentation.GaugePointer[] {
            gaugePointer1});
            gaugeRange1.EndValue = 75D;
            gaugeRange1.FillColor.BorderColor = System.Drawing.Color.Black;
            gaugeRange1.FillColor.BorderWidth = 1;
            gaugeRange1.FillColor.Color1 = System.Drawing.Color.Lime;
            gaugeRange1.FillColor.Color2 = System.Drawing.Color.Yellow;
            gaugeRange1.Name = "Range1";
            gaugeRange1.StartValue = 0D;
            gaugeRange1.StartWidth = 0.3F;
            gaugeRange2.EndValue = 100D;
            gaugeRange2.FillColor.BorderColor = System.Drawing.Color.Black;
            gaugeRange2.FillColor.BorderWidth = 1;
            gaugeRange2.FillColor.Color1 = System.Drawing.Color.Red;
            gaugeRange2.FillColor.Color2 = System.Drawing.Color.Red;
            gaugeRange2.Name = "Range3";
            gaugeRange2.StartValue = 70D;
            gaugeRange2.StartWidth = 0.3F;
            gaugeCircularScale1.Ranges.AddRange(new DevComponents.Instrumentation.GaugeRange[] {
            gaugeRange1,
            gaugeRange2});
            gaugeSection1.FillColor.Color1 = System.Drawing.Color.Transparent;
            gaugeSection1.FillColor.Color2 = System.Drawing.Color.Transparent;
            gaugeSection1.Name = "Section1";
            gaugeCircularScale1.Sections.AddRange(new DevComponents.Instrumentation.GaugeSection[] {
            gaugeSection1});
            this.gaugeControl.CircularScales.AddRange(new DevComponents.Instrumentation.GaugeCircularScale[] {
            gaugeCircularScale1});
            this.gaugeControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gaugeControl.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            gradientFillColor1.Color1 = System.Drawing.Color.Gainsboro;
            gradientFillColor1.Color2 = System.Drawing.Color.DarkGray;
            this.gaugeControl.Frame.BackColor = gradientFillColor1;
            gradientFillColor2.BorderColor = System.Drawing.Color.Gainsboro;
            gradientFillColor2.BorderWidth = 1;
            gradientFillColor2.Color1 = System.Drawing.Color.White;
            gradientFillColor2.Color2 = System.Drawing.Color.DimGray;
            this.gaugeControl.Frame.FrameColor = gradientFillColor2;
            this.gaugeControl.Frame.Style = DevComponents.Instrumentation.GaugeFrameStyle.Circular;
            this.gaugeControl.Location = new System.Drawing.Point(0, 0);
            this.gaugeControl.Margin = new System.Windows.Forms.Padding(7);
            this.gaugeControl.Name = "gaugeControl";
            this.gaugeControl.Size = new System.Drawing.Size(243, 234);
            this.gaugeControl.TabIndex = 3;
            this.gaugeControl.Text = "gaugeControl1";
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(243, 51);
            this.lblTitle.TabIndex = 9;
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ucParamKeyUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 28F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblValue);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.cbName);
            this.Font = new System.Drawing.Font("宋体", 21F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ucParamKeyUI";
            this.Size = new System.Drawing.Size(243, 318);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gaugeControl)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UIComboBox cbName;
        private System.Windows.Forms.Label lblValue;
        private System.Windows.Forms.Panel panel1;
        private DevComponents.Instrumentation.GaugeControl gaugeControl;
        private System.Windows.Forms.Label lblTitle;
    }
}
