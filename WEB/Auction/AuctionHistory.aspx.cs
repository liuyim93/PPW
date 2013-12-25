using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model.Entities;
using BLL.SystemSeting;
using BLL;
using System.Data;

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
                BindData();
            }
        }

        public void Bind() 
        {
            //所有已成交的商品
            dropProductType.DataSource = productBll.GetAllProductType();
            dropProductType.DataTextField = "TypeName";
            dropProductType.DataValueField = "ProductTypeID";
            dropProductType.DataBind();
            dropProductType.Items.Insert(0,"不限");
            dropAuctionType.DataSource = auctionBll.GetAllAuctionType();
            dropAuctionType.DataTextField = "TypeName";
            dropAuctionType.DataValueField = "AuctionTypeID";
            dropAuctionType.DataBind();            
        }

        public void BindData() 
        {
            string auctionTypeId = dropAuctionType.SelectedValue;
            string proTypeId = dropProductType.SelectedValue;
            string minProPrice = "";
            string maxProPrice = "";
            string minAuctionPrice = "";
            string maxAuctionPrice = "";
            if (dropProductPrice.SelectedValue!="-1")
            {
                string proPrice = dropProductPrice.SelectedValue;
                string[] proPrices = proPrice.Split('-');
                if (proPrices[1]!="")
                {
                    maxProPrice = proPrices[1];
                }
                minProPrice=proPrices[0];
            }

            if (dropAuctionPrice.SelectedValue!="-1")
            {
                string auctionPrice = dropAuctionPrice.SelectedValue;
                string[] auctionPrices = auctionPrice.Split('-');
                if (auctionPrices[1]!="")
                {
                    maxAuctionPrice=auctionPrices[1];
                }
                minAuctionPrice=auctionPrices[0];
            }            
            DataTable dt = auctionBll.GetAuction_History(auctionTypeId,proTypeId,minProPrice,maxProPrice,minAuctionPrice,maxAuctionPrice);
            if (dt.Rows.Count>0)
            {
                AspNetPager1.RecordCount = dt.Rows.Count;
                PagedDataSource pds = new PagedDataSource();
                pds.DataSource = dt.DefaultView;
                pds.AllowPaging = true;
                pds.PageSize = AspNetPager1.PageSize;
                pds.CurrentPageIndex = AspNetPager1.CurrentPageIndex - 1;
                Repeater1.DataSource = pds;
                Repeater1.DataBind();
            }            
        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType==ListItemType.Item||e.Item.ItemType==ListItemType.AlternatingItem)
            {
                Image imgPro = e.Item.FindControl("imgPro") as Image;
                Literal ltlProName = e.Item.FindControl("ltlProName") as Literal;
                Literal ltlProPrice = e.Item.FindControl("ltlProPrice") as Literal;
                Label lblProIntro = e.Item.FindControl("lblProIntro") as Label;
                Literal ltlWinner = e.Item.FindControl("ltlWinner") as Literal;
                Literal ltlEndTime = e.Item.FindControl("ltlEndTime") as Literal;

                List<ProductImeg> list_img = productBll.GetProtductImeg(null,ltlProName.Text);
                if (list_img.Count > 0)
                {
                    imgPro.ImageUrl = list_img[0].img;
                }
                else 
                {
                    imgPro.ImageUrl = "";
                }

                List<Product> list_pro = productBll.GetById(ltlProName.Text);
                if (list_pro.Count>0)
                {
                    ltlProName.Text=list_pro[0].productName;
                    ltlProPrice.Text=list_pro[0].productPrice.ToString();
                    lblProIntro.Text=list_pro[0].Intro;
                }

                if (ltlEndTime.Text!="")
                {
                    ltlEndTime.Text = Convert.ToDateTime(ltlEndTime.Text).ToString("yyyy/MM/dd hh:mm:ss");
                }

                ltlWinner.Text = hyBll.GetHuiYuan(ltlWinner.Text).HuiYuanName;
            }
        }

        protected void AspNetPager1_PageChanged(object sender,EventArgs e) 
        {
            BindData();
        }

        protected void imgbtnSearch_Click(object sender, ImageClickEventArgs e)
        {
            BindData();
        }
    }
}