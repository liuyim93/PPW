using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using BLL.SystemSeting;
using Model.Entities;
using System.Web.Script.Serialization;

namespace WEB.Auction.ajax
{
    public partial class UserInfoSelf : System.Web.UI.Page
    {
        ChuJiaJiLuBll recordBll = new ChuJiaJiLuBll();
        JavaScriptSerializer serialize = new JavaScriptSerializer();
        protected void Page_Load(object sender, EventArgs e)
        {
            int auctionPoint = 0;
            int freePoint = 0;
            if(Request.QueryString["tid"]!=null&&HttpContext.Current.Session["HuiYuanID"]!=null){
                string auctionId = Request.QueryString["tid"];
                string hyId = HttpContext.Current.Session["HuiYuanID"].ToString();
                List<ChuJiaJiLu> list = recordBll.GetChuJiaJiLu(auctionId, hyId);
                if (list.Count > 0)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        auctionPoint += list[i].AuctionPoint;
                        freePoint += list[i].FreePoint;
                    }
                }
            }                        
            string jsonStr = "{\"AuctionPoint\":"+auctionPoint+",\"FreePoint\":"+freePoint+"}";
            Response.Clear();
            Response.Write(jsonStr);
            Response.End();
        }
    }
}