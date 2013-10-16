using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using BLL.SystemSeting;
using Model.Entities;

namespace WEB.UserInfo
{
    public partial class AuctionOrder : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void Bind() 
        {
            if (Session["HuiYuanID"] == null || Session["HuiYuanName"] == null)
            {
                Response.Redirect("../Auction/Index.aspx");
            }
            else 
            {
                if (Request.QueryString["type"]!="")
                {
                    int type = Convert.ToInt32(Request.QueryString["type"]);
                    if (type==0)
                    {
                        BindAdress();
                    }
                }
            }
        }

        public void BindAdress() 
        {
            string hyId=Session["HuiYuanID"].ToString();

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

        }

        protected void rbtnAddAdress_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}