﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL.SystemSeting;
using Model.Entities;
using BLL;
using System.Data;

namespace WEB.UserInfo
{
    public partial class Biding : System.Web.UI.Page
    {
        ProductBLL proBll = new ProductBLL();
        ChuJiaJiLuBll recordBll = new ChuJiaJiLuBll();
        HuiYuanXinXiBll hyBll = new HuiYuanXinXiBll();
        AuctionBll auctionBll = new AuctionBll();
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
                int status = 4;
                 DataTable dt= auctionBll.getAuctionbyStatus(hyId,status);                 
                 if (dt.Rows.Count>0)
                 {
                     AspNetPager1.RecordCount = dt.Rows.Count;
                     PagedDataSource pds = new PagedDataSource();
                     pds.DataSource = dt.DefaultView;
                     pds.AllowPaging = true;
                     pds.PageSize = AspNetPager1.PageSize;
                     pds.CurrentPageIndex = AspNetPager1.CurrentPageIndex - 1;
                     dlstBiding.DataSource = pds;
                     dlstBiding.DataBind();
                 }
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
                Literal proName = e.Item.FindControl("ltlProName") as Literal;

                string auctionId=dlstBiding.DataKeys[e.Item.ItemIndex].ToString();
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
                List<auction> list_act = auctionBll.GetAuction(auctionId);
                List<ChuJiaJiLu> list_record = recordBll.GetChuJiaJiLu(auctionId,hyId);
                proName.Text=list_pro[0].productName;
                bidCount.Text =list_record.Count.ToString();
                string points = (list_record.Count * (list_record[0].AuctionPoint + list_record[0].FreePoint)).ToString();
                if (proBll.GetAuctionTypebyID(list_act[0].AuctionTypeID)[0].TypeName == "常规竞拍")
                {
                    pointCount.Text = "拍点：" + points;
                }
                else
                {
                    pointCount.Text = "返点：" + points;
                }

                List<ChuJiaJiLu> list_record1 = recordBll.GetHuiYuanIDbyauctionId(proId.Value);
                nums.Text = "共有" + list_record1.Count.ToString() + "人参与";
            }
        }

        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            Bind();
        }
    }
}