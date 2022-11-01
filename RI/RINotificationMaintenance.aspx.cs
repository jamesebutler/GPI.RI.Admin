using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Diagnostics;
using System.Configuration;
using System.Web.UI;

using Telerik.Web.UI;
using Devart.Data.Oracle;
using GPI.MILL.DataAccess.Oracle;



namespace GPI.RI.Admin.MOC
{

    public partial class RINotificationMaintenance : System.Web.UI.Page
    {
        GPI.MILL.DataAccess.Oracle.RetrieveData da = new MILL.DataAccess.Oracle.RetrieveData();


        protected void Page_Load(object sender, EventArgs e)
        {


            if (!IsPostBack)
            {

                LoadSites();

                DropDownSites.SelectedValue = "TS";

            }



        }





    protected void DropDownSites_SelectedIndexChanged(object sender, EventArgs e)
        {

           
           //RadAjaxManager1.FocusControl(DropDownSites.ClientID + "_Input");
           //DropDownLineSystemType.SelectedIndex = -1;
           //DropDownArea.SelectedIndex = -1;
           //DropDownBusinessUnit.SelectedIndex = -1;

           // GetBusinessUnits();


        }



        protected void DropDownBusinessUnit_SelectedIndexChanged(object sender, EventArgs  e)
        {
            RadAjaxManager1.FocusControl(DropDownBusinessUnit.ClientID + "_Input");
        }


        protected void btnGetData_Click(object sender, EventArgs e)
        {

            if (DropDownBusinessUnit.SelectedValue == "")
            {
                LabelMissingText.Text = "No Business Unit has been selected.";
                //alertmessage.Visible = true;
            }
            else
            {
                LoadEmployees(DropDownSites.SelectedValue, DropDownBusinessUnit.SelectedValue, DropDownArea.SelectedValue, DropDownLineSystemType.SelectedValue, DropDownToCopy.SelectedValue);
                LoadAssignedEmployees(DropDownSites.SelectedValue, DropDownBusinessUnit.SelectedValue, DropDownArea.SelectedValue, DropDownLineSystemType.SelectedValue, DropDownToCopy.SelectedValue);
                //alertmessage.Visible = false;
            }


        
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

                LoadingRecords.Visible = true;
                string copyto = "(" + DropDownToCopy.SelectedText + ")";
                //Debug.WriteLine(RadListBoxDestination.Items.Count);
                for (int i = 0; i < RadListBoxDestination.Items.Count; i++)
                {
                    
                    if (RadListBoxDestination.Items[i].Text.ToLower().Contains(copyto.ToLower())) 
                    {
                        //do nothing 

                    }
                    else
                    {
                        SaveNotification(DropDownSites.SelectedValue,RadListBoxDestination.Items[i].Value, DropDownBusinessUnit.SelectedValue, DropDownArea.SelectedValue, DropDownLineSystemType.SelectedValue, DropDownToCopy.SelectedValue);
                    }

                }

                for (int i = 0; i < RadListBoxSource.Items.Count; i++)
                {

                    if (RadListBoxSource.Items[i].Text.ToLower().Contains(copyto.ToLower()))
                    {
                        DeleteNotification(DropDownSites.SelectedValue, RadListBoxSource.Items[i].Value, DropDownBusinessUnit.SelectedValue, DropDownArea.SelectedValue, DropDownLineSystemType.SelectedValue, DropDownToCopy.SelectedValue);

                    }
                }


            }

            LoadEmployees(DropDownSites.SelectedValue, DropDownBusinessUnit.SelectedValue, DropDownArea.SelectedValue, DropDownLineSystemType.SelectedValue, DropDownToCopy.SelectedValue);

            LoadAssignedEmployees(DropDownSites.SelectedValue, DropDownBusinessUnit.SelectedValue, DropDownArea.SelectedValue, DropDownLineSystemType.SelectedValue, DropDownToCopy.SelectedValue);

            LoadingRecords.Visible = false;



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
            if (GetByLineSystemType == "")
            {
                GetByLineSystemType = "All";
            }
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

