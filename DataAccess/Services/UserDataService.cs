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
    public class UserDataService : IDataService<Users>
    {
        private readonly CompanyManagmentAppDbContextFactory _contextFactory;

        public UserDataService(CompanyManagmentAppDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<Users> Create(Users entity)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Users> Get(int id)
        {
            using (CompanyManagmentAppDbContext _dbContext = _contextFactory.CreateDbContext())
            {
                return await _dbContext.Users.Include(x => x.Employee)
                    .Include(x => x.Employee.Job)
                    .Include(x => x.Employee.Job.Department)
                    .FirstOrDefaultAsync(x => x.Id == id);
            }
        }

        public async Task<IEnumerable<Users>> GetAll(object parameter = null)
        {
            throw new NotImplementedException();
        }

        public async Task<Users> Update(int id, Users entity)
        {
            using (CompanyManagmentAppDbContext _dbContext = _contextFactory.CreateDbContext())
            {
                entity.Id = id;
                _dbContext.Users.Update(entity);
                await _dbContext.SaveChangesAsync();

                return entity;
            }
        }
    }
}
