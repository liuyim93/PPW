<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebFormShowOrder.aspx.cs" Inherits="WEB.SystemSeting.XiaoShou.WebFormShowOrder" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .dlstimg{margin-left:80px;height:80px;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <ext:ResourceManager ID="ResourceManager1" runat="server"></ext:ResourceManager>
    <ext:Store ID="storeShowOrder" runat="server">
        <Reader>
            <ext:JsonReader IDProperty="ShowOrderID">
                <Fields>
                    <ext:RecordField Name="ShowOrderID"></ext:RecordField>
                    <ext:RecordField Name="OrderID"></ext:RecordField>
                    <ext:RecordField Name="ProductName"></ext:RecordField>
                    <ext:RecordField Name="AuctionPrice"></ext:RecordField>
                    <ext:RecordField Name="ProductPrice"></ext:RecordField>
                    <ext:RecordField Name="HuiYuan"></ext:RecordField>
                    <ext:RecordField Name="Title"></ext:RecordField>
                    <ext:RecordField Name="Detail"></ext:RecordField>
                    <ext:RecordField Name="Points"></ext:RecordField>
                    <ext:RecordField Name="Reply"></ext:RecordField>
                    <ext:RecordField Name="LoadTime"></ext:RecordField>
                    <ext:RecordField Name="IsCheck"></ext:RecordField>
                    <ext:RecordField Name="IsShow"></ext:RecordField>
                </Fields>
            </ext:JsonReader>
        </Reader>
    </ext:Store>
    <ext:Store ID="storeImage" runat="server">
        <Reader>
            <ext:JsonReader IDProperty="ShowOrderImgID">
                <Fields>
                    <ext:RecordField  Name="ImgUrl"></ext:RecordField>
                </Fields>
            </ext:JsonReader>
        </Reader>        
    </ext:Store>
    <div>
        <ext:Viewport ID="viewport" runat="server" Layout="FormLayout">
            <Items>
                <ext:Panel ID="pnlSelect" runat="server" Layout="HBoxLayout" Height="70" LabelAlign="Right" LabelWidth="90">
                    <LayoutConfig><ext:HBoxLayoutConfig Align="Middle" /></LayoutConfig>
                    <Items>
                        <ext:TextField ID="txtTitle" runat="server" FieldLabel="晒单标题"></ext:TextField>
                        <ext:ComboBox ID="cboxCheck" runat="server" FieldLabel="审核状态">
                            <Items>
                                <ext:ListItem Text="通过" Value="1" />
                                <ext:ListItem Text="未通过" Value="0" />
                            </Items>
                        </ext:ComboBox>
                        <ext:ComboBox ID="cboxRead" runat="server" FieldLabel="状态">
                            <Items>
                                <ext:ListItem Text="未读" Value="0" />
                                <ext:ListItem Text="已读" Value="1" />
                            </Items>
                        </ext:ComboBox>
                        <ext:Button ID="btnSelect" runat="server" Icon="Zoom" Text="查询" Margins="0 0 0 20">
                            <Listeners>
                                <Click Handler="#{gridShowOrder}.reload();" />
                            </Listeners>
                        </ext:Button>
                    </Items>
                </ext:Panel>
                <ext:GridPanel ID="gridShowOrder" runat="server" Layout="FormLayout" Height="360" StoreID="storeShowOrder">
                    <ColumnModel>
                        <Columns>
                            <ext:RowNumbererColumn Header="序"></ext:RowNumbererColumn>
                            <ext:Column DataIndex="ProductName" Header="商品名称"></ext:Column>
                            <ext:Column DataIndex="ProductPrice" Header="商品价格"></ext:Column>
                            <ext:Column DataIndex="HuiYuan" Header="获得者"></ext:Column>
                            <ext:Column DataIndex="AuctionPrice" Header="成交价格"></ext:Column>
                            <ext:Column DataIndex="Title" Header="标题"></ext:Column>
                            <ext:Column DataIndex="Detail" Header="内容"></ext:Column>
                            <ext:Column DataIndex="Reply" Header="回复"></ext:Column>
                            <ext:Column DataIndex="Points" Header="奖励积分"></ext:Column>
                            <ext:Column DataIndex="LoadTime" Header="晒单时间"></ext:Column>
                            <ext:Column DataIndex="IsCheck" Header="审核状态"></ext:Column>
                            <ext:Column DataIndex="IsShow" Header="首页显示"></ext:Column>                            
                        </Columns>
                    </ColumnModel>
                    <SelectionModel><ext:CheckboxSelectionModel SingleSelect="true"></ext:CheckboxSelectionModel></SelectionModel>
                    <LoadMask Msg="正在加载中..." ShowMask="true" />
                    <TopBar>
                         <ext:Toolbar ID="toolbar" runat="server">
                            <Items>
                                <ext:Button ID="btnReply" runat="server" Icon="UserAlert" Text="回复" OnClick="ReplyShow" AutoPostBack="true"></ext:Button>
                                <ext:Button ID="btnUpdate" runat="server" Icon="UserEdit" Text="修改" OnClick="UpdateShow" AutoPostBack="true"></ext:Button>
                                <ext:Button ID="btnDel" runat="server" Icon="Delete" Text="删除">
                                    <DirectEvents>
                                        <Click OnEvent="Del">
                                            <Confirmation Message="确定要删除吗？" Title="提示" ConfirmRequest="true" />
                                            <EventMask Msg="正在执行删除操作..." ShowMask="true" />
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                                <ext:Button ID="btnDetail" runat="server" OnClick="ShowDetail" Text="查看详情" Icon="UserBrown" AutoPostBack="true"></ext:Button>
                            </Items>
                         </ext:Toolbar>                      
                    </TopBar>
                    <BottomBar>
                        <ext:PagingToolbar PageSize="10" runat="server"></ext:PagingToolbar>
                    </BottomBar>
                </ext:GridPanel>           
                <ext:Window ID="window_update" runat="server" Layout="FormLayout" Height="380" Width="850" Hidden="true" Icon="User">
                    <Items>
                        <ext:Panel ID="pnlOrderDetail" runat="server" Layout="FormLayout" LabelAlign="Right">
                            <Items>
                            <ext:Container ID="Container" runat="server" Layout="ColumnLayout" Height="90">
                                <Items>
                                     <ext:Container ID="container_left" runat="server" Layout="FormLayout" ColumnWidth=".5">
                                    <Items>
                                        <ext:DisplayField ID="disProName" runat="server" FieldLabel="商品名称"></ext:DisplayField>
                                        <ext:DisplayField ID="disHuiYuan" runat="server" FieldLabel="获得者"></ext:DisplayField>
                                        <ext:DisplayField ID="disTime" runat="server" FieldLabel="晒单时间"></ext:DisplayField>
                                    </Items>
                                </ext:Container>
                                <ext:Container ID="container_right" runat="server" Layout="FormLayout" ColumnWidth=".5">
                                    <Items>
                                        <ext:DisplayField ID="disProPrice" runat="server" FieldLabel="商品价格"></ext:DisplayField>
                                        <ext:DisplayField ID="disActPrice" runat="server" FieldLabel="成交价格"></ext:DisplayField>
                                    </Items>
                                </ext:Container>
                                </Items>
                            </ext:Container>                               
                                <ext:Panel ID="pnlReply" runat="server" Layout="FormLayout" Height="290" Border="false" Margins="10 0 0 0">
                                    <Items>
                                        <ext:DisplayField ID="disTitle" runat="server" FieldLabel="标题"></ext:DisplayField>
                                        <ext:DisplayField ID="disDetail" runat="server" FieldLabel="内容"></ext:DisplayField>     
                                        <ext:Panel ID="pnlImg" runat="server" Height="80" Layout="FormLayout" Border="false" Width="800">
                                            <Content>
                                                <asp:DataList ID="dlstImg" runat="server" RepeatDirection="Horizontal" CssClass="dlstimg">
                                                    <ItemTemplate>
                                                        <div style="margin-right:5px;"><a href="../<%#Eval("ImgUrl") %>" target="_blank"><img src='../<%#Eval("ImgUrl") %>' width="80px" height="80px" /></a></div>
                                                    </ItemTemplate>
                                                </asp:DataList>
                                            </Content>
                                        </ext:Panel>                                                                 
                                        <ext:ComboBox ID="cboxPass" runat="server" FieldLabel="审核" AllowBlank="false" IndicatorText="*" IsRemoteValidation="true">
                                            <Items>
                                                <ext:ListItem Text="通过" Value="1" />
                                                <ext:ListItem Text="不通过" Value="0" />
                                            </Items>
                                        </ext:ComboBox>
                                        <ext:ComboBox ID="cboxHome" runat="server" FieldLabel="首页显示" AllowBlank="false" IndicatorText="*" IsRemoteValidation="true">
                                            <Items>
                                                <ext:ListItem Text="是" Value="1" />
                                                <ext:ListItem Text="否" Value="0" />
                                            </Items>
                                        </ext:ComboBox>
                                        <ext:TextField ID="txtPoints" runat="server" FieldLabel="奖励积分" AllowBlank="false" IndicatorText="*" IsRemoteValidation="true"></ext:TextField>
                                        <ext:TextArea ID="txtReply" runat="server" FieldLabel="回复" Width="300" AllowBlank='false' IndicatorText="*" IsRemoteValidation="true"></ext:TextArea>
                                    </Items>
                                </ext:Panel>
                            </Items>
                        </ext:Panel>
                    </Items>
                    <Buttons>
                        <ext:Button ID="btnReturn" runat="server" Icon="Accept" Text="返回">
                            <Listeners>
                                <Click Handler="#{window_update}.hide();" />
                            </Listeners>
                        </ext:Button>
                        <ext:Button ID="btnSave" runat="server" Icon="UserStar" Text="保存" OnClick="Save" AutoPostBack="true"></ext:Button>
                    </Buttons>
                </ext:Window>
                <ext:Window ID="window_detail" runat="server" Layout="FormLayout" Title="查看详细信息" Hidden="true" Icon="User" Height="380" Width="850">
                    <Items>
                        <ext:Panel ID="pnl_Detail" runat="server" Layout="FormLayout"  LabelAlign="Right">
                            <Items>
                                <ext:Container ID="container3" runat="server" Layout="ColumnLayout" Height="100">
                                    <Items>
                                        <ext:Container ID="container1" runat="server" Layout="FormLayout" ColumnWidth=".5">
                                    <Items>
                                        <ext:DisplayField ID="disProductName" runat="server" FieldLabel="商品名称"></ext:DisplayField>
                                        <ext:DisplayField ID="disMemberName" runat="server" FieldLabel="获得者"></ext:DisplayField>
                                        <ext:DisplayField ID="disIsCheck" runat="server" FieldLabel="审核状态"></ext:DisplayField>
                                        <ext:DisplayField ID="disLoadTime" runat="server" FieldLabel="晒单时间"></ext:DisplayField>
                                    </Items>
                                </ext:Container>
                                <ext:Container ID="container2" runat="server" Layout="FormLayout" ColumnWidth=".5">
                                    <Items>
                                        <ext:DisplayField ID="disProductPrice" runat="server" FieldLabel="商品价格"></ext:DisplayField>
                                        <ext:DisplayField ID="disAuctionPrice" runat="server" FieldLabel="成交价格"></ext:DisplayField>
                                        <ext:DisplayField ID="disIsShow" runat="server" FieldLabel="首页显示"></ext:DisplayField> 
                                        <ext:DisplayField ID="disPoints" runat="server" FieldLabel="奖励积分"></ext:DisplayField>                                       
                                    </Items>
                                </ext:Container>
                                    </Items>
                                </ext:Container>                                
                                <ext:Panel ID="pnldetail" runat="server" Layout="FormLayout" Height="280" Border="false" Margins="10 0 0 0">
                                    <Items>
                                        <ext:DisplayField ID="disShowOrderTitle" runat="server" FieldLabel="标题"></ext:DisplayField>
                                        <ext:DisplayField ID="disShowOrderDetail" runat="server" FieldLabel="内容"></ext:DisplayField>
                                        <ext:Panel ID="pnlImage" runat="server" Layout="FormLayout" Border="false" Height="80">
                                            <Content>
                                                <asp:DataList ID="dlstImage" runat="server" RepeatDirection="Horizontal" CssClass="dlstimg">
                                                    <ItemTemplate>
                                                        <div style="margin-right:5px;"><a href="../<%#Eval("ImgUrl") %>" target="_blank"><img src="../<%#Eval("ImgUrl") %>" alt="" width="80px" height="80px" /></a></div>
                                                    </ItemTemplate>
                                                </asp:DataList>
                                            </Content>
                                        </ext:Panel>
                                        <ext:DisplayField ID="disReply" runat="server" FieldLabel="回复"></ext:DisplayField>
                                    </Items>
                                </ext:Panel>
                            </Items>
                        </ext:Panel>
                    </Items>
                    <Buttons>
                        <ext:Button ID="btnClose" runat="server" Text="关闭" Icon="Cross">
                            <Listeners>
                                <Click Handler="#{window_detail}.hide();" />
                            </Listeners>
                        </ext:Button>
                    </Buttons>
                </ext:Window>     
            </Items>
        </ext:Viewport>
    </div>
    </form>
</body>
</html>
