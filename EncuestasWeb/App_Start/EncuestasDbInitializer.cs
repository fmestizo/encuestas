using System.Collections.Generic;
using System.Data.Entity;
using EncuestasWeb.Models;

namespace EncuestasWeb.App_Start
{
    public class EncuestasDbInitializer : CreateDatabaseIfNotExists<EncuestasContext>
    {
        protected override void Seed(EncuestasContext context)
        {
            var encuestaDemo = new Encuesta
            {
                EncuestaNombre = "Encuesta de satisfacción",
                Preguntas = new List<Pregunta>
                {
                    new Pregunta
                    {
                        Orden = 1,
                        Texto = "¿Con qué frecuencia utiliza nuestro servicio?",
                        TipoRespuesta = "combo",
                        Valores = "Diario,Semanal,Mensual,Eventualmente",
                        EsObligatoria = true
                    },
                    new Pregunta
                    {
                        Orden = 2,
                        Texto = "Seleccione los motivos principales por los que nos elige",
                        TipoRespuesta = "check",
                        Valores = "Precio,Calidad,Ubicación,Recomendación",
                        EsObligatoria = false
                    },
                    new Pregunta
                    {
                        Orden = 3,
                        Texto = "Cuéntenos cómo podríamos mejorar",
                        TipoRespuesta = "texto",
                        EsObligatoria = false
                    }
                }
            };

            context.Encuestas.Add(encuestaDemo);
            context.SaveChanges();
        }
    }
}
