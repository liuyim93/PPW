<%@ Page Title="" Language="C#" MasterPageFile="~/Auction/UserCenter.master" AutoEventWireup="true" CodeBehind="UserInfo.aspx.cs" Inherits="WEB.UserInfo.UserInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content4" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content3" runat="server">
    <div id="userinfo">
        <div class="userinfo_title">个人资料</div>
        <div class="userinfo_content1">           
            <ul>                
                <li><div class="userinfo_left1">用户名：</div><div class="userinfo_right1"><asp:Label ID="lblUserName" runat="server"></asp:Label></div></li>
               <li> <div class="userinfo_left1">真实姓名：</div><div class="userinfo_right1"><asp:TextBox ID="txtRealName" runat="server"></asp:TextBox></div></li>
               <li> <div class="userinfo_left1">性别：</div><div class="userinfo_right1"><asp:RadioButtonList ID="rblSex" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="男" Value="男"></asp:ListItem>
                        <asp:ListItem Text="女" Value="女" Selected="True"></asp:ListItem>
                      </asp:RadioButtonList></div>
               <li><div class="userinfo_left1">手机：</div><div class="userinfo_right1"><asp:TextBox ID="txtPhone" runat="server"></asp:TextBox></div></li>
              <li> <div class="userinfo_left1">邮箱：</div><div class="userinfo_right1"><asp:Label ID="lblEmail" runat="server"></asp:Label>&nbsp;&nbsp;<font color="green"><asp:Label ID="lblEmailStatus" runat="server" Visible="false"></asp:Label></font></div></li>
              <li> <div class="userinfo_adress" style="width:200px;text-align:right;">住址：</div><div style="float:left;width:500px;height:70px;"><asp:TextBox ID="txtAdress" runat="server" TextMode="MultiLine"></asp:TextBox></div></li>
               <li style="padding-left:200px;float:left;"><asp:Button ID="btnSubmit" runat="server" Text="确定" onclick="btnSubmit_Click" />&nbsp;&nbsp;<asp:Button 
                    ID="btnReset" runat="server" Text="重置" onclick="btnReset_Click" /></li>
            </ul>
        </div>
    </div>
</asp:Content>
