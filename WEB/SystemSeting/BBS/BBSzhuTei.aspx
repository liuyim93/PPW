<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BBSzhuTei.aspx.cs" Inherits="WEB.SystemSeting.BBS.BBSzhuTei" %>
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
<ext:Store runat="server" ID="StoreList" OnRefreshData="Ref">
    <Reader>
        <ext:JsonReader IDProperty="TzID">
            <Fields>
                <ext:RecordField Name="TzID"></ext:RecordField>
                <ext:RecordField Name="Tile"></ext:RecordField>
                <ext:RecordField Name="Creatr"></ext:RecordField>
                <ext:RecordField Name="HuiYuanID"></ext:RecordField>
                <ext:RecordField Name="CreateTime"></ext:RecordField>
            </Fields>
        </ext:JsonReader>
    </Reader>
</ext:Store>
    <form id="form1" runat="server">
    <div>
    <ext:Viewport runat="server" Layout="BorderLayout">
        <Items>
            <ext:Panel runat="server" Title="查询条件" Layout="HBoxLayout" Height="60" Region="North" LabelAlign="Right">
                <LayoutConfig><ext:HBoxLayoutConfig Align="Middle" /></LayoutConfig>
                <Items>
                    <ext:TextField ID="txtTil" runat="server" FieldLabel="标题"></ext:TextField>
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
                    <ext:Button ID="bnt" runat="server" Text="查询" Icon="Zoom" Margins="0 0 0 20">
                        <Listeners>
                            <Click Handler="#{GridPanel1}.reload();" />
                        </Listeners>
                    </ext:Button>
                </Items>
            </ext:Panel>     
            <ext:GridPanel ID="GridPanel1" runat="server" Region="Center" StoreID="StoreList">
                <LoadMask Msg="数据正在加载……" ShowMask="true" />
                <ColumnModel>
                    <Columns>
                        <ext:RowNumbererColumn Header="序"></ext:RowNumbererColumn>
                        <ext:Column DataIndex="Tile" Header="标题" Width="200"></ext:Column>
                        <ext:Column DataIndex="Creatr" Header="内容" Width="600"></ext:Column>
                        <ext:Column DataIndex="HuiYuanID" Header="发贴者" Width="120"></ext:Column>
                        <ext:Column DataIndex="CreateTime" Header="创建时间" Width="100"></ext:Column>
                    </Columns>
                </ColumnModel>
                <SelectionModel><ext:CheckboxSelectionModel SingleSelect="true"></ext:CheckboxSelectionModel></SelectionModel>
                <TopBar>
                    <ext:Toolbar runat="server">
                        <Items>
                            <ext:Button ID="bntSele" runat="server" Text="贴子详情" Icon="Bell">
                                <DirectEvents>
                                    <Click OnEvent="Selet"></Click>
                                </DirectEvents>
                            </ext:Button>
                            <ext:Button ID="bntdele" Icon="Delete" runat="server" Text="删除">
                                <DirectEvents>
                                    <Click OnEvent="Dele">
                                        <EventMask Msg="数据正在删除……" ShowMask="true" />
                                        <Confirmation Message="真的要删除选中的数据吗？" ConfirmRequest="true" Title="提示" />
                                    </Click>
                                </DirectEvents>
                            </ext:Button>
                        </Items>
                    </ext:Toolbar>
                </TopBar>
                <BottomBar>
                    <ext:PagingToolbar runat="server" PageSize="10"></ext:PagingToolbar>
                </BottomBar>
            </ext:GridPanel>   
        </Items>
    </ext:Viewport>

    <ext:Window ID="wind1" runat="server" Title="主贴详情" Layout="FormLayout" Icon="User" Hidden="true" Width="850" Height="380">
        <AutoLoad Mode="IFrame"></AutoLoad>
        <Buttons>
            <ext:Button ID="buttClos" runat="server" Text="关闭" Icon="Cancel">
                <Listeners>
                    <Click Handler="#{wind1}.hide();" />
                </Listeners> 
            </ext:Button>
        </Buttons>
    </ext:Window>
    </div>
    </form>
</body>
</html>
