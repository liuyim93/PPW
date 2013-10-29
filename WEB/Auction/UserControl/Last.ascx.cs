using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL.SystemSeting;
using Model.Entities;

namespace WEB.Auction.UserControl
{
    public partial class Last : System.Web.UI.UserControl
    {
        ProductBLL proBll = new ProductBLL();
        HuiYuanXinXiBll hyBll = new HuiYuanXinXiBll();
        protected void Page_Load(object sender, EventArgs e)
        {
            //历史成交
            dlstHistory.DataSource = proBll.GetDoneProduct_Top5();
            dlstHistory.DataBind();
        }

        protected void dlstHistory_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HyperLink hlnkProName = e.Item.FindControl("hlnkProName") as HyperLink;
                Image imgProduct = e.Item.FindControl("imgProduct") as Image;
                HiddenField hfProductID = e.Item.FindControl("hfProductID") as HiddenField;
                HiddenField hfProductNo = e.Item.FindControl("hfProductNo") as HiddenField;
                Label lblMemberName = e.Item.FindControl("lblMemberName") as Label;
                hlnkProName.ToolTip = "第" + hfProductNo.Value + "期" + hlnkProName.Text;
                imgProduct.ToolTip = hlnkProName.ToolTip;
                List<ProductImeg> list = proBll.GetProtductImeg("", hfProductID.Value);
                if (list.Count > 0)
                {
                    imgProduct.ImageUrl = "../"+list[0].img;
                }
                else
                {
                    imgProduct.ImageUrl = "";
                }
                lblMemberName.Text = hyBll.GetHuiYuan(lblMemberName.Text).HuiYuanName;
            }
        }
    }
}