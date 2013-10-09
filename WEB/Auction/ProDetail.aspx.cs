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
using BLL;

namespace WEB.Auction
{
    public partial class Auctioning : System.Web.UI.Page
    {
        ProductBLL proBll=new ProductBLL();
        HuiYuanXinXiBll hyBll = new HuiYuanXinXiBll();
        ChuJiaJiLuBll recordBll = new ChuJiaJiLuBll();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bind();
                BindgvwHistory();
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
                    //是否已成交
                    if (pro.AuctionTime > DateTime.Now.AddSeconds(10) && pro.Status != 3)
                    {
                        TimeSpan time = Convert.ToDateTime(pro.AuctionTime) - DateTime.Now;
                        lblTime.Text = time.Hours.ToString().PadLeft(2, '0') + time.Minutes.ToString().PadLeft(2, '0') + time.Seconds.ToString().PadLeft(2, '0') + time.Milliseconds.ToString();
                        pnlAuction.Visible = true;
                        pnlEnd.Visible = false;
                    }
                    else 
                    {
                        pnlEnd.Visible = true;
                        pnlAuction.Visible = false;
                        lblStart.Text = pro.AuctionTime.ToString();
                        lblEnd.Text = pro.EndTime.ToString();
                    }
                    //判断竞拍类型
                    string free = proBll.GetAuctionTypebyName("免费竞拍")[0].AuctionTypeID;
                    if (pro.AuctionTypeID == free)
                    {
                        lblPoint.Text = pro.FreePoint.ToString();
                    }
                    else 
                    {
                        lblPoint.Text = pro.AuctionPoint.ToString();
                    }
                    lblPriceAdd.Text = pro.PriceAdd.ToString();
                    lblProName.Text = pro.productName;
                    lblIntro.Text = pro.Intro + " 第（" + pro.coding + ")期";
                    lblPrice.Text = "￥ "+pro.productPrice.ToString();
                    lblFee.Text = "￥ " + pro.Fee.ToString();
                    lblShipFee.Text = "￥ " +pro.ShipFee.ToString();
                    lblAuctionPrice.Text = "￥ " + pro.PmJGproduct.ToString();
                    lblDetail.Text = pro.ProductDetails;
                }
            }
        }

        public void BindgvwHistory() 
        {
            string proId=Request.QueryString["id"].ToString();
            gvwHistory.DataSource = recordBll.GetChuJiaJiLubyProId_Top10(proId);
            gvwHistory.DataBind();
        }

        protected void gvwHistory_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType==DataControlRowType.DataRow)
            {
                Label lblMemberName = e.Row.FindControl("lblMemberName") as Label;
                HiddenField hfMemberID = e.Row.FindControl("hfMemberID") as HiddenField;
                Label lblIPAdress = e.Row.FindControl("lblIPAdress") as Label;
                if (hfMemberID.Value != "")
                {
                    lblMemberName.Text = hyBll.GetHuiYuan(hfMemberID.Value).HuiYuanName;
                }
                else 
                {
                    lblMemberName.Text = "";
                }
                string Adress = lblIPAdress.Text;
                if (Adress != "")
                {
                    string[] IP = Adress.Split('.');
                    string IPAdress = "";
                    for (int i = 0; i < IP.Length - 1; i++)
                    {
                        IPAdress += IP[i] + ".";
                    }
                    IPAdress += "*";
                    lblIPAdress.Text = IPAdress;
                }
                else 
                {
                    lblIPAdress.Text = "";
                }
            }
        }
    }
}