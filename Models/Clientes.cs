using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroTecnicos.Models;
public class Clientes
{
    [Key]
    public int ClienteId { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio")]
    public string? ClienteNombres { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio")]
    [DataType(DataType.Date)]
    public DateTime FechaIngreso { get; set; }
    public string? Direccion { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio")]
    [MaxLength(10,ErrorMessage ="El RNC no puede exceder de los 10 digitos")]
    public string? Rnc { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio")]
    public decimal? LimiteCredito { get; set; }

    [ForeignKey("Tecnicos")]
    public int TecnicoId { get; set; }
    public Tecnicos? tecnico { get; set; }
}
