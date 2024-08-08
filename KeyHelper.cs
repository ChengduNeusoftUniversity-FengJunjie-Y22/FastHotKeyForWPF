using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

/// <summary>
/// 普通按键
/// </summary>
public enum NormalKeys : uint
{
    F1 = 0x70,
    F2 = 0x71,
    F3 = 0x72,
    F4 = 0x73,
    F5 = 0x74,
    F6 = 0x75,
    F7 = 0x76,

    F9 = 0x78,
    F10 = 0x79,
    F11 = 0x7A,
    F12 = 0x7B,

    LEFT = 0x25,
    UP = 0x26,
    RIGHT = 0x27,
    DOWN = 0x28,

    Zero = 0x30,
    One = 0x31,
    Two = 0x32,
    Three = 0x33,
    Four = 0x34,
    Five = 0x35,
    Six = 0x36,
    Seven = 0x37,
    Eight = 0x38,
    Nine = 0x39,

    SPACE = 0x20,

    A = 0x41,
    B = 0x42,
    C = 0x43,
    D = 0x44,
    E = 0x45,
    F = 0x46,
    G = 0x47,
    H = 0x48,
    I = 0x49,
    J = 0x4A,
    K = 0x4B,
    L = 0x4C,
    M = 0x4D,
    N = 0x4E,
    O = 0x4F,
    P = 0x50,
    Q = 0x51,
    R = 0x52,
    S = 0x53,
    T = 0x54,
    U = 0x55,
    V = 0x56,
    W = 0x57,
    X = 0x58,
    Y = 0x59,
    Z = 0x5A
}

/// <summary>
/// 系统按键
/// </summary>
public enum ModelKeys : uint
{
    ALT = 0x0001,
    CTRL = 0x0002,
    SHIFT = 0x0004
}

/// <summary>
/// 系统Key转为库中Key后的对应类型
/// </summary>
public enum KeyTypes
{
    ModelKey,
    NormalKey,
    None
}

namespace FastHotKeyForWPF
{
    /// <summary>
    /// 处理系统 Key 与 类库Key 之间的关系
    /// </summary>
    public static class KeyHelper
    {
        /// <summary>
        /// Key => Uint
        /// </summary>
        public static readonly Dictionary<Key, uint> KeyToUints = new Dictionary<Key, uint>()
        {

        { Key.LeftCtrl, (uint)ModelKeys.CTRL },
        { Key.LeftAlt, (uint)ModelKeys.ALT },
        { Key.LeftShift, (uint)ModelKeys.SHIFT },

        { Key.Space,(uint)NormalKeys.SPACE},

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

        { Key.F9, (uint)NormalKeys.F9 },
        { Key.F10, (uint)NormalKeys.F10 },
        { Key.F11, (uint)NormalKeys.F11 },
        { Key.F12, (uint)NormalKeys.F12 },

        };

        /// <summary>
        /// Key => NormalKeys
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
        /// Key => ModelKeys
        /// </summary>
        public static readonly Dictionary<Key, ModelKeys> KeyToModelKeys = new Dictionary<Key, ModelKeys>()
        {
        { Key.LeftCtrl, ModelKeys.CTRL },
        { Key.LeftAlt, ModelKeys.ALT },
        { Key.LeftShift, ModelKeys.SHIFT }
        };

        /// <summary>
        /// Uint => Key
        /// </summary>
        public static readonly Dictionary<uint, Key> UintToKeys = KeyToUints.ToDictionary(kv => kv.Value, kv => kv.Key);

        /// <summary>
        /// NormalKey => Key
        /// </summary>
        public static readonly Dictionary<NormalKeys, Key> NormalKeysToKeys = KeyToNormalKeys.ToDictionary(kv => kv.Value, kv => kv.Key);

        /// <summary>
        /// ModelKey => Key
        /// </summary>
        public static readonly Dictionary<ModelKeys, Key> ModelKeysToKeys = KeyToModelKeys.ToDictionary(kv => kv.Value, kv => kv.Key);

