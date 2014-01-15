using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using BLL.SystemSeting;
using BLL;
using Model.Entities;
using System.Web.Script.Serialization;

namespace WEB.Auction.ajax
{
    /// <summary>
    /// 根据竞拍ID 返回竞拍历史记录
    /// </summary>
    public class BidHistory : IHttpHandler
    {
        ChuJiaJiLuBll recordBll = new ChuJiaJiLuBll();
        JavaScriptSerializer serialize = new JavaScriptSerializer();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            string auctionId = context.Request["id"];
            List<ChuJiaJiLu> list = recordBll.GetChuJiaJiLubyauctionId_Top10(auctionId);
            string jsonStr = serialize.Serialize(list);
            context.Response.Write(jsonStr);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}