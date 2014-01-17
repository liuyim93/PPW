using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEB.Auction.ajax
{
    public partial class LoginBox : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string str = "<div class=\"loginbox\"><div class=\"loginbox_close\"><img src=\"Images/dlg_close.gif\" onClick=\"AjaxLoginBoxClose();\" alt=\"关闭\"/></div><div class=\"loginbox_reg\"><br/><br/><h3>这么多宝贝，赶快加入抢拍吧！</h3><br/><p>您只需要花不到一分钟的时间注册，既可获得我们送出拍点体验竞拍的刺激。眼红什么宝贝？马上就开始！！！</p><br/><a href=\"../Auction/Register.aspx\" target=\"_self\"><img src=\"Images/reg.gif\" alt=\"免费注册\"/></a></div><div class=\"loginbox_login\"><p><br/>&nbsp;&nbsp;已经有拍拍网帐号？</p><br/><table><tr><td>登录名：</td><td><input type=\"text\" id=\"UserId\" onkeyup='nameChange(this.value);' onfocus='nameChange(this.value);' onblur='nameChange(this.value);'></td></tr><tr><td>密码：</td><td><input type=\"password\" id=\"Password\" onkeyup='pwdChange(this.value);' onfocus='pwdChange(this.value);' onblur='pwdChange(this.value);'></input></td><td><a href=\"\" target=\"_self\">忘记密码？</a></td></tr><tr><td></td><td><img id=\"Login\" src=\"Images/sign_in.gif\" alt=\"登录\" onclick=\"Fast_Login('_username','_password');\"></td></tr></table></div></div>"+
                "<script>function nameChange(value){$(\"#_username\").val(value);}function pwdChange(value){$(\"#_password\").val(value)}</script>";
            Response.Clear();
            Response.Write(str);
            Response.End();
        }
    }
}