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

namespace GPI.RI.Admin
{
    public partial class SiteMaster : MasterPage
    {
        GPI.MILL.DataAccess.Oracle.RetrieveData da = new RetrieveData();
        GPI.User.Model.LdapUser ldap = new LdapUser();
      
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


            if (Session["TestDatabase"] == null)

            {

                string SqlString = " SELECT DISTINCT UPPER(SERVICE_NAME) SERVICE_NAME FROM V$SESSION WHERE USER# IN (SELECT USER_ID FROM USER_USERS)";
                string ServiceName = "Database ({0})";
                ServiceName = string.Format(ServiceName, da.GetDatabaseName(SqlString));
                //Application.Add("ServiceName", ServiceName);
                LabelDatabase.Text = ServiceName;
                Session["DatabaseName"] = ServiceName;
                bool check = ServiceName.Contains("GPCIOD02");

                if (check)
                {
                    Session["TestDatabase"] = "YES";
                    LabelBannerWarning.Visible = true;

                }
                else
                {
                    Session["TestDatabase"] = "NO";
                    LabelBannerWarning.Visible = false;
                }

            }

            else

            {
                if (Session["TestDatabase"].ToString() == "YES")
                {
                    LabelBannerWarning.Visible = true;
                }
                else
                {
                    LabelBannerWarning.Visible = false;
                }

            }

            LabelDatabase.Text = Session["DatabaseName"].ToString();


        }



        protected void Page_Init(object sender, EventArgs e)
        {

            if (Session["UserName"] == null)
            {
                RIUser riuser = new RIUser();

                string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                string[] fullUsername = userName.Split(System.Convert.ToChar(@"\"));


                riuser = riuser.GetEmployee(fullUsername[1].ToUpper());
                //go make sure the user is in the DB
                if (riuser.Email != null)
                {
                    //check for inactive
                    if (riuser.InactiveFlag == "N")
                    {


                        //Session["riuser"] = riuser;
                        Session.Add("riuser", riuser);

                        WriteToSession(riuser);


                        //Session.Contents();/* TODO ERROR: Skipped SkippedTokensTrivia */



                    }
                    else if (riuser.InactiveFlag == "Y")
                    {
                        //do not proceed
                        return;
                    }
                }
            }


            LabelUserName.Text = "User: " + Session["LastName"] + ", " + Session["FirstName"] + " (" + Session["SiteID"].ToString() + ")";


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