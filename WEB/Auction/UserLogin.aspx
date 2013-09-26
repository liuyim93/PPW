<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserLogin.aspx.cs" Inherits="WEB.Auction.UserLogin" %>
<%@ Register Src="UserControl/Top.ascx" TagName="Top" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>用户登录</title>
    <link href="Styles/Style.css" type="text/css" rel="Stylesheet" />
    <script type="text/javascript">
        /*验证用户名*/
        function checkusername() {
            var username = document.getElementById('<%=txtUserName.ClientID %>').value;
            var username_error = document.getElementById("username_error");
            var chk = false;
            if (username == null || username === '') {
                username_error.className = "remind";
                username_error.style.display = 'block';
                username_error.innerHTML = '请输入用户名';
            } else {
                chk = true;
                username_error.style.display = 'none';
            }
            return chk;
        }
        /*验证密码非空*/
        function checkpassword() {
            var password = document.getElementById('<%=txtPassword.ClientID %>').value;
            var error = document.getElementById("password_error");
            var chk = false;
            if (password == null || password == '') {
                error.className = 'remind';
                error.style.display = 'block';
                error.innerHTML = '请输入密码';
            } else {
                chk = true;
                error.style.display = 'none';
            }
            return chk;
        }
        /*验证码*/
        function validatecode() {
            var checkcode = document.getElementById('<%=txtCheckCode.ClientID %>').value;
            var error = document.getElementById("checkcode_error");
            var chk = true;
            if (checkcode == null || checkcode == '') {
                error.innerHTML = '请输入验证码';
                error.className = 'remind';
                chk = false;
            } else {
            if (checkcode.length < 4) {
                error.innerHTML = '请输入正确的验证码';
                error.className = 'error';
                chk = false;
            } else {
                chk = true;
                error.style.display = 'none';
             }
        }
        return chk;
    }
    /*更换验证码*/
    function changecode() {
        var checkcode = document.getElementById("imgCheckCode");
        checkcode.src = checkcode.src + '?';
    }
    /*提交登录表单*/
    function validateform() {
        var v_username = checkusername();
        var v_password = checkpassword();
        var pnlcheckcode = document.getElementById("pnlCheckCode");
        if (pnlcheckcode==null && v_password && v_username) {
            return true;
        } else {
            var v_checkcode = validatecode();
            if (v_checkcode && v_username && v_password) {
                return true;
            } else {
                return false;
             }
         }       
     }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:Top ID="Top1" runat="server" />
        <div class="reg">
            <div class="reg_title">用户登录<span>User Login</span></div>
            <div class="reg_main">
                <div class="reg_item">
                    <div class="text">用户名：</div>
                    <div class="input"><asp:TextBox ID="txtUserName" runat="server" onblur="checkusername();" CssClass="input_normal"></asp:TextBox></div>
                    <div id="username_error"></div>
                </div>
                <div class="reg_item">
                    <div class="text">密码：</div>
                    <div class="input"><asp:TextBox ID="txtPassword" runat="server" onblur="checkpassword();" CssClass="input_normal" TextMode="Password"></asp:TextBox></div>
                    <div id="password_error"></div>
                </div>
                <asp:Panel ID="pnlCheckCode" runat="server" Visible="false">
                    <div class="reg_item">
                    <div class="text">验证码：</div>
                    <div class="input">
                        <asp:TextBox ID="txtCheckCode" runat="server" onblur="" Width="50px" CssClass="input_normal"></asp:TextBox>&nbsp;
                        <img id="imgCheckCode" src="VerifyCode.aspx" alt="看不清，换一张" onclick="validatecode()" style="cursor:hand" />&nbsp;
                        <a onclick="changecode();" style="text-decoration:none; color:#3366cc; cursor:hand;">看不清？换一张</a>
                    </div>
                    <div id="checkcode_error"></div>
                </div>
                </asp:Panel>                
                <div class="reg_agreement">
                    <div>
                        <asp:CheckBox ID="chkRememberName" runat="server" Text="记住用户名" />&nbsp;
                        <a href="#" target="_self">忘记密码？</a>
                    </div>
                </div>
                <div class="reg_submit">
                    <asp:ImageButton ID="imgbtnLogin" runat="server" ImageUrl="" 
                        OnClientClick="return validateform();" onclick="imgbtnLogin_Click" />
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
