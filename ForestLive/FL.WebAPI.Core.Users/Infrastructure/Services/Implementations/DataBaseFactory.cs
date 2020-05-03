using FL.WebAPI.Core.Users.Entity.Database;
using FL.WebAPI.Core.Users.Infrastructure.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using FL.WebAPI.Core.Users.Configuration.Contracts;

namespace FL.WebAPI.Core.Users.Infrastructure.Services.Implementations
{
    public class DataBaseFactory : IDataBaseFactory
    {
        private readonly IUserConfiguration userConfiguration;

        public DataBaseFactory(IUserConfiguration userConfiguration)
        {
            this.userConfiguration = userConfiguration;
        }

        public UserDbContext GetUserDbContext()
        {
            var options = new DbContextOptionsBuilder<UserDbContext>()
                   .UseSqlServer(this.userConfiguration.ConnectionStringUsersSite)
                   .Options;

            var result = new UserDbContext(options);

            return result;
        }
    }
}
