using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace FastHotKeyForWPF
{
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
