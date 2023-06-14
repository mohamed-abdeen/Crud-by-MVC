using Demo.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Reposatory
{
    public class UnitOfWork:IUnitOfWork
    {
        public IEmployeeRepository _EmployeeRepository { get; set; }
        public IdepartmentReposatory _DepartmentReposatory { get; set; }

        public UnitOfWork(IEmployeeRepository EmployeeRepository , IdepartmentReposatory DepartmentReposatory)
        {
            _EmployeeRepository = EmployeeRepository;
            _DepartmentReposatory = DepartmentReposatory;   
        }
    }
}
