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
    public class WorkCardDataService : IDataService<WorkCard>
    {
        CompanyManagmentAppDbContextFactory _dbContextFactory;

        public WorkCardDataService(CompanyManagmentAppDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<WorkCard> Create(WorkCard entity)
        {
            using(CompanyManagmentAppDbContext _dbContext = _dbContextFactory.CreateDbContext())
            {
                entity.Employee = await _dbContext.Employees.FirstOrDefaultAsync(x=> x.Id == entity.EmployeeId);
                entity.EmployeeId = entity.Employee.Id;

                entity.Project = await _dbContext.Projects.FirstOrDefaultAsync(x => x.Id == entity.ProjectId);
                entity.ProjectId = entity.Project.Id;

                var createdEntity = await _dbContext.WorkCard.AddAsync(entity);
                await _dbContext.SaveChangesAsync();
                return createdEntity.Entity;
            }
        }

        public async Task<bool> Delete(int id)
        {
            using (CompanyManagmentAppDbContext _dbContext = _dbContextFactory.CreateDbContext())
            {
                WorkCard entity = await _dbContext.WorkCard.FirstOrDefaultAsync((e) => e.Id == id);
                _dbContext.WorkCard.Remove(entity);
                await _dbContext.SaveChangesAsync();

                return true;
            }
        }

        public async Task<WorkCard> Get(int id)
        {
            using (CompanyManagmentAppDbContext _dbContext = _dbContextFactory.CreateDbContext())
            {
                return await _dbContext.WorkCard.Include(x=>x.Employee).Include(x=>x.Project).FirstOrDefaultAsync((e) => e.Id == id);
            }
        }

        public async Task<IEnumerable<WorkCard>> GetAll(object parameter = null)
        {
            int employeeId = (int)parameter;
            using (CompanyManagmentAppDbContext _dbContext = _dbContextFactory.CreateDbContext())
            {
                return await _dbContext.WorkCard.Include(x => x.Employee).Include(x => x.Project).Where((e) => e.EmployeeId == employeeId).ToListAsync();
            }
        }

        public async Task<WorkCard> Update(int id, WorkCard entity)
        {
            using (CompanyManagmentAppDbContext _dbContext = _dbContextFactory.CreateDbContext())
            {
                entity.Id = id;
                _dbContext.WorkCard.Update(entity);
                await _dbContext.SaveChangesAsync();

                return entity;
            }
        }
    }
}
