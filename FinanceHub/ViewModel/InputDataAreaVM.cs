using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using FinanceHub.Services;
using Microsoft.Win32;

namespace FinanceHub.ViewModel
{
    public class InputDataAreaVM:ViewModelBase
    {
       private TransactionsService _transactionsService;
       public InputDataAreaVM(TransactionsService transactionsService)
        {
            _transactionsService = transactionsService;
            AddTransactionsCommand = new RelayCommand(AddTransactions);
        }


        public ICommand AddTransactionsCommand { get; set; }

        private void AddTransactions(object obj)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();

            bool? success = fileDialog.ShowDialog();
            if (success == false)
            {
                return;
            }

            string path = fileDialog.FileName;
            bool processed = _transactionsService.processFileForActiveUser(path);
            if (processed == true)
            {
                MessageBox.Show("File successfully processed!", "success", MessageBoxButton.OK);
            }
    }



    }
}
