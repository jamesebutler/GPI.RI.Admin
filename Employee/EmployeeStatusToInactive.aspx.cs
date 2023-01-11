using System;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Diagnostics;
using System.Configuration;
using System.Web.UI;
using System.Text.RegularExpressions;




using Telerik.Web.UI;
using Devart.Data.Oracle;
using GPI.MILL.DataAccess.Oracle;
using GPI.MILL.Ldap;
using GPI.User.Model;
using System.Data;

namespace GPI.RI.Admin.Employee
{
    public partial class EmployeeStatusToInactive : System.Web.UI.Page
    {


        GPI.MILL.DataAccess.Oracle.RetrieveData da = new MILL.DataAccess.Oracle.RetrieveData();
        GPI.UserModel.NotificationProfile np = new GPI.UserModel.NotificationProfile();
        GPI.User.Model.RIUser usermodel = new GPI.User.Model.RIUser();

        RIUser riuser = new RIUser();

        string FoundSiteName = string.Empty;
        string FoundInActive = string.Empty;
         string mySessionUserName = string.Empty;
        string mydefaultsiteID = string.Empty;
        string mydefaultsiteName = string.Empty;
        string taskCount = "0";

        protected void Page_Load(object sender, EventArgs e)
        {

            mySessionUserName = Session["UserName"] as string;
            if (mySessionUserName == null)
            {
                //do an error or something
            }
            mydefaultsiteID = System.Web.HttpContext.Current.Session["SiteID"].ToString();
            mydefaultsiteName = System.Web.HttpContext.Current.Session["SiteName"].ToString();


            LabelSetStatus.Text = "Set Employee to InActive for " + mydefaultsiteName;


            if (!IsPostBack)
            {
               // LoadActiveEmployees();
                LoadEmployees();
                LoadEmployeesforGrid(mydefaultsiteID,"N");

            }
        }

        protected void LoadActiveEmployees()
        {
            string Sql = null;
            string userSite = Session["SiteID"].ToString();


            string Sql1 = "Select username,  INITCAP(lastname) || ', ' || INITCAP(firstname) fullname  from refemployee where 1=1 and inactive_flag = 'N' and  siteid = '" + userSite + "' ORDER BY lastname,FIRSTNAME";
            OracleDataReader drTaskToEmployee;
            drTaskToEmployee = da.GetOracleDataReader(Sql1);
            //Create a new DataTable.
            DataTable dtTaskToEmployee = new DataTable();
            //Load DataReader into the DataTable.
            dtTaskToEmployee.Load(drTaskToEmployee);

            DropDownTaskToEmployee.Items.Clear();

            DropDownTaskToEmployee.DataTextField = "fullname";
            DropDownTaskToEmployee.DataValueField = "username";
            DropDownTaskToEmployee.DataSource = dtTaskToEmployee;
            DropDownTaskToEmployee.DataBind();


            //insert the first item

            

        }


        protected void LoadEmployees()
        {
            string Sql = null;
            string userSite = Session["SiteID"].ToString();
            string isactive = "N"; 

            Sql = "Select username,  INITCAP(lastname) || ', ' || INITCAP(firstname) fullname  from refemployee where 1=1 and inactive_flag = '" + isactive + "' and  siteid = '" + userSite + "' ORDER BY lastname,FIRSTNAME";
            //Sql = "Select username,  INITCAP(lastname) || ', ' || INITCAP(firstname)  || ' - ' || CASE inactive_flag  WHEN 'Y' then 'InActive'   WHEN 'N' then 'Active'   end fullname from refemployee where 1=1 and inactive_flag = 'N' and  siteid = '" + userSite + "' ORDER BY lastname,FIRSTNAME";

            DropDownEmployees.DataSource = "";
            DropDownEmployees.DataBind();

            OracleDataReader dr;
            dr = da.GetOracleDataReader(Sql);
            //Create a new DataTable.
            DataTable dt = new DataTable();
            //Load DataReader into the DataTable.
            dt.Load(dr);

            DropDownEmployees.Items.Clear();
            DropDownEmployees.DataTextField = "fullname";
            DropDownEmployees.DataValueField = "username";
            DropDownEmployees.DataSource = dt;
            DropDownEmployees.DataBind();


            //insert the first item
            DropDownEmployees.Items.Insert(0, new RadComboBoxItem(""));
            DropDownEmployees.Items[0].Selected = true;

                foreach (RadComboBoxItem item in DropDownEmployees.Items)
            {

                int toindex = item.Index;

                if (mySessionUserName == item.Value)
                {
                    DropDownEmployees.Items[toindex].Enabled = false;
                    DropDownEmployees.Items[0].Selected = true;
                    break;
                }

            }


        }



        protected void DropDownTaskToEmployee_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            ButtonUpdate.Enabled = false;
            if (e.Value != "")
            {
                ButtonUpdate.Enabled = true;
            }
            else
            {
                ButtonUpdate.Enabled = false;
            }

        }



