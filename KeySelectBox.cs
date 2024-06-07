using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using System.Reflection;
using System.Windows.Media;
using System.Xml.Serialization;

public enum KeyTypes
{
    Normal,
    Model,
    None
}

namespace FastHotKeyForWPF
{
    /// <summary>
    /// 组件☆
    /// <para>功能 接收用户按下的单个键，并在与其它KeySelectBox连接后，激活热键的全自动管理</para>
    /// <para>继承 TextBox类</para>
    /// <para>实现 Component接口</para>
    /// </summary>
    public class KeySelectBox : KeyBox
    {
        private Key _currentkey;
        /// <summary>
        /// 当前获取到的用户按键
        /// </summary>
        public Key CurrentKey
        {
            get { return _currentkey; }
            set
            {
                if (value == Key.None) { _currentkey = value; return; }
                var olddate = BindingRef.GetKeysFromConnection(this);
                if (olddate.Item1 != null && olddate.Item2 != null)
                {
                    GlobalHotKey.DeleteByKeys((ModelKeys)olddate.Item1, (NormalKeys)olddate.Item2);
                    //更新消息前都会先清除原来注册的热键
                }
                _currentkey = value;
                Text = value.ToString();
                var result = UpdateHotKey();
                if (result.Item1)
                {
                    IsSuccessRegister = true;
                    if (LinkBox != null) { LinkBox.IsSuccessRegister = true; }

                    if (GlobalHotKey.IsDeBug) { MessageBox.Show(result.Item2); }
                    //注册成功且处于DeBug模式下，会打印注册信息

                    var temp = BindingRef.GetKeysFromConnection(this);
                    if (temp.Item1 != null && temp.Item2 != null) { RemoveSameKeySelect((ModelKeys)temp.Item1, (NormalKeys)temp.Item2, this); }
                    //若成功则清除其它与此相同的Box
                }
                else
                {
                    IsSuccessRegister = false;
                    if (LinkBox != null) { LinkBox.IsSuccessRegister = false; }
                }
            }
        }

        /// <summary>
        /// 是否处于连接状态
        /// </summary>
        public bool IsConnected
        {
            get
            {
                if (LinkBox == null && Event_void == null && Event_return == null) return false;
                if (LinkBox != null && Event_void != null && Event_return == null) return true;
                if (LinkBox != null && Event_void == null && Event_return != null) return true;
                return false;
            }
        }

        /// <summary>
        /// 该组件的关联组件
        /// </summary>
        internal KeySelectBox? LinkBox;

        /// <summary>
        /// 当前按键的类型
        /// </summary>
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

        /// <summary>
        /// 设置成功注册时需要触发的函数
        /// </summary>
        public void UseSuccessTrigger(TextBoxChange func)
        {
            SuccessRegister = func;
            if (IsConnected)
            {
                LinkBox.SuccessRegister = func;
            }
        }

        /// <summary>
        /// 设置失败注册时需要触发的函数
        /// </summary>
        public void UseFailureTrigger(TextBoxChange func)
        {
            LoseRegister = func;
            if (IsConnected)
            {
                LinkBox.LoseRegister = func;
            }
        }

        internal KeySelectBox()
        {
            if (PrefabComponent.TempInfo == null) { return; }
            IsReadOnly = true;
            HorizontalContentAlignment = HorizontalAlignment.Center;
            FontSize = PrefabComponent.TempInfo.FontSize;
            Foreground = PrefabComponent.TempInfo.Foreground;
            Background = PrefabComponent.TempInfo.Background;
            Margin = PrefabComponent.TempInfo.Margin;
            PreviewKeyDown += WhileKeyDown;
            MouseEnter += WhileMouseEnter;
            MouseEnter += UpdateText;
            MouseLeave += WhileMouseLeave;
            MouseLeave += InvokeLeaveEvent;
            keySelectBoxes.Add(this);
        }

        private void WhileKeyDown(object sender, KeyEventArgs e)
        {
            if (IsKeySelectBoxProtected || Protected) { return; }
            Key key = (e.Key == Key.System ? e.SystemKey : e.Key);
            if (!PrefabComponent.KeyToUint.ContainsKey(key)) { if (GlobalHotKey.IsDeBug) MessageBox.Show($"当前版本不支持这个按键【{key}】"); return; }
            CurrentKey = key;
            e.Handled = true;
        }

        /// <summary>
        /// 更新一次热键信息
        /// </summary>
        /// <returns>bool 表示是否成功更新</returns>
        internal (bool, string) UpdateHotKey()
        {
            bool issucces = false;
            string info = string.Empty;
            if (IsConnected)
            {
                var date = BindingRef.GetKeysFromConnection(this);

                if (date.Item1 == null || date.Item2 == null) { return (issucces, info); }

                if (Event_void != null)
                {
                    var result = GlobalHotKey.Add((ModelKeys)date.Item1, (NormalKeys)date.Item2, Event_void);
                    issucces = result.Item1;
                    info = result.Item2;
                }
                else if (Event_return != null)
                {
                    var result = GlobalHotKey.Add((ModelKeys)date.Item1, (NormalKeys)date.Item2, Event_return);
                    issucces = result.Item1;
                    info = result.Item2;
                }
            }
            return (issucces, info);
        }

        internal void UpdateText(object sender, EventArgs e)
        {
            Text = CurrentKey.ToString();
            if (IsConnected)
            {
                LinkBox.Text = LinkBox.CurrentKey.ToString();
            }
        }

        internal void InvokeLeaveEvent(object sender, EventArgs e)
        {
            if (IsSuccessRegister) { SuccessRegister?.Invoke(this); } else { LoseRegister?.Invoke(this); }
        }
    }
}
