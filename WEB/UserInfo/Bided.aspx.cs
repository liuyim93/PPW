using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using Model.Entities;
using BLL.SystemSeting;

namespace WEB.UserInfo
{
    public partial class Bided : System.Web.UI.Page
    {
        ProductBLL proBll = new ProductBLL();
        HuiYuanXinXiBll hyBll = new HuiYuanXinXiBll();
        ChuJiaJiLuBll recordBll = new ChuJiaJiLuBll();
        AuctionBll auctionBll = new AuctionBll();
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
                string hyId=Session["HuiYuanID"].ToString();
                int status = 3;
                dlstBided.DataSource = auctionBll.GetAuctionbyStatus(hyId,status);
                dlstBided.DataBind();
            }
        }

        protected void dlstBided_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType==ListItemType.Item||e.Item.ItemType==ListItemType.AlternatingItem)
            {
                Label endTime = e.Item.FindControl("lblEndTime") as Label;
                Label fullTime = e.Item.FindControl("lblFullTime") as Label;
                Image img = e.Item.FindControl("img") as Image;
                Label nums = e.Item.FindControl("lblNums") as Label;
                Label memberName = e.Item.FindControl("lblMemberName") as Label;
                Label bidCount = e.Item.FindControl("lblBidCount") as Label;
                Label pointCount = e.Item.FindControl("lblPointCount") as Label;
                HyperLink hlnkBuy = e.Item.FindControl("hlnkBuy") as HyperLink;
                Label timeOut = e.Item.FindControl("lblTimeOut") as Label;
                Literal proName = e.Item.FindControl("ltlProName") as Literal;
                Label proPrice = e.Item.FindControl("lblPrice") as Label;
                HiddenField auctionId = e.Item.FindControl("hfAuctionID") as HiddenField;

                if (endTime.Text!="")
                {
                    fullTime.Text = Convert.ToDateTime(endTime.Text).AddDays(3).ToString();
                    if (DateTime.Now > Convert.ToDateTime(fullTime.Text))
                    {
                        timeOut.Visible = true;
                        hlnkBuy.Visible = false;
                    }
                    else 
                    {
                        timeOut.Visible = false;
                        hlnkBuy.Visible = true;
                    }
                }
                string proId = dlstBided.DataKeys[e.Item.ItemIndex].ToString();
                string hyId=Session["HuiYuanID"].ToString();
                List<ProductImeg> list_img = proBll.GetProtductImeg("",proId);
                if (list_img.Count > 0)
                {
                    img.ImageUrl = list_img[0].img;
                }
                else 
                {
                    img.ImageUrl = "";
                }

                memberName.Text = hyBll.GetHuiYuan(memberName.Text).HuiYuanName;

                List<ChuJiaJiLu> list_record = recordBll.GetChuJiaJiLu(auctionId.Value, hyId);
                List<auction> list_act = auctionBll.GetAuction(auctionId.Value);
                List<Product> list_pro = proBll.GetById(proId);
                bidCount.Text = list_record.Count.ToString();
                if (proBll.GetAuctionTypebyID(list_act[0].AuctionTypeID)[0].TypeName == "常规竞拍")
                {
                    pointCount.Text = "拍点：" + ((list_act[0].AuctionPoint + list_act[0].FreePoint) * Convert.ToInt32(bidCount.Text)).ToString();
                }
                else
                {
                    pointCount.Text = "返点：" + ((list_act[0].AuctionPoint + list_act[0].FreePoint) * Convert.ToInt32(bidCount.Text)).ToString();
                }
                proPrice.Text=list_pro[0].productPrice.ToString();
                proName.Text=list_pro[0].productName;

                List<ChuJiaJiLu> list_record1 = recordBll.GetChuJiaJiLubyauctionId(auctionId.Value);
                nums.Text = list_record1.Count.ToString();
            }
        }
    }
}