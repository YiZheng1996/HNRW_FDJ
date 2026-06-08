using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using RW.UI;

/// <summary>
/// 全局操作日志记录器（焦点消息版）
/// 在 Program.cs 的 Main() 里调用一次 GlobalClickLogger.Instance.Start() 即可
/// 
/// 核心思路：
///   抛弃 .NET 包装的 Leave 事件（会被 SunnyUI 的 public new event 隐藏），
///   改用底层 Win32 消息 WM_KILLFOCUS / WM_LBUTTONDOWN / WM_KEYDOWN，
///   任何 WinForms 控件都逃不掉，无视 SunnyUI / RW.UI 的事件重写。
/// </summary>
public class GlobalClickLogger : IMessageFilter
{
    // ── Win32 消息 ─────────────────────────────────────────────────────────
    private const int WM_LBUTTONDOWN = 0x0201;
    private const int WM_LBUTTONUP = 0x0202;
    private const int WM_KEYDOWN = 0x0100;
    private const int WM_KILLFOCUS = 0x0008;   // ★ 失去焦点
    private const int WM_SETFOCUS = 0x0007;    // 获得焦点
    private const int VK_RETURN = 0x0D;
    private const int VK_TAB = 0x09;

    [DllImport("user32.dll")]
    private static extern IntPtr GetParent(IntPtr hWnd);

    [DllImport("user32.dll")]
    private static extern bool GetCursorPos(out System.Drawing.Point pt);

    [DllImport("user32.dll")]
    private static extern IntPtr GetFocus();

    // ── 单例 ───────────────────────────────────────────────────────────────
    private static readonly Lazy<GlobalClickLogger> _inst =
        new Lazy<GlobalClickLogger>(() => new GlobalClickLogger());
    public static GlobalClickLogger Instance => _inst.Value;

    // ── 日志文件 ───────────────────────────────────────────────────────────
    private readonly string _logPath;
    private readonly string _errPath;
    private readonly object _lock = new object();

    // ── 状态：上次焦点的控件 + 进入时的初始值（用于判断是否真的改了值） ──
    private Control _lastFocused;
    private string _lastFocusedInitialValue;

    private GlobalClickLogger()
    {
        string dir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");
        Directory.CreateDirectory(dir);
        _logPath = Path.Combine(dir, string.Format("控件点击_{0:yyyyMMdd}.log", DateTime.Now));
        _errPath = Path.Combine(dir, string.Format("控件点击_err_{0:yyyyMMdd}.log", DateTime.Now));
    }

    // ── 启停 ───────────────────────────────────────────────────────────────
    public void Start()
    {
        Application.AddMessageFilter(this);
        Application.Idle += OnIdle;
        WriteLogCore("System", "GlobalClickLogger", "启动", "-", "-", "-", "系统");
    }

    public void Stop()
    {
        Application.RemoveMessageFilter(this);
        Application.Idle -= OnIdle;
        WriteLogCore("System", "GlobalClickLogger", "停止", "-", "-", "-", "系统");
    }

    // ══════════════════════════════════════════════════════════════════════
    // 消息过滤（核心）
    // ══════════════════════════════════════════════════════════════════════
    public bool PreFilterMessage(ref Message m)
    {
        try
        {
            switch (m.Msg)
            {
                case WM_LBUTTONDOWN:
                    HandleMouseDown(m.HWnd);
                    break;

                case WM_KEYDOWN:
                    if ((int)m.WParam == VK_RETURN)
                        HandleEnter(m.HWnd);
                    break;

                case WM_SETFOCUS:
                    HandleFocusEnter(m.HWnd);
                    break;

                case WM_KILLFOCUS:
                    HandleFocusLeave(m.HWnd);
                    break;
            }
        }
        catch (Exception ex) { LogError("PreFilterMessage", ex); }

        return false;
    }

