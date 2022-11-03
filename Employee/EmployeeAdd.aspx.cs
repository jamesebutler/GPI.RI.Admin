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
    public partial class EmployeeAdd : System.Web.UI.Page
    {

        
        GPI.MILL.DataAccess.Oracle.RetrieveData da = new MILL.DataAccess.Oracle.RetrieveData();
        
        string FoundSiteName = string.Empty;
        string FoundInActive = string.Empty;

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
            dr = da.GetOracleDataReader(Sql);
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

        protected void SetStatusLabels()
        {
            EmailFound.Visible = false;
            EmailNotFound.Visible = false;
            EmailInactive.Visible = false;
            EmailInRI.Visible = false;
            EmailNotValid.Visible = false;
            SuccessAdded.Visible = false;
        }
        protected void ButtonAddEmployee_Click(object sender, EventArgs e)
        {

            SetStatusLabels();
            try
            {


            string myconnection;
            //string provider;

            myconnection = ConfigurationManager.ConnectionStrings["connectionRCFATST"].ConnectionString;
                //provider = ConfigurationManager.ConnectionStrings["connectionRCFATST"].ProviderName;

     
            using (OracleConnection connection = new OracleConnection(myconnection))

            using (OracleCommand command = new OracleCommand("sp_Employee_Insert", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("inDomain", OracleDbType.VarChar).Value = TextBoxDomain.Text;
                    command.Parameters.Add("inFirstname", OracleDbType.VarChar).Value = TextBoxFirstName.Text;
                    command.Parameters.Add("inLastname", OracleDbType.VarChar).Value = TextBoxLastName.Text;
                    command.Parameters.Add("inEmail", OracleDbType.VarChar).Value = TextBoxEmailAddress.Text;
                    command.Parameters.Add("inExtension", OracleDbType.VarChar).Value = TextBoxPhoneNumber.Text;
                    command.Parameters.Add("in_inactive_flag", OracleDbType.VarChar).Value = "N";
                    command.Parameters.Add("inDefault_language", OracleDbType.VarChar).Value = "EN-US";
                    command.Parameters.Add("inSiteid", OracleDbType.VarChar).Value = DropDownSites.SelectedValue;
                    command.Parameters.Add("inSignaturefile", OracleDbType.VarChar).Value = "";
                    command.Parameters.Add("inUsername", OracleDbType.VarChar).Value = TextBoxNetWorkID.Text;
                    command.Parameters.Add("inMiddleinit", OracleDbType.VarChar).Value = TextBoxMidInit.Text;
                    command.Parameters.Add("inUpdateUserName", OracleDbType.VarChar).Value = "james.butler";
                    command.Parameters.Add("out_status", OracleDbType.VarChar).Value = "";
                    command.Parameters["out_status"].Direction = ParameterDirection.Output;
                connection.Open();
                command.ExecuteNonQuery();
                string CheckStatus = command.Parameters["out_status"].Value.ToString();
                //string SomeOutVar1 = command.Parameters["another_status"].Value.ToString();

                   if (CheckStatus == "0")
                    {
                        ButtonAddEmployee.Enabled = false;
 
                        SuccessAdded.Visible = true;
                        LoadEmployees(DropDownSites.SelectedValue,RadioButtonShowEmployees.SelectedValue);
                    }
                    

                }


            }

            catch (Exception ex)
            {
                throw;
            }



        }


        public DataSet GetRoleList()
        {
            OracleParameterCollection paramCollection = new OracleParameterCollection();
            OracleParameter param = new OracleParameter();
            System.Data.DataSet ds = null;
            System.Data.DataSet dsError = null;


            try
            {

                // ===================================================
                // output
                param = new OracleParameter();
                param.ParameterName = "rsSiteRoleInfo";
                param.OracleDbType = OracleDbType.Cursor;
                param.Direction = System.Data.ParameterDirection.Output;
                paramCollection.Add(param);

                // ds = HelperDal.GetDSFromPackage(paramCollection, "mttviewGPI.MTTVIEWSimple")
                //ds = HelperDal.GetDSFromPackage(paramCollection, "mttgeneraldata.getrolelist");

                return ds;
            }
            catch (Exception ex)
            {
                return dsError;
            }
        }

        protected void ButtonSearchForByEmail_Click(object sender, EventArgs e)
        {
            
            
            SetStatusLabels();
 
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
                return;
            }
            else
            {

                IsEmailValid("");

                if (IsEmailValid(EmailTextBox.Text))
                {
                    //check to see if email is in RI DB
                    GetByEmail(EmailTextBox.Text);
                }
                else
                {
                    EmailNotValid.Visible = true;
                    return;
                }
                

                if (FoundSiteName == "")
                { 
                    //do nothing
                }
                else
                {
                    string isActive;
                    if (FoundInActive == "Y")
                    {
                        isActive = "No";
                     }
                    else
                    {
                        isActive = "Yes";
                    }


                    EmailInRI.Text = "Email found at: " + FoundSiteName +   " - Active: " + isActive;
                    EmailInRI.Visible = true;
                    return;
                }
            }

            if (FoundSiteName == "")
            {

                
                GPILDAP testldapemail = new GPILDAP();
                GPI.User.Model.LdapUser _ldapuser = new GPI.User.Model.LdapUser();
                string emailFound = EmailTextBox.Text;
                _ldapuser = testldapemail.GetUserByEmail(emailFound);
                
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

                    //check to see if the user was found but inactive
                    if (_ldapuser.EmailAddress.ToUpper() == "inactive".ToUpper())
                    {
                        //user found but is inactive
                        EmailInactive.Visible = true;
                         ButtonAddEmployee.Enabled = false;
                    }
                    else
                    {
                        // show that the email was not found in active directory
                        EmailNotFound.Visible = true;
                         ButtonAddEmployee.Enabled = false;
                    }

                }




            }
        }


        protected void GetByEmail(string GetByEmail)
        {
            string Sql = null;

            StringBuilder SQLbuilder = new StringBuilder();
            SQLbuilder.Append(" SELECT a.siteid,a.inactive_flag,b.sitename from refemployee a,refsite b where 1=1 and a.siteid = b.siteid");
            SQLbuilder.Append(" and UPPER(a.email)  = UPPER('" + GetByEmail + "')");

            
            Sql = SQLbuilder.ToString();
            OracleDataReader dr;
            dr = da.GetOracleDataReader(Sql);
            while (dr.Read())
                
                {
                FoundSiteName = dr.GetValue("sitename").ToString();
                FoundInActive = dr.GetValue("inactive_flag").ToString();
                };


        }


        private static bool IsEmailValid(string email)
        {
            string regex = @"^[^@\s]+@[^@\s]+\.(com|net|org|gov)$";
            return Regex.IsMatch(email, regex, RegexOptions.IgnoreCase);
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