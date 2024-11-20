using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using RestauranteApi;
using RestauranteApiTests.Helpers;

namespace RestauranteApiTests
{
    public class RestauranteApiTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly CustomWebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;

        public RestauranteApiTests(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        [Fact]
        public async Task GetPedido_ReturnsNotFoundResult()
        {
            // Arrange
            await using var context = new MockDb().CreateDbContext();
            await context.Reset();

            await context.Pedidos.AddAsync(new Pedido
            {
                Id = 5,
                Producto = "Test title",
                Estado = "Test descripción"
            });

            await context.SaveChangesAsync();

            // Act
            var response = await _client.GetAsync("/pedido/1");

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task GetPedido_ReturnsExpectedResult()
        {
            // Arrange
            await using var context = new MockDb().CreateDbContext();
            await context.Reset();

            await context.Pedidos.AddAsync(new Pedido
            {
                Id = 1,
                Producto = "Pizza Margarita",
                Estado = "Preparándose"
            });

            await context.SaveChangesAsync();

            var uri = "pedido/1";

            // Act
            var response = await _client.GetAsync(uri);
            
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();

            Pedido? pedido = JsonConvert.DeserializeObject<Pedido>(result);

            // Assert
            Assert.NotNull(pedido);

            Assert.Equal("Pizza Margarita", pedido.Producto);
        }
    }
}