using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace FastHotKeyForWPF
{
    public partial class HotKeyBox : UserControl, IAutoHotKeyProperty
    {
        public HotKeyBox()
        {
            InitializeComponent();

            BoxPool.Add(this, ViewModel);
        }
        public HotKeyBox(bool IsOnlyOne)
        {
            InitializeComponent();

            if (IsOnlyOne) { BoxPool.Add(this, ViewModel); }
        }

        #region 属性

        public int PoolID { get; set; } = 0;

        public Key CurrentKeyA
        {
            get { return (Key)GetValue(CurrentKeyAProperty); }
            set { SetValue(CurrentKeyAProperty, value); }
        }
        public Key CurrentKeyB
        {
            get { return (Key)GetValue(CurrentKeyBProperty); }
            set { SetValue(CurrentKeyBProperty, value); }
        }

        public HotKeyEventHandler? HandlerData { get; set; }
        public event HotKeyEventHandler? Handler
        {
            add { SetValue(HandlerProperty, value); }
            remove { SetValue(HandlerProperty, null); }
        }

        public string ErrorText
        {
            get { return (string)GetValue(ErrorTextProperty); }
            set { SetValue(ErrorTextProperty, value); }
        }
        public string ConnectText
        {
            get { return (string)GetValue(CurrentKeyBProperty); }
            set { SetValue(CurrentKeyBProperty, value); }
        }

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public SolidColorBrush ActualBackground
        {
            get { return (SolidColorBrush)GetValue(ActualBackgroundProperty); }
            set { SetValue(ActualBackgroundProperty, value); }
        }
        public SolidColorBrush TextColor
        {
            get { return (SolidColorBrush)GetValue(TextColorProperty); }
            set { SetValue(TextColorProperty, value); }
        }
        public SolidColorBrush FixedBorderBrush
        {
            get { return (SolidColorBrush)GetValue(FixedBorderBrushProperty); }
            set { SetValue(FixedBorderBrushProperty, value); }
        }
        public SolidColorBrush HoverTextColor
        {
            get { return (SolidColorBrush)GetValue(HoverTextColorProperty); }
            set { SetValue(HoverTextColorProperty, value); }
        }
        public SolidColorBrush HoverBorderBrush
        {
            get { return (SolidColorBrush)GetValue(HoverBorderBrushProperty); }
            set { SetValue(HoverBorderBrushProperty, value); }
        }
        public Thickness FixedBorderThickness
        {
            get { return (Thickness)GetValue(FixedBorderThicknessProperty); }
            set { SetValue(FixedBorderThicknessProperty, value); }
        }

        #endregion

        #region 依赖属性注册

        public static readonly DependencyProperty CurrentKeyAProperty =
            DependencyProperty.Register(nameof(CurrentKeyA), typeof(Key), typeof(HotKeyBox), new PropertyMetadata(Key.None, OnKeyAChanged));
        public static readonly DependencyProperty CurrentKeyBProperty =
            DependencyProperty.Register(nameof(CurrentKeyB), typeof(Key), typeof(HotKeyBox), new PropertyMetadata(Key.None, OnKeyBChanged));
        public static readonly DependencyProperty HandlerProperty =
            DependencyProperty.Register(nameof(Handler), typeof(HotKeyEventHandler), typeof(HotKeyBox), new PropertyMetadata(null, OnHandlerChanged));
        public static readonly DependencyProperty ErrorTextProperty =
            DependencyProperty.Register(nameof(ErrorText), typeof(string), typeof(HotKeyBox), new PropertyMetadata(string.Empty, OnErrorTextChanged));
        public static readonly DependencyProperty ConnectTextProperty =
            DependencyProperty.Register(nameof(ConnectText), typeof(string), typeof(HotKeyBox), new PropertyMetadata(string.Empty, OnConnectTextChanged));
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(HotKeyBox), new PropertyMetadata(new CornerRadius(5), OnCornerRadiusChanged));
        public static readonly DependencyProperty ActualBackgroundProperty =
            DependencyProperty.Register(nameof(ActualBackground), typeof(SolidColorBrush), typeof(HotKeyBox), new PropertyMetadata(Brushes.Transparent, OnActualBackgroundChanged));
        public static readonly DependencyProperty TextColorProperty =
            DependencyProperty.Register(nameof(TextColor), typeof(SolidColorBrush), typeof(HotKeyBox), new PropertyMetadata(Brushes.White, OnTextColorChanged));
        public static readonly DependencyProperty FixedBorderBrushProperty =
            DependencyProperty.Register(nameof(FixedBorderBrush), typeof(SolidColorBrush), typeof(HotKeyBox), new PropertyMetadata(Brushes.White, OnFixedBorderBrushChanged));
        public static readonly DependencyProperty HoverTextColorProperty =
            DependencyProperty.Register(nameof(HoverTextColor), typeof(SolidColorBrush), typeof(HotKeyBox), new PropertyMetadata(Brushes.Cyan, OnHoverTextColorChanged));
        public static readonly DependencyProperty HoverBorderBrushProperty =
            DependencyProperty.Register(nameof(HoverBorderBrush), typeof(SolidColorBrush), typeof(HotKeyBox), new PropertyMetadata(Brushes.Cyan, OnHoverBorderBrushChanged));
        public static readonly DependencyProperty FixedBorderThicknessProperty =
            DependencyProperty.Register(nameof(FixedBorderThickness), typeof(Thickness), typeof(HotKeyBox), new PropertyMetadata(new Thickness(1), OnFixedBorderThicknessChanged));

        private static void OnKeyAChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var target = (HotKeyBox)d;
            target.ViewModel.CurrentKeyA = (Key)e.NewValue;
        }
        private static void OnKeyBChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var target = (HotKeyBox)d;
            target.ViewModel.CurrentKeyB = (Key)e.NewValue;
        }
        private static void OnHandlerChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var target = (HotKeyBox)d;
            target.HandlerData = (HotKeyEventHandler)e.NewValue;
            target.ViewModel.HandlerData = (HotKeyEventHandler)e.NewValue;
        }
        private static void OnErrorTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var target = (HotKeyBox)d;
            target.ViewModel.ErrorText = (string)e.NewValue;
        }
        private static void OnConnectTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var target = (HotKeyBox)d;
            target.ViewModel.ConnectText = (string)e.NewValue;
        }
        private static void OnCornerRadiusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var target = (HotKeyBox)d;
            target.ViewModel.CornerRadius = (CornerRadius)e.NewValue;
        }
        private static void OnActualBackgroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var target = (HotKeyBox)d;
            target.ViewModel.ActualBackground = (SolidColorBrush)e.NewValue;
        }
        private static void OnTextColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var target = (HotKeyBox)d;
            target.ViewModel.DefaultTextColor = (SolidColorBrush)e.NewValue;
        }
        private static void OnFixedBorderBrushChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var target = (HotKeyBox)d;
            target.ViewModel.DefaultBorderBrush = (SolidColorBrush)e.NewValue;
        }
        private static void OnHoverTextColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var target = (HotKeyBox)d;
            target.ViewModel.HoverTextColor = (SolidColorBrush)e.NewValue;
        }
        private static void OnHoverBorderBrushChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var target = (HotKeyBox)d;
            target.ViewModel.HoverBorderBrush = (SolidColorBrush)e.NewValue;
        }
        private static void OnFixedBorderThicknessChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var target = (HotKeyBox)d;
            target.ViewModel.DefaultBorderThickness = (Thickness)e.NewValue;
        }

        #endregion

        #region 事件

        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            FocusGet.Focus();
            ActualText.Foreground = ViewModel.HoverTextColor;
            FixedBorder.BorderBrush = ViewModel.HoverBorderBrush;
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            EmptyOne.Focus();
            ActualText.Foreground = ViewModel.DefaultTextColor;
            FixedBorder.BorderBrush = ViewModel.DefaultBorderBrush;
        }

        private void FocusGet_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            ViewModel.UpdateText();

            Key key = (e.Key == Key.System ? e.SystemKey : e.Key);

            var result = KeyHelper.IsKeyValid(key);

            if (result.Item1)
            {
                if (result.Item2 == KeyTypes.ModelKey)
                {
                    CurrentKeyA = key;
                }
                if (result.Item2 == KeyTypes.NormalKey)
                {
                    CurrentKeyB = key;
                }
            }

            e.Handled = true;
        }

        #endregion
    }
}
