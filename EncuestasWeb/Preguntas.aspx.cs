using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using EncuestasWeb.Models;

namespace EncuestasWeb
{
    public partial class Preguntas : Page
    {
        private const string ControlPrefix = "Pregunta_";

        protected void Page_Init(object sender, EventArgs e)
        {
            ConstruirPreguntas();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                using (var db = new EncuestasContext())
                {
                    var encuesta = ObtenerEncuesta(db);
                    if (encuesta == null)
                    {
                        MostrarEncuestaNoEncontrada();
                        return;
                    }

                    TituloLiteral.Text = encuesta.EncuestaNombre;
                    DescripcionLiteral.Text = encuesta.Preguntas.Any()
                        ? string.Format(CultureInfo.CurrentCulture, "<p>Complete las {0} preguntas y presione Finalizar.</p>", encuesta.Preguntas.Count)
                        : "<p>Esta encuesta aún no tiene preguntas configuradas.</p>";
                }
            }
        }

        protected void FinalizarButton_Click(object sender, EventArgs e)
        {
            ErroresLiteral.Text = string.Empty;
            ErroresPanel.Visible = false;

            using (var db = new EncuestasContext())
            {
                var encuesta = ObtenerEncuesta(db);
                if (encuesta == null)
                {
                    MostrarEncuestaNoEncontrada();
                    return;
                }

                var preguntas = encuesta.Preguntas
                    .OrderBy(p => p.Orden)
                    .ThenBy(p => p.PreguntaId)
                    .ToList();

                var errores = new List<string>();
                var detalles = new List<RespuestaDetalle>();

                foreach (var pregunta in preguntas)
                {
                    var controlId = ControlPrefix + pregunta.PreguntaId;
                    var valor = ObtenerValorPregunta(pregunta, controlId);

                    if (string.IsNullOrWhiteSpace(valor))
                    {
                        if (pregunta.EsObligatoria)
                        {
                            errores.Add(string.Format(CultureInfo.CurrentCulture, "La pregunta \"{0}\" es obligatoria.", pregunta.Texto));
                        }

                        continue;
                    }

                    detalles.Add(new RespuestaDetalle
                    {
                        PreguntaId = pregunta.PreguntaId,
                        Valor = valor
                    });
                }

                if (errores.Any())
                {
                    ErroresLiteral.Text = "<ul class=\"validation-list\">" + string.Join(string.Empty, errores.Select(e => "<li>" + e + "</li>")) + "</ul>";
                    ErroresPanel.Visible = true;
                    return;
                }

                var respuesta = new Respuesta
                {
                    EncuestaId = encuesta.EncuestaId,
                    FechaCreacion = DateTime.UtcNow,
                    Detalles = detalles
                };

                db.Respuestas.Add(respuesta);
                db.SaveChanges();

                Response.Redirect(string.Format(CultureInfo.InvariantCulture, "Respuestas.aspx?id={0}", respuesta.RespuestaId), false);
            }
        }

        private void ConstruirPreguntas()
        {
            using (var db = new EncuestasContext())
            {
                var encuesta = ObtenerEncuesta(db);
                if (encuesta == null)
                {
                    MostrarEncuestaNoEncontrada();
                    return;
                }

                var preguntas = encuesta.Preguntas
                    .OrderBy(p => p.Orden)
                    .ThenBy(p => p.PreguntaId)
                    .ToList();

                PreguntasPanel.Controls.Clear();

                foreach (var pregunta in preguntas)
                {
                    var contenedor = new Panel { CssClass = "question" };
                    var etiqueta = new Label
                    {
                        AssociatedControlID = ControlPrefix + pregunta.PreguntaId,
                        Text = pregunta.Texto + (pregunta.EsObligatoria ? " *" : string.Empty)
                    };

                    contenedor.Controls.Add(etiqueta);

                    Control controlRespuesta = CrearControlParaPregunta(pregunta);
                    contenedor.Controls.Add(controlRespuesta);

                    PreguntasPanel.Controls.Add(contenedor);
                }
            }
        }

