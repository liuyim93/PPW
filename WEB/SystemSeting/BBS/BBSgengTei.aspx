<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BBSgengTei.aspx.cs" Inherits="WEB.SystemSeting.BBS.BBSgengTei" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<ext:ResourceManager ID="ResourceManager1" runat="server">
</ext:ResourceManager>
<body>
<ext:Store runat="server" ID="storeList">
    <Reader>
        <ext:JsonReader IDProperty="GtTzID">
            <Fields>
                <ext:RecordField Name="GtTzID"></ext:RecordField>
                <ext:RecordField Name="HuiYuanID"></ext:RecordField>
                <ext:RecordField Name="Contents"></ext:RecordField>
                <ext:RecordField Name="CreateTime"></ext:RecordField>
            </Fields>
        </ext:JsonReader>
    </Reader>
</ext:Store>

<ext:Store ID="storGetZ" runat="server">
    <Reader>
        <ext:JsonReader IDProperty="HfTzId">
            <Fields>
                <ext:RecordField Name="HfTzId"></ext:RecordField>
                <ext:RecordField Name="HuiYuanID"></ext:RecordField>
                <ext:RecordField Name="creats"></ext:RecordField>
                <ext:RecordField Name="CreateTime"></ext:RecordField>
            </Fields>
        </ext:JsonReader>
    </Reader>
</ext:Store>
    <form id="form1" runat="server">
    <div>
    <ext:Viewport ID="viep" runat="server" Layout="FitLayout">
        <Items>
        <ext:TabPanel ID="tap1" runat="server" Border="false">
            <Items>
                <ext:Panel ID="pan1" runat="server" Border="false" Layout="FormLayout" LabelAlign="Right" Title="主贴信息" Padding="5">
                    <Items>
                        <ext:DisplayField runat="server" ID="disTile" FieldLabel="标题"></ext:DisplayField>
                        <ext:DisplayField runat="server" ID="disFTr" FieldLabel="发贴人"></ext:DisplayField>
                        <ext:DisplayField runat="server" ID="disTime" FieldLabel="发贴时间"></ext:DisplayField>
                        <ext:DisplayField runat="server" ID="Creata" FieldLabel="发贴内容" AnchorHorizontal="80%" Height="200"></ext:DisplayField>
                    </Items>
                </ext:Panel>
                <ext:Panel ID="pan2" runat="server" Border="false" Layout="FormLayout" Title="跟贴信息">
                    <Items>
                        <ext:GridPanel ID="grid1" runat="server" Height="290" StoreID="storeList">
                            <TopBar>
                                <ext:Toolbar runat="server">
                                    <Items>
                                        <ext:Button ID="del" runat="server" Text="删除贴子" Icon="Delete">
                                            <DirectEvents>
                                                <Click OnEvent="deleGet">
                                                    <Confirmation Title="提示" Message="真的要删除选中的数据吗？" ConfirmRequest="true" />
                                                    <EventMask Msg="数据正在删除请稍后……" ShowMask="true" />
                                                </Click>
                                            </DirectEvents>
                                        </ext:Button>
                                        <ext:Button ID="delAll" runat="server" Text="删除所有贴子" Icon="ApplicationGo">
                                            <DirectEvents>
                                                <Click OnEvent="DeleAll">
                                                    <Confirmation Message="真的要删除所有贴子吗" Title="提示" ConfirmRequest="true" />
                                                    <EventMask Msg="数据正在删除……" ShowMask="true" />
                                                </Click>
                                            </DirectEvents>
                                        </ext:Button>
                                    </Items>
                                </ext:Toolbar>
                            </TopBar>
                            <ColumnModel>
                                <Columns>
                                    <ext:Column DataIndex="HuiYuanID" Header="发贴人"></ext:Column>
                                    <ext:Column DataIndex="Contents" Header="发贴内容" Width="500"></ext:Column>
                                    <ext:Column DataIndex="CreateTime" Header="发贴时间"></ext:Column>
                                    <ext:CommandColumn Header="操作" ColumnID="sele">
                                        <Commands>
                                            <ext:GridCommand Text="查看" Icon="UserBrown" CommandName="sele"></ext:GridCommand>
                                        </Commands>
                                    </ext:CommandColumn>
                                </Columns>
                            </ColumnModel>
                            <DirectEvents>
                                <Command OnEvent="cmd">
                                    <ExtraParams>
                                        <ext:Parameter Name="type" Value="command" Mode="Raw"></ext:Parameter>
                                        <ext:Parameter Name="id" Value="record.data.HfTzId" Mode="Raw"></ext:Parameter>
                                   </ExtraParams>
                                </Command>
                            </DirectEvents>
                            <SelectionModel><ext:CheckboxSelectionModel></ext:CheckboxSelectionModel></SelectionModel>
                            <BottomBar>
                                <ext:PagingToolbar runat="server" PageSize="10"></ext:PagingToolbar>
                            </BottomBar>
                        </ext:GridPanel>
                    </Items>
                </ext:Panel>
            </Items>
        </ext:TabPanel>
        </Items>
    </ext:Viewport>
      <ext:Hidden ID="tzid" runat="server"></ext:Hidden>
       <ext:Window ID="wind1" runat="server" Title="子贴信息" Layout="FormLayout" Modal="true" Icon="User" Hidden="true" Width="750" Height="300">
        <Items>
            <ext:GridPanel ID="gridZt" runat="server" StoreID="storGetZ" Height="250">
                <ColumnModel>
                    <Columns>
                        <ext:Column DataIndex="HuiYuanID" Header="发贴人"></ext:Column>
                        <ext:Column DataIndex="creats" Header="发贴内容" Width="500"></ext:Column>
                        <ext:Column DataIndex="CreateTime" Header="发贴时间"></ext:Column>
                    </Columns>
                </ColumnModel>
                <SelectionModel><ext:CheckboxSelectionModel></ext:CheckboxSelectionModel></SelectionModel>
                <TopBar>
                    <ext:Toolbar runat="server">
                        <Items>
                            <ext:Button ID="dels" runat="server" Text="删除所有贴子" Icon="Delete">
                                <DirectEvents>
                                    <Click OnEvent="DelZAll">
                                        <Confirmation Title="提示" Message="真的要删除所有贴子吗？" ConfirmRequest="true" />
                                        <EventMask Msg="数据正在删除请稍后……" ShowMask="true" />
                                    </Click>
                                </DirectEvents>
                            </ext:Button>
                            <ext:Button ID="deld" runat="server" Text=" 删除贴子" Icon="UserDelete">
                                <DirectEvents>
                                  <Click OnEvent="DeleZt">
                                    <Confirmation Title="提示" Message="真的要删除选中的贴子吗" ConfirmRequest="true" />
                                    <EventMask Msg="数据正在删除……" ShowMask="true" />
                                  </Click>
                                </DirectEvents>
                            </ext:Button>
                        </Items>
                    </ext:Toolbar>
                </TopBar>
            </ext:GridPanel>
        </Items>
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
