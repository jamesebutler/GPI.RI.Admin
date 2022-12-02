using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using Telerik.Web.UI;
using Devart.Data.Oracle;
using GPI.MILL.DataAccess.Oracle;
using GPI.MILL.Ldap;
using GPI.User.Model;
using System.Text;
using GPI.UserModel;

namespace GPI.RI.Admin.TaskTracker
{
    public partial class TaskTrackerNotifications : System.Web.UI.Page
    {

        GPI.MILL.DataAccess.Oracle.RetrieveData da = new MILL.DataAccess.Oracle.RetrieveData();
        GPI.UserModel.NotificationProfile np = new GPI.UserModel.NotificationProfile();
       


        
        string in_RepeatingData = "";
        StringBuilder SQLbuilder = new StringBuilder();
        string mySessionUserName = string.Empty;
        
        enum DaysofWeek
        {
            Sunday,
            Monday,
            Tuesday,
            Wednesday,
            Thursday,
            Friday,
            Saturday
        };

        protected void Page_Load(object sender, EventArgs e)
        {

            mySessionUserName = Session["UserName"] as string;
            if (mySessionUserName == null)
            {
                //do an error or something
            }
      

            if (!IsPostBack)
            {
                LoadEmployees();
                PopulateDaysOfWeek();
                PopulateMonthOrdinals();
                SetPeriodOptions();

                DropDownEmployees.SelectedValue = mySessionUserName;
                LoadUserDefaults(mySessionUserName);
           
            }

            LabelAddMill.Text = "Employee Task Tracker Notifications for " + Session["SiteName"].ToString();


        }


        private void PopulateDaysOfWeek()
        {
            {

               _ddlDayOfWeek.Items.Clear();

                List<string> daysList = new List<string>(Enum.GetNames(typeof(DaysofWeek)));
                for (int i = 0; i <= daysList.Count - 1; i++)
                    _ddlDayOfWeek.Items.Add(new ListItem(daysList[i], System.Convert.ToString(i + 1)));


                _ddlResponsiblePersonDayOfWeek.Items.Clear();

                List<string> daysList1 = new List<string>(Enum.GetNames(typeof(DaysofWeek)));
                for (int i = 0; i <= daysList1.Count - 1; i++)
                    _ddlResponsiblePersonDayOfWeek.Items.Add(new ListItem(daysList1[i], System.Convert.ToString(i + 1)));



                _ddlManagerDayOfWeek.Items.Clear();

                List<string> daysList2 = new List<string>(Enum.GetNames(typeof(DaysofWeek)));
                for (int i = 0; i <= daysList2.Count - 1; i++)
                    _ddlManagerDayOfWeek.Items.Add(new ListItem(daysList2[i], System.Convert.ToString(i + 1)));



            }
        }

        private void PopulateMonthOrdinals()
        {
            {
                var withBlock = this._ddlOrdinalMonth;
                withBlock.Items.Clear();
                for (int i = 1; i <= 31; i++)
                {
                    if (withBlock.Items.FindByValue(System.Convert.ToString(i)) == null)
                        withBlock.Items.Add(new ListItem(System.Convert.ToString(i), System.Convert.ToString(i)));
                }


                var withBlock1 = this._ddlManagerOrdinalMonth;
                withBlock1.Items.Clear();
                for (int i = 1; i <= 31; i++)
                {
                    if (withBlock1.Items.FindByValue(System.Convert.ToString(i)) == null)
                        withBlock1.Items.Add(new ListItem(System.Convert.ToString(i), System.Convert.ToString(i)));
                }




            }
        }


