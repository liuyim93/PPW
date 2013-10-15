using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL.SystemSeting;
using Model.Entities;
using BLL;

namespace WEB.UserInfo
{
    public partial class Biding : System.Web.UI.Page
    {
        ProductBLL proBll = new ProductBLL();
        ChuJiaJiLuBll recordBll = new ChuJiaJiLuBll();
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
            if (Session["HuiYuanName"] == null || Session["HuiYuanID"] == null)
            {
                Response.Redirect("../Auction/Index.aspx");
            }
            else 
            {
                string hyId = Session["HuiYuanID"].ToString();
                int status = 3;
                dlstBiding.DataSource = proBll.GetProductbyStatus(hyId,status);
                dlstBiding.DataBind();
            }
        }

        protected void dlstBiding_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType==ListItemType.Item||e.Item.ItemType==ListItemType.AlternatingItem)
            {
                Label proNo = e.Item.FindControl("lblProNo") as Label;
                Label nums = e.Item.FindControl("lblNums") as Label;
                Image img = e.Item.FindControl("img") as Image;
                Label member = e.Item.FindControl("lblMemberName") as Label;
                Label bidCount = e.Item.FindControl("lblBidCount") as Label;
                Label pointCount = e.Item.FindControl("lblPointCount") as Label;
                HiddenField proId = e.Item.FindControl("hfProID") as HiddenField;
                proNo.Text = "第" + proNo.Text + "期";
                List<ProductImeg>list=proBll.GetProtductImeg("",proId.Value);
                if (list.Count > 0)
                {
                    img.ImageUrl = list[0].img;
                }
                else 
                {
                    img.ImageUrl = "";
                }
                member.Text = hyBll.GetHuiYuan(member.Text).HuiYuanName;

                string hyId=Session["HuiYuanID"].ToString();
                List<Product> list_pro = proBll.GetById(proId.Value);
                List<ChuJiaJiLu> list_record = recordBll.GetChuJiaJiLu(proId.Value,hyId);
                bidCount.Text =list_record.Count.ToString();
                string points = (list_record.Count * (list_record[0].AuctionPoint + list_record[0].FreePoint)).ToString();
                if (proBll.GetAuctionTypebyID(list_pro[0].AuctionTypeID)[0].TypeName=="常规竞拍")
                {
                    pointCount.Text = "拍点：" + points;
                }
                else 
                {
                    pointCount.Text = "返点：" + points;
                }

                List<ChuJiaJiLu> list_record1 = recordBll.GetHuiYuanIDbyProId(proId.Value);
                nums.Text = "共有" + list_record1.Count.ToString() + "人参与";
            }
        }
    }
}