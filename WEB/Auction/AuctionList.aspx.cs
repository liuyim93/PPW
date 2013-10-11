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
            dlstAuction.DataSource = proBll.GetAuctioningProduct();
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

                HiddenField hfProductNo = e.Item.FindControl("hfProductNo") as HiddenField;
                HiddenField hfProductName = e.Item.FindControl("hfProductName") as HiddenField;
                HiddenField hfProductID = e.Item.FindControl("hfProductID") as HiddenField;
                HiddenField hfMemberID = e.Item.FindControl("hfMemberID") as HiddenField;
                HiddenField hfAuctionPoint = e.Item.FindControl("hfAuctionPoint") as HiddenField;
                HiddenField hfAuctionTime = e.Item.FindControl("hfAuctionTime") as HiddenField;
                HiddenField hfTimePoint = e.Item.FindControl("hfTimePoint") as HiddenField;
                HiddenField hfStatus=e.Item.FindControl("hfStatus")as HiddenField;

                imgProduct.ToolTip = "第" + hfProductNo.Value + "期 " + hfProductName.Value;
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
                string proId = e.CommandArgument.ToString();
                if (Session["HuiYuanID"] == null || Session["HuiYuanName"] == null)
                {
                    MessageBox.AlertAndRedirect("请先登录", "UserLogin.aspx", Page);
                }
                else 
                {
                    string hyId=Session["HuiYuanID"].ToString();
                    using (TransactionScope ts=new TransactionScope())
                    {
                         //修改产品表
                            Product pro = new Product();
                            HiddenField hfStatus=e.Item.FindControl("hfStatus")as HiddenField;
                            if (hfStatus.Value!="3")
                            {
                                pro.TimePoint = 10;
                            }                           
                            pro.HuiYuanID=Session["HuiYuanID"].ToString();
                            pro.ProductID = proId;
                            proBll.UpdateProductPrice(pro);
                            //查询出价后的产品信息
                            Product pro2 = proBll.GetById(proId)[0];
                            //添加出价记录
                            ChuJiaJiLu cj = new ChuJiaJiLu();
                            cj.AuctionTime = DateTime.Now;
                            cj.ProductID = proId;
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
                    Bind();
                }
            }
        }
    }
}