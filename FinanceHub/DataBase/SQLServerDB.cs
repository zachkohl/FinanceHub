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

        public void connectForUser(string name)
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

        public async void createDBForUser(string name)
        {
          
          string  str = "CREATE DATABASE "+name+" ON PRIMARY " +
             "(NAME = "+ name+"_Data, " +
             "FILENAME = 'C:\\SQLServerDatabases\\"+name+"Data.mdf', " +
             "SIZE = 2MB, MAXSIZE = 10MB, FILEGROWTH = 10%)" +
             "LOG ON (NAME = "+name+"_Log, " +
             "FILENAME = 'C:\\SQLServerDatabases\\"+name+"Log.ldf', " +
             "SIZE = 1MB, " +
             "MAXSIZE = 5MB, " +
             "FILEGROWTH = 10%)";
          await  ExecuteQuery(str);
            connectForUser(name);
            _EF.Database.Migrate();

        }

        public async void deleteDBForUser(string name)
        {
              connectForUser("master");
          await ExecuteQuery($"Use master;");
            //ExecuteQuery($"DELAY 00:00:05");

          await ExecuteQuery($"DROP DATABASE IF EXISTS {name}");
        }

    }
}
