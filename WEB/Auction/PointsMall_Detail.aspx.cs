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
using System.Transactions;

namespace WEB.Auction
{
    public partial class PointsMall_Detail : System.Web.UI.Page
    {
        ProductBLL proBll = new ProductBLL();
        HuiYuanXinXiBll hyBll = new HuiYuanXinXiBll();
        ShouHuoDZBll adressBll = new ShouHuoDZBll();
        DingDanBll orderBll = new DingDanBll();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bind();
            }
            AjaxPro.Utility.RegisterTypeForAjax(typeof(PointsMall_Detail));
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
                    BindAdress();
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
                    img2.ImageUrl = "";
                    img3.ImageUrl = "";
                    imgPro.ImageUrl = "";
                    break;
                case 1:
                     img1.ImageUrl = list_img[0].img;
                    img2.ImageUrl = "";
                    img3.ImageUrl = "";
                    imgPro.ImageUrl = list_img[0].img;
                    break;
                case 2:
                    img1.ImageUrl = list_img[0].img;
                    img2.ImageUrl = list_img[1].img;
                    img3.ImageUrl = "";
                    imgPro.ImageUrl = list_img[0].img;
                    break;
                case 3:
                    img1.ImageUrl = list_img[0].img;
                    img2.ImageUrl = list_img[1].img;
                    img3.ImageUrl = list_img[2].img;
                    imgPro.ImageUrl = list_img[0].img;
                    break;
                default:
                    img1.ImageUrl = list_img[0].img;
                    img2.ImageUrl = list_img[1].img;
                    img3.ImageUrl = list_img[2].img;
                    imgPro.ImageUrl = list_img[0].img;
                    break;
            }
        }

        public void BindAdress()
        {
            string hyId = Session["HuiYuanID"].ToString();
            dlstShipAdress.DataSource = adressBll.GetShouHuoDZbyhyId(hyId);
            dlstShipAdress.DataBind();
        }

        [AjaxPro.AjaxMethod]
        public string Check() 
        {
            if (Session["HuiYuanID"] == null || Session["HuiYuanName"] == null)
            {
                return "0";
            }
            else 
            {
                if (lblCurPoints.Text == "" || Convert.ToInt32(lblCurPoints.Text) < Convert.ToInt32(lblPoints.Text))
                {
                    return "1";
                }
                else 
                {
                    return "2";
                }
            }
        }

        protected void dlstShipAdress_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HiddenField select = e.Item.FindControl("hfSelect") as HiddenField;
                RadioButton rbtn = e.Item.FindControl("rbtnAdress") as RadioButton;
                if (select.Value == "1")
                {
                    rbtn.Checked = true;
                }
                else
                {
                    rbtn.Checked = false;
                }
            }
        }

        protected void rbtnAddAdress_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnAddAdress.Checked == true)
            {
                pnlAddAdress.Visible = true;
                pnlAdressList.Visible = false;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (txtAdress.Text == "" || txtname.Text == "" || txtPhone.Text == "" || txtPostCode.Text == "")
            {
                return;
            }
            else
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    string hyId = Session["HuiYuanID"].ToString();
                    if (adressBll.GetShouHuoDZbyhyId(hyId).Count > 0)
                    {
                        adressBll.UpdateStatusbyhyId(hyId, 0);
                    }
                    ShouHuoDZ adress = new ShouHuoDZ();
                    adress.IsSelect = 1;
                    adress.DZ = txtAdress.Text;
                    adress.HuiYuanID = hyId;
                    adress.Remark = txtRemark.Text;
                    adress.ShouHuoName = txtname.Text;
                    adress.YouBian = txtPostCode.Text;
                    adress.Mode = txtPhone.Text;
                    adress.CreateTime = DateTime.Now;
                    adressBll.AddShouHuoDZ(adress);
                    ts.Complete();
                }
                BindAdress();
                ClearTextBox();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {            
            ClearTextBox();
        }

        /// <summary>
        /// 清空文本框
        /// </summary>
        public void ClearTextBox()
        {
            txtAdress.Text = "";
            txtname.Text = "";
            txtPhone.Text = "";
            txtPostCode.Text = "";
            txtRemark.Text = "";
            pnlAddAdress.Visible = false;
            pnlAdressList.Visible = true;
            rbtnAddAdress.Checked = false;
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                using (TransactionScope ts=new TransactionScope())
                {
                    DingDan order = new DingDan();
                    order.DingDanBH = DateTime.Now.ToString("yyyyMMddHHmmss");
                    order.DingDanTime = DateTime.Now;
                    order.Fee = 0;
                    order.ShipFee = 0;
                    order.TotalPrice = 0;
                    order.Status=8;
                    order.ProductPrice =Convert.ToDecimal(lblPrice.Text);
                    order.OrderTypeID = orderBll.GetbyName("积分兑换")[0].OrderTypeID;
                    order.InvalidTime = DateTime.MaxValue;
                    order.AuctionID = "";
                    order.ProductID=Request.QueryString["id"];
                    order.HuiYuanID=Session["HuiYuanID"].ToString();
                    for (int i = 0; i < dlstShipAdress.Items.Count; i++)
                    {
                       RadioButton rbtn=dlstShipAdress.Items[i].FindControl("rbtnAdress")as RadioButton;
                       if (rbtn.Checked)
                       {
                           order.ShouHuoDZID = dlstShipAdress.DataKeys[i].ToString();
                           break;
                       }
                    }
                    orderBll.AddOrder(order);
                    string hyId=Session["HuiYuanID"].ToString();
                    int points = Convert.ToInt32(lblPoints.Text) * -1;
                    hyBll.UpdatePoints(hyId,points);
                    ts.Complete();
                }
                Response.Redirect("../UserInfo/Exchange.aspx");
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}