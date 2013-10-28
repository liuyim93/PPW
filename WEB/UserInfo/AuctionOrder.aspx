<%@ Page Title="" Language="C#" MasterPageFile="~/Auction/UserCenter.master" AutoEventWireup="true" CodeBehind="AuctionOrder.aspx.cs" Inherits="WEB.UserInfo.AuctionOrder" %>
<asp:Content ID="Content1" ContentPlaceHolderID="content3" runat="server">
    <asp:UpdatePanel ID="updatepanel1" runat="server">
        <ContentTemplate>
            <div class="biding">
                <div class="biding_title">竞拍订单</div>
                <div class="actorder_content">
                  <asp:Panel ID="pnlShipAdress" runat="server">
                    <div class="actorder_adr">
                        <p class="actorder_adr_ttl">填写收货地址</p>
                        <asp:Panel ID="pnlAdressList" runat="server" Visible="false">
                            <asp:DataList ID="dlstShipAdress" runat="server" 
                                onitemdatabound="dlstShipAdress_ItemDataBound" DataKeyField="ShouHuoDZID">
                                <ItemTemplate>
                                    <div>
                                        <asp:RadioButton ID="rbtnAdress" runat="server" GroupName="ss" onclick="clickme(this.name)" />&nbsp;<asp:Label ID="lblname"　runat="server" Text='<%#Eval("ShouHuoName") %>'></asp:Label>&nbsp;&nbsp;<asp:Label ID="lblAdress" runat="server" Text='<%#Eval("DZ") %>'></asp:Label>&nbsp;&nbsp;<asp:Label ID="lblPhone" runat="server" Text='<%#Eval("Mode") %>'></asp:Label>
                                        <asp:HiddenField ID="hfSelect" runat="server" Value='<%#Eval("IsSelect") %>' />
                                    </div>
                                </ItemTemplate>
                            </asp:DataList>
                            <asp:RadioButton ID="rbtnAddAdress" runat="server" oncheckedchanged="rbtnAddAdress_CheckedChanged" AutoPostBack="true" Text="添加收货地址" />
                        </asp:Panel>
                        <asp:Panel ID="pnlAddAdress" runat="server">
                            <div class="actorder_addadr">
                               <ul>
                                    <li class="actorder_addadr1"><div class="addadr_left"><font color="red">*</font>收货人：</div><div class="addadr_right"><asp:TextBox ID="txtname" runat="server"></asp:TextBox></div></li>
                                    <li class="actorder_addadr2"><div class="addadr_left"><font color="red">*</font>收货地址：</div><div class="addadr_right"><asp:TextBox ID="txtAdress" runat="server" TextMode="MultiLine"></asp:TextBox></div></li>
                                    <li class="actorder_addadr1"><div class="addadr_left"><font color="red">*</font>电话号码：</div><div class="addadr_right"><asp:TextBox ID="txtPhone" runat="server"></asp:TextBox></div></li>
                                    <li class="actorder_addadr1"><div class="addadr_left"><font color="red">*</font>邮编：</div><div class="addadr_right"><asp:TextBox ID="txtPostCode" runat="server"></asp:TextBox></div></li>
                                    <li class="actorder_addadr2"><div class="addadr_left">备注：</div><div class="addadr_right"><asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine"></asp:TextBox></div></li>
                                    <li class="actorder_addadr_btn"><asp:Button ID="btnSave" runat="server" onclick="btnSave_Click" Text="保存" />&nbsp;&nbsp;<asp:Button ID="btnCancel" runat="server" onclick="btnCancel_Click" Text="取消" /></li>
                               </ul>
                            </div>
                        </asp:Panel>
                    </div>
                  </asp:Panel>                    
                    <div class="actorder_lst">
                        <div class="actorder_lsttitle">
                            <ul>
                                <li><a href="AuctionOrder.aspx?type=0" target="_self">未支付</a></li>
                                <li><a href="AuctionOrder.aspx?type=1" target="_self">已支付</a></li>
                                <li><a href="AuctionOrder.aspx?type=2" target="_self">过期及取消</a></li>
                            </ul>
                        </div>
                        <div class="actorder_lstcontent">
                            <asp:DataList ID="dlstOrderList" runat="server" Width="100%" 
                                DataKeyField="ProductID" onitemdatabound="dlstOrderList_ItemDataBound" 
                                onitemcommand="dlstOrderList_ItemCommand">
                                <ItemTemplate>
                                    <div class="actorder_area">
                                        <div class="actorder_area_title">【竞拍购买】&nbsp;订单编号：<asp:Label ID="lblOrderNo" runat="server" Text='<%#Eval("DingDanBH") %>'></asp:Label>&nbsp;&nbsp;下单时间：<asp:Label ID="lblOrderTime" runat="server" Text='<%#Eval("DingDanTime") %>'></asp:Label>&nbsp;&nbsp;过期时间：<asp:Label ID="lblInvalidTime" runat="server" Text='<%#Eval("InvalidTime") %>'></asp:Label></div>
                                        <ul>
                                            <li class="actorder_area_img"><a href="../Auction/ProDetail.aspx?id=<%#Eval("ProductID") %>" target="_self"><asp:Image ID="img" runat="server" Width="60px" Height="60px" /></a></li>
                                            <li class="actorder_area_name"><asp:HyperLink ID="hlnkProName" runat="server" NavigateUrl='../Auction/ProDetail.aspx?id=<%#Eval("ProductID") %>'></asp:HyperLink></li>
                                            <li class="actorder_area_price">商品价格<br />￥<asp:Label ID="lblactPrice" runat="server" Text='<%#Eval("ProductPrice") %>'></asp:Label></li>
                                            <li class="actorder_area_price">手续费<br />￥<asp:Label ID="lblFee" runat="server" Text='<%#Eval("Fee") %>'></asp:Label></li>
                                            <li class="actorder_area_price">运费<br />￥<asp:Label ID="lblShipFee" runat="server" Text='<%#Eval("ShipFee") %>'></asp:Label></li>
                                            <li class="actorder_area_price">总价<br />￥<asp:Label ID="lblTotalPrice" runat="server" Text='<%#Eval("TotalPrice") %>'></asp:Label></li>
                                            <li class="actorder_area_price">状态<br /><font color="red"><asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Status") %>'></asp:Label></font></li>
                                            <li class="actorder_area_operate"><asp:LinkButton ID="lbtnPay" runat="server">立即付款</asp:LinkButton><asp:LinkButton ID="lbtnShowOrder" runat="server" CommandName="show" CommandArgument='<%#Eval("DingDanID") %>'>晒单</asp:LinkButton></li>
                                        </ul>                                     
                                    </div>
                                </ItemTemplate>
                            </asp:DataList>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
      <script type="text/javascript" >
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
    </script>
</asp:Content>
