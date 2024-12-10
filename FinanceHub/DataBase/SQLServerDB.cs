using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Data.SqlClient;


namespace FinanceHub.DataBase
{
    class SQLServerDB : IDB
    {

        public void connectForUser(string name)
        {
            throw new NotImplementedException();
        }

        private bool ExecuteQuery(string rawSql)
        {
          
            String str;
            SqlConnection myConn = new SqlConnection("Server=ZACHDELL;Integrated security=SSPI;database=master;TrustServerCertificate=True;");


            SqlCommand myCommand = new SqlCommand(rawSql, myConn);
            try
            {
                myConn.Open();
                myCommand.ExecuteNonQuery();
              
                
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
          
          string  str = "CREATE DATABASE "+name+" ON PRIMARY " +
             "(NAME = "+ name+"_Data, " +
             "FILENAME = 'C:\\SQLServerDatabases\\"+name+"Data.mdf', " +
             "SIZE = 2MB, MAXSIZE = 10MB, FILEGROWTH = 10%)" +
             "LOG ON (NAME = "+name+"_Log, " +
             "FILENAME = 'C:\\SQLServerDatabases\\"+name+"Log.ldf', " +
             "SIZE = 1MB, " +
             "MAXSIZE = 5MB, " +
             "FILEGROWTH = 10%)";
            ExecuteQuery(str);


        }

        public void deleteDBForUser(string name)
        {
            ExecuteQuery($"Use master;");
            //ExecuteQuery($"DELAY 00:00:05");

            ExecuteQuery($"DROP DATABASE {name}");
        }

    }
}
