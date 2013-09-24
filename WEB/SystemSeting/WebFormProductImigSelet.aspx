<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebFormProductImigSelet.aspx.cs" Inherits="WEB.SystemSeting.WebFormProductImig" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>

<ext:ResourceManager ID="ResourceManager1" runat="server">
</ext:ResourceManager>
  <form id="form1" runat="server">
    <div>
     <ext:Viewport ID="viprot" runat="server" Layout="FitLayout" >
        <Items>
            <ext:TabPanel runat="server" Padding="0">
                <Items>
                    <ext:Panel ID="pl1" runat="server" Border="false" Padding="0" Title="基础信息">
                        <Items>
                            <ext:Container ID="conTou" runat="server" Layout="ColumnLayout" Height="200">
                                <Items>
                                    <ext:Container ID="cont1" runat="server" Layout="FormLayout" ColumnWidth=".5" LabelAlign="Right">
                                        <Items>
                                            <ext:DisplayField ID="txtBh" runat="server" FieldLabel="产品编号" ></ext:DisplayField>
                                            <ext:DisplayField ID="txtIntro" runat="server" FieldLabel="产品简介"></ext:DisplayField>  
                                            <ext:DisplayField ID="pinPai" runat="server" FieldLabel="品牌"></ext:DisplayField>
                                            <ext:DisplayField ID="PaiMaiJG" runat="server" FieldLabel="拍买价格"></ext:DisplayField>
                                            <ext:DisplayField ID="txtCreaTime" runat="server" FieldLabel="创建时间"></ext:DisplayField>
                                            <ext:DisplayField ID="SetTime" runat="server" FieldLabel="拍买间隔"></ext:DisplayField>
                                            <ext:DisplayField ID="txtFee" runat="server" FieldLabel="手续费"></ext:DisplayField>
                                            <ext:DisplayField ID="disIsSaoye" runat="server" FieldLabel="首页显示"></ext:DisplayField>
                                            
                                        </Items>
                                    </ext:Container>
                                    <ext:Container ID="Container1" runat="server" Layout="FormLayout" ColumnWidth=".5" LabelAlign="Left">
                                        <Items>
                                            <ext:DisplayField ID="txtName" runat="server" FieldLabel="产品名"></ext:DisplayField>
                                            <ext:DisplayField runat="server" ID="type" FieldLabel="产品类型"></ext:DisplayField>
                                            <ext:DisplayField ID="txtJG" runat="server" FieldLabel="市场价格"></ext:DisplayField>
                                            <ext:DisplayField ID="txtPriceAdd" runat="server" FieldLabel="价格涨幅"></ext:DisplayField>
                                            <ext:DisplayField ID="txtAuctionPoint" runat="server" FieldLabel="所需拍点"></ext:DisplayField>
                                            <ext:DisplayField ID="paisj" runat="server" FieldLabel="拍买时间"></ext:DisplayField>
                                            <ext:DisplayField ID="txtShipFee" runat="server" FieldLabel="运费"></ext:DisplayField>
                                        </Items>
                                    </ext:Container>
                                    </Items>
                              </ext:Container>
                        </Items>
                    </ext:Panel>
                    <ext:Panel ID="pl2" runat="server" Border="false" Title="产品详情">
                        <Items>
                            <ext:DisplayField ID="discpSeled" runat="server" FieldLabel="产品详情"></ext:DisplayField>
                        </Items>
                    </ext:Panel>
                    <ext:Panel ID="panle3" runat="server" Border="false" Height="300" Title="图片信息" Padding="10" Width="880">
                        <Content>
                            <asp:DataList ID="datalist" runat="server">
                                <HeaderTemplate>
                                    <div> 
                                        <div style=" background-color:Aqua; font-size:12px;">
                                            <div id="xhH" class="tp" style=" width:50px; float:left;text-align:center;">序号</div>
                                            <div id="imgH" class="tp" style="text-align:center; width:300px; float:left">图片</div>
                                            <div  style=" width:50px; float:left;text-align:center;">操作</div>
                                            <div style=" clear"></div>
                                        </div>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div id="xh" class="tp" style=" width:50px; height:100px; float:left;text-align:center; padding:30px 0px; font-size:12px;"><%#Eval("xh")%></div>
                                    <div id="img"  style="height:100px; width:300px;float:left;text-align:center;"><img height="100" width="150" src='<%#Eval("img") %>' /></div>
                                    <div style="display: table-cell;height:100px; vertical-align:middle;width:50px;float:left; text-align:center;padding:30px 0px; font-size:12px;"><a href='WebFormProductImigSelet.aspx?id=<%#Eval("ProductID")%>&&tuid=<%#Eval("id")%>'>删除</a></div>
                                    <div style=" clear"></div>
                                    <hr />
                                </ItemTemplate>
                                <FooterTemplate>
                                    </div>
                                </FooterTemplate>
                            </asp:DataList>
                        </Content>
            </ext:Panel>
                </Items>
            </ext:TabPanel>
            
        </Items>
     </ext:Viewport>
    </div>
    </form>
</body>
</html>
