using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceHub.Services;

namespace FinanceHub.ViewModel
{
    public class CatagoryManagerVM: ViewModelBase
    {

		private ObservableCollection<string> catagories=null!;

		public ObservableCollection<string> Catagories
		{
			get { return catagories; }
			set { catagories = value; OnPropertyChanged(); }
		}

		public CatagoryManagerVM(TransactionsService transactionsService)
		{
			var l = new List<string>(["gas", "groceries", "utilities"]);

            Catagories = new ObservableCollection<string>(l);
		}

	}
}
