using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using FinanceHub.DataBase;
using Microsoft.EntityFrameworkCore;
using FileHelpers;
using FinanceHub.Models;
namespace FinanceHub.Controllers
{
    public class Users(IFileSystem fileSystem, IDB DbWrapper) : RememberState(fileSystem)
    {
        IDB _db = DbWrapper;
        User? _user;

        public User? GetCurrentUser()
        {
            FinanceHubSettings settings = ReadSettings();
            if (settings.CurrentUser == null)
            {
                return null;
            }
            SwitchUser(settings.CurrentUser);
            return _user;
        }

        public bool CreateUser(string name)
        {

            if (!Regex.IsMatch(name, @"^[a-zA-Z]+$"))
            {
                return false;
            }

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

            SwitchUser(name);
            return true;
        }


        public bool SwitchUser(string name)
        {
            FinanceHubSettings settings = ReadSettings();
            if (!settings.Users.Contains<string>(name))
            {
                throw new Exception("cannot switch to user that is not already created");
            }
            settings.CurrentUser = name;
            _db.connectForUser(name);
            WriteSettings(settings);
            _user = new User(_db) { Name = name };
            return true;
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
            if (settings.CurrentUser == null)
            {
                throw new Exception("default user not found in settings.json");
            }
            _db.connectForUser(settings.CurrentUser);
            WriteSettings(settings);

        }


        public List<string> GetUsersNotCurrentUser()
        {
            FinanceHubSettings settings = ReadSettings();
            string? currentUser = settings.CurrentUser;
            if (currentUser == null)
            {
                throw new Exception("cannot read settings to get list of users");
            }

            List<string> list = settings.Users.ToList<string>();
            list.Remove(currentUser);
            return list;
        }

        public bool processFileForActiveUser(string path)
        {

            if (_fileSystem.File.Exists(path) == false)
            {
                throw new Exception("cannot find file");
            }
            var csvString = _fileSystem.File.ReadAllText(path);

            var transactions = CSVReader.Process(csvString);


            _db.saveTransactions(transactions);
            return true;
        }

    }

public class User(IDB db)
    {
       public string? Name { get; set; }
        public IDB db = db;
    }

}
