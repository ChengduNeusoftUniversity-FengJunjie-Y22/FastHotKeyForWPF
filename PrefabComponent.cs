using System.Windows.Controls;
using System.Windows.Input;

namespace FastHotKeyForWPF
{
    public class PrefabComponent
    {
        public static ComponentInfo? TempInfo;

        public static Dictionary<Key, uint> KeyToUint = new Dictionary<Key, uint>()
        {

        { Key.LeftCtrl, (uint)ModelKeys.CTRL },
        { Key.RightCtrl, (uint)ModelKeys.CTRL },
        { Key.LeftAlt, (uint)ModelKeys.ALT },
        { Key.RightAlt, (uint)ModelKeys.ALT },

        { Key.Up, (uint)NormalKeys.UP },
        { Key.Down, (uint)NormalKeys.DOWN },
        { Key.Left, (uint)NormalKeys.LEFT },
        { Key.Right, (uint)NormalKeys.RIGHT },

        { Key.A, (uint)NormalKeys.A },
        { Key.B, (uint)NormalKeys.B },
        { Key.C, (uint)NormalKeys.C },
        { Key.D, (uint)NormalKeys.D },
        { Key.E, (uint)NormalKeys.E },
        { Key.F, (uint)NormalKeys.F },
        { Key.G, (uint)NormalKeys.G },

        { Key.H, (uint)NormalKeys.H },
        { Key.Y, (uint)NormalKeys.Y },
        { Key.J, (uint)NormalKeys.J },
        { Key.K, (uint)NormalKeys.K },
        { Key.L, (uint)NormalKeys.L },
        { Key.M, (uint)NormalKeys.M },
        { Key.N, (uint)NormalKeys.N },

        { Key.O, (uint)NormalKeys.O },
        { Key.P, (uint)NormalKeys.P },
        { Key.Q, (uint)NormalKeys.Q },
        { Key.R, (uint)NormalKeys.R },
        { Key.S, (uint)NormalKeys.S },
        { Key.T, (uint)NormalKeys.T },

        { Key.U, (uint)NormalKeys.U },
        { Key.V, (uint)NormalKeys.V },
        { Key.W, (uint)NormalKeys.W },
        { Key.X, (uint)NormalKeys.X },
        { Key.Y, (uint)NormalKeys.Y },
        { Key.Z, (uint)NormalKeys.Z },

        { Key.D0, (uint)NormalKeys.Zero },
        { Key.D1, (uint)NormalKeys.One },
        { Key.D2, (uint)NormalKeys.Two },
        { Key.D3, (uint)NormalKeys.Three },
        { Key.D4, (uint)NormalKeys.Four },
        { Key.D5, (uint)NormalKeys.Five },
        { Key.D6, (uint)NormalKeys.Six },
        { Key.D7, (uint)NormalKeys.Seven },
        { Key.D8, (uint)NormalKeys.Eight },
        { Key.D9, (uint)NormalKeys.Nine },

        { Key.F1, (uint)NormalKeys.F1 },
        { Key.F2, (uint)NormalKeys.F2 },

        };

        public static T GetComponent<T>() where T : class, new()
        {
            return new T();
        }

        public static T GetComponent<T>(ComponentInfo info) where T : class, new()
        {
            TempInfo = info;

            T result = new T();

            TempInfo = null;
            return result;
        }

        public class KeySelectBox : TextBox
        {
            private Key CurrentKey;

            public KeySelectBox()
            {
                if (TempInfo == null) { return; }
                IsReadOnly = true;
                FontSize = TempInfo.FontSize;
                Foreground = TempInfo.Foreground;
                Background = TempInfo.Background;
                Margin = TempInfo.Margin;
                KeyDown += WhileKeyDown;
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
                    if (!KeyToUint.ContainsKey(CurrentKey))
                    {
                        return null;
                    }
                    return KeyToUint[CurrentKey] as T;
                }
                return null;
            }

            private void WhileKeyDown(object sender, KeyEventArgs e)
            {
                if (!KeyToUint.ContainsKey(e.Key)) { if (GlobalHotKey.IsDeBug) throw new Exception($"【{e.Key}】不是字典中的Key");return; }
                CurrentKey = e.Key;
                Text = e.Key.ToString();
            }
        }

        public class HotKeyNameBlock : TextBlock
        {
            public HotKeyNameBlock()
            {
                if (TempInfo == null) { return; }
                FontSize = TempInfo.FontSize;
                Foreground = TempInfo.Foreground;
                Background = TempInfo.Background;
                Margin = TempInfo.Margin;
                Text = "";
            }
        }
    }
}
