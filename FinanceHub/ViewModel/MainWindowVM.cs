using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FinanceHub.Services;
using FinanceHub.DataBase;
using FinanceHub.View;
using System.IO;
using System.IO.Abstractions;


namespace FinanceHub.ViewModel
{
    public class MainWindowVM : ViewModelBase
    {
        private object _currentView=null!;
        internal UsersService _users;
        internal TabHolderService _tabHolder;
        internal resetToInputView _resetInputView;

        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }

        private bool activeMenue;

        public bool ActiveMenue
        {
            get { return activeMenue; }
            set { activeMenue = value; OnPropertyChanged(); }
        }


        public ICommand SwitchUserCommand { get; set; }
        public ICommand AddUserCommand { get; set; }
        public ICommand DeleteUserCommand { get; set; }
        public ICommand BackToAppCommand { get; set; }


        private void SwitchUser(object obj) => CurrentView = new SwitchUserVM(_users, _resetInputView);
        private void AddUser(object obj) => CurrentView = new AddUserVM(_users, _resetInputView);
        private void DeleteUser(object obj) => CurrentView = new DeleteUserVM(_users, _resetInputView);
        private void BackToApp(object obj) => CurrentView = new TabAreaVM(_users, _tabHolder);


        public MainWindowVM()
        {
            _users = new UsersService(new FileSystem(), new DBWrapper());
            _tabHolder = new TabHolderService(new FileSystem());
            _users.GetCurrentUser();
            _resetInputView = () =>
            {
                User? currentUser = _users.GetCurrentUser();
                if (currentUser == null)
                {
                    ActiveMenue = false;
                }
                else
                {
                    ActiveMenue = true;
                }
                CurrentView = new TabAreaVM(_users, _tabHolder);
            };



            SwitchUserCommand = new RelayCommand(SwitchUser);
            AddUserCommand = new RelayCommand(AddUser);
            DeleteUserCommand = new RelayCommand(DeleteUser);
            BackToAppCommand = new RelayCommand(BackToApp);

            User? currentUser = _users.GetCurrentUser();
            if (currentUser == null)
            {
                CurrentView = new AddUserVM(_users, _resetInputView);
                ActiveMenue = false;
            }
            else
            {
                CurrentView = new TabAreaVM(_users, _tabHolder);
                ActiveMenue = true;
            }




        }
   
    }

    public delegate void resetToInputView();
}
