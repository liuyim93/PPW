<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShouQuan.aspx.cs" Inherits="WEB.SystemSeting.QuanXian.ShouQuan" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="../../Scripts/json2.js" type="text/javascript"></script>
    <title></title>
    <script type="text/javascript">
        function getids(tree) {
            var nodes = tree.getChecked()
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
    <ext:ResourceManager ID="ResourceManager1" runat="server">
    </ext:ResourceManager>
    <form id="form1" runat="server">
    <div>
        <ext:Hidden ID="hidlasttime" runat="server">
        </ext:Hidden>
        <ext:Viewport runat="server" Layout="FitLayout">
            <Items>
                <ext:TreePanel ID="treeqx" runat="server" AutoScroll="true" RootVisible="false" UseArrows="true">
                    <SelectionModel>
                        <ext:MultiSelectionModel runat="server"></ext:MultiSelectionModel>
                    </SelectionModel>
                    <Buttons>
                        <ext:Button ID="btnback" runat="server" Text="返回" Icon="ArrowLeft"> 
                            <DirectEvents>
                                <Click OnEvent="Back"></Click>
                            </DirectEvents>
                        </ext:Button>
                        <ext:Button ID="btnsave" runat="server" Text="保存" Icon="Disk">
                            <DirectEvents>
                                <Click OnEvent="Save" Timeout="1200000">
                                    <Confirmation ConfirmRequest="true" Title="提示" Message="是否确认修改？" />
                                    <EventMask ShowMask="true" Msg="保存中..."/>
                                    <ExtraParams>
                                        <ext:Parameter Name="ids" Value="getids(#{treeqx})" Mode="Raw"></ext:Parameter>
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
