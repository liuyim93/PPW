using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model.Entities;
using BLL.SystemSeting;
using BLL;

namespace WEB.Auction
{
    public partial class AuctionHistory : System.Web.UI.Page
    {
        ProductBLL productBll=new ProductBLL();
        HuiYuanXinXiBll hyBll = new HuiYuanXinXiBll();
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
            //所有已成交的商品
            gvwHistory.DataSource = auctionBll.GetAllAuctioned();
            gvwHistory.DataBind();
            dropProductType.DataSource = productBll.GetAllProductType();
            dropProductType.DataTextField = "TypeName";
            dropProductType.DataValueField = "ProductTypeID";
            dropProductType.DataBind();
        }

        protected void gvwHistory_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType==DataControlRowType.DataRow)
            {
                Label lblMemberName = e.Row.FindControl("lblMemberName") as Label;
                HiddenField hfProductID = e.Row.FindControl("hfProductID") as HiddenField;
                HiddenField hfProductNo = e.Row.FindControl("hfProductNo") as HiddenField;
                Image imgProduct = e.Row.FindControl("imgProduct") as Image;
                Label proPrice = e.Row.FindControl("lblPrice") as Label;
                Label intro = e.Row.FindControl("lblIntro") as Label;
                HyperLink lnkPro = e.Row.FindControl("hlnkPro") as HyperLink;
                string auctionId=gvwHistory.DataKeys[e.Row.RowIndex].Value.ToString();
                List<auction> list_act = auctionBll.GetAuction(auctionId);
                if (hfProductID.Value!="")
                {
                    List<Product> list_pro = productBll.GetById(hfProductID.Value);
                    string proName=list_pro[0].productName;
                    proPrice.Text=list_pro[0].productPrice.ToString();
                    intro.Text=list_pro[0].Intro;
                    lnkPro.Text = proName;
                    lnkPro.ToolTip="第"+list_act[0].Coding+"期 "+proName;
                    lnkPro.NavigateUrl = "../Auction/ProDetail.aspx?id=" + auctionId;
                    List<ProductImeg> list = productBll.GetProtductImeg("",hfProductID.Value);
                    if (list.Count>0)
                    {
                        imgProduct.ImageUrl = list[0].img;
                        imgProduct.ToolTip = "第" + hfProductNo.Value + "期 " + proName;
                    }
                }
                if (lblMemberName.Text!="")
                {
                    lblMemberName.Text = hyBll.GetHuiYuan(lblMemberName.Text).HuiYuanName;
                }
            }
        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

        }

        protected void AspNetPager1_PageChanged(object sender,EventArgs e) 
        {
            
        }
    }
}