<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="WEB.Auction.Register" %>
<%@ Register Src="UserControl/Top.ascx" TagName="Top" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>用户注册</title>
    <link href="Styles/Style.css" type="text/css" rel="Stylesheet" />
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
                    <div class="input"><asp:TextBox ID="txtUserName" runat="server" ></asp:TextBox></div>
                    <div class="remark">
                        <div id="username_remind" class="remind">4-15个字符，一个汉字为两个字符</div>
                        <div id="username_error" class="error">用户名不能为空！</div>
                        <div id="username_right" class="right"></div>
                    </div>
                </div>
                <div class="reg_item">
                    <div class="text">邮箱：</div>
                    <div class="input"><asp:TextBox ID="txtEmail" runat="server"></asp:TextBox></div>
                    <div class="remark">
                        <div class="remind">当密码遗失时凭此邮箱找回密码</div>
                        <div class="error">邮箱不能为空！</div>
                        <div class="right"></div>
                    </div>
                </div>
                <div class="reg_item">
                    <div class="text">密码：</div>
                    <div class="input"><asp:TextBox ID="txtPassword" runat="server"></asp:TextBox></div>
                    <div class="remark">
                        <div class="remind"></div>
                        <div class="error"></div>
                        <div class="right"></div>
                    </div>
                </div>
                <div class="reg_item">
                    <div class="text">确认密码：</div>
                    <div class="input"><asp:TextBox ID="txtPasswordConfirm" runat="server"></asp:TextBox></div>
                    <div class="remark">
                        <div class="remind"></div>
                        <div class="error"></div>
                        <div class="right"></div>
                    </div>
                </div>
                <div class="reg_item">
                    <div class="text">验证码：</div>
                    <div class="input">
                        <asp:TextBox ID="txtVerifyCode" runat="server" Width="50px"></asp:TextBox>
                        <img src="" alt="" />
                        <a >看不清楚？换一张</a>
                    </div>
                    <div class="remark">
                        <div class="remind"></div>
                        <div class="error"></div>
                        <div class="right"></div>
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
