using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tools;
using System.Data;
using Model.Entities;
using BLL.SystemSeting;

namespace WEB.Auction
{
    public partial class Auctioning : System.Web.UI.Page
    {
        ProductBLL proBll=new ProductBLL();
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
            if (Request.QueryString["id"] == null || Request.QueryString["id"] == "")
            {
                Response.Redirect("Index.aspx");
            }
            else 
            {
                string proId=Request.QueryString["id"].ToString();
                List<Product> list = proBll.GetById(proId);
                if (list.Count <= 0)
                {
                    MessageBox.AlertAndRedirect("该商品不存在！", "Index.aspx", Page);
                }
                else 
                {
                    Product pro=list[0];
                    List<ProductImeg> list_img = proBll.GetProtductImeg("",proId);
                    if (list_img.Count>0)
                    {
                        imgBig.ImageUrl=list_img[0].img;
                    }
                    //会员名
                    if (pro.HuiYuanID=="")
	                {
		                 lblMemberName.Text="";
	                }else
                    {
                        HuiYuan hy = hyBll.GetHuiYuan(pro.HuiYuanID);
                        if (hy!=null)
                        {
                            lblMemberName.Text = hy.HuiYuanName;
                        }
                    }
                    if (pro.AuctionTime>DateTime.Now.AddSeconds(10)&&pro.Status!=3)
                    {
                        TimeSpan time = Convert.ToDateTime(pro.AuctionTime) - DateTime.Now;
                        lblTime.Text = time.Hours.ToString().PadLeft(2, '0') + time.Minutes.ToString().PadLeft(2, '0') + time.Seconds.ToString().PadLeft(2, '0') + time.Milliseconds.ToString();
                    }
                    lblProName.Text = pro.productName;
                    lblIntro.Text = pro.Intro + " 第（" + pro.coding + ")期";
                    lblPrice.Text = pro.productPrice.ToString();
                    lblFee.Text = pro.Fee.ToString();
                    lblShipFee.Text = pro.ShipFee.ToString();
                    lblAuctionPrice.Text = pro.PmJGproduct.ToString();
                    
                }
            }
        }
    }
}