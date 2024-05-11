using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Diagnostics;

namespace FastHotKeyForWPF
{
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

        public static void DisConnect(KeySelectBox target)
        {
            if (target.LinkBox == null) { if (GlobalHotKey.IsDeBug) MessageBox.Show("⚠未建立连接的对象无法删除连接！"); return; }
            target.LinkBox.Event_void = null;
            target.LinkBox.Event_return = null;
            target.Event_void = null;
            target.Event_return = null;
            target.LinkBox.LinkBox = null;
            target.LinkBox = null;
        }

        public static (ModelKeys?, NormalKeys?) GetKeysFromConnection(KeySelectBox target)
        {
            if (!target.IsConnected) { if (GlobalHotKey.IsDeBug) MessageBox.Show("⚠此目标尚未建立连接，无法获取键盘组合！"); return (null, null); }

            int normal = 0;
            int model = 0;
            if (KeySelectBox.KeyToNormalKeys.ContainsKey(target.CurrentKey)) { normal = 1; }
            if (KeySelectBox.KeyToModelKeys.ContainsKey(target.CurrentKey)) { model = 1; }
            if (KeySelectBox.KeyToNormalKeys.ContainsKey(target.LinkBox.CurrentKey)) { normal = 2; }
            if (KeySelectBox.KeyToModelKeys.ContainsKey(target.LinkBox.CurrentKey)) { model = 2; }

            if (normal == 1 && model == 2)
            {
                return (KeySelectBox.KeyToModelKeys[target.LinkBox.CurrentKey], KeySelectBox.KeyToNormalKeys[target.CurrentKey]);
            }
            else if (normal == 2 && model == 1)
            {
                return (KeySelectBox.KeyToModelKeys[target.CurrentKey], KeySelectBox.KeyToNormalKeys[target.LinkBox.CurrentKey]);
            }
            return (null, null);
        }

        private event KeyInvoke_Void? FunctionVoid;
        public void Invoke()
        {
            if (FunctionVoid != null) { FunctionVoid.Invoke(); }
        }
    }
}
