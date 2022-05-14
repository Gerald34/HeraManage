using ClientAccountService.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClientAccountService.Config
{
    public class ClientDbContext : DbContext
    {
        public ClientDbContext(DbContextOptions<ClientDbContext> options) : base(options)
        {

        }

        public DbSet<ClientEntity> Clients { get; set; }
        public DbSet<ClientAccountEntity> ClientAccounts { get; set; }
    }
}