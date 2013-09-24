using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using BLL.Home;
using Model.Entities;
using Tools;
namespace WEB.SystemSeting.QuanXian
{
    public partial class SelectJiaoSe : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                string id = Request["id"].ToString();
                JiaoSeBll JSbll = new JiaoSeBll();
               JueSe JS= JSbll.SelectById(id);
               disJSMC.Text = JS.JSMC;
               disBZ.Text = JS.Remark;
               disZT.Text = ((Status)JS.status).ToString();
            }
        }
    }
}