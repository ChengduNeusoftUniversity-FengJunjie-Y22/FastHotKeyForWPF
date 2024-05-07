using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Diagnostics;

namespace FastHotKeyForWPF
{
    /// <summary>
    /// 【绑定传递】使用说明:
    /// </summary>
    public class BindingRef
    {
        private static BindingRef? Instance;
        private BindingRef() { }
        public static object? Value = null;
        public static void Awake()
        {
            if (Instance == null)
            {
                Instance = new BindingRef();
            }
            else
            {
                MessageBox.Show("请不要重复激活操作！");
            }
        }
        public static void Destroy()
        {
            Instance = null;
        }
        public static void Update(object? data)
        {
            Value = data;
            if (Instance != null)
            {
                Instance.Invoke();
            }
        }
        public static void BindingEvent(KeyInvoke_Void function)
        {
            if (Instance != null)
            {
                Instance.FunctionVoid = null;
                Instance.FunctionVoid += function;
            }
        }

        public event KeyInvoke_Void? FunctionVoid;
        public void Invoke()
        {
            if (FunctionVoid != null) { FunctionVoid.Invoke(); }
        }
    }
}
