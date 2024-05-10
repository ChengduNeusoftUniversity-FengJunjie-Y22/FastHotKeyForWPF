using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows;
using System.Windows.Media;

namespace FastHotKeyForWPF
{
    public class ComponentInfo
    {
        public ComponentInfo() { }

        public ComponentInfo(double fontsize, SolidColorBrush foreground, SolidColorBrush background, Thickness margin)
        {
            FontSize = fontsize;
            Foreground = foreground;
            Background = background;
            Margin = margin;
        }

        private double _fontsize = 20;

        private double _marginup = 0;
        private double _marginright = 0;
        private double _margindown = 0;
        private double _marginleft = 0;

        public double FontSize
        {
            get { return _fontsize; }
            set
            {
                if (value >= 0)
                {
                    _fontsize = value;
                }
            }
        }

        public SolidColorBrush Foreground = Brushes.Black;

        public SolidColorBrush Background = Brushes.White;

        public Thickness Margin
        {
            get { return new Thickness(_marginup, _marginright, _margindown, _marginleft); }
            set
            {
                if (value.Left < 0 || value.Right < 0 || value.Top < 0 || value.Bottom < 0)
                {
                    return;
                }
                _marginup = value.Left;
                _marginright = value.Top;
                _margindown = value.Right;
                _marginleft = value.Bottom;
            }
        }
    }
}
