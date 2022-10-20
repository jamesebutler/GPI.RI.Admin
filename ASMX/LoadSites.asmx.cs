using System.Collections.Generic;
using System.Configuration;
using System.Web.Services;
using System.Web.UI.WebControls;
using Devart.Data.Oracle;
using Telerik.Web.UI;
using Telerik;

namespace GPI.RI.Admin.ASMX
{
    /// <summary>
    /// Summary description for LoadSites
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
     [System.Web.Script.Services.ScriptService]
    public class LoadSites : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }


        [WebMethod]
        public List<ListItem> LoadAllSites()
        {
            string connection;
            string Provider;


            OracleConnection conCust = null/* TODO Change to default(_) if this is not a reference type */;
            OracleCommand cmdSql = null/* TODO Change to default(_) if this is not a reference type */;
            string Sql = null;
            Sql = "select siteid,sitename from refsite where domain = 'NA' and inactive_flag = 'N'";
            connection = ConfigurationManager.ConnectionStrings["connectionRCFATST"].ConnectionString;
            Provider = ConfigurationManager.ConnectionStrings["connectionRCFATST"].ProviderName;
            conCust = new OracleConnection(connection);
            conCust.Open();
            cmdSql = new OracleCommand(Sql, conCust);

            OracleDataReader dr;
            dr = cmdSql.ExecuteReader();

            List<ListItem> sites = new List<ListItem>();
            while (dr.Read())
                sites.Add(new ListItem()
                {
                    Value = dr.GetValue("siteid").ToString(),
                    Text = dr.GetValue("sitename").ToString()
                });


            return sites;
        }



        // nothing below this
    }
}
