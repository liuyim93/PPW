<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebFormAuction.aspx.cs" Inherits="WEB.SystemSeting.WebFormAuction" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <ext:ResourceManager ID="ResourceManager1" runat="server"></ext:ResourceManager>
    <ext:Store ID="storeAuction" runat="server">
        <Reader>
            <ext:JsonReader IDProperty="AuctionID">
                <Fields>
                    <ext:RecordField Name="AuctionID"></ext:RecordField>
                    <ext:RecordField Name="ProductName"></ext:RecordField>
                    <ext:RecordField Name="ProductPrice"></ext:RecordField>
                    <ext:RecordField Name="Coding"></ext:RecordField>
                    <ext:RecordField Name="HuiYuan"></ext:RecordField>
                    <ext:RecordField Name="AuctionPrice"></ext:RecordField>
                    <ext:RecordField Name="AuctionTime"></ext:RecordField>
                    <ext:RecordField Name="TimePoint"></ext:RecordField>
                    <ext:RecordField Name="CreateTime"></ext:RecordField>
                    <ext:RecordField Name="Status"></ext:RecordField>
                    <ext:RecordField Name="PriceAdd"></ext:RecordField>
                    <ext:RecordField Name="AuctionPoint"></ext:RecordField>
                    <ext:RecordField Name="FreePoint"></ext:RecordField>
                    <ext:RecordField Name="EndTime"></ext:RecordField>
                    <ext:RecordField Name="AuctionType"></ext:RecordField>
                    <ext:RecordField Name="IsRecommend"></ext:RecordField>
                </Fields>
            </ext:JsonReader>
        </Reader>
    </ext:Store>
    <div>
        
    </div>
    </form>
</body>
</html>