        public void SetPeriodOptions()
        {


                    var withBlock = _rblFutureNotificationPeriod;
                    // set with future periods
                    withBlock.Items.Clear();
                    withBlock.Items.Add(new ListItem("&nbsp;" + "All", "All"));
                    withBlock.Items.Add(new ListItem("&nbsp;" + "Next 7 Days", "Next 7 Days"));
                    withBlock.Items.Add(new ListItem("&nbsp;" + "Next 14 Days", "Next 14 Days"));
                    withBlock.Items.Add(new ListItem("&nbsp;" + "Next 30 Days", "Next 30 Days"));
                    withBlock.Items.Add(new ListItem("&nbsp;" + "Next 90 Days", "Next 90 Days"));


                    var withBlockFutureNotification = _rblResponsiblePersonFutureNotificationPeriod;
                    // set with future periods
                    withBlockFutureNotification.Items.Clear();
                    withBlockFutureNotification.Items.Add(new ListItem("&nbsp;" + "All", "All"));
                    withBlockFutureNotification.Items.Add(new ListItem("&nbsp;" + "Next 7 Days", "Next 7 Days"));
                    withBlockFutureNotification.Items.Add(new ListItem("&nbsp;" + "Next 14 Days", "Next 14 Days"));
                    withBlockFutureNotification.Items.Add(new ListItem("&nbsp;" + "Next 30 Days", "Next 30 Days"));
                    withBlockFutureNotification.Items.Add(new ListItem("&nbsp;" + "Next 90 Days", "Next 90 Days"));

                    var withBlockFutureNotification2 = _rblManagerFutureNotificationPeriod;
                    // set with future periods
                    withBlockFutureNotification2.Items.Clear();
                    withBlockFutureNotification2.Items.Add(new ListItem("&nbsp;" + "All", "All"));
                    withBlockFutureNotification2.Items.Add(new ListItem("&nbsp;" + "Next 7 Days", "Next 7 Days"));
                    withBlockFutureNotification2.Items.Add(new ListItem("&nbsp;" + "Next 14 Days", "Next 14 Days"));
                    withBlockFutureNotification2.Items.Add(new ListItem("&nbsp;" + "Next 30 Days", "Next 30 Days"));
                    withBlockFutureNotification2.Items.Add(new ListItem("&nbsp;" + "Next 90 Days", "Next 90 Days"));

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
         
            DropDownEmployees.DataTextField = "fullname";
            DropDownEmployees.DataValueField = "username";
            DropDownEmployees.DataSource = dt;
            DropDownEmployees.DataBind();

            //insert the first item
            DropDownEmployees.Items.Insert(0, new RadComboBoxItem(""));
        

        }


        protected void DropDownEmployees_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            string WhatWasSelected = DropDownEmployees.SelectedValue;
            LoadUserDefaults(WhatWasSelected);

        }

        protected string BuildCreator()
        {

            List<RecurringParametersList> rpl = new List<RecurringParametersList>();

            try
            {
                    if (_cbOptOut.Checked)
                    {
                        rpl.Add(new RecurringParametersList { EmailType = "FUTURE", ProfileTypeSeqId = 10, ProfileTypeValue = "NO EMAIL", RoleseqId = 5 });
                    }
                    else
                    { 
                            //"DAILY"
                            if (_rbEveryDay.Checked == true)
                            {
                                rpl.Add(new RecurringParametersList { EmailType = "FUTURE", ProfileTypeSeqId = 1, ProfileTypeValue = "DAILY", RoleseqId = 5 });

                            }
                            // "MONTHLY"
                            if (_rblEveryMonth.Checked == true)
                            {
                                rpl.Add(new RecurringParametersList { EmailType = "FUTURE", ProfileTypeSeqId = 1, ProfileTypeValue = "MONTHLY", RoleseqId = 5 });
                                //get day of month
                                rpl.Add(new RecurringParametersList { EmailType = "FUTURE", ProfileTypeSeqId = 5, ProfileTypeValue = _ddlOrdinalMonth.SelectedValue.ToString(), RoleseqId = 5 });
                            }
                            // "WEEKLY"
                            if (_rbEveryWeek.Checked == true)
                            {
                                rpl.Add(new RecurringParametersList { EmailType = "FUTURE", ProfileTypeSeqId = 1, ProfileTypeValue = "WEEKLY", RoleseqId = 5 });
                                //get day of week
                                int myday = Int32.Parse(_ddlDayOfWeek.SelectedValue);
                                rpl.Add(new RecurringParametersList { EmailType = "FUTURE", ProfileTypeSeqId = 7, ProfileTypeValue = myday.ToString(), RoleseqId = 5 });
                            }


                            // loop through
                            for (int i = 0; i <= 4; i++)
                                if (_rblFutureNotificationPeriod.Items[i].Selected == true)
                                {
                                    rpl.Add(new RecurringParametersList { EmailType = "FUTURE", ProfileTypeSeqId = 10, ProfileTypeValue = _rblFutureNotificationPeriod.Items[i].Value.ToUpper(), RoleseqId = 5 });

                                }

                     }
                    string creator = "";
                    for (int i = 0; i <= rpl.Count - 1; i++)
                        creator += rpl[i].EmailType + "|" + rpl[i].ProfileTypeSeqId + "|" + rpl[i].ProfileTypeValue + "|" + rpl[i].RoleseqId + ",";


                    return creator;

            }

            catch (Exception ex)
            {
                throw;
            }
        }

