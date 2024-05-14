using System.Windows.Controls;

namespace FastHotKeyForWPF
{
    /// <summary>
    /// 抽象类型，继承自TextBox并实现Component接口
    /// <para>热键设置中，接收用户按下键的组件是基于这个做下去的</para>
    /// </summary>
    public abstract class KeyBox : TextBox, Component
    {
        /// <summary>
        /// KeySelectBox组件是否处于保护中
        /// </summary>
        public static bool IsKeySelectBoxProtected = false;
        public static List<KeySelectBox> keySelectBoxes = new List<KeySelectBox>();

        /// <summary>
        /// KeysSelectBox组件是否处于保护中
        /// </summary>
        public static bool IsKeysSelectBoxProtected = false;
        public static List<KeysSelectBox> keysSelectBoxes = new List<KeysSelectBox>();
    }
}
