using System;
using System.IO;
using System.Text;
using System.Reflection;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using RW.UI;

/// <summary>
/// 全局操作日志记录器
/// 在 Program.cs 的 Main() 里调用一次 GlobalClickLogger.Instance.Start() 即可
/// </summary>
public class GlobalClickLogger : IMessageFilter
{
    // ── Win32 ─────────────────────────────────────────────────────────────
    private const int WM_LBUTTONDOWN = 0x0201;
    private const int WM_KEYDOWN = 0x0100;
    private const int VK_RETURN = 0x0D;

    [DllImport("user32.dll")]
    private static extern IntPtr GetParent(IntPtr hWnd);

    [DllImport("user32.dll")]
    private static extern bool GetCursorPos(out System.Drawing.Point pt);

    // ── 单例 ──────────────────────────────────────────────────────────────
    private static readonly Lazy<GlobalClickLogger> _inst =
        new Lazy<GlobalClickLogger>(() => new GlobalClickLogger());
    public static GlobalClickLogger Instance => _inst.Value;

    // ── 日志文件 ──────────────────────────────────────────────────────────
    private readonly string _logPath;
    private readonly string _errPath;
    private readonly object _lock = new object();

    private GlobalClickLogger()
    {
        string dir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");
        Directory.CreateDirectory(dir);
        _logPath = Path.Combine(dir, string.Format("click_{0:yyyyMMdd}.log", DateTime.Now));
        _errPath = Path.Combine(dir, string.Format("click_err_{0:yyyyMMdd}.log", DateTime.Now));
    }

    // ── 启停 ──────────────────────────────────────────────────────────────
    public void Start()
    {
        Application.AddMessageFilter(this);
        Application.Idle += OnIdle;
        // 写入启动标记，方便确认 Logger 已生效
        WriteLogCore("System", "GlobalClickLogger", "启动", "-", "-", "-", "系统");
    }

    public void Stop()
    {
        Application.RemoveMessageFilter(this);
        Application.Idle -= OnIdle;
        WriteLogCore("System", "GlobalClickLogger", "停止", "-", "-", "-", "系统");
    }

    // ══════════════════════════════════════════════════════════════════════
    // 消息过滤：按钮点击 + Enter 确认
    // 值类控件（TextBox / NumericUpDown 等）跳过，由各自 Leave/Changed 事件负责
    // ══════════════════════════════════════════════════════════════════════
    public bool PreFilterMessage(ref Message m)
    {
        // ── 鼠标左键点击 ─────────────────────────────────────────────────
        if (m.Msg == WM_LBUTTONDOWN)
        {
            try
            {
                Control ctrl = Control.FromHandle(m.HWnd);
                if (ctrl == null) ctrl = WalkUpToManagedControl(m.HWnd);
                if (ctrl != null) ctrl = HitTestDeepest(ctrl);
                if (ctrl != null && !IsLayoutContainer(ctrl) && !IsValueControl(ctrl))
                    WriteLog(ctrl, "点击");
            }
            catch (Exception ex) { LogError("PreFilterMessage-Click", ex); }
        }

        // ── Enter 键确认 ──────────────────────────────────────────────────
        if (m.Msg == WM_KEYDOWN && (int)m.WParam == VK_RETURN)
        {
            try
            {
                Control ctrl = Control.FromHandle(m.HWnd);
                if (ctrl == null) ctrl = WalkUpToManagedControl(m.HWnd);
                if (ctrl != null && !IsLayoutContainer(ctrl))
                    WriteLog(ctrl, "Enter确认");
            }
            catch (Exception ex) { LogError("PreFilterMessage-Enter", ex); }
        }

        return false; // 消息继续正常传递，不拦截
    }

    // ══════════════════════════════════════════════════════════════════════
    // Idle：自动扫描新出现的 Form 和控件，挂事件
    // ══════════════════════════════════════════════════════════════════════
    private readonly System.Collections.Generic.HashSet<Control> _hookedControls =
        new System.Collections.Generic.HashSet<Control>();
    private readonly System.Collections.Generic.HashSet<Form> _hookedForms =
        new System.Collections.Generic.HashSet<Form>();

    private void OnIdle(object sender, EventArgs e)
    {
        try
        {
            foreach (Form f in Application.OpenForms)
            {
                // ── Form 打开/关闭 ────────────────────────────────────────
                if (!_hookedForms.Contains(f))
                {
                    _hookedForms.Add(f);
                    f.Shown += OnFormShown;
                    f.FormClosed += OnFormClosed;
                }
                // ── 控件值事件 ────────────────────────────────────────────
                AttachControlEvents(f);
            }
        }
        catch (Exception ex) { LogError("OnIdle", ex); }
    }

