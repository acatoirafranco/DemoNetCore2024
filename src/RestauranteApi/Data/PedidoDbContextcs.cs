using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace RestauranteApi.Data
{
    public class PedidoDbContext : DbContext
    {
        public DbSet<Pedido> Pedidos => Set<Pedido>();

        public PedidoDbContext(DbContextOptions<PedidoDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionbuilder)
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            optionbuilder.UseSqlite($"Data Source={Path.Join(path, "RestauranteApi.db")}");
        }

        public async virtual Task Reset()
        {
            foreach (Pedido p in Pedidos)
            {
                Pedidos.Remove(p);
            }

            await SaveChangesAsync();
        }
    }
}
