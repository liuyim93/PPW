<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectJiaoSe.aspx.cs" Inherits="WEB.SystemSeting.QuanXian.SelectJiaoSe" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ext:ResourceManager ID="ResourceManager1" runat="server">
        </ext:ResourceManager>
        <ext:FormPanel ID="FormPanel1" runat="server" ButtonAlign="Right" Border="false" Height="220"  Padding="5">
            <Items>
               <ext:DisplayField ID="disJSMC" runat="server" FieldLabel="角色名称"></ext:DisplayField>
               <ext:DisplayField ID="disZT" runat="server" FieldLabel="状态"></ext:DisplayField>
               <ext:DisplayField ID="disBZ" runat="server" FieldLabel="备注" AnchorHorizontal="80%" Height="100"></ext:DisplayField>
            </Items>
         </ext:FormPanel> 
    </div>
    </form>
</body>
</html>
