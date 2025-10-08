using System;
using System.Linq;
using EncuestasWeb.Models;

namespace EncuestasWeb
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarEncuestas();
            }
        }

        private void CargarEncuestas()
        {
            using (var db = new EncuestasContext())
            {
                var encuestas = db.Encuestas.OrderBy(e => e.EncuestaNombre).ToList();
                if (encuestas.Any())
                {
                    EncuestasRepeater.DataSource = encuestas;
                    EncuestasRepeater.DataBind();
                    MensajeLiteral.Text = string.Empty;
                }
                else
                {
                    MensajeLiteral.Text = "<p>No hay encuestas disponibles.</p>";
                }
            }
        }
    }
}
