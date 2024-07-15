using FastHotKeyForWPF.DIYControls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace FastHotKeyForWPF
{
    /// <summary>
    /// HotKeysBox.xaml 的交互逻辑
    /// </summary>
    public partial class HotKeysBox : UserControl
    {
        public HotKeysBox()
        {
            InitializeComponent();

            WhileInput += KeyHandling;
        }

        /// <summary>
        /// 系统键
        /// </summary>
        public Key CurrentKeyA { get; internal set; }

        /// <summary>
        /// 普通键
        /// </summary>
        public Key CurrentKeyB { get; internal set; }

        /// <summary>
        /// 用户按下Enter键时,您希望额外处理一些事情,例如弹出提示框以告诉用户完成了热键的设置
        /// </summary>
        public event Action? WhileInput = null;

        /// <summary>
        /// 若用户输入不受支持的Key，如何显示文本
        /// </summary>
        public string ErrorText { get; set; } = "Error";

        /// <summary>
        /// 连接左右Key的字符
        /// </summary>
        public string ConnectText { get; set; } = " + ";

        /// <summary>
        /// 反映该控件及其关联控件是否成功注册了热键
        /// </summary>
        public bool IsHotKeyRegistered { get; internal set; } = false;

        /// <summary>
        /// 上一个成功注册热键的ID
        /// </summary>
        internal int LastHotKeyID { get; set; } = -1;

        /// <summary>
        /// 圆滑度
        /// </summary>
        public CornerRadius CornerRadius
        {
            set
            {
                FixedBorder.CornerRadius = value;
            }
        }

        /// <summary>
        /// 默认文本色
        /// </summary>
        public SolidColorBrush DefaultTextColor { get; set; } = Brushes.White;

        /// <summary>
        /// 默认外边框色
        /// </summary>
        public SolidColorBrush DefaultBorderBrush { get; set; } = Brushes.White;

        /// <summary>
        /// 悬停文本色
        /// </summary>
        public SolidColorBrush HoverTextColor { get; set; } = Brushes.Cyan;

        /// <summary>
        /// 悬停边框色
        /// </summary>
        public SolidColorBrush HoverBorderBrush { get; set; } = Brushes.Cyan;

        internal event KeyInvoke_Return? HandleA;
        internal event KeyInvoke_Void? HandleB;

        /// <summary>
        /// 与指定的处理函数连接
        /// </summary>
        public void ConnectWith(KeyInvoke_Void handle)
        {
            HandleA = null;
            HandleB = null;

            HandleB = handle;
        }

        /// <summary>
        /// 与其它 HotKeyBox 连接
        /// </summary>
        public void ConnectWith(KeyInvoke_Return handle)
        {
            HandleA = null;
            HandleB = null;

            HandleA = handle;
        }

        /// <summary>
        /// 取消连接
        /// </summary>
        public void DisConnect()
        {
            HandleA = null;
            HandleB = null;

            GlobalHotKey.DeleteById(LastHotKeyID);
            IsHotKeyRegistered = false;
            LastHotKeyID = -1;
        }

        /// <summary>
        /// 手动设置热键
        /// </summary>
        public bool SetHotKey(ModelKeys modelKeys, NormalKeys normalKeys, KeyInvoke_Void handle)
        {
            CurrentKeyA = KeyHelper.ModelKeysToKey[modelKeys];
            CurrentKeyB = KeyHelper.NormalKeysToKey[normalKeys];

            HandleA = null;
            HandleB = handle;

            KeyHandling();

            return false;
        }

        /// <summary>
        /// 手动设置热键
        /// </summary>
        public bool SetHotKey(ModelKeys modelKeys, NormalKeys normalKeys, KeyInvoke_Return handle)
        {
            CurrentKeyA = KeyHelper.ModelKeysToKey[modelKeys];
            CurrentKeyB = KeyHelper.NormalKeysToKey[normalKeys];

            HandleB = null;
            HandleA = handle;

            KeyHandling();
            UpdateText();

            return false;
        }

        private void UserInput(object sender, KeyEventArgs e)
        {
            Key key = (e.Key == Key.System ? e.SystemKey : e.Key);

            if (key == Key.Return) { Keyboard.ClearFocus(); WhileInput?.Invoke(); return; }

            var result = KeyHelper.IsKeyValid(key);
            if (result.Item1)
            {
                switch (result.Item2)
                {
                    case KeyTypes.Model:
                        CurrentKeyA = key;
                        break;
                    case KeyTypes.Normal:
                        CurrentKeyB = key;
                        break;
                    case KeyTypes.None:
                        break;
                }
            }

            UpdateText();
        }

        private void TextBox_MouseEnter(object sender, MouseEventArgs e)
        {
            FocusGet.Focus();
            ActualText.Foreground = HoverTextColor;
            FixedBorder.BorderBrush = HoverBorderBrush;
        }

        private void TextBox_MouseLeave(object sender, MouseEventArgs e)
        {
            KeyHandling();
            ActualText.Foreground = DefaultTextColor;
            FixedBorder.BorderBrush = DefaultBorderBrush;
            EmptyOne.Focus();
        }

        internal void KeyHandling()
        {
            GlobalHotKey.DeleteById(LastHotKeyID);

            IsHotKeyRegistered = false;
            LastHotKeyID = -1;

            var resultA = KeyHelper.IsKeyValid(CurrentKeyA);
            var resultB = KeyHelper.IsKeyValid(CurrentKeyB);

            if (resultA.Item1 && resultB.Item1)
            {
                if (HandleA != null)
                {
                    var register = GlobalHotKey.Add(KeyHelper.KeyToModelKeys[CurrentKeyA], KeyHelper.KeyToNormalKeys[CurrentKeyB], HandleA);
                    if (register.Item1)
                    {
                        IsHotKeyRegistered = true;
                        LastHotKeyID = register.Item2;
                        BoxTool.RemoveSame(CurrentKeyA, CurrentKeyB);
                        return;
                    }
                }
                if (HandleB != null)
                {
                    var register = GlobalHotKey.Add(KeyHelper.KeyToModelKeys[CurrentKeyA], KeyHelper.KeyToNormalKeys[CurrentKeyB], HandleB);
                    if (register.Item1)
                    {
                        IsHotKeyRegistered = true;
                        LastHotKeyID = register.Item2;
                        BoxTool.RemoveSame(CurrentKeyA, CurrentKeyB);
                        return;
                    }
                }
            }

            IsHotKeyRegistered = false;
            LastHotKeyID = -1;
        }

        internal void UpdateText()
        {
            ActualText.Text = CurrentKeyA.ToString() + ConnectText + CurrentKeyB.ToString();
        }
    }
}
