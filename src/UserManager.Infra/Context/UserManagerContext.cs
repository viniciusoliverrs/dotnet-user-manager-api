using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManager.Domain.Entities;
using UserManager.Infra.Mappings;

namespace UserManager.Infra.Context
{
    public class UserManagerContext : DbContext
    {
        public UserManagerContext() { }
        public UserManagerContext(DbContextOptions<UserManagerContext> options) : base(options) { }

        public virtual DbSet<User> Users { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=localhost;Database=manager-dev;User Id=sa;Password=Sa12345@;");
        //}

     protected override void OnModelCreating(ModelBuilder builder)
     {
         builder.ApplyConfiguration(new UserMap());
     }
    }
}
