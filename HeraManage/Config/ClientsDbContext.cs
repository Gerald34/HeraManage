using HeraManage.Entities;
using Microsoft.EntityFrameworkCore;

namespace HeraManage.Config
{
    public class ClientDbContext : DbContext
    {
        public ClientDbContext(DbContextOptions<ClientDbContext> options) : base(options)
        {

        }

        public DbSet<ClientEntity> Clients { get; set; }
        public DbSet<ClientAccountEntity> ClientAccounts { get; set; }
        public DbSet<AccountPointsEntity> AccountPoints { get; set; }
    }
}