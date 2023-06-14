using Demo.BLL.Interfaces;
using Demo.DAL.Context;
using Demo.DAL.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Reposatory
{
    public class DepartmentReposatory :GenaricReposatory<Department>, IdepartmentReposatory
    {
        public DepartmentReposatory(MVCAppDbContext dbContext):base(dbContext)
        {
             
        }
    }
}
