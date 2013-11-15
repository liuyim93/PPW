<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebFormproduct.aspx.cs" Inherits="WEB.SystemSeting.WebFormproductBrand" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../kindeditor/themes/default/default.css" rel="stylesheet" type="text/css" />
    <link href="../../kindeditor/plugins/code/prettify.css" rel="stylesheet" type="text/css" />
    <script  src="../../kindeditor/kindeditor.js" type="text/javascript"></script>
    <script  src="../../kindeditor/lang/zh_CN.js" type="text/javascript"></script>
    <script  src="../../kindeditor/plugins/code/prettify.js" type="text/javascript"></script>
    <script src="../kindeditor/kindeditor-all.js" type="text/javascript"></script>
    <script src="../My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script type="text/javascript">
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

  <script type="text/javascript" language="javascript"> 
      <!--
        //屏蔽js错误 
        function ResumeError() {
            return true;
        }
        window.onerror = ResumeError; 
       // --> 
       
</script> 
   <style type="text/css">
        .tp
        {
            width:100;
            height:50;
        }
    </style>
</head>

<ext:ResourceManager ID="ResourceManager1" runat="server">
</ext:ResourceManager>

<body>
<ext:Store ID="StorlList" runat="server" OnRefreshData="Ref">
    <Reader>
        <ext:JsonReader IDProperty="ProductID">
            <Fields>
                <ext:RecordField Name="ProductID"></ext:RecordField>
                <ext:RecordField Name="ProductTypeID"></ext:RecordField>
                <ext:RecordField Name="productName"></ext:RecordField>
                <ext:RecordField Name="productBrand"></ext:RecordField>
                <ext:RecordField Name="productPrice"></ext:RecordField>
                <ext:RecordField Name="Fee"></ext:RecordField>
                <ext:RecordField Name="ShipFee"></ext:RecordField>
                <ext:RecordField Name="Intro"></ext:RecordField>
                <ext:RecordField Name="IsExchange"></ext:RecordField>
                <ext:RecordField Name="Points"></ext:RecordField>
                <ext:RecordField Name="CreateTime"></ext:RecordField>
            </Fields>
        </ext:JsonReader>
    </Reader>
</ext:Store>

<ext:Store ID="StorImg" runat="server">
    <Reader>
        <ext:JsonReader>
            <Fields>
                <ext:RecordField Name="xh"></ext:RecordField>
                <ext:RecordField Name="img"></ext:RecordField>
            </Fields>
        </ext:JsonReader>
    </Reader>
