using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceHub.DataBase;
using FinanceHub.Model;

namespace FinanceHub.Services
{
   public class TransactionsService(IDB userDB,IFileSystem fileSystem)
    {
       IDB _db = userDB;
       IFileSystem _fileSystem = fileSystem;
        public List<Transaction> GetAllTransactions()
        {
            return _db.GetAllTransactions();
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
}
