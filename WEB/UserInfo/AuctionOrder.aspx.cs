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
        ShouHuoDZBll adressBll = new ShouHuoDZBll();
        DingDanBll orderBll = new DingDanBll();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bind();
            }
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
                    BindOrder();
                }
            }
        }

        public void BindAdress() 
        {
            string hyId=Session["HuiYuanID"].ToString();
            dlstShipAdress.DataSource = adressBll.GetShouHuoDZbyhyId(hyId);
            dlstShipAdress.DataBind();
        }

        public void BindOrder() 
        {
            string hyId=Session["HuiYuanID"].ToString();
            int status;
            string type=Request.QueryString["type"];
            if (type != "0" && type != "1" && type != "2")
            {
                Response.Redirect("../Auction/Index.aspx");
            }
            else 
            {
                if (type =="0")
                {
                    status = 10;
                }
                else 
                {
                    if (type == "1")
                    {
                        status = 8;
                    }
                    else 
                    {
                        status = 11;
                    }
                }
                dlstOrderList.DataSource = orderBll.GetDingDanbyhyId(hyId,status);
                dlstOrderList.DataBind();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

        }

        protected void rbtnAddAdress_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnAddAdress.Checked==true)
            {
                
            }
        }

        protected void dlstOrderList_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType==ListItemType.Item||e.Item.ItemType==ListItemType.AlternatingItem)
            {
                Image img = e.Item.FindControl("img") as Image;
                Label proName = e.Item.FindControl("lblProName") as Label;
                Label status = e.Item.FindControl("lblStatus") as Label;
                string proId = dlstOrderList.DataKeys[e.Item.ItemIndex].ToString();
            }
        }
    }
}