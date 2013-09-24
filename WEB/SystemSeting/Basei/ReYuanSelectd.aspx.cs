using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using Model.Entities;
using BLL.Basei;
namespace WEB.SystemSeting.Basei
{
    //人员通用选择表
    public partial class ReYuanSelectd :BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest && !IsPostBack)
            {
                Session[Request["key"]] = null;
                if (Request["type"]=="m")
                {
                    ((CheckboxSelectionModel)GridPanel1.SelectionModel[0]).SingleSelect = false;
                }
                DataBin();
            }
        }
          
        //绑定人员表
        public void DataBin() 
        {
            string name = txtXM.Text;
            string sex = comXB.SelectedItem.Value;
            string zw = txtZW.Text;
            RenYuanBll bll = new RenYuanBll();
            List<RenYuan> list=  bll.GetRenYuaSele(name,sex,zw,"");
            ReYuanList.DataSource = list.Select(x => new 
            {
                RenYuanId=x.RenYuanId,
                PersonName=x.PersonName,
                Sex=x.Sex,
                ZhiWei=x.ZhiWei,
                SFZ=x.SFZ,
                Email=x.Email,
                Mobile=x.Mobile
            });
            ReYuanList.DataBind();
        }

        protected void SelChange(object sender, DirectEventArgs e)
        {
            List<string> list = new List<string>();
            foreach (var item in ((CheckboxSelectionModel)GridPanel1.SelectionModel[0]).SelectedRows)
            {
                list.Add(item.RecordID);
            }
            Session[Request["key"]] = list;
        }

        public void Ref(object obj,StoreRefreshDataEventArgs e)
        {
            DataBin();
        }

    }
}