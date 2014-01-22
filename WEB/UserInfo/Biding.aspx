<%@ Page Title="" Language="C#" MasterPageFile="~/Auction/UserCenter.master" AutoEventWireup="true" CodeBehind="Biding.aspx.cs" Inherits="WEB.UserInfo.Biding" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="Webdiyer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="content3" runat="server">
    <div class="biding">
        <div class="biding_title">正在参与的竞拍</div>
        <div class="biding_content">
        <asp:DataList ID="dlstBiding" runat="server" Width="100%" 
            onitemdatabound="dlstBiding_ItemDataBound" DataKeyField="AuctionID">
            <HeaderTemplate>
                <ul class="bidinghead">
                    <li style="width:300px;">商品</li>
                    <li style="width:100px;">竞拍价</li>
                    <li style="width:120px;">最后出价</li>
                    <li style="width:120px;">出价</li>
                    <li style="width:100px;">操作</li>
                </ul>
            </HeaderTemplate>            
            <ItemTemplate>
                <div class="bidingarea">
                    <div class="bidingarea_top">
                        <span style="font-weight:bold"><asp:Label ID="lblProNo" runat="server" Text='<%#Eval("Coding") %>'></asp:Label></span>&nbsp;&nbsp;
                        <asp:Label ID="lblNums" runat="server"></asp:Label>&nbsp;&nbsp;
                        拍卖时间：<asp:Label ID="lblactTime" runat="server" Text='<%#Eval("AuctionTime") %>'></asp:Label>&nbsp;&nbsp;
                    </div>
                    <ul>
                        <li class="bidingarea_img"><a href="../Auction/ProDetail.aspx?id=<%#Eval("ProductID") %>" target="_self"><asp:Image ID="img" runat="server" Width="80px" Height="80px" /></a></li>
                        <li class="bidingarea_proname"><a href="../Auction/ProDetail.aspx?id=<%#Eval("ProductID") %>" target="_self"><asp:Literal ID="ltlProName" runat="server"></asp:Literal></a><br />
                            市场价：<asp:Label ID="lblProPrice" runat="server"></asp:Label>
                        </li>
                        <li class="bidingarea_price">￥<asp:Label ID="lblactPrice" runat="server" Text='<%#Eval("AuctionPrice") %>'></asp:Label></li>
                        <li class="bidingarea_member"><asp:Label ID="lblMemberName" runat="server" Text='<%#Eval("HuiYuanID") %>'></asp:Label></li>                        
                        <li class="bidingarea_act">
                            出价：<asp:Label ID="lblBidCount" runat="server"></asp:Label>次<br />
                            <asp:Label ID="lblPointCount" runat="server"></asp:Label>
                            <asp:HiddenField ID="hfProID" runat="server" Value='<%#Eval("ProductID") %>' />
                        </li>
                        <li class="bidingarea_operate"><a href="../Auction/ProDetail.aspx?id=<%#Eval("AuctionID") %>" target="_self">前往竞拍</a></li>
                    </ul>
                </div>
            </ItemTemplate>
        </asp:DataList>
        <webdiyer:aspnetpager ID="AspNetPager1" runat="server" CssClass="paginator" CurrentPageButtonClass="cpb"
                         LastPageText="尾页" FirstPageText="首页" PrevPageText="上一页" NextPageText="下一页" 
                            AlwaysShow="true" UrlPaging="true" PageSize="10" 
                            onpagechanged="AspNetPager1_PageChanged"></webdiyer:aspnetpager> 
                            </div>
    </div>
</asp:Content>
