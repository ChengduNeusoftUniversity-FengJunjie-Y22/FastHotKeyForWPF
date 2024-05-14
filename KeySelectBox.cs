using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using System.Reflection;
using System.Windows.Media;
using System.Runtime.CompilerServices;

public enum KeyTypes
{
    Normal,
    Model,
    None
}

namespace FastHotKeyForWPF
{
    public class KeySelectBox : PrefabTextBox
    {
        public static Dictionary<Key, NormalKeys> KeyToNormalKeys = new Dictionary<Key, NormalKeys>()
        {
        { Key.Up, NormalKeys.UP },
        { Key.Down, NormalKeys.DOWN },
        { Key.Left, NormalKeys.LEFT },
        { Key.Right, NormalKeys.RIGHT },

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
        { Key.F8, NormalKeys.F8 },
        { Key.F9, NormalKeys.F9 },
        { Key.F10,NormalKeys.F10 },
        { Key.F11,NormalKeys.F11 },
        { Key.F12,NormalKeys.F12 },

        };
        public static Dictionary<Key, ModelKeys> KeyToModelKeys = new Dictionary<Key, ModelKeys>()
        {
        { Key.LeftCtrl, ModelKeys.CTRL },
        { Key.RightCtrl, ModelKeys.CTRL },
        { Key.LeftAlt, ModelKeys.ALT },
        { Key.RightAlt, ModelKeys.ALT },
        };

        private Key _currentkey;
        /// <summary>
        /// 当前获取到的用户按键
        /// </summary>
        public Key CurrentKey
        {
            get { return _currentkey; }
            set
            {
                var olddate = BindingRef.GetKeysFromConnection(this);
                if (olddate.Item1 != null && olddate.Item2 != null)
                {
                    GlobalHotKey.DeleteByKeys((ModelKeys)olddate.Item1, (NormalKeys)olddate.Item2);
                }
                _currentkey = value;
                UpdateHotKey();
            }
        }

        /// <summary>
        /// 是否启用默认变色效果
        /// </summary>
        public bool IsDefaultColorChange = true;

        /// <summary>
        /// 是否处于连接状态
        /// </summary>
        public bool IsConnected
        {
            get
            {
                if (LinkBox == null && Event_void == null && Event_return == null) return false;
                if (LinkBox != null && Event_void != null && Event_return == null) return true;
                if (LinkBox != null && Event_void == null && Event_return != null) return true;
                return false;
            }
        }

        public bool Protected = false;

        public KeySelectBox? LinkBox;
        public KeyInvoke_Void? Event_void;
        public KeyInvoke_Return? Event_return;

        public event TextBoxFocusChange? Focused;
        public event TextBoxFocusChange? UnFocused;

        /// <summary>
        /// 当前按键的类型
        /// </summary>
        public KeyTypes KeyType
        {
            get
            {
                if (Enum.IsDefined(typeof(NormalKeys), CurrentKey.ToString()))
                {
                    return KeyTypes.Normal;
                }
                if (Enum.IsDefined(typeof(ModelKeys), CurrentKey.ToString()))
                {
                    return KeyTypes.Model;
                }
                return KeyTypes.None;
            }
        }

        internal KeySelectBox()
        {
            if (PrefabComponent.TempInfo == null) { return; }
            Width = 100;
            Height = 50;
            VerticalContentAlignment = VerticalAlignment.Center;
            HorizontalContentAlignment = HorizontalAlignment.Center;
            IsReadOnly = true;
            FontSize = PrefabComponent.TempInfo.FontSize;
            Foreground = PrefabComponent.TempInfo.Foreground;
            Background = PrefabComponent.TempInfo.Background;
            Margin = PrefabComponent.TempInfo.Margin;
            KeyDown += WhileKeyDown;
            MouseEnter += WhileMouseEnter;
            MouseLeave += WhileMouseLeave;
            keySelectBoxes.Add(this);
        }

        private void WhileKeyDown(object sender, KeyEventArgs e)
        {
            if (IsKeySelectProtected || Protected) { return; }
            Key key = (e.Key == Key.System ? e.SystemKey : e.Key);
            if (!PrefabComponent.KeyToUint.ContainsKey(key)) { if (GlobalHotKey.IsDeBug) MessageBox.Show($"当前版本不支持这个按键【{key}】"); return; }
            CurrentKey = key;
            Text = key.ToString();
            if (GlobalHotKey.IsDeBug) { MessageBox.Show($"已更新为【{key}】"); }
            e.Handled = true;
        }

        private void WhileMouseEnter(object sender, MouseEventArgs e)
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
        }

        private void WhileMouseLeave(object sender, MouseEventArgs e)
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
        }

        /// <summary>
        /// 应用父容器的尺寸，并自动调节字体大小
        /// </summary>
        /// <typeparam name="T">父容器类型</typeparam>
        public void UseFatherSize<T>() where T : UIElement
        {
            T? father = Parent as T;
            if (father == null) { return; }

            PropertyInfo? widthProperty = typeof(T).GetProperty("Width");
            PropertyInfo? heightProperty = typeof(T).GetProperty("Height");
            if (widthProperty == null) { return; }
            if (heightProperty == null) { return; }

            object? width = widthProperty.GetValue(father);
            object? height = heightProperty.GetValue(father);
            if (width == null) { return; }
            if (height == null) { return; }

            Width = (double)width;
            Height = (double)height;
            FontSize = (double)height * 0.8;
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
        public void UseFocusTrigger(TextBoxFocusChange enter, TextBoxFocusChange leave)
        {
            Focused = null;
            UnFocused = null;
            Focused = enter;
            UnFocused = leave;
            IsDefaultColorChange = false;
        }

        /// <summary>
        /// 更新一次热键信息
        /// </summary>
        /// <returns>bool 表示是否成功更新</returns>
        public bool UpdateHotKey()
        {
            bool result = false;
            if (IsConnected)
            {
                var date = BindingRef.GetKeysFromConnection(this);

                if (date.Item1 == null || date.Item2 == null) { if (GlobalHotKey.IsDeBug) MessageBox.Show("⚠不正确的键盘组合，无法注册"); return false; }

                if (Event_void != null)
                {
                    result = GlobalHotKey.Add((ModelKeys)date.Item1, (NormalKeys)date.Item2, Event_void).Item1;
                }
                else if (Event_return != null)
                {
                    result = GlobalHotKey.Add((ModelKeys)date.Item1, (NormalKeys)date.Item2, Event_return).Item1;
                }
            }
            return result;
        }

        public void Protect()
        {
            Protected = true;
        }

        public void UnProtect()
        {
            Protected = false;
        }

        public static void ProtectAll()
        {
            IsKeySelectProtected = true;
        }

        public static void UnProtectAll()
        {
            IsKeySelectProtected = false;
            foreach (KeySelectBox box in keySelectBoxes)
            {
                box.Protected = false;
            }
        }
    }
}
