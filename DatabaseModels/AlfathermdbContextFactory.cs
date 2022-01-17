using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlfaThermTaskApp.DatabaseModels
{
    public class AlfathermdbContextFactory : IDesignTimeDbContextFactory<AlfathermdbContext>
    {
        public AlfathermdbContext CreateDbContext(string[] args = null)
        {
            var options = new DbContextOptionsBuilder<AlfathermdbContext>();
            options.UseSqlServer("Server= MARKO-GALIC\\SQLEXPRESS; Database = AlfaThermDB; User Id = sa; Password = _&cf?mqND4wL;", conf =>
            {
                conf.UseHierarchyId();
            });

            return new AlfathermdbContext(options.Options);
        }
    }
}
