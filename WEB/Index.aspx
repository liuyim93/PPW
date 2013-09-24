<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="WEB.Index"  ValidateRequest="false"  Debug="true" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>后台管理软件</title>
    <link href="Image/ft.ico" rel="Shortcut Icon" type="text/css" />
    <script type="text/javascript">
        function reftree(tree, id, name) {
            tree.setDisabled(true);
            Ext.net.DirectMethods.RefreshMenu(id, {
                success: function (result) {
                    tree.setTitle(name);
                    var nodes = eval(result);

                    if (nodes.length > 0) {
                        tree.initChildren(nodes);
                    }
                    else {
                        tree.getRootNode().removeChildren();
                    }
                    tree.setDisabled(false);
                }
            });
        }
        function loadpage(node, panel) {
            if (node.attributes["menuurl"] != "") {
                var url = node.attributes["menuurl"] + "?cdid=" + node.id;
                panel.load(url);
                panel.setTitle(setContent(node.attributes.text));
            } else {
                node.toggle();
            }
        }
        function setContent(str) {
            str = str.replace(/<\/?[^>]*>/g, ''); //去除HTML tag
            str.value = str.replace(/[ | ]*\n/g, '\n'); //去除行尾空白
            str = str.replace(/\n[\s| | ]*\r/g,'\n'); //去除多余空行
            return str;
        }
        function showtime() {
            var d = new Date();
            return d.toLocaleString();
        }

        function SetTitle(title,url) {
            Ext.getCmp("Panel9").setTitle(title)
            Ext.getCmp("Panel9").load(url);
        }
    </script>
    <style type="text/css">
        .tipCls
        {
            color:Red;
            font-size:15px;
        }
    </style>
</head>
<body onload="showtime();">
    <ext:ResourceManager ID="ResourceManager1" runat="server">
    </ext:ResourceManager>
    <form id="form1" runat="server">
    <div>
        <ext:Viewport ID="Viewport1" runat="server" Layout="BorderLayout">
            <Items>
                <ext:Panel ID="Panel1" runat="server" Collapsible="false" Height="87" Region="North"
                    Split="false">
                    <Items>
                        <ext:Panel ID="Panel2" runat="server" Height="60" BodyBorder="false">
                            <Content>
                                <img src="Image/topbj.jpg" height="60" width="100%"/>
                            </Content>
                        </ext:Panel>
                        <ext:Panel ID="pantopmenu" runat="server" Height="27" Layout="ColumnLayout">
                            <LayoutConfig>
                                <ext:ColumnLayoutConfig Margin="10"/>
                            </LayoutConfig>
                        </ext:Panel>
                    </Items>
                </ext:Panel>
                <ext:Panel ID="Panel5" runat="server" Collapsible="false" Height="15" Region="South"
                    Split="false" >
                    <Content>
                       <div style="text-align:center;font-size:12px; background-image:url(Image/WeiBu.jpg);">版权所有 行网科技有限公司</div>
                    </Content>
                </ext:Panel>

                <ext:TreePanel Title="系统菜单" ID="TreePanel1"  runat="server" Icon="CdrPlay" UseArrows="true" Padding="5"
                    AutoScroll="true" RootVisible="false" Collapsible="true" CollapseMode="Mini" Region="West" Split="true" Width="200">
                    <Listeners>
                        <Click Handler="e.stopEvent();loadpage(node,#{Panel9});"/>
                    </Listeners> 
                    <LoadMask ShowMask="true" Msg="加载中..."/>
                    <TopBar>
                        <ext:Toolbar runat="server">
                            <Items>
                                <ext:Button ID="Button1" runat="server" Text="展开所有" Icon="ArrowDown">
                                    <Listeners>
                                        <Click Handler="#{TreePanel1}.expandAll()"/>
                                    </Listeners>
                                </ext:Button>
                                <ext:Button ID="Button2" runat="server" Text="折叠所有" Icon="ArrowUp">
                                    <Listeners>
                                        <Click Handler="#{TreePanel1}.collapseAll()"/>
                                    </Listeners>
                                </ext:Button>
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                </ext:TreePanel>

                <ext:Panel ID="Panel9" runat="server" Layout="Fit" Region="Center" Title="操作向导" Icon="AsteriskOrange">
                    <AutoLoad Mode="IFrame" ShowMask="true" MaskMsg="正在加载..."></AutoLoad>
                    <BottomBar>
                        <ext:StatusBar runat="server" ID="CanterStatusBar" StatusAlign="Right">
                            <Items>
                                <ext:Button ID="btnuser" runat="server" Text="User" Icon="User">
                                   <DirectEvents>
                                    <Click OnEvent="EidYHXX"></Click>
                                   </DirectEvents>
                                </ext:Button>
                                <ext:ToolbarSeparator ID="ToolbarSeparator2" runat="server"></ext:ToolbarSeparator>
                                <ext:ToolbarSeparator ID="ToolbarSeparator1" runat="server"></ext:ToolbarSeparator>
                                <ext:Button ID="BtnExt" runat="server" Text="注销" Icon="UserCross">
                                    <DirectEvents>
                                        <Click OnEvent="Exit">
                                            <EventMask ShowMask="true" Msg="注销中..."/>
                                            <Confirmation ConfirmRequest="true" Title="提示" Message="确认注销？"/>
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                            </Items>
                        </ext:StatusBar>
                    </BottomBar>
                </ext:Panel>
            </Items>
        </ext:Viewport>
       <ext:Window ID="window_adduser" runat="server" Height="330" Icon="UserAdd" Title="修改用户"
            Width="400" AutoScroll="true" Modal="true" Hidden="true">
            <Items>
                <ext:FormPanel ID="FormPanel1" runat="server" ButtonAlign="Right" Height="295" Padding="5" LabelAlign="Right" MonitorValid="true">
                    <Items>
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
                     </Items>
                    <Buttons>
                        <ext:Button ID="btneditsave" runat="server" Text="修改" FormBind="true" Icon="Disk">
                            <DirectEvents>
                                <Click OnEvent="Edit">
                                    <EventMask ShowMask="true" Msg="保存中..."/>
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                        <ext:Button ID="Button10" runat="server" Text="关闭" Icon="Cross">
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
    </div>
    </form>
</body>
</html>

