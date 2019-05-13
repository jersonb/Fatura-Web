using FaturaWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace FaturaWeb.Util
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Cliente> Emissor { get; set; }
        public DbSet<Fatura> Faturas { get; set; }
        public DbSet<Item> Itens { get; set; }
    }
}
