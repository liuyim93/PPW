<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GongGao.aspx.cs" Inherits="WEB.SystemSeting.XinXi.GongGao" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../kindeditor/themes/default/default.css" rel="stylesheet" type="text/css" />
    <link href="../../kindeditor/plugins/code/prettify.css" rel="stylesheet" type="text/css" />
    <script  src="../../kindeditor/kindeditor.js" type="text/javascript"></script>
    <script  src="../../kindeditor/lang/zh_CN.js" type="text/javascript"></script>
    <script  src="../../kindeditor/plugins/code/prettify.js" type="text/javascript"></script>
    <script src="../../kindeditor/kindeditor-all.js" type="text/javascript"></script>
    <title></title>
     <script type="text/javascript">
         var onKeyUp = function (field) {
             var v = this.processValue(this.getRawValue()),
            field;

             if (this.startDateField) {
                 field = Ext.getCmp(this.startDateField);
                 field.setMaxValue();
                 this.dateRangeMax = null;
             } else if (this.endDateField) {
                 field = Ext.getCmp(this.endDateField);
                 field.setMinValue();
                 this.dateRangeMin = null;
             }

             field.validate();
         };

         var editor1, editor2;
         KindEditor.ready(function (K) {
             editor1 = K.create('#txt_TiMuNeiRong', {
                 cssPath: '../../kindeditor/plugins/code/prettify.css',
                 uploadJson: '../../kindeditor/asp.net/upload_json.ashx',
                 fileManagerJson: '../../kindeditor/asp.net/file_manager_json.ashx',
                 allowFileManager: true,
                 resizeType: 0,
                 afterCreate: function () {
                     var self = this;
                     K.ctrl(document, 13, function () {
                         self.sync();
                         K('form[name=example]')[0].submit();
                     });
                     K.ctrl(self.edit.doc, 13, function () {
                         self.sync();
                         K('form[name=example]')[0].submit();
                     });
                 }
             });
             prettyPrint();
         });
    </script>
</head>
<ext:ResourceManager ID="ResourceManager1" runat="server">
</ext:ResourceManager>
<body>
<ext:Store ID="StorList" runat="server" OnRefreshData="Ref">
    <Reader>
        <ext:JsonReader IDProperty="GgId">
            <Fields>
                <ext:RecordField Name="GgId"></ext:RecordField>
                <ext:RecordField Name="Tile"></ext:RecordField>
                <ext:RecordField Name="GgTypeID"></ext:RecordField>
                <ext:RecordField Name="RenYuanId"></ext:RecordField>
                <ext:RecordField Name="CreatTime"></ext:RecordField>
            </Fields>
        </ext:JsonReader>
    </Reader>
