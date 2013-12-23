<%@ Page Title="" ValidateRequest="false" Language="C#" MasterPageFile="~/Auction/Main.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="WEB.Auction.Index" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Styles/Style.css" rel="Stylesheet" type="text/css" />
     <script src="../Scripts/jquery-1.2.6.js"></script>     
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="width:100%;height:100%;">
        <div id="index_top">
            <div class="top_left">
                <!--广播-->
                <div class="broadcast">
                    <div style="float:left; height:20px;width:20px; padding:10px 0;"><img src="../Image/notice_icon.gif" width="16px" height="16px" /></div>
                    <div id="marquee" style="float:left;width:710px;overflow:hidden;height:40px;line-height:40px;color:#999999;font-size:13px;">
                      <div style="width:800%;float:left;">
                        <div id="marquee1" style="float:left;">                       
                            <asp:DataList ID="dlstBroad" runat="server" Width="100%" OnItemDataBound="dlstBroad_ItemDataBound" RepeatDirection="Horizontal" DataKeyField="ProductID">
                                <ItemTemplate>
                                    <a href="../Auction/ProDetail.aspx?id=<%#Eval("AuctionID") %>" target="_blank" style="cursor:pointer;height:40px;margin-right:20px;line-height:40px;"><span style="color:Red;">恭喜</span><font color="blue"><asp:Literal ID="ltlName" runat="server" Text='<%#Eval("HuiYuanID") %>'></asp:Literal></font>&nbsp;以<asp:Label ID="lblPrice" runat="server">￥<%#Eval("AuctionPrice") %></asp:Label>赢得了<asp:Literal ID="ltlProName" runat="server" ></asp:Literal></a>
                                </ItemTemplate>
                            </asp:DataList>                        
                       </div> 
                       <div id="marquee2" style="float:left;"></div> 
                       </div>
                   </div>                 
                </div>
                <!--图片轮播-->
                <div id="picplayer">
                </div>
            </div>
            <div class="top_right1">
                <!--新闻公告-->
                <div class="news">
                    <div class="title">
                        <span><strong>&nbsp;新闻公告</strong><a class="more" href="" target="_self">更多</a></span>
                    </div>
                    <div class="content">
                    <ul>
                        <asp:DataList ID="dlstNews" runat="server" width="100%">
                            <ItemTemplate>
                                <li><a href="" target="_self"><%#Eval("Tile") %></a></li>
                            </ItemTemplate>
                        </asp:DataList>
                    </ul>
                    </div>
                </div>
                <!--新手上路-->
                <div class="newbie">
                    <img src="../Auction/Images/register_ad.jpg" width="245px" height="180px" />
                </div>
            </div>
        </div>
        <!--正在热拍-->
        <div class="selling">
            <div class="title">
                <span><a href="" target="_self">更多>></a></span>
            </div>
            <div class="content">
              <asp:UpdatePanel ID="updatepanel1" runat="server">
                <ContentTemplate>
                <asp:Timer ID="timer1" runat="server" Interval="1000" ontick="timer1_Tick"></asp:Timer>    
                <asp:DataList ID="dlstProduct" runat="server" RepeatColumns="5" 
                    onitemcommand="dlstProduct_ItemCommand" DataKeyField="AuctionID"
                    onitemdatabound="dlstProduct_ItemDataBound">
                    <ItemTemplate>
                        <div class="product_area">
                            <div class="product_name"><asp:HyperLink ID="hlnkProName" runat="server" Target="_self">&nbsp;<span style="color:Red"><asp:Literal ID="ltlIntro" runat="server"></asp:Literal></span></asp:HyperLink>
                            <asp:HiddenField ID="hfProductNo" runat="server" Value='<%#Eval("Coding") %>' />
                            <asp:HiddenField ID="hfAuctionID" runat="server" Value='<%#Eval("AuctionID") %>' /> 
                           </div>
                           <div class="product_fullprice">
                                    <img src="Images/fullprice.png" title="若竞拍未成功，可以按市场价补差价购买此商品！" alt="" width="16px" height="16px" />
                                </div>                           
                           <div class="product_img">
                               <a href="../Auction/ProDetail.aspx?id=<%#Eval("AuctionID") %>" target="_self"><asp:Image ID="imgProduct" runat="server" Width="120px" Height="120px" /></a><asp:HiddenField ID="hfProductID" runat="server" Value='<%#Eval("ProductID") %>' />
                           </div>
                            <div class="product_price">市场价：<span style="font-weight:bold;">￥<asp:Label ID="lblMarketPrice" runat="server" ></asp:Label></span></div>                       
                            <div class="product_price">拍卖价：<span style="color:Red;font-weight:bold;font-family:Arial;">￥<asp:Label ID="lblAuctionPrice" runat="server" Text='<%#Eval("AuctionPrice") %>'></asp:Label></span></div>
                            <div class="product_price">出价人：<span style="color:#00666b"><asp:Label ID="lblMemberName" runat="server"></asp:Label><asp:HiddenField ID="hfMemberID" runat="server" Value='<%#Eval("HuiYuanID") %>' /></span></div>                                             
                            <div class="product_timer">
                               <asp:Label ID="lblTimer" runat="server"></asp:Label>
                               <asp:HiddenField ID="hfTimePoint" runat="server" Value='<%#Eval("TimePoint") %>' />
                               <asp:HiddenField ID="hfAuctionTime" runat="server" Value='<%#Eval("AuctionTime") %>' /> 
                               <asp:HiddenField ID="hfStatus" runat="server" Value='<%#Eval("Status") %>' />                                                                                              
                            </div>                           
                            <div class="product_auction">
                                <asp:ImageButton ID="imgbtnAuction" runat="server" ImageUrl="Images/bid_button.gif" CommandName="auction" CommandArgument='<%#Eval("AuctionID") %>' />
                                <asp:HiddenField ID="hfAuctionPoint" runat="server" Value='<%#Eval("AuctionPoint") %>' />
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:DataList>
              </ContentTemplate>
            </asp:UpdatePanel>
            </div>
        </div>
        <!--免费竞拍-->
        <div class="free_acution">
            <div class="title"></div>
            <div class="content">
                <asp:DataList ID="dlstFree" runat="server">
                    <ItemTemplate>
                        
                    </ItemTemplate>
                </asp:DataList>
            </div>
        </div>
        <!--最新成交-->
        <div class="newest_done">
            <div class="title">
                
            </div>
            <div>
                <asp:DataList ID="dlstDone" runat="server" RepeatColumns="5" 
                    onitemdatabound="dlstDone_ItemDataBound" DataKeyField="AuctionID">
                    <ItemTemplate>
                        <div class="product_area">
                            <div class="product_name">
                                <a href="../Auction/ProDetail.aspx?id=<%#Eval("AuctionID") %>" target="_self"><asp:Literal ID="ltlProName" runat="server"></asp:Literal>&nbsp;<span style="color:Red"><asp:Literal ID="ltlIntro" runat="server"></asp:Literal></span></a></div>
                            <div class="product_fullprice">
                                <img src="Images/fullprice.png" title="若竞拍未成功，可以按市场价补差价购买此商品！" alt="" width="16px" height="16px" />
                            </div>
                            <div class="product_img">
                                <a href="../Auction/ProDetail.aspx?id=<%#Eval("AuctionID") %>" target="_self"><asp:Image ID="imgProduct" runat="server" Width="120px" Height="120px" /></a>
                                <asp:HiddenField ID="hfProductID" runat="server" Value='<%#Eval("ProductID") %>' />
                                <asp:HiddenField ID="hfProductNo" runat="server" Value='<%#Eval("coding") %>' />                                
                            </div>
                            <div class="product_price">
                                市场价：<span style="font-weight:bold;">￥<asp:Label ID="lblMarketPrice" runat="server" ></asp:Label></span>
                            </div>
                            <div class="product_price">
                                成交价：<span style="color:Red;font-weight:bold;font-family:Arial;">￥<asp:Label ID="lblAuctionPrice" runat="server" Text='<%#Eval("AuctionPrice") %>'></asp:Label></span>
                            </div>
                            <div class="product_price">
                                获得者：<span style="color:#00666b"><asp:Label ID="lblMemberName" runat="server"></asp:Label><asp:HiddenField ID="hfMemberID" runat="server" Value='<%#Eval("HuiYuanID") %>' /></span>
                            </div>
                            <div class="product_endtime">
                                <div style="float:left;width:60px;">成交时间：</div><asp:Label ID="lblEndTime" runat="server" Text='<%#Eval("EndTime") %>' Width="50px"></asp:Label>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:DataList>
            </div>
        </div>
        <!--拍客晒图-->
        <div class="showpic">
            <div class="title"></div>
                <asp:DataList ID="dlstShowPic" runat="server" RepeatColumns="2" 
                    onitemdatabound="dlstShowPic_ItemDataBound" DataKeyField="OrderID" class="showpic_con">
                    <ItemTemplate>
                        <div class="showpic_area">
                            <div class="showpic_area_top">
                                <div class="showpic_area_avatar"><asp:Image ID="imgAvatar" runat="server" Width="50px" Height="50px" ImageUrl="../Image/default_avatar.gif" /><div class="showpic_area_name"><asp:Literal ID="ltlMemberName" runat="server"></asp:Literal></div></div>
                                <div class="showpic_area_info">
                                    <ul>
                                        <li class="showpic_area_ttl"><asp:HyperLink ID="hlnkPro" runat="server" Target="_blank"><%#Eval("Title") %></asp:HyperLink></li>
                                        <li class="showpic_area_pro">成交价：<asp:Label ID="lblDonePrice" runat="server"></asp:Label>&nbsp;&nbsp;市场价：<asp:Label ID="lblMarketPrice" runat="server"></asp:Label></li>
                                        <li class="showpic_area_detail">评价：<br /><asp:Label ID="lblDetail" runat="server" Text='<%#Eval("Detail") %>'></asp:Label></li>
                                    </ul>
                                </div>
                            </div>
                            <div class="showpic_area_bottom">
                                <asp:Image ID="img1" runat="server" Width="90px" Height="90px" />&nbsp;<asp:Image ID="img2" runat="server" Width="90px" Height="90px" />&nbsp;
                                <asp:Image ID="img3" runat="server" Width="90px" Height="90px" />&nbsp;<asp:Image ID="img4" runat="server" Width="90px" Height="90px" />
                                <asp:HiddenField ID="hfShowOrderID" runat="server" Value='<%#Eval("ShowOrderID") %>' />
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:DataList>
        </div>
    </div>
    <script type="text/javascript">
        var p = $('#picplayer');
        var pics1 = [{ url: '../Image/banner1.jpg', link: 'http://www.jb51.net/#', time: 5000 }, { url: '../Image/banner2.jpg', link: 'http://www.jb51.net/#', time: 4000 },
{ url: '../Image/banner3.jpg', link: 'http://www.jb51.net', time: 6000 }, { url: 'http://img.jb51.net/online/picPlayer/2.jpg', link: 'http://www.jb51.net', time: 6000 },
{ url: 'http://img.jb51.net/online/picPlayer/1.jpg', link: 'http://www.jb51.net', time: 6000}];
        initPicPlayer(pics1, p.css('width').split('px')[0], p.css('height').split('px')[0]);
        function initPicPlayer(pics, w, h) {
            //选中的图片 
            var selectedItem;
            //选中的按钮 
            var selectedBtn;
            //自动播放的id 
            var playID;
            //选中图片的索引 
            var selectedIndex;
            //容器 
            var p = $('#picplayer');
            p.text('');
            p.append('<div id="piccontent"></div>');
            var c = $('#piccontent');
            for (var i = 0; i < pics.length; i++) {
                //添加图片到容器中 
                c.append('<a href="' + pics[i].link + '" target="_blank"><img id="picitem' + i + '" style="display: none;z-index:' + i + '" src="' + pics[i].url + '" /></a>');
            }
            //按钮容器，绝对定位在右下角 
            p.append('<div id="picbtnHolder" style="position:absolute;top:' + (h - 30) + 'px;width:' + w + 'px;height:20px;z-index:10000;"></div>');
            // 
            var btnHolder = $('#picbtnHolder');
            btnHolder.append('<div id="picbtns" style="float:right; padding-right:10px;"></div>');
            var btns = $('#picbtns');
            // 
            for (var i = 0; i < pics.length; i++) {
                //增加图片对应的按钮 
                btns.append('<span id="picbtn' + i + '" style="cursor:pointer; border:solid 1px #ccc;background-color:#eee; display:inline-block;width:16px;height:16px;text-align:center;margin-left:1px;"> ' + (i + 1) + ' </span> ');
                $('#picbtn' + i).data('index', i);
                $('#picbtn' + i).click(
                        function (event) {
                            if (selectedItem.attr('src') == $('#picitem' + $(this).data('index')).attr('src')) {
                                return;
                            }
                            setSelectedItem($(this).data('index'));
                        }
                    );
            }
            btns.append(' ');
            /// 
            setSelectedItem(0);
            //显示指定的图片index 
            function setSelectedItem(index) {
                selectedIndex = index;
                clearInterval(playID);
                //alert(index); 
                if (selectedItem) selectedItem.fadeOut('fast');
                selectedItem = $('#picitem' + index);
                selectedItem.fadeIn('slow');
                // 
                if (selectedBtn) {
                    selectedBtn.css('backgroundColor', '#eee');
                    selectedBtn.css('color', '#000');
                }
                selectedBtn = $('#picbtn' + index);
                selectedBtn.css('backgroundColor', '#8f0100');
                selectedBtn.css('color', '#fff');
                //自动播放 
                playID = setInterval(function () {
                    var index = selectedIndex + 1;
                    if (index > pics.length - 1) index = 0;
                    setSelectedItem(index);
                }, pics[index].time);
            }
        }

        //文字滚动 
        var speed = 20;
        var tab = document.getElementById("marquee");
        var tab1 = document.getElementById("marquee1");
        var tab2 = document.getElementById("marquee2");
        tab2.innerHTML = tab1.innerHTML;
        function Marquee() {
            if (tab2.offsetWidth - tab.scrollLeft <= 0)
                tab.scrollLeft -= tab1.offsetWidth
            else {
                tab.scrollLeft++;
            }
        }
        var MyMar = setInterval(Marquee, speed);
        tab.onmouseover = function () { clearInterval(MyMar) };
        tab.onmouseout = function () { MyMar = setInterval(Marquee, speed) };
    </script>
</asp:Content>
