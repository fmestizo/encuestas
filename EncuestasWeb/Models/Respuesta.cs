using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EncuestasWeb.Models
{
    public class Respuesta
    {
        public Respuesta()
        {
            Detalles = new List<RespuestaDetalle>();
        }

        [Key]
        public int RespuestaId { get; set; }

        [ForeignKey("Encuesta")]
        public int EncuestaId { get; set; }

        public DateTime FechaCreacion { get; set; }

        public virtual Encuesta Encuesta { get; set; }

        public virtual ICollection<RespuestaDetalle> Detalles { get; set; }
    }
}
