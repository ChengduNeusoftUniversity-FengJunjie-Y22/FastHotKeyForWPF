using System.Windows.Controls;

namespace FastHotKeyForWPF
{
    public class PrefabComponent
    {
        public static T GetComponent<T>() where T : class, new()
        {
            return new T();
        }

        public static T GetComponent<T>(SizeInfo info) where T : class, new()
        {
            return new T();
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
