using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model.Entities;
using BLL;
using BLL.SystemSeting;
using System.Web.Script.Serialization;

namespace WEB.Auction.ajax
{
    public partial class bidHistory : System.Web.UI.Page
    {
        ChuJiaJiLuBll recordBll = new ChuJiaJiLuBll();
        JavaScriptSerializer serialize = new JavaScriptSerializer();
        protected void Page_Load(object sender, EventArgs e)
        {
            string auctionId=Request.QueryString["id"];
            List<ChuJiaJiLu> list = recordBll.GetChuJiaJiLubyauctionId_Top10(auctionId);            
            string jsonStr = serialize.Serialize(list);
            Response.Clear();
            Response.Write(jsonStr);
            Response.End();
        }
    }
}