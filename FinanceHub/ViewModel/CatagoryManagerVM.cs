using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using FinanceHub.Services;
using FinanceHub.View;



namespace FinanceHub.ViewModel
{
    public class CatagoryManagerVM: ViewModelBase
    {

		private ObservableCollection<Catagory> catagories=null!;

		public ObservableCollection<Catagory> Catagories
		{
			get { return catagories; }
			set { catagories = value; OnPropertyChanged(); }
		}

        public ICommand PickColorCommand { get; set; }
        private void PickColor(object obj)
        {
       
            ColorPicker colorWindow = new ColorPicker();
            colorWindow.Show();

        }



        public CatagoryManagerVM()
        {
            var l = new List<Catagory> { new Catagory { name = "gas", color = "Blue" }, new Catagory { name = "groceries", color = "Red" }, new Catagory { name = "utilities", color = "Yellow" } };

            Catagories = new ObservableCollection<Catagory>(l);
            PickColorCommand = new RelayCommand(PickColor);
        }

        public CatagoryManagerVM(TransactionsService transactionsService)
        {
            var l = new List<Catagory> { new Catagory { name = "gas", color = "Blue" }, new Catagory { name = "groceries", color = "Red" }, new Catagory { name = "utilities", color = "Yellow" } };

            Catagories = new ObservableCollection<Catagory>(l);
            PickColorCommand = new RelayCommand(PickColor);
        }

	}

  public  class Catagory
    {
        public string name { get; set; } = null!;
        public string color { get; set; } = null!;
    }

}
