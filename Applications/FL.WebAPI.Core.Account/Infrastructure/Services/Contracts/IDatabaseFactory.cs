using FL.WebAPI.Core.Account.Entity.Database;

namespace FL.WebAPI.Core.Account.Infrastructure.Services.Contracts
{
    public interface IDataBaseFactory
    {
        UserDbContext GetUserDbContext();
    }
}
