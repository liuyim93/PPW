<%@ Page Title="" Language="C#" MasterPageFile="~/Auction/Main.Master" AutoEventWireup="true" CodeBehind="PointsMall.aspx.cs" Inherits="WEB.Auction.PointsMall" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <div class="pointsmall">
            <div class="pointsmall_title">积分商城</div>
            <div class="pointsmall_search">
                <div class="pointsmall_drop">产品类型：<asp:DropDownList ID="dropProType" runat="server"></asp:DropDownList>&nbsp;&nbsp;
                     积分：<asp:DropDownList ID="dropPoints" runat="server">
                        <asp:ListItem Text="不限" Value="0-0"></asp:ListItem>
                        <asp:ListItem Text="500以下" Value="0-500"></asp:ListItem>
                        <asp:ListItem Text="500~1000" Value="500-1000"></asp:ListItem>
                        <asp:ListItem Text="1000~2000" Value="1000-2000"></asp:ListItem> 
                        <asp:ListItem Text="2000以上" Value="2000-"></asp:ListItem>
                      </asp:DropDownList>
                  </div>
                  <div class="pointsmall_btn"><asp:LinkButton ID="lbtnSearch" runat="server" onclick="lbtnSearch_Click">搜索</asp:LinkButton></div>
            </div>
            <asp:DataList ID="dlstExchange" runat="server" DataKeyField="ProductID" 
                onitemdatabound="dlstExchange_ItemDataBound" RepeatColumns="5">
                <ItemTemplate>
                    <div class="pointsmall_area">
                        <div class="pointsmall_area_img"><a href="../Auction/PointsMall_Detail.aspx?id=<%#Eval("ProductID") %>" target="_self"><asp:Image ID="imgPro" runat="server" Width="120px" Height="120px" ToolTip='<%#Eval("productName") %>' /></a></div>
                        <div class="pointsmall_area_name"><a href="../Auction/PointsMall_Detail.aspx?id=<%#Eval("ProductID") %>" target="_self"><%#Eval("productName") %></a></div>
                        <div class="pointsmall_area_price">市场价：<asp:Label ID="lblProPrice" runat="server" >￥<%#Eval("productPrice") %></asp:Label></div>
                        <div class="pointsmall_area_points">积分：<asp:Label ID="lblPoints" runat="server"><%#Eval("Points") %></asp:Label></div>
                        <div style="text-align:center;margin-top:7px;"><a href="../Auction/PointsMall_Detail.aspx?id=<%#Eval("ProductID") %>" target="_self"><img src="Images/btn_exchange.jpg" alt="立即兑换" /></a></div>
                    </div>
                </ItemTemplate>
            </asp:DataList>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
