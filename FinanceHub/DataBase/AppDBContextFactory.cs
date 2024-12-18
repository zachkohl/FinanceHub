using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;

namespace FinanceHub.DataBase
{
    public class AppDBContextFactory : IDesignTimeDbContextFactory<AppDBContext> 
    {
        public AppDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDBContext>();
            optionsBuilder.UseSqlServer("Server=ZACHDELL;Integrated security=SSPI;database=zachThree;TrustServerCertificate=True");

            return new AppDBContext(optionsBuilder.Options);
        }
    }
}
