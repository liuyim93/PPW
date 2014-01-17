

window.isfoucs = 0;

/*+++++++++++++++++++++++++++++++++++++
+ 功能:通用字符串两端去空
+ 研发:宗帝
+ 时间:2010-11-27
+++++++++++++++++++++++++++++++++++++*/
String.prototype.trim = function () { return $.trim(this); }

/*+++++++++++++++++++++++++++++++++++++
+ 功能:通用弹出对话框提交
+ 研发:宗帝
+ 时间:2010-11-19
+++++++++++++++++++++++++++++++++++++*/

function AddDialogbox(objId, title, zIndex, objWidth, ScreenMaskId, zIndex_Sub, IsShowClose, strContent, DialogTitle) {
    if (ScreenMaskId == null) ScreenMaskId = ""; //2010-09-27 Add
//    addScreenMask(ScreenMaskId, zIndex_Sub);

    var objIdInHTML = objId.replace(/#/, "");
    var sTop = (document.compatMode == "CSS1Compat") ? document.documentElement.scrollTop : document.body.scrollTop;

    //why document.body.scrollTop == 0 on chrome?
//    if (sys.chrome) { sTop = document.body.scrollTop; }

    var docBodyScrollTop = sTop + $(window).height() / 7;
    var left = $(window).width() / 2 - objWidth / 2;

    if (!DialogTitle) DialogTitle = "提示";

    //处理子框生成
    if (zIndex_Sub != null) {
        docBodyScrollTop = docBodyScrollTop + 100;
    }
    //IsShowClose==true?

    var strCloseDiv = "<img style=\"width:150px;cursor:pointer\" src=\"/images/big_tuan.png\" alt=\"关闭\">";
    strCloseDiv = '<div style="line-height:25px;text-align:right;height:100%;"><div style="width:80%;float:left;text-align:left;font-weight:bold;">&nbsp;' + DialogTitle + ':</div><a href="javascript:;" style="margin:5px 5px 0 0;display:block;width:16px;float:right"><img alt="关闭" src="/images/Dlg_close.gif" /></a></div>';


    $(document.body).prepend(
		'<div id=' + objIdInHTML + ' style="position:absolute;top:' + docBodyScrollTop + 'px; left:' + left + 'px; z-index:' + (zIndex) + '; width:' + objWidth + 'px;padding:0px;background-color:#f1f1f1;border:1px solid #cdcdcd">' +
		(IsShowClose == true ? '<div style="width:' + (objWidth) + 'px;height:25px;display:block;background-color:#BBBBBB;margin:0px;border-bottom:1px solid #AAAAAA" id="' + objIdInHTML + '_close">' + strCloseDiv + '</div>' : "") +
		'<div id="' + objIdInHTML + '_Content" style="margin:0px;overflow-x:hidden;background:#f1f1f1; width:' + (objWidth) + 'px;"><div>' +

		'</div>'
	);
    $("#" + objIdInHTML + "_Content").html(strContent);
    $("#" + objId).fadeIn("fast");
    $("#" + objId + " img[src*='Dlg_close.gif']").bind("click", function () {
        RemoveDialogbox(objId, ScreenMaskId);
    });
}


function RemoveDialogbox(objId, ScreenMaskId) {
    if (ScreenMaskId == null || ScreenMaskId == "") ScreenMaskId = ""; //2010-09-27 Add
    var objIdInHTML = objId.replace(/#/, "");
    $("#" + objIdInHTML).fadeOut("fast", function () {
//        removeScreenMask(ScreenMaskId);
        $(this).remove(); //从DOM中删除本对象
    });
}


/*+++++++++++++++++++++++++++++++++++++
+ 功能:通用 Ajax 提交
+ 研发:宗帝
+ 时间:2010-11-19
+++++++++++++++++++++++++++++++++++++*/
function AjaxSubmit(_Type, _Url, _data, _success, _error, _dataType) {
    if (!_dataType || _dataType == "") _dataType = "html";
    //_dataType=(!_dataType)?"html":_dataType;
    $.ajax({
        type: (_Type ? _Type.toUpperCase() : 'POST'),
        url: (_Url ? _Url : ''),
        "dataType": (_dataType), //dataType: "script"
        data: (_data ? _data : ''),
        cache: false,
        success: (_success ? _success : null),
        error: (_error ? _error : null)
    });
}

/*
function ShowErrorMsg(msg)
{
alert(msg);
}
*/
/*+++++++++++++++++++++++++++++++++++++
+ 功能:快速登陆
+ 研发:宗帝
+ 时间:2010-11-19
+++++++++++++++++++++++++++++++++++++*/

function Fast_Login(uNameId, uPassId, datatype) {

    if (!datatype) datatype = 0;
    var uName = (datatype == 0) ? $("#" + uNameId).val() : uNameId;
    var uPass = (datatype == 0) ? $("#" + uPassId).val() : uPassId;
    //alert(uName + "|" + uPass);
    if ($.trim(uName).length < 1 || $.trim(uPass).length < 1 || uName == "用户名/邮箱" || uPass == "请输入密码") {
        //alert("请输入用户邮箱(或用户名)和密码。");
//        AlertInfo("登录异常", "请输入用户邮箱(或用户名)和密码。");
        alert("请输入用户名或密码.");
        return false;
    }
    AjaxSubmit(
		"get",
		"../Auction/ajax/FastUserLogin.aspx",
		{ "username": uName, "password": uPass },
		function (msg) {
		    if (msg == 1) {
		        AjaxLoginBoxClose();
		        //		        Fast_GetUserInfo();
		        window.location.href = window.location.href;
		    }
		    else {
		        //		        AlertError("登录失败", "用户名或密码错误.");
		        alert("用户名或密码错误。");
		        //ShowErrorMsg("用户登陆有误,可能的原因:\r\n\r\n\t1.用户名或密码错误;\r\n\t2.网络故障.", "会员登陆"); //。

		    }
		},
		function (msg) {
		    ShowErrorMsg("用户登陆有误:" + (msg.responseText ? msg.responseText : "ErrorAjax") + "。");
		}
	);

//    setCookie('islogin', '1');
    return true;
}
function f5() {

    self.parent.location.reload(true);
    //history.go(0);
}
/*+++++++++++++++++++++++++++++++++++++
+ 功能:读取登陆后的信息
+ 研发:宗帝
+ 时间:2010-11-19
+++++++++++++++++++++++++++++++++++++*/

function Fast_GetUserInfo() {
    AjaxSubmit(
		null,
		"/index.aspx?s=/User/FastUserInfo/",
		"",
		function (msg) {
		    if (msg) $("#div_UserInfo").html(msg);
		},
		function (msg) { alert(msg) }
	);
}

//更新用户状态
function Fast_UpdateUserState() {
    if (window.isfoucs == 0) {//如果当前页面不是在激活的状态
        return;
    }


    AjaxSubmit(
		null,
		"/index.aspx?s=/User/FastUserInfo/",
		"",
		function (msg) {
		    if (msg) $("#div_UserInfo").html(msg);
		    else setTimeout("Fast_UpdateUserState()", 60000); //60S

		},
		function (msg) {
		    alert(msg); setTimeout(function () { Fast_UpdateUserState() }, 60000); //60S
		}
	);

}


/*+++++++++++++++++++++++++++++++++++++
+ 功能:快速注销
+ 研发:宗帝
+ 时间:2010-11-19
+++++++++++++++++++++++++++++++++++++*/
function Fast_Logout(isAutoExit) {

    if (!isAutoExit && confirm("您是否确定退出？") == false)
        return;

    AjaxSubmit(
		null,
		"/index.aspx?s=/User/FastUserLogout/",
		"",
		function (msg) {
		    if (msg == 1) { Fast_GetUserInfo(); }
		    else {
		        ShowErrorMsg("用户安全退出有误（错误代码：" + msg + ")。");
		    }
		},
		function (msg) { ShowErrorMsg("用户安全退出有误（异常值：" + (msg.responseBody != null ? msg.responseBody : "") + ")。"); }
	);
}

/*+++++++++++++++++++++++++++++++++++++
+ 功能:弹出登陆框
+ 研发:宗帝
+ 时间:2010-11-19 
+++++++++++++++++++++++++++++++++++++*/

function AjaxLoginBox() {
    AjaxSubmit(null, "/Auction/ajax/LoginBox.aspx", ""
	, function (msg) {
	    AddDialogbox("Ajax_User_Login_Box", "", 60001, 489, "Ajax_User_Login_screenMask", 10000, false, msg);
	}, null);
}
function AjaxLoginBoxClose() {
    RemoveDialogbox('Ajax_User_Login_Box', 'Ajax_User_Login_screenMask');
}

/*+++++++++++++++++++++++++++++++++++++
+ 功能:修改密码(旧密码,新密码)
+ 研发:宗帝
+ 时间:2010-11-19
+ 入参:旧密码,新密码
+++++++++++++++++++++++++++++++++++++*/

function Fast_UpdateUserPass(uNameId, uPassId, datatype) {
    if (!datatype) datatype = 0;
    var uName = (datatype == 0) ? $("#" + uNameId).val() : uNameId;
    var uPass = (datatype == 0) ? $("#" + uPassId).val() : uPassId;
    AjaxSubmit(
		null,
		"/index.aspx?s=/UserInfo/UserPassUpdate/",
		{ "oldUserPassWord": uName, "UserPassword": uPass },
		function (msg) {
		    if (msg > 0) alert("密码已修改.");
		    else alert("密码修改失败,可能的原因:\r\n 1.旧密码不正确;\r\n 2.登陆超时;\r\n 3.系统故障.");

		},
		function (msg) { alert(msg) }
	);
}

/*+++++++++++++++++++++++++++++++++++++
+ 命名空间:PaiPaiCMS
+ 功能:默认弹出框配置
+ 研发:宗帝 prototype
+ 时间:2010-11-19
+++++++++++++++++++++++++++++++++++++*/
var PaiPaiCMS = {
    PaiPaiCmsDialogBoxId: "PaiPaiCMS_Dialog_Box",
    PaiPaiCmsDialogBox_ScreenMaskId: "PaiPaiCMS_Dialog_Box_screenMask",
    DialogBox: {
        Show: function (data) {
            if (!data) data = ""; AddDialogbox(PaiPaiCMS.PaiPaiCmsDialogBoxId, "", 60001, 300, PaiPaiCMS.PaiPaiCmsDialogBox_ScreenMaskId, 10000, true, data);
        }
		, ShowSimple: function (data, FontBold, FontColor, FontSize, TextAlign, DialogBoxWidth, Caption) {
		    if (!DialogBoxWidth || isNaN(parseInt(DialogBoxWidth, 10))) DialogBoxWidth = 300;
		    if (!data) data = "";

		    if (!Caption) Caption = "出价提示";

		    if (!FontColor || FontColor.trim().length < 1) FontColor = "#888888"; //默认
		    if (!FontBold || FontBold.toLowerCase() != "bold") FontBold = "normal"; else FontBold = "bold";
		    if (!FontSize || isNaN(parseInt(FontSize, 10))) FontSize = 50;
		    if (!TextAlign || TextAlign.trim().length < 1) TextAlign = "center"; //默认

		    var content = '<div style="text-align:' + TextAlign + ';font-size:' + FontSize + 'px;font-weight:' + FontBold + ';color:' + FontColor + ';margin:20px 20px 20px 20px;line-height:30px;border:3px double #cccccc;">'
				+ '<div style="margin:10px;">'
				+ data
				+ '</div>'
				+ '</div>';
		    AddDialogbox(PaiPaiCMS.PaiPaiCmsDialogBoxId, "", 60001, DialogBoxWidth, PaiPaiCMS.PaiPaiCmsDialogBox_ScreenMaskId, 10000, true, content, Caption);
		}
        //16:28 2010-12-09
		, ShowSimpleTip: function (data, FontBold, FontColor, FontSize, TextAlign, DialogBoxWidth, oDiv) {
		    if (!DialogBoxWidth || isNaN(parseInt(DialogBoxWidth, 10))) DialogBoxWidth = 300;
		    if (!data) data = "";
		    if (!FontColor || FontColor.trim().length < 1) FontColor = "#888888"; //默认
		    if (!FontBold || FontBold.toLowerCase() != "bold") FontBold = "normal"; else FontBold = "bold";
		    if (!FontSize || isNaN(parseInt(FontSize, 10))) FontSize = 12;
		    if (!TextAlign || TextAlign.trim().length < 1) TextAlign = "left"; //默认

		    var content = '<div style="background-color:#EEEEEE;top:10px;left:-132px;height:auto;padding:2px 0px 2px 0px; position:absolute; z-index:999; width:170px;">'
				+ '<div style="text-align:' + TextAlign + ';font-size:' + FontSize + 'px;font-weight:' + FontBold + ';color:' + FontColor + ';margin:3px 3px 3px 3px;line-height:20px;border:3px double #cccccc;">'
				+ '<div style="margin:10px;">'
				+ data
				+ '</div>'
				+ '</div>'
				+ '</div>';
		    if (oDiv) { oDiv.css("width", DialogBoxWidth).css("margin", "-22px 0px 0px -20px").css("display", "block").css("position", "absolute").css("z-Index", "90000").show().html(content); }


		}
		, AjaxAlert: function (url, data, width, IsShowCaption) {
		    if (!width) width = 300; IsShowCaption = (IsShowCaption == true);
		    if (url) {
		        if (!data) data = "";
		        AjaxSubmit(null, url, data
					, function (msg) { AddDialogbox(PaiPaiCMS.PaiPaiCmsDialogBoxId, "", 60001, width, PaiPaiCMS.PaiPaiCmsDialogBox_ScreenMaskId, 10000, IsShowCaption, msg); }
					, function (msg) { ShowErrorMsg("AJAX获取数据有误.异常值：" + (msg.responseBody != null ? msg.responseBody : "") + ")。"); }
				);
		    }
		    else { alert(url); }
		}
		, Close: function () { RemoveDialogbox(PaiPaiCMS.PaiPaiCmsDialogBoxId, PaiPaiCMS.PaiPaiCmsDialogBox_ScreenMaskId); }
		, CloseSimpleTip: function (oDiv) { if (oDiv) oDiv.hide(); }
    }, ErrorBox: {
        ShowErrorMsg: function (msg, strTitle) {
            var mymsg = '<div style="width:586px;height:330px;margin:0px;border:1px solid #AAAAAA;background-color:menu"><textarea style="width:558px;height:300px;margin:12px 3px 3px 3px;border:0px solid #EEEEEE;background-color:menu;">'
				+ msg + '</textarea></div>';
            AddDialogbox("Ajax_Err", "", 61000, 588, "Ajax_Err_screenMask", 10000, true, mymsg, strTitle);
        }
    }, PicBox: {
        UserLogo: {
            Show: function () {
                //alert("PaiPaiCMS.PicBox.UserLogo.Show();");
                //加载头像处理页面内容:参数表:头像类(0),前台类型(1),系统提供头像列表(0)
                var data = { "PhotoType": 0, "PhotoUserType": 1, "SystemType": 0 };
                var url = "/UpPic/UserLogo.aspx";
                var IsShowCaption = true;
                var width = 572;
                AjaxSubmit(null, url, data
					, function (msg) { AddDialogbox(PaiPaiCMS.PaiPaiCmsDialogBoxId, "", 60001, width, PaiPaiCMS.PaiPaiCmsDialogBox_ScreenMaskId, 10000, IsShowCaption, msg, "会员头像"); }
					, function (msg) { ShowErrorMsg("AJAX获取数据有误.异常值：" + (msg.responseBody != null ? msg.responseBody : "") + ")。"); }
				);
            }
        }
    }
};


/*+++++++++++++++++++++++++++++++++++++
+ 功能:弹出异常代码
+ 研发:宗帝
+ 时间:2010-11-19
+++++++++++++++++++++++++++++++++++++*/
function ShowErrorMsg(msg) {
    var mymsg = '<div style="width:586px;height:330px;margin:0px;border:1px solid #AAAAAA;background-color:menu"><textarea style="width:558px;height:300px;margin:12px 3px 3px 3px;border:0px solid #EEEEEE;background-color:menu;">'
		+ msg
		+ '</textarea></div>';
    AddDialogbox("Ajax_Err", "", 61000, 588, "Ajax_Err_screenMask", 10000, true, mymsg, "异常信息");
}
function ShowErrorMsg(msg, strTitle) {
    var mymsg = '<div style="width:586px;height:330px;margin:0px;border:1px solid #AAAAAA;background-color:menu"><textarea style="width:558px;height:300px;margin:12px 3px 3px 3px;border:0px solid #EEEEEE;background-color:menu;">'
		+ msg
		+ '</textarea></div>';
    AddDialogbox("Ajax_Err", "", 61000, 588, "Ajax_Err_screenMask", 10000, true, mymsg, strTitle);
}

//显示打卡信息
function setCardInfo(flag) {
    if (flag == true) {
        $("#advCardInfo").show();
    } else {
        $("#advCardInfo").hide();
    }
}

//
function daka() {
    $("#liOP").html("<img src=\"/images/51/load.gif\" />&nbsp;处理中..");
    setTimeout(cardUp, 1000);
}

//打卡
function cardUp() {
    $.ajax
    ({
        type: "post",
        url: "/user/ajax_card.aspx",
        data: {},
        success: function (msg) {
            if (msg == "0") {
                $("#ulCard").html("<li style='color:red'>× 请重新登录</li>");
            } else if (msg == "2") {
                $("#ulCard").html("<li style='color:red'>× 您今天已经打卡</li>");
            } else if (msg == "3") {
                $("#ulCard").html("<li style='color:red'>× 您的赠点余额大于500,不能打卡</li>");
                //                alert("您账户的赠点不少于500点,不能打卡");
            }
            else if (msg == "1") {
                //更新数据
                AjaxSubmit(
		                null,
		                "/index.aspx?s=/User/FastUserInfo/",
		                "",
		                function (msg) {
		                    if (msg) {
		                        $("#div_UserInfo").html(msg);
		                        $("#advCardInfo").show();
		                    }

		                },
		                function (msg) { alert(msg) }
	                );
            }
        }
    });
}

//获取不重复字符串
function getStr() {
    var d = new Date();
    return d.getMonth() + "" + d.getDate() + "" + d.getHours() + "" + d.getMinutes() + "" + d.getSeconds() + "" + d.getMilliseconds();
}




/*+++++++++++++++++++++++++++++++++++++
+ 功能:新的弹出窗口基础函数
+ 研发:wangl
+ 时间:2013-7-30 16:47:34
+++++++++++++++++++++++++++++++++++++*/
function AlertBox(objId, title, zIndex, objWidth, ScreenMaskId, zIndex_Sub, IsShowClose, strContent, DialogTitle, DialogBankgroundColor) {
    if (ScreenMaskId == null) ScreenMaskId = "";
//    addScreenMask(ScreenMaskId, zIndex_Sub);

    var objIdInHTML = objId.replace(/#/, "");
    var sTop = (document.compatMode == "CSS1Compat") ? document.documentElement.scrollTop : document.body.scrollTop;

    //why document.body.scrollTop == 0 on chrome?
//    if (sys.chrome) { sTop = document.body.scrollTop; }

    var docBodyScrollTop = sTop + $(window).height() / 7;
    var left = $(window).width() / 2 - objWidth / 2;

    if (!DialogTitle) DialogTitle = "提示";

    //处理子框生成
    if (zIndex_Sub != null) {
        docBodyScrollTop = docBodyScrollTop + 100;
    }
    var strCloseDiv = '<div style="line-height:25px;text-align:right;height:100%;"><div style="width:80%;float:left;text-align:left;font-weight:bold;color:#FFFFFF;">&nbsp;' + DialogTitle + '</div><a style="margin:5px 5px 0 0;display:block;width:16px;float:right;cursor:pointer;"><img alt="关闭" src="Images/close.png" onclick=\"RemoveAlertBox('+objId+');\" /></a></div>';


    $(document.body).prepend(
		'<div id=' + objIdInHTML + ' style="position:absolute;top:' + docBodyScrollTop + 'px; left:' + left + 'px; z-index:' + (zIndex) + '; width:' + objWidth + 'px;padding:0px;background-color:#f1f1f1;border:1px solid #cdcdcd;border-radius:5px;-webkit-border-radius:5px;-moz-border-radius:5px;">' +
		(IsShowClose == true ? '<div style="width:' + (objWidth) + 'px;height:25px;display:block;background-color:' + DialogBankgroundColor + ';margin:0px;border-bottom:1px solid #AAAAAA" id="' + objIdInHTML + '_close">' + strCloseDiv + '</div>' : "") +
		'<div id="' + objIdInHTML + '_Content" style="margin:0px;overflow-x:hidden;background:#f1f1f1; width:' + (objWidth) + 'px;"><div>' +

		'</div>'
	);
    $("#" + objIdInHTML + "_Content").html(strContent);
    $("#" + objId).fadeIn("fast");
    $("#" + objId + " img[src*='alert/close.png']").bind("click", function () {
        RemoveAlertBox(objId, ScreenMaskId);
    });
}


function RemoveAlertBox(objId, ScreenMaskId) {
    if (ScreenMaskId == null || ScreenMaskId == "") ScreenMaskId = ""; //2010-09-27 Add
    var objIdInHTML = objId.replace(/#/, "");
    $("#" + objIdInHTML).fadeOut("fast", function () {
//        removeScreenMask(ScreenMaskId);
        $(this).remove(); //从DOM中删除本对象
    });
}
/*+++++++++++++++++++++++++++++++++++++
+ 功能:弹出成功窗口
+ 研发:wangl
+ 时间:2013-7-30 16:47:34
+++++++++++++++++++++++++++++++++++++*/
function AlertSuccess(title, content) {
    var mymsg = '<div style="width:400px;clear:both;overflow:hidden; position:relative;border:1px solid #a5bbc1; color:black;border-radius:0 5px;-webkit-border-radius:0 5px;-moz-border-radius:0 5px;">' +
    	        '<div style=" margin:8px; float:left"><img src="../images/alert/success.png"/></div>' +
                '<div style=" float:left; width:300px;margin:8px;padding-top:2px; text-align:left;">' +
        	    '<h2 style="font-family:' + '微软雅黑' + '; font-size:18px;"><strong>' + title + '</strong></h2>' +
                '<p style=" font-size:12px; color:#333; line-height:21px;margin-top:8px;">' + content + '</p></div>' +
    //'<div style="position:absolute; right:8px; top:0px; cursor:pointer;"><a href="javascript:;" style="margin:5px 5px 0 0;display:block;width:16px;float:right"><img alt="关闭" src="/images/alert/close.jpg" /></a></div>' +
                '</div>';
    AlertBox("Alert_Success", "", 61000, 400, "Alert_Success_screenMask", 10000, true, mymsg, "", "green");
}
/*+++++++++++++++++++++++++++++++++++++
+ 功能:弹出失败窗口
+ 研发:wangl
+ 时间:2013-7-30 16:47:34
+++++++++++++++++++++++++++++++++++++*/
function AlertError(title, content) {
    var mymsg = '<div style="width:400px;clear:both;overflow:hidden; position:relative;border:1px solid #a5bbc1; color:black;border-radius:0 5px;-webkit-border-radius:0 5px;-moz-border-radius:0 5px;">' +
    	        '<div style=" margin:8px; float:left"><img src="../images/alert/error.png"/></div>' +
                '<div style=" float:left; width:300px;margin:8px;padding-top:2px; text-align:left;">' +
        	    '<h2 style="font-family:' + '微软雅黑' + '; font-size:18px;"><strong>' + title + '</strong></h2>' +
                '<p style=" font-size:12px; color:#333; line-height:21px;margin-top:8px;">' + content + '</p></div>' +
                '</div>';
    AlertBox("Alert_Error", "", 61000, 400, "Alert_Error_screenMask", 10000, true, mymsg, "", "#d81213");
}

/*+++++++++++++++++++++++++++++++++++++
+ 功能:弹出警告窗口
+ 研发:wangl
+ 时间:2013-7-30 16:47:34
+++++++++++++++++++++++++++++++++++++*/
function AlertWarn(title, content) {
    var mymsg = '<div style="width:400px;clear:both;overflow:hidden; position:relative;border:1px solid #a5bbc1; color:black;border-radius:0 5px;-webkit-border-radius:0 5px;-moz-border-radius:0 5px;">' +
    	        '<div style=" margin:8px; float:left"><img src="../images/alert/warn.png"/></div>' +
                '<div style=" float:left; width:300px;margin:8px;padding-top:2px; text-align:left;">' +
        	    '<h2 style="font-family:' + '微软雅黑' + '; font-size:18px;"><strong>' + title + '</strong></h2>' +
                '<p style=" font-size:12px; color:#333; line-height:21px;margin-top:8px;">' + content + '</p></div>' +
                '</div>';
    AlertBox("Alert_Warn", "", 61000, 400, "Alert_Warn_screenMask", 10000, true, mymsg, "", "#ff9201");
}
/*+++++++++++++++++++++++++++++++++++++
+ 功能:弹出信息窗口
+ 研发:wangl
+ 时间:2013-7-30 16:47:34
+++++++++++++++++++++++++++++++++++++*/
function AlertInfo(title, content) {
    var mymsg = '<div style="width:400px;clear:both;overflow:hidden; position:relative;border:1px solid #a5bbc1; color:black;border-radius:0 5px;-webkit-border-radius:0 5px;-moz-border-radius:0 5px;">' +
    	        '<div style=" margin:8px; float:left"><img src="../images/alert/info.png"/></div>' +
                '<div style=" float:left; width:300px;margin:8px;padding-top:2px; text-align:left;">' +
        	    '<h2 style="font-family:' + '微软雅黑' + '; font-size:18px;"><strong>' + title + '</strong></h2>' +
                '<p style=" font-size:12px; color:#333; line-height:21px;margin-top:8px;">' + content + '</p></div>' +
                '</div>';
    AlertBox("Alert_Info", "", 61000, 400, "Alert_Info_screenMask", 10000, true, mymsg, "", "#58a0f4");
}

/*+++++++++++++++++++++++++++++++++++++
+ 功能:弹出确认窗口
+ 研发:wangl
+ 时间:2013-7-30 16:47:34
+++++++++++++++++++++++++++++++++++++*/
function AlertConfirm(title, content, img1Url, funcName1, img2Url, funcName2) {
    var mymsg = '<div style="width:400px;clear:both;overflow:hidden; position:relative;border:1px solid #a5bbc1; color:black;border-radius:0 5px;-webkit-border-radius:0 5px;-moz-border-radius:0 5px;">' +
    	        '<div style=" margin:8px; float:left"><img src="../images/alert/info.png"/></div>' +
                '<div style=" float:left; width:300px;margin:8px;padding-top:2px; text-align:left;">' +
        	    '<h2 style="font-family:' + '微软雅黑' + '; font-size:18px;"><strong>' + title + '</strong></h2>' +
                '<p style=" font-size:12px; color:#333; line-height:21px;margin-top:8px;">' + content + '</p>' +
                '<img src="' + img1Url + '" width="122" height="30"  style=" margin-right:10px; cursor:pointer"  onclick="' + funcName1 + '"  />' +
                '<img src="' + img2Url + '" width="122" height="30"  style=" margin-right:10px; cursor:pointer"  onclick="' + funcName2 + '"  />' +
                '</div>';
    AlertBox("Alert_Confirm", "", 61000, 400, "Alert_Confirm_screenMask", 10000, true, mymsg, "", "#58a0f4");
}

/*+++++++++++++++++++++++++++++++++++++
+ 功能:弹出充值窗口 →弹出确认窗口
+ 研发:wangl
+ 时间:2013-8-1 17:23:34
+++++++++++++++++++++++++++++++++++++*/
function AlertChargeConfirm(id) {
    AlertConfirm("您的支付窗口现在已经在新的页面打开", "主页已经锁定，你的支付窗口已经在新的页面打开。如果你充值完成请点击“充值完成”按钮。",
    "http://www.dyjjp.com/images/button1.jpg", "alertChargeContinueCharge()",
    "http://www.dyjjp.com/images/button2.jpg", "alertChargeAfterCharge(" + id + ")");
}
//继续充值
function alertChargeContinueCharge() {
    RemoveAlertBox("Alert_Confirm", "Alert_Confirm_screenMask");
}
//充值完成
function alertChargeAfterCharge(id) {
    $.ajax({
        //要用post方式      
        type: "Post",
        //方法所在页面和方法名      
        url: "/qyjx/WebServices.aspx/GetIsPaySuccess",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: "{ 'id': '" + id + "' }",
        success: function (data) {
            //返回的数据用data.d获取内容  
            var resultStr = data.d;
            if (resultStr == "success") {
                AlertSuccess("恭喜您充值成功", "充值已完成，祝您购物愉快");
                window.open("../index.aspx?s=/UserInfo/", '_self');
            }
            else {
                window.open("../chinabank/ChargeFailed.aspx", '_self');
            }
        },
        error: function (err) {
            //alert(err);
        }
    });
}

/*+++++++++++++++++++++++++++++++++++++
+ 功能:弹出充值窗口
+ 研发:wangl
+ 时间:2013-7-30 16:47:34
+++++++++++++++++++++++++++++++++++++*/
function AlertCharge() {

    var mymsg = '<html><head><meta http-equiv="Content-Type" content="text/html; charset=utf-8" /><title></title>' +
'<style>' +
'.paybox{position: relative; width: 870px; border:1px solid #6cb503; border-radius:5px;-webkit-border-radius: 5px;-moz-border-radius:5px;text-align:left;background-color:#ffffff}' +
'.title{background-color:#6cb503; height:38px; line-height:38px; font-family:微软雅黑; font-size:15px; color:#FFF; padding-left:20px;border-top-left-radius:4px;border-top-right-radius:4px;}' +
'.close{position: absolute; right:10px; cursor:pointer; top:10px;}' +
'.pro_tit {background: url(../images/Recha_tit1.jpg) no-repeat scroll left top transparent;height: 24px;margin:5px; margin-top:10px;}' +
'.titbg1 {background: url(../images/Recha_tit1.jpg) no-repeat scroll left top transparent;}' +
'.clearfix:after {	clear: both;content: ".";display: block;font-size: 0;height: 0;line-height: 0;	visibility: hidden;}' +
'.opbox {	float: left;font-size: 14px;width: 65%;}' +
'.opli {color: #555555;cursor: pointer;	height: 24px;line-height: 24px;	margin: 6px 0 6px 30px;	width: 600px; text-align:left;}' +
'span.blu {	color: #49A4DA;	font-weight: bold;}' +
'span.org {	color: #FF6600;	font-weight: bold;}' +
'ol.SelectBank {border-bottom: 1px solid #CCCCCC;list-style-type: none;	margin: 0 50px;	padding: 5px 0 0 15px;	position: relative;}' +
'.payby {line-height: 30px;	padding: 2px 0 5px 15px;}' +
'.cfm_pmt {	font-size: 12px;height: 24px;line-height: 24px;	margin: 10px 0 0 5px;}' +
'.cfm_txt {	line-height: 30px;	margin: 0 0 0 20px !important;	vertical-align: middle;}' +
'.cfm_bg {	background: none repeat scroll 0 0 #fdf6d3;	border: 1px solid #fdb98c;	color: #333333;	font-size: 14px;line-height: 30px;}' +
'.cfmbox {	margin: 0 10px;	padding-bottom: 20px; padding-top:20px;}' +
'.moneykuang {width:60px; height:24px;}' +
'.memo{ position: absolute; right:34px; top:62px; background-image:url(../images/alert/memo.png);background-repeat: no-repeat; width:232px;height:272px; font-size:12px; padding:35px 15px 0 15px; color:#555555}' +
'.memo p{ line-height:21px; margin:0}' +
'</style></head><body style="background-color:yellow;">' +
'<div class="paybox">' +
	'<div class="title" >用户充值</div>' +
	'<div class="close"><img src="../images/alert/close.png" /></div>' +
   '<div style="border:1px solid #e2e2e2; margin:20px; padding:8px; padding-bottom:25px;">' +
   '<div class="memo">' +
   	'<p>“拍点”是用户参与商品竞拍的必需品，充值拿拍点赢心仪宝贝，惊喜就在眼前！</p>' +
  '<p style="color:#F00">用户充值说明：</p>' +
'<p><span style="font-weight:bold; color:#49a4da">1.</span>拍点是参与竞拍的消耗品，竞拍成功后不予返还，竞拍失败可冲抵同等价值金钱进行补差价购买。</p>' +
'<p><span  style="font-weight:bold; color:#49a4da">2.</span>拍点支持提现功能，未消耗的拍点均可在“用户中心”申请提现，所以用户可放心购买。</p>' +
'<p><span  style="font-weight:bold; color:#49a4da">3.</span>充值所获拍点永久有效。</p>' +
   '</div>' +
'<form target="_blank" onsubmit="return false;checkSubmit();" method="post" action="../chinabank/CommonSend.aspx" id="alertChargeForm" name="formwyzx">' +
'<input type="hidden" name="wyzxRealPrice" id="wyzxRealPrice" value="0" title="金额">' +
'<input type="hidden" name="wyzxPoint1" id="wyzxPoint1" value="0" title="拍点">' +
'<input type="hidden" name="wyzxPoint2" id="wyzxPoint2" value="0" title="赠点">' +
'<input type="hidden" name="v_oid" id="v_oid" title="订单号">' +
'<input type="hidden" name="type" value="_cz">' +
  '<div style="color: #333;">' +
    '<div class="pro_tit titbg1"></div>' +
    '<div class="pro_con clearfix">' +
     ' <div class="opbox">' +
        '<div class="opli" onclick="alertChargeDivonclick(20,2000,0);">' +
          '<input type="radio" name="total_fee" id="radio20" value="20" />' +
          '<span class="litxt">￥ <span class="blu">20.00</span>   =    <span class="org">2000</span>  拍点 </span> </div>' +
        '<div class="opli" onclick="alertChargeDivonclick(50,5000,400);">' +
         ' <input type="radio" name="total_fee" id="radio50" value="50" />' +
          '<span class="litxt">￥ <span class="blu">50.00</span>   =    <span class="org">5000</span>  拍点 + [赠送 <span class="red">400</span> 赠点] </span> </div>' +
        '<div class="opli" onclick="alertChargeDivonclick(100,10000,1000);">' +
         ' <input type="radio" name="total_fee" id="radio100" value="100" />' +
          '<span class="litxt">￥ <span class="blu">100.00</span>   =    <span class="org">10000</span>  拍点 + [赠送 <span class="red">1000</span> 赠点] </span> </div>' +
        '<div class="opli" onclick="alertChargeDivonclick(200,20000,2400);">' +
         ' <input type="radio" name="total_fee" id="radio200" value="200" />' +
          '<span class="litxt">￥ <span class="blu">200.00</span>   =    <span class="org">20000</span>  拍点 + [赠送 <span class="red">2400</span> 赠点] </span> </div>' +
        '<div class="opli" onclick="alertChargeDivonclick(500,50000,7500);">' +
         ' <input type="radio" name="total_fee" id="radio500" value="500" />' +
          '<span class="litxt">￥ <span class="blu">500.00</span>   =    <span class="org">50000</span>  拍点 + [赠送 <span class="red">7500</span> 赠点] </span> </div>' +
        '<div class="opli" onclick="alertChargeDivonclick(1000,100000,25000);">' +
         ' <input type="radio" name="total_fee" id="radio1000" value="1000" />' +
          '<span class="litxt">￥ <span class="blu">1000.00</span>   =    <span class="org">100000</span>  拍点 + [赠送 <span class="red">25000</span> 赠点] </span> </div>' +
        '<div class="opli" onclick="alertChargeDivonclick(0,0,0);">' +
         '<input type="radio" name="total_fee" id="radio0" />' +
          '<span class="litxt">￥  <input maxlength="8" type="text" class="moneykuang" id="user_define" value="0" onkeyup="alertChargeReDefineFee(this.value)" />' +
          '=<span class="org" id="alertChargePoint1"> 0 </span>   拍点 + [ 赠送 <span class="red" id="alertChargePoint2"> 0 </span> 赠点<span id="pointdiv_0" style="display: none">，获得' +
           '<span class="red" id="point" style="display: none"> 0 </span> 积分</span>]</span> </div>' +
      '</div>' +
    '</div>' +
  '</div>' +
'</form>' +
'</div>' +
'<!--info一直到底部-->' +
  '<div class="cfmbox">' +
    '<div class="cfm_bg">' +
     ' <div class="cfm_txt"> 您选择的购买金额为 <span class="blu" id="AlertCharge_RealPrice">0</span> 元，总共可获得 <span class="red" id="AlertCharge_Point">0</span> 点拍点，支付方式： <span id="Span3" class="red">网银在线支付</span>。 </div>' +
    '</div>' +
  '</div>' +
  '<div style="text-align: center;">' +
    '<input type="image" src="../images/Recha_ok.jpg" onclick="alertChargeSubmit()" />' +
  '</div>' +
  '<div class="cfm_pmt" style="padding-left: 20px; margin-bottom:20px;"> <span class="org"> 友情提示：</span> 为确保您购买数额准确到帐，购买成功后请务必点击跳转链接。如遇到任何问题，请拨打 <span class="blu">40068-82228</span> 咨询。 </div>' +
'</div>' +
'</body>' +
'</html>';
    AlertBox("Alert_Charge", "", 60000, 870, "Alert_Charge_screenMask", 10000, false, mymsg, "", "#58a0f4");
}

function alertChargeDivonclick(realPrice, point1, point2) {
    $("#radio" + realPrice).click();
    if (realPrice == 0) {
        realPrice = $("#user_define").val();
        point1 = $("#alertChargePoint1").html();
        point2 = $("#alertChargePoint2").html();
    }
    $("#AlertCharge_RealPrice").html(realPrice);
    $("#AlertCharge_Point").html(parseInt(point1, 10) + parseInt(point2, 10));
    $("#wyzxRealPrice").val(realPrice);
    $("#wyzxPoint1").val(point1);
    $("#wyzxPoint2").val(point2);
    //alert($("#wyzxRealPrice").val());
}

function alertChargeSubmit() {
    var x = parseInt($("#wyzxRealPrice").val(), 10);
    if (isNaN(x) || x < 1) {
        AlertError("金额无效，请看仔细喔", "金额无效,请选择您准备购买的点数，金额必须是大于0的整数.");
        return false;
    }
    else {
        $.ajax({
            //要用post方式      
            type: "Post",
            //方法所在页面和方法名      
            url: "/qyjx/WebServices.aspx/GetOrderFormId",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: "{}",
            success: function (data) {
                $("#v_oid").val(data.d);
                //获取浏览器参数  
                var browserName = navigator.userAgent.toLowerCase();
                //                if (browserName.indexOf("msie") > 0) {
                //                    $('#alertChargeForm').attr('target', '_blank');
                //                } else {
                //                    $('#alertChargeForm').attr('target', '_self');
                //                }
                $('#alertChargeForm').attr('target', '_self');
                $("#alertChargeForm").submit();
                RemoveAlertBox("Alert_Charge", "Alert_Charge_screenMask");
                AlertChargeConfirm(data.d);
            },
            error: function (err) {
            }
        });

    }
}

function alertChargeReDefineFee(value) {
    $("#radio0").click();
    value = value.replace(/[^(\d)]/g, '');
    $("#user_define").val(value);
    RemoveAlertBox("Alert_Error", "Alert_Error_screenMask");
    //    if (isNaN(value) || value < 1) {
    //        AlertError("金额无效，请看仔细喔", "金额无效,请选择您准备购买的点数，金额必须是大于0的整数.");
    //        return ;
    //    }
    //    else {
    var price = value;
    var point1 = value * 100;
    var point2 = 0;
    if (price < 50) {
        point2 = 0;
    } else if (price < 100) {
        point2 = point1 * 0.08;
    } else if (price < 200) {
        point2 = point1 * 0.1;
    } else if (price < 500) {
        point2 = point1 * 0.12;
    } else if (price < 1000) {
        point2 = point1 * 0.15;
    } else {
        point2 = point1 * 0.25;
    }
    $("#alertChargePoint1").html(point1);
    $("#alertChargePoint2").html(point2);
    $("#AlertCharge_RealPrice").html(price);
    $("#AlertCharge_Point").html(point1 + point2);
    $("#wyzxRealPrice").val(price);
    $("#wyzxPoint1").val(point1);
    $("#wyzxPoint2").val(point2);
    //    }

}