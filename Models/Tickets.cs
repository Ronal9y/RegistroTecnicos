using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroTecnicos.Models
{
    public class Tickets
    {
        [Key]
        public int TicketId { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string? Prioridad { get; set; }
        
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string? Asunto { get; set; }
        
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string? Descripcion { get; set; }
        
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public decimal? TiempoInvertido { get; set; }

        [ForeignKey("Tecnicos")]
        public int TecnicoId { get; set; }
        public Tecnicos? tecnico { get; set; }

        [ForeignKey("Clientes")]
        public int ClienteId { get; set; }
        public Clientes? clientes { get; set; }
    }
}
