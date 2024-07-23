using FastHotKeyForWPF.DIYControls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace FastHotKeyForWPF
{
    public partial class HotKeyBox : UserControl
    {
        public HotKeyBox()
        {
            InitializeComponent();

            WhileInput += KeyHandling;
            BoxPool.hotKeyBoxes.Add(this);
        }

        internal HotKeyBox? Link { get; set; } = null;
        internal bool IsMainBox { get; set; } = false;

        /// <summary>
        ///  当前键
        /// </summary>
        public Key CurrentKey { get; internal set; }

        /// <summary>
        /// 用户按下Enter键时,您希望额外处理一些事情,例如弹出提示框以告诉用户完成了热键的设置
        /// </summary>
        public event Action? WhileInput = null;

        /// <summary>
        /// 若用户输入不受支持的Key，如何显示文本
        /// </summary>
        public string ErrorText { get; set; } = "Error";

        /// <summary>
        /// 反映该控件及其关联控件是否成功注册了热键
        /// </summary>
        public bool IsHotKeyRegistered { get; internal set; } = false;

        /// <summary>
        /// 上一个成功注册热键的ID
        /// </summary>
        public int LastHotKeyID { get; internal set; } = -1;

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
        /// 实际的控件背景色
        /// </summary>
        public SolidColorBrush ActualBackground
        {
            set
            {
                FixedBorder.Background = value;
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

        internal event Func<object>? HandleA;
        internal event Action? HandleB;

        /// <summary>
        /// 与其它 HotKeyBox 连接
        /// </summary>
        public void ConnectWith(HotKeyBox hotKeyBox, Action handle)
        {
            if (Link != null) { return; }

            Link = hotKeyBox;
            hotKeyBox.Link = this;

            HandleA = null;
            Link.HandleA = null;
            HandleB = null;
            Link.HandleB = null;

            HandleB = handle;
            Link.HandleB = handle;

            IsMainBox = true;
            Link.IsMainBox = false;

            KeyHandling();
        }

        /// <summary>
        /// 与其它 HotKeyBox 连接
        /// </summary>
        /// <param name="hotKeyBox"></param>
        /// <param name="handle"></param>
        public void ConnectWith(HotKeyBox hotKeyBox, Func<object> handle)
        {
            if (Link != null) { return; }

            Link = hotKeyBox;
            hotKeyBox.Link = this;

            HandleA = null;
            Link.HandleA = null;
            HandleB = null;
            Link.HandleB = null;

            HandleA = handle;
            Link.HandleA = handle;

            IsMainBox = true;
            Link.IsMainBox = false;

            KeyHandling();
        }

        /// <summary>
        /// 取消连接
        /// </summary>
        public void DisConnect()
        {
            if (Link == null) { return; }

            HandleA = null;
            Link.HandleA = null;
            HandleB = null;
            Link.HandleB = null;

            GlobalHotKey.DeleteById(LastHotKeyID);
            IsHotKeyRegistered = false;
            Link.IsHotKeyRegistered = false;
            LastHotKeyID = -1;
            Link.LastHotKeyID = -1;

            IsMainBox = false;
            Link.IsMainBox = false;

            Link.Link = null;
            Link = null;
        }

        /// <summary>
        /// 手动设置热键
        /// </summary>
        public bool SetHotKey(ModelKeys modelKeys, NormalKeys normalKeys, Action handle)
        {
            if (Link == null) { return false; }

            CurrentKey = KeyHelper.ModelKeysToKey[modelKeys];
            Link.CurrentKey = KeyHelper.NormalKeysToKey[normalKeys];

            HandleA = null;
            Link.HandleA = null;
            HandleB = handle;
            Link.HandleB = handle;

            KeyHandling();

            return false;
        }

        /// <summary>
        /// 手动设置热键
        /// </summary>
        public bool SetHotKey(ModelKeys modelKeys, NormalKeys normalKeys, Func<object> handle)
        {
            if (Link == null) { return false; }

            CurrentKey = KeyHelper.ModelKeysToKey[modelKeys];
            Link.CurrentKey = KeyHelper.NormalKeysToKey[normalKeys];

            HandleB = null;
            Link.HandleB = null;
            HandleA = handle;
            Link.HandleA = handle;

            KeyHandling();

            return false;
        }

        private void UserInput(object sender, KeyEventArgs e)
        {
            Key key = (e.Key == Key.System ? e.SystemKey : e.Key);

            if (key == Key.Return) { EmptyOne.Focus(); WhileInput?.Invoke(); e.Handled = true; return; }

            CurrentKey = key;

            ActualText.Text = key.ToString();

            e.Handled = true;
        }

        private void TextBox_MouseEnter(object sender, MouseEventArgs e)
        {
            FocusGet.Focus();
            ActualText.Foreground = HoverTextColor;
            FixedBorder.BorderBrush = HoverBorderBrush;
            ActualText.Text = CurrentKey.ToString();
            if (Link != null) { Link.ActualText.Text = Link.CurrentKey.ToString(); }
        }

        private void TextBox_MouseLeave(object sender, MouseEventArgs e)
        {
            KeyHandling();
            ActualText.Foreground = DefaultTextColor;
            FixedBorder.BorderBrush = DefaultBorderBrush;
            if (!IsHotKeyRegistered)
            {
                ActualText.Text = ErrorText;
                if (Link != null) { Link.ActualText.Text = Link.ErrorText; }
            }
            EmptyOne.Focus();
        }

        internal void KeyHandling()
        {
            GlobalHotKey.DeleteById(LastHotKeyID);

            IsHotKeyRegistered = false;
            LastHotKeyID = -1;

            var resultA = UpdateText();

            if (Link == null) { return; }

            Link.IsHotKeyRegistered = false;
            Link.LastHotKeyID = -1;

            var resultB = Link.UpdateText();

            BoxPool.RemoveSameInKey(CurrentKey, Link.CurrentKey, this);
            BoxPool.RemoveSameInKeys(CurrentKey, Link.CurrentKey);

            if (resultA.Item1 && resultB.Item1 && (resultA.Item2 != resultB.Item2))
            {
                ModelKeys model = ModelKeys.ALT;
                NormalKeys normal = NormalKeys.F1;

                switch (resultA.Item2)
                {
                    case KeyTypes.Model:
                        model = KeyHelper.KeyToModelKeys[CurrentKey];
                        normal = KeyHelper.KeyToNormalKeys[Link.CurrentKey];
                        break;
                    case KeyTypes.Normal:
                        model = KeyHelper.KeyToModelKeys[Link.CurrentKey];
                        normal = KeyHelper.KeyToNormalKeys[CurrentKey];
                        break;
                }

                if (HandleA != null)
                {
                    var result = GlobalHotKey.Add([model], normal, HandleA);
                    if (result.Item1)
                    {
                        IsHotKeyRegistered = true;
                        Link.IsHotKeyRegistered = true;
                        LastHotKeyID = result.Item2;
                        Link.LastHotKeyID = result.Item2;
                        return;
                    }
                }
                if (HandleB != null)
                {
                    var result = GlobalHotKey.Add([model], normal, HandleB);
                    if (result.Item1)
                    {
                        IsHotKeyRegistered = true;
                        Link.IsHotKeyRegistered = true;
                        LastHotKeyID = result.Item2;
                        return;
                    }
                }
            }

            IsHotKeyRegistered = false;
            Link.IsHotKeyRegistered = false;
            LastHotKeyID = -1;
            Link.LastHotKeyID = -1;
        }

        internal (bool, KeyTypes) UpdateText()
        {
            var result = KeyHelper.IsKeyValid(CurrentKey);
            if (result.Item1)
            {
                ActualText.Text = CurrentKey.ToString();
                return (true, result.Item2);
            }
            else
            {
                ActualText.Text = ErrorText;
            }
            return (false, result.Item2);
        }
    }
}
