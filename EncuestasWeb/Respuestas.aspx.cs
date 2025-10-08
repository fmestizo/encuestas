using System;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using EncuestasWeb.Models;

namespace EncuestasWeb
{
    public partial class Respuestas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarRespuesta();
            }
        }

        private void CargarRespuesta()
        {
            if (!int.TryParse(Request.QueryString["id"], NumberStyles.Integer, CultureInfo.InvariantCulture, out var respuestaId))
            {
                MostrarNoEncontrado();
                return;
            }

            using (var db = new EncuestasContext())
            {
                var respuesta = db.Respuestas
                    .Include(r => r.Encuesta)
                    .Include(r => r.Detalles.Select(d => d.Pregunta))
                    .FirstOrDefault(r => r.RespuestaId == respuestaId);

                if (respuesta == null)
                {
                    MostrarNoEncontrado();
                    return;
                }

                TituloLiteral.Text = respuesta.Encuesta?.EncuestaNombre ?? "Encuesta";
                MetaLiteral.Text = string.Format(CultureInfo.CurrentCulture,
                    "<p class=\"meta\">Respuestas #{0} - {1:G}</p>",
                    respuesta.RespuestaId,
                    respuesta.FechaCreacion.ToLocalTime());

                var datos = respuesta.Detalles
                    .OrderBy(d => d.Pregunta.Orden)
                    .ThenBy(d => d.PreguntaId)
                    .Select(d => new
                    {
                        Pregunta = d.Pregunta?.Texto ?? "Pregunta",
                        Respuesta = string.IsNullOrWhiteSpace(d.Valor) ? "-" : d.Valor
                    })
                    .ToList();

                if (datos.Any())
                {
                    RespuestasRepeater.DataSource = datos;
                    RespuestasRepeater.DataBind();
                    MensajeLiteral.Text = string.Empty;
                    RespuestasRepeater.Visible = true;
                    ImprimirButton.Visible = true;
                }
                else
                {
                    MensajeLiteral.Text = "<p>No se registraron respuestas.</p>";
                    RespuestasRepeater.Visible = false;
                    ImprimirButton.Visible = false;
                }
            }
        }

        private void MostrarNoEncontrado()
        {
            TituloLiteral.Text = "Respuestas no disponibles";
            MetaLiteral.Text = "<p class=\"meta\">No encontramos información para la respuesta solicitada.</p>";
            MensajeLiteral.Text = "<p>No se encontró la respuesta solicitada.</p>";
            RespuestasRepeater.Visible = false;
            ImprimirButton.Visible = false;
        }
    }
}
