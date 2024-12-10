using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceHub.DataBase
{
    interface IDB
    {
        void connectForUser(string name);
        void createDBForUser(string name);

        void deleteDBForUser(string name);
    }
}
