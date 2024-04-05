using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SushiPOP_BE1B_2C2023_G1.Models
{
    [Table("T_EMPLEADO")]
    public class Empleado : Usuario
    {
        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        public int Legajo { set; get; } 
    }
}