</ext:Store>

    <form id="form1" runat="server">
    <div>
       <ext:Hidden ID="hidSelectedTreeNode2" runat="server">
        </ext:Hidden>
        <ext:Viewport ID="vpMain" runat="server" Layout="BorderLayout">
            <Items>
                <ext:Panel ID="Panel1" runat="server" Region="North" Icon="Zoom" Layout="HBoxLayout" LabelAlign="Right" Height="70" Title="查询条件" LabelWidth="90" >
                      <LayoutConfig><ext:HBoxLayoutConfig Align="Middle" /></LayoutConfig>
                    <Items>
                        <ext:TextField ID="txtSeleName" runat="server" FieldLabel="产品名"></ext:TextField>
                        <ext:TextField ID="txtPingPai" runat="server" FieldLabel="产品品牌"></ext:TextField>
                         <ext:DropDownField ID="ddfSSXK" runat="server" Width="240" TriggerIcon="SimpleArrowDown"
                                 Editable="false" Mode="ValueText" FieldLabel="产品类型">
                                    <Component>
                                        <ext:TreePanel ID="TreePanel2" runat="server" Height="210" Shadow="None" UseArrows="true"
                                            AutoScroll="true" Animate="true" ContainerScroll="true" RootVisible="true">
                                            <Root>
                                                <ext:TreeNode Text="所有产品类型" NodeID="0" Expanded="true">
                                                </ext:TreeNode>
                                            </Root>
                                            <DirectEvents>
                                                <Click OnEvent="TreeNodeClick2" After="#{ddfSSXK}.collapse();">
                                                    <ExtraParams>
                                                        <ext:Parameter Name="nodeId" Value="node.id" Mode="Raw">
                                                        </ext:Parameter>
                                                        <ext:Parameter Name="nodeText" Value="node.text" Mode="Raw">
                                                        </ext:Parameter>
                                                    </ExtraParams>
                                                </Click>
                                            </DirectEvents>
                                        </ext:TreePanel>
                                    </Component>
                                    <Triggers>
                                        <ext:FieldTrigger Icon="Clear" />
                                     </Triggers>
                                    <Listeners>
                                        <Expand Handler="#{TreePanel2}.expandAll();" />
                                        <TriggerClick Handler="this.clearValue();#{hidSelectedTreeNode2}.setValue('');" />
                                    </Listeners>
                        </ext:DropDownField>
                        <ext:Button ID="bntSele" Text="查询" runat="server" Icon="Zoom" Margins="0 0 0 20">
                            <Listeners>
                                <Click Handler="#{GridPanel1}.reload();" />
                            </Listeners>
                        </ext:Button>
                    </Items>
                </ext:Panel> 
                <ext:GridPanel ID="GridPanel1" runat="server"  StoreID="StorlList"  StripeRows="true" AutoDoLayout="true" Region="Center">
                    <ColumnModel>
                        <Columns>
                            <ext:RowNumbererColumn Header="序"></ext:RowNumbererColumn>                            
                            <ext:Column DataIndex="productName" Header="产品名" Width="150"></ext:Column>
                            <ext:Column DataIndex="Intro" Header="产品简介"></ext:Column>
                            <ext:Column DataIndex="ProductTypeID" Header="产品类型" Width="150"></ext:Column>
                            <ext:Column DataIndex="productBrand" Header="品牌" Width="120"></ext:Column>
                            <ext:Column DataIndex="productPrice" Header="市场价格"></ext:Column>                     
                            <ext:Column DataIndex="productPrice" Header="价格"></ext:Column>                                                   
                            <ext:Column DataIndex="Fee" Header="手续费"></ext:Column>
                            <ext:Column DataIndex="ShipFee" Header="运费"></ext:Column>
                            <ext:Column DataIndex="IsExchange" Header="积分兑换"></ext:Column>
                            <ext:Column DataIndex="Points" Header="所需积分"></ext:Column>
                            <ext:Column DataIndex="CreateTime" Header="添加时间"></ext:Column>
                        </Columns>
                    </ColumnModel>
                    <SelectionModel><ext:CheckboxSelectionModel SingleSelect="true"></ext:CheckboxSelectionModel></SelectionModel>
                    <LoadMask Msg="数据正在加载……" ShowMask="true" />
                    <TopBar>
                        <ext:Toolbar runat="server">
                            <Items>
                                <ext:Button ID="bntAdd" runat="server" Text="添加" Icon="UserAdd" OnClick="AddShow" AutoPostBack="true">
                                    <%--<Listeners>
                                        <Click Handler="#{FormPanel1}.getForm().reset();#{window_addGongYingShang}.setTitle('添加产品信息');#{bntAdds}.show();#{bntEids}.hide();#{window_addGongYingShang}.show();" />
                                    </Listeners>--%>
                                    
                                </ext:Button>
                                <ext:Button ID="bntEid" runat="server" Text="修改" Icon="UserEdit" OnClick="EidShow" AutoPostBack="true">
                                </ext:Button>
                                <ext:Button ID="bntDel" runat="server" Text="删除" Icon="Delete">
                                    <DirectEvents>
                                        <Click OnEvent="Dele">
                                            <Confirmation Title="提示" Message="确定要删除吗？" ConfirmRequest="true" />
                                            <EventMask Msg="正在执行删除操作……" ShowMask="true" />
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                                <ext:Button ID="bntSelse" runat="server" Text="查看详情" Icon="UserBrown">
                                    <DirectEvents>
                                        <Click OnEvent="Selectd"></Click>
                                    </DirectEvents>
                                </ext:Button>
                                <ext:Button ID="bntSC" runat="server" Text="上传图片" Icon="Accept">
                                   <Listeners><Click Handler="#{window_Img}.show();" /></Listeners>
                                </ext:Button>
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                    <BottomBar><ext:PagingToolbar PageSize="10" runat="server"></ext:PagingToolbar></BottomBar>
                </ext:GridPanel>
            </Items>
        </ext:Viewport>
        <ext:Hidden ID="hidSelectedTreeNode1" runat="server"></ext:Hidden>
        <ext:Hidden ID="hidBh" runat="server"></ext:Hidden>
        <ext:Hidden ID="cpid" runat="server"></ext:Hidden>
        <ext:Window ID="window_addGongYingShang"  runat="server"  Height="380" Icon="User"
            Width="850" AutoScroll="true" Modal="true" Layout="FormLayout" Hidden="true" Resizable="true">
            <Items>
                 <ext:FormPanel ID="FormPanel1" runat="server" ButtonAlign="Right" Layout="FormLayout" LabelAlign="Right" MonitorValid="true" Padding="0">
                     <Items>
                        <ext:TabPanel ID="TabPan1" runat="server" Height="300"  AnchorHorizontal="100%" Border="false">
                           <Items>
                               <ext:Panel ID="pan1" runat="server" Layout="FormLayout" Title="基础数据" AnchorHorizontal="100%" >
                                   <Items>
                                     <ext:Container ID="conTou" runat="server" Layout="ColumnLayout" Height="180">
                                        <Items>
                                            <ext:Container ID="cont1" runat="server" Layout="FormLayout" ColumnWidth=".5">
                                                <Items>
