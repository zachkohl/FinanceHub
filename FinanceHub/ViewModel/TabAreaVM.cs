using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceHub.Controllers;

namespace FinanceHub.ViewModel
{
    internal class TabAreaVM: ViewModelBase
    {
        public Users _users;
		public TabHolder _tabHolder;
        private object _currentView = null!;
        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }

		private object inputDataView = null!;

		public object InputDataView
        {
			get { return inputDataView; }
			set { inputDataView = value; OnPropertyChanged(); }
		}

        private object dataView = null!;

        public object DataView
        {
            get { return dataView; }
            set { dataView = value; OnPropertyChanged(); }
        }

        public TabAreaVM(Users users, TabHolder tabHolder)
		{
			_users = users;
			_tabHolder = tabHolder;
            var currentTab = _tabHolder.GetStartingTab().CurrentTab;
			SelectedTab = currentTab;


            InputDataView = new InputDataAreaVM(_users);
            DataView= new DataTableVM(_users);

        }

        private int selectectTab;

		public int SelectedTab
		{
			get { return selectectTab; }
			set { selectectTab = value;
				_tabHolder.SaveTab(selectectTab);


				OnPropertyChanged(); }
		}



	}
}
