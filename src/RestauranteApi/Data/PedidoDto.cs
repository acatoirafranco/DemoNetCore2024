namespace RestauranteApi
{
    public class PedidoDto
    {
        public string? Producto { get; set; }
        public string? Estado { get; set; }
        // Puedes añadir más propiedades según los requerimientos del negocio, como cantidad, precio, etc.

        // Constructor vacío necesario para la deserialización
        public PedidoDto() { }

        // Constructor para facilitar la creación de instancias con propiedades inicializadas
        public PedidoDto(string producto, string estado)
        {
            Producto = producto;
            Estado = estado;
        }
    }
}
