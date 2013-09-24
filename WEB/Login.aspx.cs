using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Diagnostics;
using System.Text;
using Ext.Net;
using Model.Entities;
using BLL.Home;
using Tools;
using System.Web.Security;
using System.Reflection;
using System.Configuration;

namespace WEB
{
   
    public partial class Login : Page
    {
        LoginBll loginbll = new LoginBll();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest&&!Page.IsPostBack)
            {
                CreateYzm();
            }   
        }
        public void btnlogin_click(object sender, DirectEventArgs e)
        {
            if (txtyzm.Text != Session["yzm"].ToString())
            {
                txtyzm.Clear();
                txtpwd.Clear();
                CreateYzm();
                X.Msg.Alert("提示", "验证码错误！").Show();
                return;
            }
            string hym = txtyhm.Text;
            string pwd = txtpwd.Text;
            object yh=loginbll.GetYongHu(hym,EncryptionAndDecryption.Encryption(pwd,Constant.Key));
            if (yh == null)
            {
                X.Msg.Alert("提示", "用户名或密码错误！").Show();
                txtyzm.Clear();
                txtpwd.Clear();
                CreateYzm();
                return;
            }
            Session["userid"] = yh;
            FormsAuthentication.SetAuthCookie(yh.ToString(), false);
         //   Response.Redirect("Index.aspx");
            X.Redirect("Index.aspx", "跳转中……");
        }

        private void CreateYzm()
        {
            Random r = new Random();
            int a = r.Next(1, 10);
            int b = r.Next(1,10);
            int i = r.Next(0,2);
            int c = a + b;
            string str = " + ";
            if (i == 0)
            {
                c = a * b;
                str = " × ";
            }
            Session["yzm"] = c;
            lblyzm.SetValue(a.ToString()+str+b.ToString()+" = ？");
        }
    }
}