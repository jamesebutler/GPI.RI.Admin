using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace GPI.RI.Admin
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            RadMenu1.LoadContentFile("~/Menu/Data/AdminMenu.xml");

            RadMenuItem currentItem = RadMenu1.FindItemByUrl(Request.Url.PathAndQuery);

            if (currentItem != null)
            {
                //Select the current item and his parents
                currentItem.HighlightPath();
            }
            else
            {
                //RadMenu1.Items[1].HighlightPath();
            }


        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            Page.Theme = "RIPurple";
        }
    }
}