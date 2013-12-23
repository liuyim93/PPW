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
                <asp:DropDownList ID="dropAuctionType" runat="server">
                    <asp:ListItem Text="正在热拍" Value="auction" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="免费竞拍" Value="free"></asp:ListItem>
                </asp:DropDownList>&nbsp;
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
                <asp:ImageButton ID="imgbtnSearch" runat="server" ImageUrl="Images/search.gif" />
            </div>        
            <asp:GridView ID="gvwHistory" runat="server" AutoGenerateColumns="false" DataKeyNames="AuctionID" 
                  Width="100%" onrowdatabound="gvwHistory_RowDataBound" GridLines="Both" CssClass="gvw">
                <Columns>
                    <asp:TemplateField HeaderText="商品">
                        <ItemTemplate>
                            <div>
                                <div class="product_img">
                                    <a href="../Auction/ProDetail.aspx?id=<%#Eval("AuctionID") %>" target="_self"><asp:Image ID="imgProduct" runat="server" Width="100px" Height="100px" /></a>
                                    <asp:HiddenField  ID="hfProductNo" runat="server" Value='<%#Eval("Coding") %>' />
                                    <asp:HiddenField ID="hfProductID" runat="server" Value='<%#Eval("ProductID") %>' />
                                </div>
                                <div class="product_name"><asp:HyperLink ID="hlnkPro" runat="server" ></asp:HyperLink><br />
                                    <asp:Label ID="lblIntro" runat="server" ></asp:Label>
                                </div>
                            </div>
                        </ItemTemplate>                                            
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="市场价">
                        <ItemTemplate>
                            <asp:Label ID="lblPrice" runat="server" ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="成交价">
                        <ItemTemplate>
                            <asp:Label ID="lblAuctionPrice" runat="server"><%#Eval("AuctionPrice") %></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="获得者">
                        <ItemTemplate>
                            <asp:Label ID="lblMemberName" runat="server" Text='<%#Eval("HuiYuanID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="成交时间">
                        <ItemTemplate>
                            <asp:Label ID="lblEndTime" runat="server" Text='<%#Eval("EndTime") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:Repeater ID="Repeater1" runat="server">
                <HeaderTemplate>
                    <table>
                        <tr>
                            <td>商品</td>
                            <td>成交价</td>
                            <td>获得者</td>
                            <td>成交时间</td>
                        </tr>
                    </table>
                </HeaderTemplate>
                <ItemTemplate>
                    <div>
                        <ul>
                            <li><a href="ProDetail.aspx?id<%#Eval("AuctionID") %>" target="_self"><asp:Image ID="imgPro" runat="server" Width="90px" Height="90px" /></a></li>
                            <li><a href="ProDetail.aspx?id<%#Eval("AuctionID") %>" target="_self">第<%#Eval("Coding") %>期&nbsp;<asp:Literal ID="ltlProName" runat="server" Text='<%#Eval("ProductID") %>'></asp:Literal></a><br />
                                <asp:Label ID="lblProIntro" runat="server"></asp:Label><br />
                                市场价:<span>￥<asp:Literal ID="ltlProPrice" runat="server"></asp:Literal></span>
                            </li>
                            <li>￥<%#Eval("AuctionPrice") %></li>
                            <li><asp:Literal ID="ltlWinner" runat="server" Text='<%#Eval("HuiYuanID") %>'></asp:Literal></li>
                            <li><asp:Literal ID="ltlEndTime" runat="server" Text='<%#Eval("EndTime") %>'></asp:Literal></li>
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
