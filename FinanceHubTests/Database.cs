using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Core;
using FinanceHub.Controllers;
using FinanceHub.DataBase;

namespace FinanceHub.Tests
{
    [Collection("Database Tests")]
    public class Database_Tests: IDisposable
    {
        string _testDBname = "test";
        IDB _db;
        public Database_Tests()
        {
            _db = new DBWrapper();
        }

        public void Dispose()
        {
            _db.deleteDBForUser(_testDBname);
        }

        [Fact]
        public void DBWrapper_CreateDBForUser_DoesNotError()
        {
            try
            {
                var db = new DBWrapper();
                db.createDBForUser(_testDBname);//will throw an exception and fail if the test db already exists
            }
            catch
            {
                Assert.Fail();
                return;
            }
            Assert.True(true);
          
        }
      
    }
}
