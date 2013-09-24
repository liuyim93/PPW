<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="User.aspx.cs" Inherits="WEB.SystemSeting.QuanXian.User" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        var CountrySelector = {
            add: function (source, destination) {
                source = source || gpalldw;
                destination = destination || gpseldw;

                if (source.hasSelection()) {
                    var records = source.selModel.getSelections();
                    source.deleteSelected();
                    destination.store.add(records);
                }
            },
            addAll: function (source, destination) {
                source = source || gpalldw;
                destination = destination || gpseldw;
                var records = source.store.getRange();
                source.store.removeAll();
                destination.store.add(records);
            },
            addByName: function (name) {
                if (!Ext.isEmpty(name)) {
                    var result = Store1.query("Name", name);

                    if (!Ext.isEmpty(result.items)) {
                        gpalldw.store.remove(result.items[0]);
                        gpseldw.store.add(result.items[0]);
                    }
                }
            },
            addByNames: function (name) {
                for (var i = 0; i < name.length; i++) {
                    this.addByName(name[i]);
                }
            },
            remove: function (source, destination) {
                this.add(destination, source);
            },
            removeAll: function (source, destination) {
                this.addAll(destination, source);
            }
        };
    </script>
</head>
<body>
    <ext:ResourceManager ID="ResourceManager1" runat="server">
    </ext:ResourceManager>
    <ext:Store ID="Store1" runat="server" OnRefreshData="RefData">
        <Reader>
            <ext:JsonReader IDProperty="YongHuId">
                <Fields>
                    <ext:RecordField Name="YongHuId">
                    </ext:RecordField>
                    <ext:RecordField Name="Lx">
                    </ext:RecordField>
                    <ext:RecordField Name="YHM">
                    </ext:RecordField>
                    <ext:RecordField Name="MM">
                    </ext:RecordField>
                    <ext:RecordField Name="BZ">
                    </ext:RecordField>
                    <ext:RecordField Name="status">
                    </ext:RecordField>
                    <ext:RecordField Name="name">
                    </ext:RecordField>
                    <ext:RecordField Name="jsname">
                    </ext:RecordField>
                </Fields>
            </ext:JsonReader>
        </Reader>
    </ext:Store>
    <form id="form1" runat="server">
    <div>
        <ext:Viewport runat="server" Layout="BorderLayout">
            <Items>
                <ext:Panel ID="Panel1" runat="server" Region="North" Title="查询条件" Icon="Zoom" Height="70" AutoScroll="true"
                    Layout="HBoxLayout" LabelAlign="Right">
                    <LayoutConfig>
                        <ext:HBoxLayoutConfig Align="Middle" />
                    </LayoutConfig>
                    <Items>
                        <ext:TextField ID="txtname" runat="server" FieldLabel="姓名">
                        </ext:TextField>
                        <ext:TextField ID="txtyhm" runat="server" FieldLabel="用户名">
                        </ext:TextField>
                        <ext:ComboBox ID="cmbstarus" runat="server" FieldLabel="状态" Editable="false" SelectedIndex="0" Width="200">
                            <Items>
                                <ext:ListItem Text="所有" Value="" />
                                <ext:ListItem Text="正常" Value="1" />
                                <ext:ListItem Text="禁用" Value="5" />
                            </Items>
                        </ext:ComboBox>
                        <ext:Button ID="btnsel" runat="server" Text="查询" Icon="Zoom" Margins="0 0 0 50">
                            <Listeners>
                                <Click Handler="#{GridPanel1}.reload()"/>
                            </Listeners>
                        </ext:Button>
                    </Items>
                </ext:Panel>
                <ext:GridPanel ID="GridPanel1" runat="server" StoreID="Store1" AutoDoLayout="true"
                    StripeRows="true" Region="Center">
                    <ColumnModel>
                        <Columns>
                            <ext:RowNumbererColumn Header="序">
                            </ext:RowNumbererColumn>
                            <ext:Column Header="姓名" DataIndex="name" Width="100">
                            </ext:Column>
                            <ext:Column Header="用户名" DataIndex="YHM" Width="120">
                            </ext:Column>
                            <ext:Column Header="密码" DataIndex="MM" Width="120">
                            </ext:Column>
                            <ext:Column Header="所属角色" DataIndex="jsname" Width="150">
                            </ext:Column>
                            <ext:Column Header="备注" DataIndex="BZ" Width="210">
                            </ext:Column>
                            <ext:Column Header="状态" DataIndex="status" Width="40">
                            </ext:Column>
                            <ext:CommandColumn Header="操作" Width="240" ColumnID="cmd">
                                <Commands>
                                    <ext:GridCommand Icon="User" Text="查看" CommandName="sel">
                                    </ext:GridCommand>
                                    <ext:GridCommand Icon="UserEdit" Text="修改" CommandName="edit">
                                    </ext:GridCommand>
                                    <ext:GridCommand Icon="UserKey" Text="授权" CommandName="sq">
                                    </ext:GridCommand>
                                    <ext:GridCommand Icon="PageAttach" Text="权限范围" ToolTip-Text="设置报表的查询范围" CommandName="qxfw">
                                    </ext:GridCommand>
                                </Commands>
                            </ext:CommandColumn>
                        </Columns>
                    </ColumnModel>
                    <DirectEvents>
                        <Command OnEvent="Cmd">
                            <ExtraParams>
                                <ext:Parameter Name="type" Value="command" Mode="Raw"></ext:Parameter>
                                <ext:Parameter Name="id" Value="record.data.YongHuId" Mode="Raw"></ext:Parameter>
                            </ExtraParams>
                            <EventMask ShowMask="true" Msg="加载中..." />
                        </Command>
                    </DirectEvents>
                    <LoadMask ShowMask="true" Msg="数据正在加载..." />
                    <TopBar>
                        <ext:Toolbar runat="server">
                            <Items>
                                <ext:Button ID="btnadd" runat="server" Text="添加" Icon="UserAdd">
                                    <Listeners>
                                        <Click Handler="#{cmbstatus1}.hide();#{btneditsave}.hide();#{btnaddsave}.show();#{btnreset}.show();
                                        #{hidoldhym}.value='';#{FormPanel1}.getForm().reset();#{window_adduser}.setTitle('添加用户信息');#{window_adduser}.show()" />
                                    </Listeners>
                                </ext:Button>
                                <ext:Button ID="btndel" runat="server" Text="删除" Icon="UserDelete">
                                    <DirectEvents>
                                        <Click OnEvent="DelData">
                                            <EventMask ShowMask="true" Msg="正在删除..." />
                                            <Confirmation ConfirmRequest="true" Title="提示" Message="确认删除？" />
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                    <BottomBar>
                        <ext:PagingToolbar runat="server" PageSize="10">
                        </ext:PagingToolbar>
                    </BottomBar>
                    <SelectionModel>
                        <ext:CheckboxSelectionModel runat="server" SingleSelect="false">
                        </ext:CheckboxSelectionModel>
                    </SelectionModel>
                </ext:GridPanel>
            </Items>
        </ext:Viewport>

        <ext:Store ID="StoreJueSe" runat="server">
            <Reader>
                <ext:JsonReader IDProperty="JueSeId">
                    <Fields>
                        <ext:RecordField Name="JueSeId">
                        </ext:RecordField>
                        <ext:RecordField Name="JSMC">
                        </ext:RecordField>
                    </Fields>
                </ext:JsonReader>
            </Reader>
        </ext:Store>

        <ext:Window ID="window_adduser" runat="server" Height="330" Icon="UserAdd" Title="添加用户"
            Width="400" AutoScroll="true" Modal="true" Hidden="true">
            <Items>
                <ext:FormPanel ID="FormPanel1" runat="server" ButtonAlign="Right" Height="295" Padding="5" MonitorValid="true">
                    <Items>
                        <ext:Hidden ID="hiduserid" runat="server">
                        </ext:Hidden>
                        <ext:TriggerField ID="trgname" runat="server" FieldLabel="人员" IndicatorText="*" IndicatorCls="tipCls"  AllowBlank="false" MsgTarget="Side" Editable="false">
                            <Triggers>
                                <ext:FieldTrigger Icon="Search"/>
                            </Triggers>
                            <DirectEvents>
                                <TriggerClick OnEvent="SelRY"></TriggerClick>
                            </DirectEvents>
                        </ext:TriggerField>
                        <ext:Hidden ID="hidname" runat="server">
                        </ext:Hidden>
                        <ext:TextField ID="yhm" runat="server" FieldLabel="用户名" IndicatorText="*" IndicatorCls="tipCls"  AllowBlank="false" IsRemoteValidation="true"
                            MsgTarget="Side">
                            <RemoteValidation OnValidation="YzHym">
                            </RemoteValidation>
                        </ext:TextField>
                        <ext:Hidden ID="hidoldhym" runat="server">
                        </ext:Hidden>
                        <ext:TextField ID="txtmm" runat="server" FieldLabel="密码" IndicatorText="*" IndicatorCls="tipCls"  AllowBlank="false" InputType="Password"
                            MsgTarget="Side">
                        </ext:TextField>
                        <ext:TextField ID="txtmm1" runat="server" FieldLabel="确认密码" InputType="Password" IndicatorText="*" IndicatorCls="tipCls"  AllowBlank="false"
                            Vtype="password" MsgTarget="Side">
                            <CustomConfig>
                                <ext:ConfigItem Value="#{txtmm}" Mode="Value" Name="initialPassField">
                                </ext:ConfigItem>
                            </CustomConfig>
                        </ext:TextField>
                        <ext:ComboBox ID="cmbjs" runat="server" Editable="false" FieldLabel="所属角色" StoreID="StoreJueSe"
                            DisplayField="JSMC" ValueField="JueSeId">
                        </ext:ComboBox>
                        <ext:ComboBox ID="cmbstatus1" runat="server" FieldLabel="状态" IndicatorText="*" IndicatorCls="tipCls"  AllowBlank="false" Editable="false" SelectedIndex="0">
                            <Items>
                                <ext:ListItem Text="正常" value="1"/>
                                <ext:ListItem Text="禁用" value="5"/>
                            </Items>
                        </ext:ComboBox>
                        <ext:TextArea ID="txtbz" runat="server" AnchorHorizontal="100%" Height="60" FieldLabel="用户描述">
                        </ext:TextArea>
                    </Items>
                    <Buttons>
                        <ext:Button ID="btnaddsave" runat="server" Icon="Disk" Text="添加" FormBind="true">
                            <DirectEvents>
                                <Click OnEvent="Save">
                                    <EventMask ShowMask="true" Msg="保存中..."/>
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                        <ext:Button ID="btneditsave" runat="server" Text="修改" FormBind="true" Icon="Disk">
                            <DirectEvents>
                                <Click OnEvent="Edit">
                                    <EventMask ShowMask="true" Msg="保存中..."/>
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                        <ext:Button ID="btnreset" runat="server" Text="重置" Icon="Cancel">
                            <Listeners>
                                <Click Handler="#{FormPanel1}.getForm().reset();"/>
                            </Listeners>
                        </ext:Button>
                        <ext:Button ID="Button3" runat="server" Text="关闭" Icon="Cross">
                            <Listeners>
                                <Click Handler="#{window_adduser}.hide()"/>
                            </Listeners>
                        </ext:Button>
                    </Buttons>
                </ext:FormPanel>
            </Items>
            <Listeners>
                <Show Handler="#{yhm}.remoteValidate()"/>
            </Listeners>
        </ext:Window>

        <ext:Window ID="window_seluser" runat="server"  Height="300" Icon="User"
            Title="用户信息" Width="400" Modal="true" Hidden="true">
            <AutoLoad Mode="IFrame"></AutoLoad>
            <Buttons>
                <ext:Button ID="Button1" runat="server" Text="关闭" Icon="Cancel">
                    <Listeners>
                        <Click Handler="#{window_seluser}.hide();"/>
                    </Listeners>
                </ext:Button>
            </Buttons>
        </ext:Window>
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
        <ext:Window ID="window_qxfw" runat="server" Resizable="false" Height="440" Icon="PageAttach" Modal="true" Hidden="true"
            Title="权限范围设置" Width="560" Layout="ColumnLayout">
            <Items>
                <ext:GridPanel ID="gpalldw" runat="server" ColumnWidth="0.45">
                    <Store>
                        <ext:Store runat="server" ID="storealldw">
                            <Reader>
                                <ext:JsonReader IDProperty="Id">
                                    <Fields>
                                        <ext:RecordField Name="Id"></ext:RecordField>
                                        <ext:RecordField Name="MC"></ext:RecordField>
                                    </Fields>
                                </ext:JsonReader>
                            </Reader>
                        </ext:Store>
                    </Store>
                    <ColumnModel>
                        <Columns>
                            <ext:Column Header="所有单位" DataIndex="MC" Width="215"></ext:Column>
                        </Columns>
                    </ColumnModel>
                    <SelectionModel>
                        <ext:CheckboxSelectionModel runat="server" SingleSelect="false"></ext:CheckboxSelectionModel>
                    </SelectionModel>
                </ext:GridPanel>
                <ext:Container runat="server" ColumnWidth="0.1" Layout="VBoxLayout">
                    <LayoutConfig>
                        <ext:VBoxLayoutConfig Align="Center"/>
                    </LayoutConfig>
                    <Items>
                        <ext:Button ID="Button7" runat="server" Icon="ResultsetNext" Margins="10 0 0 0">
                            <Listeners>
                                <Click Handler="CountrySelector.add();"/>
                            </Listeners>
                        </ext:Button>
                        <ext:Button ID="Button8" runat="server" Icon="ResultsetLast" Margins="10 0 0 0">
                            <Listeners>
                                <Click Handler="CountrySelector.addAll();" />
                            </Listeners>
                        </ext:Button>
                        <ext:Button ID="Button9" runat="server" Icon="ResultsetPrevious" Margins="10 0 0 0">
                            <Listeners>
                                <Click Handler="CountrySelector.remove(gpalldw, gpseldw);" />
                            </Listeners>
                        </ext:Button>
                        <ext:Button ID="Button10" runat="server" Icon="ResultsetFirst" Margins="10 0 0 0">
                            <Listeners>
                                <Click Handler="CountrySelector.removeAll(gpalldw, gpseldw);" />
                            </Listeners>
                        </ext:Button>
                    </Items>
                </ext:Container>
                <ext:GridPanel ID="gpseldw" runat="server" ColumnWidth="0.45">
                    <Store>
                        <ext:Store runat="server" ID="storeseldw" OnSubmitData="SaveSelDW">
                            <Reader>
                                <ext:JsonReader IDProperty="Id">
                                    <Fields>
                                        <ext:RecordField Name="Id"></ext:RecordField>
                                        <ext:RecordField Name="MC"></ext:RecordField>
                                    </Fields>
                                </ext:JsonReader>
                            </Reader>
                        </ext:Store>
                    </Store>
                    <SaveMask ShowMask="true" Msg="保存中..."/>
                    <ColumnModel>
                        <Columns>
                            <ext:Column Header="已选单位" DataIndex="MC" Width="215"></ext:Column>
                        </Columns>
                    </ColumnModel>
                    <SelectionModel>
                        <ext:CheckboxSelectionModel ID="CheckboxSelectionModel1" runat="server" SingleSelect="false"></ext:CheckboxSelectionModel>
                    </SelectionModel>
                </ext:GridPanel>
            </Items>
            <Buttons>
                <ext:Button ID="Button5" runat="server" Text="保存" Icon="Disk">
                    <Listeners>
                        <Click Handler="#{gpseldw}.submitData()"/>
                    </Listeners>
                </ext:Button>
                <ext:Button ID="Button6" runat="server" Text="关闭" Icon="Cancel">
                    <Listeners>
                        <Click Handler="#{window_qxfw}.hide()"/>
                    </Listeners>
                </ext:Button>
            </Buttons>
        </ext:Window>
    </div>
    </form>
</body>
</html>
