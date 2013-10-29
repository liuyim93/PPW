<%@ Page Title="" Language="C#" MasterPageFile="~/Auction/Main.Master" AutoEventWireup="true" CodeBehind="ShowOrder.aspx.cs" Inherits="WEB.UserInfo.ShowOrder" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="showorder">
        <div class="showorder_title">拍客晒图<span>&nbsp;ShowOrder</span></div>
        <div class="showorder_content">
            <div class="showorder_left">
                <div class="showorder_pro">
                    商品信息：<a id="link_Pro" runat="server"><asp:Image ID="imgPro" runat="server" Width="70px"  Height="70px" /></a>&nbsp;<a id="link_Pro1" runat="server"></a>
                </div>
                <div class="showorder_left_title"><span style="color:Red">*</span>标题：<asp:TextBox ID="txtTitle" runat="server" Width="500px" Height="20px"></asp:TextBox></div>
                <div class="showorder_left_content"><span style="color:Red">*</span>内容：<asp:TextBox ID="txtDetail" runat="server" TextMode="MultiLine" Width="500px" Height="150px"></asp:TextBox></div>
                <div class="showorder_addpic">上传图片：<asp:FileUpload ID="fileupload" runat="server"  />&nbsp;<asp:Button 
                        ID="btnUpload" runat="server" Text="上传" onclick="btnUpload_Click" /><br />
                        <span style="margin-left:10px"><asp:Literal ID="ltlStatus" runat="server"></asp:Literal></span>
                </div>
                <asp:Panel ID="pnlImg" runat="server" class="showorder_pic">
                请上传2-4张图片，每张图片不超过1M，支持的图片格式为jpg，jpeg，bmp，png，gif<br />
                    <asp:Panel ID="pnlImg1" runat="server" Visible="false" class="showorder_showpic">
                        <asp:Image ID="img1" runat="server" Width="100px" Height="100px" /><span>
                        <asp:LinkButton ID="lbtnDel1" runat="server" onclick="lbtnDel1_Click">删除</asp:LinkButton></span>
                    </asp:Panel>
                    <asp:Panel ID="pnlImg2" runat="server" Visible="false" class="showorder_showpic">
                        <asp:Image ID="img2" runat="server" Width="100px" Height="100px" /><span>
                        <asp:LinkButton ID="lbtnDel2" runat="server" onclick="lbtnDel2_Click">删除</asp:LinkButton></span>
                    </asp:Panel>
                    <asp:Panel ID="pnlImg3" runat="server" Visible="false" class="showorder_showpic">
                        <asp:Image ID="img3" runat="server" Width="100px" Height="100px" /><span>
                        <asp:LinkButton ID="lbtnDel3" runat="server" onclick="lbtnDel3_Click">删除</asp:LinkButton></span>
                    </asp:Panel>
                    <asp:Panel ID="pnlImg4" runat="server" Visible="false" class="showorder_showpic">
                        <asp:Image ID="img4" runat="server" Width="100px" Height="100px" /><span>
                        <asp:LinkButton ID="lbtnDel4" runat="server" onclick="lbtnDel4_Click">删除</asp:LinkButton></span>
                    </asp:Panel>
                </asp:Panel>
                <div class="showorder_btn"><asp:Button ID="btnSubmit" runat="server" Text="提交" 
                        onclick="btnSubmit_Click" /></div>
            </div>
            <div class="showorder_right">
                关于晒单：<br />                
    • 晒图数量不得少于2张,为提高上传速度请将图片压缩至500K以下。<br />
    • 审核通过的晒单，奖励底分为10分;<br />
    • 上传商品与本人的露脸照且在同一照片中加20分;<br />
    • 评论的内容真切，上传的图片制作精美、清晰，酌情加5-10分；<br />
    • 晒图积分状态与商品价值的倍数关系（价值越高，获得的积分倍数越大）：<br />
    1、0元～299元商品：基本积分×1<br />
    2、300元～999元商品：基本积分×2<br /> 
    3、1000元～1999元商品：基本积分×3<br />
    4、2000元以上商品：基本积分×4
            </div>
        </div>
    </div>
</asp:Content>