    /// <summary>递归扫描控件树，对每种控件挂对应的值事件</summary>
    private void AttachControlEvents(Control parent)
    {
        foreach (Control c in parent.Controls)
        {
            if (!_hookedControls.Contains(c))
            {
                _hookedControls.Add(c);
                c.Disposed += OnControlDisposed;

                string typeName = c.GetType().Name;

                // DataGridView ─────────────────────────────────────────────
                if (c is DataGridView dgv)
                {
                    dgv.CellClick += OnDgvCellClick;
                }
                // TextBox / UITextBox → 失焦记最终输入值 ───────────────────
                else if (c is TextBox || typeName.Contains("TextBox"))
                {
                    c.Leave += OnTextLeave;
                }
                // NumericUpDown / UIDoubleUpDown / UIIntegerUpDown → 失焦记值
                else if (c is NumericUpDown
                      || typeName.Contains("DoubleUpDown")
                      || typeName.Contains("IntegerUpDown"))
                {
                    c.Leave += OnNumericLeave;
                }
                // ComboBox → 用户主动选择（不记程序赋值） ──────────────────
                else if (c is ComboBox cmb)
                {
                    cmb.SelectionChangeCommitted += OnComboSelected;
                }
                else if (typeName.Contains("ComboBox"))
                {
                    // SunnyUI UIComboBox 用 Leave 兜底
                    c.Leave += OnComboLeave;
                }
                // CheckBox / UICheckBox ─────────────────────────────────────
                else if (c is CheckBox cb)
                {
                    cb.CheckedChanged += OnCheckedChanged;
                }
                else if (typeName.Contains("CheckBox"))
                {
                    AttachEventReflection(c, "CheckedChanged",
                        new EventHandler(OnCheckedChangedReflect));
                }
                // RadioButton / UIRadioButton ───────────────────────────────
                else if (c is RadioButton rb)
                {
                    rb.CheckedChanged += OnCheckedChanged;
                }
                else if (typeName.Contains("RadioButton"))
                {
                    AttachEventReflection(c, "CheckedChanged",
                        new EventHandler(OnCheckedChangedReflect));
                }
                // TabControl / UITabControl → 切换 Tab ─────────────────────
                else if (c is TabControl tc)
                {
                    tc.SelectedIndexChanged += OnTabChanged;
                }
                else if (typeName.Contains("TabControl"))
                {
                    AttachEventReflection(c, "SelectedIndexChanged",
                        new EventHandler(OnTabChangedReflect));
                }
                // DateTimePicker → 关闭日历时才算确认 ─────────────────────
                else if (c is DateTimePicker dtp)
                {
                    dtp.CloseUp += OnDateCloseUp;
                }
                // TrackBar → 松手时记，不记拖动过程 ───────────────────────
                else if (c is TrackBar)
                {
                    c.MouseUp += OnTrackMouseUp;
                }
            }

            AttachControlEvents(c); // 递归子控件
        }
    }

    /// <summary>用反射挂事件，兼容 SunnyUI 等三方控件</summary>
    private void AttachEventReflection(Control c, string eventName, Delegate handler)
    {
        try
        {
            var evt = c.GetType().GetEvent(eventName,
                BindingFlags.Public | BindingFlags.Instance);
            if (evt != null) evt.AddEventHandler(c, handler);
        }
        catch (Exception ex) { LogError("AttachEventReflection:" + eventName, ex); }
    }

    private void OnControlDisposed(object sender, EventArgs e)
    {
        _hookedControls.Remove((Control)sender);
    }

    // ══════════════════════════════════════════════════════════════════════
    // 窗口事件
    // ══════════════════════════════════════════════════════════════════════
    private void OnFormShown(object sender, EventArgs e)
    {
        try
        {
            var f = (Form)sender;
            WriteLogCore("Form", f.Name, f.Text, "-", f.Name, f.Text, "打开窗口");
        }
        catch (Exception ex) { LogError("OnFormShown", ex); }
    }

    private void OnFormClosed(object sender, FormClosedEventArgs e)
    {
        try
        {
            var f = (Form)sender;
            WriteLogCore("Form", f.Name, f.Text, "-", f.Name, f.Text, "关闭窗口");
            _hookedForms.Remove(f);
        }
        catch (Exception ex) { LogError("OnFormClosed", ex); }
    }

