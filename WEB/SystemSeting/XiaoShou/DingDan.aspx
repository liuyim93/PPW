<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DingDan.aspx.cs" Inherits="WEB.SystemSeting.XiaoShou.DingDan" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
     </script>
</head>
<ext:ResourceManager ID="ResourceManager1" runat="server">
</ext:ResourceManager>
<body>
<ext:Store ID="storList" runat="server" OnRefreshData="Ref">
    <Reader>
        <ext:JsonReader IDProperty="DingDanID">
            <Fields>
                <ext:RecordField Name="DingDanID"></ext:RecordField>
                <ext:RecordField Name="DingDanBH"></ext:RecordField>
                <ext:RecordField Name="OrderTypeID"></ext:RecordField>
                <ext:RecordField Name="ShouHuoDZID"></ext:RecordField>
                <ext:RecordField Name="HuiYuanID"></ext:RecordField>
                <ext:RecordField Name="ProductID"></ext:RecordField>
                <ext:RecordField Name="DingDanTime"></ext:RecordField>
                <ext:RecordField Name="ShouHuoName"></ext:RecordField>
                <ext:RecordField Name="Mode"></ext:RecordField>
                <ext:RecordField Name="DZ"></ext:RecordField>
                <ext:RecordField Name="YouBian"></ext:RecordField>
                <ext:RecordField Name="Status"></ext:RecordField>
            </Fields>
        </ext:JsonReader>
    </Reader>
</ext:Store>
    <form id="form1" runat="server">
    <div>
    <ext:Viewport ID="cip" runat="server" Layout="BorderLayout">
        <Items>
        <ext:Panel ID="panl1" runat="server" Layout="HBoxLayout" Region="North" Title="条件查询" Icon="Zoom" Height="60" LabelAlign="Right">
            <LayoutConfig><ext:HBoxLayoutConfig  Align="Middle"/></LayoutConfig>
            <Items>
                <ext:TextField ID="txtBH" runat="server" FieldLabel="订单编号"></ext:TextField>
                <ext:ComboBox ID="cmdStat" runat="server" FieldLabel="状态" Width="200">
                   <Items>
                    <ext:ListItem Text="已发货" Value="9" />
                    <ext:ListItem Text="待发货" Value="8" />
                    <ext:ListItem Text="完成订单" Value="7" />
                    <ext:ListItem Text="退订单" Value="6" />
                   </Items>
                   <Triggers><ext:FieldTrigger Icon="Clear" /></Triggers>
                   <Listeners>
                    <TriggerClick  Handler="this.clear();"/>
                   </Listeners>
                </ext:ComboBox>
                 <ext:DateField ID="xtxtbegin" runat="server" FieldLabel="下单时间" LabelWidth="110"  Width="220" Format="yyyy-MM-dd"
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
                <ext:Button ID="bntSel" runat="server" Text="查询" Icon="Zoom" Margins="0 0 0 20">
                    <Listeners>
                        <Click Handler="#{Grid}.reload();" />
                    </Listeners>
                </ext:Button>
            </Items>
        </ext:Panel>

        <ext:GridPanel ID="Grid" runat="server" StoreID="storList" Region="Center">
            <LoadMask Msg="数据正在加载……" ShowMask="true" />
            <ColumnModel>
                <Columns>
                    <ext:Column DataIndex="DingDanBH" Header="订单编号"></ext:Column>
                    <ext:Column DataIndex="OrderTypeID" Header="订单类型"></ext:Column>
                    <ext:Column DataIndex="HuiYuanID" Header="会员名"></ext:Column>
                    <ext:Column DataIndex="ProductID" Header="产品"></ext:Column>
                    <ext:Column DataIndex="ShouHuoName" Header="收货人"></ext:Column>
                    <ext:Column DataIndex="Mode" Header="联系电话"></ext:Column>
                    <ext:Column DataIndex="DZ" Header="地址"></ext:Column>
                    <ext:Column DataIndex="YouBian" Header="邮编"></ext:Column>
                    <ext:Column DataIndex="DingDanTime" Header="下单时间"></ext:Column>
                    <ext:Column DataIndex="Status" Header="状态"></ext:Column>
                </Columns>
            </ColumnModel>
            <TopBar>
                <ext:Toolbar runat="server">
                    <Items>
                        <ext:Button ID="bntFH" runat="server"  Icon="AwardStarBronze1" Text="发货">
                            <DirectEvents>
                                <Click OnEvent="SetSata"></Click>
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
                <ext:CheckboxSelectionModel SingleSelect="true"></ext:CheckboxSelectionModel>
            </SelectionModel>
        </ext:GridPanel>
        </Items>
    </ext:Viewport>
    </div>
    </form>
</body>
</html>
