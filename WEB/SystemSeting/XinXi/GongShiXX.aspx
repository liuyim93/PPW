<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GongShiXX.aspx.cs" Inherits="WEB.SystemSeting.XinXi.GongShiXX" %>
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
      <ext:Viewport ID="vpMain" runat="server" Layout="FitLayout">
        <Items>
           <ext:FormPanel ID="form" runat="server" Layout="VBoxLayout" LabelAlign="Right" ButtonAlign="Center" MonitorValid="true">
            <Items>
                <ext:TextField ID="txtSgName" runat="server" FieldLabel="公司名称" Width="300" Margins="7 0 0 0" IndicatorText="*" IndicatorCls="tipCls"
                 AllowBlank="false" MsgTarget="Side">
                </ext:TextField>
                <ext:TextField ID="txtMode" runat="server" FieldLabel="电话" Width="300" Margins="7 0 0 0">
                 </ext:TextField>
                <ext:TextField ID="txtWz" runat="server" FieldLabel="公司网址" Width="300" Margins="7 0 0 0"></ext:TextField>
                <ext:TextArea ID="txtDz" runat="server" FieldLabel="地址" Width="400" Height="100" Margins="7 0 0 0"
                IndicatorText="*" IndicatorCls="tipCls" AllowBlank="false" MsgTarget="Side">
                </ext:TextArea>
                <ext:TextArea ID="bqrn" runat="server"  FieldLabel="版权所有" Width="400" Height="100" Margins="7 0 0 0"></ext:TextArea>
            </Items>
            <Buttons>
                <ext:Button runat="server" Text="确定" FormBind="true">
                    <DirectEvents>
                        <Click OnEvent="ok">
                            <EventMask Msg="正在执行操作……" ShowMask="true" />
                        </Click>
                    </DirectEvents>
                </ext:Button>
            </Buttons>
           </ext:FormPanel>
        </Items>
      </ext:Viewport>
    </div>
    </form>
</body>
</html>
