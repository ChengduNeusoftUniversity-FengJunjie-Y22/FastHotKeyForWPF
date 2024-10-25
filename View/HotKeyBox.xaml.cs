using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace FastHotKeyForWPF
{
    public partial class HotKeyBox : HotKeyControlBase
    {
        public HotKeyBox()
        {
            InitializeComponent();
            BoxPool.Add(this);
        }

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(HotKeyBox), new PropertyMetadata(new CornerRadius(5)));

        public Thickness EdgeThickness
        {
            get { return (Thickness)GetValue(EdgeThicknessProperty); }
            set { SetValue(EdgeThicknessProperty, value); }
        }
        public static readonly DependencyProperty EdgeThicknessProperty =
            DependencyProperty.Register("EdgeThickness", typeof(Thickness), typeof(HotKeyBox), new PropertyMetadata(new Thickness(1)));

        public Brush EdgeBrush
        {
            get { return (Brush)GetValue(EdgeBrushProperty); }
            set { SetValue(EdgeBrushProperty, value); }
        }
        public static readonly DependencyProperty EdgeBrushProperty =
            DependencyProperty.Register("EdgeBrush", typeof(Brush), typeof(HotKeyBox), new PropertyMetadata(Brushes.White));

        public Brush HoverEdgeBrush
        {
            get { return (Brush)GetValue(HoverEdgeBrushProperty); }
            set { SetValue(HoverEdgeBrushProperty, value); }
        }
        public static readonly DependencyProperty HoverEdgeBrushProperty =
            DependencyProperty.Register("HoverEdgeBrush", typeof(Brush), typeof(HotKeyBox), new PropertyMetadata(Brushes.Cyan));

        public Brush TextBrush
        {
            get { return (Brush)GetValue(TextBrushProperty); }
            set { SetValue(TextBrushProperty, value); }
        }
        public static readonly DependencyProperty TextBrushProperty =
            DependencyProperty.Register("TextBrush", typeof(Brush), typeof(HotKeyBox), new PropertyMetadata(Brushes.White));

        public Brush HoverTextBrush
        {
            get { return (Brush)GetValue(HoverTextBrushProperty); }
            set { SetValue(HoverTextBrushProperty, value); }
        }
        public static readonly DependencyProperty HoverTextBrushProperty =
            DependencyProperty.Register("HoverTextBrush", typeof(Brush), typeof(HotKeyBox), new PropertyMetadata(Brushes.Cyan));

        public Brush ActualBackground
        {
            get { return (Brush)GetValue(ActualBackgroundProperty); }
            set { SetValue(ActualBackgroundProperty, value); }
        }
        public static readonly DependencyProperty ActualBackgroundProperty =
            DependencyProperty.Register("ActualBackground", typeof(Brush), typeof(HotKeyBox), new PropertyMetadata(Brushes.Transparent));
        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            FocusGet.Focus();
            ActualText.Foreground = HoverTextBrush;
            Edge.BorderBrush = HoverEdgeBrush;
        }
        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            EmptyOne.Focus();
            ActualText.Foreground = TextBrush;
            Edge.BorderBrush = EdgeBrush;
        }
        private void FocusGet_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            UpdateText();
            KeyHelper.KeyParse(this, e);
            e.Handled = true;
        }
    }
}
