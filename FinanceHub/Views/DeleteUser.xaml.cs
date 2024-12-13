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

namespace FinanceHub.Views
{
    /// <summary>
    /// Interaction logic for DeleteUser.xaml
    /// </summary>
    public partial class DeleteUser : UserControl
    {
        public Users _users;
        public resetToInputView _callback;
        public DeleteUser(Users users, resetToInputView callback)
        {
            _users = users;
            _callback = callback;
            InitializeComponent();
            List<string> listOfusers = _users.GetUsersNotCurrentUser();

            foreach (string name in listOfusers)
            {
                Button btn = new Button();
                btn.Content = "Delete user  "+name;
                btn.Tag = name;
                btn.Click += Button_Click;
                StackPanel panel = (StackPanel)LogicalTreeHelper.FindLogicalNode(this, "duStackPanel");
                if (panel == null)
                {
                    MessageBox.Show("panel is null", "", MessageBoxButton.OK);
                }
                else
                {
                    panel.Children.Add(btn);
                }

            }
        }

        public void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult check = MessageBox.Show("Are you sure you want to delete this user, all data WILL BE LOST FOREVER!", "WARNING", MessageBoxButton.YesNoCancel, MessageBoxImage.Exclamation);
            if (check != MessageBoxResult.Yes)
            {
                return;
            }


            string? name = (string)((Button)sender).Tag;
                _users.DeleteUser(name);
                _callback();
            

        }

    }

}
