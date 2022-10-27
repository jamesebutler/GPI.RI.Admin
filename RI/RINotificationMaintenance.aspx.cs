using System;
using System.Text;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Diagnostics;
using System.Configuration;
using System.Web.UI;

using Telerik.Web.UI;
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
                

                GetBusinessUnits();

                //DropDownSites.Enabled = false;
                
            }




            // Create a 'WebRequest' object with the specified url. 

            //System.Net.WebRequest myWebRequest = WebRequest.Create("http://www.contoso.com");

            // Send the 'WebRequest' and wait for response.

            //WebResponse myWebResponse = myWebRequest.GetResponse();

            // Obtain a 'Stream' object associated with the response object.
            //Stream ReceiveStream = myWebResponse.GetResponseStream();



        }


        protected void GetEmployees()
        {
            //Load Business Unit from Site Selected
            RadListBoxSource.Items.Clear();
            List<ListItem> Employeelist = new List<ListItem>();
            Employeelist = LoadEmployees(DropDownSites.SelectedValue, DropDownBusinessUnit.SelectedValue, DropDownArea.SelectedValue,DropDownLineSystemType.SelectedValue,DropDownToCopy.SelectedValue);
            foreach (var i in Employeelist)
            {
                RadListBoxItem item = new RadListBoxItem();
                item.Text = (i.Text);
                item.Value = (i.Value);
                RadListBoxSource.Items.Add(item);

            }
        }


        protected void GetAssignedEmployees()
        {
            //Load Business Unit from Site Selected
            RadListBoxDestination.Items.Clear();
            List<ListItem> Employeelist = new List<ListItem>();
            Employeelist = LoadAssignedEmployees(DropDownSites.SelectedValue, DropDownBusinessUnit.SelectedValue, DropDownArea.SelectedValue,DropDownLineSystemType.SelectedValue,DropDownToCopy.SelectedValue);
            foreach (var i in Employeelist)
            {
                RadListBoxItem item = new RadListBoxItem();
                item.Text = (i.Text);
                item.Value = (i.Value);
                RadListBoxDestination.Items.Add(item);

            }
        }

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
            //OrderTable.Visible = true;
            //labelMill.Text = DropDownSites.SelectedValue;
            
            //labelBusinessType.Text = DropDownBusinessUnit.SelectedValue;
            //labelBusinessArea.Text = DropDownArea.SelectedText;
            //labelBusinessLine.Text = DropDownLineSystemType.SelectedText;

            GetEmployees();
            GetAssignedEmployees();

            alertmessage.Visible = false;
        
        }


        protected void btnSaveNotification_Click(object sender, EventArgs e)
        {
            if (DropDownArea.SelectedValue == "")
            {
                alertmessage.Visible = true;
            }
            else
            {
                alertmessage.Visible = false;

                Debug.WriteLine(RadListBoxDestination.Items.Count);
                for (int i = 0; i < RadListBoxDestination.Items.Count; i++)
                {
                    Debug.WriteLine(RadListBoxDestination.Items[i].Text);
                    Debug.WriteLine(RadListBoxDestination.Items[i].Value);
                    SaveNotification(DropDownSites.SelectedValue,RadListBoxDestination.Items[i].Value, DropDownBusinessUnit.SelectedValue, DropDownArea.SelectedValue, DropDownLineSystemType.SelectedValue, DropDownToCopy.SelectedValue);

                }


            }



            //OrderTable.Visible = true;
            //labelMill.Text = DropDownSites.SelectedValue;

            //labelBusinessType.Text = DropDownBusinessUnit.SelectedValue;
            //labelBusinessArea.Text = DropDownArea.SelectedText;
            //labelBusinessLine.Text = DropDownLineSystemType.SelectedText;


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
            //Sql = "select distinct risuperarea from refsitearea where bustype = 'PM'  and siteid = '" + GetBySiteId + "' order by risuperarea";
            Sql = "Select distinct risuperarea  From TBLRISUPERAREA where bustype = 'PM' order by risuperarea";

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


       public List<ListItem> LoadEmployees(string GetBySiteId, string GetByBusinessUnit, string GetByArea, string GetByLineSystemType, string GetBynotifytype)
        {
            string connection;
            string Provider;


            OracleConnection conCust = null/* TODO Change to default(_) if this is not a reference type */;
            OracleCommand cmdSql = null/* TODO Change to default(_) if this is not a reference type */;
            string Sql = null;
            StringBuilder SQLbuilder = new StringBuilder();
            SQLbuilder.Append("Select distinct lastname, firstname, lastname || ', ' || firstname as fullname,username From Refemployee");
            SQLbuilder.Append(" WHERE SITEID = '" + GetBySiteId + "'");
            SQLbuilder.Append(" AND (inactive_flag is null or inactive_flag <> 'Y') and username not in");
            SQLbuilder.Append(" (Select distinct username From reladmin.notification_by_linesystem_vw");
            SQLbuilder.Append(" WHERE SITEID = '" + GetBySiteId + "'");
            SQLbuilder.Append(" and (risuperarea = '" + GetByBusinessUnit + "' or risuperarea = 'All') ");
            SQLbuilder.Append(" and (subarea = '" + GetByArea + "' or subarea = 'All') ");
            SQLbuilder.Append(" and (area = '" + GetByLineSystemType + "' or area = 'All')");
            SQLbuilder.Append(" and notifytype = '" + GetBynotifytype + "'");

            SQLbuilder.Append(")"); 
            SQLbuilder.Append(" Order By Lastname, firstname");


            Sql = SQLbuilder.ToString();
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
                    Value = dr.GetValue("username").ToString(),
                    Text = dr.GetValue("fullname").ToString()
                });


            return sites;
        }


        public List<ListItem> LoadAssignedEmployees(string GetBySiteId, string GetByBusinessUnit, string GetByArea, string GetByLineSystemType, string GetBynotifytype)
        {
            string connection;
            string Provider;


            OracleConnection conCust = null/* TODO Change to default(_) if this is not a reference type */;
            OracleCommand cmdSql = null/* TODO Change to default(_) if this is not a reference type */;
            string Sql = null;
            StringBuilder SQLbuilder = new StringBuilder();
            SQLbuilder.Append("Select distinct lastname, firstname, lastname || ', ' || firstname || '  (' ||  decode(notifytype,'T','To','C','Copy','Copy') || + ')' as fullname, username, decode(notifytype,'T','To','C','Copy','Copy') notifytype From reladmin.notification_by_linesystem_vw");
            SQLbuilder.Append(" WHERE SITEID = '" + GetBySiteId + "'");
            SQLbuilder.Append(" and (risuperarea = '" + GetByBusinessUnit + "' or risuperarea = 'All') ");
            SQLbuilder.Append(" and (subarea = '" + GetByArea + "' or subarea = 'All') ");
            SQLbuilder.Append(" and (area = '" + GetByLineSystemType + "' or area = 'All')");
            SQLbuilder.Append(" and notifytype = '" + GetBynotifytype + "'");
            SQLbuilder.Append(" Order By Lastname, firstname");


            Sql = SQLbuilder.ToString();
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
                    Value = dr.GetValue("username").ToString(),
                    Text = dr.GetValue("fullname").ToString()
                });


            return sites;
        }


        public void SaveNotification(string GetBySiteId,string GetByUserName, string GetByBusinessUnit, string GetByArea, string GetByLineSystemType, string GetBynotifytype)
        {
            //insert into tblrcfanotification (siteid, username, risuperarea, subarea, area, notifytype) 

            //" values('" & txtSiteid & "','" & Grouplist(I) & "','" & txtSuperArea & "','" & txtArea & "','" & txtLineSystem & "','" & txtNotifytype & "')"

            string connection;
            string Provider;
            
            OracleConnection conCust = null/* TODO Change to default(_) if this is not a reference type */;
            OracleCommand cmdSql = null/* TODO Change to default(_) if this is not a reference type */;
            string Sql = null;


            StringBuilder SQLbuilder = new StringBuilder();
            SQLbuilder.Append("insert into tblrcfanotification (siteid, username, risuperarea, subarea, area, notifytype)");
            SQLbuilder.Append(" values(");
            SQLbuilder.Append("'" + GetBySiteId + "',");
            SQLbuilder.Append("'" + GetByUserName + "',");
            SQLbuilder.Append("'" + GetByBusinessUnit + "',");
            SQLbuilder.Append("'" + GetByArea + "',");
            SQLbuilder.Append("'" + GetByLineSystemType + "',");
            SQLbuilder.Append("'" + GetBynotifytype + "'");

            SQLbuilder.Append(" )");


            Sql = SQLbuilder.ToString();
            connection = ConfigurationManager.ConnectionStrings["connectionRCFATST"].ConnectionString;
            Provider = ConfigurationManager.ConnectionStrings["connectionRCFATST"].ProviderName;
            conCust = new OracleConnection(connection);
            conCust.Open();
            cmdSql = new OracleCommand(Sql, conCust);

            
            cmdSql.ExecuteNonQuery();



        }


        //nothing below this line
    }
}