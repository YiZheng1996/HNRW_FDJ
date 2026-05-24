using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MainUI.Fault
{
    /// <summary>
    /// 操作上下文：用 using 块在控件事件处理函数最开头标记
    /// "当前是哪个窗体、哪个控件、做什么操作"，
    /// 之后在 OPC 写入层 (OpcOperationLog) 自动读取，组成完整日志。
    ///
    /// 特点：
    ///   1. ThreadStatic：UI 线程独立栈，互不干扰；WinForms 手动控制都在 UI 线程；
    ///   2. 支持嵌套：一键控制内部连写 N 个 Tag，可正确归属到外层操作；
    ///   3. 出栈靠 using 自动调用 Dispose，不会泄露；
    ///   4. 没有任何上下文时，OpcOperationLog 会用 StackTrace 兜底，仍能产日志。
    /// </summary>
    public sealed class OperationContext : IDisposable
    {
        [ThreadStatic]
        private static Stack<OperationContext> _stack;
        [ThreadStatic] private static int _suppressDepth;   // 抑制层数，过滤不需要记录日志的点位

        //是否处于抑制状态
        public static bool IsSuppressed => _suppressDepth > 0;

        // 进入抑制上下文（using 释放后自动减层）
        public static IDisposable SuppressLog()
        {
            _suppressDepth++;
            return new SuppressScope();
        }

        private class SuppressScope : IDisposable
        {
            private bool _disposed = false;
            public void Dispose()
            {
                if (_disposed) return;
                _disposed = true;
                _suppressDepth--;
            }
        }
        private static Stack<OperationContext> Stack
        {
            get
            {
                if (_stack == null) _stack = new Stack<OperationContext>();
                return _stack;
            }
        }

        /// <summary>当前栈顶上下文；为空表示没有标记，OpcOperationLog 将自行兜底。</summary>
        public static OperationContext Current
        {
            get { return Stack.Count > 0 ? Stack.Peek() : null; }
        }

        /// <summary>窗体名（Form.Name）</summary>
        public string FormName { get; private set; }
        /// <summary>窗体标题（Form.Text），便于人读</summary>
        public string FormTitle { get; private set; }
        /// <summary>控件名（Control.Name）</summary>
        public string ControlName { get; private set; }
        /// <summary>控件类型简短名（GetType().Name）</summary>
        public string ControlType { get; private set; }
        /// <summary>操作描述，如"手动调节励磁"、"打开 Y179 阀"、"一键燃油循环"</summary>
        public string Operation { get; private set; }

        private bool _disposed;

        private OperationContext() { }

        /// <summary>
        /// 标准用法：传入承载窗体（一般 this）、触发控件（一般 sender）、操作描述。
        /// 在事件处理函数最开头：
        ///     using (OperationContext.Begin(this, sender, "手动调节励磁"))
        ///     {
        ///         Common.AOgrp["励磁调节"] = (double)ucNudLC.Value;
        ///     }
        /// </summary>
        public static OperationContext Begin(Control host, object trigger, string operation)
        {
            var ctx = new OperationContext();
            ctx.Operation = operation ?? "-";

            try
            {
                // 窗体信息
                Form form = host != null ? host.FindForm() : null;
                if (form != null)
                {
                    ctx.FormName = form.Name;
                    ctx.FormTitle = form.Text;
                }
                else if (host != null)
                {
                    // 用户控件直接作为 host 也可
                    ctx.FormName = host.Name;
                    ctx.FormTitle = host.Text;
                }
                else
                {
                    ctx.FormName = "-";
                    ctx.FormTitle = "-";
                }

                // 控件信息
                Control ctrl = trigger as Control;
                if (ctrl != null)
                {
                    ctx.ControlName = string.IsNullOrEmpty(ctrl.Name) ? "-" : ctrl.Name;
                    ctx.ControlType = ctrl.GetType().Name;
                }
                else
                {
                    ctx.ControlName = "-";
                    ctx.ControlType = "-";
                }
            }
            catch
            {
                // 上下文构建失败也不能影响业务
                if (string.IsNullOrEmpty(ctx.FormName)) ctx.FormName = "-";
                if (string.IsNullOrEmpty(ctx.FormTitle)) ctx.FormTitle = "-";
                if (string.IsNullOrEmpty(ctx.ControlName)) ctx.ControlName = "-";
                if (string.IsNullOrEmpty(ctx.ControlType)) ctx.ControlType = "-";
            }

            Stack.Push(ctx);
            return ctx;
        }

        /// <summary>
        /// 简化用法：只指定操作描述，窗体/控件留空。
        /// 适合后台 Task / Timer 内手动写 OPC 的场景。
        /// </summary>
        public static OperationContext Begin(string operation)
        {
            return Begin(null, null, operation);
        }

        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true;

            try
            {
                // 防御：栈顶必须是自己，否则不动栈，避免错乱
                if (Stack.Count > 0 && ReferenceEquals(Stack.Peek(), this))
                {
                    Stack.Pop();
                }
            }
            catch { /* 绝不抛出 */ }
        }
    }
}