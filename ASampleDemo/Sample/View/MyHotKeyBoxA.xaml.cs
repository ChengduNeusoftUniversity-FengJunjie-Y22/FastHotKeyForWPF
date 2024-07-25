using FastHotKeyForWPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Sample
{
    public partial class MyHotKeyBoxA : UserControl, IAutoHotKeyProperty
    {
        public MyHotKeyBoxA()
        {
            InitializeComponent();

            BoxPool.Add(this, ViewModel);
            //必须执行这句话才可以参与库提供的唯一热键的实现流程
        }


        #region 接口实现

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

        #endregion


        #region 依赖属性定义

        public static readonly DependencyProperty CurrentKeyAProperty =
            DependencyProperty.Register(nameof(CurrentKeyA), typeof(Key), typeof(MyHotKeyBoxA), new PropertyMetadata(Key.None, OnKeyAChanged));
        public static readonly DependencyProperty CurrentKeyBProperty =
            DependencyProperty.Register(nameof(CurrentKeyB), typeof(Key), typeof(MyHotKeyBoxA), new PropertyMetadata(Key.None, OnKeyBChanged));
        public static readonly DependencyProperty HandlerProperty =
            DependencyProperty.Register(nameof(Handler), typeof(HotKeyEventHandler), typeof(MyHotKeyBoxA), new PropertyMetadata(null, OnHandlerChanged));

        private static void OnKeyAChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var target = (MyHotKeyBoxA)d;
            target.ViewModel.CurrentKeyA = (Key)e.NewValue;
        }
        private static void OnKeyBChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var target = (MyHotKeyBoxA)d;
            target.ViewModel.CurrentKeyB = (Key)e.NewValue;
        }
        private static void OnHandlerChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var target = (MyHotKeyBoxA)d;
            target.HandlerData = (HotKeyEventHandler)e.NewValue;
            target.ViewModel.HandlerData = (HotKeyEventHandler)e.NewValue;
        }

        #endregion


        #region 事件

        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            KeyGet.Focus();
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            Keyboard.ClearFocus();
        }

        private void KeyGet_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            ViewModel.UpdateText();

            Key key = (e.Key == Key.System ? e.SystemKey : e.Key);

            var result = KeyHelper.IsKeyValid(key);
            //KeyHelper提供Key合法性检查,以确保是一个受库支持的Key
            if (result.Item1)
            {
                if (result.Item2 == KeyTypes.ModelKey)
                {
                    CurrentKeyA = key;
                    //注意这里应该向依赖属性通知更改,直接通知ViewModel会导致BoxPool功能异常
                }
                else if (result.Item2 == KeyTypes.NormalKey)
                {
                    CurrentKeyB = key;
                }
            }

            e.Handled = true;
        }

        #endregion
    }
}
