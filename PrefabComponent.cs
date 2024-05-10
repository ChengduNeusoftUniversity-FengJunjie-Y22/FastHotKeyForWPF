using System.Windows.Controls;

namespace FastHotKeyForWPF
{
    public class PrefabComponent
    {
        public static T GetComponent<T>() where T : class, new()
        {
            T component = new T();
            return component;
        }

        public class KeySelectBox : TextBox
        {
            public KeySelectBox()
            {

            }
        }

        public class HotKeyNameBlock : TextBlock
        {
            public HotKeyNameBlock()
            {

            }
        }
    }
}
