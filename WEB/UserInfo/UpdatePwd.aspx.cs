using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model.Entities;
using BLL.SystemSeting;
using System.Security;
using System.Web.Security;
using Tools;

namespace WEB.UserInfo
{
    public partial class UpdatePwd : System.Web.UI.Page
    {
        HuiYuanXinXiBll hyBll = new HuiYuanXinXiBll();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bind();
            }
        }

        public void Bind() 
        {
            if (Session["HuiYuanName"] == null || Session["HuiYuanID"] == null)
            {
                Response.Redirect("../Auction/Index.aspx");
            }
            else
            {
                string hyId=Session["HuiYuanID"].ToString();
                HuiYuan hy = hyBll.GetHuiYuan(hyId);
                lblUserName.Text = hy.HuiYuanName;
                lblEmail.Text = hy.email;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (txtNewPwd.Text == "" || txtPwd.Text == "" || txtPwdConfirm.Text == "" || txtNewPwd.Text != txtPwdConfirm.Text)
            {
                return;
            }
            else 
            {
                string pwd = FormsAuthentication.HashPasswordForStoringInConfigFile(txtPwd.Text.Trim(),"md5");
                string  hyId=Session["HuiYuanID"].ToString();
                string hyName=Session["HuiYuanName"].ToString();
                if (hyBll.GetHuiYuan(hyName, pwd) == null)
                {
                    return;
                }
                else 
                {                    
                    try
                    {
                        string newPwd = FormsAuthentication.HashPasswordForStoringInConfigFile(txtPwdConfirm.Text.Trim(), "md5");
                        hyBll.UpdatePwd(hyId, newPwd);
                        txtNewPwd.Text = "";
                        txtPwd.Text = "";
                        txtPwdConfirm.Text = "";
                        MessageBox.Alert("修改成功", Page);
                    }
                    catch (Exception)
                    {

                        MessageBox.Alert("修改失败",Page);
                    }
                }
            }
        }
    }
}