<%@ Page Title="" Language="C#" MasterPageFile="~/Auction/UserCenter.master" AutoEventWireup="true" CodeBehind="Bided.aspx.cs" Inherits="WEB.UserInfo.Bided" %>
<asp:Content ID="Content1" ContentPlaceHolderID="content3" runat="server">
    <div class="biding">
        <div class="biding_title">竞拍历史</div>
        <asp:DataList ID="dlstBided" runat="server" Width="100%" 
            onitemdatabound="dlstBided_ItemDataBound" DataKeyField="ProductID">
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
                        <span style="font-weight:bold">第<asp:Label ID="lblProNo" runat="server" Text='<%#Eval("coding") %>'></asp:Label>期</span>&nbsp;&nbsp;
                        共有<asp:Label ID="lblNums" runat="server"></asp:Label>人参与&nbsp;&nbsp;
                        结束时间：<asp:Label ID="lblEndTime" runat="server" Text='<%#Eval("EndTime") %>'></asp:Label>&nbsp;&nbsp;
                        补差价截止日期：<asp:Label ID="lblFullTime" runat="server"></asp:Label>
                    </div>
                    <ul>
                        <li class="bidingarea_img"><a href="ProDetail.aspx?id=<%#Eval("ProductID") %>" target="_self"><asp:Image ID="img" runat="server" Width="80px" height="80px" /></a></li> 
                        <li class="bidingarea_proname">
                            <a href="ProDetail.aspx?id=<%#Eval("ProductID") %>" target="_self"><%#Eval("productName") %></a><br />
                            市场价：<asp:Label ID="lblPrice" runat="server" Text='<%#Eval("productPrice") %>'></asp:Label>
                        </li>
                        <li class="bidingarea_price"><asp:Label ID="lblactPrice" runat="server" Text='<%#Eval("PmJGproduct") %>'></asp:Label></li>
                        <li class="bidingarea_member"><asp:Label ID="lblMemberName" runat="server" Text='<%#Eval("HuiYuanID") %>'></asp:Label></li>
                        <li class="bidingarea_act">
                            出价：<asp:Label ID="lblBidCount" runat="server"></asp:Label>次<br />
                            <asp:Label ID="lblPointCount" runat="server"></asp:Label>
                        </li>
                        <li class="bidedarea_operate">
                            <asp:HyperLink ID="hlnkBuy" runat="server" Visible="false">补差价购买</asp:HyperLink>
                            <asp:Label ID="lblTimeOut" runat="server" Text="超过3天不能再补差价购买了" Visible="false"></asp:Label>                            
                        </li>
                    </ul>
                </div>
            </ItemTemplate>
        </asp:DataList>
    </div>
</asp:Content>
