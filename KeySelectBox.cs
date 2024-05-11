using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using System.Reflection;
using System.Windows.Media;

public enum KeyTypes
{
    Normal,
    Model,
    None
}

namespace FastHotKeyForWPF
{
    public class KeySelectBox : TextBox
    {
        private Key CurrentKey;

        private KeySelectBox? LinkKey;

        private bool NewStyle = false;

        public KeyTypes KeyType
        {
            get
            {
                if (Enum.IsDefined(typeof(NormalKeys), CurrentKey.ToString()))
                {
                    return KeyTypes.Normal;
                }
                if (Enum.IsDefined(typeof(ModelKeys), CurrentKey.ToString()))
                {
                    return KeyTypes.Model;
                }
                return KeyTypes.None;
            }
        }

        public KeySelectBox()
        {
            if (PrefabComponent.TempInfo == null) { return; }
            Width = 100;
            Height = 50;
            VerticalContentAlignment = VerticalAlignment.Center;
            HorizontalContentAlignment = HorizontalAlignment.Center;
            IsReadOnly = true;
            FontSize = PrefabComponent.TempInfo.FontSize;
            Foreground = PrefabComponent.TempInfo.Foreground;
            Background = PrefabComponent.TempInfo.Background;
            Margin = PrefabComponent.TempInfo.Margin;
            KeyUp += WhileKeyUp;
            MouseEnter += WhileMouseEnter;
            MouseLeave += WhileMouseLeave;
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
                if (!PrefabComponent.KeyToUint.ContainsKey(CurrentKey))
                {
                    return null;
                }
                return PrefabComponent.KeyToUint[CurrentKey] as T;
            }
            return null;
        }

        private void WhileKeyUp(object sender, KeyEventArgs e)
        {
            Key key = (e.Key == Key.System ? e.SystemKey : e.Key);
            if (!PrefabComponent.KeyToUint.ContainsKey(key)) { if (GlobalHotKey.IsDeBug) MessageBox.Show($"当前版本不支持这个按键【{key}】"); return; }
            CurrentKey = key;
            Text = key.ToString();
            if (GlobalHotKey.IsDeBug) { MessageBox.Show($"已更新为【{key}】"); }
            e.Handled = true;
        }

        private void WhileMouseEnter(object sender, MouseEventArgs e)
        {
            Focus();
            if (!NewStyle)
            {
                Background = Brushes.Black;
                Foreground = Brushes.Cyan;
            }
        }

        private void WhileMouseLeave(object sender, MouseEventArgs e)
        {
            Keyboard.ClearFocus();
            if (!NewStyle)
            {
                Background = Brushes.Wheat;
                Foreground = Brushes.Black;
            }
        }

        public void UseFatherSize<T>() where T : UIElement
        {
            T? father = Parent as T;
            if (father == null) { return; }

            PropertyInfo? widthProperty = typeof(T).GetProperty("Width");
            if (widthProperty == null) { return; }
            object? width = widthProperty.GetValue(father);
            if (width == null) { return; }
            Width = (double)width;

            PropertyInfo? heightProperty = typeof(T).GetProperty("Height");
            if (heightProperty == null) { return; }
            object? height = heightProperty.GetValue(father);
            if (height == null) { return; }
            Height = (double)height;
        }

        public void UseStyle(string stylename)
        {
            Style? style = (Style)FindResource(stylename);
            if (style == null) return;
            Style = style;
            NewStyle = true;
        }
    }
}
