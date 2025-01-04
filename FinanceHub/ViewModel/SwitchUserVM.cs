using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using FinanceHub.Controllers;
using FinanceHub.Model;
using FinanceHub.View;

namespace FinanceHub.ViewModel
{
    public class SwitchUserVM(Users users, resetToInputView callback) : SelectUserBase(users,callback)
    {
      

      override public void OperateOnUserInner(object obj)
        {
            _users?.SwitchUser(SelectedUser);
            _callback();
        }
    }
}
