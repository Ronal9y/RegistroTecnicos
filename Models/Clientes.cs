using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroTecnicos.Models;
public class Clientes
{
    [Key]
    public int ClienteId { get; set; }
    [Required(ErrorMessage = "Este campo es obligatorio")]
    public string? Nombre { get; set; }
    [Required(ErrorMessage = "Este campo es obligatorio")]
    public string? FechaIngreso { get; set; }
    public string? Direccion { get; set; }
    [Required(ErrorMessage = "Este campo es obligatorio")]
    public string? Rnc { get; set; }
    [Required(ErrorMessage = "Este campo es obligatorio")]
    public string? LimiteCredito { get; set; }

    [ForeignKey("Tecnicos")]
    public int TecnicoId { get; set; }
}
