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

        public static readonly DependencyProperty CurrentKeyAProperty =
            DependencyProperty.Register("CurrentKeyA", typeof(Key), typeof(HotKeyBox), new PropertyMetadata(Key.None, OnKeyAChanged));
        public static readonly DependencyProperty CurrentKeyBProperty =
            DependencyProperty.Register("CurrentKeyB", typeof(Key), typeof(HotKeyBox), new PropertyMetadata(Key.None, OnKeyBChanged));
        public static readonly DependencyProperty HandlerProperty =
            DependencyProperty.Register("Handler", typeof(HotKeyEventHandler), typeof(HotKeyBox), new PropertyMetadata(null, OnHandlerChanged));

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
            target.ViewModel.Handler = (HotKeyEventHandler)e.NewValue;
        }

        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            FocusGet.Focus();
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            EmptyOne.Focus();
        }

        private void FocusGet_PreviewKeyDown(object sender, KeyEventArgs e)
        {
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
    }
}
