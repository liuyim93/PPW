<%@ Page Title="" Language="C#" MasterPageFile="~/Auction/UserCenter.master" AutoEventWireup="true" CodeBehind="Bided.aspx.cs" Inherits="WEB.UserInfo.Bided" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="Webdiyer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="content3" runat="server">
    <div class="biding">
        <div class="biding_title">竞拍历史</div>
        <div class="biding_content">
        <asp:DataList ID="dlstBided" runat="server" Width="100%" 
            onitemdatabound="dlstBided_ItemDataBound" DataKeyField="ProductID" 
                onitemcommand="dlstBided_ItemCommand">
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
                        <span style="font-weight:bold">第<asp:Label ID="lblProNo" runat="server" Text='<%#Eval("Coding") %>'></asp:Label>期</span>&nbsp;&nbsp;
                        共有<asp:Label ID="lblNums" runat="server"></asp:Label>人参与&nbsp;&nbsp;
                        结束时间：<asp:Label ID="lblEndTime" runat="server" Text='<%#Eval("EndTime") %>'></asp:Label>&nbsp;&nbsp;
                        补差价截止日期：<asp:Label ID="lblFullTime" runat="server"></asp:Label>
                        <asp:HiddenField ID="hfAuctionID" runat="server" Value='<%#Eval("AuctionID") %>' />
                    </div>
                    <ul>
                        <li class="bidingarea_img"><a href="../Auction/ProDetail.aspx?id=<%#Eval("AuctionID") %>" target="_self"><asp:Image ID="img" runat="server" Width="80px" height="80px" /></a></li> 
                        <li class="bidingarea_proname">
                            <a href="../Auction/ProDetail.aspx?id=<%#Eval("AuctionID") %>" target="_self"><asp:Literal ID="ltlProName" runat="server"></asp:Literal></a><br />
                            市场价：<asp:Label ID="lblPrice" runat="server"></asp:Label>
                        </li>
                        <li class="bidingarea_price"><asp:Label ID="lblactPrice" runat="server" Text='<%#Eval("AuctionPrice") %>'></asp:Label></li>
                        <li class="bidingarea_member"><asp:Label ID="lblMemberName" runat="server" Text='<%#Eval("HuiYuanID") %>'></asp:Label></li>
                        <li class="bidingarea_act">
                            出价：<asp:Label ID="lblBidCount" runat="server"></asp:Label>次<br />
                            <asp:Label ID="lblPointCount" runat="server"></asp:Label>
                        </li>
                        <li class="bidedarea_operate">
                            <asp:LinkButton ID="lbtnBuy" runat="server" CommandName="buy" CommandArgument='<%#Eval("AuctionID") %>'>补差价购买</asp:LinkButton>
                            <asp:Label ID="lblTimeOut" runat="server" Text="超过3天不能再补差价购买了" Visible="false"></asp:Label>                            
                        </li>
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