    // ══════════════════════════════════════════════════════════════════════
    // 控件值事件处理
    // ══════════════════════════════════════════════════════════════════════
    private void OnTextLeave(object sender, EventArgs e)
    {
        try { WriteLog((Control)sender, "文本提交"); }
        catch (Exception ex) { LogError("OnTextLeave", ex); }
    }

    private void OnNumericLeave(object sender, EventArgs e)
    {
        try { WriteLog((Control)sender, "数值提交"); }
        catch (Exception ex) { LogError("OnNumericLeave", ex); }
    }

    private void OnComboSelected(object sender, EventArgs e)
    {
        try { WriteLog((Control)sender, "下拉选择"); }
        catch (Exception ex) { LogError("OnComboSelected", ex); }
    }

    private void OnComboLeave(object sender, EventArgs e)
    {
        try { WriteLog((Control)sender, "下拉选择"); }
        catch (Exception ex) { LogError("OnComboLeave", ex); }
    }

    private void OnCheckedChanged(object sender, EventArgs e)
    {
        try { WriteLog((Control)sender, "勾选"); }
        catch (Exception ex) { LogError("OnCheckedChanged", ex); }
    }

    private void OnCheckedChangedReflect(object sender, EventArgs e)
    {
        try { WriteLog((Control)sender, "勾选"); }
        catch (Exception ex) { LogError("OnCheckedChangedReflect", ex); }
    }

    private void OnTabChanged(object sender, EventArgs e)
    {
        try
        {
            var tc = (TabControl)sender;
            string tabText = tc.SelectedTab != null ? tc.SelectedTab.Text : "-";
            Form form = tc.FindForm();
            WriteLogCore("TabControl", tc.Name, tabText, "-",
                form != null ? form.Name : "-",
                form != null ? form.Text : "-",
                "切换Tab");
        }
        catch (Exception ex) { LogError("OnTabChanged", ex); }
    }

    private void OnTabChangedReflect(object sender, EventArgs e)
    {
        try
        {
            var c = (Control)sender;
            string tabText = "-";
            try
            {
                object selTab = c.GetType()
                    .GetProperty("SelectedTab", BindingFlags.Public | BindingFlags.Instance)
                    ?.GetValue(c, null);
                if (selTab != null)
                    tabText = selTab.GetType().GetProperty("Text")
                        ?.GetValue(selTab, null)?.ToString() ?? "-";
            }
            catch { }

            Form form = c.FindForm();
            WriteLogCore(c.GetType().Name, c.Name, tabText, "-",
                form != null ? form.Name : "-",
                form != null ? form.Text : "-",
                "切换Tab");
        }
        catch (Exception ex) { LogError("OnTabChangedReflect", ex); }
    }

    private void OnDateCloseUp(object sender, EventArgs e)
    {
        try { WriteLog((Control)sender, "日期选择"); }
        catch (Exception ex) { LogError("OnDateCloseUp", ex); }
    }

    private void OnTrackMouseUp(object sender, MouseEventArgs e)
    {
        try { WriteLog((Control)sender, "滑块"); }
        catch (Exception ex) { LogError("OnTrackMouseUp", ex); }
    }

    private void OnDgvCellClick(object sender, DataGridViewCellEventArgs e)
    {
        try
        {
            var dgv = (DataGridView)sender;
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            var cell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
            string col = dgv.Columns[e.ColumnIndex].HeaderText;
            Form form = dgv.FindForm();
            WriteLogCore("DataGridView", dgv.Name,
                string.Format("[行{0} {1}]", e.RowIndex, col),
                cell.Value != null ? cell.Value.ToString() : "(空)",
                form != null ? form.Name : "-",
                form != null ? form.Text : "-",
                "DGV单元格");
        }
        catch (Exception ex) { LogError("OnDgvCellClick", ex); }
    }

    // ══════════════════════════════════════════════════════════════════════
    // 判断辅助
    // ══════════════════════════════════════════════════════════════════════

    /// <summary>纯布局容器，Click 不记录</summary>
    private bool IsLayoutContainer(Control c)
    {
        return c is Form
            || c is Panel
            || c is GroupBox
            || c is SplitContainer
            || c is SplitterPanel
            || c is TabControl
            || c is DataGridView;
    }

    /// <summary>
    /// 值类控件：由专项 Leave/Changed 事件处理，Click 里跳过，避免重复记录
    /// </summary>
    private bool IsValueControl(Control c)
    {
        if (c is TextBox || c is NumericUpDown || c is CheckBox
         || c is RadioButton || c is ComboBox
         || c is TrackBar || c is DateTimePicker) return true;

        string n = c.GetType().Name;
        return n.Contains("TextBox")
            || n.Contains("DoubleUpDown")
            || n.Contains("IntegerUpDown")
            || n.Contains("CheckBox")
            || n.Contains("RadioButton")
            || n.Contains("ComboBox");
    }

