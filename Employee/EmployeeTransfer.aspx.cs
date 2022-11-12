using System;
using System.Data;
using System.Text;
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

namespace GPI.RI.Admin.Employee
{
    public partial class EmployeeTransfer : System.Web.UI.Page
    {

        GPI.MILL.DataAccess.Oracle.RetrieveData da = new MILL.DataAccess.Oracle.RetrieveData();

        string FoundSiteName = string.Empty;
        string FoundInActive = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {


          
            if (!IsPostBack)
            {

                LoadSites();

                LoadEmployees();

              
            }

            LabelFromMill.Text = "Transfer Employee from " + Session["SiteName"].ToString();

        }




        protected void ButtonTransfer_Click(object sender, EventArgs e)
        {

           string returnvalue = da.TransferEmployee(DropDownSites.SelectedValue,Session["UserName"].ToString(),DropDownEmployees.SelectedValue);

            if (returnvalue == "")
            {
                LoadEmployees();

                SuccessAdded.Visible = true;
                ButtonTransfer.Enabled = false;
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




            protected void LoadSites()
        {
            string Sql = null;
            string userSite = Session["SiteID"].ToString();
            Sql = "select rcfaflid,sitename from refsite where domain = 'NA' and inactive_flag = 'N' and  siteid <> '" + userSite + "' ORDER BY sitename";

            OracleDataReader dr;
            dr = da.GetOracleDataReader(Sql);
            //Create a new DataTable.
            DataTable dt = new DataTable();
            //Load DataReader into the DataTable.
            dt.Load(dr);


            DropDownSites.DataTextField = "sitename";
            DropDownSites.DataValueField = "rcfaflid";
            DropDownSites.DataSource = dt;
            DropDownSites.DataBind();
            //insert the first item
            //DropDownSites.Items.Insert(0, new RadComboBoxItem("- Select a continent -"));

            //RadAjaxManager1.FocusControl(DropDownSites);
        }


        protected void LoadEmployees()
        {
            string Sql = null;
            string userSite = Session["SiteID"].ToString();
            Sql = "Select username,  INITCAP(lastname) || ', ' || INITCAP(firstname) fullname  from refemployee where 1=1 and inactive_flag = 'N' and  siteid = '" + userSite + "' ORDER BY lastname,FIRSTNAME";

            OracleDataReader dr;
            dr = da.GetOracleDataReader(Sql);
            //Create a new DataTable.
            DataTable dt = new DataTable();
            //Load DataReader into the DataTable.
            dt.Load(dr);

            DropDownEmployees.Items.Clear();
            DropDownTaskToEmployee.Items.Clear();


            DropDownEmployees.DataTextField = "fullname";
            DropDownEmployees.DataValueField = "username";
            DropDownEmployees.DataSource = dt;
            DropDownEmployees.DataBind();

            DropDownTaskToEmployee.DataTextField = "fullname";
            DropDownTaskToEmployee.DataValueField = "username";
            DropDownTaskToEmployee.DataSource = dt;
            DropDownTaskToEmployee.DataBind();


            //insert the first item
            DropDownEmployees.Items.Insert(0, new RadComboBoxItem(""));
            DropDownTaskToEmployee.Items.Insert(0, new RadComboBoxItem(""));


        }


        protected void DropDownEmployees_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {


            if (DropDownEmployees.SelectedValue == "")
            {
                return;
            }


            SuccessAdded.Visible = false;
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

            string taskCount = da.GetEmployeeTaskCount(e.Value);
                if (taskCount == "0")
            {
                panelEmployeeHasTask.Visible = false;
                ButtonTransfer.Enabled = true;
                LabelTasks.Text = "Tasks";
            }
            else
            {
                panelEmployeeHasTask.Visible = true;
                ButtonTransfer.Enabled = false;
                LabelTasks.Visible = true;
                LabelTasks.Text = "Open tasks: " + taskCount;
            }

            
        }

        protected void DropDownTaskToEmployee_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
           ButtonTransfer.Enabled = false;
            if (e.Value != "")
            {
                ButtonTransfer.Enabled = true;
            }

        }

            //nothing below this line
        }
}