        private Control CrearControlParaPregunta(Pregunta pregunta)
        {
            var tipo = (pregunta.TipoRespuesta ?? string.Empty).Trim().ToLowerInvariant();
            var controlId = ControlPrefix + pregunta.PreguntaId;

            switch (tipo)
            {
                case "combo":
                    var lista = new DropDownList
                    {
                        ID = controlId,
                        CssClass = "field"
                    };
                    lista.Items.Add(new ListItem("Seleccione...", string.Empty));
                    foreach (var opcion in ParsearValores(pregunta.Valores))
                    {
                        lista.Items.Add(new ListItem(opcion, opcion));
                    }
                    return lista;
                case "check":
                    var checks = new CheckBoxList
                    {
                        ID = controlId,
                        RepeatDirection = RepeatDirection.Vertical,
                        CssClass = "field"
                    };
                    foreach (var opcion in ParsearValores(pregunta.Valores))
                    {
                        checks.Items.Add(new ListItem(opcion, opcion));
                    }
                    return checks;
                default:
                    return new TextBox
                    {
                        ID = controlId,
                        TextMode = TextBoxMode.MultiLine,
                        CssClass = "field"
                    };
            }
        }

        private string ObtenerValorPregunta(Pregunta pregunta, string controlId)
        {
            var tipo = (pregunta.TipoRespuesta ?? string.Empty).Trim().ToLowerInvariant();
            switch (tipo)
            {
                case "combo":
                    if (PreguntasPanel.FindControl(controlId) is DropDownList lista)
                    {
                        return lista.SelectedValue;
                    }
                    break;
                case "check":
                    if (PreguntasPanel.FindControl(controlId) is CheckBoxList checks)
                    {
                        var seleccionadas = checks.Items.Cast<ListItem>().Where(i => i.Selected).Select(i => i.Value).ToList();
                        return seleccionadas.Any() ? string.Join(",", seleccionadas) : string.Empty;
                    }
                    break;
                default:
                    if (PreguntasPanel.FindControl(controlId) is TextBox texto)
                    {
                        return texto.Text?.Trim();
                    }
                    break;
            }

            return string.Empty;
        }

        private Encuesta ObtenerEncuesta(EncuestasContext db)
        {
            if (!int.TryParse(Request.QueryString["id"], NumberStyles.Integer, CultureInfo.InvariantCulture, out var encuestaId))
            {
                return null;
            }

            return db.Encuestas.Include("Preguntas").FirstOrDefault(e => e.EncuestaId == encuestaId);
        }

        private IEnumerable<string> ParsearValores(string valores)
        {
            if (string.IsNullOrWhiteSpace(valores))
            {
                yield break;
            }

            var acumulado = new StringBuilder();
            var entreComillas = false;

            foreach (var caracter in valores)
            {
                if (caracter == '"')
                {
                    entreComillas = !entreComillas;
                    continue;
                }

                if (caracter == ',' && !entreComillas)
                {
                    var opcion = acumulado.ToString().Trim();
                    if (opcion.Length > 0)
                    {
                        yield return opcion;
                    }
                    acumulado.Clear();
                }
                else
                {
                    acumulado.Append(caracter);
                }
            }

            if (acumulado.Length > 0)
            {
                var opcion = acumulado.ToString().Trim();
                if (opcion.Length > 0)
                {
                    yield return opcion;
                }
            }
        }

        private void MostrarEncuestaNoEncontrada()
        {
            TituloLiteral.Text = "Encuesta no encontrada";
            DescripcionLiteral.Text = "<p>La encuesta solicitada no está disponible.</p>";
            PreguntasPanel.Controls.Clear();
            ErroresPanel.Visible = false;
            FinalizarButton.Visible = false;
        }
    }
}
