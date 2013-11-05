<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="WEB.test" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
         <table cellpadding="0" cellspacing="0" border="1" style="border-collapse:collapse" bordercolor="#cccccc">
            <tr>
                <td>订单号</td>
                <td>
                    <asp:TextBox ID="txtOrderNo" runat="server">0700004350100289</asp:TextBox></td>
            </tr>
            <tr>
                <td>订单数量</td>
                <td><asp:TextBox ID="txtOrderQty" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>金额</td>
                <td><asp:TextBox ID="txtAmount" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="2">
                <form name="frm1" action="http://payment.chinapay.com:8081/pay/TransGet" METHOD="POST" > 
                    <input type=hidden name="MerId" value="808080010500435" > 
                    <input type=hidden name="OrdId" value="0700004350100289" >
                    <input type=hidden name="TransAmt" value="000000106000" > 
                    <input type=hidden name="CuryId" value="156" >
                    <input type=hidden name="TransDate" value="20070703" > 
                    <input type=hidden name="TransType" value="0001" > 
                    <input type=hidden name="Version" value="20040916"> 
                    <input type=hidden name="BgRetUrl" value="accept.aspx"> 
                    <input type=hidden name="PageRetUrl" value="chinapayback.aspx"> 
                    <input type=hidden name="Priv1" value="Memo"> 
                    <input type=hidden name="ChkValue" value=<%= keyValue %> > 
                    
                    <input id="Button1" type="submit" class="button" value="进入银联支付平台" /></form>
                    </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
