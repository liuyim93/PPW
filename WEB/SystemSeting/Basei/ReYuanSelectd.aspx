<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReYuanSelectd.aspx.cs" Inherits="WEB.SystemSeting.Basei.ReYuanSelectd" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<ext:ResourceManager ID="ResourceManager1" runat="server">
</ext:ResourceManager>
<body>
    <%--人员列表--%>
    <ext:Store runat="server" ID="ReYuanList" OnRefreshData="Ref">
        <Reader>
            <ext:JsonReader IDProperty="RenYuanId">
                <Fields>
                    <ext:RecordField Name="RenYuanId"></ext:RecordField>
                    <ext:RecordField Name="PersonName"></ext:RecordField>
                    <ext:RecordField Name="Sex"></ext:RecordField>
                    <ext:RecordField Name="ZhiWei"></ext:RecordField>
                    <ext:RecordField Name="SFZ"></ext:RecordField>
                    <ext:RecordField Name="Email"></ext:RecordField>
                    <ext:RecordField Name="Mobile"></ext:RecordField>
                </Fields>
            </ext:JsonReader>
        </Reader>
    </ext:Store>
    <form id="form2" runat="server">
    <div>
       <ext:Viewport ID="viprot" runat="server" Layout="BorderLayout" >
         <Items>
              <ext:Panel ID="Panel1" runat="server" Region="North" Icon="Zoom" Layout="HBoxLayout" LabelAlign="Right" Height="70" Title="查询条件" LabelWidth="50" AutoScroll="true" >
                <LayoutConfig>
                    <ext:HBoxLayoutConfig Align="Middle"/>
                </LayoutConfig>
                 <Items>
                     <ext:TextField ID="txtXM" FieldLabel="姓名" runat="server"></ext:TextField>
                     <ext:ComboBox ID="comXB" Width="150" runat="server" FieldLabel="性别">
                        <Items>
                            <ext:ListItem Text="男" Value="男" />
                            <ext:ListItem Text="女" Value="女" />
                        </Items>
                        <Triggers><ext:FieldTrigger Icon="Clear" /></Triggers>
                        <Listeners>
                            <TriggerClick Handler="this.clear();" />
                        </Listeners>
                     </ext:ComboBox>
                     <ext:TextField ID="txtZW" FieldLabel="职位" runat="server"></ext:TextField>
                     <ext:Button ID="bntSele" runat="server" Text="查询" Icon="Zoom" Margins="0 0 0 20">
                        <Listeners><Click Handler="#{GridPanel1}.reload();" /></Listeners>
                     </ext:Button>
                 </Items>
              </ext:Panel>
             <ext:GridPanel ID="GridPanel1" runat="server"  StoreID="ReYuanList" StripeRows="true" AutoDoLayout="true" Region="Center">
                <LoadMask Msg="数据正在加载……"  ShowMask="true"/>
                <ColumnModel>
                    <Columns>
                        <ext:RowNumbererColumn Header="序"></ext:RowNumbererColumn>
                        <ext:Column Header="姓名" Width="120" DataIndex="PersonName"></ext:Column>
                        <ext:Column Header="性别" DataIndex="Sex" Width="80"></ext:Column>
                        <ext:Column Header="职位" DataIndex="ZhiWei" Width="150"></ext:Column>
                        <ext:Column Header="身份证号" DataIndex="SFZ" Width="200"></ext:Column>
                        <ext:Column Header="电子邮箱" DataIndex="Email" Width="150"></ext:Column>
                        <ext:Column Header="联系电话" DataIndex="Mobile" Width="150"></ext:Column>
                    </Columns>
                </ColumnModel>
                <SelectionModel>
                    <ext:CheckboxSelectionModel SingleSelect="true">
                           <DirectEvents>
                                <SelectionChange OnEvent="SelChange"></SelectionChange>
                            </DirectEvents>
                    </ext:CheckboxSelectionModel>
                </SelectionModel>
                <BottomBar>
                    <ext:PagingToolbar ID="PagingToolbar1" PageSize="10" runat="server"></ext:PagingToolbar>
                </BottomBar>
             </ext:GridPanel>
         </Items>
         
       </ext:Viewport>
    </div>
    </form>
</body>
</html>
