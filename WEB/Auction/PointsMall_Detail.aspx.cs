using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using BLL.SystemSeting;
using Model.Entities;
using Tools;

namespace WEB.Auction
{
    public partial class PointsMall_Detail : System.Web.UI.Page
    {
        ProductBLL proBll = new ProductBLL();
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
            if (Request.QueryString["id"] == null || Request.QueryString["id"] == "")
            {
                Response.Redirect("../Auction/PointsMall.aspx");
            }
            else 
            {
                string proId=Request.QueryString["id"];
                List<Product> list_pro = proBll.GetById(proId);
                if (list_pro.Count > 0)
                {
                    lblProName.Text = list_pro[0].productName;
                    lblPrice.Text = list_pro[0].productPrice.ToString();
                    lblPoints.Text = list_pro[0].Points.ToString();
                    lblDetail.Text = list_pro[0].ProductDetails;
                }
                else 
                {
                    MessageBox.AlertAndRedirect("商品不存在","../Auction/PointsMall.aspx",Page);
                }
                if (Session["HuiYuanID"] == null || Session["HuiYuanName"] == null)
                {
                    lblCurPoints.Text = "0";
                }
                else 
                {
                    string hyId=Session["HuiYuanID"].ToString();
                    HuiYuan hy = hyBll.GetHuiYuan(hyId);
                    lblCurPoints.Text=hy.Points.ToString();
                }
                BindImg();
            }
        }

        public void BindImg() 
        {
            string proId=Request.QueryString["id"];
            List<ProductImeg> list_img = proBll.GetProtductImeg("", proId);
            switch (list_img.Count)
            {
                case 0:
                    img1.ImageUrl = "";
                    img1.ImageUrl = "";
                    img1.ImageUrl = "";
                    imgPro.ImageUrl = "";
                    break;
                case 1:
                     img1.ImageUrl = list_img[0].img;
                    img1.ImageUrl = "";
                    img1.ImageUrl = "";
                    imgPro.ImageUrl = list_img[0].img;
                    break;
                case 2:
                    img1.ImageUrl = list_img[0].img;
                    img1.ImageUrl = list_img[1].img;
                    img1.ImageUrl = "";
                    imgPro.ImageUrl = list_img[0].img;
                    break;
                case 3:
                    img1.ImageUrl = list_img[0].img;
                    img1.ImageUrl = list_img[1].img;
                    img1.ImageUrl = list_img[2].img;
                    imgPro.ImageUrl = list_img[0].img;
                    break;
                default:
                    img1.ImageUrl = list_img[0].img;
                    img1.ImageUrl = list_img[1].img;
                    img1.ImageUrl = list_img[2].img;
                    imgPro.ImageUrl = list_img[0].img;
                    break;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

        }
    }
}