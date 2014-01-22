using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL.SystemSeting;
using BLL;
using Model.Entities;
using System.Transactions;
using Tools;

namespace WEB.UserInfo
{
    public partial class ShowOrder : System.Web.UI.Page
    {
        DingDanBll orderBll=new DingDanBll();
        ProductBLL proBll = new ProductBLL();
        ShowOrderBll showOrderBll = new ShowOrderBll();
        public static List<string> listImg = new List<string>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bind();
            }
        }

        /// <summary>
        /// 商品图片、名称
        /// </summary>
        public void Bind() 
        {
            if (Session["HuiYuanID"] == null || Session["HuiYuanName"] == null)
            {
                Response.Redirect("../Auction/Index.aspx");
            }
            else 
            {
                if (Request.QueryString["id"] == null || Request.QueryString["id"] == "")
                {
                    Response.Redirect("../Auction/Index.aspx");
                }
                else
                {
                    string orderId = Request.QueryString["id"];
                    List<DingDan> list_order = orderBll.GetDingDan(orderId);
                    if (list_order.Count > 0)
                    {
                        string proId = list_order[0].ProductID;
                        List<Product> list_pro = proBll.GetById(proId);
                        link_Pro.HRef = "../Auction/ProDetail.aspx?id=" + list_order[0].AuctionID + "";
                        link_Pro1.HRef = link_Pro.HRef;
                        link_Pro1.InnerText = list_pro[0].productName;
                        List<ProductImeg> list_img = proBll.GetProtductImeg("",proId);
                        if (list_img.Count > 0)
                        {
                            imgPro.ImageUrl=list_img[0].img;
                        }
                        else 
                        {
                            imgPro.ImageUrl = "";
                        }
                        imgPro.ToolTip = link_Pro1.InnerText;
                    }
                }
            }            
        }

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (!fileupload.HasFile)
            {
                ltlStatus.Text = "请选择要上传的图片";
            }
            else
            {
                if (listImg.Count > 4)
                {
                    ltlStatus.Text = "只能上传2到4张图片";
                }
                else 
                {
                    string filetype = System.IO.Path.GetExtension(fileupload.FileName).ToLower();
                    string filename = string.Empty;
                    if (filetype == ".jpg" || filetype == ".bmp" || filetype == ".jpeg" || filetype == ".gif" || filetype == ".png")
                    {
                        filename = System.Guid.NewGuid().ToString() + filetype;
                        fileupload.SaveAs(Server.MapPath("../Image/ShowOrderImg/") + filename);
                        listImg.Add(filename);
                        BindImg();
                    }
                    else
                    {
                        ltlStatus.Text = "上传的图片格式不正确";
                    }
                }                
            }
        }

        /// <summary>
        /// 晒单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (txtTitle.Text == "" || txtDetail.Text == "")
            {
                return;
            }
            else 
            {
                if (listImg.Count < 2 || listImg.Count > 4)
                {
                    ltlStatus.Text = "只能上传2到4张图片";
                }
                else 
                {
                    try
                    {
                        using (TransactionScope ts = new TransactionScope())
                        {
                            string orderId = Request.QueryString["id"];
                            Model.Entities.ShowOrder showOrder = new Model.Entities.ShowOrder();
                            showOrder.OrderID = orderId;
                            showOrder.Title = txtTitle.Text;
                            showOrder.Detail = txtDetail.Text;
                            showOrder.LoadTime = DateTime.Now;
                            showOrder.IsCheck = 0;
                            showOrder.IsRead = 0;
                            showOrder.IsShow = 0;
                            showOrderBll.AddShowOrder(showOrder);
                            List<Model.Entities.ShowOrder> list = showOrderBll.GetShowOrderbyOrderId(orderId);
                            string showOrderId = list[0].ShowOrderID;
                            string imgUrl = string.Empty;
                            for (int i = 0; i < listImg.Count; i++)
                            {
                                imgUrl = "../Image/ShowOrderImg/" + listImg[i];
                                showOrderBll.AddShowOrderImg(showOrderId, imgUrl);
                            }
                            ts.Complete();
                        }
                        MessageBox.AlertAndRedirect("晒单成功！", "../UserInfo/Biding.aspx", Page);
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                    finally 
                    {
                        for (int i = 0; i < listImg.Count; i++)
                        {
                            listImg.Remove(listImg[i]);
                        }
                    }
                }
            }
        }

        protected void lbtnDel1_Click(object sender, EventArgs e)
        {
            listImg.Remove(listImg[0]);
            BindImg();
        }

        protected void lbtnDel2_Click(object sender, EventArgs e)
        {
            listImg.Remove(listImg[1]);
            BindImg();
 
        }

        protected void lbtnDel3_Click(object sender, EventArgs e)
        {             
            listImg.Remove(listImg[2]);
            BindImg();
        }

        protected void lbtnDel4_Click(object sender, EventArgs e)
        {
            listImg.Remove(listImg[3]);
            BindImg();
        }

        /// <summary>
        /// 绑定上传的图片
        /// </summary>
        public void BindImg() 
        {            
            switch (listImg.Count)
            {
                case 0:
                    pnlImg1.Visible = false;
                    pnlImg2.Visible = false;
                    pnlImg3.Visible = false;
                    pnlImg4.Visible = false;
                    break;
                case 1:
                    img1.ImageUrl = "../Image/ShowOrderImg/" + listImg[0];
                    pnlImg1.Visible = true;
                    pnlImg2.Visible = false;
                    pnlImg3.Visible = false;
                    pnlImg4.Visible = false;
                    break;
                case 2:
                    img1.ImageUrl = "../Image/ShowOrderImg/" + listImg[0];
                    img2.ImageUrl = "../Image/ShowOrderImg/" + listImg[1];
                    pnlImg1.Visible = true;
                    pnlImg2.Visible = true;
                    pnlImg3.Visible = false;
                    pnlImg4.Visible = false;
                    break;
                case 3:
                    img1.ImageUrl = "../Image/ShowOrderImg/" + listImg[0];
                    img2.ImageUrl = "../Image/ShowOrderImg/" + listImg[1];
                    img3.ImageUrl = "../Image/ShowOrderImg/" + listImg[2];
                    pnlImg1.Visible = true;
                    pnlImg2.Visible = true;
                    pnlImg3.Visible = true;
                    pnlImg4.Visible = false;
                    break;
                case 4:
                    img1.ImageUrl = "../Image/ShowOrderImg/" + listImg[0];
                    img2.ImageUrl = "../Image/ShowOrderImg/" + listImg[1];
                    img3.ImageUrl = "../Image/ShowOrderImg/" + listImg[2];
                    img4.ImageUrl = "../Image/ShowOrderImg/" + listImg[3];
                     pnlImg1.Visible = true;
                    pnlImg2.Visible = true;
                    pnlImg3.Visible = true;
                    pnlImg4.Visible = true;
                    break;
                default:
                    pnlImg.Visible = false;
                    break;
            }
        }
    }
}