using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL.SystemSeting;
using Model.Entities;
using System.Transactions;
using BLL;

namespace WEB.Auction
{
    public partial class Index : System.Web.UI.Page
    {
        GonGaoBLL ggBll = new GonGaoBLL();
        ProductBLL productBll = new ProductBLL();
        HuiYuanXinXiBll hyBll = new HuiYuanXinXiBll();
        ChuJiaJiLuBll cjBll = new ChuJiaJiLuBll();
        protected void Page_Load(object sender, EventArgs e)
        {
            Bind();
        }

        public void Bind() 
        {
            //绑定新闻公告
            dlstNews.DataSource = ggBll.GetGgTop5();
            dlstNews.DataBind();
            //正在热拍
            dlstProduct.DataSource = productBll.GetAuctioningProduct();
            dlstProduct.DataBind();
        }

        protected void dlstProduct_ItemCommand(object source, DataListCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "auction":
                    try
                    {
                        using (TransactionScope ts = new TransactionScope())
                        {
                            string productId = e.CommandArgument.ToString();
                            Product pro = new Product();
                        }
                    }
                    catch (Exception)
                    {
                        
                        throw;
                    }                   
                    break;
                case "submit":
                    break;
                default:
                    break;
            }
        }

        //正在热拍
        protected void dlstProduct_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType==ListItemType.Item||e.Item.ItemType==ListItemType.AlternatingItem)
            {
                HiddenField hfProductID = e.Item.FindControl("hfProductID") as HiddenField;
                Image imgProduct = e.Item.FindControl("imgProduct") as Image;
                HiddenField hfProductName = e.Item.FindControl("hfProductName") as HiddenField;
                HiddenField hfProductNo = e.Item.FindControl("hfProductNo") as HiddenField;
                if (hfProductID.Value!="")
                {
                    List<ProductImeg> list = productBll.GetProtductImeg("",hfProductID.Value);
                    if (list.Count>0)
                    {
                        imgProduct.ImageUrl = list[0].img;
                        imgProduct.ToolTip = hfProductName.Value + " (第" + hfProductNo.Value + "号拍品)";//图片提示信息
                    }                    
                }
                HiddenField hfAuctionPoint = e.Item.FindControl("hfAuctionPoint") as HiddenField;
                ImageButton imgbtnAuction = e.Item.FindControl("imgbtnAuction") as ImageButton;
                imgbtnAuction.ToolTip = "每次出价消耗"+hfAuctionPoint.Value+"拍点";//出价按钮 提示信息
                Label lblTimer = e.Item.FindControl("lblTimer") as Label;
                if (lblTimer.Text!=""&&Convert.ToDateTime(lblTimer.Text)>DateTime.Now)
                {
                    TimeSpan ts = Convert.ToDateTime(lblTimer.Text) - DateTime.Now;
                    lblTimer.Text = ts.Hours.ToString().PadLeft(2, '0') + ":" + ts.Minutes.ToString().PadLeft(2, '0') + ":" + ts.Seconds.ToString().PadLeft(2,'0');//商品未开始竞拍的倒计时
                }
            }
        }
    }
}