using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceHub.Services;
using System.Windows.Input;
using System.Windows;

namespace FinanceHub.ViewModel
{
  public  class SelectUserBase : ViewModelBase
    {
        public UsersService? _users;
        public resetToInputView _callback;
        public SelectUserBase(UsersService users, resetToInputView callback)
        {
            _users = users;
            _callback = callback;
            collectionOfUsers = new ObservableCollection<string>();
            List<string> listOfusers = _users.GetUsersNotCurrentUser();
            foreach (string name in listOfusers)
            {
                CollectionOfUsers.Add(name);
            }
            OperateOnUserCommand = new RelayCommand(OperateOnUserInner);
        }


        private ObservableCollection<string> collectionOfUsers;

        public ObservableCollection<string> CollectionOfUsers
        {
            get { return collectionOfUsers; }
            set
            {
                collectionOfUsers = value;
                OnPropertyChanged();
            }
        }

        private string? selectedUser;

        public string SelectedUser
        {
            get { return selectedUser!; }
            set
            {
                selectedUser = value;
                BtnEnable = selectedUser != null;
                OnPropertyChanged();
            }
        }


        private bool btnEnable;

        public bool BtnEnable
        {
            get { return btnEnable; }
            set
            {
                btnEnable = value;
                OnPropertyChanged();
            }
        }



        public ICommand OperateOnUserCommand { get; set; }

        virtual public void OperateOnUserInner(object obj)
        {
            throw new Exception("Operate on user needs to be implemented");
        }
}
}
