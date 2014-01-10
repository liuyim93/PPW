using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL.SystemSeting;
using Model.Entities;
using System.Web.Script.Serialization;
using BLL;
using System.Transactions;

namespace WEB.Auction
{
    public partial class testajax : System.Web.UI.Page
    {
        AuctionBll auctionBll=new AuctionBll();
        DingDanBll orderBll = new DingDanBll();
        ProductBLL proBll = new ProductBLL();
        JavaScriptSerializer serialize = new JavaScriptSerializer();
        protected void Page_Load(object sender, EventArgs e)
        {
            string auctionIdlist=Request.QueryString["tid"];
            string[] auctionIds = auctionIdlist.Split(',');
            List<auction> list = new List<auction>();
            for (int i = 0; i < auctionIds.Length; i++)
            {
                List<auction> list_act = auctionBll.GetAuction(auctionIds[i]);
                if (list_act[0].Status != 3)
                {
                    if (list_act[0].AuctionTime < DateTime.Now.AddSeconds(10))
                    {
                        auctionBll.UpdateTimePoint(auctionIds[i]);
                        List<auction> list_act1 = auctionBll.GetAuction(auctionIds[i]);
                        if (list_act1[0].TimePoint == 0)
                        {
                            try
                            {
                                using (TransactionScope ts = new TransactionScope())
                                {
                                    //生成竞拍订单
                                    DingDan dd = new DingDan();
                                    dd.DingDanBH = DateTime.Now.ToString("yyyyMMddHHmmss");
                                    dd.HuiYuanID = list_act1[0].HuiYuanID;
                                    dd.ProductID = list_act1[0].ProductID;
                                    dd.ProductPrice = list_act1[0].AuctionPrice;
                                    dd.Status = 10;
                                    dd.OrderTypeID = orderBll.GetbyName("竞拍订单")[0].OrderTypeID;
                                    dd.Fee = proBll.GetById(list_act1[0].ProductID)[0].Fee;
                                    dd.ShipFee = proBll.GetById(list_act1[0].ProductID)[0].ShipFee;
                                    dd.TotalPrice = dd.Fee + dd.ShipFee + dd.ProductPrice;
                                    dd.InvalidTime = DateTime.Now.AddDays(7);
                                    dd.AuctionID = auctionIds[i];
                                    dd.ShouHuoDZID = "";
                                    orderBll.AddOrder(dd);

                                    //修改拍品状态为已成交
                                    auction auction2 = new auction();
                                    auction2.Status = 3;
                                    auction2.EndTime = DateTime.Now;
                                    auction2.AuctionID = auctionIds[i];
                                    auctionBll.UpdateStatus(auction2);
                                    ts.Complete();
                                }
                            }
                            catch (Exception)
                            {

                                throw;
                            }
                        }
                        list.Insert(list.Count, list_act1[0]);
                    }
                    else 
                    {
                        list.Insert(list.Count,list_act[0]);
                    }                                        
                }
                else 
                {
                    list.Insert(list.Count, list_act[0]);
                }                
            }            
            string jsonStr = serialize.Serialize(list);
            Response.Clear();
            Response.Write(jsonStr);
            Response.End();
        }
    }
}