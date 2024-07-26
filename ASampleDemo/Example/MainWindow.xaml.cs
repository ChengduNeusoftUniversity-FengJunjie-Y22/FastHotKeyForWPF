using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FastHotKeyForWPF;

namespace Example
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void HandlerA(object sender, HotKeyEventArgs e)
        {
            int ID = e.RegisterInfo.RegisterID;
            //此处可获取热键的具体信息

            MessageBox.Show($"HotKeyA Has Been Invoked Whose ID is {ID}");
        }

        private void HandlerB(object sender, HotKeyEventArgs e)
        {
            int ID = e.RegisterInfo.RegisterID;

            MessageBox.Show($"HotKeyB Has Been Invoked Whose ID is {ID}");
        }
    }
}