        public void DeleteNotification(string GetBySiteId, string GetByUserName, string GetByBusinessUnit, string GetByArea, string GetByLineSystemType, string GetBynotifytype)
        {
            //insert into tblrcfanotification (siteid, username, risuperarea, subarea, area, notifytype) 

            //" values('" & txtSiteid & "','" & Grouplist(I) & "','" & txtSuperArea & "','" & txtArea & "','" & txtLineSystem & "','" & txtNotifytype & "')"

            string connection;
            string Provider;

            OracleConnection conCust = null/* TODO Change to default(_) if this is not a reference type */;
            OracleCommand cmdSql = null/* TODO Change to default(_) if this is not a reference type */;
            string Sql = null;

            //, , , , , 
            StringBuilder SQLbuilder = new StringBuilder();
            SQLbuilder.Append("DELETE tblrcfanotification WHERE 1=1");
            SQLbuilder.Append(" AND siteid = '" + GetBySiteId + "'");
            SQLbuilder.Append(" AND username = '" + GetByUserName + "'");
            SQLbuilder.Append(" AND risuperarea = '" + GetByBusinessUnit + "'");
            SQLbuilder.Append(" AND subarea = '" + GetByArea + "'");
            if (GetByLineSystemType == "")
            {
                SQLbuilder.Append(" AND area = 'All'");
            }
            else
            {
                SQLbuilder.Append(" AND area = '" + GetByLineSystemType + "'");
            }
            SQLbuilder.Append(" AND notifytype = '" + GetBynotifytype + "'");
            Sql = SQLbuilder.ToString();
            connection = ConfigurationManager.ConnectionStrings["connectionRCFATST"].ConnectionString;
            Provider = ConfigurationManager.ConnectionStrings["connectionRCFATST"].ProviderName;
            conCust = new OracleConnection(connection);
            conCust.Open();
            cmdSql = new OracleCommand(Sql, conCust);


            cmdSql.ExecuteNonQuery();

        }

        //********************************************

        protected void LoadSites()
        {
            string Sql = null;
            Sql = "select siteid,sitename from refsite where domain = 'NA' and inactive_flag = 'N'";

            OracleDataReader dr;
            dr = da.GetOracleDataReader(Sql);
            //Create a new DataTable.
            DataTable dt = new DataTable();
            //Load DataReader into the DataTable.
            dt.Load(dr);
  

            DropDownSites.DataTextField = "sitename";
            DropDownSites.DataValueField = "siteid";
            DropDownSites.DataSource = dt;
            DropDownSites.DataBind();
            //insert the first item
            //DropDownSites.Items.Insert(0, new RadComboBoxItem("- Select a continent -"));

            RadAjaxManager1.FocusControl(DropDownSites);
        }


        protected void LoadBusinessUnits(string GetBySiteId)
        {
            string Sql = null;
            //Sql = "select distinct risuperarea from refsitearea where bustype = 'PM'  and siteid = '" + GetBySiteId + "' order by risuperarea";
            Sql = "Select distinct risuperarea  From TBLRISUPERAREA where bustype = 'PM' order by risuperarea";


            OracleDataReader dr;
            dr = da.GetOracleDataReader(Sql);

            //Create a new DataTable.
            DataTable dt = new DataTable();
            //Load DataReader into the DataTable.
            dt.Load(dr);


            DropDownBusinessUnit.DataTextField = "risuperarea";
            DropDownBusinessUnit.DataValueField = "risuperarea";
            DropDownBusinessUnit.DataSource = dt;
            DropDownBusinessUnit.DataBind();
            //insert the first item
            DropDownBusinessUnit.Items.Insert(0, new RadComboBoxItem("Select one..."));

            RadAjaxManager1.FocusControl(DropDownBusinessUnit);
        }



        protected void LoadArea(string GetBySiteId, string GetByBusinessUnit)
        {

            string Sql = null;
            //Sql = "select distinct a.subarea from refsitearea a where 1=1 and a.bustype = 'PM'  and a.risuperarea = '" + businessunit + "' and a.siteid = '" + siteid + "'  order by a.subarea";
            Sql = "Select Distinct SubArea From TBLRISUPERAREA where bustype = 'PM'  and risuperarea = '" + GetByBusinessUnit + "'  order by subarea";

            Debug.WriteLine(Sql);

            OracleDataReader dr;
            //Create a new DataTable.
            dr = da.GetOracleDataReader(Sql);

            DataTable dt = new DataTable();
            //Load DataReader into the DataTable.
            dt.Load(dr);

            DropDownArea.DataTextField = "SubArea";
            DropDownArea.DataValueField = "SubArea";
            DropDownArea.DataSource = dt;
            DropDownArea.DataBind();
            //insert the first item
            DropDownArea.Items.Insert(0, new RadComboBoxItem("Select..."));

            RadAjaxManager1.FocusControl(DropDownArea);
        }


