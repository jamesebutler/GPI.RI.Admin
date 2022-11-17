using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

using GPI.MILL.DataAccess.Oracle;
using Microsoft.VisualBasic;
using GPI.Error.Logging;
using System.Configuration;

namespace GPI.RI.Admin
{
    public class Global : HttpApplication
    {


        void Application_Start(object sender, EventArgs e)
        {



            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


           

        }

        void Application_Error(object sender, EventArgs e)
        {


            Exception ex = Server.GetLastError();

            
            //log the error
            string errMsg = ex.ToString();
            errMsg = errMsg.Replace(ControlChars.CrLf, "<BR />");

            System.Web.HttpContext.Current.Session["errormessage"] = errMsg;

            //can pass currentHttpcontext to error handling class
            var currentHttpcontext = HttpContext.Current;

            ErrorLog errorLog = new ErrorLog();

                errorLog.UserName = Request.LogonUserIdentity.Name;
                errorLog.ErrorNumber = 0;
                errorLog.ErrorSeverity = 0;
                errorLog.ErrorState = 0;
                errorLog.ErrorProcedure = "";
                errorLog.ErrorForm = "Global.asax";
                errorLog.ErrorLine = 0;
                errorLog.ErrorMessage = errMsg;
                errorLog.ErrorMessageShort = ex.Message;
                errorLog.DBConnection = ConfigurationManager.ConnectionStrings["connectionRCFATST"].ConnectionString; ;
               string test = errorLog.LogError();



            Server.ClearError();
            Server.Transfer("~/ErrorPage.aspx");
        }



        void Session_Start(object sender, EventArgs e)
        {

            Session.Timeout = 180;
             string myname = Request.LogonUserIdentity.Name;
            System.Web.HttpContext.Current.Session["iname"] = myname;

        }












        //nothing below this line
    }
}