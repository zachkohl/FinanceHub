using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceHub.DataBase
{

   public class DBWrapper : IDB 
    {
        readonly IDB _db;

      public  DBWrapper()
        {
            this._db = new SQLServerDB();
        }

        public void connectForUser(string name)
        {
            _db.connectForUser(name);
        }

        public void createDBForUser(string name)
        {
            _db.createDBForUser(name);
        }

        public void deleteDBForUser(string name)
        {
            _db.deleteDBForUser(name);
        }
    }
}
