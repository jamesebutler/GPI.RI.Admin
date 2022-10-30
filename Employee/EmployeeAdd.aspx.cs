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
using GPI.MILL.Ldap;
using GPI.User.Model;

namespace GPI.RI.Admin.Employee
{
    public partial class EmployeeAdd : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                LoadSites();
                LoadEmployees("TS","N");
                DropDownSites.SelectedValue = "TS";

            }
        }

        protected void LoadSites()
        {
            string Sql = null;
            Sql = "select siteid,sitename from refsite where domain = 'NA' and inactive_flag = 'N'";

            OracleDataReader dr;
            dr = RetrieveData.GetOracleDataReader(Sql);
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

            //RadAjaxManager1.FocusControl(DropDownSites);
        }

        protected void LoadEmployees(string GetBySiteId, string GetByInActive)
        {
            string Sql = null;
 
            StringBuilder SQLbuilder = new StringBuilder();
            SQLbuilder.Append(" SELECT e.username, e.lastname, e.firstname, e.middleinit, e.email, e.extension, e.domain, UPPER(e.default_language) default_language, e.inactive_flag,");
            SQLbuilder.Append(" CASE e.inactive_flag   WHEN 'N' then 'Yes'    WHEN 'Y' then 'No'   end newinactive_flag");
            SQLbuilder.Append(" FROM refemployee e");
            SQLbuilder.Append(" WHERE 1=1 and e.domain = 'NA' and e.siteid = '" + GetBySiteId + "'");
            
            if (GetByInActive == "B")
            {
                // get only all records
                 
            }
            else
            {
                //get only inactive records
                SQLbuilder.Append(" and e.inactive_flag =  '" + GetByInActive + "'");

            }

            SQLbuilder.Append(" ORDER by e.lastname ");


            Sql = SQLbuilder.ToString();

            OracleDataReader dr;
            dr = RetrieveData.GetOracleDataReader(Sql);
            //Create a new DataTable.
            DataTable dt = new DataTable();
            //Load DataReader into the DataTable.
            dt.Load(dr);


            Session["EmployeeRecords"] = dt;


        RadGridEmployees.DataSource = dt;
            RadGridEmployees.DataBind();
            //insert the first item
            //DropDownSites.Items.Insert(0, new RadComboBoxItem("- Select a continent -"));

            //RadAjaxManager1.FocusControl(DropDownSites);
        }

    
        protected void ButtonSearchForByEmail_Click(object sender, EventArgs e)
        {
            
            EmailFound.Visible = false;
            EmailNotFound.Visible = false;
            ButtonAddEmployee.Enabled = false;
            TextBoxNetWorkID.Text = "";
            TextBoxLastName.Text = "";
            TextBoxFirstName.Text = "";
            TextBoxMidInit.Text = "";
            TextBoxEmailAddress.Text = "";
            TextBoxPhoneNumber.Text = "";
            TextBoxDomain.Text = "";
            TextBoxDefaultLang.Text = "";


            if (EmailTextBox.Text == "")
            {
                //do nothing
            }
           else
            { 
            
                GPILDAP testldapemail = new GPILDAP();
                GPI.User.Model.LdapUser _ldapuser = new GPI.User.Model.LdapUser();
                GPI.User.Model.LdapUser _PassingUser = new GPI.User.Model.LdapUser();
                string emailFound = EmailTextBox.Text;
                _PassingUser.EmailAddress = emailFound;

                _ldapuser = testldapemail.GetUserLdapInformationByEmail(_PassingUser);


                if (_ldapuser.EmailAddress.ToUpper() == EmailTextBox.Text.ToUpper())
                {
                    TextBoxNetWorkID.Text = _ldapuser.SamAccountName.ToUpper();
                    TextBoxLastName.Text = _ldapuser.Surname;
                    TextBoxFirstName.Text = _ldapuser.GivenName;
                    TextBoxMidInit.Text = _ldapuser.MiddleName;
                    TextBoxEmailAddress.Text = _ldapuser.EmailAddress;
                    TextBoxPhoneNumber.Text = "";
                    TextBoxDomain.Text = "NA";
                    TextBoxDefaultLang.Text = "EN-US";

                    
                    ButtonAddEmployee.Enabled = true;
                    EmailFound.Visible = true;
                
                }
                else
                {
                    EmailNotFound.Visible = true;
                    // show that the email was not found in active directory
                 }
            }
        }


        protected void DropDownSites_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            // RadAjaxManager1.FocusControl(DropDownArea.ClientID + "_Input");
            string WhatWasSelected = RadioButtonShowEmployees.SelectedValue;
            LoadEmployees(DropDownSites.SelectedValue,WhatWasSelected);

            
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




                //    GridDataItem dataBoundItem = e.Item as GridDataItem;

                //if (dataBoundItem["​newinactive_flag"].Text == "Yes")
                //{
                //    dataBoundItem["newinactive_flag"].ForeColor = System.Drawing.Color.Orange;

                //    dataBoundItem["newinactive_flag"].Font.Bold = true;
                //     /*e.Item.BackColor = System.Drawing.Color.LightGoldenrodYellow; */
                // }

            }
        }


        protected void RadioButtonShowEmployees_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            string WhatWasSelected = RadioButtonShowEmployees.SelectedValue;
            
            LoadEmployees(DropDownSites.SelectedValue, WhatWasSelected);
            
            //EventLogConsole1.LoggedEvents.Add(String.Format("Selected index changed. Selected value is <strong>{0}</strong>.", RadioButtonList1.SelectedValue));
        }

        protected void RadGridEmployees_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            
            // In the business tier, session can be accessed with:   
            // System.Web.HttpContext.Current.Session

            RadGridEmployees.DataSource = Session["EmployeeRecords"];
        }



        //Nothing below this line =======================================
    }
}