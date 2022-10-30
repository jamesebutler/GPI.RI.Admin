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
            Sql = "  from refemployee e where e.siteid = 'TS'";


            StringBuilder SQLbuilder = new StringBuilder();
            SQLbuilder.Append(" SELECT e.username, e.lastname, e.firstname, e.middleinit, e.email, e.extension, e.domain, UPPER(e.default_language) default_language, e.inactive_flag,");
            SQLbuilder.Append(" CASE e.inactive_flag   WHEN 'N' then 'Yes'    WHEN 'Y' then 'No'   end newinactive_flag");
            SQLbuilder.Append(" FROM refemployee e");
            SQLbuilder.Append(" WHERE 1=1 and e.domain = 'NA' and e.siteid = '" + GetBySiteId + "'");
            
            if (GetByInActive == "Y")
            {
                // get all records
            }
            else
            {
                //get only active records
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
            TextBoxNetWorkID.Text = "test.test";
            ButtonAddEmployee.Enabled = true;
        }


        protected void DropDownSites_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            // RadAjaxManager1.FocusControl(DropDownArea.ClientID + "_Input");
            if ((bool)CheckBoxAcitveOnly.Checked)
            {
                LoadEmployees(DropDownSites.SelectedValue, "Y");

            }
            else
            { 
                LoadEmployees(DropDownSites.SelectedValue,"N");
                }
        }


        protected void CheckBoxAcitveOnly_CheckedChanged(object sender, EventArgs e)
        {
            string checkBoxState = (bool)CheckBoxAcitveOnly.Checked ? "checked" : "unchecked";
       
            if (checkBoxState == "unchecked")
            {
                LoadEmployees(DropDownSites.SelectedValue, "N");

            }
            else
            {
                LoadEmployees(DropDownSites.SelectedValue, "Y");

            }
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

                    e.Item.BackColor = System.Drawing.Color.LightGoldenrodYellow; // for whole row
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


        protected void RadGridEmployees_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            
            // In the business tier, session can be accessed with:   
            // System.Web.HttpContext.Current.Session

            RadGridEmployees.DataSource = Session["EmployeeRecords"];
        }



        //Nothing below this line =======================================
    }
}