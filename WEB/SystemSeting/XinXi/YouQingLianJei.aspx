<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="YouQingLianJei.aspx.cs" Inherits="WEB.SystemSeting.XinXi.YouQingLianJei" %>
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
        <ext:JsonReader IDProperty="YouQinLianJiId">
            <Fields>
                <ext:RecordField Name="YouQinLianJiId"></ext:RecordField>
                <ext:RecordField Name="GsName"></ext:RecordField>
                <ext:RecordField Name="Url"></ext:RecordField>
                <ext:RecordField Name="DispType"></ext:RecordField>
                <ext:RecordField Name="xh"></ext:RecordField>
            </Fields>
        </ext:JsonReader>
    </Reader>
</ext:Store>
    <form id="form1" runat="server">
    <div>
     <ext:Viewport ID="view" runat="server" Layout="BorderLayout">
        <Items>
            <ext:Panel runat="server" Layout="HBoxLayout" Region="North" Height="60" LabelAlign="Right">
                <LayoutConfig><ext:HBoxLayoutConfig Align="Middle" /></LayoutConfig>
                <Items>
                    <ext:TextField ID="txtTile" runat="server" FieldLabel="网站名称"></ext:TextField>
                    <ext:ComboBox ID="comxDis" runat="server" FieldLabel="显示类型" Width="240">
                        <Items>
                            <ext:ListItem Text="文字" Value="文字" />
                            <ext:ListItem Text="图片" Value="图片" />
                        </Items>
                        <Triggers>
                            <ext:FieldTrigger  Icon="Clear"/>
                        </Triggers>
                        <Listeners>
                            <TriggerClick Handler="this.clear();" />
                        </Listeners>
                    </ext:ComboBox>
                    <ext:Button ID="bnt" runat="server" Text="查询" Icon="Zoom" Margins="0 0 0 20">
                        <Listeners>
                            <Click Handler="#{grid}.reload();" />
                        </Listeners>
                    </ext:Button>
                </Items>
            </ext:Panel>
           <ext:GridPanel ID="grid" runat="server" Region="Center" StoreID="StoreList">
           <LoadMask ShowMask="true" Msg="数据正在加载……" />
            <TopBar>
                <ext:Toolbar runat="server">
                    <Items>
                        <ext:Button ID="bntAdd" runat="server" Icon="Add" Text="添加">
                            <Listeners>
                                <Click Handler="#{FormPanel1}.getForm().reset();#{bntEids}.hide();#{bntAdds}.show();#{window_add}.setTitle('添加友情连接');#{window_add}.show();" />
                            </Listeners>
                        </ext:Button>
                        <ext:Button ID="bntEid" runat="server" Icon="UserEdit" Text="修改">
                            <DirectEvents>
                                <Click OnEvent="EidShow"></Click>
                            </DirectEvents>
                        </ext:Button>
                        <ext:Button ID="bntDel" runat="server" Text="删除" Icon="UserDelete">
                            <DirectEvents>
                                <Click OnEvent="Dele"></Click>
                            </DirectEvents>
                        </ext:Button>
                        <ext:Button ID="bntSele" runat="server" Icon="UserBrown" Text="查看">
                           <DirectEvents>
                            <Click OnEvent="Select"></Click>
                           </DirectEvents>
                        </ext:Button>
                    </Items>
                </ext:Toolbar>
            </TopBar>
            <ColumnModel>
                <Columns>
                    <ext:RowNumbererColumn Header="序"></ext:RowNumbererColumn>
                   <ext:Column Header="公司名称" DataIndex="GsName" Width="150"></ext:Column>
                   <ext:Column Header="网址" DataIndex="Url" Width="200"></ext:Column>
                   <ext:Column Header="显示方试" DataIndex="DispType"></ext:Column>
                   <ext:Column Header="序号" DataIndex="xh"></ext:Column>
                </Columns>
            </ColumnModel>
            <SelectionModel><ext:CheckboxSelectionModel SingleSelect="true"></ext:CheckboxSelectionModel></SelectionModel>
            <BottomBar>
                <ext:PagingToolbar runat="server" PageSize="10"></ext:PagingToolbar>
            </BottomBar>
           </ext:GridPanel>
        </Items>
     </ext:Viewport>

       <ext:Window ID="window_add"  runat="server"  Height="230" Icon="User"
            Width="450" AutoScroll="true" Modal="true" Layout="FormLayout" Hidden="true" Resizable="true">
            <Items>
                <ext:FormPanel ID="FormPanel1" runat="server" ButtonAlign="Right" Layout="FormLayout" LabelAlign="Right" MonitorValid="true" Padding="10">
                    <Items>
                        <ext:TextField ID="textGsName" runat="server" FieldLabel="网站名"></ext:TextField>
                        <ext:TextField ID="txtXh" runat="server" FieldLabel="序号" AllowBlank="false" ViewStateMode="Inherit" IndicatorCls="tipCls" IndicatorText="*"></ext:TextField>
                        <ext:TextField ID="textWZ" runat="server" FieldLabel="网址"></ext:TextField>
                        <ext:ComboBox ID="comType" runat="server" FieldLabel="显示方试" AllowBlank="false" ViewStateMode="Inherit" IndicatorCls="tipCls" IndicatorText="*">
                            <Items>
                                <ext:ListItem Text="文字" Value="文字" />
                                <ext:ListItem Text="图片" Value="图片" />
                            </Items>
                        </ext:ComboBox>
                        <ext:FileUploadField ID="filedTP" runat="server" FieldLabel="上传图片" Width="260" ButtonText="浏览"></ext:FileUploadField>
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
                                <Click Handler="#{window_add}.hide();" />
                            </Listeners>
                        </ext:Button>
                    </Buttons>
                </ext:FormPanel>
            </Items>
       </ext:Window>

        <ext:Window ID="window_select" Title="查看详情"  runat="server"  Height="380" Icon="User" Padding="0"
            Width="750" AutoScroll="true" Modal="true" Layout="FormLayout" Hidden="true" Resizable="true">
            <Items>
                <ext:FormPanel runat="server" Layout="FormLayout" Border="false" Width="730" LabelAlign="Right">
                    <Items>
                        <ext:DisplayField ID="disName" runat="server" FieldLabel="名称"></ext:DisplayField>
                        <ext:DisplayField ID="disUrl" runat="server" FieldLabel="地址"></ext:DisplayField>
                        <ext:DisplayField ID="disType" runat="server" FieldLabel="显示方式"></ext:DisplayField>
                        <ext:DisplayField ID="disXh" runat="server" FieldLabel="序号"></ext:DisplayField>
                        <ext:DisplayField ID="disTp" runat="server" FieldLabel="图片"></ext:DisplayField>
                    </Items>
                 </ext:FormPanel>
            </Items>
            <Buttons>
                <ext:Button Text="关闭" Icon="Cancel" runat="server">
                    <Listeners>
                        <Click Handler="#{window_select}.hide();" />
                    </Listeners>
                </ext:Button>
            </Buttons>
         </ext:Window>
    </div>
    </form>
</body>
</html>