    // ══════════════════════════════════════════════════════════════════════
    // 句柄查找 / 命中测试
    // ══════════════════════════════════════════════════════════════════════
    private Control WalkUpToManagedControl(IntPtr hwnd)
    {
        IntPtr h = hwnd;
        for (int i = 0; i < 16; i++)
        {
            h = GetParent(h);
            if (h == IntPtr.Zero) break;
            Control c = Control.FromHandle(h);
            if (c != null) return c;
        }
        return null;
    }

    private Control HitTestDeepest(Control root)
    {
        System.Drawing.Point screen;
        if (!GetCursorPos(out screen)) return root;
        Control current = root;
        for (int depth = 0; depth < 16; depth++)
        {
            System.Drawing.Point local = current.PointToClient(screen);
            Control child = current.GetChildAtPoint(local,
                GetChildAtPointSkip.Invisible | GetChildAtPointSkip.Disabled);
            if (child == null || child == current) break;
            current = child;
        }
        return current;
    }

    // ══════════════════════════════════════════════════════════════════════
    // 读取控件值（反射兜底，覆盖 SunnyUI / RW.UI 所有控件）
    // ══════════════════════════════════════════════════════════════════════
    private string GetValue(Control c)
    {
        // 标准控件直接读，避免反射开销
        if (c is CheckBox cb) return cb.Checked.ToString();
        if (c is RadioButton rb) return rb.Checked.ToString();
        if (c is ComboBox cmb) return cmb.Text;
        if (c is TextBox tb) return tb.Text;
        if (c is NumericUpDown n) return n.Value.ToString();
        if (c is TrackBar t) return t.Value.ToString();
        if (c is DateTimePicker d) return d.Value.ToString("yyyy-MM-dd HH:mm:ss");

        // 反射兜底：
        //   Switch  → RButton（开关状态）
        //   Checked → UICheckBox / UIRadioButton
        //   Value   → UIDoubleUpDown / UIIntegerUpDown
        var type = c.GetType();
        foreach (string prop in new[] { "Switch", "Checked", "Value", "SelectedValue", "SelectedItem" })
        {
            PropertyInfo pi = type.GetProperty(prop, BindingFlags.Public | BindingFlags.Instance);
            if (pi == null) continue;
            try
            {
                object val = pi.GetValue(c, null);
                if (val != null) return val.ToString();
            }
            catch { }
        }
        return string.IsNullOrEmpty(c.Text) ? "-" : c.Text;
    }

    // ══════════════════════════════════════════════════════════════════════
    // 写日志（统一入口）
    // ══════════════════════════════════════════════════════════════════════
    private void WriteLog(Control c, string trigger)
    {
        Form form = c.FindForm();
        WriteLogCore(
            c.GetType().Name,
            c.Name,
            c.Text,
            GetValue(c),
            form != null ? form.Name : "-",
            form != null ? form.Text : "-",
            trigger);
    }

    private void WriteLogCore(string ctrlType, string ctrlName,
                              string text, string value,
                              string formName, string formTitle,
                              string trigger)
    {
        string user = "-";
        try { user = RWUser.User != null ? RWUser.User.Username : "未登录"; }
        catch { }

        string line = string.Format(
            "[{0:yyyy-MM-dd HH:mm:ss.fff}] 用户={1,-10} [{2,-6}] 窗口={3}({4})  控件={5}/{6}  Text={7}  Value={8}",
            DateTime.Now, user, trigger,
            formTitle, formName,
            ctrlType, ctrlName,
            text, value);

        lock (_lock)
        {
            try { File.AppendAllText(_logPath, line + Environment.NewLine, Encoding.UTF8); }
            catch (Exception ex) { LogError("WriteLogCore-IO", ex); }
        }
    }

    // ══════════════════════════════════════════════════════════════════════
    // 错误日志（写入独立文件，不影响主流程）
    // ══════════════════════════════════════════════════════════════════════
    private void LogError(string location, Exception ex)
    {
        try
        {
            string line = string.Format("[{0:yyyy-MM-dd HH:mm:ss.fff}] [{1}] {2}",
                DateTime.Now, location, ex.Message);
            lock (_lock)
                File.AppendAllText(_errPath, line + Environment.NewLine, Encoding.UTF8);
        }
        catch { }
    }
}