using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SushiPOP_BE1B_2C2023_G1.Models
{
    [Table("T_PRODUCTO")]
    public class Producto
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        [MaxLength(100, ErrorMessage = ErrorViewModel.CaracteresMaximos)]
        public string Nombre { get; set; }

        [Display(Name = "Descripción")]
        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        [MinLength(20, ErrorMessage = ErrorViewModel.CaracteresMinimos)]
        [MaxLength(250, ErrorMessage = ErrorViewModel.CaracteresMaximos)]
        public string Descripcion { get; set;}

        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        public decimal Precio { get; set; }

        public string Foto { get; set; }

        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        public int Stock { get; set; }

        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        public decimal Costo { get; set; }

        //Relaciones
        public int CategoriaId { get; set; }
        public Categoria? Categoria { get; set; }

        //Relaciones
        public ICollection<CarritoItem>? CarritosItems { get; set; }
        public ICollection<Descuento>? Descuentos { get; set; }
    }
}
