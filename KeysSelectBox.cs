using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using static System.Net.Mime.MediaTypeNames;

namespace FastHotKeyForWPF
{
    /// <summary>
    /// 组件☆
    /// <para>功能 接收用户按下的前两个按键，激活热键的全自动管理</para>
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
                key1 = value; 
                BindingRef.DisConnect(this); 
                UpdateHotKey(); 
                UpdateText();
            }
        }
        public Key CurrentKeyB
        {
            get { return key2; }
            set
            {
                key2 = value;
                BindingRef.DisConnect(this);
                UpdateHotKey();
                UpdateText();
            }
        }

        public KeyTypes KeyTypeA
        {
            get
            {
                if (Enum.IsDefined(typeof(NormalKeys), CurrentKeyA.ToString()))
                {
                    return KeyTypes.Normal;
                }
                if (Enum.IsDefined(typeof(ModelKeys), CurrentKeyA.ToString()))
                {
                    return KeyTypes.Model;
                }
                return KeyTypes.None;
            }
        }
        public KeyTypes KeyTypeB
        {
            get
            {
                if (Enum.IsDefined(typeof(NormalKeys), CurrentKeyB.ToString()))
                {
                    return KeyTypes.Normal;
                }
                if (Enum.IsDefined(typeof(ModelKeys), CurrentKeyB.ToString()))
                {
                    return KeyTypes.Model;
                }
                return KeyTypes.None;
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
            KeyDown += WhileKeysDown;
            MouseEnter += WhileMouseEnter;
            MouseLeave += WhileMouseLeave;
            keysSelectBoxes.Add(this);
        }

        private void WhileKeysDown(object sender, KeyEventArgs e)
        {
            if (IsKeysSelectBoxProtected || Protected) { return; }
            Key key = (e.Key == Key.System ? e.SystemKey : e.Key);
            if (KeySelectBox.KeyToModelKeys.ContainsKey(key))
            {
                CurrentKeyA = key;
            }
            else if (KeySelectBox.KeyToNormalKeys.ContainsKey(key))
            {
                CurrentKeyB = key;
            }
            e.Handled = true;
        }

        private void UpdateHotKey()
        {
            if (Event_void == null && Event_return == null) { return; }
            if (KeyTypeA == KeyTypes.None || KeyTypeB == KeyTypes.None) { return; }
            else
            {
                if (KeyTypeA == KeyTypes.Model)
                {
                    if (Event_void != null)
                    {
                        GlobalHotKey.Add(KeySelectBox.KeyToModelKeys[CurrentKeyA], KeySelectBox.KeyToNormalKeys[CurrentKeyB], Event_void);
                        return;
                    }
                    if (Event_return != null)
                    {
                        GlobalHotKey.Add(KeySelectBox.KeyToModelKeys[CurrentKeyA], KeySelectBox.KeyToNormalKeys[CurrentKeyB], Event_return);
                        return;
                    }
                }
                else
                {
                    if (Event_void != null)
                    {
                        GlobalHotKey.Add(KeySelectBox.KeyToModelKeys[CurrentKeyB], KeySelectBox.KeyToNormalKeys[CurrentKeyA], Event_void);
                        return;
                    }
                    if (Event_return != null)
                    {
                        GlobalHotKey.Add(KeySelectBox.KeyToModelKeys[CurrentKeyB], KeySelectBox.KeyToNormalKeys[CurrentKeyA], Event_return);
                        return;
                    }
                }
            }
        }

        private void UpdateText()
        {
            Text = key1.ToString() + " + " + key2.ToString();
        }
    }
}
