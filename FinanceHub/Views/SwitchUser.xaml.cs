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
using FinanceHub.Controllers;
using static FinanceHub.MainWindow;

namespace FinanceHub.Views
{
    /// <summary>
    /// Interaction logic for SwitchUser.xaml
    /// </summary>
    public partial class SwitchUser : UserControl
    {
        public Users _users;
        public resetToInputView _callback;
        public SwitchUser(Users users, resetToInputView callback)
        {
            _users = users;
            _callback = callback;
            InitializeComponent();
            List<string> listOfusers = _users.GetUsersNotCurrentUser();

            foreach (string name in listOfusers)
            {
                Button btn = new Button();
                btn.Content = name;
                btn.Tag = name;
                btn.Click += Button_Click;
                StackPanel panel = (StackPanel)LogicalTreeHelper.FindLogicalNode(this, "suStackPanel");
                if (panel == null)
                {
                    MessageBox.Show("panel is null","", MessageBoxButton.OK);
                }
                else {
                    panel.Children.Add(btn);
                }
                
            }
    }

    public void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender != null)
            {
                string? name = (string)((Button)sender).Tag;
                _users.SwitchUser(name);
                _callback();
            }
         
        }

    }
}
