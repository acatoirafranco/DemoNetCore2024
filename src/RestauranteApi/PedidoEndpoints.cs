using Microsoft.AspNetCore.Http.HttpResults;
using RestauranteApi.Data;

namespace RestauranteApi
{
    public static class PedidoEndpoints
    {

        public static RouteGroupBuilder MapPedidosApi(this RouteGroupBuilder group)
        {
            group.MapGet("pedido/{id}", GetPedido);
            group.MapGet("/{id}", GetPedido);
            group.MapPost("/", CreatePedido);
            return group;
        }

        public static async Task<Results<Ok<Pedido>, NotFound>> GetPedido(int id, PedidoDbContext database)
        {
            var todo = await database.Pedidos.FindAsync(id);

            if (todo != null)
            {
                return TypedResults.Ok(todo);
            }

            return TypedResults.NotFound();
        }

        public static async Task<Created<Pedido>> CreatePedido(PedidoDto todo, PedidoDbContext database)
        {
            var newPedido = new Pedido
            {
                Producto = todo.Producto,
                Estado = todo.Estado
            };

            await database.Pedidos.AddAsync(newPedido);
            await database.SaveChangesAsync();

            return TypedResults.Created($"/pedido/{newPedido.Id}", newPedido);
        }
    }
}