    // ══════════════════════════════════════════════════════════════════════
    // 鼠标点击
    // ══════════════════════════════════════════════════════════════════════
    private void HandleMouseDown(IntPtr hwnd)
    {
        Control ctrl = Control.FromHandle(hwnd);
        if (ctrl == null) ctrl = WalkUpToManagedControl(hwnd);
        if (ctrl != null) ctrl = HitTestDeepest(ctrl);
        if (ctrl == null) return;

        Control outer = FindOuterValueControl(ctrl) ?? ctrl;

        if (!IsLayoutContainer(outer) && !IsInputControl(outer))
            WriteLog(outer, "点击");
    }

    // ══════════════════════════════════════════════════════════════════════
    // Enter 键
    // ══════════════════════════════════════════════════════════════════════
    private void HandleEnter(IntPtr hwnd)
    {
        Control ctrl = Control.FromHandle(hwnd);
        if (ctrl == null) ctrl = WalkUpToManagedControl(hwnd);
        if (ctrl == null) return;

        Control outer = FindOuterValueControl(ctrl) ?? ctrl;
        if (!IsLayoutContainer(outer))
            WriteLog(outer, "Enter确认");
    }

    // ══════════════════════════════════════════════════════════════════════
    // 焦点进入：记录初始值，便于失焦时比较是否变化
    // ══════════════════════════════════════════════════════════════════════
    private void HandleFocusEnter(IntPtr hwnd)
    {
        Control ctrl = Control.FromHandle(hwnd);
        if (ctrl == null) ctrl = WalkUpToManagedControl(hwnd);
        if (ctrl == null) return;

        Control outer = FindOuterValueControl(ctrl) ?? ctrl;
        if (IsInputControl(outer))
        {
            _lastFocused = outer;
            _lastFocusedInitialValue = SafeGetValue(outer);
        }
    }

    // ══════════════════════════════════════════════════════════════════════
    // 焦点离开：值类控件统一在此记录
    // 这是核心 ── 绕开所有 .NET 包装事件被 SunnyUI new 隐藏的问题
    // ══════════════════════════════════════════════════════════════════════
    private void HandleFocusLeave(IntPtr hwnd)
    {
        Control ctrl = Control.FromHandle(hwnd);
        if (ctrl == null) ctrl = WalkUpToManagedControl(hwnd);
        if (ctrl == null) return;

        Control outer = FindOuterValueControl(ctrl) ?? ctrl;
        if (!IsInputControl(outer)) return;

        string current = SafeGetValue(outer);

        if (_lastFocused == outer)
        {
            // 只在值确实变化时才记录，避免大量"未操作但失焦"的噪声
            if (current != _lastFocusedInitialValue)
            {
                string trigger = IsNumericLike(outer) ? "数值提交" : "文本提交";
                WriteLog(outer, trigger);
            }
            _lastFocused = null;
            _lastFocusedInitialValue = null;
        }
        else
        {
            // 焦点跳跃兜底
            string trigger = IsNumericLike(outer) ? "数值提交" : "文本提交";
            WriteLog(outer, trigger);
        }
    }

    // ══════════════════════════════════════════════════════════════════════
    // Idle：扫描窗体，挂"开窗/关窗" + 非输入框类控件事件
    // ══════════════════════════════════════════════════════════════════════
    private readonly HashSet<Control> _hookedControls = new HashSet<Control>();
    private readonly HashSet<Form> _hookedForms = new HashSet<Form>();

    private void OnIdle(object sender, EventArgs e)
    {
        try
        {
            foreach (Form f in Application.OpenForms)
            {
                if (!_hookedForms.Contains(f))
                {
                    _hookedForms.Add(f);
                    f.Shown += OnFormShown;
                    f.FormClosed += OnFormClosed;
                }
                AttachNonInputEvents(f);
            }
        }
        catch (Exception ex) { LogError("OnIdle", ex); }
    }

