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

            LabelShowAssignments.Text = "";

            if (DropDownBusinessUnit.SelectedValue == "")
            {
                Session["EmployeeRecords"] = null;
                RadGridEmployees.DataSource = null;
                RadGridEmployees.Rebind();
                LabelShowAssignments.Text = "No Business Unit has been selected.";
                //alertmessage.Visible = true;
            }
            else
            {
                LoadEmployees(DropDownSites.SelectedValue, DropDownBusinessUnit.SelectedValue, DropDownArea.SelectedValue, DropDownLineSystemType.SelectedValue, "");
                LoadAssignedEmployees(DropDownSites.SelectedValue, DropDownBusinessUnit.SelectedValue, DropDownArea.SelectedValue, DropDownLineSystemType.SelectedValue, "");
                //alertmessage.Visible = false;
            }


        
        }


        protected void btnSaveNotification_Click(object sender, EventArgs e)
        {
            //if (DropDownArea.SelectedValue == "")
            //{
            //    alertmessage.Visible = true;
            //}
            //else
            //{
            //    alertmessage.Visible = false;

            //    LoadingRecords.Visible = true;
            //    string copyto = "(" + DropDownToCopy.SelectedText + ")";
            //    //Debug.WriteLine(RadListBoxDestination.Items.Count);
            //    for (int i = 0; i < RadListBoxDestination.Items.Count; i++)
            //    {
                    
            //        if (RadListBoxDestination.Items[i].Text.ToLower().Contains(copyto.ToLower())) 
            //        {
            //            //do nothing 

            //        }
            //        else
            //        {
            //            SaveNotification(DropDownSites.SelectedValue,RadListBoxDestination.Items[i].Value, DropDownBusinessUnit.SelectedValue, DropDownArea.SelectedValue, DropDownLineSystemType.SelectedValue, DropDownToCopy.SelectedValue);
            //        }

            //    }

            //    for (int i = 0; i < RadListBoxSource.Items.Count; i++)
            //    {

            //        if (RadListBoxSource.Items[i].Text.ToLower().Contains(copyto.ToLower()))
            //        {
            //            DeleteNotification(DropDownSites.SelectedValue, RadListBoxSource.Items[i].Value, DropDownBusinessUnit.SelectedValue, DropDownArea.SelectedValue, DropDownLineSystemType.SelectedValue, DropDownToCopy.SelectedValue);

            //        }
            //    }


            //}

            //LoadEmployees(DropDownSites.SelectedValue, DropDownBusinessUnit.SelectedValue, DropDownArea.SelectedValue, DropDownLineSystemType.SelectedValue, DropDownToCopy.SelectedValue);

            //LoadAssignedEmployees(DropDownSites.SelectedValue, DropDownBusinessUnit.SelectedValue, DropDownArea.SelectedValue, DropDownLineSystemType.SelectedValue, DropDownToCopy.SelectedValue);

            //LoadingRecords.Visible = false;



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

            try
            {



                string Sql = null;
                StringBuilder SQLbuilder = new StringBuilder();
                SQLbuilder.Append("Select distinct lastname, firstname, INITCAP(lastname) || ', ' || INITCAP(firstname) || '  (' ||  decode(notifytype,'T','To','C','Copy','Copy') || + ')' as fullname, username, decode(notifytype,'T','To','C','Copy','Copy') notifytype From reladmin.notification_by_linesystem_vw");
                SQLbuilder.Append(" WHERE SITEID = '" + GetBySiteId + "'");
                SQLbuilder.Append(" and (risuperarea = '" + GetByBusinessUnit + "' or risuperarea = 'All') ");
                SQLbuilder.Append(" and (subarea = '" + GetByArea + "' or subarea = 'All') ");
                SQLbuilder.Append(" and (area = '" + GetByLineSystemType + "' or area = 'All')");
                //SQLbuilder.Append(" and notifytype = '" + GetBynotifytype + "'");
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



                //testing new way using grid

                string Sql1 = null;
                StringBuilder SQLbuilder1 = new StringBuilder();

                SQLbuilder1.Append(" Select  '1', ");
                SQLbuilder1.Append(" lastname, firstname, risuperarea,subarea,area,");
                SQLbuilder1.Append(" INITCAP(lastname) || ', ' || INITCAP(firstname)  as fullname, ");
                SQLbuilder1.Append(" username, decode(notifytype,'T','To','C','Copy','Copy') notifytype , siteid ");
                SQLbuilder1.Append(" From reladmin.notification_by_linesystem_vw ");
                SQLbuilder1.Append(" WHERE SITEID = '" + GetBySiteId + "'");
                
                if (GetByBusinessUnit == "All")
                    {
                    SQLbuilder1.Append(" and risuperarea <> '" + GetByBusinessUnit + "'");
                    }
                 
                else
                { 

                    if (GetByBusinessUnit.Length > 0)
                    {
                    SQLbuilder1.Append(" and risuperarea = '" + GetByBusinessUnit + "'");
                    }

                    if (GetByArea.Length > 0)
                    {
                        SQLbuilder1.Append(" and subarea = '" + GetByArea + "'");
                    }

                    if (GetByLineSystemType.Length > 0)
                    {
                        SQLbuilder1.Append(" and area = '" + GetByLineSystemType + "'");
                    }

                }


                if (RadCheckBoxIncludeAll.Checked == true)

                    { 

                    SQLbuilder1.Append(" union");


                    SQLbuilder1.Append(" Select '2',");
                    SQLbuilder1.Append(" lastname, firstname, risuperarea,subarea,area,");
                    SQLbuilder1.Append(" INITCAP(lastname) || ', ' || INITCAP(firstname)  as fullname, ");
                    SQLbuilder1.Append(" username, decode(notifytype,'T','To','C','Copy','Copy') notifytype , siteid");
                    SQLbuilder1.Append(" From reladmin.notification_by_linesystem_vw ");
                    SQLbuilder1.Append(" WHERE SITEID = '" + GetBySiteId + "'");
                    SQLbuilder1.Append(" and (risuperarea = 'All')  ");
                    SQLbuilder1.Append(" and (subarea = 'All')  ");
                    SQLbuilder1.Append(" and (area = 'All') ");


                    if (GetByBusinessUnit == "All")
                    {
                        SQLbuilder1.Append(" Order By Lastname, firstname");

                    }
                    else
                    {
                        SQLbuilder1.Append(" Order By 1, risuperarea,subarea,area,Lastname, firstname");

                    }

                }

                Sql1 = SQLbuilder1.ToString();

                OracleDataReader dr1;
                //Create a new DataTable.
                dr1 = da.GetOracleDataReader(Sql1);

                DataTable datatableEmployee = new DataTable();
                //Load DataReader into the DataTable.
                datatableEmployee.Load(dr1);


                if (datatableEmployee != null)
                {
                    Session["EmployeeRecords"] = datatableEmployee;
                    RadGridEmployees.DataSource = datatableEmployee;
                    RadGridEmployees.DataBind();
                }
                else
                {
                    Session["EmployeeRecords"] = null;
                    RadGridEmployees.DataSource = null;
                    RadGridEmployees.Rebind();
                }




            }
            catch (Exception)
            {

                throw;
            }
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



        protected void RadGridEmployees_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {



            if ((e.Item is GridDataItem))
            {


                //Get the instance of the right type
                GridDataItem dataBoundItem = e.Item as GridDataItem;
                //if(dataBoundItem.GetDataKeyValue("ID").ToString() == "you Compared Text") // you can also use datakey also
                if (dataBoundItem["risuperarea"].Text == "All")
                {
                    //dataBoundItem["newinactive_flag"].Font.Size = 15;
                    dataBoundItem["risuperarea"].Font.Bold = true;
                    dataBoundItem["risuperarea"].ForeColor = System.Drawing.Color.Red; // chanmge particuler cell
                    //dataBoundItem["newinactive_flag"].BackColor = System.Drawing.Color.LightGoldenrodYellow; // chanmge particuler cell

                    //if (RadioButtonShowEmployees.SelectedValue == "Y")
                    //{
                    //    //do nothing
                    //}
                    //else
                    //{
                    //    e.Item.BackColor = System.Drawing.Color.LightGoldenrodYellow; // for whole row
                    //}
                }

                //if (dataBoundItem["notifytype"].Text == "Copy")
                //{
                //    e.Item.BackColor = System.Drawing.Color.LightGoldenrodYellow; // for whole row
                //}
            }
        }


        protected void RadGridEmployees_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {

            // In the business tier, session can be accessed with:   
            // System.Web.HttpContext.Current.Session

            RadGridEmployees.DataSource = Session["EmployeeRecords"];
        }



        protected void RadGrid_ItemCreated(object sender, GridItemEventArgs e)
        {
            //(this.RadGrid1.MasterTableView.AutoGeneratedColumns[0] as GridBoundColumn).MaxLength = 5;
            //GridEditableItem item = e.Item as GridEditableItem;
            //if (item != null && e.Item.IsInEditMode && e.Item.ItemIndex != -1)
            //{
            //    (item.EditManager.GetColumnEditor("CustomerID").ContainerControl.Controls[0] as TextBox).Enabled = false;
            //}
        }

        protected void RadGridEmployees_ItemUpdated(object source, Telerik.Web.UI.GridUpdatedEventArgs e)
        {
            //if (e.Exception != null)
            //{
            //    e.KeepInEditMode = true;
            //    e.ExceptionHandled = true;
            //    DisplayMessage(true, "Customer " + e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["CustomerID"] + " cannot be updated due to invalid data.");
            //}
            //else
            //{
            //    DisplayMessage(false, "Customer " + e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["CustomerID"] + " updated");
            //}
        }

        protected void RadGridEmployees_ItemInserted(object source, GridInsertedEventArgs e)
        {
            //if (e.Exception != null)
            //{
            //    e.ExceptionHandled = true;
            //    e.KeepInInsertMode = true;
            //    DisplayMessage(true, "Customer cannot be inserted due to invalid data.");
            //}
            //else
            //{
            //    DisplayMessage(false, "Customer inserted");
            //}
        }

        protected void RadGridEmployees_ItemDeleted(object source, GridDeletedEventArgs e)
        {
            //if (e.Exception != null)
            //{
            //    e.ExceptionHandled = true;
            //    DisplayMessage(true, "Customer " + e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["CustomerID"] + " cannot be deleted");
            //}
            //else
            //{
            //    DisplayMessage(false, "Customer " + e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["CustomerID"] + " deleted");
            //}
        }

        private void DisplayMessage(bool isError, string text)
        {
            if (isError)
            {
                this.LabelError.Text = text;
            }
            else
            {
                this.LabelSuccess.Text = text;

            }
        }

        //nothing below this line
    }
}