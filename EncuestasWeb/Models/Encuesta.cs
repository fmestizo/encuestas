using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EncuestasWeb.Models
{
    public class Encuesta
    {
        public Encuesta()
        {
            Preguntas = new List<Pregunta>();
            Respuestas = new List<Respuesta>();
        }

        [Key]
        public int EncuestaId { get; set; }

        [Required]
        [StringLength(200)]
        public string EncuestaNombre { get; set; }

        public virtual ICollection<Pregunta> Preguntas { get; set; }

        public virtual ICollection<Respuesta> Respuestas { get; set; }
    }
}
