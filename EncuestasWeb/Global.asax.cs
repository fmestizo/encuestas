using System;
using System.Data.Entity;

namespace EncuestasWeb
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            Database.SetInitializer(new App_Start.EncuestasDbInitializer());
        }
    }
}
