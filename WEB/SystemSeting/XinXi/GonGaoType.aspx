<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GonGaoType.aspx.cs" Inherits="WEB.SystemSeting.XinXi.GonGaoType" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<ext:ResourceManager ID="ResourceManager1" runat="server">
</ext:ResourceManager>
<body>
<ext:Store ID="StoreList" runat="server" OnRefreshData="Ref">
    <Reader>
        <ext:JsonReader IDProperty="GgTypeID">
            <Fields>
                <ext:RecordField Name="GgTypeID"></ext:RecordField>
                <ext:RecordField Name="TypeName"></ext:RecordField>
            </Fields>
        </ext:JsonReader>
    </Reader>
</ext:Store>
    <form id="form1" runat="server">
    <div>
      <ext:Viewport ID="vpMain" runat="server" Layout="BorderLayout">
        <Items>
             <ext:Panel ID="Panel1" runat="server" Region="North" Icon="Zoom" Layout="HBoxLayout" LabelAlign="Right" Height="70" Title="查询条件" LabelWidth="90" >
                <LayoutConfig><ext:HBoxLayoutConfig Align="Middle" /></LayoutConfig>
                <Items>
                    <ext:TextField ID="txtName" runat="server" FieldLabel="类型名"></ext:TextField>
                    <ext:Button ID="bntSele" runat="server" Text="查询" Icon="Zoom" Margins="0 0 0 20">
                        <Listeners>
                            <Click Handler="#{GridPanel1}.reload();" />
                        </Listeners>
                    </ext:Button>
                </Items>
             </ext:Panel>
            <ext:GridPanel ID="GridPanel1" runat="server"  StoreID="StoreList"  StripeRows="true"  AutoDoLayout="true" Region="Center">
                <LoadMask Msg="请稍后数据正在加载……" ShowMask="true" />
                <TopBar>
                    <ext:Toolbar runat="server">
                        <Items>
                            <ext:Button ID="bntAdd" runat="server" Text="添加" Icon="Add">
                                <Listeners>
                                    <Click Handler="#{window_addGgType}.setTitle('添加公告类型');#{FormPanel1}.getForm().reset();#{bntEids}.hide();#{bntAdds}.show();#{window_addGgType}.show();" />
                                </Listeners>
                            </ext:Button>
                            <ext:Button runat="server" ID="bntEid" Text="修改" Icon="UserEdit">
                                <DirectEvents><Click OnEvent="EidShow"></Click></DirectEvents>
                            </ext:Button>
                            <ext:Button ID="bntDele" runat="server" Text="删除" Icon="Delete">
                                <DirectEvents>
                                    <Click OnEvent="Delete">
                                        <Confirmation Message="真的要删除选中数据吗？" Title="提示" ConfirmRequest="true" />
                                    </Click>
                                </DirectEvents>
                            </ext:Button>
                        </Items>
                    </ext:Toolbar>
                </TopBar>
                <ColumnModel>
                    <Columns>
                    <ext:RowNumbererColumn Header="序"></ext:RowNumbererColumn>
                        <ext:Column DataIndex="TypeName" Header="类型名"></ext:Column>
                    </Columns>
                </ColumnModel>
                <SelectionModel>
                <ext:CheckboxSelectionModel SingleSelect="true"></ext:CheckboxSelectionModel>
                </SelectionModel>
                <BottomBar>
                    <ext:PagingToolbar runat="server" PageSize="10"></ext:PagingToolbar>
                </BottomBar>
            </ext:GridPanel>
        </Items>
      </ext:Viewport>

      <ext:Window ID="window_addGgType"  runat="server"  Height="125" Icon="User"
            Width="300" AutoScroll="true" Modal="true" Layout="FormLayout" Hidden="true" Resizable="true">
            <Items>
                <ext:FormPanel ID="FormPanel1" runat="server" Padding="10" ButtonAlign="Right" Layout="FormLayout" LabelAlign="Right" MonitorValid="true">
                    <Items>
                        <ext:TextField ID="textTypeName" runat="server" FieldLabel="类型名" IndicatorText="*" IndicatorCls="tipCls" AllowBlank="false" MsgTarget="Side">
                        </ext:TextField>
                    </Items>
                     <Buttons>
                        <ext:Button ID="bntAdds" runat="server" Text="添加" Icon="UserAdd" FormBind="true">
                            <DirectEvents>
                               <Click OnEvent="Add">
                                <EventMask Msg="正在执行操作" ShowMask="true"/>
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                        <ext:Button ID="bntEids" runat="server" Text="修改" Icon="UserEdit" FormBind="true">
                            <DirectEvents>
                                <Click OnEvent="EidSeve">
                                    <EventMask Msg="正在执行操作……" ShowMask="true" />
                                  
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                        <ext:Button ID="bntClos" runat="server" Text="关闭" Icon="Cross">
                            <Listeners>
                                <Click Handler="#{window_addGgType}.hide();" />
                            </Listeners>
                        </ext:Button>
                    </Buttons>
                </ext:FormPanel>
            </Items>
       </ext:Window>
    </div>
    </form>
</body>
</html>
