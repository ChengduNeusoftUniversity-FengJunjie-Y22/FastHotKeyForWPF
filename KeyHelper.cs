using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

/// <summary>
/// 系统Key转为库中Key后的对应类型
/// </summary>
public enum KeyTypes
{
    Model,
    Normal,
    None
}

namespace FastHotKeyForWPF
{
    /// <summary>
    /// 为Key的转换、合法性判断提供便利
    /// </summary>
    public static class KeyHelper
    {
        /// <summary>
        /// Key => Uint
        /// </summary>
        public static Dictionary<Key, uint> KeyToUint = new Dictionary<Key, uint>()
        {

        { Key.LeftCtrl, (uint)ModelKeys.CTRL },
        { Key.LeftAlt, (uint)ModelKeys.ALT },

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
        };

        /// <summary>
        /// Uint => Key
        /// </summary>
        public static Dictionary<uint, Key> UintToKey = KeyToUint.ToDictionary(kv => kv.Value, kv => kv.Key);

        /// <summary>
        /// NormalKey => Key
        /// </summary>
        public static Dictionary<NormalKeys, Key> NormalKeysToKey = KeyToNormalKeys.ToDictionary(kv => kv.Value, kv => kv.Key);

        /// <summary>
        /// ModelKey => Key
        /// </summary>
        public static Dictionary<ModelKeys, Key> ModelKeysToKey = KeyToModelKeys.ToDictionary(kv => kv.Value, kv => kv.Key);

        /// <summary>
        /// 检查键是否合法,以及它的所属类型
        /// </summary>
        /// <param name="target">要检查的键</param>
        /// <returns>如果键存在于 KeyToModelKeys 或 KeyToNormalKeys 中，返回 true 和其所属类型；否则返回 false 和 KeyTypes.None</returns>
        public static (bool, KeyTypes) IsKeyValid(Key target)
        {
            bool result = KeyToModelKeys.ContainsKey(target) ^ KeyToNormalKeys.ContainsKey(target);
            KeyTypes type = result ? (KeyToModelKeys.ContainsKey(target) ? KeyTypes.Model : KeyTypes.Normal) : KeyTypes.None;
            return (result, type);
        }
    }
}
