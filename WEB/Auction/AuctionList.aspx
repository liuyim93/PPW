<%@ Page Title="" Language="C#" MasterPageFile="~/Auction/Main.Master" AutoEventWireup="true" CodeBehind="AuctionList.aspx.cs" Inherits="WEB.Auction.AuctionList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="auctionlist">
    <div>正在热拍：</div>
    <div class="auctionlist">
        <div class="auctionlist_left">
            <div class="auctionlist_left_title">正在热拍</div>
            <div class="auctionlist_left_content">
                <asp:UpdatePanel ID="updatepanel1" runat="server">
                    <ContentTemplate>
                        <asp:Timer ID="timer1" runat="server"></asp:Timer>
                        <asp:DataList ID="dlstAuction" runat="server" RepeatColumns="4" 
                            onitemcommand="dlstAuction_ItemCommand" 
                            onitemdatabound="dlstAuction_ItemDataBound">
                            <ItemTemplate>
                                <div class="product_area">
                                    <div class="product_name"><a href="" target="_self" title='<%#Eval("productName") %>'><%#Eval("productName") %>&nbsp;<span><%#Eval("Intro") %></span></a></div>
                                    <div class="product_fullprice"><img src="Images/fullprice.png" width="16px" height="16px" title="若竞拍未成功，可以按市场价补差价购买此商品！" /></div>
                                    <div class="product_img"><a href="" target="_self"><asp:Image ID="imgProduct" runat="server" Width="120px" Height="120px" /></a></div>
                                    <div class="product_price">市场价：￥<span style="font-weight:bold;"><asp:Label ID="lblPrice" runat="server" Text='<%#Eval("productPrice") %>'></asp:Label></span></div>
                                    <div class="product_price">拍卖价：<span style="color:Red;font-weight:bold;font-family:Arial;">￥<asp:Label ID="lblAuctionPrice" runat="server" Text='<%#Eval("PmJGproduct") %>'></asp:Label></span></div>
                                    <div class="product_price">出价人：<span style="color:#00666b"><asp:Label ID="lblMemberName" runat="server"></asp:Label></span><asp:HiddenField ID="hfMemberID" runat="server" Value='<%#Eval("HuiYuanID") %>' /></div>
                                    <div class="product_timer"><asp:Label ID="lblTime" runat="server"></asp:Label></div>
                                    <div class="product_auction"><asp:ImageButton ID="imgbtnAuction" runat="server" ImageUrl="Images/bid_button.gif" CommandName="auction" CommandArgument='<%#Eval("ProductID") %>' /></div>
                                    <asp:HiddenField ID="hfTimePoint" runat="server" Value='<%#Eval("TimePoint") %>' />
                                    <asp:HiddenField ID="hfAuctionTime" runat="server" Value='<%#Eval("AuctionTime") %>' /> 
                                   <asp:HiddenField ID="hfStatus" runat="server" Value='<%#Eval("Status") %>' />
                                   <!--运费、手续费 -->                                                               
                                   <asp:HiddenField ID="hfFee" runat="server" Value='<%#Eval("Fee") %>' />
                                   <asp:HiddenField ID="hfShipFee" runat="server" Value='<%#Eval("ShipFee") %>' />
                                   <asp:HiddenField ID="hfAuctionPoint" runat="server" Value='<%#Eval("AuctionPoint") %>' />
                                   <asp:HiddenField ID="hfProductName" runat="server" Value='<%#Eval("productName") %>' />
                                   <asp:HiddenField ID="hfProductID" runat="server" Value='<%#Eval("ProductID") %>' />
                                   <asp:HiddenField ID="hfProductNo" runat="server" Value='<%#Eval("coding") %>' />                                  
                                </div>
                            </ItemTemplate>
                        </asp:DataList>
                    </ContentTemplate>
                </asp:UpdatePanel>                
            </div>
        </div>
        <div class="auctionlist_right">
            <div class="auctionlist_right_reg"><a href="" target="_self"><img src="Images/register_ad.jpg" /></a></div>
            <div class="auctionlist_right_guide"><a href="" target="_self"><img src="Images/fresh_guide.jpg" /></a></div>
            <div class="auctionlist_rcomd">
                <div class="auctionlist_rcomd_title">推荐商品</div>
                <asp:DataList ID="dlstRecommend" runat="server">
                    <ItemTemplate>
                        <div>
                            <div><asp:HyperLink ID="hlnkProName" runat="server" Text='<%#Eval("productName") %>'></asp:HyperLink></div>
                            <div>
                                <div><a href="" target="_self"><asp:Image ID="imgProduct" runat="server" /></a></div>
                                <div>
                                    市场价：<span>￥<asp:Label ID="lblProPrice" runat="server" Text='<%#Eval("productPrice") %>'></asp:Label></span><br />
                                    拍卖时间：<asp:Label ID="lblAuctionTime" runat="server" Text='<%#Eval("AuctionTime") %>'></asp:Label>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:DataList>
            </div>
            <div class="auctionlist_rcomd">
                <div class="auctionlist_rcomd_title">历史成交</div>
                <asp:DataList ID="dlstHistory" runat="server">
                    <ItemTemplate>
                        <div>
                            <div><asp:HyperLink ID="hlnkProName" runat="server" Text='<%#Eval("productName") %>'></asp:HyperLink></div>
                            <div>
                                <div><a href="" target="_self"><asp:Image ID="imgProduct" runat="server" /></a></div>
                                <div>
                                    成交价：<span>￥<asp:Label ID="lblPrice" runat="server" Text='<%#Eval("PmJGproduct") %>'></asp:Label></span><br />
                                    获得者：<span><asp:Label ID="lblMemberName" runat="server" Text='<%#Eval("HuiYuanID") %>'></asp:Label></span>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:DataList>
            </div>
        </div>
    </div>
</div>
</asp:Content>
