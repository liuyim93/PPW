<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="WEB.Auction.Test" %>

<%@ Register Src="UserControl/Top.ascx" TagName="Top" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="../Scripts/jquery-1.9.1.js"></script>
    <link rel="Stylesheet" type="text/css" href="Styles/Style.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:Top ID="top1" runat="server" />
        <asp:DataList ID="dlstAuction" runat="server" RepeatColumns="5" 
            onitemdatabound="dlstAuction_ItemDataBound" DataKeyField="AuctionID">
            <ItemTemplate>
                <div id="testarea<%#Eval("AuctionID") %>" class="product_area">
                    <div class="product_name" id="testarea_name<%#Eval("AuctionID") %>">
                        <asp:HyperLink ID="hlnkPro" runat="server" Target="_self">
                            &nbsp;<asp:Label ID="lblIntro" runat="server"></asp:Label></asp:HyperLink></div>
                    <div class="product_fullprice">
                        <img src="Images/fullprice.png" width="16px" height="16px" title="若竞拍未成功，可以按市场价补差价购买此商品！"
                            alt="" /></div>
                    <div class="product_img">
                        <a href="../Auction/ProDetail.aspx?id=<%#Eval("AuctionID") %>" target="_self">
                            <asp:Image ID="imgProduct" runat="server" Width="120px" Height="120px" /></a></div>
                    <div class="product_price">
                        市场价：￥<span style="font-weight: bold;"><asp:Label ID="lblPrice" runat="server"></asp:Label></span></div>
                    <div class="product_price" id="testarea_price<%#Eval("AuctionID") %>">
                        拍卖价：<span style="color: Red; font-weight: bold; font-family: Arial;">￥<asp:Label
                            ID="lblAuctionPrice" runat="server" Text='<%#Eval("AuctionPrice") %>'></asp:Label></span></div>
                    <div class="product_price" id="testarea_member<%#Eval("AuctionID") %>">
                        出价人：<span style="color: #00666b"><asp:Label ID="lblMemberName" runat="server"></asp:Label></span><asp:HiddenField
                            ID="hfMemberID" runat="server" Value='<%#Eval("HuiYuanID") %>' />
                    </div>
                    <div class="product_timer" id="testarea_time<%#Eval("AuctionID") %>">
                        <asp:Label ID="lblTime" runat="server"></asp:Label><asp:Label ID="lblMS" runat="server"></asp:Label></div>
                    <div class="product_auction" id="testarea_btn<%#Eval("AuctionID") %>">
                        <asp:ImageButton ID="imgbtnAuction" runat="server" ImageUrl="Images/bid_button.gif"
                            CommandName="auction" CommandArgument='<%#Eval("AuctionID") %>' /></div>
                    <asp:HiddenField ID="hfTimePoint" runat="server" Value='<%#Eval("TimePoint") %>' />
                    <asp:HiddenField ID="hfAuctionTime" runat="server" Value='<%#Eval("AuctionTime") %>' />
                    <asp:HiddenField ID="hfStatus" runat="server" Value='<%#Eval("Status") %>' />
                    <asp:HiddenField ID="hfAuctionPoint" runat="server" Value='<%#Eval("AuctionPoint") %>' />
                    <asp:HiddenField ID="hfProductID" runat="server" Value='<%#Eval("ProductID") %>' />
                    <asp:HiddenField ID="hfProductNo" runat="server" Value='<%#Eval("Coding") %>' />
                </div>
            </ItemTemplate>
        </asp:DataList>
    </div>
    </form>
</body>
</html>
