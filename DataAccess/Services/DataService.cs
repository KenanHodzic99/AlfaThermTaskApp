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
    public class DataService<T> : IDataService<T> where T : DomainObject
    {
        private readonly CompanyManagmentAppDbContextFactory _contextFactory;

        public DataService(CompanyManagmentAppDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }


        public async Task<T> Create(T entity)
        {
            using(CompanyManagmentAppDbContext context = _contextFactory.CreateDbContext())
            {
                var createdEntity = await context.Set<T>().AddAsync(entity);
                await context.SaveChangesAsync();

                return createdEntity.Entity;
            }
        }

        public async Task<bool> Delete(int id)
        {
            using (CompanyManagmentAppDbContext context = _contextFactory.CreateDbContext())
            {
                T entity = await context.Set<T>().FirstOrDefaultAsync((e) => e.Id == id);
                context.Set<T>().Remove(entity);
                await context.SaveChangesAsync();

                return true;
            }
        }

        public async Task<T> Get(int id)
        {
            using (CompanyManagmentAppDbContext context = _contextFactory.CreateDbContext())
            {
                T entity = await context.Set<T>().FirstOrDefaultAsync((e) => e.Id == id);


                return entity;
            }
        }

        public async Task<IEnumerable<T>> GetAll(object parameter = null)
        {
            using (CompanyManagmentAppDbContext context = _contextFactory.CreateDbContext())
            {
                IEnumerable<T> entities = await context.Set<T>().ToListAsync();
                
                return entities;
            }
        }

        public async Task<T> Update(int id, T entity)
        {
            using (CompanyManagmentAppDbContext context = _contextFactory.CreateDbContext())
            {
                entity.Id = id;
                context.Set<T>().Update(entity);
                await context.SaveChangesAsync();

                return entity;
            }
        }
    }
}
