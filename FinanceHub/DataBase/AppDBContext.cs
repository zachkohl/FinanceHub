using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FinanceHub.DataBase
{
    public class AppDBContext : DbContext
    {
  
   

        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }

        public DbSet<Transactions>? Transactions { get; set; }

    }


    public class Transactions
    {
        public Guid id { get; set; }
        public string? date { get; set; }
        public decimal amount { get; set; }
    }

}
