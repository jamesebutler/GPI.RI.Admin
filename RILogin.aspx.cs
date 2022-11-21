using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Configuration;


using Devart.Data.Oracle;
using Telerik.Web.UI;

using GPI.MILL.DataAccess.Oracle;
using GPI.MILL.Ldap;
using GPI.User.Model;
using GPI.Error.Logging;
using GPI.RI.Admin;


namespace GPI.RI.Admin
{
    public partial class RILogin : System.Web.UI.Page
    {

        GPI.MILL.DataAccess.Oracle.RetrieveData da = new RetrieveData();
        GPI.User.Model.LdapUser ldap = new LdapUser();
        ErrorLog errorLog = new ErrorLog();
        protected void Page_Load(object sender, EventArgs e)
        {

            System.Security.Principal.IPrincipal User;
            User = System.Web.HttpContext.Current.User;

            Labeluser.Text = "nothing yet";
           string myname = Session["iname"].ToString();
            string myredirect = "about.aspx?name=" + myname;
            Response.Redirect(myredirect);
            if (myname != "")
            {
              System.Web.HttpContext.Current.Session["iname"] = myname;
                string[] fullUsername = myname.Split(System.Convert.ToChar(@"\"));
                string myusername = fullUsername[1].ToUpper();


                object m = null;
                string s = m.ToString();

                if (myusername != "")
                {
                    if (Session["UserName"] == null)
                    {
                        RetrieveUser(myusername);
                        Labeluser.Text = "here i am";
                    }

                    else
                    {
                       
                    }
                }


               // Response.Redirect("about.aspx");
            }

            Labeluser.Text = "made it here but why";


        }




        private void RetrieveUser(string myusername)
        {
            RIUser riuser = new RIUser();
            riuser = riuser.GetEmployee(myusername);

            //go make sure the user is in the DB
            if (riuser.Email != null)
            {
                //check for inactive
                if (riuser.InactiveFlag == "N")
                {
                    WriteToSession(riuser);
                    //LabelUserName.Text = "User: " + Session["LastName"] + ", " + Session["FirstName"] + " (" + Session["SiteID"].ToString() + ")";

                }
                else if (riuser.InactiveFlag == "Y")
                {
                    //do not proceed
                     throw new System.ArgumentException("Parameter cannot be null", "why is this not right?");
                }
            }
        }



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

            object m = null;
            string s = m.ToString();

            return (null);
        }



    }
}