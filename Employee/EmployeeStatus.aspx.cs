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
    public partial class EmployeeStaus : System.Web.UI.Page
    {

        GPI.MILL.DataAccess.Oracle.RetrieveData da = new MILL.DataAccess.Oracle.RetrieveData();
        GPI.UserModel.NotificationProfile np = new GPI.UserModel.NotificationProfile();
        GPI.User.Model.RIUser usermodel = new GPI.User.Model.RIUser();



        string FoundSiteName = string.Empty;
        string FoundInActive = string.Empty;

        string taskCount = "0";
        protected void Page_Load(object sender, EventArgs e)
        {

                if (!IsPostBack)
                {
                    LoadActiveEmployees();    
                    LoadEmployees("N");

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
                ;
            
            
            
            //go add task tracker notifications
            //Boolean myreturn = AddTaskTrackerNotifications(TextBoxNetWorkID.Text, mySessionUserName);





            if (returnvalue == "0")
            {

                string WhatWasSelected = RadioButtonShowEmployees.SelectedValue;

                LoadEmployees(WhatWasSelected);
               

                SuccessStatus.Visible = true;
                ButtonUpdate.Enabled = false;
                //DropDownEmployees.Items[DropDownEmployees.SelectedIndex].Enabled = false;
                DropDownEmployees.Items[0].Selected = true;
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




        protected void RadioButtonShowEmployees_SelectedIndexChanged(object sender, EventArgs e)
        {

            string WhatWasSelected = RadioButtonShowEmployees.SelectedValue;

            ButtonUpdate.Enabled = false;
            LoadEmployees(WhatWasSelected);
    
            

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

            DropDownTaskToEmployee.DataTextField = "fullname";
            DropDownTaskToEmployee.DataValueField = "username";
            DropDownTaskToEmployee.DataSource = dtTaskToEmployee;
            DropDownTaskToEmployee.DataBind();


            //insert the first item
            
            DropDownTaskToEmployee.Items.Insert(0, new RadComboBoxItem(""));
       

        }


        protected void LoadEmployees(string isactive)
        {
            string Sql = null;
            string userSite = Session["SiteID"].ToString();


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
            DropDownEmployees.SelectedIndex = -1;

            if (isactive == "Y")
            {
                LabelSetStatusInActive.Visible = false;
                LabelSetStatusActive.Visible = true;
            }
            else
            {
                LabelSetStatusInActive.Visible = true;
                LabelSetStatusActive.Visible = false;
            }

        }


        protected void DropDownEmployees_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {


            if (DropDownEmployees.SelectedValue == "")
            {
                return;
            }


            SuccessStatus.Visible = false;
            foreach (RadComboBoxItem item in DropDownTaskToEmployee.Items)
            {
                DropDownTaskToEmployee.Items[item.Index].Enabled = true;
            }


            foreach (RadComboBoxItem item in DropDownTaskToEmployee.Items)
            {

                int toindex = item.Index;

                if (e.Value == item.Value)
                {
                    DropDownTaskToEmployee.Items[toindex].Enabled = false;
                    break;
                }

            }

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
                panelEmployeeHasTask.Visible = true;
                ButtonUpdate.Enabled = false;
                LabelTasks.Visible = true;
                LabelTasks.Text = "Open tasks total: " + taskCount;
            }


        }

        protected void DropDownTaskToEmployee_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            ButtonUpdate.Enabled = false;
            if (e.Value != "")
            {
                ButtonUpdate.Enabled = true;
            }

        }



        protected Boolean AddTaskTrackerNotifications(string in_ProfileUser, string in_username)
        {


            string in_RepeatingData = "FUTURE|1|WEEKLY|1,FUTURE|7|1|1,FUTURE|10|NEXT 30 DAYS|1,ENTERED|1|DAILY|4,FUTURE|1|WEEKLY|4,FUTURE|7|2|4,FUTURE|10|NEXT 14 DAYS|4,FUTURE|1|WEEKLY|5,FUTURE|7|1|5,FUTURE|10|NEXT 30 DAYS|5";

            Boolean myreturn = np.UpdateNotificationProfile(in_ProfileUser, in_RepeatingData, in_username);

            return true;
        }


        // nothing below this line
    }
}