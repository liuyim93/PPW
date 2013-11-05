using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model.Entities;
using BLL.SystemSeting;
using BLL;

namespace WEB.Auction.UserControl
{
    public partial class Recommand : System.Web.UI.UserControl
    {
        ProductBLL proBll = new ProductBLL();
        AuctionBll auctionBll = new AuctionBll();
        protected void Page_Load(object sender, EventArgs e)
        {
            //推荐商品
            dlstRecommend.DataSource = auctionBll.GetRecommendAuction_Top5();
            dlstRecommend.DataBind();
        }

        protected void dlstRecommend_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HyperLink hlnkProName = e.Item.FindControl("hlnkProName") as HyperLink;
                Image imgProduct = e.Item.FindControl("imgProduct") as Image;
                Label lblAuctionTime = e.Item.FindControl("lblAuctionTime") as Label;
                HiddenField hfProductID = e.Item.FindControl("hfProductID") as HiddenField;
                HiddenField hfProductNo = e.Item.FindControl("hfProductNo") as HiddenField;
                Label proPrice = e.Item.FindControl("lblProPrice") as Label;
                string auctionId = dlstRecommend.DataKeys[e.Item.ItemIndex].ToString();

                List<ProductImeg> list = proBll.GetProtductImeg("", hfProductID.Value);               
                if (list.Count > 0)
                {
                    imgProduct.ImageUrl = "../"+list[0].img;
                }
                else
                {
                    imgProduct.ImageUrl = "";
                }

                List<Product> list_pro = proBll.GetById(hfProductID.Value);
                if (list_pro.Count>0)
                {
                    hlnkProName.ToolTip = "第" + hfProductNo.Value + "期 " + list_pro[0].productName;
                    hlnkProName.NavigateUrl = "../Auction/ProDetail.aspx?id="+auctionId;
                    hlnkProName.Text=list_pro[0].productName;
                    imgProduct.ToolTip = hlnkProName.ToolTip;
                    proPrice.Text=list_pro[0].productPrice.ToString();
                }
                lblAuctionTime.Text = Convert.ToDateTime(lblAuctionTime.Text).Hour.ToString().PadLeft(2, '0') + ":" + Convert.ToDateTime(lblAuctionTime.Text).Minute.ToString().PadLeft(2, '0');
            }
        }
    }
}