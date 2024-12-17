using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;
using FinanceHub.DataBase;
using FinanceHub.Models;

namespace FinanceHub.Controllers
{
    public  class CSVReader()
    {
         public static List<Transaction> Process(string csv)
        {

            string[] rows = csv.Split('\n');
            List<Transaction> transactions = new List<Transaction>();
            Console.WriteLine(rows);
            for(int i=1;i<rows.Length;i++)
            {
                rows[i].Replace("\",\"", "");
                string[] cells = rows[i].Split(',');
                if (cells.Length < 2)
                {
                    continue;
                }
                Transaction temp = new Transaction();
                temp.Id = new Guid();
                temp.Date = DateOnly.FromDateTime(DateTime.Parse(cells[0]));
                temp.Description = cells[1];
                temp.OriginalDescription = cells[2];
                temp.BankCatagory = cells[3];
                temp.Amount = decimal.Parse(cells[4]);
                temp.BankStatus = cells[5].Trim();
                transactions.Add(temp);
            }
            return transactions;
        }
    }
}
