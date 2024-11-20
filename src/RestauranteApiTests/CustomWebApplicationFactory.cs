using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RestauranteApi.Data;
using System.Data.Common;

namespace RestauranteApiTests
{
    public class CustomWebApplicationFactory<TProgram>
        : WebApplicationFactory<TProgram> where TProgram : class
    {

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var dbContextDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<PedidoDbContext>));

                services.Remove(dbContextDescriptor!);

                services.AddDbContext<PedidoDbContext>(options =>
                {
                    var path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                    options.UseSqlite($"Data Source={Path.Join(path, "RestauranteApi.db")}");
                });
            });

            builder.UseEnvironment("development");
        }

        //protected override void ConfigureWebHost(IWebHostBuilder builder)
        //{
        //    builder.UseStartup<TestStartup>();

        //    builder.ConfigureServices(services =>
        //    {
        //        var dbContextDescriptor = services.SingleOrDefault(
        //            d => d.ServiceType ==
        //                typeof(DbContextOptions<PedidoDbContext>));

        //        services.Remove(dbContextDescriptor);

        //        var dbConnectionDescriptor = services.SingleOrDefault(
        //            d => d.ServiceType ==
        //                typeof(DbConnection));

        //        services.Remove(dbConnectionDescriptor);

        //        // Create open SqliteConnection so EF won't automatically close it.
        //        services.AddSingleton<DbConnection>(container =>
        //        {
        //            var connection = new SqliteConnection("DataSource=:memory:");
        //            connection.Open();

        //            return connection;
        //        });

        //        services.AddDbContext<PedidoDbContext>((container, options) =>
        //        {
        //            var connection = container.GetRequiredService<DbConnection>();
        //            options.UseSqlite(connection);
        //        });
        //    });

        //    builder.UseEnvironment("Development");
        //}
    }
 }
