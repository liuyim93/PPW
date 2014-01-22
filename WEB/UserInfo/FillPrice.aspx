<%@ Page Title="" Language="C#" MasterPageFile="~/Auction/UserCenter.master" AutoEventWireup="true" CodeBehind="FillPrice.aspx.cs" Inherits="WEB.UserInfo.FillPrice" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content4" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content3" runat="server">
    <div>
        <div>补差价订单</div>
        <div>
            <div>
                <div>选择收货地址</div>
                <div>
                    <asp:Repeater ID="repeater_adr" runat="server">
                        <ItemTemplate>
                            <div>                                                           
                                <input type="radio" id="radio_<%#Eval("ShouHuoDZID") %>" name="adr" />&nbsp;&nbsp;<span><%#Eval("ShouHuoName") %></span>&nbsp;&nbsp;<span><%#Eval("DZ") %></span>&nbsp;&nbsp;<span><%#Eval("Mode") %></span></div>
                        </ItemTemplate>
                    </asp:Repeater>
                    <input type="radio" id="radio_AddAdr" name="adr" />添加收货地址
                </div>
            </div>
            <div>
                <div><a href="" target="_self">未支付</a>|<a href="" target="_self">已支付</a>|<a href="" target="_self">过期及取消</a></div>
                <div>
                    <asp:Repeater ID="repeater_order" runat="server">                        
                        <ItemTemplate>
                            <div>
                                <div></div>
                                <div></div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
