using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SushiPOP_BE1B_2C2023_G1.Models
{
    [Table("T_RESERVA")]
    public class Reserva
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        public string Local { get; set; }

        [Display(Name = "Fecha y hora")]
        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        public DateTime FechaHora { get; set; }

        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        public bool Confirmada { get; set; } = false;

        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        [MinLength(5, ErrorMessage = ErrorViewModel.CaracteresMinimos)]
        [MaxLength(30, ErrorMessage = ErrorViewModel.CaracteresMaximos)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        [MinLength(2, ErrorMessage = ErrorViewModel.CaracteresMinimos)]
        [MaxLength(30, ErrorMessage = ErrorViewModel.CaracteresMaximos)]
        public string Apellido { get; set; }

        //Relaciones
        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        public int ClienteId { get; set; }

        public Cliente? Cliente { get; set; }
    }
}
