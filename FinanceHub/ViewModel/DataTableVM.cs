using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceHub.Controllers;
using FinanceHub.Model;

namespace FinanceHub.ViewModel
{
    internal class DataTableVM:ViewModelBase
    {
        private Users _users;


        private ObservableCollection<Transaction> transactions=null!;

        public ObservableCollection<Transaction> Transactions
        {
            get { return transactions; }
            set { transactions = value; }
        }

        public DataTableVM(Users users)
        {
            _users = users;
            Transactions = new ObservableCollection<Transaction>(_users.GetAllTransactions());
        }



    }
}
