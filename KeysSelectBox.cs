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
                if (value == Key.None) { key1 = value; return; }
                RemoveOldHotKey();
                key1 = value;
                UpdateText(this, new EventArgs());
                var result = UpdateHotKey();
                if (result.Item1)
                {
                    IsSuccessRegister = true;
                    if (GlobalHotKey.IsDeBug) { MessageBox.Show(result.Item2); }
                    //若更新成功且处于DeBug模式，则打印注册结果

                    RemoveSameKeysSelect(KeyToModelKeys[key1], KeyToNormalKeys[key2], this);
                    //若更新成功，清除与此相同的
                }
                else
                {
                    IsSuccessRegister = false;
                }
            }
        }
        public Key CurrentKeyB
        {
            get { return key2; }
            set
            {
                if (value == Key.None) { key2 = value; return; }
                RemoveOldHotKey();
                key2 = value;
                UpdateText(this, new EventArgs());
                var result = UpdateHotKey();
                if (result.Item1)
                {
                    IsSuccessRegister = true;

                    if (GlobalHotKey.IsDeBug) { MessageBox.Show(result.Item2); }
                    //若更新成功且处于DeBug模式，则打印注册结果

                    RemoveSameKeysSelect(KeyToModelKeys[key1], KeyToNormalKeys[key2], this);
                    //若更新成功，清除与此相同的
                }
                else
                {
                    IsSuccessRegister = false;
                }
            }
        }

        public bool IsConnected = false;

        /// <summary>
        /// 设置成功注册时需要触发的函数
        /// </summary>
        public void UseSuccessTrigger(TextBoxChange func)
        {
            SuccessRegister = func;
        }

        /// <summary>
        /// 设置失败注册时需要触发的函数
        /// </summary>
        public void UseFailureTrigger(TextBoxChange func)
        {
            LoseRegister = func;
        }

        internal KeysSelectBox()
        {
            if (PrefabComponent.TempInfo == null) { return; }
            HorizontalContentAlignment = HorizontalAlignment.Center;
            IsReadOnly = true;
            FontSize = PrefabComponent.TempInfo.FontSize;
            Foreground = PrefabComponent.TempInfo.Foreground;
            Background = PrefabComponent.TempInfo.Background;
            Margin = PrefabComponent.TempInfo.Margin;
            PreviewKeyDown += WhileKeysDown;
            MouseEnter += WhileMouseEnter;
            MouseEnter += UpdateText;
            MouseLeave += WhileMouseLeave;
            MouseLeave += InvokeLeaveEvent;
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

        private (bool, string) UpdateHotKey()
        {
            bool result1 = false;
            string result2 = string.Empty;
            if (Event_void == null && Event_return == null) { return (result1, result2); }
            if (KeyToModelKeys.ContainsKey(CurrentKeyA) && KeyToNormalKeys.ContainsKey(CurrentKeyB))
            {
                if (Event_void != null)
                {
                    var result = GlobalHotKey.Add(KeyToModelKeys[CurrentKeyA], KeyToNormalKeys[CurrentKeyB], Event_void);
                    result1 = result.Item1;
                    result2 = result.Item2;
                }
                else if (Event_return != null)
                {
                    var result = GlobalHotKey.Add(KeyToModelKeys[CurrentKeyA], KeyToNormalKeys[CurrentKeyB], Event_return);
                    result1 = result.Item1;
                    result2 = result.Item2;
                }
            }
            return (result1, result2);
        }

        private void UpdateText(object sender, EventArgs e)
        {
            Text = key1.ToString() + " + " + key2.ToString();
        }
        internal void InvokeLeaveEvent(object sender, EventArgs e)
        {
            if (IsSuccessRegister) { SuccessRegister?.Invoke(this); } else { LoseRegister?.Invoke(this); }
        }
    }
}
