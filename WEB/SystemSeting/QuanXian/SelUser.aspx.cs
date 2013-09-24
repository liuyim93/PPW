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
    public partial class SelUser : BasePage
    {
        UserBll userbll = new UserBll();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                string id = Request.QueryString["id"];
                YongHu user = userbll.GetUserById(id);
                disname.Text = userbll.GetUserNameById(user.YongHuId);
                disyhm.Text = user.YHM;
                dismm.Text = EncryptionAndDecryption.Decryption(user.MM,Constant.Key);
                disjs.Text = user.JueSe == null ? "" : user.JueSe.JSMC;
                diszt.Text = ((Status)user.status).ToString();
                disbz.Text = user.BZ;
            }
        }
    }
}