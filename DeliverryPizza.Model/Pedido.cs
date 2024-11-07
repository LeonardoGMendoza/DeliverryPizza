using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using DeliverryPizza.Model;

namespace DeliverryPizza.Model
{
    public class Pedido
    {
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public decimal Preco { get; set; }

        [Required]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public StatusPedido Status { get; set; }

        [Required]
        [Phone]
        public string CelularCliente { get; set; }
    }
}
