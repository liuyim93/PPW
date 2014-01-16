<%@ Page Title="" Language="C#" MasterPageFile="~/Auction/Main.Master" AutoEventWireup="true" CodeBehind="ProDetail.aspx.cs" Inherits="WEB.Auction.Auctioning" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">      
        function setTab(num) {
            var auction_my = document.getElementById('title_my');
            var auction_history = document.getElementById('title_history');
            var content_my = document.getElementById('content_my');
            var content_history = document.getElementById('content_history');
            if (num == 2) {
                auction_my.className = "title_focus";
                content_my.style.display = "block";
                content_history.style.display = "none";
                auction_history.className = "title_history";
            } else {
                auction_history.className = "title_focus";
                content_history.style.display = "block";
                content_my.style.display = "none";
                auction_my.className = "title_my";
            }
        }        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server"> 
    <div class="detail">
        <div class="pro_info">
            <div class="left">
                <div class="img_big">
                    <asp:Image ID="imgBig" runat="server" Width="300px" Height="300px" />
                </div>
                <div class="img_small">
                    <asp:Repeater ID="repeater_img" runat="server">
                        <ItemTemplate>
                            <img id="img_<%#Eval("ProductImegId") %>" src="<%#Eval("img") %>" alt="" class="pointsdetail_img_non" title="温馨提示：图片仅供参考，商品以实物为准。" />
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
            <div class="center">
                <div class="name">
                    <asp:Label ID="lblProName" runat="server"></asp:Label><asp:Label ID="lblIntro" runat="server"></asp:Label>
                </div>
                <div class="middle">
                    <ol>
                        <li>市场价：<asp:Label ID="lblPrice" runat="server"></asp:Label></li>
                        <li>手续费：<asp:Label ID="lblFee" runat="server"></asp:Label></li>
                        <li>运费：<asp:Label ID="lblShipFee" runat="server"></asp:Label></li>
                    </ol>
                    <ol>
                        <li>当前价格：<div id="Price_<%=auctionId %>" class="product_price_auctionprice"></div></li>
                        <li>最后出价：<div id="UserInfo_<%=auctionId %>" class="product_price_userinfo"></div></li>
                    </ol>
                </div>
                <asp:Panel ID="pnlAuction" runat="server" CssClass="auction">
                    <div class="time">
                        <div class="text">剩余时间：</div>
                        <div class="product_timer" id="timer_<%=auctionId %>">                                                       
                        </div>
                    </div>
                    <div class="product_auction" id="btn_<%=auctionId %>">
                        <img id="<%=auctionId %>" src="Images/bid_button.gif" alt="出价" />
                    </div>
                </asp:Panel>
                <div class="bottom">
                    每次出价->消耗<asp:Label ID="lblPoint" runat="server"></asp:Label>点->拍卖价增加<asp:Label ID="lblPriceAdd" runat="server"></asp:Label>元<br />
                    每次出价->成交时间回滚至10秒<br />
                    倒计时为0->成交！
                </div>
            </div>
            <div class="pro_info_right">
                <div class="right_top">
                    <div class="top_title">
                        <div id="title_history" onmouseover="setTab(1);" class="title_focus">竞拍历史</div>
                        <div id="title_my" onmouseover="setTab(2);" class="title_my">我的竞拍</div>                        
                    </div>
                    <div class="top_content">
                        <div id="content_my">
                            <%--<ul>
                                <li>使用拍点：<asp:Label ID="lblAuctionPoint" runat="server">0</asp:Label></li>
                                <li>使用返点：<asp:Label ID="lblFreePoint" runat="server">0</asp:Label></li>
                                <li>补差价购买：￥<asp:Literal ID="ltlProPrice" runat="server"></asp:Literal>-<asp:Label ID="lblUsed" runat="server">0</asp:Label>=￥<asp:Label ID="lblPay" runat="server"></asp:Label></li>
                            </ul>--%>
                        </div>
                        <div id="content_history">
                            
                        </div>
                    </div>
                </div>
                <div class="right_bottom">
                    
                </div>
            </div>
        </div>
        <div class="pro_detail">
            <div class="title">商品描述</div>
            <div class="content">
                <asp:Label ID="lblDetail" runat="server"></asp:Label>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        var actId='<%=auctionId %>';
        var isLogin=<%=isLogin %>;
        var proPrice=<%=proPrice %>;
        var PaiPai_Manage=null;
        $(function(){
            PaiPai_Manage=new PaiPaiBid();
            $("div[id^='btn_']").click(function(){
                PaiPai_Manage.Bid($(this).attr("id").replace("btn_",""));   
            });
            PaiPai_Manage.Add(actId);
            PaiPai_Manage.Start();
            PaiPai_Manage.BidHistory(actId,0,$("#content_history"));
            PaiPai_Manage.UserInfoSelf(actId,proPrice,$("#content_my"),isLogin);
        })
    </script>
</asp:Content>
