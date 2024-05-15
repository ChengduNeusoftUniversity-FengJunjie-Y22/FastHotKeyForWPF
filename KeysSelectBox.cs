using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using static System.Net.Mime.MediaTypeNames;

namespace FastHotKeyForWPF
{
    /// <summary>
    /// 组件☆
    /// <para>功能 接收两个用户按下的按键，并在与其它函数Connect后，激活热键的全自动管理</para>
    /// <para>继承 TextBox类</para>
    /// <para>实现 Component接口</para>
    /// </summary>
    public class KeysSelectBox : KeyBox
    {
        Key key1;
        Key key2;

        public Key CurrentKeyA
        {
            get { return key1; }
            set
            {
                RemoveOldHotKey();
                key1 = value;
                UpdateText();
                UpdateHotKey();
            }
        }
        public Key CurrentKeyB
        {
            get { return key2; }
            set
            {
                RemoveOldHotKey();
                key2 = value;
                UpdateText();
                UpdateHotKey();
            }
        }

        internal KeysSelectBox()
        {
            if (PrefabComponent.TempInfo == null) { return; }
            Width = 200;
            Height = 50;
            VerticalContentAlignment = VerticalAlignment.Center;
            HorizontalContentAlignment = HorizontalAlignment.Center;
            IsReadOnly = true;
            FontSize = PrefabComponent.TempInfo.FontSize;
            Foreground = PrefabComponent.TempInfo.Foreground;
            Background = PrefabComponent.TempInfo.Background;
            Margin = PrefabComponent.TempInfo.Margin;
            PreviewKeyDown += WhileKeysDown;
            MouseEnter += WhileMouseEnter;
            MouseLeave += WhileMouseLeave;
            keysSelectBoxes.Add(this);
        }

        private void WhileKeysDown(object sender, KeyEventArgs e)
        {
            if (IsKeysSelectBoxProtected || Protected) { return; }
            Key key = (e.Key == Key.System ? e.SystemKey : e.Key);
            if (KeyToModelKeys.ContainsKey(key))
            {
                CurrentKeyA = key;
            }
            else if (KeyToNormalKeys.ContainsKey(key))
            {
                CurrentKeyB = key;
            }
            e.Handled = true;
        }

        private void RemoveOldHotKey()
        {
            if (KeyToModelKeys.ContainsKey(CurrentKeyA) && KeyToNormalKeys.ContainsKey(CurrentKeyB))
            {
                GlobalHotKey.DeleteByKeys(KeyToModelKeys[CurrentKeyA], KeyToNormalKeys[CurrentKeyB]);
            }
        }

        private void UpdateHotKey()
        {
            if (Event_void == null && Event_return == null) { return; }
            if (KeyToModelKeys.ContainsKey(CurrentKeyA) && KeyToNormalKeys.ContainsKey(CurrentKeyB))
            {
                if (Event_void != null)
                {
                    var result = GlobalHotKey.Add(KeyToModelKeys[CurrentKeyA], KeyToNormalKeys[CurrentKeyB], Event_void);
                    if (result.Item1)
                    {
                        if (GlobalHotKey.IsDeBug) { MessageBox.Show(result.Item2); }
                    }
                    return;
                }
                if (Event_return != null)
                {
                    var result = GlobalHotKey.Add(KeyToModelKeys[CurrentKeyA], KeyToNormalKeys[CurrentKeyB], Event_return);
                    if (result.Item1)
                    {
                        if (GlobalHotKey.IsDeBug) { MessageBox.Show(result.Item2); }
                    }
                    return;
                }
            }
        }

        private void UpdateText()
        {
            Text = key1.ToString() + " + " + key2.ToString();
        }
    }
}
