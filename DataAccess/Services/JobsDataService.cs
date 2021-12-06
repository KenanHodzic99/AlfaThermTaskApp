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
    public class JobsDataService : IDataService<Jobs>
    {
        private readonly AlfathermdbContextFactory _contextFactory;

        public JobsDataService(AlfathermdbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<Jobs> Create(Jobs entity)
        {
            using (AlfathermdbContext context = _contextFactory.CreateDbContext())
            {
                var newEntity = entity;
                newEntity.Department = await context.Departments.FirstOrDefaultAsync(x=>x.Id == entity.DepartmentId);
                newEntity.DepartmentId = newEntity.Department.Id;

                var createdEntity = await context.Jobs.AddAsync(newEntity);
                await context.SaveChangesAsync();

                return createdEntity.Entity;
            }
        }

        public async Task<bool> Delete(int id)
        {
            using (AlfathermdbContext context = _contextFactory.CreateDbContext())
            {
                Jobs entity = await context.Jobs.FirstOrDefaultAsync((e) => e.Id == id);
                context.Jobs.Remove(entity);
                await context.SaveChangesAsync();

                return true;
            }
        }

        public async Task<Jobs> Get(int id)
        {
            using (AlfathermdbContext context = _contextFactory.CreateDbContext())
            {
                Jobs entity = await context.Jobs.Include(x=>x.Department).FirstOrDefaultAsync((e) => e.Id == id);


                return entity;
            }
        }

        public async Task<IEnumerable<Jobs>> GetAll(object parameter = null)
        {
            using (AlfathermdbContext context = _contextFactory.CreateDbContext())
            {
                IEnumerable<Jobs> entities = await context.Jobs.Include(x => x.Department).ToListAsync();

                return entities;
            }
        }

        public async Task<Jobs> Update(int id, Jobs entity)
        {
            using (AlfathermdbContext context = _contextFactory.CreateDbContext())
            {
                entity.Id = id;
                context.Jobs.Update(entity);
                await context.SaveChangesAsync();

                return entity;
            }
        }
    }
}
