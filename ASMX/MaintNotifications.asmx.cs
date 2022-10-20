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
        public List<ListItem> GetBusinessUnit()
        {
            string connection;
            string Provider;

            OracleConnection conCust = null/* TODO Change to default(_) if this is not a reference type */;
            OracleCommand cmdSql = null/* TODO Change to default(_) if this is not a reference type */;
            string Sql = null;
            Sql = "Select distinct risuperarea  From reladmin.notification_by_linesystem_vw where 1=1  and siteid = 'AU' ";

            Sql = "Select Distinct RISuperArea From TBLRISUPERAREA where bustype = 'PM' order by risuperarea";
            connection = ConfigurationManager.ConnectionStrings["connectionRCFATST"].ConnectionString;

            Provider = ConfigurationManager.ConnectionStrings["connectionRCFATST"].ProviderName;


            conCust = new OracleConnection(connection);
            conCust.Open();
            cmdSql = new OracleCommand(Sql, conCust);

            OracleDataReader dr;
            dr = cmdSql.ExecuteReader();

            List<ListItem> customers = new List<ListItem>();


            while (dr.Read())
                customers.Add(new ListItem()
                {
                    Value = dr.GetValue("risuperarea").ToString(),
                    Text = dr.GetValue("risuperarea").ToString()
                });


            return customers;
        }


        //[WebMethod]
        //public DropDownListData LoadBusinessUnit()
        //{
        //    string connection;
        //    string Provider;

        //    OracleConnection conCust = null/* TODO Change to default(_) if this is not a reference type */;
        //    OracleCommand cmdSql = null/* TODO Change to default(_) if this is not a reference type */;
        //    string Sql = null;
        //    Sql = "Select distinct risuperarea  From reladmin.notification_by_linesystem_vw where 1=1  and siteid = 'AU' ";

        //    connection = ConfigurationManager.ConnectionStrings["connectionRCFATST"].ConnectionString;
        //    Provider = ConfigurationManager.ConnectionStrings["connectionRCFATST"].ProviderName;

        //    conCust = new OracleConnection(connection);
        //    conCust.Open();
        //    cmdSql = new OracleCommand(Sql, conCust);

        //    OracleDataReader dr;
        //    dr = cmdSql.ExecuteReader();

        //    //Create a new DataTable.
        //    DataTable dtCustomers = new DataTable("Customers");
        //    //Load DataReader into the DataTable.
        //    dtCustomers.Load(dr);

        //    List<DropDownListItemData> result = new List<DropDownListItemData>();
        //    DropDownListData dropdownData = new DropDownListData();

        //    try
        //    {
        //        result = new List<DropDownListItemData>();

        //        for (int i = 0; i < dtCustomers.Rows.Count; i++)
        //        {
        //            DropDownListItemData itemData = new DropDownListItemData();

        //            itemData.Text = dtCustomers.Rows[i]["risuperarea"].ToString();
        //            itemData.Value = dtCustomers.Rows[i]["risuperarea"].ToString();
        //            result.Add(itemData);
        //        }
        //    }
        //    catch (Exception e)
        //    {

        //    }

        //    dropdownData.Items = result.ToArray();
        //    return dropdownData;
        //}




 //       nothing below this line
    }
}
