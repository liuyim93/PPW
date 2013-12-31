using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL.SystemSeting;
using Model.Entities;
using BLL;
using System.Transactions;
using System.Web;

namespace WEB.Auction
{
    public partial class submitauction : System.Web.UI.Page
    {
        HuiYuanXinXiBll hyBll = new HuiYuanXinXiBll();
        AuctionBll auctionBll = new AuctionBll();
        ChuJiaJiLuBll recordBll = new ChuJiaJiLuBll();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Request.QueryString["AuctionID"]!=""){
                string userName=HttpContext.Current.Session["HuiYuanName"].ToString();
                string auctionId=Request.QueryString["AuctionID"];
                List<HuiYuan>list_hy = hyBll.GetHuiYuan(userName,"","");
                if(list_hy.Count==1){
                    string hyId=list_hy[0].HuiYuanID;                    
                    using(TransactionScope ts=new TransactionScope()){
                        auction act = new auction();
                        act.AuctionID = auctionId;
                        act.HuiYuanID = hyId;
                        act.TimePoint = 10;  
                        auctionBll.UpdateAuctionPrice(act);
                        List<auction>list_act = auctionBll.GetAuction(auctionId);
                        if(list_act.Count>0){
                            ChuJiaJiLu record = new ChuJiaJiLu();
                            record.AuctionID = auctionId;
                            record.HuiYuanID = hyId;
                            record.IPAdress = HttpContext.Current.Request.UserHostAddress;
                            record.Status = 1;
                            record.Price=list_act[0].AuctionPrice;
                            record.AuctionPoint=list_act[0].AuctionPoint;
                            record.FreePoint=list_act[0].FreePoint;
                            record.AuctionTime = DateTime.Now;
                            recordBll.AddChuJiaJiLu(record);

                            HuiYuan hy = new HuiYuan();
                            hy.HuiYuanID = hyId;
                            hy.PaiDian=list_act[0].AuctionPoint*(-1);
                            hy.FreePoint=list_act[0].FreePoint*(-1);
                            hyBll.UpdateHuiYuanPoint(hy);
                            ts.Complete();
                        }
                    }
                }
                Response.Clear();
                Response.Write("ok");
                Response.End();
            }
        }
    }
}