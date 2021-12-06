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
    public class EmployeeDataService : IDataService<Employees>
    {
        private readonly AlfathermdbContextFactory _contextFactory;

        public EmployeeDataService(AlfathermdbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }


        public async Task<Employees> Create(Employees entity)
        {
            using (AlfathermdbContext context = _contextFactory.CreateDbContext())
            {
                var Job = await context.Jobs.Include(x=>x.Department).FirstOrDefaultAsync(x => x.Id == entity.JobId);

                var newEntity = entity;
                newEntity.Job = Job;
                newEntity.JobId = Job.Id;

                newEntity.Job.Department = Job.Department;
                newEntity.Job.DepartmentId = Job.DepartmentId;

                var createdEntity = await context.Employees.AddAsync(entity);
                await context.SaveChangesAsync();

                return createdEntity.Entity;
            }
        }

        public async Task<bool> Delete(int id)
        {
            using (AlfathermdbContext context = _contextFactory.CreateDbContext())
            {
                var userToDelete = await context.Users.FirstOrDefaultAsync((e)=>e.EmployeeId == id);
                var permissionsToDelete = await context.Permission.Where((e) => e.UserId == userToDelete.Id).ToListAsync();
                Employees entity = await context.Employees.FirstOrDefaultAsync((e) => e.Id == id);

                context.Permission.RemoveRange(permissionsToDelete);
                context.Users.Remove(userToDelete);
                context.Employees.Remove(entity);
                await context.SaveChangesAsync();

                return true;
            }
        }

        public async Task<Employees> Get(int id)
        {
            using (AlfathermdbContext context = _contextFactory.CreateDbContext())
            {
                Employees entity = await context.Employees.Include(x=>x.Job).Include(x=>x.Job.Department).FirstOrDefaultAsync((e) => e.Id == id);


                return entity;
            }
        }

        public async Task<IEnumerable<Employees>> GetAll(object parameter = null)
        {
            using (AlfathermdbContext context = _contextFactory.CreateDbContext())
            {
                string searchParameter = (string)parameter;
                if (searchParameter == string.Empty)
                {
                    IEnumerable<Employees> entities = await context.Employees.Include(x => x.Job).Include(x => x.Job.Department).ToListAsync();
                    return entities;
                }
                else
                {
                    IEnumerable<Employees> entities = await context.Employees.Include(x => x.Job).Include(x => x.Job.Department)
                        .Where(x=> (x.FirstName + " " + x.LastName).Contains(searchParameter)).ToListAsync();
                    return entities;
                }
            }
        }

        public async Task<Employees> Update(int id, Employees entity)
        {
            using (AlfathermdbContext context = _contextFactory.CreateDbContext())
            {
                entity.Id = id;
                context.Employees.Update(entity);
                await context.SaveChangesAsync();

                return entity;
            }
        }
    }
}