    private void AttachNonInputEvents(Control parent)
    {
        foreach (Control c in parent.Controls)
        {
            bool skipRecurse = false;

            if (!_hookedControls.Contains(c))
            {
                _hookedControls.Add(c);
                c.Disposed += OnControlDisposed;

                string typeName = c.GetType().Name;

                if (c is DataGridView dgv)
                {
                    dgv.CellClick += OnDgvCellClick;
                }
                else if (IsInputControlByTypeName(typeName))
                {
                    // ★ 输入框完全跳过，由 WM_KILLFOCUS 处理
                    skipRecurse = true;
                }
                else if (c is ComboBox cmb)
                {
                    cmb.SelectionChangeCommitted += OnComboSelected;
                }
                else if (typeName.Contains("ComboBox"))
                {
                    skipRecurse = true;
                    TryAttachEvent(c, "SelectedIndexChanged",
                        new EventHandler(OnComboSelectedReflect));
                }
                else if (c is CheckBox cb)
                {
                    cb.CheckedChanged += OnCheckedChanged;
                }
                else if (typeName.Contains("CheckBox"))
                {
                    skipRecurse = true;
                    if (!TryAttachSunnyBoolEvent(c, "ValueChanged"))
                        TryAttachEvent(c, "CheckedChanged",
                            new EventHandler(OnCheckedChangedReflect));
                }
                else if (c is RadioButton rb)
                {
                    rb.CheckedChanged += OnCheckedChanged;
                }
                else if (typeName.Contains("RadioButton"))
                {
                    skipRecurse = true;
                    if (!TryAttachSunnyBoolEvent(c, "ValueChanged"))
                        TryAttachEvent(c, "CheckedChanged",
                            new EventHandler(OnCheckedChangedReflect));
                }
                else if (c is TabControl tc)
                {
                    tc.SelectedIndexChanged += OnTabChanged;
                }
                else if (typeName.Contains("TabControl"))
                {
                    TryAttachEvent(c, "SelectedIndexChanged",
                        new EventHandler(OnTabChangedReflect));
                }
                else if (c is DateTimePicker dtp)
                {
                    dtp.CloseUp += OnDateCloseUp;
                }
                else if (c is TrackBar)
                {
                    c.MouseUp += OnTrackMouseUp;
                }
            }

            if (!skipRecurse)
                AttachNonInputEvents(c);
        }
    }

