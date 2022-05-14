using Microsoft.EntityFrameworkCore;
using ClientAccountPointsService.Entities;

namespace ClientAccountPointsService
{
    class AccountPointsDbContext : DbContext
    {
        public AccountPointsDbContext(DbContextOptions<AccountPointsDbContext> options) : base(options)
        {

        }

        public DbSet<AccountPointsEntity> AccountPoints { get; set; }
    }
}