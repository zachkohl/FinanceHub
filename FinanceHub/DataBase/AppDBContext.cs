using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileHelpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using FinanceHub.Models;

namespace FinanceHub.DataBase
{
    public class AppDBContext : DbContext
    {



        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }


        public DbSet<Transaction>? Transactions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=ZACHDELL;Integrated security=SSPI;database=zachTwo;TrustServerCertificate=True");
        }

    }



}
