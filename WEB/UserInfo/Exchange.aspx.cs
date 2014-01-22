using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model.Entities;
using BLL;
using BLL.SystemSeting;
using System.Data;

namespace WEB.UserInfo
{
    public partial class Exchange : System.Web.UI.Page
    {
        DingDanBll orderBll = new DingDanBll();
        ProductBLL proBll = new ProductBLL();
        ShouHuoDZBll adressBll = new ShouHuoDZBll();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bind();
            }
        }

        public void Bind() 
        {
            if (Session["HuiYuanID"] == null || Session["HuiYuanName"] == null)
            {
                Response.Redirect("../Auction/Index.aspx");
            }
            else 
            {
                string hyId=Session["HuiYuanID"].ToString();
                string orderType = "积分兑换";
                DataTable dt = orderBll.getDingDanbyType(hyId,orderType);
                if (dt.Rows.Count>0)
                {
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
        }

        protected void dlstExchange_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType==ListItemType.Item||e.Item.ItemType==ListItemType.AlternatingItem)
            {
                Image imgPro = e.Item.FindControl("imgPro") as Image;
                Literal proName = e.Item.FindControl("ltlProName") as Literal;
                Label points = e.Item.FindControl("lblPoints") as Label;
                Label shipAdress = e.Item.FindControl("lblShipAdress") as Label;

                string proId=dlstExchange.DataKeys[e.Item.ItemIndex].ToString();
                List<Product> list_pro = proBll.GetById(proId);
                if (list_pro.Count>0)
                {
                    proName.Text=list_pro[0].productName;
                    points.Text=list_pro[0].Points.ToString();
                }
                List<ProductImeg> list_img = proBll.GetProtductImeg("",proId);
                if (list_img.Count>0)
                {
                    imgPro.ImageUrl=list_img[0].img;                    
                }
                List<ShouHuoDZ> list_adr = adressBll.GetShouHuoDZ(shipAdress.Text);
                if (list_adr.Count > 0)
                {
                    shipAdress.Text = list_adr[0].ShouHuoName;
                }
                else 
                {
                    shipAdress.Text = "";
                }
            }
        }

        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            Bind();
        }
    }
}