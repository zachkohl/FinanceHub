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
using Microsoft.Win32;

namespace FinanceHub.Views
{
    /// <summary>
    /// Interaction logic for InputDataArea.xaml
    /// </summary>
    public partial class InputDataArea : UserControl
    {
        Users _users;
        public InputDataArea(Users users)
        {
            _users = users;
            InitializeComponent();
        }


public void AddTransactions_USAA_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();

            bool? success = fileDialog.ShowDialog();
            if (success == false)
            {
                return;
            }

             string path = fileDialog.FileName;
              bool processed=  _users.processFileForActiveUser(path);
                if (processed == true)
                {
                MessageBox.Show("File successfully processed!", "success",MessageBoxButton.OK);
                }
            }

        }

}
