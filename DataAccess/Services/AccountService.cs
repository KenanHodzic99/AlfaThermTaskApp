using AlfaThermTaskApp.DataAccess.IServices;
using AlfaThermTaskApp.DatabaseModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlfaThermTaskApp.DataAccess.Services
{
    public class AccountService : IAccountService
    {
        private readonly AlfathermdbContextFactory _contextFactory;

        public AccountService(AlfathermdbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<Users> Create(Users entity)
        {
            using (AlfathermdbContext context = _contextFactory.CreateDbContext())
            {

                Users newUser = entity;

                newUser.Employee = await context.Employees.FirstOrDefaultAsync(x=>x.Id == newUser.EmployeeId);
                newUser.EmployeeId = newUser.Employee.Id;
                newUser.Employee.Job = await context.Jobs.FirstOrDefaultAsync(x => x.Id == newUser.Employee.JobId);
                newUser.Employee.JobId = newUser.Employee.Job.Id;
                newUser.Employee.Job.Department = await context.Departments.FirstOrDefaultAsync(x=>x.Id == newUser.Employee.Job.DepartmentId);
                newUser.Employee.Job.DepartmentId = newUser.Employee.Job.Department.Id;

                var createdEntity = await context.Users.AddAsync(newUser);

                await context.SaveChangesAsync();

                return createdEntity.Entity;
            }
        }

        public async Task<bool> Delete(int id)
        {
            using (AlfathermdbContext context = _contextFactory.CreateDbContext())
            {
                Users entity = await context.Users.FirstOrDefaultAsync((e) => e.Id == id);
                context.Users.Remove(entity);
                await context.SaveChangesAsync();

                return true;
            }
        }

        public async Task<Users> Get(int id)
        {
            using (AlfathermdbContext context = _contextFactory.CreateDbContext())
            {
                Users entity = await context.Users.Include(x=>x.Employee).FirstOrDefaultAsync((e) => e.Id == id);

                return entity;
            }
        }

        public async Task<IEnumerable<Users>> GetAll(object parameter = null)
        {
            using (AlfathermdbContext context = _contextFactory.CreateDbContext())
            {
                IEnumerable<Users> entities = await context.Users.Include(x => x.Employee).ToListAsync();

                return entities;
            }
        }

        public async Task<Users> GetByEmployee(Employees employee)
        {
            using (AlfathermdbContext context = _contextFactory.CreateDbContext())
            {
                Users user = await context.Users.FirstOrDefaultAsync(x=>x.EmployeeId==employee.Id);
                return user;
            }
        }

        public async Task<Users> GetByUsername(string username)
        {
            using (AlfathermdbContext context = _contextFactory.CreateDbContext())
            {
                return await context.Users.Include(x => x.Employee).FirstOrDefaultAsync(x => x.Username == username);
            }
        }

        public async Task<Users> Update(int id, Users entity)
        {
            using (AlfathermdbContext context = _contextFactory.CreateDbContext())
            {
                entity.Id = id;
                context.Users.Update(entity);
                await context.SaveChangesAsync();

                return entity;
            }
        }
    }
}
