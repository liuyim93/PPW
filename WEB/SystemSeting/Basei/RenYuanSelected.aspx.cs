using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using BLL.Basei;
using Model.Entities;
namespace WEB.SystemSeting.Basei
{
    //人员查看详情
    public partial class RenYuanSelected :BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack&&!X.IsAjaxRequest)
            {
                string id = Request["id"];
                DataBin(id);
            }
        }

        public void DataBin(string id) 
        {
            RenYuanBll bll = new RenYuanBll();
            List<RenYuan> list = bll.GetRenYuaSele("", "", "", id).ToList();
            txtName.Text = list[0].PersonName;
            txtEml.Text = list[0].Email;
            comSex.Value = list[0].Sex;
            txtZWM.Text = list[0].ZhiWei;
            txtSFZ.Text = list[0].SFZ;
            txtMobile.Text = list[0].Mobile;
            txtQQ.Text = list[0].QQ;
            txtBZ.Text = list[0].Remark;
        }
    }
}