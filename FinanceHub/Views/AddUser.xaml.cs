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
using FinanceHub;

namespace FinanceHub.Views
{
    /// <summary>
    /// Interaction logic for AddUser.xaml
    /// </summary>
    public partial class AddUser : UserControl
    {
        public Users _users;
        public Grid _InputGrid;
        public AddUser(Users users, Grid InputGrid)
        {
            _users = users;
            _InputGrid = InputGrid;
            InitializeComponent();
        }

        void OnSubmitCandidateNameClick(object sender, RoutedEventArgs e) 
        {
          bool success = _users.CreateUser(CandidateName.Text);
            if (success == false)
            {
                MessageBox.Show("Name must be made of only letters", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (success == true)
            {
               

                _InputGrid.Children.Clear();
                InputData MyAddUser = new InputData(_users, _InputGrid);
                _InputGrid.Children.Add(MyAddUser);

            }

        }
    }
}
