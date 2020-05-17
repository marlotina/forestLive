using FL.WebAPI.Core.Users.Domain.Entities;
using FL.WebAPI.Core.Users.Domain.Repositories;
using FL.WebAPI.Core.Users.Infrastructure.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Users.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDataBaseFactory iDataBaseFactory;

        public UserRepository(
            IDataBaseFactory iDataBaseFactory)
        {
            this.iDataBaseFactory = iDataBaseFactory;
        }

        public async Task<Domain.Entities.User> GetByIdAsync(Guid id)
        {
            using (var context = this.iDataBaseFactory.GetUserDbContext())
            {
                var result = await context.Users
                        .Where(x => x.Id == id)
                        .FirstOrDefaultAsync();

                return result;
            }
        }

        public async Task<IEnumerable<Domain.Entities.User>> FindByEmailAsync(string email)
        {
            using (var context = this.iDataBaseFactory.GetUserDbContext())
            {
                var result = await context.Users
                    .Where(x => x.NormalizedEmail.Contains(email.ToLower()))
                    .ToListAsync();

                return result;
            }
        }
        
        public async Task<bool> UpdateAsync(Domain.Entities.User user)
        {
            using (var context = this.iDataBaseFactory.GetUserDbContext())
            {
                context.Update(user);
                var result = await context.SaveChangesAsync() > 0;
                return result;
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            using (var context = this.iDataBaseFactory.GetUserDbContext())
            {
                context.Users.Remove(context.Users.Find(id));
                var result = await context.SaveChangesAsync() > 0;
                return result;
            }
        }

        public async Task<User> GetByUserNameAsync(string userName)
        {
            using (var context = this.iDataBaseFactory.GetUserDbContext())
            {
                var result = await context.Users
                        .Where(x => x.UserName == userName)
                        .FirstOrDefaultAsync();

                return result;
            }
        }
    }
}
