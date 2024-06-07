using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace FastHotKeyForWPF
{
    /// <summary>
    /// 抽象类型，继承自TextBox并实现Component接口
    /// <para>热键设置中，接收用户按下键的组件是基于这个做下去的</para>
    /// </summary>
    public abstract class KeyBox : TextBox, Component
    {
        internal static bool IsKeySelectBoxProtected = false;// KeySelectBox组件是否处于保护中（公共）
        internal static List<KeySelectBox> keySelectBoxes = new List<KeySelectBox>();

        internal static bool IsKeysSelectBoxProtected = false;// KeysSelectBox组件是否处于保护中（公共）
        internal static List<KeysSelectBox> keysSelectBoxes = new List<KeysSelectBox>();

        /// <summary>
        /// Key => NormalKeys 字典
        /// </summary>
        public static readonly Dictionary<Key, NormalKeys> KeyToNormalKeys = new Dictionary<Key, NormalKeys>()
        {
        { Key.Up, NormalKeys.UP },
        { Key.Down, NormalKeys.DOWN },
        { Key.Left, NormalKeys.LEFT },
        { Key.Right, NormalKeys.RIGHT },

        {Key.Space, NormalKeys.SPACE },

        { Key.A, NormalKeys.A },
        { Key.B, NormalKeys.B },
        { Key.C, NormalKeys.C },
        { Key.D, NormalKeys.D },
        { Key.E, NormalKeys.E },
        { Key.F, NormalKeys.F },
        { Key.G, NormalKeys.G },

        { Key.H, NormalKeys.H },
        { Key.I, NormalKeys.I },
        { Key.J, NormalKeys.J },
        { Key.K, NormalKeys.K },
        { Key.L, NormalKeys.L },
        { Key.M, NormalKeys.M },
        { Key.N, NormalKeys.N },

        { Key.O, NormalKeys.O },
        { Key.P, NormalKeys.P },
        { Key.Q, NormalKeys.Q },
        { Key.R, NormalKeys.R },
        { Key.S, NormalKeys.S },
        { Key.T, NormalKeys.T },

        { Key.U, NormalKeys.U },
        { Key.V, NormalKeys.V },
        { Key.W, NormalKeys.W },
        { Key.X, NormalKeys.X },
        { Key.Y, NormalKeys.Y },
        { Key.Z, NormalKeys.Z },

        { Key.D0, NormalKeys.Zero },
        { Key.D1, NormalKeys.One },
        { Key.D2, NormalKeys.Two },
        { Key.D3, NormalKeys.Three },
        { Key.D4, NormalKeys.Four },
        { Key.D5, NormalKeys.Five },
        { Key.D6, NormalKeys.Six },
        { Key.D7, NormalKeys.Seven },
        { Key.D8, NormalKeys.Eight },
        { Key.D9, NormalKeys.Nine },

        { Key.F1, NormalKeys.F1 },
        { Key.F2, NormalKeys.F2 },
        { Key.F3, NormalKeys.F3 },
        { Key.F4, NormalKeys.F4 },
        { Key.F5, NormalKeys.F5 },
        { Key.F6, NormalKeys.F6 },
        { Key.F7, NormalKeys.F7 },

        { Key.F9, NormalKeys.F9 },
        { Key.F10,NormalKeys.F10 },
        { Key.F11,NormalKeys.F11 },
        { Key.F12,NormalKeys.F12 },

        };

        /// <summary>
        /// Key => ModelKeys 字典
        /// </summary>
        public static readonly Dictionary<Key, ModelKeys> KeyToModelKeys = new Dictionary<Key, ModelKeys>()
        {
        { Key.LeftCtrl, ModelKeys.CTRL },
        { Key.RightCtrl, ModelKeys.CTRL },
        { Key.LeftAlt, ModelKeys.ALT },
        { Key.RightAlt, ModelKeys.ALT },
        };

        internal static void RemoveSameKeySelect(ModelKeys modelKeys, NormalKeys normalKeys, KeySelectBox protect)
        {
            foreach (var key in keySelectBoxes)
            {
                if (key.IsConnected && key != protect && key != protect.LinkBox)
                {
                    var temp = BindingRef.GetKeysFromConnection(key);
                    if (temp.Item1 == modelKeys && temp.Item2 == normalKeys)
                    {
                        key.CurrentKey = Key.None;
                        key.LinkBox.CurrentKey = Key.None;
                        key.Text = string.Empty;
                        key.LinkBox.Text = string.Empty;
                        return;
                    }
                }
            }
            foreach (var key in keysSelectBoxes)
            {
                if (key.IsConnected && KeyToModelKeys.ContainsKey(key.CurrentKeyA) && KeyToNormalKeys.ContainsKey(key.CurrentKeyB))
                {
                    if (KeyToModelKeys[key.CurrentKeyA] == modelKeys && KeyToNormalKeys[key.CurrentKeyB] == normalKeys)
                    {
                        key.CurrentKeyA = Key.None;
                        key.CurrentKeyB = Key.None;
                        key.Text = string.Empty;
                        return;
                    }
                }
            }
        }

        internal static void RemoveSameKeysSelect(ModelKeys modelKeys, NormalKeys normalKeys, KeysSelectBox protect)
        {
            foreach (var key in keysSelectBoxes)
            {
                if (key.IsConnected && key != protect && KeyToModelKeys.ContainsKey(key.CurrentKeyA) && KeyToNormalKeys.ContainsKey(key.CurrentKeyB))
                {
                    if (KeyToModelKeys[key.CurrentKeyA] == modelKeys && KeyToNormalKeys[key.CurrentKeyB] == normalKeys)
                    {
                        key.CurrentKeyA = new Key();
                        key.CurrentKeyB = new Key();
                        key.Text = string.Empty;
                        return;
                    }
                }
            }
            foreach (var key in keySelectBoxes)
            {
                if (key.IsConnected)
                {
                    var temp = BindingRef.GetKeysFromConnection(key);
                    if (temp.Item1 == modelKeys && temp.Item2 == normalKeys)
                    {
                        key.CurrentKey = new Key();
                        key.LinkBox.CurrentKey = new Key();
                        key.Text = string.Empty;
                        key.LinkBox.Text = string.Empty;
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// 是否被保护（独立）
        /// </summary>
        internal bool Protected = false;
        internal bool IsProtectedFromFunc = false;

        /// <summary>
        /// 是否启用默认变色效果（独立）
        /// </summary>
        public bool IsDefaultColorChange = false;

        /// <summary>
        /// 该组件负责管理的事件之一
        /// </summary>
        internal KeyInvoke_Void? Event_void;
        /// <summary>
        /// 该组件负责管理的事件之一
        /// </summary>
        internal KeyInvoke_Return? Event_return;

        /// <summary>
        /// 获取焦点时的行为
        /// </summary>
        internal TextBoxChange? Focused;
        /// <summary>
        /// 失去焦点时的行为
        /// </summary>
        internal TextBoxChange? UnFocused;

        internal bool IsSuccessRegister = false;

        internal TextBoxChange? SuccessRegister;

        internal TextBoxChange? LoseRegister;

        public string DefaultErrorText = "Error";

        /// <summary>
        /// 应用父容器的尺寸，并自动调节字体大小
        /// </summary>
        /// <typeparam name="T">父容器类型</typeparam>
        public void UseFatherSize<T>() where T : UIElement
        {
            T? father = Parent as T;
            if (father == null) { return; }

            PropertyInfo? heightProperty = typeof(T).GetProperty("Height");
            if (heightProperty == null) { return; }

            object? height = heightProperty.GetValue(father);
            if (height == null) { return; }

            FontSize = (double)height * 0.8;
        }
        /// <summary>
        /// 应用父容器的尺寸，并自动调节字体大小
        /// </summary>
        /// <typeparam name="T">父容器类型</typeparam>
        /// <param name="rate">字体适应比率,默认为0.8</param>
        public void UseFatherSize<T>(double rate) where T : UIElement
        {
            T? father = Parent as T;
            if (father == null) { return; }

            PropertyInfo? heightProperty = typeof(T).GetProperty("Height");
            if (heightProperty == null) { return; }

            object? height = heightProperty.GetValue(father);
            if (height == null) { return; }

            if (rate > 0) { FontSize = (double)height * rate; }
            else { FontSize = (double)height * 0.8; }
        }

        /// <summary>
        /// 应用资源样式中的全部属性
        /// </summary>
        /// <param name="styleName">资源样式的Key</param>
        public void UseStyleProperty(string styleName)
        {
            Style? style = (Style)TryFindResource(styleName);
            if (style == null) return;

            if (style.TargetType == typeof(TextBox))
            {
                foreach (SetterBase setterBase in style.Setters)
                {
                    if (setterBase is Setter setter)
                    {
                        SetValue(setter.Property, setter.Value);
                    }
                }
            }
        }

        /// <summary>
        /// 应用资源样式中，指定名称的属性
        /// </summary>
        /// <param name="styleName">资源样式的Key</param>
        /// <param name="targetProperties">属性名</param>
        public void UseStyleProperty(string styleName, string[] targetProperties)
        {
            Style? style = (Style)TryFindResource(styleName);
            if (style == null) return;

            if (style.TargetType == typeof(TextBox))
            {
                foreach (string target in targetProperties)
                {
                    Setter? targetSetter = style.Setters.FirstOrDefault(s => ((Setter)s).Property.Name == target) as Setter;
                    if (targetSetter != null)
                    {
                        SetValue(targetSetter.Property, targetSetter.Value);
                    }
                }
            }
        }

        /// <summary>
        /// 使用你自定义的焦点变色函数，这些函数必须带有一个TextBox参数定义
        /// </summary>
        /// <param name="enter">获取焦点时</param>
        /// <param name="leave">失去焦点时</param>
        public void UseFocusTrigger(TextBoxChange enter, TextBoxChange leave)
        {
            Focused = null;
            UnFocused = null;
            Focused = enter;
            UnFocused = leave;
            IsDefaultColorChange = false;
        }

        internal void WhileMouseEnter(object sender, MouseEventArgs e)
        {
            Focus();
            if (IsDefaultColorChange)
            {
                Background = Brushes.Black;
                Foreground = Brushes.Cyan;
            }
            else
            {
                if (Focused != null) Focused.Invoke(this);
            }
            if (sender is KeySelectBox && !IsProtectedFromFunc)
            {
                ((KeySelectBox)sender).Protected = false;
            }
            else if (sender is KeysSelectBox && !IsProtectedFromFunc)
            {
                ((KeysSelectBox)sender).Protected = false;
            }
            Focused?.Invoke(this);
        }

        internal void WhileMouseLeave(object sender, MouseEventArgs e)
        {
            Keyboard.ClearFocus();
            if (IsDefaultColorChange)
            {
                Background = Brushes.Wheat;
                Foreground = Brushes.Black;
            }
            else
            {
                if (UnFocused != null) UnFocused.Invoke(this);
            }
            if (sender is KeySelectBox)
            {
                ((KeySelectBox)sender).Protected = true;
            }
            else if (sender is KeysSelectBox)
            {
                ((KeysSelectBox)sender).Protected = true;
            }           
        }

        /// <summary>
        /// 将此对象设为保护
        /// </summary>
        public void Protect()
        {
            Protected = true;
            IsProtectedFromFunc = true;
        }

        /// <summary>
        /// 解除此对象的保护
        /// </summary>
        public void UnProtect()
        {
            Protected = false;
            IsProtectedFromFunc = false;
        }

        /// <summary>
        /// 圆角盒子场景下，样式的修改
        /// </summary>
        internal void UseRoundStyle<T>() where T : UIElement
        {
            Background = Brushes.Transparent;
            BorderBrush = Brushes.Transparent;
            BorderThickness = new Thickness(0);
        }
    }
}
