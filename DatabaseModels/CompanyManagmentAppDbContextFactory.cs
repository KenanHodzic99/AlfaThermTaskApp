using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagmentApp.DatabaseModels
{
    public class CompanyManagmentAppDbContextFactory : IDesignTimeDbContextFactory<CompanyManagmentAppDbContext>
    {
        public CompanyManagmentAppDbContext CreateDbContext(string[] args = null)
        {
            var options = new DbContextOptionsBuilder<CompanyManagmentAppDbContext>();
            options.UseSqlServer("Data Source=.;Initial Catalog=AlfaThermDB;Integrated Security=True", conf =>
            {
                conf.UseHierarchyId();
            });

            return new CompanyManagmentAppDbContext(options.Options);
        }
    }
}
