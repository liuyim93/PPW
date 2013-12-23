<%@ Page Title="" Language="C#" MasterPageFile="~/Auction/Main.Master" AutoEventWireup="true" CodeBehind="FutureAuction.aspx.cs" Inherits="WEB.Auction.FutureAuction" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="Webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="pointsmall">
        <div class="pointsmall_title">即将竞拍
        </div>
        <div class="future_content">
            <asp:Repeater ID="repeater1" runat="server" 
                onitemdatabound="repeater1_ItemDataBound">
                <HeaderTemplate>
                    <table id="tab_head">
                        <tr>
                            <td  style="width:530px;padding-left:40px;">商品</td>
                            <td  style="width:150px;text-align:center;">起拍价</td>
                            <td style="width:170px;text-align:center;">开始时间</td>
                        </tr>
                    </table>
                </HeaderTemplate>
                <ItemTemplate>
                    <div class="future_area" onmouseover="this.className='future_area_current'" onmouseout="this.className='future_area'">
                        <ul>
                            <li class="future_area_img"><a href="ProDetail.aspx?id=<%#Eval("AuctionID") %>" target="_self"><asp:Image  ID="imgPro" runat="server" Width="100px" Height="100px" /></a></li>
                            <li class="future_area_name">
                                <a href="ProDetail.aspx?id=<%#Eval("AuctionID") %>" target="_self">第<%#Eval("Coding") %>期&nbsp;<asp:Literal ID="ltlProName" runat="server" Text='<%#Eval("ProductID") %>'></asp:Literal>&nbsp;</a><br />
                                <asp:Label ID="lblProIntro" runat="server"></asp:Label><br />
                                市场价：<span style="color:#ff6600;font-weight:bold;">￥<asp:Literal ID="ltlProPrice" runat="server"></asp:Literal></span>
                            </li>
                            <li class="future_area_price">￥0</li>
                            <li class="future_area_time"><asp:Label ID="lblAuctionTime" runat="server" Text='<%#Eval("AuctionTime") %>'></asp:Label></li>
                        </ul>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
                <webdiyer:aspnetpager ID="AspNetPager1" runat="server" CssClass="paginator" CurrentPageButtonClass="cpb"
                         LastPageText="尾页" FirstPageText="首页" PrevPageText="上一页" NextPageText="下一页" 
                            AlwaysShow="true" UrlPaging="true" PageSize="9" 
                            onpagechanged="AspNetPager1_PageChanged"></webdiyer:aspnetpager>                                        
        </div>
    </div>
</asp:Content>
