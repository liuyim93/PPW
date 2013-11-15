﻿using System;
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
        AuctionBll auctionBll = new AuctionBll();
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
                Response.Redirect("../Auction/Index.aspx");
            }
            else
            {
                string auctionId = Request.QueryString["id"].ToString();
                List<auction> list = auctionBll.GetAuction(auctionId);
                if (list.Count <= 0)
                {
                    MessageBox.AlertAndRedirect("该商品不存在！", "../Auction/Index.aspx", Page);
                }
                else
                {
                    string proId=list[0].ProductID;
                    Product pro = proBll.GetById(proId)[0];
                    List<ProductImeg> list_img = proBll.GetProtductImeg("", proId);
                    if (list_img.Count > 0)
                    {
                        imgBig.ImageUrl = list_img[0].img;
                    }
                    //会员名
                    if (list[0].HuiYuanID == "")
                    {
                        lblMemberName.Text = "";
                    }
                    else
                    {
                        HuiYuan hy = hyBll.GetHuiYuan(list[0].HuiYuanID);
                        if (hy != null)
                        {
                            lblMemberName.Text = hy.HuiYuanName;
                        }
                    }
                    //是否已成交
                    if (list[0].Status != 3)
                    {
                        pnlAuction.Visible = true;
                        pnlEnd.Visible = false;
                        if (list[0].AuctionTime > DateTime.Now.AddSeconds(10))
                        {
                            TimeSpan time = Convert.ToDateTime(list[0].AuctionTime) - DateTime.Now;
                            lblTime.Text = time.Hours.ToString().PadLeft(2, '0') + time.Minutes.ToString().PadLeft(2, '0') + time.Seconds.ToString().PadLeft(2, '0') + time.Milliseconds.ToString();
                        }
                        else
                        {
                            lblTime.Text = "00:00:" + list[0].TimePoint.ToString().PadLeft(2, '0');
                        }
                    }
                    else
                    {
                        pnlEnd.Visible = true;
                        pnlAuction.Visible = false;
                        lblStart.Text = list[0].AuctionTime.ToString();
                        lblEnd.Text = list[0].EndTime.ToString();
                    }
                    //判断竞拍类型
                    string free = proBll.GetAuctionTypebyName("免费竞拍")[0].AuctionTypeID;
                    if (list[0].AuctionTypeID == free)
                    {
                        lblPoint.Text = list[0].FreePoint.ToString();
                    }
                    else
                    {
                        lblPoint.Text = list[0].AuctionPoint.ToString();
                    }
                    lblPriceAdd.Text = list[0].PriceAdd.ToString();
                    lblProName.Text = pro.productName;
                    lblIntro.Text = pro.Intro + " 第（" + list[0].Coding + ")期";
                    lblPrice.Text = "￥ " + pro.productPrice.ToString();
                    lblFee.Text = "￥ " + pro.Fee.ToString();
                    lblShipFee.Text = "￥ " + pro.ShipFee.ToString();
                    lblAuctionPrice.Text = "￥ " + list[0].AuctionPrice.ToString();
                    lblDetail.Text = pro.ProductDetails;
                }
            }
        }

        //出价记录
        public void BindgvwHistory() 
        {
            string auctionId=Request.QueryString["id"].ToString();
            gvwHistory.DataSource = recordBll.GetChuJiaJiLubyauctionId_Top10(auctionId);
            gvwHistory.DataBind();
        }
        //我的竞拍
        public void BindMyAuction() 
        {
            string auctionId=Request.QueryString["id"].ToString();
            List<auction> list = auctionBll.GetAuction(auctionId);
            if (list.Count>0)
            {
                List<Product> list_pro = proBll.GetById(list[0].ProductID);
                ltlProPrice.Text = list_pro[0].productPrice.ToString();
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
                List<ChuJiaJiLu> list1 = recordBll.GetChuJiaJiLu(auctionId,memberId);
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
            string auctionId = Request.QueryString["id"];
            if (Session["HuiYuanID"] == null || Session["HuiYuanName"] == null)
            {
                MessageBox.AlertAndRedirect("请先登录", "../Auction/UserLogin.aspx", Page);
            }
            else
            {
                string hyId = Session["HuiYuanID"].ToString();
                using (TransactionScope ts = new TransactionScope())
                {
                    //修改竞拍表
                    auction act = new auction();
                    act.AuctionID = auctionId;
                    act.HuiYuanID = hyId;
                    act.TimePoint = 10;
                    auctionBll.UpdateAuctionPrice(act);

                    //修改出价记录
                    auction act1=auctionBll.GetAuction(auctionId)[0];
                    ChuJiaJiLu record = new ChuJiaJiLu();
                    record.AuctionID = auctionId;
                    record.HuiYuanID = hyId;
                    record.Price = Convert.ToDecimal(act1.AuctionPrice);
                    record.IPAdress = Request.UserHostAddress;
                    record.Status = 1;
                    record.AuctionTime = DateTime.Now;
                    record.AuctionPoint = act1.AuctionPoint;
                    record.FreePoint = act1.FreePoint;
                    recordBll.AddChuJiaJiLu(record);

                    //扣除会员相应的拍点或返点
                    HuiYuan hy = new HuiYuan();
                    hy.HuiYuanID = hyId;
                    hy.PaiDian = act1.AuctionPoint * -1;
                    hy.FreePoint = act1.FreePoint * -1;
                    hyBll.UpdateHuiYuanPoint(hy);
                    lblAuctionPrice.Text = act1.AuctionPrice.ToString();
                    lblMemberName.Text = hyBll.GetHuiYuan(act1.HuiYuanID).HuiYuanName;
                    ts.Complete();
                }
                BindgvwHistory();
                BindMyAuction();
            }
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            string auctionId = Request.QueryString["id"];
            List<auction> list = auctionBll.GetAuction(auctionId);
            if (list.Count > 0)
            {
                auction act=list[0];
                DateTime auctiontime = Convert.ToDateTime(act.AuctionTime);
                if (auctiontime > DateTime.Now.AddSeconds(10) && act.Status != 3)
                {
                    TimeSpan ts = auctiontime - DateTime.Now;
                    lblTime.Text = ts.Hours.ToString().PadLeft(2, '0')+":"+ ts.Minutes.ToString().PadLeft(2, '0')+":" + ts.Seconds.ToString().PadLeft(2, '0');
                }
                else
                {
                    if (act.Status != 3 && act.TimePoint > 0)
                    {
                        auctionBll.UpdateTimePoint(auctionId);
                        auction act1=auctionBll.GetAuction(auctionId)[0];
                        if (act1.TimePoint != 0)
                        {
                            lblTime.Text = "00:00:" + act1.TimePoint.ToString().PadLeft(2, '0');
                        }
                        else
                        {
                            lblTime.Text = "即将成交";
                            using (TransactionScope ts1 = new TransactionScope())
                            {
                                //修改商品状态
                                auction act2 = new auction();
                                act2.AuctionID = auctionId;
                                act2.Status = 3;
                                act2.EndTime = DateTime.Now;
                                auctionBll.UpdateStatus(act2);

                                //生成竞拍订单
                                auction act3=auctionBll.GetAuction(auctionId)[0];
                                Product pro=proBll.GetById(act3.ProductID)[0];
                                DingDan dd = new DingDan();
                                dd.HuiYuanID = act3.HuiYuanID;
                                dd.OrderTypeID = orderBll.GetbyName("竞拍订单")[0].OrderTypeID;
                                dd.ProductID = act3.ProductID;
                                dd.ProductPrice = Convert.ToDecimal(act3.AuctionPrice);
                                dd.ShipFee = pro.ShipFee;
                                dd.Fee = pro.Fee;
                                dd.TotalPrice = dd.ProductPrice + dd.ShipFee + dd.Fee;
                                dd.Status = 10;
                                dd.InvalidTime = DateTime.Now.AddDays(7);
                                dd.DingDanBH = DateTime.Now.ToString().GetHashCode().ToString();
                                dd.ShouHuoDZID = "";
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