using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using BLL.SystemSeting;
using Model.Entities;
using System.Transactions;

namespace WEB.Auction
{
    public partial class auctioncomplete : System.Web.UI.Page
    {
        AuctionBll auctionBll = new AuctionBll();
        DingDanBll orderBll = new DingDanBll();
        ProductBLL proBll = new ProductBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Request.QueryString["AuctionID"]!=""&&Request.QueryString["AuctionID"]!=null&&Request.QueryString["hyId"]!=""&&Request.QueryString["hyId"]!=null){
                string auctionId=Request.QueryString["AuctionID"];
                using(TransactionScope ts=new TransactionScope()){
                    //修改拍品状态
                    auction act = new auction();
                    act.AuctionID = auctionId;
                    act.EndTime = DateTime.Now;
                    act.Status = 3;
                    auctionBll.UpdateStatus(act);

                    List<auction>list_act=auctionBll.GetAuction(auctionId);
                    List<Product>list_pro=proBll.GetById(list_act[0].ProductID);

                    //生成订单
                    DingDan order = new DingDan();
                    order.AuctionID = auctionId;
                    order.HuiYuanID=Request.QueryString["hyId"];
                    order.DingDanBH = DateTime.Now.ToString("yyyyMMddhhmmss");
                    order.ProductID=list_act[0].ProductID;
                    order.ProductPrice=list_act[0].AuctionPrice;
                    order.OrderTypeID = orderBll.GetOrderTypebyName("竞拍订单")[0].OrderTypeID;
                    order.Fee = list_pro[0].Fee;
                    order.ShipFee=list_pro[0].ShipFee;
                    order.Status = 10;
                    order.TotalPrice = order.Fee + order.ShipFee + order.ProductPrice;
                    order.InvalidTime = DateTime.Now.AddDays(7);
                    order.ShouHuoDZID = "";
                    orderBll.AddOrder(order);
                    ts.Complete();
                }
                Response.Clear();
                Response.Write("ok");
                Response.End();
            }
        }
    }
}