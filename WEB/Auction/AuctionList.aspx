<%@ Page Title="" Language="C#" MasterPageFile="~/Auction/Main.Master" AutoEventWireup="true" CodeBehind="AuctionList.aspx.cs" Inherits="WEB.Auction.AuctionList" %>
<%@ Register TagName="Recommend" Src="UserControl/Recommend.ascx" TagPrefix="uc1" %>
<%@ Register TagName="Last" Src="UserControl/Last.ascx" TagPrefix="uc2" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="Webdiyer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="auctionlist">
    <div class="auctionlist_top">正在热拍：</div>
    <div class="auctionlist">
        <div class="auctionlist_left">
            <div class="auctionlist_left_title">正在热拍</div>
            <div class="auctionlist_left_content">  
                <asp:Repeater ID="repeater1" runat="server">
                    <ItemTemplate>
                        <div id="bid_<%#Eval("AuctionID") %>" class="product_area">
                            <div class="product_name">
                                <a href="../Auction/ProDetail.aspx?id=<%#Eval("AuctionID") %>" target="_self" title='<%#Eval("productName") %> <%#Eval("Intro") %>'><%#Eval("productName") %></a>
                            </div>
                            <div class="product_fullprice">
                                <img src="Images/fullprice.png" title="若竞拍未成功，可以按市场价补差价购买此商品！" alt="" width="16px" height="16px" />
                                <span id="spanTip_<%#Eval("AuctionID") %>"></span>
                            </div>
                            <div class="product_img">
                                <a href="../Auction/ProDetail.aspx?id=<%#Eval("AuctionID") %>" target="_self"><img id="imgPro" width="120px" height="120px" src="<%#Eval("img") %>" alt="" title='<%#Eval("productName") %>' /></a>
                            </div>
                            <div class="product_price">
                                市场价：<span><%#Eval("productPrice") %></span></div>
                            <div class="product_price">
                                拍卖价：<div id="Price_<%#Eval("AuctionID") %>" class="product_price_auctionprice"></div>
                            </div>
                            <div class="product_price">
                                最后出价：<div id="UserInfo_<%#Eval("AuctionID") %>" class="product_price_userinfo"></div>
                            </div>
                            <div id="timer_<%#Eval("AuctionID") %>" class="product_timer"></div>
                            <div id="btn_<%#Eval("AuctionID") %>" class="product_auction">
                                <img src="Images/bid_button.gif" id='<%#Eval("AuctionID") %>' title="每次出价消耗<%#Eval("AuctionPoint") %>拍点" />
                            </div>                            
                        </div>
                    </ItemTemplate>
                </asp:Repeater>       
                <webdiyer:aspnetpager ID="AspNetPager1" runat="server" CssClass="paginator" CurrentPageButtonClass="cpb"
                         LastPageText="尾页" FirstPageText="首页" PrevPageText="上一页" NextPageText="下一页" 
                            AlwaysShow="true" UrlPaging="true" PageSize="9" 
                            onpagechanged="AspNetPager1_PageChanged"></webdiyer:aspnetpager>     
            </div>
             <script type="text/javascript">
                 $(function () {
                     var PaiPai_Manage = new PaiPaiBid();
                     $("div[id^='bid_']").each(function (i) {
                         PaiPai_Manage.Add(this.id.substr(4));
                     });
                     $("div[id^='btn_']").click(function () {
                         PaiPai_Manage.Bid($(this).attr("id").replace("btn_", ""));
                     });
                     PaiPai_Manage.Start();
                 });
    </script>
        </div>
        <div class="auctionlist_right">
            <div class="auctionlist_right_reg"><a href="" target="_self"><img src="Images/register_ad.jpg" /></a></div>
            <div class="auctionlist_right_guide"><a href="" target="_self"><img src="Images/fresh_guide.jpg" /></a></div>
            <uc1:Recommend ID="recommend1" runat="server" />
            <uc2:Last ID="last1" runat="server" />
        </div>
    </div>
</div>
</asp:Content>
