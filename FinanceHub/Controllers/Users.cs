using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceHub.DataBase;
using Microsoft.EntityFrameworkCore;

namespace FinanceHub.Controllers
{
    internal class Users : RememberState
    {
        IDB _db;

        public Users(IFileSystem fileSystem,IDB DbWrapper) : base(fileSystem) 
        {
            _db =  DbWrapper;
        }
        public User? GetCurrentUser()
        {
            FinanceHubSettings settings = ReadSettings();
            if (settings.CurrentUser == null)
            {
                return null;
            }
            return new User(_db) { Name = settings.CurrentUser };
        }

        public User CreateUser(string name)
        {
            FinanceHubSettings settings = ReadSettings();
            if (settings.Users == null)
            {
                throw new Exception("Users list missing in json");
            }

            if (settings.Users.Contains<string>(name))
            {
                throw new Exception("this user is already in the settings as existing.");
            }
            List<string> list = settings.Users.ToList<string>();
            list.Add(name);
            settings.Users = list.ToArray();
            _db.createDBForUser(name);
            WriteSettings(settings);

            User myUser = new User(_db) { Name = name };
            return myUser;
        }


        public User SwitchUser(string name)
        {
            FinanceHubSettings settings = ReadSettings();
            if (!settings.Users.Contains<string>(name))
            {
                throw new Exception("cannot switch to user that is not already created");
            }
            settings.CurrentUser = name;
            _db.connectForUser(name);
            WriteSettings(settings);

            return new User(_db);
        }

        public void DeleteUser(string name)
        {
            FinanceHubSettings settings = ReadSettings();
            if (settings.CurrentUser==name)
            {
                throw new Exception("cannot remove the current user. Please switch to another user and then attempt again");
            }
            List<string> list= settings.Users.ToList<string>();
          bool success=  list.Remove(name);
            if (!success)
            {
                throw new Exception("Something went wrong with removing user from the list");
            }
            settings.Users = list.ToArray();
            _db.deleteDBForUser(name);
            WriteSettings(settings);

        }

    }

class User(IDB db)
    {
       public string? Name { get; set; }
        public IDB db = db;
    }

}
