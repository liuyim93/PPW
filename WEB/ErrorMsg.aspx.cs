using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using Model.Entities;
using BLL.Home;
using Tools;
using System.IO;
using BLL;

namespace WEB
{
    public partial class ErrorMsg : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //try
                //{
                //    Exception ex = Server.GetLastError().GetBaseException();
                //    BLL.ErrorLogBLL log = new BLL.ErrorLogBLL();
                //    log.WriteErrorLog(ex.ToString(), "");
                //    Server.ClearError();
                //}
                //catch
                //{

                //}
            }
        }
    }
}