using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using BLL.SystemSeting;
using Model.Entities;
using System.Data;

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
            dropProType.DataSource = proBll.GetAllProductType();
            dropProType.DataTextField = "TypeName";
            dropProType.DataValueField = "ProductTypeID";
            dropProType.DataBind();
            dropProType.Items.Insert(0,"不限");
            BindData();
        }

        public void BindData() {
            string proTypeId = "";
            string minPoints = "0";
            string maxPoints = "0";
            DataTable dt = proBll.GetExchangeProduct(proTypeId, minPoints, maxPoints);
            if(dt.Rows.Count>0){
                AspNetPager1.RecordCount = dt.Rows.Count;
                PagedDataSource pds = new PagedDataSource();
                pds.DataSource = dt.DefaultView;
                pds.AllowPaging = true;
                pds.PageSize = AspNetPager1.PageSize;
                pds.CurrentPageIndex = AspNetPager1.CurrentPageIndex - 1;
                dlstExchange.DataSource = pds;
                dlstExchange.DataBind();
            }            
        }

        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            string proTypeId = dropProType.SelectedValue;
            if(proTypeId=="不限"){
                proTypeId = "";
            }
            string Points = dropPoints.SelectedValue;
            string[] Point = Points.Split('-');
            string minPoint=Point[0];
            string maxPoint=Point[1];
            dlstExchange.DataSource = proBll.GetExchangeProduct(proTypeId,minPoint,maxPoint);
            dlstExchange.DataBind();
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

        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            BindData();
        }
    }
}