        /// <summary>
        /// 将若干ModelKeys合并为uint
        /// </summary>
        public static uint UintSum(params object[] keys)
        {
            uint[] temp = new uint[keys.Length];
            for (int i = 0; i < temp.Length; i++)
            {
                temp[i] = ValueToUint(keys[i]);
            }
            return temp.Length == 0 ? 0x0000 : temp.Aggregate((current, next) => current | next);
        }

        /// <summary>
        /// 解算一个 uint 具体由哪些 ModelKeys 构成
        /// </summary>
        /// <typeparam name="T">目标集合类型</typeparam>
        /// <param name="value">待转换的 uint值</param>
        /// <returns>uint 拆分为多个 ModelKeys 的结果集合</returns>
        public static T UintSplit<T>(uint value) where T : ICollection<ModelKeys>, new()
        {
            T result = new T();

            foreach (ModelKeys item in Enum.GetValues(typeof(ModelKeys)))
            {
                if ((value & (uint)item) == (uint)item)
                {
                    result.Add(item);
                }
            }

            return result;
        }

        /// <summary>
        /// 判断一个object是否为 ModelKeys、NormalKeys、ICollection 中的一种 ，然后转换为对应的 uint 值
        /// </summary>
        /// <param name="value">待转换对象</param>
        /// <returns>uint 转换结果 ，默认 0x0000</returns>
        public static uint ValueToUint(object value)
        {
            return value switch
            {
                uint => (uint)value,
                ModelKeys => (uint)value,
                NormalKeys => (uint)value,
                ICollection<ModelKeys> keys => keys.Count == 0 ? 0x0000 : (uint)keys.Aggregate((current, next) => current | next),

                _ => 0x0000
            };
        }

        /// <summary>
        /// 将 uint值 转换为 enum值
        /// </summary>
        /// <typeparam name="T">目标枚举类型</typeparam>
        /// <param name="value">待转换的 uint 值</param>
        /// <returns>enum</returns>
        public static T UintToEnum<T>(uint value) where T : Enum, new()
        {
            if (typeof(T) == typeof(Key))
            {
                if (!UintToKeys.ContainsKey(value))
                {
                    return new T();
                }

                Key temp = UintToKeys[value];
                return (T)(object)temp;
            }
            else
            {
                if (!Enum.IsDefined(typeof(T), value))
                {
                    return new T();
                }

                return (T)Enum.ToObject(typeof(T), value);
            }
        }

        /// <summary>
        /// 检查键是否合法,以及它的所属类型
        /// </summary>
        /// <returns>如果键存在于 KeyToModelKeys 或 KeyToNormalKeys 中，返回 true 和其所属类型；否则返回 false 和 KeyTypes.None</returns>
        public static (bool, KeyTypes) IsKeyValid(Key target)
        {
            bool result = KeyToModelKeys.ContainsKey(target) ^ KeyToNormalKeys.ContainsKey(target);
            KeyTypes type = result ? (KeyToModelKeys.ContainsKey(target) ? KeyTypes.ModelKey : KeyTypes.NormalKey) : KeyTypes.None;
            return (result, type);
        }

        /// <summary>
        /// 为WPF的 [KeyDown事件] 提供 [Key值处理] 逻辑
        /// </summary>
        /// <param name="item">实现了IAutoHotKeyProperty接口的View层，通常是一个用户控件</param>
        /// <param name="e">KeyDown事件中的 KeyEventArgs e</param>
        public static Key KeyParse(IAutoHotKeyProperty item, KeyEventArgs e)
        {
            Key key = (e.Key == Key.System ? e.SystemKey : e.Key);

            var result = IsKeyValid(key);

            if (!result.Item1) { return key; }

            if (result.Item2 == KeyTypes.ModelKey)
            {
                List<ModelKeys> original = UintSplit<List<ModelKeys>>(item.CurrentKeyA);
                ModelKeys model = KeyToModelKeys[key];

                if (original.Contains(model))
                {
                    original.Remove(model);
                }
                else
                {
                    original.Add(model);
                }

                uint newValue = UintSum(original);
                item.CurrentKeyA = newValue;

                return key;
            }

            if (result.Item2 == KeyTypes.NormalKey)
            {
                item.CurrentKeyB = key;

                return key;
            }

            return key;
        }
    }
}