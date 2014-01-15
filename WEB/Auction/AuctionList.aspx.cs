using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL.SystemSeting;
using BLL;
using Model.Entities;
using System.Transactions;
using Tools;
using System.Data;

namespace WEB.Auction
{
    public partial class AuctionList : System.Web.UI.Page
    {
        ProductBLL proBll = new ProductBLL();
        HuiYuanXinXiBll hyBll = new HuiYuanXinXiBll();
        ChuJiaJiLuBll cjBll = new ChuJiaJiLuBll();
        AuctionBll auctionBll = new AuctionBll();
        DingDanBll orderBll = new DingDanBll();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bind();
            }
        }

        public void Bind() 
        {
            //正在热拍
            DataTable dt = auctionBll.getNormalAuctioning();
            if(dt.Rows.Count>0){
                AspNetPager1.RecordCount = dt.Rows.Count;
                PagedDataSource pds = new PagedDataSource();
                pds.DataSource = dt.DefaultView;
                pds.PageSize = AspNetPager1.PageSize;
                pds.AllowPaging = true;
                pds.CurrentPageIndex = AspNetPager1.CurrentPageIndex - 1;
                repeater1.DataSource = pds;
                repeater1.DataBind();
            }            
        }

        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            Bind();
        }

        //protected void dlstAuction_ItemDataBound(object sender, DataListItemEventArgs e)
        //{
        //    if (e.Item.ItemType==ListItemType.Item||e.Item.ItemType==ListItemType.AlternatingItem)
        //    {
        //        Image imgProduct = e.Item.FindControl("imgProduct") as Image;
        //        Label lblMemberName = e.Item.FindControl("lblMemberName") as Label;
        //        ImageButton imgbtnAuction = e.Item.FindControl("imgbtnAuction") as ImageButton;
        //        Label lblTime = e.Item.FindControl("lblTime") as Label;
        //        HyperLink hlnkPro = e.Item.FindControl("hlnkPro") as HyperLink;
        //        Label lblIntro = e.Item.FindControl("lblIntro") as Label;
        //        Label lblPrice = e.Item.FindControl("lblPrice") as Label;

        //        HiddenField hfProductNo = e.Item.FindControl("hfProductNo") as HiddenField;
        //        HiddenField hfProductID = e.Item.FindControl("hfProductID") as HiddenField;
        //        HiddenField hfMemberID = e.Item.FindControl("hfMemberID") as HiddenField;
        //        HiddenField hfAuctionPoint = e.Item.FindControl("hfAuctionPoint") as HiddenField;
        //        HiddenField hfAuctionTime = e.Item.FindControl("hfAuctionTime") as HiddenField;
        //        HiddenField hfTimePoint = e.Item.FindControl("hfTimePoint") as HiddenField;
        //        HiddenField hfStatus=e.Item.FindControl("hfStatus")as HiddenField;

        //        string auctionId=dlstAuction.DataKeys[e.Item.ItemIndex].ToString();
        //        List<Product> list_pro = proBll.GetById(hfProductID.Value);
        //        string proName=list_pro[0].productName;
        //        imgProduct.ToolTip = "第" + hfProductNo.Value + "期 " +list_pro[0].productName;
        //        hlnkPro.Text = proName;
        //        hlnkPro.ToolTip = imgProduct.ToolTip;
        //        hlnkPro.NavigateUrl = "../Auction/ProDetail.aspx?id="+auctionId;
        //        lblPrice.Text=list_pro[0].productPrice.ToString();
        //        lblIntro.Text=list_pro[0].Intro;

        //        List<ProductImeg>list=proBll.GetProtductImeg("",hfProductID.Value);
        //        if (list.Count>0)
        //        {
        //            imgProduct.ImageUrl=list[0].img;
        //        }else
        //        {
        //            imgProduct.ImageUrl="";
        //        }
        //        if (hfMemberID.Value == "")
        //        {
        //            lblMemberName.Text = "";
        //        }
        //        else 
        //        {
        //            lblMemberName.Text = hyBll.GetHuiYuan(hfMemberID.Value).HuiYuanName;
        //        }               
        //        imgbtnAuction.ToolTip = "每次出价消耗"+hfAuctionPoint.Value+"拍点";
        //        if (hfAuctionTime.Value!=""&&hfTimePoint.Value!="")
        //        {
        //            if (Convert.ToDateTime(hfAuctionTime.Value) > DateTime.Now.AddSeconds(10) && hfStatus.Value != "3")
        //            {
        //                TimeSpan ts = Convert.ToDateTime(hfAuctionTime.Value) - DateTime.Now;
        //                lblTime.Text = ts.Hours.ToString().PadLeft(2, '0')+":" + ts.Minutes.ToString().PadLeft(2, '0')+":"+ ts.Seconds.ToString().PadLeft(2, '0');
        //            }
        //            else 
        //            {
        //                if (hfStatus.Value!="3")
        //                {
        //                    lblTime.Text = "00:00:" + hfTimePoint.Value.PadLeft(2,'0');
        //                }
        //            }
        //        }
        //    }
        //}

        //protected void dlstAuction_ItemCommand(object source, DataListCommandEventArgs e)
        //{
        //    if (e.CommandName=="auction")
        //    {
        //        string auctionId = e.CommandArgument.ToString();
        //        if (Session["HuiYuanID"] == null || Session["HuiYuanName"] == null)
        //        {
        //            MessageBox.AlertAndRedirect("请先登录", "../Auction/UserLogin.aspx", Page);
        //        }
        //        else 
        //        {
        //            string hyId=Session["HuiYuanID"].ToString();
        //            using (TransactionScope ts=new TransactionScope())
        //            {
        //                 //修改产品表
        //                HiddenField hfStatus=e.Item.FindControl("hfStatus")as HiddenField;
        //                auction act = new auction();
        //                if (hfStatus.Value!="3")
        //                {
        //                    act.TimePoint = 10;
        //                }
        //                act.HuiYuanID=Session["HuiYuanID"].ToString();
        //                act.AuctionID = auctionId;
        //                auctionBll.UpdateAuctionPrice(act);
        //                //查询出价后的拍品信息 
        //                auction act1 = auctionBll.GetAuction(auctionId)[0];
        //                //添加出价记录
        //                ChuJiaJiLu cj = new ChuJiaJiLu();
        //                cj.AuctionTime = DateTime.Now;
        //                cj.AuctionID = auctionId;
        //                cj.Status=1;
        //                cj.Price = Convert.ToDecimal(act1.AuctionPrice);
        //                cj.IPAdress=Request.UserHostAddress;
        //                cj.HuiYuanID = Session["HuiYuanID"].ToString();
        //                cj.AuctionPoint = act1.AuctionPoint;
        //                cj.FreePoint = Convert.ToInt32(act1.FreePoint);
        //                cjBll.AddChuJiaJiLu(cj);
        //                //扣除会员相应的拍点
        //                HuiYuan hy = new HuiYuan();
        //                hy.HuiYuanID=Session["HuiYuanID"].ToString();
        //                hy.PaiDian = act1.AuctionPoint * -1;
        //                hy.FreePoint = act1.FreePoint * -1;
        //                hyBll.UpdateHuiYuanPoint(hy);
        //                ts.Complete();
        //            }
        //            Bind();
        //        }
        //    }
        //}

        //protected void timer1_Tick(object sender, EventArgs e)
        //{
        //    for (int i = 0; i < dlstAuction.Items.Count; i++)
        //    {
        //        HiddenField hfProductID = dlstAuction.Items[i].FindControl("hfProductID") as HiddenField;
        //        HiddenField hfTimePoint = dlstAuction.Items[i].FindControl("hfTimePoint") as HiddenField;
        //        HiddenField hfAuctionTime = dlstAuction.Items[i].FindControl("hfAuctionTime") as HiddenField;
        //        Label lblTime = dlstAuction.Items[i].FindControl("lblTime") as Label;
        //        HiddenField hfStaus = dlstAuction.Items[i].FindControl("hfStatus") as HiddenField;
        //        HiddenField hfMemberID = dlstAuction.Items[i].FindControl("hfMemberID") as HiddenField;
        //        Label lblAuctionPrice = dlstAuction.Items[i].FindControl("lblAuctionPrice") as Label;
        //        List<Product> list_pro = proBll.GetById(hfProductID.Value);
        //        decimal fee = list_pro[0].Fee;
        //        decimal shipFee = list_pro[0].ShipFee;
        //        string auctionId = dlstAuction.DataKeys[i].ToString();
        //        if (hfAuctionTime.Value != "" && Convert.ToDateTime(hfAuctionTime.Value) > DateTime.Now.AddSeconds(10))
        //        {
        //            TimeSpan ts = Convert.ToDateTime(hfAuctionTime.Value) - DateTime.Now;
        //            lblTime.Text = ts.Hours.ToString().PadLeft(2, '0') + ":" + ts.Minutes.ToString().PadLeft(2, '0') + ":" + ts.Seconds.ToString().PadLeft(2, '0');//商品未开始竞拍的倒计时
        //        }
        //        if (Convert.ToDateTime(hfAuctionTime.Value) <= DateTime.Now.AddSeconds(10) && hfProductID != null)
        //        {

        //            auction auction = auctionBll.GetAuction(auctionId)[0];
        //            if (auction.TimePoint > 0 && auction.Status != 3)
        //            {
        //                auctionBll.UpdateTimePoint(auctionId);
        //                Product pro1 = proBll.GetById(hfProductID.Value)[0];
        //                auction auction1 = auctionBll.GetAuction(auctionId)[0];
        //                lblTime.Text = "00:00:" + auction1.TimePoint.ToString().PadLeft(2, '0');
        //                if (auction1.TimePoint == 0)
        //                {
        //                    try
        //                    {
        //                        string typeName = "竞拍订单";
        //                        using (TransactionScope ts1 = new TransactionScope())
        //                        {
        //                            //生成竞拍订单
        //                            DingDan dd = new DingDan();
        //                            dd.DingDanBH = DateTime.Now.ToString("yyyyMMddHHmmss");
        //                            dd.HuiYuanID = hfMemberID.Value;
        //                            dd.ProductID = hfProductID.Value;
        //                            dd.ProductPrice = Convert.ToDecimal(lblAuctionPrice.Text);
        //                            dd.Status = 10;
        //                            dd.OrderTypeID = orderBll.GetbyName(typeName)[0].OrderTypeID;
        //                            dd.Fee = fee;
        //                            dd.ShipFee = shipFee;
        //                            dd.TotalPrice = dd.Fee + dd.ShipFee + dd.ProductPrice;
        //                            dd.InvalidTime = DateTime.Now.AddDays(7);
        //                            dd.AuctionID = auctionId;
        //                            dd.ShouHuoDZID = "";
        //                            orderBll.AddOrder(dd);

        //                            //修改拍品状态为已成交
        //                            auction auction2 = new auction();
        //                            auction2.Status = 3;
        //                            auction2.EndTime = DateTime.Now;
        //                            auction2.AuctionID = auctionId;
        //                            auctionBll.UpdateStatus(auction2);
        //                            ts1.Complete();
        //                            lblTime.Text = "已成交";
        //                        }
        //                    }
        //                    catch (Exception)
        //                    {

        //                        throw;
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}

        //protected void dlstRecommend_ItemDataBound(object sender, DataListItemEventArgs e)
        //{
        //    if (e.Item.ItemType==ListItemType.Item||e.Item.ItemType==ListItemType.AlternatingItem)
        //    {
        //        HyperLink hlnkProName = e.Item.FindControl("hlnkProName") as HyperLink;
        //        Image imgProduct = e.Item.FindControl("imgProduct") as Image;
        //        Label lblAuctionTime = e.Item.FindControl("lblAuctionTime") as Label;
        //        HiddenField hfProductID = e.Item.FindControl("hfProductID") as HiddenField;
        //        HiddenField hfProductNo = e.Item.FindControl("hfProductNo") as HiddenField;
        //        hlnkProName.ToolTip = "第" + hfProductNo.Value + "期 " + hlnkProName.Text;
        //        imgProduct.ToolTip = hlnkProName.ToolTip;
        //        List<ProductImeg> list = proBll.GetProtductImeg("",hfProductID.Value);
        //        if (list.Count > 0)
        //        {
        //            imgProduct.ImageUrl = list[0].img;
        //        }
        //        else 
        //        {
        //            imgProduct.ImageUrl = "";
        //        }
        //        lblAuctionTime.Text = Convert.ToDateTime(lblAuctionTime.Text).Hour.ToString().PadLeft(2, '0') +":"+ Convert.ToDateTime(lblAuctionTime.Text).Minute.ToString().PadLeft(2, '0');
        //    }
        //}

        //protected void dlstHistory_ItemDataBound(object sender, DataListItemEventArgs e)
        //{
        //    if (e.Item.ItemType==ListItemType.Item||e.Item.ItemType==ListItemType.AlternatingItem)
        //    {
        //        HyperLink hlnkProName = e.Item.FindControl("hlnkProName") as HyperLink;
        //        Image imgProduct = e.Item.FindControl("imgProduct") as Image;
        //        HiddenField hfProductID = e.Item.FindControl("hfProductID") as HiddenField;
        //        HiddenField hfProductNo = e.Item.FindControl("hfProductNo") as HiddenField;
        //        Label lblMemberName = e.Item.FindControl("lblMemberName") as Label;
        //        hlnkProName.ToolTip = "第" + hfProductNo.Value + "期" + hlnkProName.Text;
        //        imgProduct.ToolTip = hlnkProName.ToolTip;
        //        List<ProductImeg> list = proBll.GetProtductImeg("", hfProductID.Value);
        //        if (list.Count > 0)
        //        {
        //            imgProduct.ImageUrl = list[0].img;
        //        }
        //        else
        //        {
        //            imgProduct.ImageUrl = "";
        //        }
        //        lblMemberName.Text = hyBll.GetHuiYuan(lblMemberName.Text).HuiYuanName;
        //    }
        //}
    }
}