    private bool TryAttachEvent(Control c, string eventName, Delegate handler)
    {
        try
        {
            var evt = c.GetType().GetEvent(eventName,
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            if (evt == null)
            {
                evt = c.GetType().GetEvent(eventName,
                    BindingFlags.Public | BindingFlags.Instance);
            }
            if (evt == null) return false;

            if (evt.EventHandlerType.IsAssignableFrom(handler.GetType())
             || evt.EventHandlerType == handler.GetType())
            {
                evt.AddEventHandler(c, handler);
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            LogError("TryAttachEvent:" + eventName, ex);
            return false;
        }
    }

    private bool TryAttachSunnyBoolEvent(Control c, string eventName)
    {
        try
        {
            var evt = c.GetType().GetEvent(eventName,
                BindingFlags.Public | BindingFlags.Instance);
            if (evt == null) return false;

            MethodInfo mi = typeof(GlobalClickLogger).GetMethod(
                "OnSunnyBoolValueChanged",
                BindingFlags.Public | BindingFlags.Instance);

            Delegate d = Delegate.CreateDelegate(evt.EventHandlerType, this, mi, false);
            if (d == null) return false;

            evt.AddEventHandler(c, d);
            return true;
        }
        catch (Exception ex)
        {
            LogError("TryAttachSunnyBoolEvent:" + eventName, ex);
            return false;
        }
    }

    public void OnSunnyBoolValueChanged(object sender, bool value)
    {
        try { WriteLog((Control)sender, "勾选"); }
        catch (Exception ex) { LogError("OnSunnyBoolValueChanged", ex); }
    }

    private void OnControlDisposed(object sender, EventArgs e)
    {
        _hookedControls.Remove((Control)sender);
        if (_lastFocused == sender) { _lastFocused = null; _lastFocusedInitialValue = null; }
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
    // 非输入框控件事件回调
    // ══════════════════════════════════════════════════════════════════════
    private void OnComboSelected(object sender, EventArgs e)
    {
        try { WriteLog((Control)sender, "下拉选择"); }
        catch (Exception ex) { LogError("OnComboSelected", ex); }
    }

    private void OnComboSelectedReflect(object sender, EventArgs e)
    {
        try { WriteLog((Control)sender, "下拉选择"); }
        catch (Exception ex) { LogError("OnComboSelectedReflect", ex); }
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
        try { WriteLog((Control)sender, "切换Tab"); }
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
            Form form = dgv.FindForm();
            string colName = dgv.Columns[e.ColumnIndex].HeaderText;
            WriteLogCore("DataGridView", dgv.Name,
                string.Format("[行{0} {1}]", e.RowIndex, colName),
                cell.Value != null ? cell.Value.ToString() : "(空)",
                form != null ? form.Name : "-",
                form != null ? form.Text : "-",
                "DGV单元格");
        }
        catch (Exception ex) { LogError("OnDgvCellClick", ex); }
    }

    // ══════════════════════════════════════════════════════════════════════
    // 控件类型判断
    // ══════════════════════════════════════════════════════════════════════
    private bool IsLayoutContainer(Control c)
    {
        return c is Form
            || c is Panel
            || c is GroupBox
            || c is SplitContainer
            || c is SplitterPanel
            || c is TabControl
            || c is TabPage
            || c is DataGridView;
    }

    private bool IsInputControl(Control c)
    {
        if (c is TextBox || c is NumericUpDown) return true;
        return IsInputControlByTypeName(c.GetType().Name);
    }

    private bool IsInputControlByTypeName(string typeName)
    {
        return typeName.Contains("DoubleUpDown")
            || typeName.Contains("IntegerUpDown")
            || typeName == "UITextBox"
            || typeName == "UIEdit"
            || typeName == "UIRichTextBox"
            || (typeName.Contains("TextBox") && !typeName.Contains("Combo"))
            || typeName == "NumericUpDown";
    }

    private bool IsNumericLike(Control c)
    {
        if (c is NumericUpDown) return true;
        string n = c.GetType().Name;
        return n.Contains("DoubleUpDown") || n.Contains("IntegerUpDown");
    }

    /// <summary>
    /// 失焦/点击命中可能落在 SunnyUI 复合控件内部的 UIEdit，
    /// 沿父链向上找到外层的真正业务控件（UIDoubleUpDown / UITextBox 等）
    /// </summary>
    private Control FindOuterValueControl(Control c)
    {
        Control cur = c;
        for (int i = 0; i < 8 && cur != null; i++)
        {
            string n = cur.GetType().Name;
            if (n.Contains("DoubleUpDown") || n.Contains("IntegerUpDown")
             || n == "UITextBox" || n.Contains("UIComboBox")
             || n == "UIRichTextBox")
                return cur;
            if (n == "UIEdit" || (cur is TextBox && string.IsNullOrEmpty(cur.Name)))
                cur = cur.Parent;
            else
                break;
        }
        return null;
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
    // 读取控件值（反射兜底）
    // ══════════════════════════════════════════════════════════════════════
    private string SafeGetValue(Control c)
    {
        try { return GetValue(c); }
        catch { return c.Text ?? "-"; }
    }

    private string GetValue(Control c)
    {
        if (c is CheckBox cb) return cb.Checked.ToString();
        if (c is RadioButton rb) return rb.Checked.ToString();
        if (c is ComboBox cmb) return cmb.Text;
        if (c is TextBox tb) return tb.Text;
        if (c is NumericUpDown n) return n.Value.ToString();
        if (c is TrackBar t) return t.Value.ToString();
        if (c is DateTimePicker d) return d.Value.ToString("yyyy-MM-dd HH:mm:ss");

        var type = c.GetType();
        foreach (string prop in new[] { "Value", "Switch", "Checked",
                                        "SelectedValue", "SelectedItem" })
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
    // 写日志
    // ══════════════════════════════════════════════════════════════════════
    private void WriteLog(Control c, string trigger)
    {
        Form form = c.FindForm();
        WriteLogCore(
            c.GetType().Name,
            c.Name,
            c.Text,
            SafeGetValue(c),
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