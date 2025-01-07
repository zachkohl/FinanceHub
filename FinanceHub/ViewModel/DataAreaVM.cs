using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceHub.Services;
using FinanceHub.Model;

namespace FinanceHub.ViewModel
{
    internal class DataAreaVM:ViewModelBase
    {
        private TransactionsService _transactionsService;


        private ObservableCollection<Transaction> transactions=null!;

        public ObservableCollection<Transaction> Transactions
        {
            get { return transactions; }
            set { transactions = value; }
        }

        public DataAreaVM(TransactionsService transactionsService)
        {
            _transactionsService = transactionsService;
            Transactions = new ObservableCollection<Transaction>(_transactionsService.GetAllTransactions());
        }



    }
}
