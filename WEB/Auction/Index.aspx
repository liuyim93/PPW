<%@ Page Title="" ValidateRequest="false" Language="C#" MasterPageFile="~/Auction/Main.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="WEB.Auction.Index" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Styles/Style.css" rel="Stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="width:100%;height:100%;">
        <div id="index_top">
            <div class="top_left">
                <!--广播-->
                <div class="broadcast">
                    <marquee></marquee>
                </div>
                <!--图片轮播-->
                <div class="pic_baner">
                    
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
                <asp:Timer ID="timer1" runat="server" Interval="1200" ontick="timer1_Tick"></asp:Timer>    
                <asp:DataList ID="dlstProduct" runat="server" RepeatColumns="5" Width="100%" 
                    onitemcommand="dlstProduct_ItemCommand" 
                    onitemdatabound="dlstProduct_ItemDataBound">
                    <ItemTemplate>
                        <div class="product_area">
                            <div class="product_name"><a href="ProDetail.aspx?id=<%#Eval("ProductID") %>" target="_self"><%#Eval("ProductName") %>&nbsp;<span style="color:Red"><%#Eval("Intro") %></span></a><asp:HiddenField ID="hfProductName" runat="server" Value='<%#Eval("ProductName") %>' />
                            <asp:HiddenField ID="hfProductNo" runat="server" Value='<%#Eval("coding") %>' /> 
                           </div>
                           <div class="product_fullprice">
                                    <img src="Images/fullprice.png" title="若竞拍未成功，可以按市场价补差价购买此商品！" alt="" width="16px" height="16px" />
                                </div>                           
                           <div class="product_img">
                               <a href="ProDetail.aspx?id=<%#Eval("ProductID") %>" target="_self"><asp:Image ID="imgProduct" runat="server" Width="120px" Height="120px" /></a><asp:HiddenField ID="hfProductID" runat="server" Value='<%#Eval("ProductID") %>' />
                           </div>
                            <div class="product_price">市场价：<span style="font-weight:bold;">￥<asp:Label ID="lblMarketPrice" runat="server" Text='<%#Eval("productPrice") %>'></asp:Label></span></div>                       
                            <div class="product_price">拍卖价：<span style="color:Red;font-weight:bold;font-family:Arial;">￥<asp:Label ID="lblAuctionPrice" runat="server" Text='<%#Eval("PmJGproduct") %>'></asp:Label></span></div>
                            <div class="product_price">出价人：<span style="color:#00666b"><asp:Label ID="lblMemberName" runat="server"></asp:Label><asp:HiddenField ID="hfMemberID" runat="server" Value='<%#Eval("HuiYuanID") %>' /></span></div>                                             
                            <div class="product_timer">
                               <asp:Label ID="lblTimer" runat="server"></asp:Label>
                               <asp:HiddenField ID="hfTimePoint" runat="server" Value='<%#Eval("TimePoint") %>' />
                               <asp:HiddenField ID="hfAuctionTime" runat="server" Value='<%#Eval("AuctionTime") %>' /> 
                               <asp:HiddenField ID="hfStatus" runat="server" Value='<%#Eval("Status") %>' />
                               <!--运费、手续费 -->                                                               
                               <asp:HiddenField ID="hfFee" runat="server" Value='<%#Eval("Fee") %>' />
                               <asp:HiddenField ID="hfShipFee" runat="server" Value='<%#Eval("ShipFee") %>' />
                            </div>                           
                            <div class="product_auction">
                                <asp:ImageButton ID="imgbtnAuction" runat="server" ImageUrl="Images/bid_button.gif" CommandName="auction" CommandArgument='<%#Eval("ProductID") %>' />
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
                    onitemdatabound="dlstDone_ItemDataBound">
                    <ItemTemplate>
                        <div class="product_area">
                            <div class="product_name">
                                <a href="ProDetail.aspx?id=<%#Eval("ProductID") %>" target="_self"><%#Eval("productName") %>&nbsp;<span style="color:Red"><%#Eval("Intro") %></span></a></div>
                            <div class="product_fullprice">
                                <img src="Images/fullprice.png" title="若竞拍未成功，可以按市场价补差价购买此商品！" alt="" width="16px" height="16px" />
                            </div>
                            <div class="product_img">
                                <a href="ProDetail.aspx?id=<%#Eval("ProductID") %>" target="_self"><asp:Image ID="imgProduct" runat="server" Width="120px" Height="120px" /></a>
                                <asp:HiddenField ID="hfProductID" runat="server" Value='<%#Eval("ProductID") %>' />
                                <asp:HiddenField ID="hfProductNo" runat="server" Value='<%#Eval("coding") %>' />
                                <asp:HiddenField ID="hfProductName" runat="server" Value='<%#Eval("ProductName") %>' />
                            </div>
                            <div class="product_price">
                                市场价：<span style="font-weight:bold;">￥<asp:Label ID="lblMarketPrice" runat="server" Text='<%#Eval("productPrice") %>'></asp:Label></span>
                            </div>
                            <div class="product_price">
                                成交价：<span style="color:Red;font-weight:bold;font-family:Arial;">￥<asp:Label ID="lblAuctionPrice" runat="server" Text='<%#Eval("PmJGproduct") %>'></asp:Label></span>
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
            <div>
                <asp:DataList ID="dlstPic" runat="server">
                    <ItemTemplate>
                        <div>
                            
                        </div>
                    </ItemTemplate>
                </asp:DataList>
            </div>
        </div>
    </div>
</asp:Content>
