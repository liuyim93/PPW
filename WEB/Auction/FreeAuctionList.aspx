<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FreeAuctionList.aspx.cs"
    Inherits="WEB.Auction.FreeAuctionList" %>

<%@ Register Src="UserControl/Top.ascx" TagName="Top" TagPrefix="uc1" %>
<%@ Register Src="UserControl/Bottom.ascx" TagName="Bottom" TagPrefix="uc2" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="Webdiyer" %>
<%@ Register TagName="Recommend" Src="UserControl/Recommend.ascx" TagPrefix="uc3" %>
<%@ Register TagName="Last" Src="UserControl/Last.ascx" TagPrefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Styles/Style.css" type="text/css" rel="Stylesheet" />
    <script src="Scripts/Main.js" type="text/javascript"></script>
    <script src="../Scripts/PaiPai_20140108_dyjjp.js" type="text/javascript"></script>
    <script src="../Scripts/Common.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-1.9.1.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-1.2.6.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <uc1:Top ID="top1" runat="server" />
    <div class="auctionlist">        
        <div class="auctionlist_top">
            正在热拍：</div>
        <div class="">
            <div class="auctionlist_left">
                <div class="auctionlist_left_title">
                    免费赢取<span>&nbsp;Free Auction</span></div>
                <div class="auctionlist_left_content">
                    <asp:Repeater ID="repeater1" runat="server">
                        <ItemTemplate>
                            <div id="bid_<%#Eval("AuctionID") %>" class="product_area">
                                <div class="product_name">
                                    <a href="../Auction/ProDetail.aspx?id=<%#Eval("AuctionID") %>" target="_self" title='<%#Eval("productName") %> <%#Eval("Intro") %>'>
                                        <%#Eval("productName") %></a>
                                </div>
                                <div class="product_fullprice">
                                    <img src="Images/free.png" title="新手专享，免费赢取" alt="" width="16px"
                                        height="16px" />
                                    <span id="spanTip_<%#Eval("AuctionID") %>"></span>
                                </div>
                                <div class="product_img">
                                    <a href="../Auction/ProDetail.aspx?id=<%#Eval("AuctionID") %>" target="_self">
                                        <img id="imgPro" width="120px" height="120px" src="<%#Eval("img") %>" alt="" title='<%#Eval("productName") %>' /></a>
                                </div>
                                <div class="product_price">
                                    市场价：<span><%#Eval("productPrice") %></span></div>
                                <div class="product_price">
                                    拍卖价：<div id="Price_<%#Eval("AuctionID") %>" class="product_price_auctionprice">
                                    </div>
                                </div>
                                <div class="product_price">
                                    最后出价：<div id="UserInfo_<%#Eval("AuctionID") %>" class="product_price_userinfo">
                                    </div>
                                </div>
                                <div id="timer_<%#Eval("AuctionID") %>" class="product_timer">
                                </div>
                                <div id="btn_<%#Eval("AuctionID") %>" class="product_auction">
                                    <img src="Images/bid_button.gif" id='<%#Eval("AuctionID") %>' title="每次出价消耗<%#Eval("FreePoint") %>返点" />
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                    <Webdiyer:AspNetPager ID="AspNetPager1" runat="server" CssClass="paginator" CurrentPageButtonClass="cpb"
                        LastPageText="尾页" FirstPageText="首页" PrevPageText="上一页" NextPageText="下一页" AlwaysShow="true"
                        UrlPaging="true" PageSize="16" OnPageChanged="AspNetPager1_PageChanged">
                    </Webdiyer:AspNetPager>
                </div>
            </div>
        </div>
        <div class="auctionlist_right">
            <div class="auctionlist_right_reg">
                <a>
                    <img src="Images/register_ad.jpg" alt="" /></a></div>
            <div class="auctionlist_right_guide">
                <a>
                    <img src="Images/fresh_guide.jpg" alt="" /></a></div>
            <uc3:Recommend ID="recommend1" runat="server" />
            <uc4:Last ID="last1" runat="server" />
        </div>
    </div>
        <uc2:Bottom ID="bottom1" runat="server" />  
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
    </form>
</body>
</html>
