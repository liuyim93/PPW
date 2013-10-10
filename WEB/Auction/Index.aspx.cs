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
using Tools;

namespace WEB.Auction
{
    public partial class Index : System.Web.UI.Page
    {
        GonGaoBLL ggBll = new GonGaoBLL();
        ProductBLL productBll = new ProductBLL();
        HuiYuanXinXiBll hyBll = new HuiYuanXinXiBll();
        ChuJiaJiLuBll cjBll = new ChuJiaJiLuBll();
        DingDanBll orderBll = new DingDanBll();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["HuiYuanName"]==null||Session["HuiYuanID"]==null)
                {
                    Response.Redirect("UserLogin.aspx");
                }
                Bind();
            }            
       }

        public void Bind() 
        {
            //绑定新闻公告
            dlstNews.DataSource = ggBll.GetGgTop5();
            dlstNews.DataBind();
            //正在热拍
            dlstProduct.DataSource = productBll.GetAuctioningProduct();
            dlstProduct.DataBind();
            //最新成交五条信息
            dlstDone.DataSource = productBll.GetDoneProduct_Top5();
            dlstDone.DataBind();
        }

        //查询最新的25条竞拍信息，包括已成交的拍品
        public void BindProduct() 
        {
            dlstProduct.DataSource = productBll.GetProduct_Top25();
            dlstProduct.DataBind();
        }

        /// <summary>
        /// 正在热拍
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void dlstProduct_ItemCommand(object source, DataListCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                    //出价
                case "auction":
                    try
                    {
                        string productId = e.CommandArgument.ToString();
                        using (TransactionScope ts = new TransactionScope())
                        {                            
                            //修改产品表
                            Product pro = new Product();
                            HiddenField hfAuctionTime = e.Item.FindControl("hfAuctionTime") as HiddenField;
                            Label lblTimer = e.Item.FindControl("lblTimer") as Label;
                            HiddenField hfStatus=e.Item.FindControl("hfStatus")as HiddenField;
                            if (hfStatus.Value!="3")
                            {
                                pro.TimePoint = 10;
                            }                           
                            pro.HuiYuanID=Session["HuiYuanID"].ToString();
                            pro.ProductID = productId;
                            productBll.UpdateProductPrice(pro);
                            //查询出价后的产品信息
                            Product pro2 = productBll.GetById(productId)[0];
                            //添加出价记录
                            ChuJiaJiLu cj = new ChuJiaJiLu();
                            cj.AuctionTime = DateTime.Now;
                            cj.ProductID = productId;
                            cj.Status=1;
                            cj.Price = Convert.ToDecimal(pro2.PmJGproduct);
                            cj.IPAdress=Request.UserHostAddress;
                            cj.HuiYuanID = Session["HuiYuanID"].ToString();
                            cj.AuctionPoint = pro2.AuctionPoint;
                            cj.FreePoint = Convert.ToInt32(pro2.FreePoint);
                            cjBll.AddChuJiaJiLu(cj);
                            //扣除会员相应的拍点
                            HuiYuan hy = new HuiYuan();
                            hy.HuiYuanID=Session["HuiYuanID"].ToString();
                            hy.PaiDian = pro2.AuctionPoint*-1;
                            hy.FreePoint = pro2.FreePoint*-1;
                            hyBll.UpdateHuiYuanPoint(hy);
                            ts.Complete();
                        }
                        BindProduct();
                    }
                    catch (Exception)
                    {                        
                        throw;
                    }                   
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
                //显示出价人
                HiddenField hfMemberID = e.Item.FindControl("hfMemberID") as HiddenField;
                Label lblMemberName = e.Item.FindControl("lblMemberName") as Label;
                if (hfMemberID != null && hfMemberID.Value != "")
                {
                    lblMemberName.Text = hyBll.GetHuiYuan(hfMemberID.Value).HuiYuanName;
                }
                else 
                {
                    lblMemberName.Text = "";
                }
                //显示倒计时
                HiddenField hfAuctionTime = e.Item.FindControl("hfAuctionTime") as HiddenField;
                Label lblTimer = e.Item.FindControl("lblTimer") as Label;
                HiddenField hfTimePoint = e.Item.FindControl("hfTimePoint") as HiddenField;
                HiddenField hfStatus=e.Item.FindControl("hfStatus")as HiddenField;
                if (hfAuctionTime.Value != "" && Convert.ToDateTime(hfAuctionTime.Value) > DateTime.Now.AddSeconds(10))
                {
                    TimeSpan ts = Convert.ToDateTime(hfAuctionTime.Value) - DateTime.Now;
                    lblTimer.Text = ts.Hours.ToString().PadLeft(2, '0') + ":" + ts.Minutes.ToString().PadLeft(2, '0') + ":" + ts.Seconds.ToString().PadLeft(2, '0');//商品未开始竞拍的倒计时
                }
                else 
                {
                    if (Convert.ToInt32(hfStatus.Value) == 3)
                    {
                        lblTimer.Text = "已成交";
                    }
                    else 
                    {
                        if (hfTimePoint.Value!="-1")
                        {
                            lblTimer.Text = "00:00:" + hfTimePoint.Value.PadLeft(2, '0');
                        }                        
                    }                    
                }
            }
        }

        protected void timer1_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < dlstProduct.Items.Count; i++)
            {
                HiddenField hfProductID = dlstProduct.Items[i].FindControl("hfProductID") as HiddenField;
                HiddenField hfTimePoint = dlstProduct.Items[i].FindControl("hfTimePoint") as HiddenField;
                HiddenField hfAuctionTime=dlstProduct.Items[i].FindControl("hfAuctionTime")as HiddenField;
                Label lblTimer=dlstProduct.Items[i].FindControl("lblTimer")as Label;
                HiddenField hfStaus=dlstProduct.Items[i].FindControl("hfStatus")as HiddenField;
                HiddenField hfMemberID = dlstProduct.Items[i].FindControl("hfMemberID") as HiddenField;
                Label lblAuctionPrice=dlstProduct.Items[i].FindControl("lblAuctionPrice")as Label;
                //运费、手续费
                HiddenField hfFee=dlstProduct.Items[i].FindControl("hfFee")as HiddenField;
                HiddenField hfShipFee=dlstProduct.Items[i].FindControl("hfShipFee")as HiddenField;
                if (hfAuctionTime.Value!= "" && Convert.ToDateTime(hfAuctionTime.Value) > DateTime.Now.AddSeconds(10))
                {
                    TimeSpan ts = Convert.ToDateTime(hfAuctionTime.Value) - DateTime.Now;
                    lblTimer.Text = ts.Hours.ToString().PadLeft(2, '0') + ":" + ts.Minutes.ToString().PadLeft(2, '0') + ":" + ts.Seconds.ToString().PadLeft(2, '0');//商品未开始竞拍的倒计时
                }
                if (Convert.ToDateTime(hfAuctionTime.Value)<=DateTime.Now.AddSeconds(10)&&hfProductID!=null)
                {
                    Product pro=productBll.GetById(hfProductID.Value)[0];
                    if (pro.TimePoint>0&&pro.Status!=3)
                    {
                        productBll.UpdateTimePoint(hfProductID.Value);
                        Product pro1 = productBll.GetById(hfProductID.Value)[0];
                        lblTimer.Text = "00:00:" + pro1.TimePoint.ToString().PadLeft(2,'0');
                        if (pro1.TimePoint == 0)
                        {                            
                            try
                            {
                                string typeName = "竞拍订单";
                                using (TransactionScope ts1 = new TransactionScope())
                                {
                                    //生成竞拍订单
                                    DingDan dd = new DingDan();
                                    dd.DingDanBH = DateTime.Now.ToString().GetHashCode().ToString();
                                    dd.HuiYuanID = hfMemberID.Value;
                                    dd.ProductID = hfProductID.Value;
                                    dd.ProductPrice = Convert.ToDecimal(lblAuctionPrice.Text);
                                    dd.Status = 10;                                    
                                    dd.OrderTypeID = orderBll.GetbyName(typeName).OrderTypeID;
                                    dd.Fee = Convert.ToDecimal(hfFee.Value);
                                    dd.ShipFee = Convert.ToDecimal(hfShipFee.Value);
                                    dd.TotalPrice = dd.Fee + dd.ShipFee + dd.ProductPrice;
                                    dd.InvalidTime = DateTime.Now.AddDays(7);
                                    orderBll.AddOrder(dd);
                                    //修改拍品状态为已成交
                                    Product pro2 = new Product();
                                    pro2.ProductID = hfProductID.Value;
                                    pro2.Status = 3;
                                    productBll.UpdateProductStatus(pro2);                                    
                                    ts1.Complete();
                                    lblTimer.Text = "已成交";
                                }
                            }
                            catch (Exception)
                            {
                                
                                throw;
                            }
                        }
                    }                    
                }                               
            }
        }

        //最近成交
        protected void dlstDone_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType==ListItemType.Item||e.Item.ItemType==ListItemType.AlternatingItem)
            {
                HiddenField hfProductID = e.Item.FindControl("hfProductID") as HiddenField;
                HiddenField hfMemberID = e.Item.FindControl("hfMemberID") as HiddenField;
                Image imgProduct = e.Item.FindControl("imgProduct") as Image;
                HiddenField hfProductName=e.Item.FindControl("hfProductName")as HiddenField;
                HiddenField hfProductNo=e.Item.FindControl("hfProductNo")as HiddenField;
                //拍品提示信息、图片地址
                if (hfProductID!=null&&hfProductID.Value!="")                    
                {
                    List<ProductImeg> list = productBll.GetProtductImeg("", hfProductID.Value);
                    if (list.Count>0)
                    {
                        imgProduct.ImageUrl = list[0].img;
                        imgProduct.ToolTip = hfProductName.Value + " 第" + hfProductNo.Value + "号拍品";
                    }                   
                }
                Label lblMemberName = e.Item.FindControl("lblMemberName") as Label;
                //获得者
                if (hfMemberID.Value!=""&&hfMemberID!=null)
                {
                    lblMemberName.Text = hyBll.GetHuiYuan(hfMemberID.Value).HuiYuanName;
                }
            }
        }
    }
}