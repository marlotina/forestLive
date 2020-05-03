using FL.WebAPI.Core.Users.Entity.Database;

namespace FL.WebAPI.Core.Users.Infrastructure.Services.Contracts
{
    public interface IDataBaseFactory
    {
        UserDbContext GetUserDbContext();
    }
}
