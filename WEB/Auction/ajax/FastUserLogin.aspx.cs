using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using BLL.SystemSeting;
using Model.Entities;
using System.Web.Security;

namespace WEB.Auction.ajax
{
    public partial class FastUserLogin : System.Web.UI.Page
    {
        HuiYuanXinXiBll hyBll = new HuiYuanXinXiBll();
        protected void Page_Load(object sender, EventArgs e)
        {
            string msg = "";
            if(Request.QueryString["username"]!=null&&Request.QueryString["password"]!=null){
                string userName=Request.QueryString["username"];
                string password=Request.QueryString["password"];
                string pwd = FormsAuthentication.HashPasswordForStoringInConfigFile(password,"md5");
                HuiYuan hy = hyBll.GetHuiYuan(userName,pwd);
                if (hy!=null)
                {
                    msg = "1";
                    Session["HuiYuanName"]=hy.HuiYuanName;
                    Session["HuiYuanID"]=hy.HuiYuanID;
                }
            }
            Response.Clear();
            Response.Write(msg);
            Response.End();
        }
    }
}