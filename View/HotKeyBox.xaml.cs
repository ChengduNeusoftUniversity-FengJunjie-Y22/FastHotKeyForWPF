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
            UpdateText();
            KeyHelper.KeyParse(this, e);
            e.Handled = true;
        }
    }
}
