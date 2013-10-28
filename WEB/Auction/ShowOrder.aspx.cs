using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model.Entities;
using BLL;
using Tools;
using BLL.SystemSeting;

namespace WEB.Auction
{
    public partial class ShowOrder : System.Web.UI.Page
    {
        ShowOrderBll showOrderBll = new ShowOrderBll();
        ProductBLL proBll = new ProductBLL();
        DingDanBll orderBll = new DingDanBll();
        HuiYuanXinXiBll hyBll = new HuiYuanXinXiBll();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bind();
            }
        }

        public void Bind() 
        {
            int isCheck = 1;
            dlstShowOrder.DataSource = showOrderBll.GetShowOrderbyCheck(isCheck);
            dlstShowOrder.DataBind();
        }

        protected void dlstShowOrder_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType==ListItemType.Item||e.Item.ItemType==ListItemType.AlternatingItem)
            {
                
            }
        }
    }
}