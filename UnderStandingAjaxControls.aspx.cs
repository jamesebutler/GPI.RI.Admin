using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GPI.RI.Admin
{
    public partial class UnderStandingAjaxControls : System.Web.UI.Page
    {


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Label1.Text = DateTime.Now.TimeOfDay.ToString();
                Label2.Text = DateTime.Now.TimeOfDay.ToString();
                Label3.Text = DateTime.Now.TimeOfDay.ToString();
            }
        }

        protected void UpdateTime(object sender, EventArgs e)
        {
            Label2.Text = DateTime.Now.TimeOfDay.ToString();
            Label3.Text = DateTime.Now.TimeOfDay.ToString();
            Label1.Text = DateTime.Now.TimeOfDay.ToString();
        }

        protected void UpdateTime2(object sender, EventArgs e)
        {
            Label2.Text = DateTime.Now.TimeOfDay.ToString();

        }


    }
}