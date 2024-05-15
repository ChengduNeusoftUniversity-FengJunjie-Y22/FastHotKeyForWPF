using System.Windows;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FastHotKeyForWPF
{
    /// <summary>
    /// 此类型主要负责一些监测任务、绑定任务
    /// </summary>
    public class BindingRef
    {
        private static BindingRef? Instance;

        private BindingRef() { }

        private static object? _value = null;
        /// <summary>
        /// 最新监测到的返回值
        /// </summary>
        public static object? Value
        {
            get { return _value; }
        }

        private KeyInvoke_Void? FunctionVoid;

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

        internal static void Update(object? data)
        {
            _value = data;
            if (Instance != null)
            {
                Instance.Invoke();
            }
        }

        /// <summary>
        /// 绑定自动事件，它将在监测到返回值的时候自动触发
        /// </summary>
        /// <param name="function">你自定义的处理函数</param>
        public static void BindingAutoEvent(KeyInvoke_Void function)
        {
            if (Instance != null)
            {
                Instance.FunctionVoid = null;
                Instance.FunctionVoid = function;
            }
        }
        public static void RemoveAutoEvent()
        {
            if (Instance != null)
            {
                Instance.FunctionVoid = null;
            }
        }

        public static void Connect(KeySelectBox box1, KeySelectBox box2, KeyInvoke_Void work)
        {
            if (box1.IsConnected || box2.IsConnected) { if (GlobalHotKey.IsDeBug) throw new Exception("⚠不允许重复的连接操作！"); return; }
            box1.LinkBox = box2;
            box2.LinkBox = box1;
            box1.Event_return = null;
            box2.Event_return = null;
            box1.Event_void = work;
            box2.Event_void = work;
        }
        public static void Connect(KeySelectBox box1, KeySelectBox box2, KeyInvoke_Return work)
        {
            if (box1.IsConnected || box2.IsConnected) { if (GlobalHotKey.IsDeBug) MessageBox.Show("⚠重复的连接操作！"); return; }
            box1.LinkBox = box2;
            box2.LinkBox = box1;
            box1.Event_return = work;
            box2.Event_return = work;
            box1.Event_void = null;
            box2.Event_void = null;
        }
        //连接两个分散的KeySelectBox

        public static void Connect(KeysSelectBox target, KeyInvoke_Void work)
        {
            target.Event_void = work;
            target.Event_return = null;
            target.IsConnected = true;
        }
        public static void Connect(KeysSelectBox target, KeyInvoke_Return work)
        {
            target.Event_return = work;
            target.Event_void = null;
            target.IsConnected = true;
        }
        //为一个KeysSelectBox指定处理函数

        /// <summary>
        /// 取消两个KeySelectBox之间的联系
        /// </summary>
        /// <param name="target"></param>
        public static void DisConnect(KeySelectBox target)
        {
            if (target.LinkBox == null) { if (GlobalHotKey.IsDeBug) MessageBox.Show("⚠未建立连接的对象无法删除连接！"); return; }
            var result = GetKeysFromConnection(target);
            if (result.Item1 != null && result.Item2 != null)
            {
                GlobalHotKey.DeleteByKeys((ModelKeys)result.Item1, (NormalKeys)result.Item2);
            }
            target.LinkBox.Event_void = null;
            target.LinkBox.Event_return = null;
            target.Event_void = null;
            target.Event_return = null;
            target.CurrentKey = new System.Windows.Input.Key();
            target.LinkBox.CurrentKey = new System.Windows.Input.Key();
            target.LinkBox.LinkBox = null;
            target.LinkBox = null;
        }

        /// <summary>
        /// 取消KeysSelectBox指定的处理函数
        /// </summary>
        /// <param name="target"></param>
        public static void DisConnect(KeysSelectBox target)
        {
            if (KeyBox.KeyToModelKeys.ContainsKey(target.CurrentKeyA) && KeyBox.KeyToNormalKeys.ContainsKey(target.CurrentKeyB))
            {
                GlobalHotKey.DeleteByKeys(KeyBox.KeyToModelKeys[target.CurrentKeyA], KeyBox.KeyToNormalKeys[target.CurrentKeyB]);
            }
            target.Event_void = null;
            target.Event_return = null;
            target.CurrentKeyA = new System.Windows.Input.Key();
            target.CurrentKeyB = new System.Windows.Input.Key();
            target.IsConnected = false;
        }

        /// <summary>
        /// 从连接体的任何一方，获取ModelKeys与NormalKeys
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static (ModelKeys?, NormalKeys?) GetKeysFromConnection(KeySelectBox target)
        {
            if (!target.IsConnected) { if (GlobalHotKey.IsDeBug) MessageBox.Show("⚠此目标尚未建立连接，无法获取键盘组合！"); return (null, null); }

            int normal = 0;
            int model = 0;
            if (KeyBox.KeyToNormalKeys.ContainsKey(target.CurrentKey)) { normal = 1; }
            if (KeyBox.KeyToModelKeys.ContainsKey(target.CurrentKey)) { model = 1; }
            if (KeyBox.KeyToNormalKeys.ContainsKey(target.LinkBox.CurrentKey)) { normal = 2; }
            if (KeyBox.KeyToModelKeys.ContainsKey(target.LinkBox.CurrentKey)) { model = 2; }

            if (normal == 1 && model == 2)
            {
                return (KeyBox.KeyToModelKeys[target.LinkBox.CurrentKey], KeyBox.KeyToNormalKeys[target.CurrentKey]);
            }
            else if (normal == 2 && model == 1)
            {
                return (KeyBox.KeyToModelKeys[target.CurrentKey], KeyBox.KeyToNormalKeys[target.LinkBox.CurrentKey]);
            }
            return (null, null);
        }

        public void Invoke()
        {
            if (FunctionVoid != null) { FunctionVoid.Invoke(); }
        }
    }
}
