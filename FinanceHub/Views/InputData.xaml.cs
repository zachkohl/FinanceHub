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
    /// Interaction logic for InputData.xaml
    /// </summary>
    public partial class InputData : UserControl
    {
        public Users _users;
        public Grid _InputGrid;
        public InputData(Users users, Grid InputGrid)
        {
            _users = users;
            _InputGrid = InputGrid;
            InitializeComponent();
        }
    }
}
