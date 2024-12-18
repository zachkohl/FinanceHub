using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using FinanceHub.Models;

namespace FinanceHub.DataBase
{
  internal  class SQLServerDB : IDB
    {

        internal AppDBContext _EF;

        internal SQLServerDB()
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDBContext>();
            var connectionString = "Server=ZACHDELL;Integrated security=SSPI;database=master;TrustServerCertificate=True;";
            optionsBuilder.UseSqlServer(connectionString);
            optionsBuilder.ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning));
            _EF = new AppDBContext(optionsBuilder.Options);
        }

        public  void connectForUser(string name)
        {
        
            var optionsBuilder = new DbContextOptionsBuilder<AppDBContext>();
            var connectionString = string.Format("Server=ZACHDELL;Integrated security=SSPI;database={0};TrustServerCertificate=True;",name);
            optionsBuilder.UseSqlServer(connectionString);
            optionsBuilder.ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning));
            _EF = new AppDBContext(optionsBuilder.Options);
            
        }

        private async Task<bool> ExecuteQuery(string rawSql)
        {
          
            SqlConnection myConn = new SqlConnection("Server=ZACHDELL;Integrated security=SSPI;database=master;TrustServerCertificate=True;");
    


            SqlCommand myCommand = new SqlCommand(rawSql, myConn);
            try
            {
                myConn.Open();
                await myCommand.ExecuteNonQueryAsync();

            }
            catch (System.Exception ex)
            {
                //throw new Exception(ex.ToString());
                MessageBox.Show(ex.ToString(), "MyProgram", MessageBoxButton.OK);

            }
            finally
            {
                if (myConn.State == System.Data.ConnectionState.Open)
                {
                    myConn.Close();
                   
                }
               
            }

            return true;
        }

        public void createDBForUser(string name)
        {


            var optionsBuilder = new DbContextOptionsBuilder<AppDBContext>();
            var connectionString = string.Format("Server=ZACHDELL;Integrated security=SSPI;database={0};TrustServerCertificate=True;", name);
            optionsBuilder.UseSqlServer(connectionString);
            optionsBuilder.ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning));
            _EF = new AppDBContext(optionsBuilder.Options);


        }

        public async void deleteDBForUser(string name)
        {
              connectForUser("master");
          await ExecuteQuery($"Use master;");

          await ExecuteQuery($"DROP DATABASE IF EXISTS {name}");
        }

        public void saveTransactions(List<Transaction> transactions)
        {
            _EF.Transactions?.AddRange(transactions);
            _EF.SaveChanges();
        }

        public List<Transaction> GetAllTransactions()
        {
           return _EF.Transactions!.ToList();
        }
    }
}
