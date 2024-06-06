using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;

namespace FastHotKeyForWPF
{
    /// <summary>
    /// 一个预制组件的基本信息
    /// </summary>
    public class ComponentInfo
    {
        public ComponentInfo() { }

        private double _fontsize = 1;
        private double _heightrate = 1;
        private double _widthrate = 1;
        private double _fontsizerate = 0.8;
        private double _width = 400;
        private double _height = 100;

        public double FontSize
        {
            get => _fontsize;
            set
            {
                if (value >= 1)
                {
                    _fontsize = value;
                }
            }
        }      

        public double WidthRate
        {
            get => _widthrate;
            set
            {
                if (value >= 0)
                {
                    _widthrate = value;
                }
            }
        }

        public double HeightRate
        {
            get => _heightrate;
            set
            {
                if (value >= 0)
                {
                    _heightrate = value;
                }
            }
        }

        public double FontSizeRate
        {
            get => _fontsizerate;
            set
            {
                if (value >= 0)
                {
                    _fontsizerate = value;
                }
            }
        }

        public double Width
        {
            get => _width;
            set
            {
                if (value >= 0)
                {
                    _width = value;
                }
            }
        }

        public double Height
        {
            get => _height;
            set
            {
                if (value >= 0)
                {
                    _height = value;
                }
            }
        }

        public SolidColorBrush Foreground = Brushes.Transparent;

        public SolidColorBrush Background = Brushes.Transparent;

        public Thickness Margin = new Thickness(0);

        public SolidColorBrush BorderBrush = Brushes.Transparent;

        public Thickness BorderThickness = new Thickness(0);

        public CornerRadius CornerRadius = new CornerRadius(0);
    }
}
