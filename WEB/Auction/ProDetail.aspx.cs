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
using System.Transactions;

namespace WEB.Auction
{
    public partial class Auctioning : System.Web.UI.Page
    {
        ProductBLL proBll=new ProductBLL();
        HuiYuanXinXiBll hyBll = new HuiYuanXinXiBll();
        ChuJiaJiLuBll recordBll = new ChuJiaJiLuBll();
        DingDanBll orderBll = new DingDanBll();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bind();
                BindgvwHistory();
                BindMyAuction();
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
                    if (pro.Status != 3)
                    {                        
                        pnlAuction.Visible = true;
                        pnlEnd.Visible = false;
                        if (pro.AuctionTime > DateTime.Now.AddSeconds(10))
                        {
                            TimeSpan time = Convert.ToDateTime(pro.AuctionTime) - DateTime.Now;
                            lblTime.Text = time.Hours.ToString().PadLeft(2, '0') + time.Minutes.ToString().PadLeft(2, '0') + time.Seconds.ToString().PadLeft(2, '0') + time.Milliseconds.ToString();
                        }
                        else 
                        {
                            lblTime.Text = "00:00:" + pro.TimePoint.ToString().PadLeft(2,'0');
                        }
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

        //出价记录
        public void BindgvwHistory() 
        {
            string proId=Request.QueryString["id"].ToString();
            gvwHistory.DataSource = recordBll.GetChuJiaJiLubyProId_Top10(proId);
            gvwHistory.DataBind();
        }
        //我的竞拍
        public void BindMyAuction() 
        {
            string proId=Request.QueryString["id"].ToString();
            List<Product> list = proBll.GetById(proId);
            if (list.Count>0)
            {
                ltlProPrice.Text=list[0].productPrice.ToString();
            }
            if (Session["HuiYuanName"] == null || Session["HuiYuanID"] == null)
            {
                lblAuctionPoint.Text = "0";
                lblFreePoint.Text = "0";
                lblUsed.Text = "0";
            }
            else 
            {
                int auction = 0;
                int free = 0;
                string memberId=Session["HuiYuanID"].ToString();
                List<ChuJiaJiLu> list1 = recordBll.GetChuJiaJiLu(proId,memberId);
                if (list1 == null)
                {
                    lblAuctionPoint.Text = "0";
                    lblFreePoint.Text = "0";
                    lblUsed.Text = "0";
                }
                else 
                {
                    for (int i = 0; i < list1.Count; i++)
                    {
                        auction += list1[i].AuctionPoint;
                        free+=list1[i].FreePoint;
                    }
                    lblAuctionPoint.Text = auction.ToString();
                    lblFreePoint.Text = free.ToString();
                    lblUsed.Text=((auction+free)/100).ToString();
                }
            }
            if (ltlProPrice.Text!=""&&lblUsed.Text!="")
            {
                lblPay.Text = (Convert.ToDecimal(ltlProPrice.Text) - Convert.ToDecimal(lblUsed.Text)).ToString();
            }
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

        protected void imgbtnAuction_Click(object sender, ImageClickEventArgs e)
        {
            string proId=Request.QueryString["id"];
            if (Session["HuiYuanID"] == null || Session["HuiYuanName"] == null)
            {
                MessageBox.AlertAndRedirect("请先登录", "UserLogin.aspx", Page);
            }
            else 
            {
                string hyId=Session["HuiYuanID"].ToString();
                using (TransactionScope ts=new TransactionScope())
                {
                    Product pro = new Product();
                    pro.ProductID = proId;
                    pro.HuiYuanID = hyId;
                    pro.TimePoint = 10;
                    proBll.UpdateProductPrice(pro);

                    Product pro1 = proBll.GetById(proId)[0];
                    ChuJiaJiLu record = new ChuJiaJiLu();
                    record.ProductID = proId;
                    record.HuiYuanID = hyId;
                    record.Price = Convert.ToDecimal(pro1.PmJGproduct);
                    record.IPAdress = Request.UserHostAddress;
                    record.Status = 1;
                    record.AuctionTime = DateTime.Now;
                    record.AuctionPoint = pro1.AuctionPoint;
                    record.FreePoint = pro1.FreePoint;
                    recordBll.AddChuJiaJiLu(record);

                    HuiYuan hy = new HuiYuan();
                    hy.HuiYuanID = hyId;
                    hy.PaiDian = pro1.AuctionPoint * -1;
                    hy.FreePoint = pro1.FreePoint * -1;
                    hyBll.UpdateHuiYuanPoint(hy);
                    lblAuctionPrice.Text = pro1.PmJGproduct.ToString();
                    lblMemberName.Text = hyBll.GetHuiYuan(pro1.HuiYuanID).HuiYuanName;
                    ts.Complete();                    
                }                
                BindgvwHistory();
                BindMyAuction();
            }
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            string proId=Request.QueryString["id"];
            List<Product>list = proBll.GetById(proId);
            if (list.Count>0)
            {
                Product pro=list[0];
                DateTime auctiontime = Convert.ToDateTime(pro.AuctionTime);
                if (auctiontime > DateTime.Now.AddSeconds(10) && pro.Status != 3)
                {
                    TimeSpan ts = auctiontime-DateTime.Now;
                    lblTime.Text = ts.Hours.ToString().PadLeft(2, '0') + ts.Minutes.ToString().PadLeft(2, '0') + ts.Seconds.ToString().PadLeft(2, '0') + ts.Milliseconds.ToString().PadLeft(2, '0');
                }
                else 
                {
                    if (pro.Status!=3&&pro.TimePoint>0)
                    {
                        proBll.UpdateTimePoint(proId);
                        Product pro1 = proBll.GetById(proId)[0];
                        if (pro1.TimePoint != 0)
                        {
                            lblTime.Text = "00:00:" + pro1.TimePoint.ToString().PadLeft(2, '0');
                        }
                        else 
                        {
                            lblTime.Text = "即将成交";
                            using (TransactionScope ts1=new TransactionScope())
                            {
                                //修改商品状态
                                Product pro2 = new Product();
                                pro2.ProductID = proId;
                                pro2.Status = 3;
                                pro2.EndTime = DateTime.Now;
                                proBll.UpdateProductStatus(pro2);

                                //生成竞拍订单
                                Product pro3=proBll.GetById(proId)[0];
                                DingDan dd = new DingDan();                                
                                dd.HuiYuanID = pro3.HuiYuanID;
                                dd.OrderTypeID = orderBll.GetbyName("竞拍订单").OrderTypeID;
                                dd.ProductID = proId;
                                dd.ProductPrice = Convert.ToDecimal(pro3.PmJGproduct);
                                dd.ShipFee = pro3.ShipFee;
                                dd.Fee = pro3.Fee;
                                dd.TotalPrice = dd.ProductPrice + dd.ShipFee + dd.Fee;
                                dd.Status = 10;
                                dd.InvalidTime = DateTime.Now.AddDays(7);
                                dd.DingDanBH = DateTime.Now.ToString().GetHashCode().ToString();
                                orderBll.AddOrder(dd);
                                ts1.Complete();
                            }
                            lblTime.Text = "已成交";
                        }
                    }
                }
            }
        }
    }
}