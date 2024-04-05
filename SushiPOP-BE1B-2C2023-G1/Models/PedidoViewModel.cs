namespace SushiPOP_BE1B_2C2023_G1.Models
{
    public class PedidoViewModel
    {
        public int CarritoId { get; set; }
        public ICollection<CarritoItem>? Items { get; set; }
        public string? Cliente { get; set; }

        public string? Direccion { get; set; }
        public decimal Subtotal { get; set; }
        public decimal GastoEnvio { get; set; }


    }
}
