using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Telerik.Web.UI;
using GPI.MILL.DataAccess.Oracle;
using GPI.MILL.Ldap;
using GPI.User.Model;
using System.Xml;
using System.Text;

using System.Web.Security;



using System.Security.Principal;
using System.Net;

using Devart.Data.Oracle;



namespace GPI.RI.Admin
{
    public partial class SiteMaster : MasterPage
    {
        GPI.MILL.DataAccess.Oracle.RetrieveData da = new RetrieveData();
        GPI.User.Model.LdapUser ldap = new LdapUser();
      
        protected void Page_Load(object sender, EventArgs e)
        {

            
            if (Session["DatabaseName"] == null)
            { 
            
                string SqlString = " SELECT DISTINCT UPPER(SERVICE_NAME) SERVICE_NAME FROM V$SESSION WHERE USER# IN (SELECT USER_ID FROM USER_USERS)";
                string ServiceName = "Database ({0})";
                ServiceName = string.Format(ServiceName, da.GetDatabaseName(SqlString));
                //ServiceName = string.Format(ServiceName, GetDatabaseName(SqlString));


                LabelDatabase.Text =  ServiceName;
                System.Web.HttpContext.Current.Session["DatabaseName"] = ServiceName;
                bool check = ServiceName.Contains("GPCIOD02");

                if (check)
                {
                    System.Web.HttpContext.Current.Session["TestDatabase"] = "YES";
                    LabelBannerWarning.Visible = true;

                }
                else
                {
                    System.Web.HttpContext.Current.Session["TestDatabase"] = "NO";
                    LabelBannerWarning.Visible = false;
                }

            }
            else
            {
                LabelDatabase.Text = Session["DatabaseName"].ToString();

                if (Session["TestDatabase"].ToString() == "NO")
                {
                    LabelBannerWarning.Visible = false;
                }
                else
                {
                    LabelBannerWarning.Visible = true;
                }
            }
            
            string myname = Request.LogonUserIdentity.Name;
            System.Web.HttpContext.Current.Session["iname"] = myname;


            RIUser riuser = new RIUser();
            string[] fullUsername = myname.Split(System.Convert.ToChar(@"\"));
            riuser = riuser.GetEmployee(fullUsername[1].ToUpper());

            //go make sure the user is in the DB
            if (riuser.Email != null)
            {
                //check for inactive
                if (riuser.InactiveFlag == "N")
                {

                }
                else if (riuser.InactiveFlag == "Y")
                {
                    //do not proceed
                    return;
                }
            }


            WriteToSession(riuser);

            LabelUserName.Text = "User: " + Session["LastName"] + ", " + Session["FirstName"] + " (" + Session["SiteID"].ToString() + ")";
           


 

        }



        protected void Page_Init(object sender, EventArgs e)
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
        }


        //get the Ldap and RI user settings

        private RIUser WriteToSession(RIUser riuser)

        {

            System.Web.HttpContext.Current.Session["UserName"] = riuser.UserName;
            System.Web.HttpContext.Current.Session["Domain"] = riuser.Domain;
            System.Web.HttpContext.Current.Session["SiteID"] = riuser.SiteID;
            System.Web.HttpContext.Current.Session["EmployeeID"] = riuser.EmployeeID;
            System.Web.HttpContext.Current.Session["FirstName"] = riuser.FirstName;
            System.Web.HttpContext.Current.Session["LastName"] = riuser.LastName;
            System.Web.HttpContext.Current.Session["Email"] = riuser.Email;
            System.Web.HttpContext.Current.Session["Extension"] = riuser.Extension;
            System.Web.HttpContext.Current.Session["InactiveFlag"] = riuser.InactiveFlag;
            System.Web.HttpContext.Current.Session["DefaultLanguage"] = riuser.DefaultLanguage;
            System.Web.HttpContext.Current.Session["PlantCode"] = riuser.PlantCode;
            System.Web.HttpContext.Current.Session["SignatureFile"] = riuser.SignatureFile;
            System.Web.HttpContext.Current.Session["LastUpdateUserName"] = riuser.LastUpdateUserName;
            System.Web.HttpContext.Current.Session["LastUpDateDate"] = riuser.LastUpDateDate;
            System.Web.HttpContext.Current.Session["MiddleInit"] = riuser.MiddleInit;
            System.Web.HttpContext.Current.Session["ManagerUserName"] = riuser.ManagerUserName;
            System.Web.HttpContext.Current.Session["UITheme"] = riuser.UITheme;
            System.Web.HttpContext.Current.Session["SiteName"] = riuser.SiteName;


            return (null);
        }



        // nothing below this line
    }
}