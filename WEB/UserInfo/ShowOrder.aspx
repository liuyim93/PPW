<%@ Page Title="" Language="C#" MasterPageFile="~/Auction/Main.Master" AutoEventWireup="true" CodeBehind="ShowOrder.aspx.cs" Inherits="WEB.UserInfo.ShowOrder" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <div></div>
        <div>
            <div>
                <div>
                    商品信息：<a id="link_Pro" runat="server"><asp:Image ID="imgPro" runat="server" Width="70px"  Height="70px" /></a>&nbsp;<a id="link_Pro1" runat="server"></a>
                </div>
                <div>标题：<asp:TextBox ID="txtTitle" runat="server"></asp:TextBox></div>
                <div>内容：<asp:TextBox ID="txtDetail" runat="server" TextMode="MultiLine"></asp:TextBox></div>
                <div>上传图片：<asp:FileUpload ID="fileupload" runat="server"  />&nbsp;<asp:Button 
                        ID="btnUpload" runat="server" Text="上传" onclick="btnUpload_Click" /><br />
                        <asp:Literal ID="ltlStatus" runat="server"></asp:Literal>
                </div>
                <asp:Panel ID="pnlImg" runat="server">
                    <asp:Panel ID="pnlImg1" runat="server" Visible="false">
                        <asp:Image ID="img1" runat="server" Width="100px" Height="100px" /><span>
                        <asp:LinkButton ID="lbtnDel1" runat="server" onclick="lbtnDel1_Click">删除</asp:LinkButton></span>
                    </asp:Panel>
                    <asp:Panel ID="pnlImg2" runat="server" Visible="false">
                        <asp:Image ID="img2" runat="server" Width="100px" Height="100px" /><span>
                        <asp:LinkButton ID="lbtnDel2" runat="server" onclick="lbtnDel2_Click">删除</asp:LinkButton></span>
                    </asp:Panel>
                    <asp:Panel ID="pnlImg3" runat="server" Visible="false">
                        <asp:Image ID="img3" runat="server" Width="100px" Height="100px" /><span>
                        <asp:LinkButton ID="lbtnDel3" runat="server" onclick="lbtnDel3_Click">删除</asp:LinkButton></span>
                    </asp:Panel>
                    <asp:Panel ID="pnlImg4" runat="server" Visible="false">
                        <asp:Image ID="img4" runat="server" Width="100px" Height="100px" /><span>
                        <asp:LinkButton ID="lbtnDel4" runat="server" onclick="lbtnDel4_Click">删除</asp:LinkButton></span>
                    </asp:Panel>
                </asp:Panel>
                <div><asp:Button ID="btnSubmit" runat="server" Text="提交" 
                        onclick="btnSubmit_Click" /></div>
            </div>
            <div>
                关于晒单：<br />
            </div>
        </div>
    </div>
</asp:Content>
