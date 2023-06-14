using Demo.DAL.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Context
{
    public class MVCAppDbContext :IdentityDbContext<APPUser>
    {
        public MVCAppDbContext(DbContextOptions<MVCAppDbContext> options) : base(options)
        {

        }
        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //=> optionsBuilder.UseSqlServer("server=. ; database=MVCAppDbContext ; trusted_connection=true ;");
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        //public DbSet<APPUser> Users { get; set; }
    }
}