        protected string BuildResponsible()
        {

            List<RecurringParametersList> rpl = new List<RecurringParametersList>();

            try
            {


                //"Notify me when"
                if (_rbEnteredNotifEveryDay.Checked == true)
                {
                    rpl.Add(new RecurringParametersList { EmailType = "ENTERED", ProfileTypeSeqId = 1, ProfileTypeValue = "DAILY", RoleseqId = 4 });

                }
                if (_rbEnteredNotifImmediately.Checked == true)
                {
                    rpl.Add(new RecurringParametersList { EmailType = "ENTERED", ProfileTypeSeqId = 1, ProfileTypeValue = "IMMEDIATE", RoleseqId = 4 });

                }


                //"DAILY"
                if (_rbResponsiblePersonEveryDay.Checked == true)
                {
                    rpl.Add(new RecurringParametersList { EmailType = "FUTURE", ProfileTypeSeqId = 1, ProfileTypeValue = "DAILY", RoleseqId = 4 });

                }
                // "WEEKLY"
                if (_rbResponsiblePersonEveryWeek.Checked == true)
                {
                    rpl.Add(new RecurringParametersList { EmailType = "FUTURE", ProfileTypeSeqId = 1, ProfileTypeValue = "WEEKLY", RoleseqId = 4 });
                    //get day of week
                    int myday = Int32.Parse(_ddlResponsiblePersonDayOfWeek.SelectedValue);
                    rpl.Add(new RecurringParametersList { EmailType = "FUTURE", ProfileTypeSeqId = 7, ProfileTypeValue = myday.ToString(), RoleseqId = 4 });
                }


                // loop through
                for (int i = 0; i <= 4; i++)
                    if (_rblResponsiblePersonFutureNotificationPeriod.Items[i].Selected == true)
                    {
                        rpl.Add(new RecurringParametersList { EmailType = "FUTURE", ProfileTypeSeqId = 10, ProfileTypeValue = _rblResponsiblePersonFutureNotificationPeriod.Items[i].Value.ToUpper(), RoleseqId = 4 });

                    }

                string responsible = "";
                for (int i = 0; i <= rpl.Count - 1; i++)
                    responsible += rpl[i].EmailType + "|" + rpl[i].ProfileTypeSeqId + "|" + rpl[i].ProfileTypeValue + "|" + rpl[i].RoleseqId + ",";


                return responsible;


            }
            catch (Exception ex)
            {
                throw;
            }

        }