<%--                                                    <ext:TextField ID="txtBh" runat="server" FieldLabel="产品编号" IndicatorText="*" IndicatorCls="tipCls"
                                                      AllowBlank="false" MsgTarget="Side" IsRemoteValidation="true" >
                                                        <RemoteValidation OnValidation="YzBH">
                                                        </RemoteValidation>
                                                      </ext:TextField> --%> 
                                                    <ext:TextField ID="pinPai" runat="server" FieldLabel="品牌"></ext:TextField>
<%--                                                      <ext:NumberField ID="PaiMaiJG" runat="server" FieldLabel="拍买价格" Text="0" IndicatorText="*" IndicatorCls="tipCls"  AllowBlank="false" MsgTarget="Side" IsRemoteValidation="true">
                                                        <RemoteValidation OnValidation="YzJG"></RemoteValidation>
                                                     </ext:NumberField>--%>
                                                     <%--<ext:NumberField ID="txtPriceAdd" runat="server" FieldLabel="价格涨幅" AllowBlank="false" IndicatorText="*" MsgTarget="Side"></ext:NumberField>--%>
                                                     <ext:NumberField ID="txtFee" runat="server" FieldLabel="手续费"></ext:NumberField>
                                                     <ext:NumberField ID="txtShipFee" runat="server" FieldLabel="运费"></ext:NumberField>
<%--                                                     <ext:TextField ID="SetTime" runat="server" FieldLabel="设置拍买时间"></ext:TextField>--%>
                                                       <ext:Checkbox ID="chkGift" runat="server" FieldLabel="积分兑换"></ext:Checkbox>
                                                       <ext:NumberField ID="txtPoints" runat="server" FieldLabel="所需积分"></ext:NumberField>
                                                </Items>
                                            </ext:Container>
                                             <ext:Container ID="Container1" runat="server" Layout="FormLayout" ColumnWidth=".5">
                                                <Items>
                                                     <ext:TextField ID="txtName" runat="server" FieldLabel="产品名" IndicatorText="*" IndicatorCls="tipCls"  AllowBlank="false" MsgTarget="Side"></ext:TextField>
                                                     <ext:TextField ID="txtIntro" runat="server" FieldLabel="产品简介"></ext:TextField>
                                                        <ext:DropDownField ID="ddsCPLX" runat="server" Width="200" TriggerIcon="SimpleArrowDown"
                                                                   Mode="ValueText" FieldLabel="产品类型"  IndicatorText="*" IndicatorCls="tipCls"  AllowBlank="false" MsgTarget="Side"
                                                                    Enabled="false">
                                                        <Component>
                                                            <ext:TreePanel ID="TreePanel1" runat="server" Height="210" Shadow="None" UseArrows="true"
                                                                AutoScroll="true" Animate="true" ContainerScroll="true" RootVisible="true">
                                                                <Root>
                                                                    <ext:TreeNode Text="所有产品类型" NodeID="0" Expanded="true">
                                                                    </ext:TreeNode>
                                                                </Root>
                                                                <DirectEvents>
                                                                    <Click OnEvent="TreeNodeClick1" After="#{ddsCPLX}.collapse();">
                                                                        <ExtraParams>
                                                                            <ext:Parameter Name="nodeId" Value="node.id" Mode="Raw">
                                                                            </ext:Parameter>
                                                                            <ext:Parameter Name="nodeText" Value="node.text" Mode="Raw">
                                                                            </ext:Parameter>
                                                                        </ExtraParams>
                                                                    </Click>
                                                                </DirectEvents>
                                                            </ext:TreePanel>
                                                        </Component>
                                                        <Listeners>
                                                            <Expand Handler="#{TreePanel1}.expandAll();" />
                                                            <TriggerClick Handler="this.clearValue();#{hidSelectedTreeNode1}.setValue('');" />
                                                        </Listeners>
                                                     </ext:DropDownField>
                                                     <ext:NumberField ID="txtJG" runat="server" FieldLabel="市场价格" Text="0" IndicatorText="*" IndicatorCls="tipCls"  AllowBlank="false" MsgTarget="Side" IsRemoteValidation="true">
                                                        <RemoteValidation OnValidation="YzJG"></RemoteValidation>
                                                     </ext:NumberField>
                                                     <%--<ext:NumberField ID="txtAcutionPoint" runat="server" FieldLabel="需要拍点" AllowBlank="false" IndicatorText="*" MsgTarget="Side"></ext:NumberField>--%>
                                                     <ext:ComboBox ID="comx" runat="server" FieldLabel="显示首页" IndicatorText="*" IndicatorCls="tipCls"  AllowBlank="false" MsgTarget="Side">
                                                        <Items>
                                                            <ext:ListItem Text="不显示" Value="0" />
                                                            <ext:ListItem  Text="显示" Value="1"/>
                                                        </Items>
                                                     </ext:ComboBox>
                                                </Items>
                                             </ext:Container>
                                            </Items>
                                        </ext:Container>
                                   </Items>
