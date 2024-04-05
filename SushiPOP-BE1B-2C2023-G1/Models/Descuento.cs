using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SushiPOP_BE1B_2C2023_G1.Models
{
    [Table("T_DESCUENTO")]
    public class Descuento
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Día")]
        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        public int Dia { get; set; }

        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        public int? Porcentaje { get; set; } 

        [Display(Name = "Dsecuento máximo")]
        public decimal DescuentoMaximo { get; set; } 

        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        public bool Activo { get; set; } = true;

        //Relaciones
        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        public int ProductoId { get; set; }
        [ForeignKey("ProductoId")]

        public Producto? Producto { get; set; }
    }
}
