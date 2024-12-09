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
using FinanceHub.Controllers;
using System.IO;
using System.IO.Abstractions;

namespace FinanceHub
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var myTabHolder = new TabHolder(new FileSystem());
            FinanceHubSettings settings = myTabHolder.GetStartingTab();
            Dispatcher.BeginInvoke((Action)(() => MyTabControl.SelectedIndex = settings.CurrentTab));


           

        }

       private void OnTabSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int current = ((TabControl)sender).SelectedIndex;
            var myTabHolder = new TabHolder(new FileSystem());
            myTabHolder.SaveTab(current);
        }
    }
}