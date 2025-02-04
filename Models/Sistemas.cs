using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroTecnicos.Models
{
    public class Sistemas
    {

        [Key]
        public int SistemaId { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string? Descripcion { get; set; }
        [Range(1, 100, ErrorMessage = "La complejidad deba estar entre 1 y 100")]
        public double Complejidad { get; set; }
        
    }
}
