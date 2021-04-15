using FL.WebAPI.Core.Users.Domain.Entities;
using FL.WebAPI.Core.Users.Domain.Repositories;
using FL.WebAPI.Core.Users.Infrastructure.Services.Contracts;
using Microsoft.EntityFrameworkCore;
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

        public async Task<User> GetByUserNameAsync(string userName)
        {
            using (var context = this.iDataBaseFactory.GetUserDbContext())
            {
                var result = await context.Users
                        .Where(x => x.NormalizedUserName == userName.ToUpperInvariant())
                        .FirstOrDefaultAsync();

                return result;
            }
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            using (var context = this.iDataBaseFactory.GetUserDbContext())
            {
                var result = await context.Users.Where(x => x.EmailConfirmed).ToListAsync();

                return result;
            }
        }
    }
}
