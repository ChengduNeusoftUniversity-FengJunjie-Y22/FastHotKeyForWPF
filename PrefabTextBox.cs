using System.Windows.Controls;

namespace FastHotKeyForWPF
{
    /// <summary>
    /// 抽象类型，所有需要继承自TextBox并实现Component接口的类型都继承自此类型
    /// </summary>
    public abstract class PrefabTextBox : TextBox, Component
    {
        /// <summary>
        /// KeySelectBox组件是否处于保护中
        /// </summary>
        public static bool IsKeySelectProtected = false;
        public static List<KeySelectBox> keySelectBoxes = new List<KeySelectBox>();

        /// <summary>
        /// KeysSelectBox组件是否处于保护中
        /// </summary>
        public static bool IsKeysSelectProtected = false;
        public static List<KeysSelectBox> keysSelectBoxes = new List<KeysSelectBox>();
    }
}
