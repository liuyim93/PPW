<%@ Page Title="" Language="C#" MasterPageFile="~/Auction/UserCenter.master" AutoEventWireup="true" CodeBehind="UpdatePwd.aspx.cs" Inherits="WEB.UserInfo.UpdatePwd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content4" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content3" runat="server">
    <div class="updatepwd">
        <div class="userinfo_title">修改密码</div>
        <div class="userinfo_content1">
            <div class="updatepwd_tr"><div class="userinfo_left1">会员名：</div><div class="userinfo_right1"><asp:Label ID="lblUserName" runat="server"></asp:Label></div></div>
            <div class="updatepwd_tr"><div class="userinfo_left1">邮箱：</div><div class="userinfo_right1"><asp:Label ID="lblEmail" runat="server"></asp:Label></div></div>
            <div class="updatepwd_tr"><div class="userinfo_left1"><span style="color:Red">*</span>原密码：</div><div class="userinfo_right1"><asp:TextBox ID="txtPwd" runat="server" TextMode="Password"></asp:TextBox></div></div>
            <div class="updatepwd_tr"><div class="userinfo_left1"><span style="color:Red">*</span>新密码：</div><div class="userinfo_right1"><asp:TextBox ID="txtNewPwd" runat="server" TextMode="Password"></asp:TextBox></div></div>
            <div class="updatepwd_tr"><div class="userinfo_left1"><span style="color:Red">*</span>确认密码：</div><div class="userinfo_right1">
                <asp:TextBox ID="txtPwdConfirm" runat="server" TextMode="Password"></asp:TextBox></div></div>
            <div class="updatepwd_submit"><asp:Button ID="btnSubmit" runat="server" Text="提交" 
                    onclick="btnSubmit_Click" /></div>
        </div>
    </div>
</asp:Content>
