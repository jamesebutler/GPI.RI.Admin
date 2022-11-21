using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;

using GPI.User.Model;

namespace GPI.RI.Admin.UserControls
{
    public partial class NotificationFrequency : System.Web.UI.UserControl
    {
        private StringDictionary notificationDefaults = new StringDictionary();
        private System.Collections.Generic.List<UserModel.NotificationProfile> _notificationProfile;

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

        public enum NotificationPeriod
        {
            FuturePeriod,
            PreviousPeriod,
            NoPeriod

        }

        public enum PreviousPeriod
        {
            Previous7Days,
            PreviousCalendarMonth
        }

        public enum FuturePeriod
        {
            All,
            Next7Days,
            Next14Days,
            ThisMonth,
            ThisQuarter
        }

        public enum UserTypeValue
        {
            Creator,
            Responsible,
            BusinessUnitManager,
            TypeManager
        }


        private bool _AllowOptOut; // = False

        public bool AllowOptOut
            {
                get
                {
                    return _AllowOptOut;
                }
                set
                {
                    _AllowOptOut = value;
                }
            }
        public System.Collections.Generic.List<UserModel.NotificationProfile> NotificationProfile
        {
            get
            {
                return _notificationProfile; // GetSelectedNotificationProfile()
            }
            set
            {
                _notificationProfile = value;
            }
        }
        public bool Enabled
            {
                get
                {
                    return this._pnlNotificationFrequency.Enabled;
                }
                set
                {
                    this._pnlNotificationFrequency.Enabled = value;
                }
            }



            private UserTypeValue mUserType = UserTypeValue.Creator;
            public UserTypeValue UserType
            {
                get
                {
                    return mUserType;
                }
                set
                {
                    mUserType = value;
                    SetupNotificationOptions();
                }
            }

            public string GroupingText
            {
                get
                {
                    return this._pnlNotificationFrequency.GroupingText;
                }
                set
                {
                    this._pnlNotificationFrequency.GroupingText = value.Trim();
                }
            }

            public string OptOutLabel
            {
                get
                {
                    return _cbOptOut.Text;
                }
                set
                {
                    this._cbOptOut.Text = value;
                }
            }
            public string NotificationLabel
            {
                get
                {
                    return _lblNotificationHeading.Text;
                }
                set
                {
                    _lblNotificationHeading.Text = value;
                }
            }

            public string ImmediateLabel
            {
                get
                {
                    return _rblImmediate.Text;
                }
                set
                {
                    _rblImmediate.Text = value;
                }
            }

            public string EveryDayLabel
            {
                get
                {
                    return _rbEveryDay.Text;
                }
                set
                {
                    _rbEveryDay.Text = value;
                }
            }

            public string EveryWeekLabel
            {
                get
                {
                    return _rbEveryWeek.Text;
                }
                set
                {
                    _rbEveryWeek.Text = value;
                }
            }

            public string EveryMonthLabel
            {
                get
                {
                    return _rblEveryMonth.Text;
                }
                set
                {
                    _rblEveryMonth.Text = value;
                }
            }


            public string WhenEnteredLabel
            {
                get
                {
                    return _lblWhenEntered.Text;
                }
                set
                {
                    _lblWhenEntered.Text = value;
                }
            }

            public string FutureTimePeriodLabel
            {
                get
                {
                    return _lblFutureTimePeriodHeader.Text;
                }
                set
                {
                    _lblFutureTimePeriodHeader.Text = value;
                }
            }


        private void SetupNotificationOptions()
        {
            switch (UserType)
            {
                case UserTypeValue.BusinessUnitManager:
                    {
                        // Daily,Weekly,Monthly
                        SetFrequencyOptions(false, true, true, true, false);
                        SetPeriodOptions(true);
                        break;
                    }

                case UserTypeValue.Creator:
                    {
                        // Daily,Weekly,Monthly
                        SetFrequencyOptions(false, true, true, true, true);
                        SetPeriodOptions(true);
                        break;
                    }

                case UserTypeValue.Responsible:
                    {
                        // Daily,Weekly
                        SetFrequencyOptions(true, true, true, false, false);
                        SetPeriodOptions(true);
                        break;
                    }

                case  UserTypeValue.TypeManager:
                    {
                        // Daily,Weekly,Monthly
                        SetFrequencyOptions(false, true, true, true, false);
                        SetPeriodOptions(true);
                        break;
                    }
            }
        }


