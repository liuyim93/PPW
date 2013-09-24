<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelUser.aspx.cs" Inherits="WEB.SystemSeting.QuanXian.SelUser" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <ext:ResourceManager ID="ResourceManager1" runat="server">
    </ext:ResourceManager>
    <form id="form1" runat="server">
    <div>
        <ext:FormPanel ID="FormPanel1" runat="server" Border="false"  Padding="5" FormGroup="true" LabelAlign="Right">
            <Items>
                <ext:DisplayField ID="disname" runat="server" FieldLabel="人员姓名">
                </ext:DisplayField>
                <ext:DisplayField ID="disyhm" runat="server" FieldLabel="用户名">
                </ext:DisplayField>
                <ext:DisplayField ID="dismm" runat="server" FieldLabel="密码">
                </ext:DisplayField>
                <ext:DisplayField ID="disjs" runat="server" FieldLabel="角色">
                </ext:DisplayField>
                <ext:DisplayField ID="diszt" runat="server" FieldLabel="状态">
                </ext:DisplayField>
                <ext:DisplayField ID="disbz" runat="server" Height="70" Width="250" FieldLabel="备注" ReadOnly="true">
                </ext:DisplayField>
            </Items>
        </ext:FormPanel>
    </div>
    </form>
</body>
</html>
