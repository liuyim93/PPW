<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Recommend.ascx.cs" Inherits="WEB.Auction.UserControl.Recommand" %>
<div class="auctionlist_rcomd">
                <div class="auctionlist_rcomd_title">推荐商品</div>
                <asp:DataList ID="dlstRecommend" runat="server" 
                    onitemdatabound="dlstRecommend_ItemDataBound" Width="100%">
                    <ItemTemplate>
                        <div class="rcomd_area">
                            <div class="rcomd_img"><a href="" target="_self"><asp:Image ID="imgProduct" runat="server" Width="70px" Height="70px" /></a></div>                            
                            <div class="rcomd_detail">
                                <div class="rcomd_name"><asp:HyperLink ID="hlnkProName" runat="server" Text='<%#Eval("productName") %>'></asp:HyperLink></div>
                                <div class="rcomd_price">
                                    市场价：<span>￥<asp:Label ID="lblProPrice" runat="server" Text='<%#Eval("productPrice") %>'></asp:Label></span><br />
                                    拍卖时间：<asp:Label ID="lblAuctionTime" runat="server" Text='<%#Eval("AuctionTime") %>'></asp:Label>
                                </div>
                            </div>
                        </div>
                        <asp:HiddenField ID="hfProductID" runat="server" Value='<%#Eval("ProductID") %>' />
                        <asp:HiddenField ID="hfProductNo" runat="server" Value='<%#Eval("coding") %>' />
                    </ItemTemplate>
                </asp:DataList>
            </div>