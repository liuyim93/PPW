<%@ Page Title="" Language="C#" MasterPageFile="~/Auction/Main.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="WEB.Auction.Index" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Styles/Style.css" rel="Stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="width:100%;height:100%;">
        <div class="index_top">
            <div class="top_left">
                <!--广播-->
                <div class="broadcast">
                    
                </div>
                <!--图片轮播-->
                <div class="pic_baner">
                    
                </div>
            </div>
            <div class="top_right">
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
                <asp:Timer ID="timer1" runat="server" Interval="1000"></asp:Timer>    
                <asp:DataList ID="dlstProduct" runat="server" RepeatColumns="5" Width="100%" 
                    onitemcommand="dlstProduct_ItemCommand" 
                    onitemdatabound="dlstProduct_ItemDataBound">
                    <ItemTemplate>
                        <div class="product_area">
                            <div class="product_name"><a href="" target="_self"><%#Eval("ProductName") %>&nbsp;<span style="color:Red"><%#Eval("Intro") %></span></a>
                            <asp:HiddenField ID="hfProductName" runat="server" Value='<%#Eval("ProductName") %>' />
                            <asp:HiddenField ID="hfProductNo" runat="server" Value='<%#Eval("coding") %>' /> 
                           </div>
                           <div class="product_fullprice">
                                    <img src="Images/fullprice.png" title="若竞拍未成功，可以按市场价补差价购买此商品！" alt="" width="16px" height="16px" />
                                </div>                           
                           <div class="product_img">
                               <a href="" target="_self"><asp:Image ID="imgProduct" runat="server" Width="120px" Height="120px" /></a><asp:HiddenField ID="hfProductID" runat="server" Value='<%#Eval("ProductID") %>' />
                           </div>
                            <div class="product_price">市场价：<span style="font-weight:bold;"><asp:Label ID="lblMarketPrice" runat="server">￥<%#Eval("productPrice") %></asp:Label></span></div>                       
                            <div class="product_price">拍卖价：<span style="color:Red;font-weight:bold;font-family:Arial;"><asp:Label ID="lblAuctionPrice" runat="server">￥<%#Eval("PmJGproduct") %></asp:Label></span></div>
                            <div class="product_price">出价人：<span style="color:#00666b"><asp:Label ID="lblMemberName" runat="server" Text='<%#Eval("HuiYuanID") %>'></asp:Label></span></div>                                             
                            <div class="product_timer">
                               <asp:Label ID="lblTimer" runat="server" Text='<%#Eval("AuctionTime") %>'></asp:Label>
                               <asp:HiddenField ID="hfTimePoint" runat="server" Value='<%#Eval("TimePoint") %>' />                                                                   
                            </div>                           
                            <div class="product_auction">
                                <asp:ImageButton ID="imgbtnAuction" runat="server" ImageUrl="Images/bid_button.gif" CommandName="auction" CommandArgument='<%#Eval("ProductID") %>' />
                                <asp:HiddenField ID="hfAuctionPoint" runat="server" Value='<%#Eval("AuctionPoint") %>' />
                                <asp:Button ID="btnSubmit" runat="server" Visible="false" CommandName="submit" />
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
                <asp:DataList ID="dlstDone" runat="server">
                    <ItemTemplate>
                        
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
                        
                    </ItemTemplate>
                </asp:DataList>
            </div>
        </div>
    </div>
</asp:Content>
