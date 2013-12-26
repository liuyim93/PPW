<%@ Page Title="" Language="C#" MasterPageFile="~/Auction/Main.Master" AutoEventWireup="true" CodeBehind="AuctionList.aspx.cs" Inherits="WEB.Auction.AuctionList" %>
<%@ Register TagName="Recommend" Src="UserControl/Recommend.ascx" TagPrefix="uc1" %>
<%@ Register TagName="Last" Src="UserControl/Last.ascx" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript">
    function loadms() {
        var datalist = document.getElementById('<%=dlstAuction.ClientID %>').children[0];
        var rows = datalist.children.length;
        for (var row = 0; row < rows; row++) {
            var columns = datalist.children[0];
            for (var j = 0; j < columns.children.length; j++) {
                if (columns.children[j].innerHTML != "" && columns.children[j].innerHTML != null) {
                    var item = columns.children[j].children;
                    var itemlen = item.children.length;
                    var timems = item.children[6].children[2];
                    var timepoint = item.children[8];
                    if (timepoint.innerText != "" && timepoint.innerText > 0 && timepoint.innerText < 10) {
                        setInterval("updatems(timems)", 100);
                    }
                }
            }
        }
    }
    function updatems(timems) {        
        var i = 9;
        if (i >= 0) {
            timems.innerText = i;
            i--;
        } else {
            i = 9;
        }
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="auctionlist">
    <div class="auctionlist_top">正在热拍：</div>
    <div class="auctionlist">
        <div class="auctionlist_left">
            <div class="auctionlist_left_title">正在热拍</div>
            <div class="auctionlist_left_content">
                <asp:UpdatePanel ID="updatepanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Timer ID="timer1" runat="server" ontick="timer1_Tick" Interval="1000"></asp:Timer>
                        <asp:DataList ID="dlstAuction" runat="server" RepeatColumns="4" 
                            onitemcommand="dlstAuction_ItemCommand" DataKeyField="AuctionID"
                            onitemdatabound="dlstAuction_ItemDataBound">
                            <ItemTemplate>
                                <div class="product_area">
                                    <div class="product_name"><asp:HyperLink ID="hlnkPro" runat="server" Target="_self">&nbsp;<asp:Label ID="lblIntro" runat="server"></asp:Label></asp:HyperLink></div>
                                    <div class="product_fullprice"><img src="Images/fullprice.png" width="16px" height="16px" title="若竞拍未成功，可以按市场价补差价购买此商品！" alt="" /></div>
                                    <div class="product_img"><a href="../Auction/ProDetail.aspx?id=<%#Eval("AuctionID") %>" target="_self"><asp:Image ID="imgProduct" runat="server" Width="120px" Height="120px" /></a></div>
                                    <div class="product_price">市场价：￥<span style="font-weight:bold;"><asp:Label ID="lblPrice" runat="server" ></asp:Label></span></div>
                                    <div class="product_price">拍卖价：<span style="color:Red;font-weight:bold;font-family:Arial;">￥<asp:Label ID="lblAuctionPrice" runat="server" Text='<%#Eval("AuctionPrice") %>'></asp:Label></span></div>
                                    <div class="product_price">出价人：<span style="color:#00666b"><asp:Label ID="lblMemberName" runat="server"></asp:Label></span><asp:HiddenField ID="hfMemberID" runat="server" Value='<%#Eval("HuiYuanID") %>' /></div>
                                    <div class="product_timer"><asp:Label ID="lblTime" runat="server"></asp:Label><asp:Label ID="lblMS" runat="server"></asp:Label></div>
                                    <div class="product_auction"><asp:ImageButton ID="imgbtnAuction" runat="server" ImageUrl="Images/bid_button.gif" CommandName="auction" CommandArgument='<%#Eval("AuctionID") %>' /></div>
                                    <asp:HiddenField ID="hfTimePoint" runat="server" Value='<%#Eval("TimePoint") %>' />
                                    <asp:HiddenField ID="hfAuctionTime" runat="server" Value='<%#Eval("AuctionTime") %>' /> 
                                   <asp:HiddenField ID="hfStatus" runat="server" Value='<%#Eval("Status") %>' />                                                             
                                   <asp:HiddenField ID="hfAuctionPoint" runat="server" Value='<%#Eval("AuctionPoint") %>' />
                                   <asp:HiddenField ID="hfProductID" runat="server" Value='<%#Eval("ProductID") %>' />
                                   <asp:HiddenField ID="hfProductNo" runat="server" Value='<%#Eval("Coding") %>' />                                  
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
            <uc1:Recommend ID="recommend1" runat="server" />
            <uc2:Last ID="last1" runat="server" />
        </div>
    </div>
</div>
</asp:Content>
