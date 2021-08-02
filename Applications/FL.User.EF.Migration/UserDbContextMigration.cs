using FL.WebAPI.Core.Users.Entity.Database;
using Microsoft.EntityFrameworkCore;

namespace User.EF.Migration
{
    public class UserDbContextMigration : UserDbContext
    {

        static string DevEnvironment = @"Server=tcp:wachingbirdsqa.database.windows.net,1433;Initial Catalog=users;Persist Security Info=False;User ID=superbird;Password=dJwejKSOn5S%Sas;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";


        public UserDbContextMigration() :
            base(new DbContextOptionsBuilder<UserDbContext>()
                .UseSqlServer(DevEnvironment)
                .Options)
        {
        }
    }
}
