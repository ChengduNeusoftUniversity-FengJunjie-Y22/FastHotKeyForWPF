using System.Windows.Controls;

namespace FastHotKeyForWPF
{
    public class PrefabComponent
    {
        public static ComponentInfo? TempInfo;

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
            public KeySelectBox()
            {
                if (TempInfo == null) { return; }
                FontSize = TempInfo.FontSize;
                Foreground = TempInfo.Foreground;
                Background = TempInfo.Background;
                Margin = TempInfo.Margin;
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
            }
        }
    }
}
