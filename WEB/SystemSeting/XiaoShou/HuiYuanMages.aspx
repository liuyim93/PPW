<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HuiYuanMages.aspx.cs" Inherits="WEB.SystemSeting.XiaoShou.HuiYuanMages" %>
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
        <ext:JsonReader IDProperty="HuiYuanID">
            <Fields>
                <ext:RecordField Name="HuiYuanID"></ext:RecordField>
                <ext:RecordField Name="HuiYuanName"></ext:RecordField>
                <ext:RecordField Name="MM"></ext:RecordField>
                <ext:RecordField Name="email"></ext:RecordField>
                <ext:RecordField Name="prName"></ext:RecordField>
                <ext:RecordField Name="sex"></ext:RecordField>
                <ext:RecordField Name="sfz"></ext:RecordField>
                <ext:RecordField Name="sjh"></ext:RecordField>
                <ext:RecordField Name="PaiDian"></ext:RecordField>
                <ext:RecordField Name="JiFen"></ext:RecordField>
                <ext:RecordField Name="DJ"></ext:RecordField>
                <ext:RecordField Name="CreatTime"></ext:RecordField>
            </Fields>
        </ext:JsonReader>
    </Reader>
</ext:Store>
    <form id="form1" runat="server">
    <div>
         <ext:Viewport ID="prot" runat="server" Layout="BorderLayout">
            <Items>
                <ext:Panel runat="server" Title="查询" Layout="HBoxLayout" Region="North"  Height="60" LabelAlign="Right" Icon="Zoom">
                    <LayoutConfig><ext:HBoxLayoutConfig Align="Middle" /></LayoutConfig>
                    <Items>
                        <ext:TextField FieldLabel="会员名" runat="server" ID="textsHuiYanName"></ext:TextField>
                        <ext:TextField FieldLabel="真实名" runat="server" ID="textName"></ext:TextField>
                        <ext:ComboBox ID="comxDj" runat="server" FieldLabel="等级" Width="260">
                            <Items>
                                <ext:ListItem  Value="普通会员" Text="普通会员"/>
                                <ext:ListItem Value="白银会员" Text="白银会员" />
                                <ext:ListItem Value="黄金会员" Text="黄金会员" />
                                <ext:ListItem Value="钻石会员" Text="钻石会员" />
                            </Items>
                            <Triggers><ext:FieldTrigger Icon="Clear" /></Triggers>
                            <Listeners>
                                <TriggerClick Handler="this.clear();" />
                            </Listeners>
                        </ext:ComboBox>
                        <ext:Button ID="bntSele" runat="server" Text="查询" Icon="Zoom" Margins="0 0 0 20">
                            <Listeners>
                                <Click Handler="#{Grid}.reload();" />
                            </Listeners>
                        </ext:Button>
                    </Items>
              </ext:Panel>
               <ext:GridPanel ID="Grid" runat="server" Region="Center" StoreID="StoreList">
                    <LoadMask Msg="数据正在加载……" ShowMask="true" />
                    <ColumnModel>
                        <Columns>
                            <ext:Column DataIndex="HuiYuanName" Header="会员名"></ext:Column>
                            <ext:Column DataIndex="MM" Header="密码"></ext:Column>
                            <ext:Column DataIndex="prName" Header="真实名"></ext:Column>
                            <ext:Column DataIndex="sex" Header="性别"></ext:Column>
                            <ext:Column DataIndex="email" Header="电子邮箱" Width="120"></ext:Column>
                            <ext:Column DataIndex="sfz" Header="身份证"></ext:Column>
                            <ext:Column DataIndex="sjh" Header="联系电话"></ext:Column>
                            <ext:Column DataIndex="PaiDian" Header="拍卖点"></ext:Column>
                            <ext:Column DataIndex="JiFen" Header="积分"></ext:Column>
                            <ext:Column DataIndex="DJ" Header="会员等级"></ext:Column>
                            <ext:Column DataIndex="CreatTime" Header="创建时间"></ext:Column>
                        </Columns>
                    </ColumnModel>
                    <SelectionModel><ext:RowSelectionModel></ext:RowSelectionModel></SelectionModel>
                    <BottomBar><ext:PagingToolbar PageSize="10" runat="server"></ext:PagingToolbar></BottomBar>
                </ext:GridPanel>
            </Items>
         </ext:Viewport>
    </div>
    </form>
</body>
</html>