</ext:Store>
<ext:Store ID="StorType" runat="server">
    <Reader>
        <ext:JsonReader>
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
                        <ext:TextField ID="txtTile" runat="server" FieldLabel="标题" LabelWidth="70"></ext:TextField>
                        <ext:ComboBox ID="comType" runat="server" FieldLabel="类型" StoreID="StorType" LabelWidth="70" ValueField="GgTypeID" DisplayField="TypeName" Width="200">
                            <Triggers><ext:FieldTrigger Icon="Clear" /></Triggers>
                            <Listeners>
                                <TriggerClick Handler="this.clear();" />
                            </Listeners>
                        </ext:ComboBox>
                        <ext:TriggerField ID="trgname" runat="server" FieldLabel="人员" Editable="false" Width="200">
                            <Triggers>
                                <ext:FieldTrigger Icon="Search"/>
                            </Triggers>
                            <DirectEvents>
                                <TriggerClick OnEvent="SelRY"></TriggerClick>
                            </DirectEvents>
                        </ext:TriggerField>
                        <ext:DateField ID="xtxtbegin" runat="server" FieldLabel="创建时间" LabelWidth="110"  Width="220" Format="yyyy-MM-dd"
                                     Vtype="daterange" EnableKeyEvents="true">
                                        <Listeners>
                                             <KeyUp Fn="onKeyUp" />
                                        </Listeners>
                                        <CustomConfig>
                                            <ext:ConfigItem Name="endDateField" Value="#{xtxtbend}" Mode="Value">
                                            </ext:ConfigItem>
                                        </CustomConfig>
                                  </ext:DateField>
                                <ext:DateField ID="xtxtbend" runat="server" FieldLabel="到" LabelWidth="25" Width="135"
                                    Format="yyyy-MM-dd" Vtype="daterange" EnableKeyEvents="true">
                                    <Listeners>
                                        <KeyUp Fn="onKeyUp" />
                                    </Listeners>
                                    <CustomConfig>
                                        <ext:ConfigItem Name="startDateField" Value="#{xtxtbegin}" Mode="Value">
                                        </ext:ConfigItem>
                                    </CustomConfig>
                                </ext:DateField>
                        <ext:Button ID="bntSele" runat="server" Text="查询" Icon="Zoom" Margins="0 0 0 20">
                            <Listeners>
                                <Click Handler="GridPanel1.reload();" />
                            </Listeners>
                        </ext:Button>
                    </Items>
             </ext:Panel>
              <ext:GridPanel ID="GridPanel1" runat="server"  StoreID="StorList"   StripeRows="true"  AutoDoLayout="true" Region="Center">
                <LoadMask  Msg="数据正在加载……" ShowMask="true"/>
                <TopBar>
                    <ext:Toolbar runat="server">
                        <Items>
                            <ext:Button runat="server" ID="bntAdd" Text="添加" Icon="Add">
                                <Listeners>
                                    <Click Handler="#{FormPanel1}.getForm().reset();#{bntEids}.hide();#{bntAdds}.show();#{window_addGonGao}.setTitle('添加公告信息');#{window_addGonGao}.show();" />
                                </Listeners>
                            </ext:Button>
                            <ext:Button runat="server" ID="bntEid" Text="修改" Icon="UserEdit">
                                <DirectEvents>
                                    <Click OnEvent="EidShow"></Click>
                                </DirectEvents>
                            </ext:Button>
                            <ext:Button runat="server" Text="查看" Icon="UserBrown">
                                <DirectEvents>
                                    <Click OnEvent="Selectd"></Click>
                                </DirectEvents>
                            </ext:Button>
                            <ext:Button runat="server" ID="bntDelect" Text="删除" Icon="UserDelete">
                                <DirectEvents>
                                    <Click OnEvent="Delet">
                                        <Confirmation Message="真的要删除吗？" Title="提示" ConfirmRequest="true" />
                                        <EventMask Msg="正在删除数据！请稍后……" ShowMask="true" />
                                    </Click>
                                </DirectEvents>
                            </ext:Button>
                        </Items>
                    </ext:Toolbar>
                </TopBar>
                <ColumnModel>
                    <Columns>
                       <ext:RowNumbererColumn Header="序"></ext:RowNumbererColumn>
                       <ext:Column Header="标题" DataIndex="Tile" Width="300" ></ext:Column>
                       <ext:Column Header="类型" DataIndex="GgTypeID"></ext:Column>
                       <ext:Column Header="创建人" DataIndex="RenYuanId"></ext:Column>
                       <ext:Column Header="创建时间" DataIndex="CreatTime"></ext:Column>
                    </Columns>
                </ColumnModel>
                <BottomBar>
                    <ext:PagingToolbar runat="server" PageSize="10"> </ext:PagingToolbar>
                </BottomBar>
                <SelectionModel><ext:CheckboxSelectionModel SingleSelect="true"></ext:CheckboxSelectionModel></SelectionModel>
              </ext:GridPanel>
            </Items>
        </ext:Viewport>

          <ext:Window ID="window_addGonGao"  runat="server"  Height="380" Icon="User"
            Width="850" AutoScroll="true" Modal="true" Layout="FormLayout" Hidden="true" Resizable="true">
            <Items>
                 <ext:FormPanel ID="FormPanel1" runat="server" ButtonAlign="Right" Layout="FormLayout"  LabelAlign="Right" MonitorValid="true" >
                    <Items>
                      <ext:Container ID="conTou" runat="server" Layout="ColumnLayout" Height="30">
                        <Items>
                              <ext:Container ID="cont1" runat="server" Layout="FormLayout"  ColumnWidth=".5">
                                <Items>
                                    <ext:ComboBox ID="combType" runat="server" FieldLabel="公告类型" StoreID="StorType" IndicatorText="*" IndicatorCls="tipCls" AllowBlank="false" MsgTarget="Side" LabelWidth="70" ValueField="GgTypeID" DisplayField="TypeName" Width="150">
                                    </ext:ComboBox>
                                </Items>
                              </ext:Container>
                              <ext:Container ID="Container1" runat="server" Layout="FormLayout" ColumnWidth=".5">
                                <Items>
                                    <ext:TextField runat="server" ID="txtTileAdd" FieldLabel="标题" Width="200" IndicatorText="*" IndicatorCls="tipCls" AllowBlank="false" MsgTarget="Side"></ext:TextField>
                                </Items>
                              </ext:Container>
                        </Items>
                      </ext:Container>
                       <ext:Container ID="Container4" runat="server" Height="270" Layout="FormLayout" AnchorHorizontal="90%">
                            <Content>
                                <table style="width: 760px; margin-left: -4px; table-layout: fixed;">
                                    <tr>
                                        <td valign="top" style="width: 101px; font-size: 13px; text-align: right;">
                                            公告内容:
                                        </td>
                                        <td style="padding-left: 4px; text-align: left; width: 675px;">
                                            <textarea id="txt_TiMuNeiRong" name="txt_TiMuNeiRong" style="width: 668px; height: 270px;
                                                margin-bottom: 10px;"></textarea>
                                        </td>
                                    </tr>
                                </table>
                            </Content>
                        </ext:Container>
                        <ext:Hidden ID="hidTiMuNeiRong" runat="server">
                        </ext:Hidden>
                    </Items>
                     <Buttons>
                        <ext:Button ID="bntAdds" runat="server" Text="添加" Icon="UserAdd" FormBind="true">
                            <DirectEvents>
                               <Click OnEvent="Add">
                                <EventMask Msg="正在执行操作" ShowMask="true"/>
                                 <ExtraParams>
                                        <ext:Parameter Name="txt_TiMuNeiRong" Value="editor1.html()" Mode="Raw">
                                        </ext:Parameter>
                                 </ExtraParams>
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                        <ext:Button ID="bntEids" runat="server" Text="修改" Icon="UserEdit" FormBind="true">
                            <DirectEvents>
                                <Click OnEvent="EidSever">
                                    <EventMask Msg="正在执行操作……" ShowMask="true" />
                                    <ExtraParams>
                                        <ext:Parameter Name="txt_TiMuNeiRong" Value="editor1.html()" Mode="Raw">
                                        </ext:Parameter>
                                   </ExtraParams>
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                        <ext:Button ID="bntClos" runat="server" Text="关闭" Icon="Cross">
                            <Listeners>
                                <Click Handler="#{window_addGonGao}.hide();" />
                            </Listeners>
                        </ext:Button>
                    </Buttons>
                 </ext:FormPanel>
            </Items>
            <Listeners>
                <Show Handler="editor1.html(#{hidTiMuNeiRong}.getValue());editor1.fullscreen(false);" />
            </Listeners>
            </ext:Window>
        <ext:Hidden ID="hidname" runat="server"></ext:Hidden>
          <ext:Window ID="window_selRy" runat="server" Height="400" Icon="Application" Width="950" Modal="true" Hidden="true">
            <AutoLoad Mode="IFrame"></AutoLoad>
            <Buttons>
                <ext:Button ID="Button2" runat="server" Text="选择" Icon="Accept">
                    <DirectEvents>
                        <Click OnEvent="SelectedRY"></Click>
                    </DirectEvents>
                </ext:Button>
                <ext:Button ID="Button4" runat="server" Text="清空" Icon="Delete">
                    <Listeners>
                        <Click Handler="#{trgname}.clear();#{hidname}.clear();#{window_selRy}.hide();"/>
                    </Listeners>
                </ext:Button>
            </Buttons>
        </ext:Window>

          <ext:Window ID="window_GgSele"  runat="server"  Height="380" Icon="User"
            Width="850" AutoScroll="true" Modal="true" Title="公告详情" Layout="FormLayout" Padding="0" Hidden="true" Resizable="true">
            <Items>
                 <ext:FormPanel ID="FormPanel2" runat="server" ButtonAlign="Right" Layout="FormLayout"  LabelAlign="Right" MonitorValid="true" >
                    <Items>
                      <ext:Container ID="Container2" runat="server" Layout="ColumnLayout" Height="50">
                        <Items>
                              <ext:Container ID="Container3" runat="server" Layout="FormLayout" ColumnWidth=".5">
                                <Items>
                                   <ext:DisplayField ID="disType" runat="server" FieldLabel="公告类型"></ext:DisplayField>
                                   <ext:DisplayField ID="disTime" runat="server" FieldLabel="创建时间"></ext:DisplayField>
                                </Items>
                              </ext:Container>
                              <ext:Container ID="Container5" runat="server" Layout="FormLayout" ColumnWidth=".5">
                                <Items>
                                    <ext:DisplayField runat="server" ID="TextSeleTile" FieldLabel="标题" ></ext:DisplayField>
                                </Items>
                              </ext:Container>
                        </Items>
                      </ext:Container>
                       <ext:DisplayField ID="disConts" runat="server" FieldLabel="公告内容" Width="700" Height="250" AutoScroll="true"></ext:DisplayField>
                    </Items>
                 </ext:FormPanel>
            </Items>
               <Buttons>
                    <ext:Button ID="Button5" runat="server" Text="关闭" Icon="Cross">
                        <Listeners>
                            <Click Handler="#{window_GgSele}.hide();" />
                        </Listeners>
                    </ext:Button>
               </Buttons>
            </ext:Window>
    </div>
    </form>
</body>
</html>
