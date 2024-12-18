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
    /// Interaction logic for DataTabView.xaml
    /// </summary>
    public partial class DataTabView : UserControl
    {
        public Users _users;

        public DataTabView(Users users)
        {
       
            _users = users;
            InitializeComponent();
        
            DataGridTransactions.ItemsSource = _users.GetAllTransactions().Select(e=> new {e.Date,e.Amount,e.Description});
        }
    }
}
