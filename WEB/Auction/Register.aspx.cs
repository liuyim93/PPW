using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL.SystemSeting;
using Model.Entities;
using Tools;
using System.Web.Security;

namespace WEB.Auction
{
    public partial class Register : System.Web.UI.Page
    {
        HuiYuanXinXiBll hyBll = new HuiYuanXinXiBll();
        protected void Page_Load(object sender, EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(Register));
        }
        //验证邮箱
        [AjaxPro.AjaxMethod]
        public string IsEmailAvailable(string email)
        {
            object obj = hyBll.IsEmailAvailable(email);
            if (obj == null)
            {
                return "1";
            }
            else 
            {
                return "-1";
            }
        }
        //验证用户名是否可用
        [AjaxPro.AjaxMethod]
        public string IsUserNameAvailable(string username) 
        {
            object obj = hyBll.IsUserNameAvailable(username);
            if (obj == null)
            {
                return "1";
            }
            else 
            {
                return "-1";
            }
        }
        //注册
        protected void imgbtnReg_Click(object sender, ImageClickEventArgs e)
        {
            if (txtUserName.Text == "")
            {
                MessageBox.Alert("用户名不能为空", this);
                return;
            }
            if (txtEmail.Text=="")
            {
                MessageBox.Alert("邮箱不能为空",this);
                return;
            }
            if (txtPassword.Text.Trim()=="")
            {
                MessageBox.Alert("密码不能为空",this);
                return;
            }
            if (txtVerifyCode.Text.ToLower()!=Session["ValidateNum"].ToString().ToLower())
            {
                MessageBox.Alert("验证码错误",this);
                return;
            }
            try
            {
                HuiYuan hy = new HuiYuan();
                hy.HuiYuanName = txtUserName.Text.Trim();
                hy.MM =FormsAuthentication.HashPasswordForStoringInConfigFile(txtPassword.Text.Trim(),"md5");
                hy.email = txtEmail.Text.Trim();
                hy.DJ = "普通会员";
                hyBll.AddHuiYuan(hy);
                MessageBox.Alert("注册成功",this);
            }
            catch (Exception)
            {

                MessageBox.Alert("注册失败",this);
            }            
        }
    }
}