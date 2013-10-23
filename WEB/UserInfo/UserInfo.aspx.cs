using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model.Entities;
using BLL.SystemSeting;

namespace WEB.UserInfo
{
    public partial class UserInfo : System.Web.UI.Page
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
                if (hy == null)
                {
                    Response.Redirect("../Auction/Index.aspx");
                }
                else 
                {
                    txtAdress.Text = hy.Adress;
                    txtPhone.Text = hy.sjh;
                    txtRealName.Text = hy.prName;
                    lblEmail.Text = hy.email;
                    lblUserName.Text = hy.HuiYuanName;
                    if (hy.IsEmailVerify == 1)
                    {
                        lblEmailStatus.Visible = true;
                    }
                    else 
                    {
                        lblEmailStatus.Visible = false;
                    }
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            HuiYuan hy = new HuiYuan();
            hy.HuiYuanID=Session["HuiYuanID"].ToString();
            hy.sex = rblSex.SelectedValue;
            hy.prName = txtRealName.Text.Trim();
            hy.sjh = txtPhone.Text.Trim();
            hy.Adress = txtAdress.Text;
            hy.sfz = "";
            hyBll.UpdateUserInfo(hy);
            Bind();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtAdress.Text = "";
            txtPhone.Text = "";
            txtRealName.Text = "";
            txtRealName.Focus();
        }

    }
}