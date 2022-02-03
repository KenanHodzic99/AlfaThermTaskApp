using CompanyManagmentApp.DataAccess.IServices;
using CompanyManagmentApp.DatabaseModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagmentApp.DataAccess.Services
{
    public class PermissionDataService : IDataService<Permission>
    {
        CompanyManagmentAppDbContextFactory alfathermdbContextFactory;

        public PermissionDataService(CompanyManagmentAppDbContextFactory alfathermdbContextFactory)
        {
            this.alfathermdbContextFactory = alfathermdbContextFactory;
        }

       
        public async Task<Permission> Create(Permission entity)
        {
           using(CompanyManagmentAppDbContext context = alfathermdbContextFactory.CreateDbContext())
            {
                var newPermission = entity;
                newPermission.User = await context.Users.FirstOrDefaultAsync(x=>x.Id == newPermission.UserId);
                newPermission.UserId = newPermission.User.Id;
                newPermission.User.Employee = await context.Employees.FirstOrDefaultAsync(x => x.Id == newPermission.User.EmployeeId);
                newPermission.User.EmployeeId = newPermission.User.Employee.Id;
                newPermission.User.Employee.Job = await context.Jobs.FirstOrDefaultAsync(x => x.Id == newPermission.User.Employee.JobId);
                newPermission.User.Employee.JobId = newPermission.User.Employee.Job.Id;
                newPermission.User.Employee.Job.Department = await context.Departments.FirstOrDefaultAsync(x => x.Id == newPermission.User.Employee.Job.DepartmentId);
                newPermission.User.Employee.Job.DepartmentId = newPermission.User.Employee.Job.Department.Id;

                context.Permission.Add(newPermission);

                await context.SaveChangesAsync();
                return newPermission;
            }
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Permission> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Permission>> GetAll(object parameter = null)
        {
            throw new NotImplementedException();
        }

        public Task<Permission> Update(int id, Permission entity)
        {
            throw new NotImplementedException();
        }
    }
}
