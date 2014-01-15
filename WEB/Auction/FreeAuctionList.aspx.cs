using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL.SystemSeting;
using BLL;
using System.Data;
using Model.Entities;

namespace WEB.Auction
{
    public partial class FreeAuctionList : System.Web.UI.Page
    {
        AuctionBll auctionBll = new AuctionBll();
        protected void Page_Load(object sender, EventArgs e)
        {
            Bind();
        }

        public void Bind() {
            DataTable dt = auctionBll.getFreeAuctioning();
            if(dt.Rows.Count>0){
                AspNetPager1.RecordCount = dt.Rows.Count;
                PagedDataSource pds = new PagedDataSource();
                pds.DataSource = dt.DefaultView;
                pds.AllowPaging = true;
                pds.PageSize = AspNetPager1.PageSize;
                pds.CurrentPageIndex = AspNetPager1.CurrentPageIndex - 1;
                repeater1.DataSource = pds;
                repeater1.DataBind();
            }            
        }

        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            Bind();
        }
    }
}