using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Core;
using FinanceHub.Controllers;
using FinanceHub.DataBase;
using FinanceHub.Models;

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

        [Fact]
        public void DBWrapper_SaveTransactions_LoadsDataIntoDB()
        {
            Transaction myTransaction = new Transaction { Date = new DateOnly(2024, 12, 12), Amount = -3.5m, Description = "SQ *TERRE COFFEE & B             121224", OriginalDescription = "SQ *TERRE COFFEE & B             121224", BankCatagory = "Category Pending", BankStatus = "Pending" };
            List<Transaction> mockList = new List<Transaction>() { myTransaction };
            var db = new DBWrapper();
            db.createDBForUser(_testDBname);

            db.saveTransactions(mockList);

            var transactions = db.GetAllTransactions();
            Assert.Equal(mockList[0].Amount, transactions[0].Amount);

        }
      
    }
}
