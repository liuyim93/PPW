using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Model;

namespace WEB
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
           
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            //Session.Timeout = 60;
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            //try
            //{
            //    Exception ex = Server.GetLastError().GetBaseException();
            //    BLL.ErrorLogBLL log = new BLL.ErrorLogBLL();
            //    log.WriteErrorLog(ex.ToString(), "");
            //    Response.Redirect("~/ErrorMsg.aspx");
            //}
            //catch
            //{

            //}
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}