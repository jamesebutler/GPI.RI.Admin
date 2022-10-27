using System.Collections.Generic;
using System.Configuration;
using System.Web.Services;
using System.Web.UI.WebControls;
using System.Diagnostics;
using Devart.Data.Oracle;
using Telerik.Web.UI;
using Telerik;


namespace GPI.RI.Admin.ASMX
{
    /// <summary>
    /// Summary description for MaintNotifications
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class MaintNotifications : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }



        [WebMethod]
        public List<ListItem> LoadBusinessUnit()
        {
            string connection;
            string Provider;

            OracleConnection conCust = null/* TODO Change to default(_) if this is not a reference type */;
            OracleCommand cmdSql = null/* TODO Change to default(_) if this is not a reference type */;
            string Sql = null;
            Sql = "select distinct risuperarea from refsitearea where bustype = 'PM'  and siteid = 'AU'  order by risuperarea";
            Debug.WriteLine(Sql);
            connection = ConfigurationManager.ConnectionStrings["connectionRCFATST"].ConnectionString;
            Provider = ConfigurationManager.ConnectionStrings["connectionRCFATST"].ProviderName;
            conCust = new OracleConnection(connection);
            conCust.Open();
            cmdSql = new OracleCommand(Sql, conCust);

            OracleDataReader dr;
            dr = cmdSql.ExecuteReader();

            List<ListItem> Unit = new List<ListItem>();
            while (dr.Read())
                Unit.Add(new ListItem()
                {
                    Value = dr.GetValue("risuperarea").ToString(),
                    Text = dr.GetValue("risuperarea").ToString()
                });


            return Unit;
        }




        [WebMethod]
        public List<ListItem> LoadArea(DropDownListContext  context) 
        {
            string connection;
            string Provider;

 

            var businessunit = context.UserContext["businessunit"];
            var siteid = context.UserContext["siteid"];

            OracleConnection conCust = null/* TODO Change to default(_) if this is not a reference type */;
            OracleCommand cmdSql = null/* TODO Change to default(_) if this is not a reference type */;
            string Sql = null;
            //Sql = "select distinct a.subarea from refsitearea a where 1=1 and a.bustype = 'PM'  and a.risuperarea = '" + businessunit + "' and a.siteid = '" + siteid + "'  order by a.subarea";
            Sql = "Select Distinct SubArea From TBLRISUPERAREA where bustype = 'PM'  and risuperarea = '" + businessunit +  "'  order by subarea";

            Debug.WriteLine(Sql);
            connection = ConfigurationManager.ConnectionStrings["connectionRCFATST"].ConnectionString;
            Provider = ConfigurationManager.ConnectionStrings["connectionRCFATST"].ProviderName;
            conCust = new OracleConnection(connection);
            conCust.Open();
            cmdSql = new OracleCommand(Sql, conCust);

            OracleDataReader dr;
            dr = cmdSql.ExecuteReader();

            List<ListItem> areas = new List<ListItem>();

            areas.Add(new ListItem()
            {
                Value = "All",
                Text = "All"
            });


            while (dr.Read())
                areas.Add(new ListItem()
                {
                    Value = dr.GetValue("subarea").ToString(),
                    Text = dr.GetValue("subarea").ToString()
                });


            return areas;
        }

         [WebMethod]
        public List<ListItem> LoadLine(DropDownListContext context)
        {
            string connection;
            string Provider;



            var businessunit = context.UserContext["businessunit"];
            var siteid = context.UserContext["siteid"];
            var area = context.UserContext["area"];

            OracleConnection conCust = null/* TODO Change to default(_) if this is not a reference type */;
            OracleCommand cmdSql = null/* TODO Change to default(_) if this is not a reference type */;
            string Sql = null;
            Sql = "select distinct a.area from refsitearea a where 1=1 and a.bustype = 'PM'  and a.risuperarea = '" + businessunit + "' and a.siteid = '" + siteid + "'" + " and a.subarea = '" + area + "' order by a.area";
            Debug.WriteLine(Sql);
            connection = ConfigurationManager.ConnectionStrings["connectionRCFATST"].ConnectionString;
            Provider = ConfigurationManager.ConnectionStrings["connectionRCFATST"].ProviderName;
            conCust = new OracleConnection(connection);
            conCust.Open();
            cmdSql = new OracleCommand(Sql, conCust);

            OracleDataReader dr;
            dr = cmdSql.ExecuteReader();

            List<ListItem> line = new List<ListItem>();
            line.Add(new ListItem()
            {
                Value = "All",
                Text = "All"
            });

            while (dr.Read())
                line.Add(new ListItem()
                {
                    Value = dr.GetValue("area").ToString(),
                    Text = dr.GetValue("area").ToString()
                });


            return line;
        }

        [WebMethod]
        public List<ListItem> LoadEmployeeByArea()
        {
            string connection;
            string Provider;

            OracleConnection conCust = null/* TODO Change to default(_) if this is not a reference type */;
            OracleCommand cmdSql = null/* TODO Change to default(_) if this is not a reference type */;
            string Sql = null;
            Sql = "Select * From reladmin.notification_by_linesystem_vw where 1=1 and siteid =  'AU' and (risuperarea = 'Pulp') order by lastname";
            Debug.WriteLine(Sql);
            connection = ConfigurationManager.ConnectionStrings["connectionRCFATST"].ConnectionString;
            Provider = ConfigurationManager.ConnectionStrings["connectionRCFATST"].ProviderName;
            conCust = new OracleConnection(connection);
            conCust.Open();
            cmdSql = new OracleCommand(Sql, conCust);

            OracleDataReader dr;
            dr = cmdSql.ExecuteReader();

            List<ListItem> employees = new List<ListItem>();
            while (dr.Read())
                employees.Add(new ListItem()
                {
                    Value = dr.GetValue("risuperarea").ToString(),
                    Text = dr.GetValue("risuperarea").ToString()
                });


            return employees;
        }





        //       nothing below this line
    }
}
