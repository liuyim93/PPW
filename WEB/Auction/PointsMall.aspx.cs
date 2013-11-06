using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using BLL.SystemSeting;
using Model.Entities;

namespace WEB.Auction
{
    public partial class PointsMall : System.Web.UI.Page
    {
        ProductBLL proBll = new ProductBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bind();
            }
        }

        public void Bind() 
        {
            dlstExchange.DataSource = proBll.GetAllExchangeProduct();
            dlstExchange.DataBind();
        }

        protected void lbtnSearch_Click(object sender, EventArgs e)
        {

        }

        protected void dlstExchange_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType==ListItemType.Item||e.Item.ItemType==ListItemType.AlternatingItem)
            {
                Image imgPro = e.Item.FindControl("imgPro") as Image;
                string proId=dlstExchange.DataKeys[e.Item.ItemIndex].ToString();

                List<ProductImeg> list_img = proBll.GetProtductImeg("",proId);
                if (list_img.Count>0)
                {
                    imgPro.ImageUrl=list_img[0].img;
                }
            }
        }
    }
}