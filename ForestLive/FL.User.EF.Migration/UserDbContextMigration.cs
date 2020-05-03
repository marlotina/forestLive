using FL.WebAPI.Core.Users.Entity.Database;
using Microsoft.EntityFrameworkCore;

namespace User.EF.Migration
{
    public class UserDbContextMigration : UserDbContext
    {

        static string DevEnvironment = @"data source=DESKTOP-JIFKME0\SQLEXPRESS; initial catalog=flusersdev; Integrated Security=True;";


        public UserDbContextMigration() :
            base(new DbContextOptionsBuilder<UserDbContext>()
                .UseSqlServer(DevEnvironment)
                .Options)
        {
        }
    }
}
