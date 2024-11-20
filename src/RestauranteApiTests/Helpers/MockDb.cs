using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RestauranteApi.Data;


namespace RestauranteApiTests.Helpers
{
    public class MockDb : IDbContextFactory<PedidoDbContext>
    {
        public PedidoDbContext CreateDbContext()
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
     
            var options = new DbContextOptionsBuilder<PedidoDbContext>()
                .UseSqlite($"Data Source={Path.Join(path, "RestauranteApi.db")}")
                .Options;

            return new PedidoDbContext(options);
        }
    }
}
