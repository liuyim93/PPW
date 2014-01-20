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

namespace WEB.Auction.ajax
{
    public partial class Points : System.Web.UI.Page
    {
        HuiYuanXinXiBll hyBll = new HuiYuanXinXiBll();
        DingDanBll orderBll = new DingDanBll();
        ProductBLL proBll = new ProductBLL();        
        protected void Page_Load(object sender, EventArgs e)
        {            
            string msg = "";
            if (HttpContext.Current.Session["HuiYuanID"]==null) {
                msg = "2";
            }else if (Request.QueryString["adrId"] != null&&Request.QueryString["proId"] != null) {
                string hyId=HttpContext.Current.Session["HuiYuanID"].ToString();
                string adrId = Request.QueryString["adrId"];
                string proId = Request.QueryString["proId"];
                decimal proPrice = Convert.ToDecimal(proBll.GetById(proId)[0].productPrice);
                int points = -1 * proBll.GetById(proId)[0].Points;
                try
                {
                    using (TransactionScope ts = new TransactionScope())
                    {
                        DingDan order = new DingDan();
                        order.AuctionID = "";
                        order.DingDanBH = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                        order.DingDanTime = DateTime.Now;
                        order.ShouHuoDZID = adrId;
                        order.Status = 8;
                        order.ShipFee = 0;
                        order.Fee = 0;
                        order.TotalPrice = 0;
                        order.ProductPrice = proPrice;
                        order.HuiYuanID = hyId;
                        order.ProductID = proId;
                        order.OrderTypeID=orderBll.GetOrderTypebyName("积分兑换")[0].OrderTypeID;
                        order.InvalidTime = DateTime.MaxValue;
                        orderBll.AddOrder(order);
                        hyBll.UpdatePoints(hyId,points);
                        ts.Complete();
                    }
                    msg = "1";
                }
                catch (Exception)
                {
                    
                    throw;
                }
            }
            Response.Clear();
            Response.Write(msg);
            Response.End();
        }
    }
}