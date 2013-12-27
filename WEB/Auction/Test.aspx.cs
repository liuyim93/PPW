using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using BLL.SystemSeting;
using Model.Entities;
using System.Runtime.Serialization;
using Tools;
using System.Web.Script.Serialization;

namespace WEB.Auction
{
    public partial class Test : System.Web.UI.Page
    {
        ProductBLL proBll = new ProductBLL();
        HuiYuanXinXiBll hyBll = new HuiYuanXinXiBll();
        ChuJiaJiLuBll cjBll = new ChuJiaJiLuBll();
        AuctionBll auctionBll = new AuctionBll();
        DingDanBll orderBll = new DingDanBll();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
            }           
        }

        public void BindData() 
        {
            dlstAuction.DataSource = auctionBll.GetAllAuctioning();
            dlstAuction.DataBind();
        }

        protected void dlstAuction_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Image imgProduct = e.Item.FindControl("imgProduct") as Image;
                Label lblMemberName = e.Item.FindControl("lblMemberName") as Label;
                ImageButton imgbtnAuction = e.Item.FindControl("imgbtnAuction") as ImageButton;
                Label lblTime = e.Item.FindControl("lblTime") as Label;
                HyperLink hlnkPro = e.Item.FindControl("hlnkPro") as HyperLink;
                Label lblIntro = e.Item.FindControl("lblIntro") as Label;
                Label lblPrice = e.Item.FindControl("lblPrice") as Label;

                HiddenField hfProductNo = e.Item.FindControl("hfProductNo") as HiddenField;
                HiddenField hfProductID = e.Item.FindControl("hfProductID") as HiddenField;
                HiddenField hfMemberID = e.Item.FindControl("hfMemberID") as HiddenField;
                HiddenField hfAuctionPoint = e.Item.FindControl("hfAuctionPoint") as HiddenField;
                HiddenField hfAuctionTime = e.Item.FindControl("hfAuctionTime") as HiddenField;
                HiddenField hfTimePoint = e.Item.FindControl("hfTimePoint") as HiddenField;
                HiddenField hfStatus = e.Item.FindControl("hfStatus") as HiddenField;

                string auctionId = dlstAuction.DataKeys[e.Item.ItemIndex].ToString();
                List<Product> list_pro = proBll.GetById(hfProductID.Value);
                string proName = list_pro[0].productName;
                imgProduct.ToolTip = "第" + hfProductNo.Value + "期 " + list_pro[0].productName;
                hlnkPro.Text = proName;
                hlnkPro.ToolTip = imgProduct.ToolTip;
                hlnkPro.NavigateUrl = "../Auction/ProDetail.aspx?id=" + auctionId;
                lblPrice.Text = list_pro[0].productPrice.ToString();
                lblIntro.Text = list_pro[0].Intro;

                List<ProductImeg> list = proBll.GetProtductImeg("", hfProductID.Value);
                if (list.Count > 0)
                {
                    imgProduct.ImageUrl = list[0].img;
                }
                else
                {
                    imgProduct.ImageUrl = "";
                }
                if (hfMemberID.Value == "")
                {
                    lblMemberName.Text = "";
                }
                else
                {
                    lblMemberName.Text = hyBll.GetHuiYuan(hfMemberID.Value).HuiYuanName;
                }
                imgbtnAuction.ToolTip = "每次出价消耗" + hfAuctionPoint.Value + "拍点";
                //if (hfAuctionTime.Value != "" && hfTimePoint.Value != "")
                //{
                //    if (Convert.ToDateTime(hfAuctionTime.Value) > DateTime.Now.AddSeconds(10) && hfStatus.Value != "3")
                //    {
                //        TimeSpan ts = Convert.ToDateTime(hfAuctionTime.Value) - DateTime.Now;
                //        lblTime.Text = ts.Hours.ToString().PadLeft(2, '0') + ":" + ts.Minutes.ToString().PadLeft(2, '0') + ":" + ts.Seconds.ToString().PadLeft(2, '0');
                //    }
                //    else
                //    {
                //        if (hfStatus.Value != "3")
                //        {
                //            lblTime.Text = "00:00:" + hfTimePoint.Value.PadLeft(2, '0');
                //        }
                //    }
                //}
            }
        }
    }
}