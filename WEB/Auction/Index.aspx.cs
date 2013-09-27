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
                        using (TransactionScope ts = new TransactionScope())
                        {
                            string productId = e.CommandArgument.ToString();
                            //修改产品表
                            Product pro = new Product();
                            HiddenField hfAuctionTime = e.Item.FindControl("hfAuctionTime") as HiddenField;
                            if (hfAuctionTime != null && Convert.ToDateTime(hfAuctionTime.Value)<=DateTime.Now.AddSeconds(10))
                            {
                                pro.TimePoint = 10;
                            }
                            else 
                            {
                                pro.TimePoint = -1;
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
                            hy.PaiDian = pro2.AuctionPoint;
                            hy.FreePoint = pro2.FreePoint;
                            hyBll.UpdateHuiYuanPoint(hy);
                            ts.Complete();
                        }
                        Bind();
                    }
                    catch (Exception)
                    {                        
                        throw;
                    }                   
                    break;
                    //成交                
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
                if (hfTimePoint!=null&&hfTimePoint.Value!="-1"&&hfProductID!=null&&Convert.ToDateTime(hfAuctionTime.Value)<=DateTime.Now.AddSeconds(10)&&hfStaus.Value!="3")
                {
                    productBll.UpdateTimePoint(hfProductID.Value);
                    Product pro = productBll.GetById(hfProductID.Value)[0];
                    lblTimer.Text = "00:00:0" + pro.TimePoint;
                    if (pro.TimePoint == 0)
                    {
                        Product pro2 = new Product();
                        pro2.ProductID = hfProductID.Value;
                        pro2.Status = 3;
                        productBll.UpdateProductStatus(pro2);
                        lblTimer.Text = "已成交";
                        MessageBox.Alert("商品已成交",Page);
                    }
                }                               
            }
            Bind();
        }
    }
}