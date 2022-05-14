using HeraManage.Entities;
using Microsoft.EntityFrameworkCore;

namespace HeraManage.Config
{
    public class UsersDbContext : DbContext
    {
        public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options)
        {

        }

        public DbSet<UserEntity> AdminUsers { get; set; }
    }


}