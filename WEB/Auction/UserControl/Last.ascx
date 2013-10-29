<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Last.ascx.cs" Inherits="WEB.Auction.UserControl.Last" %>
<div class="auctionlist_rcomd">
                <div class="auctionlist_rcomd_title">最近成交</div>
                <asp:DataList ID="dlstHistory" runat="server" 
                    onitemdatabound="dlstHistory_ItemDataBound" Width="100%">
                    <ItemTemplate>
                        <div class="rcomd_area">
                            <div class="rcomd_img"><a href="" target="_self"><asp:Image ID="imgProduct" runat="server" Width="70px" Height="70px" /></a></div>
                            <div class="rcomd_detail">                                
                                <div class="rcomd_name"><asp:HyperLink ID="hlnkProName" runat="server" Text='<%#Eval("productName") %>'></asp:HyperLink></div>
                                <div class="rcomd_price">
                                    成交价：<span>￥<asp:Label ID="lblPrice" runat="server" Text='<%#Eval("PmJGproduct") %>'></asp:Label></span><br />
                                    获得者：<span><asp:Label ID="lblMemberName" runat="server" Text='<%#Eval("HuiYuanID") %>'></asp:Label></span>
                                </div>
                            </div>
                        </div>
                        <asp:HiddenField ID="hfProductID" runat="server" Value='<%#Eval("ProductID") %>' />
                        <asp:HiddenField ID="hfProductNo" runat="server" Value='<%#Eval("coding") %>' />
                    </ItemTemplate>
                </asp:DataList>
            </div>