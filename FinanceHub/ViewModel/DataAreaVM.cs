﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceHub.Services;
using FinanceHub.Model;
using System.Data;
using FinanceHub.View;

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


   

        private object _spendingChartView = null!;
        public object _SpendingChartView
        {
            get { return _spendingChartView; }
            set { _spendingChartView = value; OnPropertyChanged(); }
        }

        private object _catagoryManagerView = null!;
        public object _CatagoryManagerView
        {
            get { return _catagoryManagerView; }
            set { _catagoryManagerView = value; OnPropertyChanged(); }
        }

        public DataAreaVM(TransactionsService transactionsService)
        {
            _transactionsService = transactionsService;
            Transactions = new ObservableCollection<Transaction>(_transactionsService.GetAllTransactions());
            _CatagoryManagerView = new CatagoryManagerVM(_transactionsService);
            _SpendingChartView = new SpendingChartView("passed in title");
        }



    }
}
