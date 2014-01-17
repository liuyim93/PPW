<%@ Page Title="" Language="C#" EnableSessionState="True" MasterPageFile="~/Auction/Main.Master" AutoEventWireup="true" CodeBehind="PointsMall_Detail.aspx.cs" Inherits="WEB.Auction.PointsMall_Detail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<%--        <script type="text/javascript">
            $(document).ready(function () {
                $(".img_small img").mouseover(function () {
                    $(this).css("border", "1px solid #8f0100");
                    $(this).siblings().css("border", "none");
                    var bigImgSrc = $(this).attr("src");
                    $(".img_big img").attr("src", bigImgSrc);
                });
            }); 
                        
            function loginadress() {
                var response = WEB.Auction.PointsMall_Detail.Check().value;
                var btn_return = document.getElementById("btn_return");
                var adressdiv = document.getElementById('<%=adressdiv.ClientID %>');
                if (response == "0") {
                    return false;
                }
                else {
                    if (response == "1") {
                        btn_return.innerText = "积分不足";
                        adressdiv.style.visibility = 'visible';
                        return false;
                    } else {
                        adressdiv.style.visibility = 'visible';
                        return false;
                    }
                }
            }

            function back() {
                var adressdiv = document.getElementById('<%=adressdiv.ClientID %>');
                adressdiv.style.visibility = "hidden";
                return false;
            }

            function clickme(rName) {
                var r = document.getElementsByTagName("input");
                for (var i = 0; i < r.length; i++) {
                    var rr = r[i];
                    if (rr.type == "radio") {
                        rr.checked = false;
                        if (rr.name == rName) {
                            rr.checked = true;
                        }
                    } else {
                        continue;
                    }
                }
            }
            function static() {
                var adressdiv = document.getElementById('<%=adressdiv.ClientID %>');
                adressdiv.style.visibility = "visible";
                return true;
             }
    </script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server"> 
    <div class="pointsdetail">
        <div class="pointsdetail_top">
            <div class="pointsdetail_left">
                <div class="img_big"><asp:Image ID="imgPro" runat="server" Width="300px" Height="260px" /></div>
                <div class="img_small">
                    <asp:Repeater ID="repeater_img" runat="server">
                        <ItemTemplate>
                            <img src="<%#Eval("img") %>" class="pointsdetail_img_non" />
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>  
            <div class="pointsdetail_right">
                <div class="pointsdetail_name"><asp:Label ID="lblProName" runat="server"></asp:Label></div>
                <div class="pointsdetail_price">市场价：<asp:Label ID="lblPrice" runat="server"></asp:Label></div>
                <div class="pointsdetail_points">所需积分：<asp:Label ID="lblPoints" runat="server"></asp:Label></div>
                <div class="pointsdetail_price">当前积分：<asp:Label ID="lblCurPoints" runat="server"></asp:Label></div>
                <div class="pointsdetail_btn"><asp:Button ID="btnSubmit" runat="server" Text="立即兑换" OnClientClick="return loginadress();" /></div>
                <div style="width:100%;height:25px;line-height:25px;"><p id="btn_return"></p></div>
            </div> 
        </div>
        <div class="pointsdetail_bottom">
            <div class="pointsdetail_title">商品详情</div>
            <div class="pointsdetail_detail"><asp:Label ID="lblDetail" runat="server"></asp:Label></div>
        </div>
    </div>
    <div id="adressdiv" class="adressdiv">
        <div id="adressdiv_bg"></div>
        <div id="adressdiv_top">
            <div class="adressdiv_top_close"><img id="img_close" src="../../Image/Close.png" width="16px" height="16px" border="0px" onclick="back();" /></div>
            <div class="adressdiv_top_title"><asp:Literal ID="ltlTitle" runat="server" Text="选择收货地址"></asp:Literal></div>
            <div ID="pnlAdressList" class="adressdiv_adrlst">
                <%--<asp:DataList ID="dlstShipAdress" runat="server" onitemdatabound="dlstShipAdress_ItemDataBound" DataKeyField="ShouHuoDZID">
                    <ItemTemplate>
                        <div>
                            <asp:RadioButton ID="rbtnAdress" runat="server" GroupName="ss" onclick="clickme(this.name)" />&nbsp;<asp:Label ID="lblname"　runat="server" Text='<%#Eval("ShouHuoName") %>'></asp:Label>&nbsp;&nbsp;<asp:Label ID="lblAdress" runat="server" Text='<%#Eval("DZ") %>'></asp:Label>&nbsp;&nbsp;<asp:Label ID="lblPhone" runat="server" Text='<%#Eval("Mode") %>'></asp:Label>
                            <asp:HiddenField ID="hfSelect" runat="server" Value='<%#Eval("IsSelect") %>' />
                        </div>
                    </ItemTemplate>
                </asp:DataList>
                 <asp:RadioButton ID="rbtnAddAdress" runat="server" oncheckedchanged="rbtnAddAdress_CheckedChanged" AutoPostBack="true" Text="添加收货地址" onClientClick="return static();" />
                 <div class="adressdiv_adrlst_btn"><asp:Button ID="btnConfirm" runat="server" onclick="btnConfirm_Click" Text="确定" />&nbsp;&nbsp;<asp:Button ID="btnBack" runat="server" OnClientClick=" return back();" Text="取消" /></div>--%>
                   <div>
                    <asp:Repeater ID="repeater_adr" runat="server">
                        <ItemTemplate>
                            <input type="radio" id="radio_<%#Eval("ShouHuoDZID") %>" />&nbsp;&nbsp;<p><%#Eval("ShouHuoName") %></p>&nbsp;&nbsp;
                            <p><%#Eval("DZ") %></p>&nbsp;&nbsp;<p><%#Eval("Mode") %></p>
                        </ItemTemplate>
                    </asp:Repeater>
                     </div>
                    <input type="radio" id="radioAddAdr" value="添加收货地址" /><br />
                    <div class=="adressdiv_adrlst-btn"><img src="" alt="确定" />&nbsp;&nbsp;<img src="" alt="取消" /></div>                
            </div>
            <div id="pnlAddAdress"> 
                <div class="actorder_addadr">
                    <ul>
                        <li class="actorder_addadr1"><div class="addadr_left" style="width:170px;"><font color="red">*</font>收货人：</div><div class="addadr_right"><input type="text" id="txtname" /></div></li>
                        <li class="actorder_addadr2"><div class="addadr_left" style="width:170px;"><font color="red">*</font>收货地址：</div><div class="addadr_right"><input id="txtadr" type="text" /></div></li>
                        <li class="actorder_addadr1"><div class="addadr_left" style="width:170px;"><font color="red">*</font>电话号码：</div><div class="addadr_right"><input type="text" id="txtphone" /></div></li>
                        <li class="actorder_addadr1"><div class="addadr_left" style="width:170px;"><font color="red">*</font>邮编：</div><div class="addadr_right"><input type="text" id="txtcode" /></div></li>
                        <li class="actorder_addadr2"><div class="addadr_left" style="width:170px;">备注：</div><div class="addadr_right"><input type="text" id="txtremark" /></div></li>
                        <li class="actorder_addadr_btn" style="padding-left:180px;"><img src="" alt="确定" />&nbsp;&nbsp;<img src="" alt="取消" /></li>
                    </ul>
                </div>
            </div>
        </div>        
    </div>
</asp:Content>
