using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

using System.Configuration;

using Devart.Data.Oracle;

namespace GPI.RI.Admin.MOC
{
    public partial class RINotificationMaintenance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //load defaults only for the first time,
                //not every time page loaded


      

                List<ListItem> list = new List<ListItem>();
                list = LoadAllSites();

             foreach (var i in list)
                {
                     RadComboBoxItem  item = new RadComboBoxItem ();
                    item.Text =  (i.Text);
                    item.Value = (i.Value);
                    DropDownSites.Items.Add(item);
                    
                }


            }
            
            // Create a 'WebRequest' object with the specified url. 

            //System.Net.WebRequest myWebRequest = WebRequest.Create("http://www.contoso.com");

            // Send the 'WebRequest' and wait for response.
            
            //WebResponse myWebResponse = myWebRequest.GetResponse();

            // Obtain a 'Stream' object associated with the response object.
            //Stream ReceiveStream = myWebResponse.GetResponseStream();

            

        }



        protected void DropDownBusinessUnit_SelectedIndexChanged(object sender, DropDownListEventArgs e)
        {
            DropDownLineSystemType.SelectedIndex = -1;
        }

        protected void DropDownArea_SelectedIndexChanged(object sender, DropDownListEventArgs e)
        {
            DropDownLineSystemType.SelectedIndex = -1;
        }

        protected void btnGetData_Click(object sender, EventArgs e)
        {
            OrderTable.Visible = true;
            labelMill.Text = DropDownSites.SelectedValue;
            
            labelBusinessType.Text = DropDownBusinessUnit.SelectedValue;
            labelBusinessArea.Text = DropDownArea.SelectedText;
            labelBusinessLine.Text = DropDownLineSystemType.SelectedText;
        }


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






        public DropDownListData LoadAllSitesDropDown()
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

            List<DropDownListItemData> result = new List<DropDownListItemData>();
            DropDownListData dropdownData = new DropDownListData();
           
            try
            {
                result = new List<DropDownListItemData>();

                while (dr.Read())
                
                {
                     DropDownListItemData itemData = new DropDownListItemData();

                    itemData.Text = dr.GetValue("siteid").ToString();
                    itemData.Value = dr.GetValue("sitename").ToString();
                    result.Add(itemData);
                    }
            }
            catch (Exception e)
            {

            }

            dropdownData.Items = result.ToArray();

            return dropdownData;
        }





        //nothing below this line
    }
}