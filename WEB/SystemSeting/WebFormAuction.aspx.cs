using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using BLL.SystemSeting;
using Ext.Net;
using Model.Entities;

namespace WEB.SystemSeting
{
    public partial class WebFormAuction : System.Web.UI.Page
    {
        AuctionBll auctionBll = new AuctionBll();
        ProductBLL proBll = new ProductBLL();
        HuiYuanXinXiBll hyBll = new HuiYuanXinXiBll();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void AddShow(object sender,EventArgs e) 
        {
            
        }

        public void EditShow(object sender,EventArgs e) 
        {
            
        }

        protected void Del(object sender,DirectEventArgs e) 
        {
            
        }

        protected void ShowDetail(object sender,DirectEventArgs e) 
        {
            
        }

        protected void AddAuction(object sender,DirectEventArgs e) 
        {
            
        }

        protected void EditAuction(object sender,DirectEventArgs e) 
        {
            
        }

        public void BindData() 
        {
            string proName = txtProName.Text;
            string auctionType = cboxAuctionType.Text;
            string status = cboxStatus.Value.ToString();
            List<auction> list_act = auctionBll.GetAuction(proName,auctionType,status);
            storeAuction.DataSource = list_act.Select(x=> new 
            {
               AuctionID=x.AuctionID,
               ProductName=proBll.GetById(x.ProductID)[0].productName,
               ProductPrice=proBll.GetById(x.ProductID)[0].productPrice,
               Coding=x.Coding,
               HuiYuan=x.HuiYuanID==null?"":hyBll.GetHuiYuan(x.HuiYuanID).HuiYuanName,
               AuctionPrice=x.AuctionPrice,
               AuctionPoint=x.AuctionPoint,
               FreePoint=x.FreePoint,
               AuctionTime=x.AuctionTime,
               CreateTime=x.CreateTime,
               Status=x.Status==3?"已成交":"未成交",
               PriceAdd=x.PriceAdd,
               EndTime=x.EndTime==null?"":x.EndTime.ToString(),
               AuctionType=auctionBll.GetAuctionTypebyId(x.AuctionTypeID),
               IsRecommend=x.IsRecommend==1?"是":"否"
            });
        }
    }
}