        protected string BuildBusUnitMgr()
        {

            List<RecurringParametersList> rpl = new List<RecurringParametersList>();

            try
            {

                //"DAILY"
                if (_rbManagerEveryDay.Checked == true)
                {
                    rpl.Add(new RecurringParametersList { EmailType = "FUTURE", ProfileTypeSeqId = 1, ProfileTypeValue = "DAILY", RoleseqId = 1 });

                }
                // "MONTHLY"
                    if (_rblManagerEveryMonth.Checked == true)
                {
                    rpl.Add(new RecurringParametersList{EmailType ="FUTURE", ProfileTypeSeqId = 1, ProfileTypeValue = "MONTHLY", RoleseqId = 1});
                    //get day of month
                    rpl.Add(new RecurringParametersList{EmailType ="FUTURE", ProfileTypeSeqId = 5, ProfileTypeValue =  _ddlManagerOrdinalMonth.SelectedValue.ToString(), RoleseqId = 1});
                }
                // "WEEKLY"
                if (_rbManagerEveryWeek.Checked == true)
                {
                    rpl.Add(new RecurringParametersList{ EmailType = "FUTURE", ProfileTypeSeqId = 1, ProfileTypeValue = "WEEKLY", RoleseqId = 1 });
                    //get day of week
                    int myday = Int32.Parse(_ddlManagerDayOfWeek.SelectedValue);
                    rpl.Add(new RecurringParametersList { EmailType = "FUTURE", ProfileTypeSeqId = 7, ProfileTypeValue = myday.ToString(), RoleseqId = 1 });
                }

                
                // loop through
                for (int i = 0; i <= 4 ; i++)
                if (_rblManagerFutureNotificationPeriod.Items[i].Selected == true)
                {
                    rpl.Add(new RecurringParametersList { EmailType = "FUTURE", ProfileTypeSeqId = 10, ProfileTypeValue = _rblManagerFutureNotificationPeriod.Items[i].Value.ToUpper(), RoleseqId = 1 });

                }

                string busunitmgr = "";
                for (int i = 0; i <= rpl.Count - 1; i++)
                    busunitmgr += rpl[i].EmailType + "|" + rpl[i].ProfileTypeSeqId + "|" + rpl[i].ProfileTypeValue + "|" + rpl[i].RoleseqId + ",";


                return busunitmgr;


            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {

           
            try
            {

              in_RepeatingData = BuildBusUnitMgr();

              in_RepeatingData +=  BuildResponsible();

              in_RepeatingData +=  BuildCreator();


              if (in_RepeatingData.EndsWith(","))
                {
                    //remove ,
                    in_RepeatingData = in_RepeatingData.Remove(in_RepeatingData.Length - 1, 1); 
                }

             Boolean myreturn =  np.UpdateNotificationProfile("JAMES.BUTLER", in_RepeatingData, "JAMES.BUTLER");
               
            }

            catch (Exception ex)
            {
                throw;
            }



        }

        protected void SaveUserDefaults(string mySessionUserName)
        {




        }

        protected void LoadUserDefaults(string mySessionUserName)
        {

            List<UserModel.NotificationProfile> returnlist = new List<UserModel.NotificationProfile>();
            returnlist = np.GetUserDefaults(mySessionUserName);

            //load up the fields

            foreach (var p in returnlist)
            {
                Console.WriteLine(p);

                switch (p.RoleSeqId)
                {

                    case "1":   //BUSUNITMGR

                        switch (p.ProfileTypeSeqId)
                        {
                            case "1":
                                if (p.ProfileTypeValue == "MONTHLY")
                                { _rblManagerEveryMonth.Checked = true; }
                                if (p.ProfileTypeValue == "WEEKLY")
                                { _rbManagerEveryWeek.Checked = true; }
                                if (p.ProfileTypeValue == "DAILY")
                                {
                                    _rbManagerEveryDay.Checked = true;

                                }

                                break;
                            case "5":   //DAY

                                _ddlManagerOrdinalMonth.SelectedIndex = int.Parse(p.ProfileTypeValue) - 1;


                                break;

                            case "7":   //WEEKDAY

                                _ddlManagerDayOfWeek.SelectedIndex = int.Parse(p.ProfileTypeValue) - 1;


                                break;

                            case "10":
                                if (p.ProfileTypeValue == "ALL")
                                { _rblManagerFutureNotificationPeriod.Items[0].Selected = true; }
                                if (p.ProfileTypeValue == "NEXT 7 DAYS")
                                { _rblManagerFutureNotificationPeriod.Items[1].Selected = true; }
                                if (p.ProfileTypeValue == "NEXT 14 DAYS")
                                { _rblManagerFutureNotificationPeriod.Items[2].Selected = true; }
                                if (p.ProfileTypeValue == "NEXT 30 DAYS")
                                { _rblManagerFutureNotificationPeriod.Items[3].Selected = true; }
                                if (p.ProfileTypeValue == "NEXT 90 DAYS")
                                { _rblManagerFutureNotificationPeriod.Items[4].Selected = true; }
                                
                                break;

                        }

                        break;

                    case "4":   //RESPONSIBLE

                        switch (p.EmailType)

                        {
                            case "ENTERED":

                                
                                if (p.ProfileTypeValue == "IMMEDIATE")
                                { _rbEnteredNotifImmediately.Checked = true; }
                                if (p.ProfileTypeValue == "DAILY")
                                { _rbEnteredNotifEveryDay.Checked = true; }

                                break;


                            case "FUTURE":


                                switch (p.ProfileTypeSeqId)
                                {
                                    case "1":
                                        if (p.ProfileTypeValue == "WEEKLY")
                                        { _rbResponsiblePersonEveryWeek.Checked = true; }
                                        if (p.ProfileTypeValue == "DAILY")
                                        { _rbResponsiblePersonEveryDay.Checked = true; }

                                        break;

                                    case "7":   //WEEKDAY

                                        _ddlResponsiblePersonDayOfWeek.SelectedIndex = int.Parse(p.ProfileTypeValue) - 1;


                                        break;

                                    case "10":
                                        if (p.ProfileTypeValue == "ALL")
                                        { _rblResponsiblePersonFutureNotificationPeriod.Items[0].Selected = true; }
                                        if (p.ProfileTypeValue == "NEXT 7 DAYS")
                                        { _rblResponsiblePersonFutureNotificationPeriod.Items[1].Selected = true; }
                                        if (p.ProfileTypeValue == "NEXT 14 DAYS")
                                        { _rblResponsiblePersonFutureNotificationPeriod.Items[2].Selected = true; }
                                        if (p.ProfileTypeValue == "NEXT 30 DAYS")
                                        { _rblResponsiblePersonFutureNotificationPeriod.Items[3].Selected = true; }
                                        if (p.ProfileTypeValue == "NEXT 90 DAYS")
                                        { _rblResponsiblePersonFutureNotificationPeriod.Items[4].Selected = true; }
                                        break;

                                }


                                break;
                        }

                    break;

                    case "5":   //Task Creator

                        switch (p.ProfileTypeSeqId)
                        { 
                            case "1":
                                if (p.ProfileTypeValue == "MONTHLY")
                                { _rblEveryMonth.Checked = true; }
                                if (p.ProfileTypeValue == "WEEKLY")
                                { _rbEveryWeek.Checked = true; }
                                if (p.ProfileTypeValue == "DAILY")
                                {   
                                    _rbEveryDay.Checked = true;
                                    _ddlOrdinalMonth.SelectedIndex = -1;
                                    _ddlDayOfWeek.SelectedIndex = -1;
                                }
                               
                                break;
                            case "5":   //DAY

                                _ddlOrdinalMonth.SelectedIndex = int.Parse(p.ProfileTypeValue)-1;


                                break;

                            case "7":   //WEEKDAY

                                _ddlDayOfWeek.SelectedIndex = int.Parse(p.ProfileTypeValue) - 1;


                                break;

                            case "10":
                                if (p.ProfileTypeValue == "ALL")
                                { _rblFutureNotificationPeriod.Items[0].Selected = true; }
                                if (p.ProfileTypeValue == "NEXT 7 DAYS")
                                { _rblFutureNotificationPeriod.Items[1].Selected = true; }
                                if (p.ProfileTypeValue == "NEXT 14 DAYS")
                                { _rblFutureNotificationPeriod.Items[2].Selected = true; }
                                if (p.ProfileTypeValue == "NEXT 30 DAYS")
                                { _rblFutureNotificationPeriod.Items[3].Selected = true; }
                                if (p.ProfileTypeValue == "NEXT 90 DAYS")
                                { _rblFutureNotificationPeriod.Items[4].Selected = true; }
                                if (p.ProfileTypeValue == "NO EMAIL")
                                { _cbOptOut.Checked = true; }
                                break;

                        }

                    break;
                }
            }
        }


        //nothing below this line    
    }
}