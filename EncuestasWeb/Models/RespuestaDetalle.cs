using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EncuestasWeb.Models
{
    public class RespuestaDetalle
    {
        [Key]
        public int RespuestaDetalleId { get; set; }

        [ForeignKey("Respuesta")]
        public int RespuestaId { get; set; }

        [ForeignKey("Pregunta")]
        public int PreguntaId { get; set; }

        [Required]
        public string Valor { get; set; }

        public virtual Respuesta Respuesta { get; set; }

        public virtual Pregunta Pregunta { get; set; }
    }
}
