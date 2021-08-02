using FL.WebAPI.Core.Account.Domain.Entities;
using FL.WebAPI.Core.Account.Infrastructure.DataBase.FluentConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System;

namespace FL.WebAPI.Core.Account.Entity.Database
{
    public class UserDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public UserDbContext(DbContextOptions options)
            : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.ApplyConfiguration(new UserConfiguration());

            modelBuilder.Entity<IdentityRole<Guid>>(x => x.ToTable("Role"));
            modelBuilder.Entity<IdentityUserClaim<Guid>>(x => x.ToTable("UserClaim"));
            modelBuilder.Entity<IdentityUserRole<Guid>>(x => x.ToTable("UserRole"));
            modelBuilder.Entity<IdentityUserLogin<Guid>>(x => x.ToTable("UserLogin"));
            modelBuilder.Entity<IdentityRoleClaim<Guid>>(x => x.ToTable("RoleClaim"));
            modelBuilder.Entity<IdentityUserToken<Guid>>(x => x.ToTable("UserToken"));
        }
    }
}
