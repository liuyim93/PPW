using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL.SystemSeting;
using Model.Entities;
using Tools;
using BLL;
using System.Transactions;


namespace WEB.UserInfo
{
    public partial class ShipAdress : System.Web.UI.Page
    {
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
                gvwAdress.DataSource = adressBll.GetShouHuoDZbyhyId(hyId);
                gvwAdress.DataBind();
            }
        }

        protected void lbtnAdressList_Click(object sender, EventArgs e)
        {
            pnlAdressList.Visible = true;
            pnlUpdateAdress.Visible = false;
        }

        protected void lbtnAddAdress_Click(object sender, EventArgs e)
        {
            pnlUpdateAdress.Visible = true;
            pnlAdressList.Visible = false;
        }

        protected void gvwAdress_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "change":
                    string adressId = e.CommandArgument.ToString();
                    ShouHuoDZ adress = adressBll.GetShouHuoDZ(adressId)[0];
                    txtName.Text = adress.ShouHuoName;
                    txtPhone.Text = adress.Mode;
                    txtPostCode.Text = adress.YouBian;
                    txtRemark.Text = adress.Remark;
                    txtAdress.Text = adress.DZ;
                    hfAdressID.Value = adressId;
                    hfSelect.Value = adress.IsSelect.ToString();
                    pnlAdressList.Visible = false;
                    pnlUpdateAdress.Visible = true;
                    break;
                case "del":
                    string adrId = e.CommandArgument.ToString();
                    try
                    {
                        adressBll.DelShouHuoDZ(adrId);
                        Bind();
                    }
                    catch (Exception)
                    {

                        MessageBox.Alert("删除失败",Page);
                    }
                    break;
                default:
                    break;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (txtAdress.Text == "" || txtName.Text == "" || txtPhone.Text == "" || txtPostCode.Text == "")
            {
                return;
            }
            else 
            {
                string hyId = Session["HuiYuanID"].ToString();
                ShouHuoDZ adress = new ShouHuoDZ();                
                adress.DZ = txtAdress.Text;
                adress.HuiYuanID = hyId;
                adress.Remark = txtRemark.Text;
                adress.ShouHuoName = txtName.Text;
                adress.YouBian = txtPostCode.Text;
                adress.Mode = txtPhone.Text;
                adress.CreateTime = DateTime.Now;
                if (hfAdressID.Value == "")
                {
                    using (TransactionScope ts = new TransactionScope())
                    {                        
                        if (adressBll.GetShouHuoDZbyhyId(hyId).Count > 0)
                        {
                            adressBll.UpdateStatusbyhyId(hyId, 0);
                        }
                        adress.IsSelect = 1;
                        adressBll.AddShouHuoDZ(adress);
                        ts.Complete();
                    }
                    ClearTextBox();
                    Bind();
                }
                else 
                {
                    adress.IsSelect = Convert.ToInt32(hfSelect.Value);
                    adress.ShouHuoDZID = hfAdressID.Value;
                    try
                    {
                        adressBll.UpdateShouHuoDZ(adress);
                        ClearTextBox();
                        Bind();
                    }
                    catch (Exception)
                    {

                        MessageBox.Alert("修改失败",Page);
                    }
                }
            }
        }

        /// <summary>
        /// 清空文本框
        /// </summary>
        public void ClearTextBox() 
        {
            txtName.Text = "";
            txtAdress.Text = "";
            txtPhone.Text = "";
            txtPostCode.Text = "";
            txtRemark.Text = "";
            hfAdressID.Value = "";
            pnlAdressList.Visible = true;
            pnlUpdateAdress.Visible = false;
        }
    }
}