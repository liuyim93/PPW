<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebFormProductType.aspx.cs" Inherits="WEB.SystemSeting.WebFormProductType" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
            function RefTree(tree, hid) {
                Ext.net.DirectMethods.CilenRefTree({
                    success: function (result) {
                        var nodes = eval(result);
                        if (nodes.length > 0) {
                            tree.initChildren(nodes);
                            //                        tree.expandAll();
                            var currentNode = tree.getNodeById(hid.getValue());
                            currentNode.ensureVisible();
                            currentNode.select();
                            currentNode.expand(false);
                        }
                        else {
                            tree.getRootNode().removeChildren();
                        }
                    }
                });

            }
    </script>
    <style type="text/css">
       .clsss
       {
       	background-image:url(../Image/daohang/kuChun.gif) !important;
       	}
    </style>
</head>
<ext:ResourceManager ID="ResourceManager1" runat="server">
</ext:ResourceManager>
<body>
  <ext:Store ID="StoreDic" runat="server" OnRefreshData="BindDics" >
        <Reader>
            <ext:JsonReader IDProperty="Id">
                <Fields>
                    <ext:RecordField Name="Id">
                    </ext:RecordField>
                    <ext:RecordField Name="MC">
                    </ext:RecordField>
                    <ext:RecordField Name="FID">
                    </ext:RecordField>
                    <ext:RecordField Name="BZ">
                    </ext:RecordField>
                    <ext:RecordField Name="status">
                    </ext:RecordField>
                </Fields>
            </ext:JsonReader>
        </Reader>
    </ext:Store>
    <form id="form1" runat="server">
    <div>
     <ext:Hidden ID="hidSelectedTreeNode" runat="server">
      </ext:Hidden>
       <ext:Viewport ID="vpMain" runat="server" Layout="BorderLayout">
        <Items>
             <ext:TreePanel ID="TreePanel1" runat="server" Width="260" Title="产品类型" Region="West"
                    UseArrows="true" AutoScroll="true" Icon="AsteriskRed">
                    <TopBar>
                        <ext:Toolbar ID="Toolbar1" runat="server">
                            <Items>
                                <ext:Button ID="Button1" runat="server" Text="展开所有" Icon="ArrowDown">
                                    <Listeners>
                                        <Click Handler="#{TreePanel1}.expandAll()" />
                                    </Listeners>
                                </ext:Button>
                                <ext:Button ID="Button2" runat="server" Text="折叠所有" Icon="ArrowUp">
                                    <Listeners>
                                        <Click Handler="#{TreePanel1}.collapseAll()" />
                                    </Listeners>
                                </ext:Button>
                                <ext:Button ID="Button4" runat="server" Text="刷新" Icon="ArrowRefresh">
                                    <Listeners>
                                        <Click Handler="RefTree(#{TreePanel1},#{hidSelectedTreeNode});" />
                                    </Listeners>
                                </ext:Button>
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                    <DirectEvents>
                        <Click OnEvent="TreeNodeClick">
                            <EventMask ShowMask="true" Msg="加载中..." />
                            <ExtraParams>
                                <ext:Parameter Name="nodeId" Value="node.id" Mode="Raw">
                                </ext:Parameter>
                            </ExtraParams>
                        </Click>
                    </DirectEvents>
                </ext:TreePanel>
               <ext:Panel ID="Panel1" runat="server" Region="Center" Layout="BorderLayout" Border="false">
                <Items>
                     <ext:Panel ID="Panel2" runat="server" Height="80" Title="查询条件" Icon="Zoom" Layout="HBoxLayout"
                            AutoScroll="true" Padding="10" LabelWidth="60" LabelAlign="Right" Region="North">
                            <LayoutConfig><ext:HBoxLayoutConfig Align="Middle"/></LayoutConfig>
                            <Items>
                                <ext:TextField id="txtName" runat="server" FieldLabel="产品类型"></ext:TextField>
                                <ext:Button ID="bntsele" Text="查询" runat="server" Margins="0 0 0 20" Icon="Zoom">
                                    <Listeners>
                                        <Click Handler="#{GridPanel1}.reload();" />
                                    </Listeners>
                                </ext:Button>
                            </Items>
                     </ext:Panel>
                      <ext:GridPanel ID="GridPanel1" runat="server" StoreID="StoreDic" AutoDoLayout="true"
                            StripeRows="true" Region="Center">
                            <ColumnModel>
                                <Columns>
                                    <ext:RowNumbererColumn Header="序号" Width="50">
                                    </ext:RowNumbererColumn>
                                    <ext:Column DataIndex="MC" Width="180" Header="名称">
                                    </ext:Column>
                                    <ext:Column DataIndex="BZ" Width="400" Header="备注">
                                    </ext:Column>
                                    <ext:CommandColumn Header="操作" ColumnID="cmd">
                                        <Commands>
                                            <ext:GridCommand Icon="BookEdit" Text="修改" CommandName="edit">
                                            </ext:GridCommand>
                                        </Commands>
                                    </ext:CommandColumn>
                                </Columns>
                            </ColumnModel>
                              <DirectEvents>
                                <Command OnEvent="Cmd">
                                    <ExtraParams>
                                        <ext:Parameter Name="type" Value="command" Mode="Raw">
                                        </ext:Parameter>
                                        <ext:Parameter Name="id" Value="record.data.Id" Mode="Raw">
                                        </ext:Parameter>
                                    </ExtraParams>
                                </Command>
                            </DirectEvents>
                              <TopBar>
                                <ext:Toolbar ID="Toolbar2" runat="server">
                                    <Items>
                                        <ext:Button ID="btnAdd" runat="server" Text="添加"  Icon="BookAdd">
                                            <Listeners>
                                                <Click Handler="#{btnEditSave}.hide();#{btnAddSave}.show();Ext.net.DirectMethods.BindFname();#{hidOldDM}.value='';#{FormPanel1}.getForm().reset();#{Window_Add_Edit}.setTitle('添加产品类型');#{Window_Add_Edit}.show();" />
                                            </Listeners>
                                        </ext:Button>
                                        <ext:Button ID="btnDel" runat="server" Text="删除" Icon="BookDelete">
                                            <DirectEvents>
                                                <Click OnEvent="DataDelete" After="RefTree(#{TreePanel1},#{hidSelectedTreeNode});">
                                                    <EventMask ShowMask="true" Msg="删除中..." />
                                                    <Confirmation ConfirmRequest="true" Message="确认删除？" Title="提示" />
                                                </Click>
                                            </DirectEvents>
                                        </ext:Button>
                                    </Items>
                                </ext:Toolbar>
                            </TopBar>
                            <LoadMask Msg="数据正在加载……" ShowMask="true" />
                            <BottomBar>
                                <ext:PagingToolbar PageSize="10" runat="server"></ext:PagingToolbar>
                            </BottomBar>
                            <SelectionModel><ext:CheckboxSelectionModel SingleSelect="true"></ext:CheckboxSelectionModel></SelectionModel>
                     </ext:GridPanel>
                </Items>
               </ext:Panel>
        </Items>
       </ext:Viewport>
           <ext:Window ID="Window_Add_Edit" runat="server" AutoScroll="true" Modal="true" Hidden="true"
            Height="230" Icon="BookAdd" Title="" Width="350" Resizable="false">
            <Items>
                <ext:FormPanel ID="FormPanel1" runat="server" ButtonAlign="Right" Height="195" Padding="5"
                    MonitorValid="true">
                    <Items>
                        <ext:Hidden ID="hidId" runat="server">
                        </ext:Hidden>
                         <ext:Hidden ID="hidOldDM" runat="server">
                        </ext:Hidden>
                        <ext:DisplayField ID="disFname" Text="无" FieldLabel="父级名称" runat="server" IsFormField="false">
                        </ext:DisplayField>
                        <ext:TextField ID="txt_MC" runat="server" IndicatorText="*" IndicatorCls="tipCls"
                            AllowBlank="false" FieldLabel="类型名称" MsgTarget="Side" MaxLength="25">
                        </ext:TextField>
                        <ext:TextArea ID="txt_BZ" runat="server" FieldLabel="备注" Width="200">
                        </ext:TextArea>
                    </Items>
                    <Buttons>
                        <ext:Button ID="btnAddSave" runat="server" Icon="Disk" Text="添加" FormBind="true">
                            <DirectEvents>
                                <Click OnEvent="Add" After="RefTree(#{TreePanel1},#{hidSelectedTreeNode});">
                                    <EventMask ShowMask="true" Msg="保存中..." />
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                        <ext:Button ID="btnEditSave" runat="server" Text="修改" FormBind="true" Icon="Disk">
                            <DirectEvents>
                                <Click OnEvent="Edit" After="RefTree(#{TreePanel1},#{hidSelectedTreeNode});/*#{TreePanel1}.expandAll();#{TreePanel1}.getNodeById(#{hidSelectedTreeNode}.getValue()).getUI().toggleCheck(true);*/">
                                    <EventMask ShowMask="true" Msg="保存中..." />
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                        <ext:Button ID="btnClose" runat="server" Text="关闭" Icon="Cross">
                            <Listeners>
                                <Click Handler="#{Window_Add_Edit}.hide()" />
                            </Listeners>
                        </ext:Button>
                    </Buttons>
                </ext:FormPanel>
            </Items>
        </ext:Window>
    </div>
    </form>
</body>
</html>
