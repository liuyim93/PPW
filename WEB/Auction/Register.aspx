<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="WEB.Auction.Register" %>
<%@ Register Src="UserControl/Top.ascx" TagName="Top" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>用户注册</title>
    <link href="Styles/Style.css" type="text/css" rel="Stylesheet" />
    <script src="Scripts/Main.js"></script>
    <script type="text/javascript">
        /*验证邮箱*/
        function ValidateEmail() {
            var email = document.getElementById('<%=txtEmail.ClientID %>').value;
            var email_div = document.getElementById("email_div");           
            var chk = false;
            if (email == null || email == '') {
                email_div.className = 'error';
                email_div.style.display = 'block';
                email_div.innerHTML = '邮箱不能为空';
            } else {
                if (!(email.match(/^[a-z]([a-z0-9])+@([a-z0-9])+[\.][a-z]{2,3}$/))) {
                    email_div.style.display='block';
                    email_div.innerText = '邮箱格式不正确';
                    email_div.className = 'error';
                }else
                {
                    var state = WEB.Auction.Register.IsEmailAvailable(email).value;
                    if (state == "1") {
                        chk = true;
                        email_div.className = 'right';
                        email_div.style.display = 'block';
                        email_div.innerHTML = '';
                    } else {                 
                        error.innerText = '邮箱已被注册';
                        email_div.className = 'error';
                        email_div.style.display = 'block';                        
                     }
                }            
           }
        return chk;
    }

    /*检查用户名*/
    function ValidateUserName() {
        var str = document.getElementById('<%=txtUserName.ClientID %>').value;
        var username_div = document.getElementById("username_div");        
        var chk = false;
        if (str == null || str == '') {
            username_div.className = 'error';
            username_div.style.display = 'block';
            username_div.innerHTML = '用户名不能为空';            
        } else {
            var username = escape(str);
            if (!(username.match(/^[a-zA-Z]\w{3,15}$/ig))) {
                username_div.style.display = 'block';
                username_div.innerText = '用户名格式不正确';
                username_div.className = 'error';
            } else {
                var state = WEB.Auction.Register.IsEmailAvailable(username).value;
                if (state== "1") {
                    chk = true;
                    username_div.className = 'right';
                    username_div.style.display = 'block';
                    username_div.innerHTML = '';
                }
                else {
                    username_div.innerText = '用户名已经被注册';
                    username_div.style.display = 'block';
                    username_div.className = 'error';
                }
              }           
        }
        return chk;
    }

    /*用户名输入提示*/
    function remindusername() {
        var remind = document.getElementById("username_remind");
        var error = document.getElementById("username_error");
        var right = document.getElementById("username_error");
        remind.style.display = 'block';
        error.style.display = 'none';
        right.style.display = 'none';
    }

    /*邮箱输入提示*/
    function remindemail() {
        var remind = document.getElementById("email_remind");
        var error = document.getElementById("email_error");
        var right = document.getElementById("email_right");
        remind.style.display = 'block';
        error.style.display = 'none';
        right.style.display = 'none';
    }

    /*验证密码*/
    function validatepassword() {
        var password = document.getElementById('<%=txtPassword.ClientID %>').value;
        var remind = document.getElementById("password_remind");
        var error = document.getElementById("password_error");
        var right = document.getElementById("password_right");
        var chk = false;
        if (password == null || password == '') {
            remind.style.display = 'none';
            error.style.display = 'block';
            right.style.display = 'none';
            chk = false;
        } else {
        if (password.length < 6) {
            remind.style.display = 'none';
            error.style.display = 'block';
            right.style.display = 'none';
            error.innerText = '密码不能小于6个字符';
            chk = false;
        } else {
        if (password.length > 16) {
            remind.style.display = 'none';
            error.style.display = 'block';
            right.style.display = 'none';
            error.innerText = '密码不能大于16个字符';
            chk = false;
        } else {
            remind.style.display = 'none';
            error.style.display = 'none';
            right.style.display = 'block';
            chk = true;
         }
         }
}
        return chk;
    }

    /*密码提示*/
    function remindpassword() {
        var remind = document.getElementById("password_remind");
        var error = document.getElementById("password_error");
        var right = document.getElementById("password_right");
        remind.style.display = 'block';
        error.style.display = 'none';
        right.style.display = 'none';
    }

    /*验证确认密码*/
    function validatepwdconfirm() {
        var pwdconfirm = document.getElementById('<%=txtPwdConfirm.ClientID %>');
        var pwd = document.getElementById('<%=txtPassword.ClientID %>');
        var pwdconfirm_div = document.getElementById("pwdconfirm_div");
        var chk = false;
        if (pwdconfirm.value == null || pwdconfirm.value == '') {
            pwdconfirm_div.className = 'error';
            pwdconfirm_div.innerText = '请再次输入密码';
            pwdconfirm.className = 'input_error';
            pwdconfirm_div.style.display = 'block';
            chk = false;
        } else {
        if (pwdconfirm.value!= pwd.value) {
            pwdconfirm.className = 'input_error';
            pwdconfirm_div.className = 'error';
            pwdconfirm_div.innerText = '两次密码输入不一致';
            pwdconfirm_div.style.display = 'block';
            chk = false;
        } else {
            pwdconfirm_div.className = 'right';
            pwdconfirm_div.style.display = 'block';
            pwdconfirm_div.innerText = '';
            chk = true;
         }
    }
    return chk;
}

