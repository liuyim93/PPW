using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model.Entities;
using BLL.SystemSeting;
using Tools;
using System.Web.Security;

namespace WEB.Auction
{
    public partial class UserLogin : System.Web.UI.Page
    {
        HuiYuanXinXiBll hyBll = new HuiYuanXinXiBll();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //记录用户登录失败的次数，超过三次显示验证码
                Session["Error"] = 0;
                if (Convert.ToInt32(Session["Error"])>2&&Session["Error"]!=null)
                {
                    pnlCheckCode.Visible = true;
                }
                if (Response.Cookies["UserName"]!=null)
                {
                    txtUserName.Text=Response.Cookies["UserName"].Value;
                }
            }
        }

        protected void imgbtnLogin_Click(object sender, ImageClickEventArgs e)
        {
            if (txtUserName.Text.Trim()==""||txtUserName.Text.Trim()==null)
            {
                //MessageBox.Alert("用户名不能为空",Page);
                return;
            }
            if (txtPassword.Text.Trim()==""||txtPassword.Text.Trim()==null)
            {
                //MessageBox.Alert("请输入密码",Page);
                return;
            }
            if (pnlCheckCode.Visible==true&&txtCheckCode.Text.Trim().ToLower()!=Session["ValidateNum"].ToString().ToLower())
            {
                MessageBox.Alert("验证码错误",Page);
                return;
            }            
            //创建cookie，记住用户名
            Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(-1);
            if (chkRememberName.Checked)
            {
                Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(7);
            }
            string username = txtUserName.Text.Trim();
            Response.Cookies["UserName"].Value=username;
            string password = FormsAuthentication.HashPasswordForStoringInConfigFile(txtPassword.Text.Trim(),"md5");            
            HuiYuan hy = hyBll.GetHuiYuan(username,password);
            if (hy == null)
            {
                int error = Convert.ToInt32(Session["Error"]);
                error++;
                Session["Error"] = error;
                MessageBox.Alert("用户名或密码错误", Page);
            }
            else 
            {
                Session["HuiYuanName"] = username;
                Session["HuiYuanID"] = hy.HuiYuanID;
                Response.Redirect("index.aspx");
            }
        }
    }
}