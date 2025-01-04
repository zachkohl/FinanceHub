using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using FinanceHub.Controllers;

namespace FinanceHub.ViewModel
{
    internal class AddUserVM:ViewModelBase
    {
        public Users _users;
        public resetToInputView _callback;
        public string? CandidateName { get; set; }

        public ICommand AddUserCommand { get; set; }

        private void AddUser(object obj)
        {
            bool success = _users.CreateUser(CandidateName);
            if (success == false)
            {
                MessageBox.Show("Name must be made of only letters", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (success == true)
            {
                _callback(); 
            }
        }


        public AddUserVM(Users users, resetToInputView callback)
        {
            _users = users;
            _callback = callback;

            AddUserCommand = new RelayCommand(AddUser);
        }
    }
}
