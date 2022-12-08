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
using GPI.MILL.RI.Class;




namespace GPI.RI.Admin.Employee
{
    public partial class EmployeeAdd : System.Web.UI.Page
    {
     GPI.UserModel.NotificationProfile np = new GPI.UserModel.NotificationProfile();
        GPI.MILL.DataAccess.Oracle.RetrieveData da = new MILL.DataAccess.Oracle.RetrieveData();

        GPI.MILL.RI.Class.Sites riclass = new GPI.MILL.RI.Class.Sites();



        RIUser riuser = new RIUser();
        string FoundSiteName = string.Empty;
        string FoundInActive = string.Empty;
        string mySessionUserName = string.Empty;

        protected void Page_Init(object sender, EventArgs e)
        { 
        
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            



        }

        protected void Page_Load(object sender, EventArgs e)
        {

            mySessionUserName = Session["UserName"] as string;
            if (mySessionUserName == null)
            {
                //do an error or something
            }


            //test
            //System.Data.DataSet ds = null;
            //ds = GetRoleList();


            string mydefaultsiteName = System.Web.HttpContext.Current.Session["SiteName"].ToString();
            string mydefaultsiteID = System.Web.HttpContext.Current.Session["SiteID"].ToString();

            //EventLog.WriteEntry("RIAdmin in EmployeeAdd", mydefaultsiteName.ToString()); 


            if (!IsPostBack)
            {

                LoadSites();
                LoadEmployees(mydefaultsiteID,"N");
                DropDownSites.SelectedValue = mydefaultsiteID;

            }

            ButtonSearchForByEmail.Focus();


            LabelAddMill.Text = "Add Employee to " + mydefaultsiteName;

        }


        protected void LoadSites()
        {

            DataTable datatableSites = new DataTable();
            datatableSites = riclass.GetReaderFacility();


            DropDownSites.DataTextField = "sitename";
            DropDownSites.DataValueField = "siteid";
            DropDownSites.DataSource = datatableSites;
            DropDownSites.DataBind();
            //insert the first item
            //DropDownSites.Items.Insert(0, new RadComboBoxItem("- Select a continent -"));


        }

        protected void LoadEmployees(string GetBySiteId, string GetByInActive)
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

                RIUser newuser = new RIUser();

                newuser.Domain = TextBoxDomain.Text;
                newuser.FirstName = TextBoxFirstName.Text;
                newuser.LastName = TextBoxLastName.Text;
                newuser.Email = TextBoxEmailAddress.Text;
                newuser.Extension = TextBoxPhoneNumber.Text;
                newuser.InactiveFlag = "N";
                newuser.DefaultLanguage = "EN-US";
                newuser.SiteID = DropDownSites.SelectedValue;
                newuser.SignatureFile = "";
                newuser.UserName = TextBoxNetWorkID.Text;
                newuser.MiddleInit = TextBoxMidInit.Text;
                newuser.LastUpdateUserName = "JAMES.BUTLER";

               Boolean CheckStatus = riuser.AddEmployee(newuser);

                if (CheckStatus)
                {

                    //go add task tracker notifications
                    Boolean myreturn = AddTaskTrackerNotifications(TextBoxNetWorkID.Text, mySessionUserName);
                    ButtonAddEmployee.Enabled = false;

                    SuccessAdded.Visible = true;
                    LoadEmployees(DropDownSites.SelectedValue, RadioButtonShowEmployees.SelectedValue);
                }
                else
                {

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

                
                ds = da.GetDataSetFromPackage(paramCollection, "mttgeneraldata.getrolelist");

                //if (ds.Tables[0].Rows.Count == 0)
                //{ do something; }

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

                if (IsEmailValid(EmailTextBox.Text + LabelLookUpAt.Text))
                {
                    // only allow _ or . in name
                    var trimmed = Regex.Replace(EmailTextBox.Text, @"[^0-9a-zA-Z_.]+", "");
                    EmailTextBox.Text = trimmed;
                    GetByEmail(EmailTextBox.Text + LabelLookUpAt.Text);
                }
                else
                {
                    EmailNotValid.Text = EmailTextBox.Text + LabelLookUpAt.Text + " email is not valid. Check spelling.";
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
                string emailFound = EmailTextBox.Text + LabelLookUpAt.Text;
                _ldapuser = testldapemail.GetUserByEmail(emailFound);
                
                if (_ldapuser.EmailAddress.ToUpper() == EmailTextBox.Text.ToUpper() + LabelLookUpAt.Text.ToUpper())
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
                        EmailNotFound.Text = emailFound + " email was not found";
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

            LabelAddMill.Text = "Add Employee to " + DropDownSites.Items[DropDownSites.SelectedIndex].Text.ToString();

                
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

        protected Boolean AddTaskTrackerNotifications(string in_ProfileUser,string in_username)
        {
            
 
            string in_RepeatingData = "FUTURE|1|WEEKLY|1,FUTURE|7|1|1,FUTURE|10|NEXT 30 DAYS|1,ENTERED|1|DAILY|4,FUTURE|1|WEEKLY|4,FUTURE|7|2|4,FUTURE|10|NEXT 14 DAYS|4,FUTURE|1|WEEKLY|5,FUTURE|7|1|5,FUTURE|10|NEXT 30 DAYS|5";

            Boolean myreturn = np.UpdateNotificationProfile(in_ProfileUser, in_RepeatingData, in_username);

            return true;
        }

        //Nothing below this line =======================================
    }
}