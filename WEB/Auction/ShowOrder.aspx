<%@ Page Title="" Language="C#" MasterPageFile="~/Auction/Main.Master" AutoEventWireup="true" CodeBehind="ShowOrder.aspx.cs" Inherits="WEB.Auction.ShowOrder" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <div>
            <div>拍客晒图</div>
            <div>
                <asp:DataList ID="dlstShowOrder" runat="server" Width="100%" 
                    onitemdatabound="dlstShowOrder_ItemDataBound" DataKeyField="OrderID" >
                    <ItemTemplate>
                        <div>
                            <div><asp:HyperLink ID="hlnkPro" runat="server"><%#Eval("Title") %></asp:HyperLink></div>
                            <div>成交价:<asp:Label ID="lblDonePrice" runat="server"></asp:Label>&nbsp;&nbsp;市场价：<asp:Label ID="lblMarketPrice" runat="server"></asp:Label>&nbsp;&nbsp;晒单时间：<asp:Literal ID="ltlLoadTime" runat="server" Text='<%#Eval("LoadTime") %>'></asp:Literal>&nbsp;&nbsp;奖励<asp:Label ID="lblPoints" runat="server" Text='<%#Eval("Points") %>'></asp:Label>积分</div>
                            <div>
                                <ul>
                                    <li><asp:Image ID="imgAvatar" runat="server" Width="70px" Height="70px" /><br /><asp:Literal ID="ltlMemberName" runat="server"></asp:Literal></li>
                                    <li><asp:Literal ID="ltlDetail" runat="server"></asp:Literal></li>
                                    <li>
                                        <asp:Image ID="img1" runat="server" Width="" Height="" />&nbsp;<asp:Image ID="img2" runat="server" width="" Height="" />&nbsp;
                                        <asp:Image ID="img3" runat="server" Width="" Height="" />&nbsp;<asp:Image ID="img4" runat="server" Width="" Height="" />
                                    </li>
                                    <li>回复:<asp:Literal ID="ltlReply" runat="server" Text='<%#Eval("Reply") %>'></asp:Literal></li><asp:HiddenField ID="hfShowOrderID" runat="server" Value='<%#Eval("ShowOrderID") %>'/>
                                </ul>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:DataList>
            </div>
        </div>
        <div>
            
        </div>
    </div>
</asp:Content>
