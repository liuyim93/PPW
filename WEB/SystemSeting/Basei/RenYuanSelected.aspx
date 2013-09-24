<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RenYuanSelected.aspx.cs" Inherits="WEB.SystemSeting.Basei.RenYuanSelected" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<ext:ResourceManager ID="ResourceManager1" runat="server">
</ext:ResourceManager>
<body>
    <form id="form1" runat="server">
    <div>
        <ext:Viewport ID="viprot" runat="server" Layout="FitLayout" >
            <Items>
                <ext:Panel ID="pan1" runat="server" Layout="FormLayout" LabelAlign="Right">
                    <Items>
                        <ext:Container ID="conTou" runat="server" Layout="ColumnLayout" Height="120">
                    <Items>
                         <ext:Container ID="contr1" runat="server" Layout="FormLayout" ColumnWidth=".5"  LabelAlign="Right">
                                <Items>
                                  <ext:DisplayField ID="txtName" FieldLabel="姓名" runat="server">
                                  </ext:DisplayField>
                                  <ext:DisplayField ID="txtZWM" runat="server" FieldLabel="职位"></ext:DisplayField>
                                  <ext:DisplayField ID="txtEml" runat="server" FieldLabel="电子邮箱"></ext:DisplayField>
                                  <ext:DisplayField ID="txtQQ" runat="server" FieldLabel="QQ"></ext:DisplayField>
                                </Items>
                            </ext:Container>
                             <ext:Container ID="Container1" runat="server" Layout="FormLayout" ColumnWidth=".5" LabelAlign="Right" >
                                <Items>
                                    <ext:DisplayField ID="comSex" runat="server" FieldLabel="性别">
                                    </ext:DisplayField>
                                    <ext:DisplayField ID="txtSFZ" runat="server" FieldLabel="身份证号"></ext:DisplayField>
                                    <ext:DisplayField ID="txtMobile" runat="server" FieldLabel="手机号"></ext:DisplayField>
                                </Items>
                            </ext:Container>
                    </Items>
                 </ext:Container>
                 <ext:DisplayField ID="txtBZ" runat="server" FieldLabel="备注" AnchorHorizontal="90%" Height="170" AutoScroll="true" LabelAlign="Right"></ext:DisplayField>
                    </Items>
                </ext:Panel>
                 
            </Items>
        </ext:Viewport>
    </div>
    </form>
</body>
</html>
