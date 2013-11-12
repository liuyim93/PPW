<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebFormAuction.aspx.cs" Inherits="WEB.SystemSeting.WebFormAuction" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <ext:ResourceManager ID="ResourceManager1" runat="server"></ext:ResourceManager>
    <ext:Store ID="storeAuction" runat="server">
        <Reader>
            <ext:JsonReader IDProperty="AuctionID">
                <Fields>
                    <ext:RecordField Name="AuctionID"></ext:RecordField>
                    <ext:RecordField Name="ProductName"></ext:RecordField>
                    <ext:RecordField Name="ProductPrice"></ext:RecordField>
                    <ext:RecordField Name="Coding"></ext:RecordField>
                    <ext:RecordField Name="HuiYuan"></ext:RecordField>
                    <ext:RecordField Name="AuctionPrice"></ext:RecordField>
                    <ext:RecordField Name="AuctionTime"></ext:RecordField>                 
                    <ext:RecordField Name="CreateTime"></ext:RecordField>
                    <ext:RecordField Name="Status"></ext:RecordField>
                    <ext:RecordField Name="PriceAdd"></ext:RecordField>
                    <ext:RecordField Name="AuctionPoint"></ext:RecordField>
                    <ext:RecordField Name="FreePoint"></ext:RecordField>
                    <ext:RecordField Name="EndTime"></ext:RecordField>
                    <ext:RecordField Name="AuctionType"></ext:RecordField>
                    <ext:RecordField Name="IsRecommend"></ext:RecordField>
                </Fields>
            </ext:JsonReader>
        </Reader>
    </ext:Store>
    <div>
        <ext:Viewport ID="viewport1" runat="server" Layout="BorderLayout">
            <Items>
            <ext:Panel ID="pnlSelect" runat="server" Layout="HBoxLayout" Height="70" Title="查询条件" Icon="Zoom" LabelAlign="Right" LabelWidth="90" Region="North">
                <LayoutConfig><ext:HBoxLayoutConfig Align="Middle" /></LayoutConfig>
                <Items>
                    <ext:TextField ID="txtProName" runat="server" FieldLabel="拍品名称"></ext:TextField>
                    <ext:ComboBox ID="cboxAuctionType" runat="server" FieldLabel="竞拍类型">
                        <Items>
                            <ext:ListItem Value="常规竞拍" Text="常规竞拍" />
                            <ext:ListItem Value="免费竞拍" Text="免费竞拍" />
                        </Items>
                    </ext:ComboBox>
                    <ext:ComboBox ID="cboxStatus" runat="server" FieldLabel="状态" >
                        <Items>
                            <ext:ListItem Value="3" Text="已成交" />
                            <ext:ListItem Value="4" Text="未成交" />
                        </Items>
                    </ext:ComboBox>
                    <ext:Button ID="btnSelect" runat="server" Text="查询" Icon="Zoom" Margins="0 0 0 20">
                        <Listeners>
                            <Click Handler="#{GridPanel1}.reload();" />
                        </Listeners>
                    </ext:Button>
                </Items>
            </ext:Panel>
            <ext:GridPanel ID="GridPanel1" runat="server" StoreID="storeAuction" Region="Center" AutoDoLayout="true">
                <ColumnModel>
                    <Columns>
                        <ext:RowNumbererColumn Header="序"></ext:RowNumbererColumn>
                        <ext:Column DataIndex="Coding" Header="编号"></ext:Column>
                        <ext:Column DataIndex="ProductName" Header="拍品名称"></ext:Column>
                        <ext:Column DataIndex="ProductPrice" Header="价格"></ext:Column>
                        <ext:Column DataIndex="HuiYuan" Header="最后出价"></ext:Column>
                        <ext:Column DataIndex="AuctionPrice" Header="竞拍价格"></ext:Column>
                        <ext:Column DataIndex="AuctionTime" Header="竞拍时间"></ext:Column>
                        <ext:Column DataIndex="PriceAdd" Header="价格涨幅"></ext:Column>
                        <ext:Column DataIndex="AuctionPoint" Header="所需拍点"></ext:Column>
                        <ext:Column DataIndex="FreePoint" Header="所有返点"></ext:Column>
                        <ext:Column DataIndex="EndTime" Header="结束时间"></ext:Column>
                        <ext:Column DataIndex="AuctionType" Header="竞拍类型"></ext:Column>
                        <ext:Column DataIndex="IsRecommend" Header="推荐商品"></ext:Column>
                    </Columns>
                </ColumnModel>
                <SelectionModel><ext:CheckboxSelectionModel SingleSelect="true"></ext:CheckboxSelectionModel></SelectionModel>
                <LoadMask Msg="数据正在加载中..." ShowMask="true" />
                <TopBar>
                    <ext:Toolbar runat="server">
                        <Items>
                            <ext:Button ID="btnAdd" runat="server" Text="添加" Icon="UserAdd" OnClick="AddShow" AutoPostBack="true"></ext:Button>
                            <ext:Button ID="btnEdit" runat="server" Text="修改" Icon="UserEdit" OnClick="EditShow" AutoPostBack="true"></ext:Button>
                            <ext:Button ID="btnDel" runat="server" Text="删除" Icon="Delete">
                                <DirectEvents>
                                    <Click OnEvent="Del">
                                        <Confirmation Title="提示" Message="确定要删除吗？" ConfirmRequest="true" />
                                        <EventMask Msg="正在执行删除操作..." ShowMask="true" />
                                    </Click>
                                </DirectEvents>
                            </ext:Button>
                            <ext:Button ID="btnDetail" runat="server" Text="查看详情" Icon="UserBrown">
                                <DirectEvents>
                                    <Click OnEvent="ShowDetail"></Click>
                                </DirectEvents>
                            </ext:Button>
                        </Items>
                    </ext:Toolbar>
                </TopBar>
                <BottomBar>
                    <ext:PagingToolbar PageSize="10" runat="server"></ext:PagingToolbar>
                </BottomBar>
            </ext:GridPanel>                
            </Items>
        </ext:Viewport>
        <ext:Window ID="window_update" runat="server" Icon="User" Layout="FormLayout" AutoScroll="true" Width="850" Height="380" Modal="true" Resizable="true" Visible="false">
            <Items>
                <ext:FormPanel ID="FormPanel1" runat="server" Layout="FormLayout" ButtonAlign="Right" LabelAlign="Right">
                    <Items>
                        <ext:Panel ID="pnl1" runat="server" AnchorHorizontal="100%" Title="基础数据" Layout="FormLayout">
                            <Items>
                                <ext:Container ID="container_left" runat="server" ColumnWidth=".5">
                                    <Items>
                                        <ext:DropDownField ID="dropPro" runat="server" FieldLabel="选择商品"></ext:DropDownField>
                                        <ext:NumberField ID="txtAuctionPrice" runat="server" FieldLabel="竞拍价格"></ext:NumberField>
                                        <ext:NumberField ID="txtPriceAdd" runat="server" FieldLabel="价格涨幅"></ext:NumberField>
                                        <ext:NumberField ID="txtAuctionPoint" runat="server" FieldLabel="所需拍点"></ext:NumberField>
                                    </Items>
                                </ext:Container>
                                <ext:Container ID="container_right" runat="server" ColumnWidth=".5">
                                    <Items>
                                        <ext:DropDownField ID="dropAuctionType1" runat="server" FieldLabel="竞拍类型"></ext:DropDownField>
                                        <ext:NumberField ID="numFreePoint" runat="server" FieldLabel="所需返点"></ext:NumberField>
                                        <ext:ComboBox ID="chkRecommend" runat="server" FieldLabel="推荐商品">
                                            <Items>
                                                <ext:ListItem Text="是" Value="1" />
                                                <ext:ListItem Text="否" Value="0" />
                                            </Items>
                                        </ext:ComboBox>
                                    </Items>
                                </ext:Container>
                            </Items>
                        </ext:Panel>                        
                    </Items>
                    <Buttons>
                        <ext:Button ID="btnAdds" runat="server" Icon="UserAdd" Text="添加">
                            <DirectEvents>
                                <Click OnEvent="AddAuction">
                                    <EventMask Msg="正在执行操作..." ShowMask="true" />
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                        <ext:Button ID="btnEdits" runat="server" Icon="UserEdit" Text="修改">
                            <DirectEvents>
                                <Click OnEvent="EditAuction">
                                    <EventMask Msg="正在执行操作..." ShowMask="true" />
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                        <ext:Button ID="btnClose" runat="server" Icon="Cross" Text="关闭">
                            <Listeners>
                                <Click Handler="#{window_update}.hide();" />
                            </Listeners>
                        </ext:Button>
                    </Buttons>
                </ext:FormPanel>
            </Items>
        </ext:Window>
        <ext:Window ID="window_detail" runat="server" Layout="FormLayout" Hidden="true" Width="850" Height="380">
            <AutoLoad Mode="IFrame"></AutoLoad>
            <Buttons>
                <ext:Button ID="btnClose1" runat="server" Text="关闭" Icon="Cross">
                    <Listeners>
                        <Click Handler="#{window_detail}.hide()" />
                    </Listeners>
                </ext:Button>
            </Buttons>
        </ext:Window>
    </div>
    </form>
</body>
</html>