/*确认密码提示*/
function remindpwdconfirm() {
    var pwdconfirm_div = document.getElementById("pwdconfirm_div");
    pwdconfirm_div.className = 'remind';
    pwdconfirm_div.innerText = '请再次输入密码';
    pwdconfirm_div.style.display = 'block';
}

/*验证码*/
function validateverifycode() {
    var verifycode = document.getElementById('<%=txtVerifyCode.ClientID %>');
 }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:Top ID="Top1" runat="server" />
        <div class="reg">
            <div class="reg_title">
                用户注册<span>User Registration</span>
            </div>
            <div class="reg_main">
                <div class="reg_item">
                    <div class="text">用户名：</div>
                    <div class="input"><asp:TextBox id="txtUserName" runat="server" onblur="return ValidateUserName();" onfocus="remindusername();" CssClass="input_normal" ></asp:TextBox></div>
                    <div class="remark">
                        <div id="username_div" class="remind">4-15个字符，一个汉字为两个字符</div>
                    </div>
                </div>
                <div class="reg_item">
                    <div class="text">邮箱：</div>
                    <div class="input"><asp:TextBox id="txtEmail" runat="server" onblur="ValidateEmail();" onfocus="remindemail();" CssClass="input_normal"></asp:TextBox></div>
                    <div class="remark">
                        <div class="remind" id="email_div">当密码遗失时凭此邮箱找回密码</div>
                    </div>
                </div>
                <div class="reg_item">
                    <div class="text">密码：</div>
                    <div class="input"><asp:TextBox ID="txtPassword" runat="server" onblur="validatepassword();" onfocus="remindpassword();" TextMode="Password" CssClass="input_normal" ></asp:TextBox></div>
                    <div class="remark">
                        <div class="remind" id="password_div">密码应在6-16个字符内</div>
                    </div>
                </div>
                <div class="reg_item">
                    <div class="text">确认密码：</div>
                    <div class="input"><asp:TextBox ID="txtPwdConfirm" runat="server" onblur="validatepwdconfirm();" onfocus="remindpwdconfirm();" TextMode="Password" CssClass="input_normal"></asp:TextBox></div>
                    <div class="remark">
                        <div id="pwdconfirm_div"></div>
                    </div>
                </div>
                <div class="reg_item">
                    <div class="text">验证码：</div>
                    <div class="input">
                        <asp:TextBox ID="txtVerifyCode" runat="server" Width="50px" CssClass="input_normal" onblur="validateverifycode();" onfocus="remindverifycode();"></asp:TextBox>
                        <img src="" alt="" />
                        <a >看不清楚？换一张</a>
                    </div>
                    <div class="remark">
                        <div id="verifycode_div"></div>
                    </div>
                </div>
                <div class="reg_agreement"><asp:CheckBox ID="chkAgreement" runat="server" Checked="true" />&nbsp;阅读并同意服务协议书<a href="" target="_blank">《拍拍网协议书》</a></div>
                <div class="reg_submit"><asp:ImageButton ID="imgbtnReg" runat="server" 
                        ImageUrl="images/register.jpg" onclick="imgbtnReg_Click" /></div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
