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

namespace WEB.Auction
{
    public partial class submitauction : System.Web.UI.Page
    {
        HuiYuanXinXiBll hyBll = new HuiYuanXinXiBll();
        AuctionBll auctionBll = new AuctionBll();
        ChuJiaJiLuBll recordBll = new ChuJiaJiLuBll();
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Expires = -1000;
            string auctionId = Request.QueryString["tid"];       
            string isRobotUser = Request.QueryString["RobotUser"];
            string msg = "0";
            if (auctionId != "" && isRobotUser != "")
            {               
                if (HttpContext.Current.Session["HuiYuanID"] == null)
                {
                    msg = "2";
                    Response.Clear();
                    Response.Write(msg);
                    Response.End();
                }
                else
                {
                    string userName = HttpContext.Current.Session["HuiYuanName"].ToString();
                    string userId = HttpContext.Current.Session["HuiYuanID"].ToString();
                    List<auction> list_act = auctionBll.GetAuction(auctionId);
                    if (list_act.Count > 0)
                    {
                        if (list_act[0].Status == 3)
                        {
                            msg = "5";
                            Response.Clear();
                            Response.Write(msg);
                            Response.End();
                        }
                        else 
                        {
                            if (list_act[0].HuiYuanID != userId)
                            {
                                msg = "4";
                                Response.Clear();
                                Response.Write(msg);
                                Response.End();
                            }
                            else {
                                HuiYuan hy = hyBll.GetHuiYuan(userId);
                                if (auctionBll.GetAuctionTypebyName("常规竞拍")[0].AuctionTypeID == list_act[0].AuctionTypeID)
                                {
                                    if (hy.PaiDian < list_act[0].AuctionPoint)
                                    {
                                        msg = "3";
                                        Response.Clear();
                                        Response.Write(msg);
                                        Response.End();
                                    }
                                }
                                else {
                                    if (hy.FreePoint<list_act[0].FreePoint)
                                    {
                                        msg = "18";
                                        Response.Clear();
                                        Response.Write(msg);
                                        Response.End();
                                    }
                                }
                            }
                        }
                    }
                }
                string hyId = HttpContext.Current.Session["HuiYuanID"].ToString(); ;
                    try
                    {
                        using (TransactionScope ts = new TransactionScope())
                        {
                            auction act = new auction();
                            act.AuctionID = auctionId;
                            act.HuiYuanID = hyId;
                            act.TimePoint = 10;
                            auctionBll.UpdateAuctionPrice(act);

                            List<auction> list_act = auctionBll.GetAuction(auctionId);
                            if (list_act.Count > 0)
                            {
                                ChuJiaJiLu record = new ChuJiaJiLu();
                                record.AuctionID = auctionId;
                                record.HuiYuanID = hyId;
                                record.IPAdress = HttpContext.Current.Request.UserHostAddress;
                                record.Status = 1;
                                record.Price = list_act[0].AuctionPrice;
                                record.AuctionPoint = list_act[0].AuctionPoint;
                                record.FreePoint = list_act[0].FreePoint;
                                record.AuctionTime = DateTime.Now;
                                recordBll.AddChuJiaJiLu(record);

                                HuiYuan hy = new HuiYuan();
                                hy.HuiYuanID = hyId;
                                hy.PaiDian = list_act[0].AuctionPoint * (-1);
                                hy.FreePoint = list_act[0].FreePoint * (-1);
                                hyBll.UpdateHuiYuanPoint(hy);
                                
                            }
                            ts.Complete();
                            msg = "1";
                        }
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