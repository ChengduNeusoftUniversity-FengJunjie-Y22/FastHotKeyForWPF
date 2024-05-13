using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using System.Reflection;
using System.Windows.Media;

public enum KeyTypes
{
    Normal,
    Model,
    None
}

namespace FastHotKeyForWPF
{
    public class KeySelectBox : TextBox, Component
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
        private bool NewStyle = false;
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
        public KeySelectBox? LinkBox;
        public KeyInvoke_Void? Event_void;
        public KeyInvoke_Return? Event_return;

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

        public KeySelectBox()
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
        }

        public T? GetKey<T>() where T : class
        {
            if (typeof(T) == typeof(string))
            {
                return CurrentKey.ToString() as T;
            }
            if (typeof(T) == typeof(int))
            {
                return (int)CurrentKey as T;
            }
            if (typeof(T) == typeof(uint))
            {
                if (!PrefabComponent.KeyToUint.ContainsKey(CurrentKey))
                {
                    return null;
                }
                return PrefabComponent.KeyToUint[CurrentKey] as T;
            }
            return null;
        }

        private void WhileKeyDown(object sender, KeyEventArgs e)
        {
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
            if (!NewStyle)
            {
                Background = Brushes.Black;
                Foreground = Brushes.Cyan;
            }
        }

        private void WhileMouseLeave(object sender, MouseEventArgs e)
        {
            Keyboard.ClearFocus();
            if (!NewStyle)
            {
                Background = Brushes.Wheat;
                Foreground = Brushes.Black;
            }
        }

        public void UseFatherSize<T>() where T : UIElement
        {
            T? father = Parent as T;
            if (father == null) { return; }

            PropertyInfo? widthProperty = typeof(T).GetProperty("Width");
            if (widthProperty == null) { return; }
            object? width = widthProperty.GetValue(father);
            if (width == null) { return; }
            Width = (double)width;

            PropertyInfo? heightProperty = typeof(T).GetProperty("Height");
            if (heightProperty == null) { return; }
            object? height = heightProperty.GetValue(father);
            if (height == null) { return; }
            Height = (double)height;
        }

        public void UseStyle(string stylename)
        {
            Style? style = null;
            try
            {
                style = (Style)FindResource(stylename);
            }
            catch { }
            if (style == null) return;
            Style = style;
            NewStyle = true;
        }

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
    }
}
