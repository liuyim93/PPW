<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WEB.Login" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>拍拍网后台管理软件</title>
    <link href="Image/ft.ico" rel="Shortcut Icon" type="text/css" />
    <script type="text/javascript">
        var lod = function () {
            var img = document.getElementById("img1");
            img.height = document.documentElement.clientHeight;
            img.width = document.documentElement.clientWidth;

        }
        function a() {
            window.location.href = window.location.href;
        };
    </script>
    <style type="text/css">
        .con
        {
            position:absolute;
	        top:37%;
	        left:40%;
	        width:500px;margin-left: auto;margin-right: auto; 
        	}
        .boc
        {
        	background-image:url(Image/login.jpg); 
        	background-repeat:no-repeat;
        	background-position:center; 
        	height:500px; width:100%; font-size:12px;
        	position:relative;
        	}
    </style>
</head>
<body onload="lod()" style="overflow:hidden;">
    <ext:ResourceManager ID="ResourceManager1" runat="server">
    </ext:ResourceManager>
    <form id="form1" runat="server">
    <div id="box" style="width:100%;">&nbsp;</div>
    <div  id="img1" class="boc">
         <div id="cr" class="con">
              <div id="yhma">
                    <ext:TextField ID="txtyhm" Margins="0 0 0 80"  runat="server" LabelAlign="Right" FieldLabel="用户名"  EmptyText="请输入用户名"></ext:TextField>
                   <ext:TextField ID="txtpwd" Margins="0 0 0 80" runat="server"  LabelAlign="Right" FieldLabel=" 密  码 "  InputType="Password"></ext:TextField>
              </div>
              <div id="yz" style="margin:5px 0 0 0;">
                    <ext:Panel ID="Panel1" runat="server" Layout="HBoxLayout" FormGroup="true">
                         <LayoutConfig>
                        <ext:HBoxLayoutConfig Align="Middle" />
                       </LayoutConfig>
                       <Items>
                         <ext:TextField ID="txtyzm" runat="server" LabelAlign="Right" FieldLabel="验证码"  Width="150"   EnableKeyEvents="true">
                            <Listeners>
                                <KeyPress Handler="if(e.getKey()==13){document.getElementById('btnlogin').click();}" />
                            </Listeners>
                        </ext:TextField>
                       <ext:DisplayField ID="lblyzm" runat="server" Margins="0 0 0 10">
                        </ext:DisplayField>
                       </Items>
                    </ext:Panel>
                   
               </div>
               <div id="buttons" style=" margin:0 0 0 60px">
                        <div style=" float:left; margin:0 20px 0 0px;">
                             <ext:Button ID="btnlogin" runat="server" Text="登录" Icon="UserKey">
                                <DirectEvents>
                                    <Click OnEvent="btnlogin_click">
                                        <EventMask ShowMask="true" Msg="Login..." />
                                    </Click>
                                </DirectEvents>
                             </ext:Button>
                        </div>
                    <div>
                        <ext:Button ID="btnexit" runat="server" Text="退出" Icon="DoorOut">
                            <Listeners>
                                <Click Handler="window.open('', '_parent', '');window.close();" />
                            </Listeners>
                        </ext:Button>
                    </div>
              </div>
       </div>
    </div>
       
    </form>

    <!--[if IE]> 
        <script type="text/javascript">
            var box = document.getElementById("box");
            box.attachEvent("onresize", a);
        </script>
    <![endif]-->
    <!--[if !IE]>-->
        <script type="text/javascript">
            window.onresize = function () {
                window.location.href = window.location.href;
            };
        </script>
    <!--<![endif]-->
    
</body>
</html>
