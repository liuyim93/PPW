using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model.Entities;
using BLL.SystemSeting;

namespace WEB.Auction
{
    public partial class AuctionHistory : System.Web.UI.Page
    {
        ProductBLL productBll=new ProductBLL();
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
            //所有已成交的商品
            gvwHistory.DataSource = productBll.GetAllDoneProduct();
            gvwHistory.DataBind();
        }

        protected void gvwHistory_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType==DataControlRowType.DataRow)
            {
                Label lblMemberName = e.Row.FindControl("lblMemberName") as Label;
                HiddenField hfProductID = e.Row.FindControl("hfProductID") as HiddenField;
                HiddenField hfProductNo = e.Row.FindControl("hfProductNo") as HiddenField;
                Image imgProduct = e.Row.FindControl("imgProduct") as Image;
                HiddenField hfProductName=e.Row.FindControl("hfProductName")as HiddenField;
                if (hfProductID.Value!="")
                {
                    List<ProductImeg> list = productBll.GetProtductImeg("",hfProductID.Value);
                    if (list.Count>0)
                    {
                        imgProduct.ImageUrl = list[0].img;
                        imgProduct.ToolTip="第"+hfProductNo.Value+"期 "+hfProductName.Value;
                    }
                }
                if (lblMemberName.Text!="")
                {
                    lblMemberName.Text = hyBll.GetHuiYuan(lblMemberName.Text).HuiYuanName;
                }
            }
        }
    }
}