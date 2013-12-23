using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using Model;
using Model.Entities;
using Tools;
using System.Data;
using BLL.SystemSeting;

namespace WEB.Auction
{
    public partial class FutureAuction : System.Web.UI.Page
    {
        AuctionBll auctionBll = new AuctionBll();
        ProductBLL proBll=new ProductBLL();       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bind();
            }
        }

        public void Bind() 
        {
            DataTable dt = auctionBll.GetAuction_Future();
            if (dt.Rows.Count>0)
            {
                AspNetPager1.RecordCount = dt.Rows.Count;
                PagedDataSource pds = new PagedDataSource();
                pds.DataSource = dt.DefaultView;
                pds.AllowPaging = true;
                pds.PageSize = AspNetPager1.PageSize;
                pds.CurrentPageIndex = AspNetPager1.CurrentPageIndex - 1;
                repeater1.DataSource = pds;
                repeater1.DataBind();
            }
        }

        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            Bind();
        }

        protected void repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType==ListItemType.Item||e.Item.ItemType==ListItemType.AlternatingItem)
            {
                Literal ltlProName = e.Item.FindControl("ltlProName") as Literal;
                Literal ltlProPrice = e.Item.FindControl("ltlProPrice") as Literal;
                Label lblAuctionTime = e.Item.FindControl("lblAuctionTime") as Label;
                Image imgPro = e.Item.FindControl("imgPro") as Image;
                Label lblProIntro = e.Item.FindControl("lblProIntro") as Label;

                List<ProductImeg> list_img = proBll.GetProtductImeg(null, ltlProName.Text);
                if (list_img.Count > 0)
                {
                    imgPro.ImageUrl = list_img[0].img;
                }
                else
                {
                    imgPro.ImageUrl = "";
                } 

                List<Product> list_pro = proBll.GetById(ltlProName.Text);
                if (list_pro.Count > 0)
                {
                    ltlProName.Text = list_pro[0].productName;
                    ltlProPrice.Text = list_pro[0].productPrice.ToString();
                    lblProIntro.Text=list_pro[0].Intro;
                }

                if (lblAuctionTime.Text!="")
                {
                    DateTime dt = Convert.ToDateTime(lblAuctionTime.Text);
                    lblAuctionTime.Text = dt.Month.ToString() + "月" + dt.Day.ToString() + "日" + dt.Hour.ToString() + "时" + dt.Minute.ToString() + "分开拍";
                }

                               
            }           
        }
    }
}