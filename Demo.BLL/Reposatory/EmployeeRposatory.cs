using Demo.BLL.Interfaces;
using Demo.DAL.Context;
using Demo.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Reposatory
{
    public class EmployeeReposatory : GenaricReposatory<Employee>, IEmployeeRepository
    {
        private readonly MVCAppDbContext _dbContext;

        public EmployeeReposatory(MVCAppDbContext dbContext):base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Employee> GetEmployeeByName(string name)
        
            =>  _dbContext.Employees.Where(E => E.Name.Contains(name));
        

        public IQueryable<Employee> GetEmployeesByDepartmentName(string departmentName)
        {
            
            throw new NotImplementedException();
        }
    }
}
