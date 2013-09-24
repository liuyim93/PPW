<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JiaoShe.aspx.cs" Inherits="WEB.SystemSeting.QuanXian.JiaoShe" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
    .clc
    {
    	background:#456213;
    	}
    </style>
</head>
<body>
    <ext:ResourceManager ID="ResourceManager1" runat="server">
    </ext:ResourceManager>
    <ext:Store ID="Store1" runat="server" OnRefreshData="Ref">
      <Reader>
        <ext:JsonReader IDProperty="JueSeId">
           <Fields>
             <ext:RecordField Name="JueSeId"></ext:RecordField>
             <ext:RecordField Name="JSMC"></ext:RecordField>
             <ext:RecordField Name="BZ"></ext:RecordField>
             <ext:RecordField Name="status"></ext:RecordField>
           </Fields>
        </ext:JsonReader>
      </Reader>
    </ext:Store>
    <form id="form1" runat="server">
    <div>
    <ext:Hidden ID="hiddenjsid" runat="server">
     </ext:Hidden>
       <ext:Viewport ID="viprot" runat="server" Layout="BorderLayout" >
          <Items>
              <ext:Panel ID="Panel1" runat="server" Region="North" Icon="Zoom" Layout="HBoxLayout" LabelAlign="Right" Height="70" Title="查询条件" >
                  <LayoutConfig>
                    <ext:HBoxLayoutConfig Align="Middle" />
                  </LayoutConfig>
                  <Items>
                     <ext:TextField ID="txtJSMC" FieldLabel="角色名" runat="server"></ext:TextField>
                     <ext:ComboBox ID="cmbstarus" runat="server" FieldLabel="状态" Editable="false" Width="200" SelectedIndex="0">
                            <Items>
                                <ext:ListItem Text="所有" Value="" />
                                <ext:ListItem Text="正常" Value="1" />
                                <ext:ListItem Text="禁用" Value="5" />
                            </Items>
                        </ext:ComboBox>
                     <ext:Button ID="bntselect" runat="server" Text="查询" Icon="Zoom" Margins="0 0 0 20">
                        <Listeners>
                            <Click Handler="#{GridPanel1}.reload()"/>
                        </Listeners>
                        </ext:Button>
                  </Items>
              </ext:Panel>
              <ext:GridPanel ID="GridPanel1" runat="server" StoreID="Store1" StripeRows="true" Region="Center">
                 <ColumnModel>
                   <Columns>
                      <ext:RowNumbererColumn Header="序"></ext:RowNumbererColumn>
                      <ext:Column Header="角色名" DataIndex="JSMC" Width="250"></ext:Column>
                      <ext:Column Header="状态" DataIndex="status"></ext:Column>
                      <ext:Column Header="备注" DataIndex="BZ" Width="500"> </ext:Column>
                      <ext:CommandColumn Header="操作" ColumnID="sel" Width="160">
                            <Commands>
                                <ext:GridCommand Icon="UserEdit" Text="修改" CommandName="eid"> </ext:GridCommand>
                                <ext:GridCommand Icon="User" Text="查看" CommandName="select"></ext:GridCommand>
                                <ext:GridCommand Icon="UserKey" Text="授权" CommandName="sq" ></ext:GridCommand>
                            </Commands>
                        </ext:CommandColumn>
                   </Columns>
                 </ColumnModel>
                 <LoadMask ShowMask="true" Msg="数据正在加载"/>
                 <DirectEvents>
                    <Command OnEvent="cmds">
                        <ExtraParams>
                            <ext:Parameter Name="ComandName" Value="command" Mode="Raw"></ext:Parameter>
                            <ext:Parameter Name="id" Value="record.data.JueSeId" Mode="Raw"></ext:Parameter>
                        </ExtraParams>
                        <EventMask ShowMask="true" Msg="加载中..." />
                    </Command>
                 </DirectEvents>
                 <TopBar>
                    <ext:Toolbar runat="server">
                        <Items>
                            <ext:Button ID="btnAdd" Text="增加" runat="server"  Icon="UserAdd">
                                  <Listeners>
                                        <Click Handler="#{Window1}.setTitle('添加角色信息');#{addstatus}.hide();#{bnteid}.hide();#{Window1}.show();#{Button1}.show();#{FormPanel1}.getForm().reset();" />
                                    </Listeners>
                            </ext:Button>
                            <ext:Button ID="btnDele" Text="删除" runat="server" Icon="UserDelete">
                                <DirectEvents>
                                    <Click OnEvent="Delect">
                                        <EventMask ShowMask="true" Msg="数据正在删除..."/>
                                        <Confirmation ConfirmRequest="true" Message="确认删除选中的角色？" Title="提示"/>
                                    </Click>
                                </DirectEvents>
                            </ext:Button>
                        </Items>
                    </ext:Toolbar>
                 </TopBar>
                 <SelectionModel>
                  <ext:CheckboxSelectionModel></ext:CheckboxSelectionModel>
                 </SelectionModel>
                 <BottomBar>
                    <ext:PagingToolbar PageSize="10" runat="server"></ext:PagingToolbar>
                 </BottomBar>
              </ext:GridPanel>
          </Items>
       </ext:Viewport>
        <ext:Window ID="Window1" runat="server" Collapsible="true" Height="250" Width="450" Icon="User" Modal="true" Hidden="true">
            <Items>
                <ext:FormPanel ID="FormPanel1" runat="server" ButtonAlign="Right" Height="220" Width="435" Padding="5" MonitorValid="true">
                    <Items>
                        <ext:TextField ID="JSMC" runat="server" Width="100" FieldLabel="角色名称" IndicatorText="*" IndicatorCls="tipCls"  AllowBlank="false" MsgTarget="Side">
                        </ext:TextField>
                        <ext:ComboBox ID="addstatus" runat="server" FieldLabel="状态" SelectedIndex="0" IndicatorText="*" IndicatorCls="tipCls"  AllowBlank="false" MsgTarget="Side">
                            <Items>
                                <ext:ListItem Text="正常" Value="1" />
                                <ext:ListItem Text="禁用" Value="5" />
                            </Items>
                        </ext:ComboBox>
                        <ext:TextArea ID="BZ" FieldLabel="备注" runat="server" Width="300" Height="100"></ext:TextArea>
                    </Items>
                    <Buttons>
                        <ext:Button ID="Button1" runat="server" Icon="Disk" Text="保存" FormBind="true">
                            <DirectEvents>
                                <Click OnEvent="Add">
                                    <EventMask Msg="保存中........" ShowMask="true" />
                                </Click>
                            </DirectEvents>
                        </ext:Button >   
                        <ext:Button  ID="bnteid" runat="server" Icon="UserEdit" Text="修改" FormBind="true">
                            <DirectEvents>
                                <Click OnEvent="Eid">
                                    <EventMask Msg="修改中......" ShowMask="true" />
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                        <ext:Button ID="bntCZ" runat="server" Text="重置" Icon="Cancel">
                           <Listeners>
                                <Click Handler="#{FormPanel1}.getForm().reset();"/>
                            </Listeners>
                        </ext:Button>
                        <ext:Button ID="bntClos" Text="关闭" runat="server" Icon="Cross">
                            <Listeners>
                                <Click Handler="#{Window1}.hide();" />
                            </Listeners>
                        </ext:Button>
                    </Buttons>
                </ext:FormPanel>
            </Items>
        </ext:Window>
        <ext:Window ID="Window2" runat="server" Collapsible="true" Height="300"  Icon="User"
            Title="角色信息" Width="350" Hidden="true" Modal="true">
           <AutoLoad Mode="IFrame"></AutoLoad>
           <Buttons>
            <ext:Button Text="关闭" Icon="Cross" runat="server">
                <Listeners>
                    <Click Handler="#{Window2}.hide();" />
                </Listeners>
            </ext:Button>
           </Buttons>
        </ext:Window>
    </div>
    </form>
</body>
</html>
