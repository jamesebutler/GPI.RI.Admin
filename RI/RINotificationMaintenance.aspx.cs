using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

using Telerik.Web.UI;
using System.Web.UI;


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

                // Load All Sites
                List<ListItem> list = new List<ListItem>();
                list = LoadAllSites();
                 foreach (var i in list)
                    {
                         RadComboBoxItem  item = new RadComboBoxItem ();
                        item.Text =  (i.Text);
                        item.Value = (i.Value);
                        DropDownSites.Items.Add(item);
                    
                    }
                DropDownSites.SelectedValue = "TS";
                //DropDownSites.Enabled = false;

                GetBusinessUnits();
                
            }




            // Create a 'WebRequest' object with the specified url. 

            //System.Net.WebRequest myWebRequest = WebRequest.Create("http://www.contoso.com");

            // Send the 'WebRequest' and wait for response.

            //WebResponse myWebResponse = myWebRequest.GetResponse();

            // Obtain a 'Stream' object associated with the response object.
            //Stream ReceiveStream = myWebResponse.GetResponseStream();



        }


        //protected void GetArea()
        //{
        //    //Load Business Unit from Site Selected
        //    DropDownArea.Items.Clear();
        //    List<ListItem> Arealist = new List<ListItem>();
        //    Arealist = LoadArea(DropDownSites.SelectedValue,DropDownBusinessUnit.SelectedValue);
        //    foreach (var i in Arealist)
        //    {
        //        RadComboBoxItem item = new RadComboBoxItem();
        //        item.Text = (i.Text);
        //        item.Value = (i.Value);
        //        DropDownArea.Items.Add(item);

        //    }
        //}
        protected void GetBusinessUnits()
        {
            //Load Business Unit from Site Selected
            DropDownBusinessUnit.Items.Clear();
            List<ListItem> Businesslist = new List<ListItem>();
            Businesslist = LoadBusinessUnits(DropDownSites.SelectedValue);
            foreach (var i in Businesslist)
            {
                RadComboBoxItem item = new RadComboBoxItem();
                item.Text = (i.Text);
                item.Value = (i.Value);
                DropDownBusinessUnit.Items.Add(item);

            }
        }

    protected void DropDownSites_SelectedIndexChanged(object sender, EventArgs e)
        {

           
           
            DropDownLineSystemType.SelectedIndex = -1;
           DropDownArea.SelectedIndex = -1;
           DropDownBusinessUnit.SelectedIndex = -1;

            GetBusinessUnits();


        }



        protected void DropDownBusinessUnit_SelectedIndexChanged(object sender, EventArgs  e)
        {
           
            DropDownLineSystemType.Items.Clear();
            DropDownArea.Items.Clear();
            DropDownLineSystemType.SelectedIndex = -1;
            DropDownArea.SelectedIndex = -1;



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



        public List<ListItem> LoadBusinessUnits(string GetBySiteId )
        {
            string connection;
            string Provider;


            OracleConnection conCust = null/* TODO Change to default(_) if this is not a reference type */;
            OracleCommand cmdSql = null/* TODO Change to default(_) if this is not a reference type */;
            string Sql = null;
            Sql = "select distinct risuperarea from refsitearea where bustype = 'PM'  and siteid = '" + GetBySiteId + "' order by risuperarea";
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
                    Value = dr.GetValue("risuperarea").ToString(),
                    Text = dr.GetValue("risuperarea").ToString()
                });


            return sites;
        }






        //nothing below this line
    }
}