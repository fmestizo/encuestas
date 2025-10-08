using System.Data.Entity;

namespace EncuestasWeb.Models
{
    public class EncuestasContext : DbContext
    {
        public EncuestasContext()
            : base("name=EncuestasContext")
        {
        }

        public DbSet<Encuesta> Encuestas { get; set; }
        public DbSet<Pregunta> Preguntas { get; set; }
        public DbSet<Respuesta> Respuestas { get; set; }
        public DbSet<RespuestaDetalle> RespuestaDetalles { get; set; }
    }
}
