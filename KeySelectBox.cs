using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;

namespace FastHotKeyForWPF
{
    public class KeySelectBox : TextBox
    {
        private Key CurrentKey;

        public KeySelectBox()
        {
            if (PrefabComponent.TempInfo == null) { return; }
            IsReadOnly = true;
            FontSize = PrefabComponent.TempInfo.FontSize;
            Foreground = PrefabComponent.TempInfo.Foreground;
            Background = PrefabComponent.TempInfo.Background;
            Margin = PrefabComponent.TempInfo.Margin;
            KeyUp += WhileKeyUp;
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
    }
}
