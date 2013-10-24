<%@ Page Title="" Language="C#" MasterPageFile="~/Auction/UserCenter.master" AutoEventWireup="true" CodeBehind="ShipAdress.aspx.cs" Inherits="WEB.UserInfo.ShipAdress" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content4" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content3" runat="server">
    <asp:UpdatePanel ID="updatepanel1" runat="server">
    <ContentTemplate>
        <div class="shipadress">
        <div class="shipadress_title">收货地址</div>
        <div class="shipadress_select">
            <asp:LinkButton ID="lbtnAdressList" runat="server" 
                onclick="lbtnAdressList_Click">地址列表</asp:LinkButton>|<asp:LinkButton 
                ID="lbtnAddAdress" runat="server" onclick="lbtnAddAdress_Click">添加收货地址</asp:LinkButton>
        </div>        
        <asp:Panel ID="pnlAdressList" runat="server" CssClass="adresslist">
            <div class="adresslist_title">地址列表</div>
            <asp:GridView ID="gvwAdress" runat="server" AutoGenerateColumns="false" 
                onrowcommand="gvwAdress_RowCommand" GridLines="None" CssClass="adresslist_gvw">
                <Columns>
                    <asp:TemplateField HeaderText="收货人姓名">
                        <ItemTemplate>
                            <asp:Label ID="lblName" runat="server" Text='<%#Eval("ShouHuoName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="联系电话">
                        <ItemTemplate>
                            <asp:Label ID="lblPhone" runat="server" Text='<%#Eval("Mode") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="收货地址" ItemStyle-Width="200px">
                        <ItemTemplate>
                            <asp:Label ID="lblAdress" runat="server" Text='<%#Eval("DZ") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="邮编" ItemStyle-Width="100px">
                        <ItemTemplate>
                            <asp:Label ID="lblPostCode" runat="server" Text='<%#Eval("YouBian") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="操作">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnUpdate" runat="server" CommandName="change" CommandArgument='<%#Eval("ShouHuoDZID") %>'>编辑</asp:LinkButton>&nbsp;&nbsp;
                            <asp:LinkButton ID="lbtnDel" runat="server" CommandName="del" CommandArgument='<%#Eval("ShouHuoDZID") %>' OnClientClick="return confirm('确定删除吗？')">删除</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </asp:Panel>
        <asp:Panel ID="pnlUpdateAdress" runat="server" class="updateadress" Visible="false">
            <div class="updateadress_tr">
                <div class="updateadress_tr_left"><span style="color:Red">*</span>收货人：</div>
                <div class="updateadress_tr_right">
                    <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                    <asp:HiddenField ID="hfAdressID" runat="server" />
                    <asp:HiddenField ID="hfSelect" runat="server" />
               </div>
            </div>
            <div class="updateadress_adress">
                <div class="updateadress_adress_left"><span style="color:Red">*</span>收货地址：</div>
                <div class="updateadress_adress_right"><asp:TextBox ID="txtAdress" runat="server" TextMode="MultiLine"></asp:TextBox></div>
            </div>
            <div class="updateadress_tr">
                <div class="updateadress_tr_left"><span style="color:Red">*</span>联系电话：</div>
                <div class="updateadress_tr_right"><asp:TextBox ID="txtPhone" runat="server"></asp:TextBox></div>
            </div>
            <div class="updateadress_tr">
                <div class="updateadress_tr_left"><span style="color:Red">*</span>邮编：</div>
                <div class="updateadress_tr_right"><asp:TextBox ID="txtPostCode" runat="server"></asp:TextBox></div>
            </div>
            <div class="updateadress_adress">
                <div class="updateadress_adress_left">备注：</div>
                <div class="updateadress_adress_right"><asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine"></asp:TextBox></div>
            </div>
            <div class="updateadress_save"><asp:Button ID="btnSave" runat="server" Text="保存" onclick="btnSave_Click" /></div>
        </asp:Panel>
    </div>
    </ContentTemplate>
</asp:UpdatePanel>    
</asp:Content>
