<%@ Page Title="" Language="C#" MasterPageFile="~/Auction/Main.Master" AutoEventWireup="true" CodeBehind="AuctionHistory.aspx.cs" Inherits="WEB.Auction.AuctionHistory" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="Webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="history">
        <div class="history_title">历史竞拍&nbsp;<span style="color:#ccc;font-size:12px;">Auction History</span></div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
          <ContentTemplate>
            <div class="history_search">
                竞拍类型：
                <asp:DropDownList ID="dropAuctionType" runat="server"></asp:DropDownList>&nbsp;
                拍品类型：<asp:DropDownList ID="dropProductType" runat="server"></asp:DropDownList>&nbsp;
                拍品价格：
                <asp:DropDownList ID="dropProductPrice" runat="server">
                    <asp:ListItem Text="不限" Value="-1" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="200元以下" Value="0-200"></asp:ListItem>
                    <asp:ListItem Text="200-500元" Value="200-500"></asp:ListItem>
                    <asp:ListItem Text="500-1000元" Value="500-1000"></asp:ListItem>
                    <asp:ListItem Text="1000元以上" Value="1000-"></asp:ListItem>
                </asp:DropDownList>&nbsp;
                成交价格：
                <asp:DropDownList ID="dropAuctionPrice" runat="server">
                    <asp:ListItem Text="不限" Value="-1"></asp:ListItem>
                    <asp:ListItem Text="5元以下" Value="0-5"></asp:ListItem>
                    <asp:ListItem Text="5-50元" Value="5-50"></asp:ListItem>
                    <asp:ListItem Text="50元以上" Value="50-"></asp:ListItem>
                </asp:DropDownList>&nbsp;
                <asp:ImageButton ID="imgbtnSearch" runat="server" ImageUrl="Images/search.gif" 
                    onclick="imgbtnSearch_Click" />
            </div>        
            <asp:Repeater ID="Repeater1" runat="server" 
                  onitemdatabound="Repeater1_ItemDataBound">
                <HeaderTemplate>
                    <table id="tab_head">
                        <tr>
                            <td style="padding-left:45px;width:500px;">商品</td>
                            <td style="width:100px;text-align:center;">成交价</td>
                            <td style="width:100px;text-align:center;">获得者</td>
                            <td style="width:150px;text-align:center;">成交时间</td>
                        </tr>
                    </table>
                </HeaderTemplate>
                <ItemTemplate>
                    <div class="future_area" onmouseover="this.className='future_area_current'" onmouseout="this.className='future_area'">
                        <ul>
                            <li class="future_area_img"><a href="ProDetail.aspx?id=<%#Eval("AuctionID") %>" target="_self"><asp:Image ID="imgPro" runat="server" Width="90px" Height="90px" /></a></li>
                            <li class="future_area_name"><a href="ProDetail.aspx?id=<%#Eval("AuctionID") %>" target="_self">第<%#Eval("Coding") %>期&nbsp;<asp:Literal ID="ltlProName" runat="server" Text='<%#Eval("ProductID") %>'></asp:Literal></a><br />
                                <asp:Label ID="lblProIntro" runat="server"></asp:Label><br />
                                市场价:<span style="color:#ff6600; font-weight:bold;">￥<asp:Literal ID="ltlProPrice" runat="server"></asp:Literal></span>
                            </li>
                            <li class="history_area_price">￥<%#Eval("AuctionPrice") %></li>
                            <li class="history_area_winner"><asp:Literal ID="ltlWinner" runat="server" Text='<%#Eval("HuiYuanID") %>'></asp:Literal></li>
                            <li class="history_area_time"><asp:Literal ID="ltlEndTime" runat="server" Text='<%#Eval("EndTime") %>'></asp:Literal></li>
                        </ul>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
            <webdiyer:aspnetpager ID="AspNetPager1" runat="server" CssClass="paginator" CurrentPageButtonClass="cpb"
                         LastPageText="尾页" FirstPageText="首页" PrevPageText="上一页" NextPageText="下一页" 
                            AlwaysShow="true" UrlPaging="true" PageSize="9" 
                            onpagechanged="AspNetPager1_PageChanged"></webdiyer:aspnetpager>
          </ContentTemplate>            
        </asp:UpdatePanel>        
    </div>
</asp:Content>
