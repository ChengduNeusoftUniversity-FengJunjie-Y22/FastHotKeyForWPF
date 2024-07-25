using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace FastHotKeyForWPF
{
    /// <summary>
    /// 控件 HotKeyBox 的数据模型
    /// </summary>
    public class HotKeyBoxModel : HotKeyModelBase
    {
        public HotKeyBoxModel() { }

        public string Text { get; set; } = string.Empty;

        public string ErrorText { get; set; } = "Error";

        public string ConnectText { get; set; } = " + ";

        public bool IsHotKeyRegistered { get; internal set; } = false;

        internal int LastHotKeyID { get; set; } = -1;

        public CornerRadius CornerRadius { get; set; } = new CornerRadius(5);

        public SolidColorBrush ActualBackground { get; set; } = Brushes.Transparent;

        public SolidColorBrush DefaultTextColor { get; set; } = Brushes.White;

        public SolidColorBrush DefaultBorderBrush { get; set; } = Brushes.White;

        public Thickness DefaultBorderThickness { get; set; } = new Thickness(1);

        public SolidColorBrush HoverTextColor { get; set; } = Brushes.Cyan;

        public SolidColorBrush HoverBorderBrush { get; set; } = Brushes.Cyan;
    }
}
