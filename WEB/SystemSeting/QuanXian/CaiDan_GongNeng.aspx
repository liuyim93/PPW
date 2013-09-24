<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CaiDan_GongNeng.aspx.cs" Inherits="WEB.SystemSeting.QuanXian.CaiDan_GongNeng" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        function getids(tree) {
            var nodes = tree.getChecked();
            var length = nodes.length;
            var i = 0;
            var obj = new Array(length);
            Ext.each(nodes, function (node) {
                var o = { cdid: node.attributes["cdid"], gnid: node.attributes["gnid"] };
                obj[i] = o;
                i++;
            });
            return JSON.stringify(obj);
        }
    </script>
</head>
<body>
    <ext:ResourceManager ID="ResourceManager1" runat="server" >
    </ext:ResourceManager>
    <form id="form1" runat="server">
    <div>
        <ext:Viewport runat="server" Layout="FitLayout">
            <Items>
                <ext:TreePanel ID="TreePanel1" runat="server" RootVisible="false" AutoScroll="true" UseArrows="true">
                   <Buttons>
                       <ext:Button ID="Button1" runat="server" Text="保存" Icon="Disk">
                            <DirectEvents>
                                <Click OnEvent="Save" Timeout="120000">
                                    <EventMask ShowMask="true" Msg="保存中..."/>
                                    <Confirmation ConfirmRequest="true" Title="提示" Message="确认修改？"/>
                                    <ExtraParams>
                                        <ext:Parameter Name="ids" Value="getids(#{TreePanel1})" Mode="Raw"></ext:Parameter>
                                    </ExtraParams>
                                </Click>
                            </DirectEvents>
                       </ext:Button>
                   </Buttons>
                </ext:TreePanel>
            </Items>
          
        </ext:Viewport>
    </div>
    </form>
</body>
</html>
