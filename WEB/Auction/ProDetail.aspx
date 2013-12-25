<%@ Page Title="" Language="C#" MasterPageFile="~/Auction/Main.Master" AutoEventWireup="true" CodeBehind="ProDetail.aspx.cs" Inherits="WEB.Auction.Auctioning" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript" src="~/Scripts/jquery-1.9.1.js"></script>
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
<asp:UpdatePanel ID="updatepanel1" runat="server">
    <ContentTemplate>
    <asp:Timer ID="Timer1" runat="server" Interval="1000" ontick="Timer1_Tick"></asp:Timer>       
    <div class="detail">
        <div class="pro_info">
            <div class="left">
                <div class="img_big">
                    <asp:Image ID="imgBig" runat="server" Width="300px" Height="300px" />
                </div>
                <div class="img_small">
                    
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
                        <li><asp:Literal ID="ltlPrice" runat="server" Text="当前价格："></asp:Literal><asp:Label ID="lblAuctionPrice" runat="server" ></asp:Label></li>
                        <li><asp:Literal ID="ltlMember" runat="server" Text="出价人："></asp:Literal><asp:Label ID="lblMemberName" runat="server"></asp:Label></li>
                    </ol>
                </div>
                <asp:Panel ID="pnlAuction" runat="server" CssClass="auction">
                    <div class="time">
                        <div class="text">剩余时间：</div>
                        <div class="timer">
                            <asp:Label ID="lblTime" runat="server"></asp:Label>                            
                        </div>
                    </div>
                    <div class="button">
                        <asp:ImageButton ID="imgbtnAuction" runat="server" 
                            ImageUrl="Images/bid_button.gif" onclick="imgbtnAuction_Click" />
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnlEnd" runat="server" class="end">
                    <div class="text">竞拍已结束</div>
                    <div class="timespan">开始时间：<asp:Label ID="lblStart" runat="server"></asp:Label><br />
                        结束时间：<asp:Label ID="lblEnd" runat="server"></asp:Label>
                    </div>
                </asp:Panel>
                <div class="bottom">
                    每次出价->消耗<asp:Label ID="lblPoint" runat="server"></asp:Label>点->拍卖价增加<asp:Label ID="lblPriceAdd" runat="server"></asp:Label>元<br />
                    每次出价->成交时间回滚至10秒<br />
                    倒计时为0->成交！
                </div>
            </div>
            <div class="right">
                <div class="right_top">
                    <div class="top_title">
                        <div id="title_history" onmouseover="setTab(1);" class="title_focus">竞拍历史</div>
                        <div id="title_my" onmouseover="setTab(2);" class="title_my">我的竞拍</div>                        
                    </div>
                    <div class="top_content">
                        <div id="content_my">
                            <ul>
                                <li>使用拍点：<asp:Label ID="lblAuctionPoint" runat="server">0</asp:Label></li>
                                <li>使用返点：<asp:Label ID="lblFreePoint" runat="server">0</asp:Label></li>
                                <li>补差价购买：￥<asp:Literal ID="ltlProPrice" runat="server"></asp:Literal>-<asp:Label ID="lblUsed" runat="server">0</asp:Label>=￥<asp:Label ID="lblPay" runat="server"></asp:Label></li>
                            </ul>
                        </div>
                        <div id="content_history">
                            <asp:GridView ID="gvwHistory" runat="server" AutoGenerateColumns="false" 
                                 onrowdatabound="gvwHistory_RowDataBound" GridLines="None" Width="100%">
                                <Columns>
                                    <asp:TemplateField HeaderText="出价人">
                                        <ItemTemplate>
                                            <a class="membername"><asp:Label ID="lblMemberName" runat="server"></asp:Label></a>
                                            <asp:HiddenField ID="hfMemberID" runat="server" Value='<%#Eval("HuiYuanID") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="参与价">
                                        <ItemTemplate>
                                            ￥<asp:Label ID="lblPrice" runat="server" Text='<%#Eval("Price") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="IP地址">
                                        <ItemTemplate>
                                            <asp:Label ID="lblIPAdress" runat="server" Text='<%#Eval("IPAdress") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
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
    </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
