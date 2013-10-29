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
                Label donePrice = e.Item.FindControl("lblDonePrice") as Label;
                Label marketPrice = e.Item.FindControl("lblMarketPrice") as Label;
                Literal memberName = e.Item.FindControl("ltlMemberName") as Literal;
                Image img1 = e.Item.FindControl("img1") as Image;
                Image img2 = e.Item.FindControl("img2") as Image;
                Image img3 = e.Item.FindControl("img3") as Image;
                Image img4 = e.Item.FindControl("img4") as Image;
                HiddenField showOrderId = e.Item.FindControl("hfShowOrderID") as HiddenField;
                string orderId = dlstShowOrder.DataKeys[e.Item.ItemIndex].ToString();
                List<DingDan> list_order = orderBll.GetDingDan(orderId);
                if (list_order.Count>0)
                {
                    donePrice.Text="￥"+list_order[0].ProductPrice.ToString();
                    string proId=list_order[0].ProductID;
                    List<Product> list_pro = proBll.GetById(proId);
                    marketPrice.Text = "￥" + list_pro[0].productPrice.ToString();
                    string hyId=list_order[0].HuiYuanID;
                    HuiYuan hy = hyBll.GetHuiYuan(hyId);
                    memberName.Text = hy.HuiYuanName;
                }
                #region BindImg
                if (showOrderId.Value!="")
                {
                    List<ShowOrderImg> list_img = showOrderBll.GetShowOrderImg(showOrderId.Value);
                    switch (list_img.Count)
                    {
                        case 0:
                            img1.Visible = false;
                            img2.Visible = false;
                            img3.Visible = false;
                            img4.Visible = false;
                            break;
                        case 1:
                            img1.ImageUrl=list_img[0].ImgUrl;
                            img1.Visible = true;
                            img2.Visible = false;
                            img3.Visible = false;
                            img4.Visible = false;
                            break;
                        case 2:
                            img1.ImageUrl = list_img[0].ImgUrl;
                            img2.ImageUrl=list_img[1].ImgUrl;
                            img1.Visible = true;
                            img2.Visible = true;
                            img3.Visible = false;
                            img4.Visible = false;
                            break;
                        case 3:
                            img1.ImageUrl = list_img[0].ImgUrl;
                            img2.ImageUrl=list_img[1].ImgUrl;
                            img3.ImageUrl=list_img[2].ImgUrl;
                            img1.Visible = true;
                            img2.Visible = true;
                            img3.Visible = true;
                            img4.Visible = false;
                            break;
                        case 4:
                            img1.ImageUrl = list_img[0].ImgUrl;
                            img2.ImageUrl=list_img[1].ImgUrl;
                            img3.ImageUrl=list_img[2].ImgUrl;
                            img4.ImageUrl=list_img[3].ImgUrl;
                            img1.Visible = true;
                            img2.Visible = true;
                            img3.Visible = true;
                            img4.Visible = true;
                            break;
                        default:
                            break;
                    }
                #endregion
                }
            }
        }
    }
}