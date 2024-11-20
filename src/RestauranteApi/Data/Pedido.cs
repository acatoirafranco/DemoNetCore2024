namespace RestauranteApi
{
    public class Pedido
    {
        public int Id { get; set; }
        public string? Producto { get; set; }
        public string? Estado { get; set; }
        // Puedes añadir más propiedades según los requerimientos del negocio, como cantidad, precio, etc.

        // Constructor vacío necesario para la deserialización
        public Pedido() { }

        // Constructor para facilitar la creación de instancias con propiedades inicializadas
        public Pedido(int id, string producto, string estado)
        {
            Id = id;
            Producto = producto;
            Estado = estado;
        }
    }
}
