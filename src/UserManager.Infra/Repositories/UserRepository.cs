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
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly UserManagerContext _context;
        public UserRepository(UserManagerContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User> GetByEmail(string email)
        {
            return await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Email.ToLower() == email.ToLower());
        }

        public async Task<List<User>> SearchByEmail(string email)
        {
            return await _context.Users.AsNoTracking().Where(x => x.Email.ToLower().Contains(email.ToLower())).ToListAsync();
        }

        public async Task<List<User>> SearchByName(string name)
        {
            return await _context.Users.AsNoTracking().Where(x => x.Name.ToLower().Contains(name.ToLower())).ToListAsync();
        }
    }
}
