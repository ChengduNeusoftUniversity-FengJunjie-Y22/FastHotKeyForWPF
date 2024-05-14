using System.Reflection;
using System.Windows.Input;

namespace FastHotKeyForWPF
{
    /// <summary>
    /// 组件管控☆
    /// 通过GetComponent获取组件的实例对象
    /// 通过Protect与UnProtect管理组件是否处于保护（锁定）状态
    /// </summary>
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
        { Key.I, (uint)NormalKeys.I },
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
        { Key.F3, (uint)NormalKeys.F3 },
        { Key.F4, (uint)NormalKeys.F4 },
        { Key.F5, (uint)NormalKeys.F5 },
        { Key.F6, (uint)NormalKeys.F6 },
        { Key.F7, (uint)NormalKeys.F7 },
        { Key.F8, (uint)NormalKeys.F8 },
        { Key.F9, (uint)NormalKeys.F9 },
        { Key.F10, (uint)NormalKeys.F10 },
        { Key.F11, (uint)NormalKeys.F11 },
        { Key.F12, (uint)NormalKeys.F12 },

        };

        /// <summary>
        /// 得到默认的组件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>new T（）</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static T GetComponent<T>() where T : Component
        {
            TempInfo = new ComponentInfo();

            ConstructorInfo? constructor = typeof(T).GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null, new Type[0], null);

            if (constructor == null) { throw new InvalidOperationException($"您尝试获取的{typeof(T).Name}组件不存在构造函数！"); }

            T result = (T)constructor.Invoke(null);

            TempInfo = null;
            return result;
        }

        /// <summary>
        /// 得到指定字体大小、字体颜色、背景色、Margin的组件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="info">组件的基本信息</param>
        /// <returns>new T（）</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static T GetComponent<T>(ComponentInfo info) where T : Component
        {
            TempInfo = info;

            ConstructorInfo? constructor = typeof(T).GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null, new Type[0], null);

            if (constructor == null) { throw new InvalidOperationException($"您尝试获取的{typeof(T).Name}组件不存在构造函数！"); }

            T result = (T)constructor.Invoke(null);

            TempInfo = null;
            return result;
        }

        /// <summary>
        /// 设为保护状态
        /// </summary>
        /// <typeparam name="T">组件类型</typeparam>
        public static void ProtectSelectBox<T>() where T : KeyBox
        {
            if (typeof(T).Name == "KeySelectBox")
            {
                KeyBox.IsKeySelectBoxProtected = true;
                return;
            }
            if (typeof(T).Name == "KeysSelectBox")
            {
                KeyBox.IsKeysSelectBoxProtected = true;
                return;
            }
        }

        /// <summary>
        /// 解除保护状态
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void UnProtectSelectBox<T>() where T : KeyBox
        {
            if (typeof(T).Name == "KeySelectBox")
            {
                KeyBox.IsKeySelectBoxProtected = false;
                foreach (KeySelectBox keySelectBox in KeyBox.keySelectBoxes)
                {
                    keySelectBox.Protected = false;
                }
                return;
            }
            if (typeof(T).Name == "KeysSelectBox")
            {
                KeyBox.IsKeysSelectBoxProtected = false;
                foreach (KeysSelectBox keysSelectBox in KeyBox.keysSelectBoxes)
                {
                    keysSelectBox.Protected = false;
                }
                return;
            }
        }
    }
}
