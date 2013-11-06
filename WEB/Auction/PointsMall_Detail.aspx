<%@ Page Title="" Language="C#" MasterPageFile="~/Auction/Main.Master" AutoEventWireup="true" CodeBehind="PointsMall_Detail.aspx.cs" Inherits="WEB.Auction.PointsMall_Detail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="pointsdetail">
        <div class="pointsdetail_top">
            <div class="pointsdetail_left">
                <div class="pointsdetail_imgpro"><asp:Image ID="imgPro" runat="server" Width="120px" Height="120px" /></div>
                <div class="pointsdetail_img">
                    <asp:Image ID="img1" runat="server" Width="50px" Height="50px" />
                    <asp:Image ID="img2" runat="server" Width="50px" Height="50px" />
                    <asp:Image ID="img3" runat="server" Width="50px" Height="50px" />
                </div>
            </div>  
            <div class="pointsdetail_right">
                <div class="pointsdetail_name"><asp:Label ID="lblProName" runat="server"></asp:Label></div>
                <div class="pointsdetail_price">市场价：<asp:Label ID="lblPrice" runat="server"></asp:Label></div>
                <div class="pointsdetail_points">所需积分：<asp:Label ID="lblPoints" runat="server"></asp:Label></div>
                <div class="pointsdetail_curpoints">当前积分：<asp:Label ID="lblCurPoints" runat="server"></asp:Label></div>
                <div class="pointsdetail_btn"><asp:Button ID="btnSubmit" runat="server" Text="立即兑换" 
                        onclick="btnSubmit_Click" /></div>
            </div> 
        </div>
        <div class="pointsdetail_bottom">
            <div class="pointsdetail_title">商品详情</div>
            <div class="pointsdetail_detail"><asp:Label ID="lblDetail" runat="server"></asp:Label></div>
        </div>
    </div>
</asp:Content>
