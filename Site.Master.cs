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
        GPI.User.Model.CurrentUserProfile currentprofile = new GPI.User.Model.CurrentUserProfile();
       
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



            string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            string[] fullUsername = userName.Split(System.Convert.ToChar(@"\"));

            
             
            
            LabelUserName.Text = "User: " + fullUsername[1];

          


             




        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            Page.Theme = "RIPurple";
        }

        protected void Page_Init(object sender, EventArgs e)
        {
         

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
                {
                    LabelBannerWarning.Visible = false;
                }

            }

            LabelDatabase.Text = Session["DatabaseName"].ToString();

        }




        //public static string CreateXMLUserInfo(userprofile as currentprofile)
        //{

        //    // write results to session xml
        //    XmlWriterSettings settings = new XmlWriterSettings();
        //    {
        //        var withBlock = settings;
        //        withBlock.CloseOutput = true;
        //        withBlock.Encoding = Encoding.UTF8;
        //        withBlock.Indent = false;
        //        withBlock.OmitXmlDeclaration = true;
        //    }
        //    System.IO.StringWriter sw = new System.IO.StringWriter();
        //    using (XmlWriter xw = XmlWriter.Create(sw, settings))
        //    {
        //        {
        //            var withBlock = xw;
        //            withBlock.WriteStartDocument();
        //            withBlock.WriteStartElement("UserProfileInfo");

        //            withBlock.WriteStartElement("AuthLevel");
        //            withBlock.WriteValue(userprofile.AuthLevel);
        //            withBlock.WriteEndElement();

        //            withBlock.WriteStartElement("AuthLevelID");
        //            withBlock.WriteValue(userprofile.AuthLevelID);
        //            withBlock.WriteEndElement();

        //            withBlock.WriteStartElement("BusType");
        //            withBlock.WriteValue(userprofile.BusType);
        //            withBlock.WriteEndElement();

        //            withBlock.WriteStartElement("DefaultDivision");
        //            withBlock.WriteValue(userprofile.DefaultDivision);
        //            withBlock.WriteEndElement();

        //            withBlock.WriteStartElement("DefaultFacility");
        //            withBlock.WriteValue(userprofile.DefaultFacility);
        //            withBlock.WriteEndElement();

        //            withBlock.WriteStartElement("DefaultLanguage");
        //            withBlock.WriteValue(userprofile.DefaultLanguage);
        //            withBlock.WriteEndElement();

        //            withBlock.WriteStartElement("DistinguishedName");
        //            withBlock.WriteValue(userprofile.DistinguishedName);
        //            withBlock.WriteEndElement();

        //            withBlock.WriteStartElement("DivestedLocation");
        //            withBlock.WriteValue(userprofile.DivestedLocation);
        //            withBlock.WriteEndElement();

        //            withBlock.WriteStartElement("DomainName");
        //            withBlock.WriteValue(userprofile.DomainName);
        //            withBlock.WriteEndElement();

        //            withBlock.WriteStartElement("Email");
        //            withBlock.WriteValue(userprofile.Email);
        //            withBlock.WriteEndElement();

        //            withBlock.WriteStartElement("FullName");
        //            withBlock.WriteValue(userprofile.FullName);
        //            withBlock.WriteEndElement();

        //            withBlock.WriteStartElement("GroupName");
        //            withBlock.WriteValue(userprofile.GroupName);
        //            withBlock.WriteEndElement();

        //            withBlock.WriteStartElement("InActiveFlag");
        //            withBlock.WriteValue(userprofile.InActiveFlag);
        //            withBlock.WriteEndElement();

        //            withBlock.WriteStartElement("ProfileTable");
        //            withBlock.WriteValue(userprofile.ProfileTable);
        //            withBlock.WriteEndElement();

        //            withBlock.WriteStartElement("Username");
        //            withBlock.WriteValue(userprofile.Username);
        //            withBlock.WriteEndElement();

        //            withBlock.WriteStartElement("ImpersonateUsername");
        //            withBlock.WriteValue("nothing");
        //            withBlock.WriteEndElement();

        //            withBlock.WriteStartElement("PageTheme");
        //            withBlock.WriteValue(userprofile.PageTheme);
        //            withBlock.WriteEndElement();

        //            withBlock.WriteEndElement();
        //            withBlock.WriteEndDocument();
        //        }
        //    }


        //    return sw.ToString();
        //}



        // nothing below this line
    }
}