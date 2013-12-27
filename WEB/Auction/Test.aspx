<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="WEB.Auction.Test" %>

<%@ Register Src="UserControl/Top.ascx" TagName="Top" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="../Scripts/jquery-1.9.1.js"></script>
    <link rel="Stylesheet" type="text/css" href="Styles/Style.css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $.extend({
                getData: function () {
                    $.ajax({
                        url: "testajax.aspx",
                        type: "GET",
                        dataType: "json",
                        data: "",
                        success: function (data) {
                            for (var i = 0; i < data.length; i++) {
                                var $id = data[i].AuctionID;
                                var $price = $("#testarea_price" + $id + " span");
                                var $member = $("#testarea_member" + $id + " span");
                                var $time = $("#testarea_time" + $id + " span:first");
                                var $timems = $("#testarea_time" + $id + " span:last");
                                var $timepoint = $("#testarea_time" + $id + " input");
                                $timepoint.text(data[i].TimePoint);
                                var auctionprice = data[i].AuctionPrice;
                                if (auctionprice != $price.text()) {
                                    $price.toggleClass('product_price_auctionprice_toggle');
                                    $price.text("￥" + data[i].AuctionPrice);
                                }                                
                                $member.text(data[i].HuiYuanID);
                                var datetime = new Date(ChangeDateFormat(data[i].AuctionTime));
                                var datenow = new Date();
                                $time.text(getTimeSpan(datetime, datenow));
                            }
                        },
                        error: function () { alert("ajax error."); }
                    });
                }
            });
            $.extend({
                getMS: function () {
                    $.ajax({
                        url: "testajax.aspx",
                        type: "GET",
                        dataType: "json",
                        data: "",
                        success: function (data) {
                            for (var i = 0; i < data.length; i++) {
                                var $id = data[i].AuctionID;
                                var $timepoint = $("#testarea_time" + $id + " input");
                                var $timems = $("#testarea_time" + $id + " span:last");
                                $timepoint.text(data[i].TimePoint);
                                $timems.text(timeMS($timepoint.text(), $timems.text()));
                            }
                        },
                        error: function () { }
                    });
                }
            });
            setInterval("$.getData()", 1000);
            setInterval("$.getMS()", 300);

            function ChangeDateFormat(jsondate) {
                var date = new Date(parseInt(jsondate.replace("/Date(", "").replace(")/", ""), 10));
                var year = date.getFullYear();
                var mouth = date.getMonth() + 1 < 10 ? "0" + (date.getMonth() + 1) : (date.getMonth() + 1);
                var day = date.getDate();
                var hour = date.getHours();
                var mm = date.getMinutes();
                var ss = date.getSeconds();
                return year + "/" + mouth + "/" + day + " " + hour + ":" + mm + ":" + ss;
            }

            function getTimeSpan(date1, date2) {
                var dateSpan = date1.getTime() - date2.getTime();
                var num = dateSpan % (24 * 3600 * 1000);
                var hours = Math.floor(num / (3600 * 1000));
                var nums = dateSpan % (3600 * 1000);
                var minutes = Math.floor(nums / (60 * 1000));
                var nums1 = nums % (60 * 1000);
                var seconds = Math.floor(nums1 / 1000);
                if (hours < 10 && hours > 0) {
                    hours = "0" + hours;
                }
                if (minutes < 10 && minutes > 0) {
                    minutes = "0" + minutes;
                }
                if (seconds < 10 && seconds > 0) {
                    seconds = "0" + seconds;
                }
                return hours + ":" + minutes + ":" + seconds;
            }

            function timeMS(timepoint, ms) {
                if (timepoint > 0 && timepoint <= 10) {
                    var results;
                    if (ms == "" || ms <= 0) {
                        ms = 9;
                    } else {
                        ms--;
                    }
                    results = ms;
                }
                return results;
            }
        });
    </script>
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
                    <div class="product_price_auctionprice" id="testarea_price<%#Eval("AuctionID") %>">
                        拍卖价：<asp:Label
                            ID="lblAuctionPrice" runat="server" Text='<%#Eval("AuctionPrice") %>'></asp:Label></div>
                    <div class="product_price" id="testarea_member<%#Eval("AuctionID") %>">
                        出价人：<span style="color: #00666b"><asp:Label ID="lblMemberName" runat="server"></asp:Label></span><asp:HiddenField
                            ID="hfMemberID" runat="server" Value='<%#Eval("HuiYuanID") %>' />
                    </div>
                    <div class="product_timer" id="testarea_time<%#Eval("AuctionID") %>">
                        <asp:Label ID="lblTime" runat="server"></asp:Label><asp:Label ID="lblMS" runat="server"></asp:Label>
                        <asp:HiddenField ID="hfTimePoint" runat="server" Value='<%#Eval("TimePoint") %>' />
                    </div>
                    <div class="product_auction" id="testarea_btn<%#Eval("AuctionID") %>">
                        <asp:ImageButton ID="imgbtnAuction" runat="server" ImageUrl="Images/bid_button.gif"
                            CommandName="auction" CommandArgument='<%#Eval("AuctionID") %>' /></div>                    
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
