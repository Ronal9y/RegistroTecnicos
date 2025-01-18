using System.ComponentModel.DataAnnotations;

namespace RegistroTecnicos.Models
{
    public class Tecnicos
    {
        [Key]
        public int TecnicoId { get; set; }
        [Required(ErrorMessage ="Este campo es obligatorio")]
        public string? Nombres { get; set; }
        [Required(ErrorMessage ="Este campo es obligatorio")]
        public decimal? SueldoHora { get; set; }
    }
}
