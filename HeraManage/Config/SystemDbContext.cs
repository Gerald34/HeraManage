using Microsoft.EntityFrameworkCore;
using HeraManage.Entities;

namespace HeraManage.Config
{
    public class SystemDbContext : DbContext
    {
        public SystemDbContext(DbContextOptions<SystemDbContext> options) : base(options)
        {

        }

        public DbSet<AccountTypesEntity> AccountTypes { get; }
    }
}