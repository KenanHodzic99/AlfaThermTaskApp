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
            options.UseSqlServer("Data Source=.;Initial Catalog=AlfaThermDB;Integrated Security=True", conf =>
            {
                conf.UseHierarchyId();
            });

            return new AlfathermdbContext(options.Options);
        }
    }
}
