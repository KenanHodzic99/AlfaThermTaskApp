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
    public class UserDataService : IDataService<Users>
    {
        private readonly AlfathermdbContextFactory _contextFactory;

        public UserDataService(AlfathermdbContextFactory contextFactory)
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
            using (AlfathermdbContext _dbContext = _contextFactory.CreateDbContext())
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
            using (AlfathermdbContext _dbContext = _contextFactory.CreateDbContext())
            {
                entity.Id = id;
                _dbContext.Users.Update(entity);
                await _dbContext.SaveChangesAsync();

                return entity;
            }
        }
    }
}
