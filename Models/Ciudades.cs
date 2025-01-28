using System.ComponentModel.DataAnnotations;

namespace RegistroTecnicos.Models
{
    public class Ciudades
    {
        [Key]
        public int CiudadId { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string? NombreCiudad { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string? DireccionCiudad { get; set; }

    }
}
