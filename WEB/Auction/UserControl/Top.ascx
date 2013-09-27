<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Top.ascx.cs" Inherits="WEB.Auction.UserControl.Top" %>
 <div class="top">
            <asp:Panel ID="Panel1" runat="server">
                 <div class="top_login">欢迎来到拍拍网！&nbsp;<a href="UserLogin.aspx" target="_self">[登录]</a>&nbsp;<a href="Register.aspx" target="_self">[免费注册]</a></div>
            </asp:Panel>
           <asp:Panel ID="Panel2" runat="server" Visible="false">
                <div class="top_login">您好！&nbsp;<asp:HyperLink ID="hlnkUserName" runat="server"></asp:HyperLink>&nbsp;<a href="#" target="_self">个人中心</a>&nbsp;<asp:LinkButton 
                        ID="lbtnLoginOut" runat="server" Text="安全退出" onclick="lbtnLoginOut_Click"></asp:LinkButton></div>
           </asp:Panel>
            <div class="top_right"><a href="">设为首页</a>|<a>加入收藏</a>|<a href="" target="_self">帮助中心</a></div>
        </div>
        <div>
            <div class="header">
                <!--LOGO-->
                <div class="logo"><a href="" target="_self"><img src="" alt="" width="400px" height="70px" /></a></div>
                <!--搜索-->
                <div class="search"><asp:TextBox ID="txtSearch" runat="server"></asp:TextBox><asp:ImageButton ID="imgbtnSearch" runat="server" ImageUrl="" Width="50px" Height="20px" /><br />
                    <span>热门搜索：<a href="" target="_self">手机</a>&nbsp;<a href="" target="_self">笔记本电脑</a></span>
                </div>
                <div>
                    
                </div>
            </div>
            <div class="top_menu">
                <ul>
                    <li><a href="index.aspx" target="_self">首页</a></li>
                    <li><a href="" target="_self">正在热拍</a></li>
                    <li><a href="" target="_self">即将竞拍</a></li>
                    <li><a href="" target="_self">历史竞拍</a></li>
                    <li><a href="" target="_self">免费体验</a></li>
                    <li><a href="" target="_self">积分商城</a></li>
                    <li><a href="" target="_self">拍客晒图</a></li>
                </ul>
            </div>
          </div>