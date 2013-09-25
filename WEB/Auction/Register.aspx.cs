using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL.SystemSeting;
using Model;

namespace WEB.Auction
{
    public partial class Register : System.Web.UI.Page
    {
        HuiYuanXinXiBll hyBll = new HuiYuanXinXiBll();
        protected void Page_Load(object sender, EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(Register));
        }
        //验证邮箱
        [AjaxPro.AjaxMethod]
        public string IsEmailAvailable(string email)
        {
            object obj = hyBll.IsEmailAvailable(email);
            if (obj == null)
            {
                return "1";
            }
            else 
            {
                return "-1";
            }
        }
        //验证用户名是否可用
        [AjaxPro.AjaxMethod]
        public string IsUserNameAvailable(string username) 
        {
            object obj = hyBll.IsUserNameAvailable(username);
            if (obj == null)
            {
                return "1";
            }
            else 
            {
                return "-1";
            }
        }
        //注册
        protected void imgbtnReg_Click(object sender, ImageClickEventArgs e)
        {

        }
    }
}