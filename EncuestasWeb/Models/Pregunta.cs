using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EncuestasWeb.Models
{
    public class Pregunta
    {
        public Pregunta()
        {
            Respuestas = new List<RespuestaDetalle>();
        }

        [Key]
        public int PreguntaId { get; set; }

        [ForeignKey("Encuesta")]
        public int EncuestaId { get; set; }

        public int Orden { get; set; }

        [Required]
        [StringLength(400)]
        public string Texto { get; set; }

        [Required]
        [StringLength(20)]
        public string TipoRespuesta { get; set; }

        public string Valores { get; set; }

        public bool EsObligatoria { get; set; }

        public virtual Encuesta Encuesta { get; set; }

        public virtual ICollection<RespuestaDetalle> Respuestas { get; set; }
    }
}
