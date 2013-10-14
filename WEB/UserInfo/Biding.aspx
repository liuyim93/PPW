<%@ Page Title="" Language="C#" MasterPageFile="~/Auction/UserCenter.master" AutoEventWireup="true" CodeBehind="Biding.aspx.cs" Inherits="WEB.UserInfo.Biding" %>
<asp:Content ID="Content1" ContentPlaceHolderID="content3" runat="server">
    <div class="biding">
        <div class="biding_title">正在参与的竞拍</div>
        <asp:DataList ID="dlstBiding" runat="server" Width="100%">
            <ItemTemplate>
                <div class="bidingarea">
                    <div class="bidingarea_top">
                        <asp:Label ID="lblProNo" runat="server" Text='<%#Eval("coding") %>'></asp:Label>&nbsp;&nbsp;
                        <asp:Label ID="lblNums" runat="server"></asp:Label>&nbsp;&nbsp;
                        <asp:Label ID="lblactTime" runat="server" Text='<%#Eval("AuctionTime") %>'></asp:Label>&nbsp;&nbsp;
                    </div>
                    <ul>
                        <li class="bidingarea_img"><a href="" target="_self"><asp:Image ID="img" runat="server" Width="" Height="" /></a></li>
                        <li class="bidingarea_proname"><a href="" target="_self"><%#Eval("productName") %></a><br />
                            市场价：<asp:Label ID="lblProPrice" runat="server" Text='<%#Eval("productPrice") %>'></asp:Label>
                        </li>
                        <li><asp:Label ID="lblactPrice" runat="server" Text='<%#Eval("PmJGproduct") %>'></asp:Label></li>
                        <li><asp:Label ID="lblMemberName" runat="server" Text='<%#Eval("HuiYuanID") %>'></asp:Label></li>                        
                        <li>
                            出价：<asp:Label ID="lblBidCount" runat="server"></asp:Label>次<br />
                            <asp:Label ID="lblPointCount" runat="server"></asp:Label>
                            <asp:HiddenField ID="hfProID" runat="server" Value='<%#Eval("ProductID") %>' />
                        </li>
                        <li><a href="" target="_self">前往竞拍</a></li>
                    </ul>
                </div>
            </ItemTemplate>
        </asp:DataList>
    </div>
</asp:Content>