<%--                                   <Content>
                                    &nbsp; &nbsp; &nbsp;拍买时间：<input type="text" name="cfg" value="" id="d241" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})" runat="server" class="Wdate" style="width:150px"/>
                                   </Content>--%>
                               </ext:Panel>
                               <ext:Panel ID="pan2" runat="server" Layout="FormLayout" Title="产品详情" AnchorHorizontal="100%" >
                                    <Items>
                                            <ext:Container ID="Container4" runat="server" Height="300" Layout="FormLayout" AnchorHorizontal="90%">
                                            <Content>
                                                <table style="width: 760px; margin-left: -4px; table-layout: fixed;">
                                                    <tr>
                                                        <td valign="top" style="width: 101px; font-size: 13px; text-align: right;">
                                                           产品详情:
                                                        </td>
                                                        <td style="padding-left: 4px; text-align: left; width: 675px;">
                                                            <textarea id="txt_TiMuNeiRong" name="txt_TiMuNeiRong" style="width: 668px; height: 280px;
                                                                margin-bottom: 10px;"></textarea>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </Content>
                                     </ext:Container>
                                        <ext:Hidden ID="hidTiMuNeiRong" runat="server">
                                        </ext:Hidden>
                                    </Items>
                               </ext:Panel>
                           </Items>
                        </ext:TabPanel>
                    </Items>
                    <Buttons>
                        <ext:Button ID="bntAdds" runat="server" Text="添加" Icon="UserAdd" FormBind="true">
                            <DirectEvents>
                               <Click OnEvent="Add" >
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
                                <Click OnEvent="EidSeve">
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
                                <Click Handler="#{window_addGongYingShang}.hide();" />
                            </Listeners>
                        </ext:Button>
                    </Buttons>
                 </ext:FormPanel>
            </Items>
            <Listeners>
                   <Show Handler="editor1.html(#{hidTiMuNeiRong}.getValue());editor1.fullscreen(false);#{txtJG}.remoteValidate();#{txtBh}.remoteValidate();#{ddsCPLX}.validate();"/>
            </Listeners>
         </ext:Window>

         <%--查看产品详情--%>
          <ext:Window ID="window_sele"  runat="server"  Height="380" Icon="User" Title="产品详情"
            Width="850" AutoScroll="true" Modal="true" Layout="FormLayout" Padding="0" Hidden="true" Resizable="true">
                <AutoLoad Mode="IFrame"></AutoLoad>
                <Buttons>
                    <ext:Button ID="bntClo" runat="server" Text="关闭" Icon="Cross">
                        <Listeners>
                            <Click Handler="#{window_sele}.hide();" />
                        </Listeners>
                    </ext:Button>
                </Buttons>
           </ext:Window>
           <%--上传图片--%>
           <ext:Window ID="window_Img"  runat="server"  Height="160" Width="500" Hidden="true" Icon="User" Title="上传图片">
                <Items>
                    <ext:Panel ID="Panel2" runat="server" Region="North" Icon="Zoom" Layout="FormLayout" LabelAlign="Right" Padding="5" Height="100" Width="880"  LabelWidth="90" >
                        <Items>
                        <ext:FileUploadField ID="fileLud" runat="server" Width="300" FieldLabel="上传图片" ButtonText="浏览"></ext:FileUploadField>
                        <ext:TextField ID="textXh" runat="server" FieldLabel="序号"></ext:TextField>
                        <ext:Button ID="bntOK" runat="server" Text="确定上传" Icon="AsteriskOrange">
                            <DirectEvents>
                                <Click OnEvent="ImegSever"></Click>
                            </DirectEvents>
                        </ext:Button>
                        </Items>
                    </ext:Panel>
                </Items>
                <Buttons>
                    <ext:Button Text="关闭" runat="server" Icon="Cross">
                        <Listeners>
                            <Click Handler="#{window_Img}.hide();" />
                        </Listeners>
                    </ext:Button>
                </Buttons>
           </ext:Window>
           </div>
    </form>
</body>
</html>
