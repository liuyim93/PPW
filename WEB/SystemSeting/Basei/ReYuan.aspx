<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReYuan.aspx.cs" Inherits="WEB.SystemSeting.Basei.ReYuan" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>人员管理</title>
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
    <form id="form1" runat="server">
    <div>
       <ext:Viewport ID="viprot" runat="server" Layout="BorderLayout" >
         <Items>
              <ext:Panel ID="Panel1" runat="server" Region="North" Icon="Zoom" Layout="HBoxLayout" LabelAlign="Right" Height="70" Title="查询条件" >
                <LayoutConfig>
                    <ext:HBoxLayoutConfig Align="Middle"/>
                </LayoutConfig>
                 <Items>
                     <ext:TextField ID="txtXM" FieldLabel="姓名" runat="server"></ext:TextField>
                     <ext:ComboBox ID="comXB" Width="200" runat="server" Editable="false" FieldLabel="性别">
                        <Items>
                            <ext:ListItem Text="男" Value="男" />
                            <ext:ListItem Text="女" Value="女" />
                        </Items>
                        <Triggers>
                                <ext:FieldTrigger Icon="Clear" />
                            </Triggers>
                            <Listeners>
                                <TriggerClick Handler="this.clearValue();" />
                            </Listeners>
                     </ext:ComboBox>
                     <ext:TextField ID="txtZW" FieldLabel="职位" runat="server"></ext:TextField>
                     <ext:Button ID="bntSele" runat="server" Text="查询" Icon="Zoom" Margins="0 0 0 20">
                        <Listeners><Click Handler="#{GridPanel1}.reload();" /></Listeners>
                     </ext:Button>
                 </Items>
              </ext:Panel>
             <ext:GridPanel ID="GridPanel1" runat="server"  StoreID="ReYuanList"  StripeRows="true" AutoDoLayout="true" Region="Center">
                <LoadMask Msg="数据正在加载……"  ShowMask="true"/>
                <ColumnModel>
                    <Columns>
                        <ext:RowNumbererColumn Header="序"></ext:RowNumbererColumn>
                        <ext:Column Header="姓名" Width="120" DataIndex="PersonName"></ext:Column>
                        <ext:Column Header="性别" DataIndex="Sex" Width="80"></ext:Column>
                        <ext:Column Header="职位" DataIndex="ZhiWei" Width="150"></ext:Column>
                        <ext:Column Header="身份证号" DataIndex="SFZ" Width="150"></ext:Column>
                        <ext:Column Header="电子邮箱" DataIndex="Email" Width="150"></ext:Column>
                        <ext:Column Header="联系电话" DataIndex="Mobile"></ext:Column>
                    </Columns>
                </ColumnModel>
                <SelectionModel><ext:CheckboxSelectionModel SingleSelect="true" ></ext:CheckboxSelectionModel></SelectionModel>
                <TopBar>
                    <ext:Toolbar runat="server">
                        <Items>
                            <ext:Button ID="bntAdd" Text="添加" Icon="Add" runat="server">
                                <Listeners>
                                    <Click Handler="#{window_addRenYuan}.setTitle('添加员信息');#{bntEidSeve}.hide();#{bntAddSeve}.show();#{FormPanel1}.getForm().reset();#{window_addRenYuan}.show();" />
                                </Listeners>
                            </ext:Button>
                            <ext:Button ID="bntEid" Text="修改" Icon="UserEdit" runat="server">
                                <DirectEvents>
                                    <Click OnEvent="Eid"><EventMask Msg="正在加载……" ShowMask="true" /></Click>
                                </DirectEvents>
                            </ext:Button>
                            <ext:Button ID="bntDel" Text="删除" Icon="UserDelete" runat="server">
                                <DirectEvents>
                                    <Click OnEvent="Dele">
                                      <Confirmation Title="提示" ConfirmRequest="true" Message="确定要删除吗？" />
                                       <EventMask Msg="正在删除……" ShowMask="true" />
                                    </Click>
                                </DirectEvents>
                            </ext:Button>
                            <ext:Button ID="bntSelec" Text="查看详情" Icon="UserBrown" runat="server">
                                <DirectEvents>
                                    <Click OnEvent="Selectd">
                                         <EventMask Msg="请稍后正在加载……" ShowMask="true" />
                                    </Click>
                                </DirectEvents>
                            </ext:Button>
                        </Items>
                    </ext:Toolbar>
                </TopBar>
                <BottomBar>
                    <ext:PagingToolbar PageSize="10" runat="server"></ext:PagingToolbar>
                </BottomBar>
             </ext:GridPanel>
         </Items>
       </ext:Viewport>
       <ext:Window ID="window_addRenYuan"  runat="server" Height="400" Icon="UserAdd"
            Width="850" AutoScroll="true" Modal="true" Layout="FormLayout" Hidden="true">
           <Items>
               <ext:FormPanel ID="FormPanel1" runat="server" ButtonAlign="Right" Layout="FormLayout" LabelAlign="Right" Padding="5" MonitorValid="true">
                     <Items>
                        <ext:Container ID="conTou" runat="server" Layout="ColumnLayout" Height="140">
                           <Items>
                             <ext:Container ID="contr1" runat="server" Layout="FormLayout" ColumnWidth=".5" >
                                <Items>
                                    <ext:TextField ID="txtName" FieldLabel="姓名" runat="server" IndicatorText="*" IndicatorCls="tipCls"  AllowBlank="false" MsgTarget="Side">
                                    </ext:TextField>
                                    <ext:TextField ID="txtZWM" runat="server" FieldLabel="职位"></ext:TextField>
                                    <ext:TextField ID="txtEml" runat="server" FieldLabel="电子邮箱"></ext:TextField>
                                    <ext:NumberField ID="txtQQ" runat="server" FieldLabel="QQ" MaxLength="20"></ext:NumberField>
                                </Items>
                            </ext:Container>
                             <ext:Container ID="Container1" runat="server" Layout="FormLayout" Enabled="false" ColumnWidth=".5" >
                                <Items>
                                    <ext:ComboBox ID="comSex" runat="server" FieldLabel="性别" Width="100" Editable="false" IndicatorText="*" IndicatorCls="tipCls"  AllowBlank="false" SelectedIndex="0" MsgTarget="Side">
                                        <Items>
                                            <ext:ListItem Text="男" Value="男"/>
                                            <ext:ListItem Text="女" Value="女" />
                                        </Items>
                                    </ext:ComboBox>
                                    <ext:TextField ID="txtSFZ" runat="server" FieldLabel="身份证号"></ext:TextField>
                                    <ext:NumberField ID="txtMobile" runat="server" FieldLabel="手机号" MaxLength="20"></ext:NumberField>
                                </Items>
                            </ext:Container>
                           </Items>
                        </ext:Container>
                        <ext:TextArea ID="txtBZ" runat="server" FieldLabel="备注" AnchorHorizontal="90%" Height="170"></ext:TextArea>
                     </Items>
                     <Buttons>
                        <ext:Button ID="bntAddSeve" runat="server" Text="添加" Icon="UserAdd" FormBind="true">
                            <DirectEvents>
                                <Click OnEvent="AddSeve">
                                    <EventMask Msg="正在保存数据……" ShowMask="true" />
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                        <ext:Button ID="bntEidSeve" runat="server" Text="修改" Icon="UserEdit" FormBind="true">
                            <DirectEvents><Click OnEvent="EidSeve">
                                <EventMask ShowMask="true" Msg="正在保存中……" />
                            </Click></DirectEvents>
                        </ext:Button>
                        <ext:Button ID="bntRefon" runat="server" Text="重置" Icon="Cancel">
                            <Listeners>
                                <Click Handler="#{FormPanel1}.getForm().reset();" />
                            </Listeners>
                        </ext:Button>
                        <ext:Button ID="Button3" runat="server" Text="关闭" Icon="Cross">
                            <Listeners>
                                <Click Handler="#{window_addRenYuan}.hide()"/>
                            </Listeners>
                       </ext:Button>
                     </Buttons>
               </ext:FormPanel>
           </Items>  
       </ext:Window>

        <ext:Window ID="Window_RenYuanSele" runat="server" Collapsible="true" Height="400" Icon="User"
            Title="人员信息" Width="750" Hidden="true" Modal="true">
               <AutoLoad Mode="IFrame"></AutoLoad>
                <Buttons>
            <ext:Button ID="Button1" Text="关闭" runat="server" Icon="Cross">
                <Listeners>
                    <Click Handler="#{Window_RenYuanSele}.hide();" />
                </Listeners>
            </ext:Button>
           </Buttons>
         </ext:Window>
    </div>
    </form>
</body>
</html>
