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

namespace WEB.Auction
{
    public partial class AuctionList : System.Web.UI.Page
    {
        ProductBLL proBll = new ProductBLL();
        HuiYuanXinXiBll hyBll = new HuiYuanXinXiBll();
        ChuJiaJiLuBll cjBll = new ChuJiaJiLuBll();
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
            //正在热拍
            dlstAuction.DataSource = auctionBll.GetAllAuctioning();
            dlstAuction.DataBind();            
        }

        protected void dlstAuction_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType==ListItemType.Item||e.Item.ItemType==ListItemType.AlternatingItem)
            {
                Image imgProduct = e.Item.FindControl("imgProduct") as Image;
                Label lblMemberName = e.Item.FindControl("lblMemberName") as Label;
                ImageButton imgbtnAuction = e.Item.FindControl("imgbtnAuction") as ImageButton;
                Label lblTime = e.Item.FindControl("lblTime") as Label;
                HyperLink hlnkPro = e.Item.FindControl("hlnkPro") as HyperLink;
                Label lblIntro = e.Item.FindControl("lblIntro") as Label;
                Label lblPrice = e.Item.FindControl("lblPrice") as Label;

                HiddenField hfProductNo = e.Item.FindControl("hfProductNo") as HiddenField;
                HiddenField hfProductID = e.Item.FindControl("hfProductID") as HiddenField;
                HiddenField hfMemberID = e.Item.FindControl("hfMemberID") as HiddenField;
                HiddenField hfAuctionPoint = e.Item.FindControl("hfAuctionPoint") as HiddenField;
                HiddenField hfAuctionTime = e.Item.FindControl("hfAuctionTime") as HiddenField;
                HiddenField hfTimePoint = e.Item.FindControl("hfTimePoint") as HiddenField;
                HiddenField hfStatus=e.Item.FindControl("hfStatus")as HiddenField;

                string auctionId=dlstAuction.DataKeys[e.Item.ItemIndex].ToString();
                List<Product> list_pro = proBll.GetById(hfProductID.Value);
                string proName=list_pro[0].productName;
                imgProduct.ToolTip = "第" + hfProductNo.Value + "期 " +list_pro[0].productName;
                hlnkPro.Text = proName;
                hlnkPro.ToolTip = imgProduct.ToolTip;
                hlnkPro.NavigateUrl = "../Auction/ProDetail.aspx?id="+auctionId;
                lblPrice.Text=list_pro[0].productPrice.ToString();
                lblIntro.Text=list_pro[0].Intro;

                List<ProductImeg>list=proBll.GetProtductImeg("",hfProductID.Value);
                if (list.Count>0)
                {
                    imgProduct.ImageUrl=list[0].img;
                }else
                {
                    imgProduct.ImageUrl="";
                }
                lblMemberName.Text = hyBll.GetHuiYuan(hfMemberID.Value).HuiYuanName;
                imgbtnAuction.ToolTip = "每次出价消耗"+hfAuctionPoint.Value+"拍点";
                if (hfAuctionTime.Value!=""&&hfTimePoint.Value!="")
                {
                    if (Convert.ToDateTime(hfAuctionTime.Value) > DateTime.Now.AddSeconds(10) && hfStatus.Value != "3")
                    {
                        TimeSpan ts = Convert.ToDateTime(hfAuctionTime.Value) - DateTime.Now;
                        lblTime.Text = ts.Hours.ToString().PadLeft(2, '0')+":" + ts.Minutes.ToString().PadLeft(2, '0')+":"+ ts.Seconds.ToString().PadLeft(2, '0');
                    }
                    else 
                    {
                        if (hfStatus.Value!="3")
                        {
                            lblTime.Text = "00:00:" + hfTimePoint.Value.PadLeft(2,'0');
                        }
                    }
                }
            }
        }

        protected void dlstAuction_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName=="auction")
            {
                string auctionId = e.CommandArgument.ToString();
                if (Session["HuiYuanID"] == null || Session["HuiYuanName"] == null)
                {
                    MessageBox.AlertAndRedirect("请先登录", "../Auction/UserLogin.aspx", Page);
                }
                else 
                {
                    string hyId=Session["HuiYuanID"].ToString();
                    using (TransactionScope ts=new TransactionScope())
                    {
                         //修改产品表
                        HiddenField hfStatus=e.Item.FindControl("hfStatus")as HiddenField;
                        auction act = new auction();
                        if (hfStatus.Value!="3")
                        {
                            act.TimePoint = 10;
                        }
                        act.HuiYuanID=Session["HuiYuanID"].ToString();
                        act.AuctionID = auctionId;
                        auctionBll.UpdateAuctionPrice(act);
                        //查询出价后的拍品信息 
                        auction act1 = auctionBll.GetAuction(auctionId)[0];
                        //添加出价记录
                        ChuJiaJiLu cj = new ChuJiaJiLu();
                        cj.AuctionTime = DateTime.Now;
                        cj.AuctionID = auctionId;
                        cj.Status=1;
                        cj.Price = Convert.ToDecimal(act1.AuctionPrice);
                        cj.IPAdress=Request.UserHostAddress;
                        cj.HuiYuanID = Session["HuiYuanID"].ToString();
                        cj.AuctionPoint = act1.AuctionPoint;
                        cj.FreePoint = Convert.ToInt32(act1.FreePoint);
                        cjBll.AddChuJiaJiLu(cj);
                        //扣除会员相应的拍点
                        HuiYuan hy = new HuiYuan();
                        hy.HuiYuanID=Session["HuiYuanID"].ToString();
                        hy.PaiDian = act1.AuctionPoint * -1;
                        hy.FreePoint = act1.FreePoint * -1;
                        hyBll.UpdateHuiYuanPoint(hy);
                        ts.Complete();
                    }
                    Bind();
                }
            }
        }

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