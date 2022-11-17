using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GPI.RI.Admin
{
    public partial class ErrorPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            


        }

        protected void Page_PreInit(object sender, EventArgs e)
        {

            string mySessionVar = Session["errormessage"] as string;
            //errormessage.Text = "what is this";
            if (mySessionVar != null)
            {
                errormessage.Text = mySessionVar;
            }



        }
    }
}