<%@ Page Title="" Language="C#" MasterPageFile="~/Auction/Main.Master" AutoEventWireup="true" CodeBehind="ShowOrder.aspx.cs" Inherits="WEB.Auction.ShowOrder" %>
<%@ Register Src="UserControl/Recommend.ascx" TagName="Recommend" TagPrefix="uc1" %><%@ Register Src="UserControl/Last.ascx" TagName="Last" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="ShowOrder">
        <div class="ShowOrder_left">
            <div class="ShowOrder_left_title">拍客晒图<span>&nbsp;ShowOrder</span></div>
            <div class="ShowOrder_left_content">
                <asp:DataList ID="dlstShowOrder" runat="server" Width="100%" 
                    onitemdatabound="dlstShowOrder_ItemDataBound" DataKeyField="OrderID" >
                    <ItemTemplate>
                        <div class="ShowOrder_area">
                            <div class="ShowOrder_area_title"><asp:HyperLink ID="hlnkPro" runat="server"><%#Eval("Title") %></asp:HyperLink></div>
                            <div class="ShowOrder_area_info">成交价:<asp:Label ID="lblDonePrice" runat="server"></asp:Label>&nbsp;&nbsp;市场价：<asp:Label ID="lblMarketPrice" runat="server"></asp:Label>&nbsp;&nbsp;晒单时间：<asp:Literal ID="ltlLoadTime" runat="server" Text='<%#Eval("LoadTime") %>'></asp:Literal>&nbsp;&nbsp;奖励<asp:Label ID="lblPoints" runat="server" Text='<%#Eval("Points") %>'></asp:Label>积分</div>
                            <div class="ShowOrder_area_pic">
                                <ul>
                                    <li class="ShowOrder_area_avatar"><asp:Image ID="imgAvatar" runat="server" Width="70px" Height="70px" ImageUrl="../Image/default_avatar.gif" /><br /><div style="color:#999999;text-align:center;margin-top:3px; width:70px;"><asp:Literal ID="ltlMemberName" runat="server"></asp:Literal></div></li>
                                    <li class="ShowOrder_area_detail"><asp:Literal ID="ltlDetail" runat="server" Text='<%#Eval("Detail") %>'></asp:Literal></li>
                                    <li class="ShowOrder_area_img">
                                        <asp:Image ID="img1" runat="server" Width="70px" Height="70px" Visible="false" />&nbsp;&nbsp;<asp:Image ID="img2" runat="server" width="70px" Height="70px" Visible="false" />&nbsp;&nbsp;
                                        <asp:Image ID="img3" runat="server" Width="70px" Height="70px" Visible="false" />&nbsp;&nbsp;<asp:Image ID="img4" runat="server" Width="70px" Height="70px" Visible="false" />
                                    </li>
                                </ul>
                            </div>
                            <div class="ShowOrder_area_reply"><span style="color:#8f0100;font-weight:bold;">回复:&nbsp;</span><asp:Literal ID="ltlReply" runat="server" Text='<%#Eval("Reply") %>'></asp:Literal></div><asp:HiddenField ID="hfShowOrderID" runat="server" Value='<%#Eval("ShowOrderID") %>'/>                                                           
                        </div>
                    </ItemTemplate>
                </asp:DataList>
            </div>
        </div>
        <div class="auctionlist_right">
            <div class="auctionlist_right_reg"><a href="../Auction/Register.aspx" target="_self"><img src="../Auction/Images/register_ad.jpg" width="190px" height="160px" border="0" /></a></div>
            <div class="auctionlist_right_guide"><a href="" target="_self"><img src="../Auction/Images/fresh_guide.jpg" width="190px" height="160px" border="0" /></a></div>
            <uc1:Recommend ID="recommend1" runat="server" />
            <uc2:Last ID="last1" runat="server" />
        </div>
    </div>
</asp:Content>
