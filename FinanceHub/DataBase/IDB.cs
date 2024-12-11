using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceHub.DataBase
{
   public interface IDB
    {
        public void connectForUser(string name);
        public void createDBForUser(string name);

        public void deleteDBForUser(string name);
    }
}
