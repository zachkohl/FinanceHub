using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using FinanceHub.Controllers;

namespace FinanceHub.ViewModel
{
    internal class DeleteUserVM(Users users, resetToInputView callback) : SelectUserBase(users, callback)
    {


        override public void OperateOnUserInner(object obj)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this user? This action cannot be undone and will remove all the user data assocated with it!", "DANGER!", MessageBoxButton.YesNoCancel);
            if (result == MessageBoxResult.Yes)
            {
                _users?.DeleteUser(SelectedUser);
                _callback();
            }

        }
    }
}
