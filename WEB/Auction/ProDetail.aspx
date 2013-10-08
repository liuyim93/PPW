<%@ Page Title="" Language="C#" MasterPageFile="~/Auction/Main.Master" AutoEventWireup="true" CodeBehind="ProDetail.aspx.cs" Inherits="WEB.Auction.Auctioning" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="detail">
        <div class="pro_info">
            <div class="left">
                <div class="img_big">
                    <asp:Image ID="imgBig" runat="server" />
                </div>
                <div class="img_small">
                    
                </div>
            </div>
            <div class="center">
                <div>
                    <asp:Label ID="lblProName" runat="server"></asp:Label><asp:Label ID="lblIntro" runat="server"></asp:Label>
                </div>
                <div>
                    <ol>
                        <li>市场价：<asp:Label ID="lblPrice" runat="server"></asp:Label></li>
                        <li>手续费：<asp:Label ID="lblFee" runat="server"></asp:Label></li>
                        <li>运费：<asp:Label ID="lblShipFee" runat="server"></asp:Label></li>
                    </ol>
                    <ol>
                        <li><asp:Literal ID="ltlPrice" runat="server" Text="当前价格："></asp:Literal><asp:Label ID="lblAuctionPrice" runat="server"></asp:Label></li>
                        <li><asp:Literal ID="ltlMember" runat="server" Text="出价人："></asp:Literal><asp:Label ID="lblMemberName" runat="server"></asp:Label></li>
                    </ol>
                </div>
                <asp:Panel ID="pnlAuction" runat="server">
                    <div>
                    <div>
                        <div>剩余时间：</div>
                        <div>
                            <asp:Label ID="lblTime" runat="server"></asp:Label>                            
                        </div>
                    </div>
                    <div>
                        <asp:ImageButton ID="imgbtnAuction" runat="server" ImageUrl="../Images/bid_button2.jpg" />
                    </div>
                </div>
                </asp:Panel>
                <asp:Panel ID="pnlEnd" runat="server">
                    <div>竞拍已结束</div>
                    <div>开始时间：<asp:Label ID="lblStart" runat="server"></asp:Label><br />
                        结束时间:<asp:Label ID="lblEnd" runat="server"></asp:Label>
                    </div>
                </asp:Panel>
                <div>
                    每次出价->消耗<asp:Label ID="lblPoint" runat="server"></asp:Label>点->拍卖价增加<asp:Label ID="lblPriceAdd" runat="server"></asp:Label>元<br />
                    每次出价->成交时间回滚至10秒<br />
                    倒计时为0->成交！
                </div>
            </div>
            <div class="right">
                <div>
                    <div>
                        <div>我的竞拍</div>
                        <div>竞拍历史</div>
                    </div>
                    <div>
                        <div></div>
                        <div></div>
                    </div>
                </div>
                <div>
                    
                </div>
            </div>
        </div>
        <div class="pro_detail">
            <div>商品描述</div>
            <div>
                
            </div>
        </div>
    </div>
</asp:Content>
