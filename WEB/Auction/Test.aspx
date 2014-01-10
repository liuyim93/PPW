<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="WEB.Auction.Test" %>

<%@ Register Src="UserControl/Top.ascx" TagName="Top" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="../Scripts/jquery-1.9.1.js"></script>
    <script type="text/javascript" src="../Scripts/PaiPai_20140108_dyjjp.js"></script>
    <script type="text/javascript" src="../Scripts/Common.js"></script>
    <link rel="Stylesheet" type="text/css" href="Styles/Style.css" />
    <%--<script type="text/javascript">
        $(document).ready(function () {
            $.extend({
                getData: function () {
                    var $id = "";
                    $.ajax({
                        url: "testajax.aspx",
                        type: "GET",
                        dataType: "json",
                        async:false,
                        data: "",
                        success: function (data) {
                            for (var i = 0; i < data.length; i++) {
                                $id = data[i].AuctionID;
                                var $price = $("#testarea_price" + $id + " span");
                                var $member = $("#testarea_member" + $id + " span");
                                var $time = $("#testarea_time" + $id + " span:first");
                                var $timems = $("#testarea_time" + $id + " span:last");
                                var $timepoint = $("#testarea_time" + $id + " input");
                                $timepoint.text(data[i].TimePoint);
                                var auctionprice = data[i].AuctionPrice;
                                if ("￥" + auctionprice != $price.text()) {
                                    $price.toggleClass('product_price_auctionprice_toggle');
                                    $price.text("￥" + data[i].AuctionPrice);
                                } else {
                                    $price.removeClass('product_price_auctionprice_toggle');
                                }
                                $member.text(data[i].HuiYuanID);
                                var datetime = new Date(ChangeDateFormat(data[i].AuctionTime));
                                var datenow = new Date();

                                if (data[i].Status == 3) {
                                    $time.text("已成交");
                                    $timems.text("");
                                } else {
                                    if (getTimeSpan(datetime, datenow) != "") {
                                        $time.text(getTimeSpan(datetime, datenow));
                                    } else {
                                        if (data[i].TimePoint == 10) {
                                            $time.text("00:00:" + data[i].TimePoint);
                                        } else {
                                            $time.text("00:00:0" + data[i].TimePoint);
                                            if (data[i].TimePoint == 0) {
                                                $.ajax({
                                                    url: "auctioncomplete.aspx",
                                                    type: "get",
                                                    dataType: "json",
                                                    async: false,
                                                    data: "AuctionID=" + data[i].AuctionID + "&hyId=" + escape($("#testarea_member" + $id + " input").text()),
                                                    success: function (data) {
                                                        $time.text("已成交");
                                                    },
                                                    error: function () { }
                                                });
                                            }
                                        }
                                    }
                                }
                            }
                        },
                        error: function () { }
                    });
                }
            });
            $.extend({
                getMS: function () {
                    $.ajax({
                        url: "testajax.aspx",
                        type: "GET",
                        dataType: "json",
                        async:false,
                        data: "",
                        success: function (data) {
                            for (var i = 0; i < data.length; i++) {
                                var $id = data[i].AuctionID;
                                var $timepoint = $("#testarea_time" + $id + " input");
                                var $timems = $("#testarea_time" + $id + " span:last");
                                $timepoint.text(data[i].TimePoint);
                                if (data[i].Status == 3) {
                                    $timems.text("");
                                } else {
                                    $timems.text(timeMS($timepoint.text(), $timems.text()));
                                }
                            }
                        },
                        error: function (msg) { }
                    });
                }
            });
            var $auctionbtn = $(".auctionbtn");
            $auctionbtn.click(function () {
                var $auctionId = $(this).next().attr("value");
                $.ajax({
                    url: "submitauction.aspx",
                    type: "GET",
                    async:false,
                    data: "AuctionID=" + $auctionId,
                    success: function () { },
                    error: function (msg) { }
                });
            });
            setInterval("$.getData()", 1000);
            setInterval("$.getMS()", 200);
        });            

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
                if (dateSpan > 10000) {
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
                } else {
                    return "";
                }
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
    </script>--%>
</head>
<body>
    <form id="form1" runat="server">
    <%--<div>
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
                            CommandName="auction" CommandArgument='<%#Eval("AuctionID") %>' CssClass="auctionbtn"/>
                            <input type="text" value='<%#Eval("AuctionID") %>' style="display:none" />
                            </div>                    
                    <asp:HiddenField ID="hfAuctionTime" runat="server" Value='<%#Eval("AuctionTime") %>' />
                    <asp:HiddenField ID="hfStatus" runat="server" Value='<%#Eval("Status") %>' />
                    <asp:HiddenField ID="hfAuctionPoint" runat="server" Value='<%#Eval("AuctionPoint") %>' />
                    <asp:HiddenField ID="hfProductID" runat="server" Value='<%#Eval("ProductID") %>' />
                    <asp:HiddenField ID="hfProductNo" runat="server" Value='<%#Eval("Coding") %>' />
                </div>
            </ItemTemplate>
        </asp:DataList>
    </div>--%>
    <div style="width:1000px;position:relative;overflow:hidden; border:1px solid #ccc;margin:0px auto;height:1000px;">
        <asp:Repeater ID="repeater1" runat="server">
            <ItemTemplate>
                <div style="border:1px solid #ccc;float:left;width:150px;height:350px;margin:5px; position:relative;" id="bid_<%#Eval("AuctionID") %>">                    
                    <a href="#"><%#Eval("ProductID") %></a>
                    市场价：<input type="text" id="price_<%#Eval("AuctionID") %>" value="<%#Eval("ProductID") %>" />
                    拍卖价：<div id="Price_<%#Eval("AuctionID") %>"></div>
                    最后出价：<div id="UserInfo_<%#Eval("AuctionID") %>"></div>
                    <div id="timer_<%#Eval("AuctionID") %>"></div>
                    <div id="btn_<%#Eval("AuctionID") %>">
                        <a>出价</a>
                    </div>                    
                    <span id="spanTip_<%#Eval("AuctionID") %>" style="display:none;"></span>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
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