        protected void LoadLine(string GetBySiteId, string GetByBusinessUnit, string GetByArea)
        {

            string Sql = null;
            Sql = "select distinct a.area from refsitearea a where 1=1 and a.bustype = 'PM'  and a.risuperarea = '" + GetByBusinessUnit + "' and a.siteid = '" + GetBySiteId + "'" + " and a.subarea = '" + GetByArea + "' order by a.area";
            Debug.WriteLine(Sql);

            OracleDataReader dr;
            //Create a new DataTable.
            dr = da.GetOracleDataReader(Sql);

            DataTable dt = new DataTable();
            //Load DataReader into the DataTable.
            dt.Load(dr);


            DropDownLineSystemType.DataTextField = "area";
            DropDownLineSystemType.DataValueField = "area";
            DropDownLineSystemType.DataSource = dt;
            DropDownLineSystemType.DataBind();
            //insert the first item
            DropDownLineSystemType.Items.Insert(0, new RadComboBoxItem("All"));

            RadAjaxManager1.FocusControl(DropDownLineSystemType);
        }

        protected void LoadEmployees(string GetBySiteId, string GetByBusinessUnit, string GetByArea, string GetByLineSystemType, string GetBynotifytype)
        {

            string Sql = null;
            StringBuilder SQLbuilder = new StringBuilder();
            SQLbuilder.Append("Select distinct lastname, firstname, INITCAP(lastname) || ', ' || INITCAP(firstname) as fullname,username From Refemployee");
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

            OracleDataReader dr;
            //Create a new DataTable.
            dr = da.GetOracleDataReader(Sql);

            DataTable dt = new DataTable();
            //Load DataReader into the DataTable.
            dt.Load(dr);


            RadListBoxSource.DataTextField = "fullname";
            RadListBoxSource.DataValueField = "username";
            RadListBoxSource.DataSource = dt;
            RadListBoxSource.DataBind();

            //RadAjaxManager1.FocusControl(RadListBoxSource);
        }

        protected void LoadAssignedEmployees(string GetBySiteId, string GetByBusinessUnit, string GetByArea, string GetByLineSystemType, string GetBynotifytype)
        {

            string Sql = null;
            StringBuilder SQLbuilder = new StringBuilder();
            SQLbuilder.Append("Select distinct lastname, firstname, INITCAP(lastname) || ', ' || INITCAP(firstname) || '  (' ||  decode(notifytype,'T','To','C','Copy','Copy') || + ')' as fullname, username, decode(notifytype,'T','To','C','Copy','Copy') notifytype From reladmin.notification_by_linesystem_vw");
            SQLbuilder.Append(" WHERE SITEID = '" + GetBySiteId + "'");
            SQLbuilder.Append(" and (risuperarea = '" + GetByBusinessUnit + "' or risuperarea = 'All') ");
            SQLbuilder.Append(" and (subarea = '" + GetByArea + "' or subarea = 'All') ");
            SQLbuilder.Append(" and (area = '" + GetByLineSystemType + "' or area = 'All')");
            SQLbuilder.Append(" and notifytype = '" + GetBynotifytype + "'");
            SQLbuilder.Append(" Order By Lastname, firstname");
            Sql = SQLbuilder.ToString();

            OracleDataReader dr;
            //Create a new DataTable.
            dr = da.GetOracleDataReader(Sql);

            DataTable dt = new DataTable();
            //Load DataReader into the DataTable.
            dt.Load(dr);


            RadListBoxDestination.DataTextField = "fullname";
            RadListBoxDestination.DataValueField = "username";
            RadListBoxDestination.DataSource = dt;
            RadListBoxDestination.DataBind();

            //RadAjaxManager1.FocusControl(RadListBoxDestination);
        }


        protected void DropDownSites_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            RadAjaxManager1.FocusControl(DropDownBusinessUnit.ClientID + "_Input");

        }

        protected void DropDownBusinessUnit_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
           // RadAjaxManager1.FocusControl(DropDownArea.ClientID + "_Input");
       
        }


        protected void DropDownSites_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
             LoadSites();
        }

        protected void DropDownBusinessUnit_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            DropDownArea.Items.Clear();
            DropDownLineSystemType.Items.Clear();
            DropDownArea.SelectedIndex = -1;


            LoadBusinessUnits(DropDownSites.SelectedValue);
        }


        protected void DropDownArea_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            DropDownLineSystemType.Items.Clear();
            LoadArea(DropDownSites.SelectedValue,DropDownBusinessUnit.SelectedValue);
        }


        protected void DropDownLineSystemType_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            LoadLine(DropDownSites.SelectedValue, DropDownBusinessUnit.SelectedValue,DropDownArea.SelectedValue);
        }

        //nothing below this line
    }
}