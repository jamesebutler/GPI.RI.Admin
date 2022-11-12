using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GPI.RI.Admin
{
    public partial class About : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
              label1.Text = System.Web.HttpContext.Current.Session["iname"].ToString();
        }
    }
}