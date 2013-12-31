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

namespace WEB.Auction
{
    public partial class testajax : System.Web.UI.Page
    {
        AuctionBll auctionBll=new AuctionBll();
        JavaScriptSerializer serialize = new JavaScriptSerializer();
        protected void Page_Load(object sender, EventArgs e)
        {
            string auctionId=Request.QueryString["AuctionID"];
            List<auction> list = auctionBll.GetAllAuctioning();
            string jsonStr = serialize.Serialize(list);
            Response.Clear();
            Response.Write(jsonStr);
            Response.End();
        }
    }
}