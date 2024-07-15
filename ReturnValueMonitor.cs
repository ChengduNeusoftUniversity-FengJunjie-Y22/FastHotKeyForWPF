using System.Windows;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FastHotKeyForWPF
{
    /// <summary>
    /// 返回值监听模块
    /// </summary>
    public class ReturnValueMonitor
    {
        private static ReturnValueMonitor? Instance;

        private ReturnValueMonitor() { }

        private KeyInvoke_Void? FunctionVoid;

        private static object? _value = null;
        /// <summary>
        /// 最新监测到的返回值,它来自于热键处理函数返回的object
        /// </summary>
        public static object? Value
        {
            get { return _value; }
        }

        /// <summary>
        /// 启用监听
        /// </summary>
        public static void Awake()
        {
            if (Instance == null)
            {
                Instance = new ReturnValueMonitor();
            }
            else
            {
                MessageBox.Show("请不要重复激活操作！");
            }
        }

        /// <summary>
        /// 关闭监听
        /// </summary>
        public static void Destroy()
        {
            Instance = null;
        }

        internal static void Update(object? data)
        {
            if (Instance == null) { return; }

            _value = data;
            Instance.FunctionVoid?.Invoke();
        }

        /// <summary>
        /// 指定一个无参无返回值的处理函数，用于处理最新监测到的object对象
        /// </summary>
        public static void BindingAutoEvent(KeyInvoke_Void function)
        {
            if (Instance != null)
            {
                Instance.FunctionVoid = null;
                Instance.FunctionVoid = function;
            }
        }

        /// <summary>
        /// 移除object值的处理函数
        /// </summary>
        public static void RemoveAutoEvent()
        {
            if (Instance != null)
            {
                Instance.FunctionVoid = null;
            }
        }
    }
}
