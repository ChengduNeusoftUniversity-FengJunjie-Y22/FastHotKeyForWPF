using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using FastHotKeyForWPF;

namespace Sample
{
    /// <summary>
    /// 基于抽象基类设计 ViewModel 层 , 要求使用统一的 HotKeyBoxModel 作为 Model ( 已在基类中定义_model )
    /// </summary>
    public class MyHotKeyBoxViewModelA : HotKeyViewModelBase
    {
        public MyHotKeyBoxViewModelA()
        {
            _model = new HotKeyBoxModel();
        }

        private SolidColorBrush _fixedtransparent = Brushes.Transparent;
        public SolidColorBrush FixedTransparent
        {
            get => _fixedtransparent;
            set
            {
                if (_fixedtransparent != value)
                {
                    OnPropertyChanged(nameof(FixedTransparent));
                }
            }
        }
        //…
        //拓展属性,它们不存在于Model,只负责逻辑数据的处理
        //例如,将【UserControl的Background】与【FixedTransparent】做【TwoWay绑定】,即可永远保持为透明


        //…
        //重写属性,多数情况并不需要这一步


        public override void UpdateText()
        {
            string? PossibilityA = (CurrentKeyA == Key.LeftCtrl || CurrentKeyA == Key.RightCtrl) ? "CTRL" : null;
            string? PossibilityB = (CurrentKeyA == Key.LeftAlt || CurrentKeyA == Key.RightAlt) ? "ALT" : null;

            string Left = (PossibilityA == null ? string.Empty : PossibilityA) + (PossibilityB == null ? string.Empty : PossibilityB);

            Text = Left + " + " + CurrentKeyB.ToString();
        }
        //…
        //重写方法
        //例如,您希望不再区分 CTRL/ALT的左右,那么您可以重写UpdateText()
    }
}
