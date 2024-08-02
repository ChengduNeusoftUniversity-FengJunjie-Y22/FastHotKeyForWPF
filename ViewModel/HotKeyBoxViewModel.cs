using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;

namespace FastHotKeyForWPF
{
    /// <summary>
    /// HotKeyBox的 ViewModel
    /// </summary>
    public class HotKeyBoxViewModel : HotKeyViewModelBase
    {
        public HotKeyBoxViewModel()
        {
            _model = new HotKeyBoxModel();
        }

        /// <summary>
        /// 圆滑度
        /// </summary>
        public CornerRadius CornerRadius
        {
            get => _model.CornerRadius;
            set
            {
                if (value != _model.CornerRadius)
                {
                    _model.CornerRadius = value;
                    OnPropertyChanged(nameof(CornerRadius));
                }
            }
        }

        /// <summary>
        /// 真实背景色
        /// </summary>
        public SolidColorBrush ActualBackground
        {
            get => _model.ActualBackground;
            set
            {
                if (_model.ActualBackground != value)
                {
                    _model.ActualBackground = value;
                    OnPropertyChanged(nameof(ActualBackground));
                }
            }
        }

        /// <summary>
        /// 默认文字颜色
        /// </summary>
        public SolidColorBrush DefaultTextColor
        {
            get => _model.DefaultTextColor;
            set
            {
                if (value != _model.DefaultTextColor)
                {
                    _model.DefaultTextColor = value;
                    OnPropertyChanged(nameof(DefaultTextColor));
                }
            }
        }

        /// <summary>
        /// 默认边框颜色
        /// </summary>
        public SolidColorBrush DefaultBorderBrush
        {
            get => _model.DefaultBorderBrush;
            set
            {
                if (value != _model.DefaultBorderBrush)
                {
                    _model.DefaultBorderBrush = value;
                    OnPropertyChanged(nameof(DefaultBorderBrush));
                }
            }
        }

        public Thickness DefaultBorderThickness
        {
            get => _model.DefaultBorderThickness;
            set
            {
                if (value != _model.DefaultBorderThickness)
                {
                    _model.DefaultBorderThickness = value;
                    OnPropertyChanged(nameof(DefaultBorderThickness));
                }
            }
        }

        /// <summary>
        /// 悬停文字颜色
        /// </summary>
        public SolidColorBrush HoverTextColor
        {
            get => _model.HoverTextColor;
            set
            {
                if (value != _model.HoverTextColor)
                {
                    _model.HoverTextColor = value;
                    OnPropertyChanged(nameof(HoverTextColor));
                }
            }
        }

        /// <summary>
        /// 悬停边框颜色
        /// </summary>
        public SolidColorBrush HoverBorderBrush
        {
            get => _model.HoverBorderBrush;
            set
            {
                if (value != _model.HoverBorderBrush)
                {
                    _model.HoverBorderBrush = value;
                    OnPropertyChanged(nameof(HoverBorderBrush));
                }
            }
        }
    }
}
