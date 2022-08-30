using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManager.Domain.Entities;
using UserManager.Infra.Context;
using UserManager.Infra.Interfaces;

namespace UserManager.Infra.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : Base
    {
        private readonly UserManagerContext _context;

        public BaseRepository(UserManagerContext context)
        {
            _context = context;
        }

        public virtual async Task<T> Create(T obj)
        {
            await _context.AddAsync(obj);
            await _context.SaveChangesAsync();
            return obj;
        }
        public virtual async Task<T> Update(T obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return obj;
        }

        public async Task Remove(long id)
        {
            var obj = await GetById(id);
            if(obj is not null)
            {
                _context.Remove(obj);
            }
        }

        public virtual async Task<List<T>> GetAll()
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }

        public virtual async Task<T> GetById(long id)
        {
            return await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(x=>x.Id == id);
        }

    }
}
