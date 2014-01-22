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
using System.Data;

namespace WEB.UserInfo
{
    public partial class AuctionOrder : System.Web.UI.Page
    {
        ShouHuoDZBll adressBll = new ShouHuoDZBll();
        DingDanBll orderBll = new DingDanBll();
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
            if (Session["HuiYuanID"] == null || Session["HuiYuanName"] == null)
            {
                Response.Redirect("../Auction/Index.aspx");
            }
            else 
            {
                if (Request.QueryString["type"]!="")
                {
                    int type = Convert.ToInt32(Request.QueryString["type"]);
                    if (type==0)
                    {
                        BindAdress();
                    }
                    BindOrder();
                }
            }
        }

        public void BindAdress() 
        {
            string hyId=Session["HuiYuanID"].ToString();
            dlstShipAdress.DataSource = adressBll.GetShouHuoDZbyhyId(hyId);
            dlstShipAdress.DataBind();
        }

        public void BindOrder() 
        {
            string hyId=Session["HuiYuanID"].ToString();
            int status;
            string type=Request.QueryString["type"];
            string orderType = "竞拍订单";
            if (type != "0" && type != "1" && type != "2")
            {
                Response.Redirect("../Auction/Index.aspx");
            }
            else 
            {
                if (type =="0")
                {
                    status = 10;
                    pnlShipAdress.Visible = true;
                    pnlAddAdress.Visible = false;
                    pnlAdressList.Visible = true;
                }
                else 
                {
                    if (type == "1")
                    {
                        status = 8;
                        pnlShipAdress.Visible = false;
                    }
                    else 
                    {
                        status = 11;
                        pnlShipAdress.Visible = false;
                    }
                }
                DataTable dt = orderBll.getDingDanbyhyId(hyId,status,orderType);
                if(dt.Rows.Count>0){
                    AspNetPager1.RecordCount = dt.Rows.Count;
                    PagedDataSource pds = new PagedDataSource();
                    pds.DataSource = dt.DefaultView;
                    pds.AllowPaging = true;
                    pds.PageSize = AspNetPager1.PageSize;
                    pds.CurrentPageIndex = AspNetPager1.CurrentPageIndex - 1;
                    dlstOrderList.DataSource = pds;
                    dlstOrderList.DataBind();
                }
            }
        }

        /// <summary>
        /// 取消添加收货地址
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearTextBox();
        }

        /// <summary>
        /// 保存收货地址
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (txtAdress.Text == "" || txtname.Text == "" || txtPhone.Text == "" || txtPostCode.Text == "")
            {
                return;
            }
            else 
            {                                
                using (TransactionScope ts=new TransactionScope())
                {
                    string hyId = Session["HuiYuanID"].ToString();
                    if (adressBll.GetShouHuoDZbyhyId(hyId).Count>0)
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

        protected void rbtnAddAdress_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnAddAdress.Checked==true)
            {
                pnlAddAdress.Visible = true;
                pnlAdressList.Visible = false;
            }
        }

        protected void dlstOrderList_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType==ListItemType.Item||e.Item.ItemType==ListItemType.AlternatingItem)
            {
                Image img = e.Item.FindControl("img") as Image;
                HyperLink proName = e.Item.FindControl("hlnkProName") as HyperLink;
                Label status = e.Item.FindControl("lblStatus") as Label;
                string proId = dlstOrderList.DataKeys[e.Item.ItemIndex].ToString();
                proName.Text=proBll.GetById(proId)[0].productName;
                List<ProductImeg> list_img = proBll.GetProtductImeg("",proId);
                if (list_img.Count>0)
                {
                    img.ImageUrl=list_img[0].img;
                }
                switch (Convert.ToInt32(status.Text))
                {
                    case 10:
                        status.Text = "未付款";
                        break;
                    case 11:
                        status.Text = "已取消";
                        break;
                    case 8:
                        status.Text ="待发货";
                        break;
                    case 9:
                        status.Text = "已发货";
                        break;
                    case 7:
                        status.Text = "交易成功";
                        break;
                    default:
                        status.Text = "";
                        break;
                }
            }
        }

        protected void dlstShipAdress_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType==ListItemType.Item||e.Item.ItemType==ListItemType.AlternatingItem)
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

        protected void dlstOrderList_ItemCommand(object source, DataListCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "show":
                    string orderId = e.CommandArgument.ToString();
                    Response.Redirect("../UserInfo/ShowOrder.aspx?id="+orderId);
                    break;
                default:
                    break;
            }
        }

        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            BindOrder();
        }
    }
}