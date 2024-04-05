using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SushiPOP_BE1B_2C2023_G1.Models
{
    [Table("T_RECLAMO")]
    public class Reclamo
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Nombre completo")]
        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        [MaxLength(255, ErrorMessage = ErrorViewModel.CaracteresMaximos)]
        public string NombreCompleto { get; set; }

        [Display(Name = "Correo electrónico")]
        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        public string Email { get; set; }

        [Display(Name = "Teléfono")]
        [MinLength(10, ErrorMessage = ErrorViewModel.CaracteresMinimos)]
        [MaxLength(10, ErrorMessage = ErrorViewModel.CaracteresMaximos)]
        public string Telefono { get; set; }

        [Display(Name = "Detalle de envio")]
        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        [MinLength(50, ErrorMessage = ErrorViewModel.CaracteresMinimos)]
        public string DetalleEnvio { get; set; }

        //Relaciones
        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        public int PedidoId { get; set; }

        public Pedido? Pedido { get; set;}
    }
}
