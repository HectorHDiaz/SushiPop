using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SushiPOP_BE1B_2C2023_G1.Models
{
    [Table("T_CLIENTE")]
    public class Cliente : Usuario
    {
        public int? NumeroCliente { get; set; }

        //Relaciones
        public ICollection<Carrito>? Carritos { get; set; }
        public ICollection<Reserva>? Reservas { get; set; }
    }
}
