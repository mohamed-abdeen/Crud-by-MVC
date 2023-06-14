using Demo.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Interfaces
{
    public interface IEmployeeRepository:IGenaricReposatory<Employee>
    {
        IQueryable<Employee> GetEmployeesByDepartmentName(string departmentName);

        IQueryable<Employee> GetEmployeeByName(string name);
    }
}
