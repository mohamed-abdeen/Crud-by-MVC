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
    public class GenaricReposatory<T> : IGenaricReposatory<T> where T : class
    {
        private readonly MVCAppDbContext _DbContext;
        public GenaricReposatory(MVCAppDbContext dbContext)
        {
            _DbContext = dbContext;
        }
        public async Task<int> Add(T item)
        {
            await _DbContext.Set<T>().AddAsync(item);
            return await _DbContext.SaveChangesAsync();
        }

        public async Task<int> Delete(T item)
        {
            _DbContext.Set<T>().Remove(item);
            return await _DbContext.SaveChangesAsync();
        }

        public async Task< T> Get(int id)

          //  return _DbContext.Set<T>().Where(d => d.Id == id).FirstOrDefault();
          => await _DbContext.Set<T>().FindAsync(id);


        public async Task< IEnumerable<T> >GetAll()

           => await _DbContext.Set<T>().ToListAsync();


        public async Task< int> Update(T item)
        {
            _DbContext.Set<T>().Update(item);
            return await _DbContext.SaveChangesAsync();
        }
    }
}
