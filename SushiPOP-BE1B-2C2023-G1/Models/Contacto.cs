using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SushiPOP_BE1B_2C2023_G1.Models
{
    [Table("T_CONTACTO")]
    public class Contacto
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
        public string Telefono { get; set;}

        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        [MaxLength(8000, ErrorMessage = ErrorViewModel.CaracteresMaximos)]
        public string Mensaje { get; set;}

        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        public bool Leido { get; set; } = false;
    }
}
