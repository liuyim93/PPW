<%@ Page Title="" Language="C#" MasterPageFile="~/Auction/UserCenter.master" AutoEventWireup="true" CodeBehind="Exchange.aspx.cs" Inherits="WEB.UserInfo.Exchange" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content4" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content3" runat="server">
    <div class="gift">
        <div class="gift_title">积分兑换</div>
        <div class="gift_con">
            <asp:DataList ID="dlstExchange" runat="server" Width="100%" 
                DataKeyField="ProductID" onitemdatabound="dlstExchange_ItemDataBound">
                <HeaderTemplate>
                    <ul class="gift_head">
                        <li style="width:200px;">商品名称</li>
                        <li style="width:100px;">商品价格</li>
                        <li style="width:100px;">积分</li>
                        <li style="width:100px;">兑换时间</li>
                        <li style="width:100px;">收货人</li>
                        <li style="width:80px;">状态</li>
                        <li style="width:80px;">操作</li>
                    </ul>
                </HeaderTemplate>
                <ItemTemplate>
                    <div class="gift_area">
                        <ul>
                            <li class="gift_area_img"><a href="../Auction/PointsMall_Detail.aspx?id=<%#Eval("ProductID") %>" target="_self"><asp:Image ID="imgPro" runat="server" Width="70px" Height="70px" /></a></li>
                            <li class="gift_area_name"><a href="../Auction/PointsMall_Detail.aspx?id=<%#Eval("ProductID") %>" target="_self"><asp:Literal ID="ltlProName" runat="server"></asp:Literal></a></li>
                            <li class="gift_area_price"><asp:Label ID="lblPrice" runat="server">￥<%#Eval("ProductPrice") %></asp:Label></li>
                            <li class="gift_area_points"><asp:Label ID="lblPoints" runat="server"></asp:Label></li>
                            <li class="gift_area_time"><asp:Label ID="lblTime" runat="server" Text='<%#Eval("DingDanTime") %>'></asp:Label></li>
                            <li class="gift_area_points"><asp:Label ID="lblShipAdress" runat="server" Text='<%#Eval("ShouHuoDZID") %>'></asp:Label></li>
                            <li class="gift_area_points"><asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Status") %>'></asp:Label></li>
                            <li class="gift_area_points"><asp:LinkButton ID="lbtnCancel" runat="server" CommandName="cancel" CommandArgument='<%#Eval("DingDanID") %>'>取消</asp:LinkButton></li>
                        </ul>
                    </div>
                </ItemTemplate>
            </asp:DataList>
        </div>
    </div>
</asp:Content>
