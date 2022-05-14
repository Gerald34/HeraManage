using Microsoft.EntityFrameworkCore;
using ClientAccountService.Entities;

namespace ClientAccountService.Config
{
    public class SystemDbContext : DbContext
    {
        public SystemDbContext(DbContextOptions<SystemDbContext> options) : base(options)
        {

        }

        public DbSet<AccountTypesEntity> AccountTypes { get; }
    }
}