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

namespace Sample
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

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            GlobalHotKey.Awake();
        }

        protected override void OnClosed(EventArgs e)
        {
            GlobalHotKey.Destroy();

            base.OnClosed(e);
        }

        private void HandlerA(object sender, HotKeyEventArgs e)
        {
            int ID = e.RegisterInfo.RegisterID;

            MessageBox.Show($"HotKeyA Has Been Invoked Whose ID Is {ID}");
        }

        private void HandlerB(object sender, HotKeyEventArgs e)
        {
            int ID = e.RegisterInfo.RegisterID;

            MessageBox.Show($"HotKeyB Has Been Invoked Whose ID Is {ID}");
        }
    }
}