        public void SetFrequencyOptions(bool allowImmediate, bool allowDaily, bool allowWeekly, bool allowMonthly, bool allowOptOut)
        {
            this._rblImmediate.Enabled = allowImmediate;
            this._rblImmediate.Visible = allowImmediate;
            this._rbEveryDay.Visible = allowDaily;
            this._lblWhenEntered.Enabled = allowImmediate;
            this._lblWhenEntered.Visible = allowImmediate;
            this._rbEveryWeek.Visible = allowWeekly;
            this._ddlDayOfWeek.Visible = allowWeekly;
            this._rblEveryMonth.Visible = allowMonthly;
            this._ddlOrdinalMonth.Visible = allowMonthly;
            this._cbOptOut.Visible = allowOptOut;
            _lblOptOut.Visible = allowOptOut;
            _litOptOut.Visible = allowOptOut;
        }

        public void SetPeriodOptions(bool showNotificationDateRange)
        {

            if (showNotificationDateRange)
            {
      
                {
                    var withBlock = _rblFutureNotificationPeriod;
                    // set with future periods
                    withBlock.Items.Clear();
                    withBlock.Items.Add(new ListItem("&nbsp;" + "All","All" ));
                    withBlock.Items.Add(new ListItem("&nbsp;" + "Next 7 Days","Next 7 Days"));
                    withBlock.Items.Add(new ListItem("&nbsp;" + "Next 14 Days","Next 14 Days"));
                    withBlock.Items.Add(new ListItem("&nbsp;" + "Next 30 Days","Next 30 Days"));
                    withBlock.Items.Add(new ListItem("&nbsp;" + "Next 90 Days","Next 90 Days"));
                }
            }
            else if (showNotificationDateRange == false)
            {
                {
                    var withBlock = _rblFutureNotificationPeriod;
                    // set with future periods
                    withBlock.Items.Clear();
                }
                this._lblFutureTimePeriodHeader.Visible = false;
            }
        }



        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

                PopulateDaysOfWeek();
                PopulateMonthOrdinals();
                DefaultSettings();
                //SetDefaultValues();
            }

        }
        

        private void DefaultSettings()
        {

            if (OptOutLabel.Length == 0)
                OptOutLabel = "&nbsp;" + "Do not send Emails";
            if (NotificationLabel.Length == 0)
                NotificationLabel = "&nbsp;" + "Task Notification Frequency";
          if (EveryDayLabel.Length == 0)
                EveryDayLabel = "&nbsp;" + "Daily";
           if (EveryWeekLabel.Length == 0)
                EveryWeekLabel = "&nbsp;" + "Weekly on";
            if (EveryMonthLabel.Length == 0)
                EveryMonthLabel = "&nbsp;" + "Monthly on day";
            if (FutureTimePeriodLabel.Length == 0)
                FutureTimePeriodLabel = "&nbsp;" + "Notification Time Period for Future Tasks";
           if (WhenEnteredLabel.Length == 0)
                WhenEnteredLabel = "&nbsp;" + "When tasks are Entered Notify Me";

            this._rbEveryDay.GroupName = "Group" + ID;
            this._rbEveryWeek.GroupName = "Group" + ID;
            this._rblEveryMonth.GroupName = "Group" + ID;
            PopulateImmediateOptions();
            SetupNotificationOptions();
        }



        private void PopulateDaysOfWeek()
        {
            {

                _ddlDayOfWeek.Items.Clear();

                List<string> daysList = new List<string>(Enum.GetNames(typeof(DaysofWeek)));
                for (int i = 0; i <= daysList.Count-1; i++) 
                    _ddlDayOfWeek.Items.Add(new ListItem(daysList[i],System.Convert.ToString(i+1)));

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
            }
        }

        private void PopulateImmediateOptions()
        {
            {
                var withBlock = _rblImmediate;
                withBlock.Items.Clear();
                withBlock.Items.Add(new ListItem("Immediately","IMMEDIATE"));
                withBlock.Items.Add(new ListItem("Daily", "DAILY"));
            }
        }

    }
}