        protected void DropDownEmployees_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {


            if (DropDownEmployees.SelectedValue == "")
            {
                return;
            }

            SuccessStatus.Visible = false;
            //foreach (RadComboBoxItem item in DropDownTaskToEmployee.Items)
            //{
            //    DropDownTaskToEmployee.Items[item.Index].Enabled = true;
            //}

            taskCount = "0";
            taskCount = da.GetEmployeeTaskCount(e.Value);

            if (taskCount == "0")
            {
  
                panelEmployeeHasTask.Visible = false;
                ButtonUpdate.Enabled = true;
                LabelTasks.Text = "Tasks";
            }
            else
            {
                DropDownTaskToEmployee.Items.Clear();
                LoadActiveEmployees();
                DropDownTaskToEmployee.Items.Insert(0, new RadComboBoxItem(""));
                DropDownTaskToEmployee.Items[0].Selected = true;
                DropDownTaskToEmployee.EmptyMessage = "";
                foreach (RadComboBoxItem item in DropDownTaskToEmployee.Items)
                {
                    int toindex = item.Index;

                    if (e.Value == item.Value)
                    {
                        DropDownTaskToEmployee.Items[toindex].Enabled = false;
                       // DropDownTaskToEmployee.Items[0].Selected = true;
                        break;
                    }
                }
                LabelTasks.Text = "Open tasks total: " + taskCount;
                LabelTasks.Visible = true;
                panelEmployeeHasTask.Visible = true;
                DropDownTaskToEmployee.Visible = true;
                ButtonUpdate.Enabled = false;
            }


        }


        protected void ButtonUpdate_Click(object sender, EventArgs e)
        {

            string userplantcode = Session["PlantCode"].ToString();
            string returnvalue = usermodel.UpdateEmployeeStatusToInactive(
                DropDownEmployees.SelectedValue,
                userplantcode,
                Session["UserName"].ToString(),
                DropDownTaskToEmployee.SelectedValue);

            //go add task tracker notifications
            //Boolean myreturn = AddTaskTrackerNotifications(TextBoxNetWorkID.Text, mySessionUserName);

            if (returnvalue == "0")
            { 
                DropDownEmployees.Items.Clear();
                LoadEmployees();
                string WhatIsSelected = RadioButtonShowEmployees.SelectedValue;
                LoadEmployeesforGrid(mydefaultsiteID, WhatIsSelected);

                SuccessStatus.Visible = true;
                ButtonUpdate.Enabled = false;
                //DropDownEmployees.Items[DropDownEmployees.SelectedIndex].Enabled = false;
                //DropDownEmployees.Items[0].Selected = true;
                DropDownEmployees.EmptyMessage = "";

                DropDownTaskToEmployee.Items.Clear();
                DropDownTaskToEmployee.EmptyMessage = "";
                DropDownTaskToEmployee.Visible = false;
                panelEmployeeHasTask.Visible = false;

            }
            else
            {
                FailureAdded.Visible = true;
            }




            //look at mttgeneraldata.transferemployee

            //look at mttgeneraldata.getreassigntasklist
        }

        protected void RadGridEmployees_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {



            if ((e.Item is GridDataItem))
            {


                //Get the instance of the right type
                GridDataItem dataBoundItem = e.Item as GridDataItem;
                //if(dataBoundItem.GetDataKeyValue("ID").ToString() == "you Compared Text") // you can also use datakey also
                if (dataBoundItem["newinactive_flag"].Text == "No")
                {
                    //dataBoundItem["newinactive_flag"].Font.Size = 15;
                    dataBoundItem["newinactive_flag"].Font.Bold = true;
                    dataBoundItem["newinactive_flag"].ForeColor = System.Drawing.Color.Red; // chanmge particuler cell
                    //dataBoundItem["newinactive_flag"].BackColor = System.Drawing.Color.LightGoldenrodYellow; // chanmge particuler cell

                    if (RadioButtonShowEmployees.SelectedValue == "Y")
                    {
                        //do nothing
                    }
                    else
                    {
                        e.Item.BackColor = System.Drawing.Color.LightGoldenrodYellow; // for whole row
                    }
                }

            }
        }


        protected void RadioButtonShowEmployees_SelectedIndexChanged(object sender, EventArgs e)
        {

            string WhatWasSelected = RadioButtonShowEmployees.SelectedValue;

            LoadEmployeesforGrid(mydefaultsiteID, WhatWasSelected);

            //EventLogConsole1.LoggedEvents.Add(String.Format("Selected index changed. Selected value is <strong>{0}</strong>.", RadioButtonList1.SelectedValue));
        }

        protected void RadGridEmployees_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {

            // In the business tier, session can be accessed with:   
            // System.Web.HttpContext.Current.Session

            RadGridEmployees.DataSource = Session["EmployeeRecords"];
        }


        protected void LoadEmployeesforGrid(string GetBySiteId, string GetByInActive)
        {


            try
            {

                DataTable datatableEmployee = new DataTable();
                datatableEmployee = riuser.GetAllEmployeeBySiteByActive(GetBySiteId, GetByInActive);

                if (datatableEmployee != null)
                {
                    Session["EmployeeRecords"] = datatableEmployee;
                    RadGridEmployees.DataSource = datatableEmployee;
                    RadGridEmployees.DataBind();
                }


            }
            catch (Exception)
            {

                throw;
            }

        }


    }
}