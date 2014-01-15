/*+++++++++++++++++++++++++++++++++++++
+ 功能:竞拍倒计时的单体对象
+ 研发:宗帝
+ 时间:2010-12-20
+++++++++++++++++++++++++++++++++++++*/
function PaiPaiBidItem(_GameType, _Vid, _PaiPai) {

    this.GameType = (_GameType ? _GameType :'2D5ED96D-7AE9-4F0D-975B-2CF1DB756B31'), //游戏类型
	this.PaiPaiBid = _PaiPai; //竞拍核心对象
    this.id = _Vid,
	this.RobotUser = _PaiPai.GetRobotUser(this.id), //机器人对象
	this.VendueState = 0,
	this.Price = 0,
	this.UserId = this.PaiPaiBid.UserId,
	this.VendueUserId = 0, //当前竞拍信息对应的会员编号
	this.UserNickName = '',
	this.UserHeadImage = '',
    this.auctionTime ='',//开始拍卖时间
	this.EndStopTimeSecond = 0,
	this.EndStopTimeSecondOld = 0,
	this.EndStopTimeString = '',
	this.Mill = 0, //毫秒值
    this.timePoint = 0,
	this.MillStrFormat = [
		'<font style="color:#888;font-size:12px;valign:bottom;display:inline-block;width:10px;line-height:16px;"></font>'
		, '<font style="color:#888;font-size:12px;valign:bottom;display:inline-block;width:10px;line-height:16px;">', '</font>']
    //赋值.
    //jQuery对象
    this.oPrice = null,
	this.oUserInfo = null,
	this.oTimer = null,
	this.oBtn = null,
	this.oSpanTip = null,
	this.CheckObject = function () {
	    var _this = this;
	    if (this.oPrice == null) { this.oPrice = jQuery("#Price_" + this.id); }
	    if (this.oUserInfo == null) { this.oUserInfo = jQuery("#UserInfo_" + this.id); }
	    if (this.oTimer == null) { this.oTimer = jQuery("#timer_" + this.id); }
	    if (this.oBtn == null) { this.oBtn = jQuery("#btn_" + this.id); }
	    if (this.oSpanTip == null) { this.oBtn = jQuery("#spanTip_" + this.id); }
	},
	this.Fill = function () {
	    var _this = this;
	    //this.CheckObject();	
	    //赋值
	    this.oPrice.html("￥" + this.Price);
	    this.oUserInfo.html(this.UserNickName);
	    if (this.GameType == '2D5ED96D-7AE9-4F0D-975B-2CF1DB756B31') {//常规竞拍
	        if (this.VendueState != 3) {
	            this.EndStopTimeString = _this.PaiPaiBid.FormatTime_3(_this); //倒计时
	            //即将竞拍商品
//	            if (this.EndStopTimeString != '马上赢取' && this.EndStopTimeString > '02:00:00') {

//	                var myDate = new Date();
//	                myDate.setSeconds(myDate.getSeconds() + parseInt(_this.EndStopTimeSecond, 10) * 1);
//	                //	                alert(myDate.Format("yyyy年MM月dd日HH时mm分ss秒"));
//	                var str = ""; //myDate.getFullYear() + "年";
//	                str += (myDate.getMonth() + 1) + "月";
//	                str += myDate.getDate() + "日";
//	                str += myDate.getHours() + "时";
//	                str += myDate.getMinutes() + "分";

//	                this.oTimer.html("<font style='font-size:15px;color:red;'>" + str + "竞拍</font>"); this.oBtn.html("");
//	                //	                _this = null;
//	            }
//	            else {

	                if (this.EndStopTimeSecond > 30000) {
	                    this.oTimer.html('<font color="green">' + this.EndStopTimeString + this.MillStrFormat[0] + '</font>'); ////this.EndStopTimeSecondOld
	                }
	                else if (this.EndStopTimeSecond > 0||(this.timePoint<10&&this.timePoint>0)) {
	                    this.oTimer.html('<font color="red">' + this.EndStopTimeString + this.MillStrFormat[1] + this.MillStrFormat[2] + '</font>'); ////this.EndStopTimeSecondOld
	                }
	                else {
	                    this.oTimer.html('<font color="red">' + this.EndStopTimeString + this.MillStrFormat[0] + '</font>'); ////this.EndStopTimeSecondOld
	                }

	                //分身
	                //window.status = "Vid=" + this.VendueUserId + "/Uid="+this.UserId + "/状态:" + this.RobotUser.GetState();  
	                //出价条件.

	                if (this.UserId > 0 && this.VendueUserId != this.UserId)//&& this.EndStopTimeSecond<=1)
	                {
	                    //this.PaiPaiBid.RobotUserBid(_this.id,_this.Price,_this.EndStopTimeSecond);
	                    _this.RobotUser.Running(_this.Price, _this.EndStopTimeSecond);
	                }
//	            }
	        }
	        else {
	            this.oTimer.html("已成交"); this.oBtn.html("");
//	            _this.PaiPaiBid.Remove(this.id);
//	            _this = null;
	        }
	    }
	    else if (this.GameType == '89737BC5-37DB-4DC8-B527-B1136162839E') {
	        if (this.VendueState != 3) {
	            this.EndStopTimeString = _this.PaiPaiBid.FormatTime_3(_this);
	            if (this.EndStopTimeSecond > 30000) {
	                this.oTimer.html('<font color="green">' + this.EndStopTimeString + this.MillStrFormat[0] + '</font>'); ////this.EndStopTimeSecondOld
	            }
	            else if (this.EndStopTimeSecond > 0 || (this.timePoint < 10 && this.timePoint > 0)) {
	                this.oTimer.html('<font color="red">' + this.EndStopTimeString + this.MillStrFormat[1] + this.MillStrFormat[2] + '</font>'); ////this.EndStopTimeSecondOld
	            }
	            else {
	                this.oTimer.html('<font color="red">' + this.EndStopTimeString + this.MillStrFormat[0] + '</font>'); ////this.EndStopTimeSecondOld
	            }

	        }
	        else {
	            this.oTimer.html("秒杀已成交");
//	            _this.PaiPaiBid.Remove(this.id);
//	            _this = null;
	        }
	    }
	},
	this.FillEndStopTime = function () {
	    var _this = this;
	    if (_this.timePoint < 10 && _this.timePoint > 0) {
	        var s = '<font color="red">' + this.EndStopTimeString
				+ this.MillStrFormat[1] + this.Mill
				+ this.MillStrFormat[2] + '</font>';
	        this.oTimer.html(s);
	        this.Mill--;
	        if (this.Mill < 0) this.Mill = 0;
	    }
	},
	this.Input = function (d) {
	    //id,timelength,price,state,saveprice,stoptime,nick,uiddec,headimage,servertime
	    var _this = this;
	    try {
	        this.CheckObject();
	        this.Price = d["AuctionPrice"];
	        this.VendueState = d["Status"];
	        this.VendueUserId = d["HuiYuanID"]; //竞拍信息对应的会员编号
	        this.UserNickName = d["HuiYuanName"];
	        this.UserHeadImage = d["HuiYuanID"];
	        this.timePoint = d["TimePoint"];
	        this.AuctionTime = new Date(parseInt(d["AuctionTime"].replace("/Date(", "").replace(")/", ""), 10));
	        this.EndStopTimeSecond =this.AuctionTime.getTime()-(new Date().getTime());
	        if (!this.RobotUser)
	            this.RobotUser = this.PaiPaiBid.GetRobotUser(this.id);
	    }
	    catch (e) {
	        //	alert(e.meseage);
	    }
	    //alert(this.id);
	}




}

/*+++++++++++++++++++++++++++++++++++++
+ 功能:核心托管类
+ 研发:宗帝
+ 时间:2010-11-19
+++++++++++++++++++++++++++++++++++++*/
function PaiPaiBid() {
    this.GameType = 0, //游戏类型
	this.PointType = 2, //使用点的类型(1,2,3)
	this.SeMilli = 1000,
	this.MinMilli = this.SeMilli * 60; //每分钟多少毫秒
    this.HrMilli = this.MinMilli * 60; //每小时多少毫秒
    this.DayMilli = this.HrMilli * 24; //	

    //内部变量(public)

    this.UserId = 0; //0, //当前会员编号
    this.Url ="/Auction/testajax.aspx",
	this.VendueList = "", //所有时间项
	this.VendueList2 = "", //最后30秒项
	this.VendueList3 = "", //最后10秒项(1/10显示项)
	this.DivList = [], //0:时间,1:会员链接,2:会员ID,3,竞拍价,4:按钮
	this.RobotUserList = [], //分身对象列表
	this.timer = null,
	this.timerTmp = null,
	this.ArrTimer = [],
	this.AjaxDataAll = null, //总数据
	this.AjaxData_1 = [], //各自的本秒次数据
	this.AjaxData_2 = [], //各自的本秒次增量数据
    //this.CurrentDate = new Date().getHour(),
	this._Hour = new Date().getHours(),
	this.index = 0,
	this.IsShowHistory = 0, //0不显示,1显示[详细页]	
	this.IsShowUserInfo = 0, //0不显示,1显示[详细页]
	this.timer_Detail = new Array(2); //0:历史,1当前会员
    //获取数据,并调用填充
    this.GetData = function () {
        this.timerTmp = new Array(this.VendueList.length);
        var _this = this; _Hour = new Date().getHours();
        //检查时间,60s,30s
        //alert(this.DivList["Bid_1"]);
        //karemake时间段修改为全天
        //        if (_Hour >= 0 && _Hour < 9) { this.timer = setTimeout(function () { _this.GetData() }, 60000); }
        //        else 
        if (this.VendueList.length < 1) { this.timer = setTimeout(function () { _this.GetData() }, 30000); }
        else {
            var _this = this; this.Ajax("GET", this.Url, { "tid": this.VendueList }, "json"
			, function (dt) {
			    if (dt != null && dt.length > 0) {
			        try {
			            _this.AjaxData_1[0] = dt = eval(dt);
			            //alert(dt);
			            //if(1==0)
			            for (var i = 0; i < dt.length; i++) {
			                var d = (dt[i]);
			                var id = d["AuctionID"].toString(); //id值
			                var o = _this.DivList["Bid_" + id];

			                //if(id==5)	window.status = d[1];

			                o.Input(d);
			                o.Fill();
			                o.Mill = 9;
			            }
			        }
			        catch (e) {
			            alert(e.message);
			        }
			    }
			    this.timer = setTimeout(function () { _this.GetData() }, 800);
			}, function (msg) { /*error*/alert(msg); });
        }
    }, //格式化填充
	this.Fill = function () {
	    var _this = this;
	    var _t;
	    //倒计时,
	    var dt = _this.AjaxData_1[0];
	    //if(dt)
	    {
        if(dt!=null&&dt.length>0){
            try {
                //alert(dt);
                //if(1==0)
                for (var i = 0; i < dt.length; i++) {
                    var d = (dt[i]);
                    var id = d["AuctionID"].toString(); //id值
                    var o = _this.DivList["Bid_" + id];
                    o.FillEndStopTime();
                    //o.Mill --;
                }
            } catch (se) {//window.status = e.message;
                alert(se);
            }
        }	        
	    }
	    //var n = parseInt(window.status,10);if(isNaN(n))n=0;window.status = (++n);
	    _this.ArrTimer[0] = setTimeout(function () { _this.Fill(); }, 100); //启动1/10计时器

	}
    //添加倒计时项
	, this.Add = function (id)//,arr)
	{
	    var _this = this;
	    //0:时间,1:会员链接,2:会员头像和链接,3,竞拍价,4:按钮,5:tip显示框,
	    var arr = ["timer_" + id, "UserInfo_" + id, "UserHeadImage_" + id, "Price_" + id, "btn_" + id, "spanTip_" + id];
	    //this.DivList["Bid_"+id]= arr;//9:44 2010-12-20加入bid
	    //说明有值
	    if (this.VendueList.length > 0) this.VendueList += "," + id;
	    else this.VendueList = id;
	    this.DivList["Bid_" + id] = new PaiPaiBidItem(_this.GameType, id, _this);
	    this.RobotUserList["Bid_" + id] = new RobotUser(_this.GameType, id, _this)
	    //alert(this.DivList["Bid_"+id].id);
	    //alert(this.RobotUserList["Bid_"+id].VendueId);
	}
    //移除:成交项将移出.
	, this.Remove = function (id) {
	    var _this = this; var arr = _this.VendueList.toString().split(","); var list = "";
	    for (var n = 0; n < arr.length; n++) {
	        if (arr[n] == id) { _this.DivList["Bid_" + id] = null; _this.ArrTimer[id] = null; }
	        else { list += arr[n] + ","; }
	    }
	    if (list.length > 0 && list.substr(list.length - 1, 1) == ",") _this.VendueList = list.substr(0, list.length - 1);
	    else _this.VendueList = list;
	}
    //设置闪动
	, this.SetActiveBackGroundColor = function (o, ActiveColor, DefaultColor) {
	    var x = o.css("background-color");
	    o.css("background-color", ActiveColor);
	    o.fadeTo("1", 0.4, function () {
	        o.css("background-color", x); //.css("color","red");
	        if (jQuery.browser.msie) o.css("filter", ""); else o.fadeTo("100", 1);
	    });
	}
    //设置时间双位
	, this.Ft = function (d) {
	    d = parseInt(d, 10);
	    if (d < 10) { d = "0" + d } return d;
	}
    //开始托管程序
	, this.Start = function () {
	    var _this = this;
	    this.GetData();
	    _this.ArrTimer[0] = setTimeout(function () { _this.Fill(); }, 1000); //启动1/10计时器
	    //window.status =_this.ArrTimer[0];
	}
    //停止托管程序
	, this.Stop = function () { clearInterval(this.timer); clearInterval(this.ArrTimer[0]); }
	, this.GetRobotUser = function (Vid) {
	    return this.RobotUserList["Bid_" + Vid];
	}
    //,this.StartRobotUser = function(Vid,_Type,_Count,_Price){
    //_StartType,_StartPrice,_StartTime,_StartCount
    //	,_TacticType ,_StopType,_StopPrice,_StopPriceStep,_StopTime
    //this.RobotUserList["Bid_"+Vid].Start(_Type,_Count,_Price);
	, this.StartRobotUser = function (Vid, _StartType, _StartPrice, _StartTime, _StartCount
		, _TacticType, _StopType, _StopPrice, _StopPriceStep, _StopTime) {
	    this.RobotUserList["Bid_" + Vid].Start(_StartType, _StartPrice, _StartTime, _StartCount
			, _TacticType, _StopType, _StopPrice, _StopPriceStep, _StopTime);


	    //alert(this.RobotUserList["Bid_"+id].State);
	}
	, this.StopRobotUser = function (Vid) {
	    this.RobotUserList["Bid_" + Vid].Stop();
	}
	, this.RobotUserBid = function (Vid, _Price, _EndStopTimeSecond) {
	    this.RobotUserList["Bid_" + Vid].Bid(_Price);
	}
	, this.IsIp = function (ip) { var re = /^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$/; return re.test(ip); }
	, this.IsPhone = function (phone) { var re = /^1[0-9]\d{9}$/; return re.test(phone); }
	, //测试调用
	this.test = function () {
	    var _this = this; var v = "2010-12-01 12:50:50";
	    if (this.DivList.length > 0) { var o = jQuery("#" + this.DivList["Bid_" + "16"]); this.SetActiveBackGroundColor(o, "#dd0000", "#ffffff"); o.html(this.FormatTime(v)) }
	    this.timer = setTimeout(function () { _this.test() }, 1000);
	}
};


//绑定数据内容:
//倒计时,竞拍人(ID,头像,昵称),竞拍价,竞拍状态
//s	/INDEX/index
//t	1291130482845
//tid	1251,1,7,6,9,1253,19,15,14,13,12,11
//http://127.0.0.1:8002/thread.aspx?s=/INDEX/index&tid=1251%2C1%2C7%2C6%2C9%2C1253%2C19%2C15%2C14%2C13%2C12%2C11&t=1291130465977

/*+++++++++++++++++++++++++++++++++++++
+ 功能:通用 Ajax 提交
+ 研发:宗帝
+ 时间:2010-11-19
+++++++++++++++++++++++++++++++++++++*/
PaiPaiBid.prototype.Ajax = function (_Type, _Url, _data, _dataType, _success, _error) {
    jQuery.ajax({ "type": (_Type ? _Type : "POST"), "url": (_Url ? _Url : ""), "data": (_data ? _data : ""),"dataType":(_dataType?_dataType:""),"cache":false
		, success: (_success ? _success : null), error: (_error ? _error : null)
    });
}
/*+++++++++++++++++++++++++++++++++++++
+ 功能:时间格式化,并调用闪动
+ 研发:宗帝
+ 时间:2010-11-19
+++++++++++++++++++++++++++++++++++++*/
PaiPaiBid.prototype.FormatTime = function (v, servertime, VendueId, oThis) {

    var _this = this;
    return _this.FormatTime_2(v, servertime, VendueId, oThis);

    //var v = "2010-11-29 13:24:00";
    var arr = v.split(/[-|\x20|:]/);
    var d = new Date(parseInt(arr[0], 10), parseInt(arr[1], 10) - 1, parseInt(arr[2], 10), parseInt(arr[3], 10), parseInt(arr[4], 10), parseInt(arr[5], 10));

    arr = servertime.split(/[-|\x20|:]/);
    var t = new Date(); //parseInt(arr[0],10),parseInt(arr[1],10)-1,parseInt(arr[2],10),	parseInt(arr[3],10),parseInt(arr[4],10),parseInt(arr[5],10));
    var dt = d.getTime();
    var tt = t.getTime(); //毫秒数//1秒=1000毫秒
    var x = dt - tt;
    if (x < 0) x = 0; x = parseInt(x / 100, 10);
    var SeMilli = 10;
    var MinMilli = SeMilli * 60; //每分钟多少毫秒
    var HrMilli = MinMilli * 60; //每小时多少毫秒
    var DayMilli = HrMilli * 24; //
    //x = x % DayMilli;
    var h = parseInt(x / HrMilli, 10);
    var m = parseInt((x % HrMilli) / MinMilli, 10);
    var s = parseInt((x % MinMilli) / SeMilli, 10);
    var ms = x % SeMilli;
    var str = _this.Ft(h) + ":" + _this.Ft(m) + ":" + _this.Ft(s); //+" "+ Ft(ms);
    if (x < 1) x = 0;
    //处理

    if (VendueId && _this.DivList["Bid_" + VendueId] != null && _this.DivList["Bid_" + VendueId].length > 0) {
        var o = jQuery("#" + _this.DivList["Bid_" + VendueId][0]);
        var LastTimeLong = o.attr("LastTimeLong");
        if (LastTimeLong < x) {
            _this.SetActiveBackGroundColor(jQuery("#" + _this.DivList["Bid_" + VendueId][3]), "#ff0000", "");
        }
        o.attr("LastTimeLong", x);
    }

    if (x > 300) {//30s
        str = _this.Ft(h) + ":" + _this.Ft(m) + ":" + _this.Ft(s);
        return ('<font color="green">' + str + '</font>');
    } else if (x > 100) {//100
        str = _this.Ft(h) + ":" + _this.Ft(m) + ":" + _this.Ft(s);
        return ('<font color="red">' + str + '</font>');
    }
    else if (x > 0) {
        str = _this.Ft(h) + ":" + _this.Ft(m) + ":" + _this.Ft(s) //+ " " + ms;
				+ ' <font style="background-color:#666;color:yellow;font-size:14px;valign:bottom;padding:3px 3px 2px 3px">' + ms + '</font>';
        return ('<font color="red">' + str + '</font>');
        //return str;
    }
    else {
        return ('<font color="red">马上赢取</font>');
    }

}
PaiPaiBid.prototype.FormatTime_2 = function (v, servertime, VendueId, oThis) {
    var _this = this;
    //秒数
    var x = parseInt(v, 10) * 1;
    //x = parseInt(x/100,10);
    //alert(x);
    var SeMilli = 1; //每秒多少毫秒
    var MinMilli = SeMilli * 60; //每分钟多少毫秒
    var HrMilli = MinMilli * 60; //每小时多少毫秒
    var DayMilli = HrMilli * 24; //
    //x = x % DayMilli;
    var h = parseInt(x / HrMilli, 10);
    var m = parseInt((x % HrMilli) / MinMilli, 10);
    var s = parseInt((x % MinMilli) / SeMilli, 10);
    var ms = x % SeMilli;
    var str = _this.Ft(h) + ":" + _this.Ft(m) + ":" + _this.Ft(s); //+" "+ Ft(ms);
    if (x < 1) x = 0;
    //处理

    if (VendueId && _this.DivList["Bid_" + VendueId] != null && _this.DivList["Bid_" + VendueId].length > 0) {
        var o = jQuery("#" + _this.DivList["Bid_" + VendueId][0]);
        var LastTimeLong = o.attr("LastTimeLong");
        if (LastTimeLong < x) {
            _this.SetActiveBackGroundColor(jQuery("#" + _this.DivList["Bid_" + VendueId][3]), "#ff0000", "");
        }
        o.attr("LastTimeLong", x);
    }

    if (x > 300) {//30s
        str = _this.Ft(h) + ":" + _this.Ft(m) + ":" + _this.Ft(s);
        return ('<font color="green">' + str + '</font>');
    } else if (x > 100) {//100
        str = _this.Ft(h) + ":" + _this.Ft(m) + ":" + _this.Ft(s);
        return ('<font color="red">' + str + '</font>');
    }
    else if (x > 0) {
        str = _this.Ft(h) + ":" + _this.Ft(m) + ":" + _this.Ft(s) //+ " " + ms;
				+ ' <font style="background-color:#666;color:yellow;font-size:14px;valign:bottom;padding:3px 3px 2px 3px">' + ms + '</font>';
        return ('<font color="red">' + str + '</font>');
        //return str;
    }
    else {
        return ('<font color="red">马上赢取...</font>');
    }
}


PaiPaiBid.prototype.FormatTime_3 = function (oItemThis) {
    var _this = this;
    //秒数
    //    var x = parseInt(oItemThis.EndStopTimeSecond, 10) * 1;
    var auctiontime = oItemThis.AuctionTime;
    var date = new Date();
    var x = parseInt((auctiontime.getTime() - date.getTime()),10);
    if (x <= 10000) {
        x = (oItemThis.timePoint)*1000;
    }    
    //if(oItemThis.id==9876)	window.status = oItemThis.EndStopTimeSecond;

    var SeMilli = 1000; //每秒多少毫秒
    var MinMilli = SeMilli * 60; //每分钟多少毫秒
    var HrMilli = MinMilli * 60; //每小时多少毫秒
    var DayMilli = HrMilli * 24; //
    //x = x % DayMilli;
    var h = parseInt(x / HrMilli, 10);
    var m = parseInt((x % HrMilli) / MinMilli, 10);
    var s = parseInt((x % MinMilli) / SeMilli, 10);


    var str = _this.Ft(h) + ":" + _this.Ft(m) + ":" + _this.Ft(s)
    if (x < 1) x = 0;
    //处理
    if (oItemThis.EndStopTimeSecondOld < x) { _this.SetActiveBackGroundColor(oItemThis.oPrice, "#ff0000", ""); }
    oItemThis.EndStopTimeSecondOld = x;

    //window.status = x;
    if (x > 0) {
        return (_this.Ft(h) + ":" + _this.Ft(m) + ":" + _this.Ft(s)); // + "|"+x;
    }
    else {
            return x;
    }

}

/*+++++++++++++++++++++++++++++++++++++
+ 功能:IP显示格式化
+ 研发:宗帝
+ 时间:2010-11-19
+++++++++++++++++++++++++++++++++++++*/
PaiPaiBid.prototype.FormatIp = function (strIP) {
    if (this.IsIp(strIP)) {
        var len = strIP.lastIndexOf(".");
        return strIP.substr(0, len + 1) + "*";
    }
    else return strIP;
}

/*+++++++++++++++++++++++++++++++++++++
+ 功能:手机号显示格式化
+ 研发:宗帝
+ 时间:2010-11-19
+++++++++++++++++++++++++++++++++++++*/
PaiPaiBid.prototype.FormatPhone = function (strPhone) {
    if (this.IsPhone(strPhone)) {

        return strPhone.substr(0, 3) + "****" + strPhone.substr(7, 4);
    }
    else return strPhone;
}


/*+++++++++++++++++++++++++++++++++++++
+ 功能:点击触发时的处理时,显示提示信息,并在5秒后消失.
+ 研发:宗帝
+ 时间:2010-11-19
+++++++++++++++++++++++++++++++++++++*/
PaiPaiBid.prototype.ShowBidTip = function (id, content, FontColor, Second) {
    var _this = this;
    if (!Second) Second = 1500;

    var o;
    o = jQuery("#spanTip_" + id);
    PaiPaiCMS.DialogBox.ShowSimpleTip(content + "", "bold", FontColor, 15, "center", 200, o);
    _this.timerTmp[id] = setTimeout(function () { PaiPaiCMS.DialogBox.CloseSimpleTip(o); clearInterval(_this.timerTmp[id]); }, Second);
}

/*+++++++++++++++++++++++++++++++++++++
+ 功能:点击触发时的处理
+ 研发:宗帝
+ 时间:2010-11-19
+++++++++++++++++++++++++++++++++++++*/
PaiPaiBid.prototype.Bid = function (id, IsRobotUserRunning) {

    var _ajaxtimeout = 5000;
    var _this = this;

    if (!IsRobotUserRunning) IsRobotUserRunning = 0;
    //alert("Bid="+IsRobotUsrRunning);
    if (IsRobotUserRunning == 0) {
        _this.StopRobotUser(id);
    }

    //alert("bid"+this);
    ////加入判断如果完成了则不提示信息
    if (jQuery("#timer_" + id).text() == '--:--:--') {
        showBidTip(id, '出价信息到达服务器时竞拍已经结束，下次请提早出价！');
        return false;
    }
    //本段代码为解决首页免费竞拍不消耗返点的问题
    $.ajax({
        type: "GET", //"POST",
        timeout: _ajaxtimeout,
        url: "/Auction/submitauction.aspx",
        cache: false,
        data: { 'tid': id, "RobotUser": IsRobotUserRunning },
        success: function (data) {
            _this.BidSuccess(id, data);
        }, error: function (msg, status) {
            alert(msg);
            alert(status);
        }
    });
}

PaiPaiBid.prototype.BidSuccess = function (id, msg) {
    var _this = this;

    //window.status = msg;
    ////1正常,0操作失败,2没登陆,3值不够,4已出过价,5游戏已结束,6封盘 #ff0000  #339900
    if (msg == 1) {//ShowBidTip(id,'<span style="color:#ff0000">您出价成功</span>',"#339900");
        _this.ShowBidTip(id, '您出价成功', "#339900");
    } else if (msg == 0) {
        //_this.ShowBidTip(id,'您出价失败，请重试');
    } else if (msg == 2) {
        AjaxLoginBoxClose();
        AjaxLoginBox();
        //_this.ShowBidTip(id,'请先登录',"#339900");
    } else if (msg == 3) {
        _this.ShowBidTip(id, '您的余额不足，请充值!'); //<a href="javascript:paycountnow();" title="充值，获取闪点">【马上充值】</a>');
        isAlertCharge(id);

    } else if (msg == 4) {
        _this.ShowBidTip(id, '您已出价，无需重复', "#FF0000");
        //var msg = '<div class="tc"><span class="button"><span class="first-child"><button onclick="removeDialogbox(\'#userAway\');" type="button">您已出价，无需重复!</button></span></span></div>';
        //addDialogbox("#userAway","拍拍网提示",10010,300);
        //jQuery("#userAway .dialogboxContent").html(msg);
    } else if (msg == 5) {
        _this.ShowBidTip(id, '商品已成交', "#339900");
    } else if (msg == 6) {
        _this.ShowBidTip(id, '现已经到达封盘时段（00:00 - 09:00），请于开盘时再竞拍');
    } else if (msg == 7) {
        ShowBidTip(id, '您的资金账号已被冻结，请与我们的客服联系！', "#FF0000");
    } else if (msg == 8) {
        _this.ShowBidTip(id, '您的用户账号已被冻结，请与我们的客服联系！', "#FF0000");
        Fast_Logout(true); //用户冻结之后马上退出
    } else if (msg == 9) {
        _this.ShowBidTip(id, '已达到4周获得游戏封顶', "#FF0000");
        //delCookie('topboxopen');
        //opentopbox();//控制打开个人获得的浮动层
    } else if (msg == 10) {
        _this.ShowBidTip(id, '您已获得过商品，此为新人竞拍商品', "#FF0000");
    } else if (msg == 11) {
        _this.ShowBidTip(id, '您出价太快，请放松', "#FF0000");
    } else if (msg == 12) {
        _this.ShowBidTip(id, '此为新人竞拍商品，请先进行账户安全认证', "#FF0000");
    } else if (msg == 13) {
        _this.ShowBidTip(id, '检测点击频率太快，暂停出价30秒', "#FF0000");
    } else if (msg == 14) {
        _this.ShowBidTip(id, '您已设置分身进行竞拍，请取消或者等待分身出价结束后再操作！', "#FF0000");
    } else if (msg == 15) {
        _this.showBidTip(id, '您已达到出价次数的上限！', "#FF0000");
    }
    else if (msg == 16) {
        _this.ShowBidTip(id, '竞拍前请先手机验证！');
        phoneVerification();
    } else if (msg == 17) {
        _this.ShowBidTip(id, '本期商品竞拍尚未开始', "#339900");
    } else if (msg == 100) {
        _this.ShowBidTip(id, '游戏类型不一致');
    }else if (msg == 18) {
        _this.ShowBidTip(id,'您的返点不足！');
    } else {
        _this.ShowBidTip(id, '出价信息到达服务器时竞拍已经结束，下次请提早出价！', "#FF0000");
    }

}




PaiPaiBid.prototype.BidSuccessForSecondGame = function (id, msg) {
    var _this = this;

    //alert("bidsuccess"+this+"|"+msg);
    //var ShowBidTip = _this.ShowBidTip;
    //window.status = msg;
    ////1正常,0操作失败,2没登陆,3值不够,4已出过价,5游戏已结束,6封盘 #ff0000  #339900
    if (msg == 1) {//ShowBidTip(id,'<span style="color:#ff0000">您出价成功</span>',"#339900");
        _this.ShowBidTip(id, '您秒杀成功', "#339900");
    } else if (msg == 0) {
        //_this.ShowBidTip(id,'您出价失败，请重试');
    } else if (msg == 2) {
        AjaxLoginBoxClose();
        AjaxLoginBox();
        //_this.ShowBidTip(id,'请先登录',"#339900");
    } else if (msg == 3) {
        _this.ShowBidTip(id, '您的余额不足，请充值!'); //<a href="javascript:paycountnow();" title="充值，获取闪点">【马上充值】</a>');
    } else if (msg == 4) {
        _this.ShowBidTip(id, '您已秒杀，无需重复', "#FF0000");
        //var msg = '<div class="tc"><span class="button"><span class="first-child"><button onclick="removeDialogbox(\'#userAway\');" type="button">您已出价，无需重复!</button></span></span></div>';
        //addDialogbox("#userAway","拍拍网提示",10010,300);
        //jQuery("#userAway .dialogboxContent").html(msg);
    } else if (msg == 5) {
        _this.ShowBidTip(id, '商品已成交', "#339900");
    } else if (msg == 6) {
        _this.ShowBidTip(id, '现已经到达封盘时段（00:00 - 09:00），请于开盘时再竞拍');
    } else if (msg == 7) {
        ShowBidTip(id, '您的资金账号已被冻结，请与我们的客服联系！', "#FF0000");
    } else if (msg == 8) {
        _this.ShowBidTip(id, '您的用户账号已被冻结，请与我们的客服联系！', "#FF0000");
        Fast_Logout(true); //用户冻结之后马上退出
    } else if (msg == 9) {
        _this.ShowBidTip(id, '已达到4周获得游戏封顶', "#FF0000");
        //delCookie('topboxopen');
        //opentopbox();//控制打开个人获得的浮动层
    } else if (msg == 10) {
        _this.ShowBidTip(id, '您已获得过商品，此为新人竞拍商品', "#FF0000");
    } else if (msg == 11) {
        _this.ShowBidTip(id, '您出价太快，请放松', "#FF0000");
    } else if (msg == 12) {
        _this.ShowBidTip(id, '此为新人秒杀商品，请先进行账户安全认证', "#FF0000");
    } else if (msg == 13) {
        _this.ShowBidTip(id, '检测点击频率太快，暂停出价30秒', "#FF0000");
    } else if (msg == 14) {
        _this.ShowBidTip(id, '您已设置分身进行秒杀，请取消或者等待分身出价结束后再操作！', "#FF0000");
    } else if (msg == 15) {
        _this.showBidTip(id, '您已达到出价次数的上限！', "#FF0000");
    } else if (msg == 20) {
        //_this.ShowBidTip(id,'秒杀已结束,下次请提早出价！',"#FF0000");
        _this.ShowBidTip(id, '秒杀还未启动！', "#FF0000");
    } else if (msg == 21) {
        //_this.ShowBidTip(id,'秒杀已结束,下次请提早出价！',"#FF0000");
        _this.ShowBidTip(id, '未到秒杀时间！', "#FF0000");
    } else if (msg == 100) {
        //	alert(msg);
        _this.ShowBidTip(id, '游戏类型不一致');
    } else {
        _this.ShowBidTip(id, msg);
    }

}

PaiPaiBid.prototype.BidHistory = function (id, VendueState, oDiv) {

    if (!VendueState) VendueState = 0;
    //VendeuState = parseInt(VendeuState,10);
    if (isNaN(VendueState)) VendueState = 0;
    if (VendueState >= 3) { clearInterval(this.timer_Detail[0]); return false; }
    else {
        var _this = this;
        var _url = "/Auction/ajax/BidHistory.ashx";
        //alert(_url);
        AjaxSubmit(null, _url, { "id":id }
			, function (msg) { _this.BidHistoryFill(msg, id, oDiv); }
			, null, 'json'
		);
        this.timer_Detail[0] = setTimeout(function () { _this.BidHistory(id, VendueState, oDiv) }, 6000); //3s
        //clearInterval(this.timer_Detail[0]);
    }
}
PaiPaiBid.prototype.BidHistoryFill = function (data, id, oDiv) {

    //var n=1;//先采用全部刷新的方式
    var _this = this;
    var tid = id;
    var data_all = data;
    var htmlvar;
    var html_header = '<table cellspacing="0" cellpadding="0" border="0" class="n_bidusers">'
		+ '<tr><th class="l2"><div  style="width:75px;white-space:nowrap;overflow:hidden;">会员</div></th><th class="l2">IP</th><th class="l2">手机</th><th class="l1">参与价</th></tr>';
    var html_footer = '</table>'; //

    var html_body = '';
    var item_num = 0;
    var item_type = '';
    //alert(data_all["all"]);
    //更新所有记录
    jQuery.each(data_all["all"], function (i, n) {
        var item = n;
        if (i > 9) return false;
        //item_num++;
        //if(item.bidtype==1){item_type = '分身';//0
        //}else{item_type = '手动';//1
        //}		
        html_body += '<tr><td ><div  style="width:75px;white-space:nowrap;overflow:hidden;"><a class="noreturn n_u1">' + item.nickname + '</a></div></td>'
     			+ '<td >' + _this.FormatIp(item.ip) + '</td>'
     			+ '<td >' + _this.FormatPhone(item.UserMobile) + '</td>'
     			+ '<td style="text-align:center">¥' + item.price + '</td>'
     			+ '</tr>';

        //有更新时才更新??		
        /*
        if(i<1)	{
        if(const_OtherCity.indexOf(item.locate)==-1)
        {
        if(LastCityName!=item.locate && item.locate.length>1)
        {
        Detail_set_obj( item.locate );
        LastCityName=item.locate;
        }
        }
        }*/


    });

    oDiv.html(html_header + html_body + html_footer);

    //Detail_set_obj



}

//针对性处理

PaiPaiBid.prototype.UserInfoSelf = function (id, SafetyPrice, oDiv) {
    //alert(SafetyPrice);
    var _this = this;
    var tid = id;
    var islogin = 1; //getCookie('islogin');
    var js_saveprice = SafetyPrice;
    var html_header = '<table cellspacing="0" cellpadding="0" border="0" class="n_bidusers">';
    var html_footer = '</table>'; //
    var html_body = '';

    var _url = "/index.aspx?s=/Auction/ajaxmycount/tid/" + tid;
    if (islogin) {
        AjaxSubmit("POST", _url, { "GameType": _this.GameType }, function (data) {
            //alert(data["self_point_a"]);
            //data.self_point_a=530;
            //data.self_point_b=330;
            //data.self_price_point_a = 8;

            html_body += '<tr><td class="l1">使用拍点:</td><td class="l2">' + data["self_point_a"] + '</td></tr>';
            html_body += '<tr><td class="l1">使用赠点:</td><td class="l2">' + data["self_point_b"] + '</td></tr>';
            html_body += '<tr><td class="l1">使用拍点总数:</td><td class="l2">' + (data["self_point_a"] * 1 + data["self_point_b"] * 1) + '</td></tr>';
            html_body += '<tr><td class="l1">补差价购买价:</td><td class="l2">&yen;<font color="red">'
				+ js_saveprice + '</font> - ' + (data["self_point_a"]) / 100.0 + ' = ¥<font color="red">'
				+ (js_saveprice - (data["self_point_a"]) / 100.0)
				+ '</font></td></tr>';
            oDiv.html(html_header + html_body + html_footer);
            //_this.UserInfoSelf(id,SafetyPrice,oDiv);
            window.setTimeout(function () { _this.UserInfoSelf(id, SafetyPrice, oDiv) }, 3000); //开始
        }, null, "json");



    }
    else {
        html_body += '<tr><td class="l1">使用拍点</td><td class="l2">0 点</td></tr>';
        html_body += '<tr><td class="l1">使用赠点</td><td class="l2">0 点</td></tr>';
        html_body += '<tr><td class="l1">使用拍点总数</td><td class="l2">0 点</td></tr>';
        html_body += '<tr><td class="l1">补差价购买价</td><td class="l2">&yen;' + (js_saveprice) + '- 0 = ' + js_saveprice + '</td></tr>';
        oDiv.html(html_header + html_body + html_footer);
    }

}

/*
var PaiPai_Manage = new PaiPaiBid();
//m.DivList = ["txTimeDiv"];//txTime
PaiPai_Manage.Add ("16","txTimeDiv");
PaiPai_Manage.test();
*/


/*+++++++++++++++++++++++++++++++++++++
+ 功能:分身(又名,自动出价器)
+ 研发:宗帝
+ 时间:2010-12-23
+++++++++++++++++++++++++++++++++++++*/
function RobotUser(_GameType, _Vid, _PaiPai) {
    this.GameType = _GameType,
	this.PaiPaiBid = _PaiPai;

    this.UserId = (this.PaiPaiBid ? this.PaiPaiBid.UserId : 0),
	this.VendueId = _Vid,

	this.StartType = 0,
	this.StartPrice = 0,
	this.StartTimeStr = "",
	this.StartTime = new Date();
    this.StartCount = 0,

	this.TacticType = 0, //0最后10秒价,1随机出价(1~30秒)

	this.StopType = 0, //0:手工停止,1依次数,2依价格,3依时间
	this.StopPrice = 0,
	this.StopPriceStep = 1, //分.不是元
	this.StopCount = 0, //StopCount >=StartCount 时停止
	this.StopTime = new Date(),


	this.State = -1, //0:未启动状态,1执行中状态,2停止状态
    //出价策略 strategy / tactic



    //	this.StartTime = new Date(),//本次启动时的当前时间(客户端电脑时间,可能与服务器不一致),暂无功能
    //
	this.Start = function (_StartType, _StartPrice, _StartTime, _StartCount
		, _TacticType, _StopType, _StopPrice, _StopPriceStep, _StopTime) {
	    this.SetConfig(_StartType, _StartPrice, _StartTime, _StartCount
		, _TacticType, _StopType, _StopPrice, _StopPriceStep, _StopTime);
	},
	this.SetConfig = function (_StartType, _StartPrice, _StartTime, _StartCount
		, _TacticType, _StopType, _StopPrice, _StopPriceStep, _StopTime) {//钱以元为单位.
	    var _this = this;

	    if (this.UserId == 0 && getCookie("islogin") == 1) {

	        f5();
	    }
	    if (_this.UserId > 0) {
	        _this.State = -1;

	        _this.StartType = parseInt(_StartType, 10);
	        _this.StartPrice = _StartPrice;
	        _this.StartTimeStr = _StartTime;
	        var t = _this.GetDateParse(_StartTime);
	        _this.StartTime = t;
	        _this.StartCount = _StartCount;

	        _this.TacticType = parseInt(_TacticType, 10);
	        _this.StopType = _StopType;
	        _this.StopPrice = _StopPrice;
	        _this.StopPriceStep = _StopPriceStep;
	        _this.StopTime = _StopTime;

	        _this.StopCount = 0; //初始
	        //alert(_this.StartTime);
	        //this.Running();
	        _this.State = 0; //启动;
	        if (_this.StopType == 1 && (_this.StartCount - _this.StopCount) < 1) _this.State = -1;
	        if (_this.State == -1) { alert("自动出价器功能已停止." + _this.StartCount); }
	        else if (_this.State == 0) { alert("自动出价器功能已启动."); } //但还未到自动出价状态.
	        else if (_this.State == 1) { alert("自动出价器功能运行中,正在参于出价."); }

	        //显示配置信息
	        _this.ShowSetConfigToPage();

	    }
	    else {
	        AjaxLoginBoxClose();
	        AjaxLoginBox(); //alert("您还没有登陆,健身功能暂不能使用.");
	    }
	},
	this.Running = function (_Price, _EndStopTimeSecond) {

	    var _this = this;
	    if (_this.State <= -1) {
	        //已停止,不处理
	    }
	    else if (_this.State == 0) {
	        _this.StartRunning(_Price);
	    }
	    else if (_this.State == 1) {
	        //策略,核心组
	        _this.TacticRunning(_Price, _EndStopTimeSecond);
	    }
	    //window.status = "运行中...("+ _this.StopCount +").." + new Date().getTime();
	    //jQuery("#input_StartCount").val(_this.StartCount - _this.StopCount);
	    this.ShowSetConfigToPage2();
	},

	this.StartRunning = function (_Price) {//判断是否需要启动
	    var _this = this;
	    //0:price,1:time
	    if (_this.StartType == 0) { if (_Price >= _this.StartPrice) { _this.State = 1; } }
	    if (_this.StartType == 1) {
	        var t = new Date();
	        if (t >= _this.StartTime) {
	            _this.State = 1;
	        }
	    }
	},
	this.TacticRunning = function (_Price, _EndStopTimeSecond) {
	    //window.status +="|" + _EndStopTimeSecond + "秒";
	    var _this = this;
	    if (_this.State < 0) return;
	    //判断策略,判断停止
	    if (_this.TacticType == 0)//最后10秒
	    {
	        if (_EndStopTimeSecond <= 10) _this.Bid(_Price);
	    }
	    else if (_this.TacticType == 1)//1~30秒
	    {
	        var x = _this.GetRandom(1, 30);
	        if (_EndStopTimeSecond <= x) _this.Bid(_Price);
	    }

	},
	this.Stop = function () {
	    var _this = this;
	    _this.State = -1; //停止
	    _this.StartPrice = 0;
	    _this.StartType = 0;
	    _this.StartCount = 0;

	    _this.TacticType = 0;
	    _this.StopType = 0;
	    _this.StopPrice = 0;
	    _this.StopPriceStep = 0;
	    _this.StopCount = 0;
	    //alert("stop");
	    //alert("分身功能已取消.");
	},
	this.Bid = function (_Price) {
	    //alert("Robot="+_Price);
	    var _this = this;

	    if (_this.State == 1)// return true;
	    {
	        //添加分身标识
	        switch (_this.StopType) {
	            case 0: _this.VenudeBid(); break;
	            case 1: //次数到了,停
	                if ((_this.StartCount - _this.StopCount) > 0) {
	                    _this.VenudeBid();
	                    _this.StopCount++;

	                }
	                else {
	                    _this.State = -1;
	                }
	                break;
	            case 2: //价格到了,停

	                if (_this.StopPrice > _Price) {
	                    _this.VenudeBid();
	                    _this.StopCount++;
	                }
	                else {
	                    _this.State = -1;
	                }
	                break;
	            default: //手工停
	                {
	                    _this.VenudeBid();
	                    _this.StopCount++;
	                }
	                break;
	        }
	        //window.status = "robot=" + _Price;	
	    }
	    //window.status = "robotOther=" + _Price + "State=" + _this.State;
	},
	this.IsState = function () {
	    return (this.State == 1); //是否处于分身状态
	},
	this.VenudeBid = function () {
	    this.PaiPaiBid.Bid(this.VendueId, 1);
	},
	this.GetState = function () {
	    var _this = this;
	    var _return = "类型:" + _this.State + ";";

	    if (_this.State == 1) {
	        switch (_this.Type) {
	            case 0: _return += "分身功能启动中"; break;
	            case 1: //次
	                _return += "分身功能启动中...剩余次数:" + (_this.StartCount - _this.StopCount);
	                break;
	            case 2:
	                _return += "分身功能启动中...目标价位:" + _this.StopPrice;
	                break;
	            default: _return += "分身功能启动中(default)" + _this.StopType; break;
	        }
	    }
	    else {
	        _return += "分身功能已取消";
	    }
	    return _return;
	}
	,
	this.GetRandom = function (min, max) {
	    var x = Math.round(Math.random() * max);
	    if (x > min)
	        x = min + Math.round(Math.random() * (max - min));
	    return x;
	}
	, this.GetDateParse = function (_time) {
	    var y = 2011;
	    var M = 1;
	    var d = 1;
	    var h = 0;
	    var m = 0;
	    var s = 0;
	    //2011-02-03 10:10:10
	    if (_time.length > 4) y = parseInt(_time.substr(0, 4));
	    if (_time.length > 7) M = parseInt(_time.substr(5, 2));
	    if (_time.length > 10) d = parseInt(_time.substr(8, 2));

	    if (_time.length > 13) h = parseInt(_time.substr(11, 2));
	    if (_time.length > 16) m = parseInt(_time.substr(14, 2));
	    if (_time.length >= 19) s = parseInt(_time.substr(17, 2));

	    return new Date(y, M - 1, d, h, m, s);
	}
	, this.ShowSetConfigToPage = function () {
	    var _this = this;
	    switch (parseInt(_this.StartType, 10)) {
	        case 0: jQuery("#span_Span_StartInfo").html("到当前价位为" + _this.StartPrice + "元时启动."); break;
	        case 1:
	            jQuery("#span_Span_StartInfo").html("定时于" + _this.StartTimeStr + "时启动."); break;
	    }

	    switch (parseInt(_this.TacticType, 10)) {
	        case 0: jQuery("#span_Span_StartTactic").html("最后10秒出价."); break;
	        case 1: jQuery("#span_Span_StartTactic").html("最后1~30秒随机出价."); break;
	    }
	    switch (parseInt(_this.StopType, 10)) {
	        case 0: jQuery("#span_Span_StopInfo").html("手工停止自动出价."); break;
	        case 1: jQuery("#span_Span_StopInfo").html("剩余次数为0时停止出价,还剩" + (_this.StartCount - _this.StopCount) + "次."); break;
	        case 2: jQuery("#span_Span_StopInfo").html("到当前价位为" + _this.StopPrice + "元时停止出价."); break;
	    }
	}
	, this.ShowSetConfigToPage2 = function () {
	    var _this = this;
	    switch (parseInt(_this.StopType, 10)) {
	        case 0: jQuery("#span_Span_StopInfo").html("手工停止自动出价."); break;
	        case 1: jQuery("#span_Span_StopInfo").html("剩余次数为0时停止出价,还剩" + (_this.StartCount - _this.StopCount) + "次."); break;
	        case 2: jQuery("#span_Span_StopInfo").html("到当前价位为" + _this.StopPrice + "元时停止出价."); break;
	    }
	}

}



/**手机号验证，代码负责人wangl**/
function phoneVerification() {
    var urlPhone = '/user/PhoneVerification.aspx';
    window.open(urlPhone, '_self'); //新窗口打开参数, 'newwindow', 'height=100,width=400,top=0,left=0,toolbar=no,menubar=no,scrollbars=no, resizable=no,location=no, status=no'
}


function isAlertCharge(id) {
    $.ajax({
        //要用post方式      
        type: "Post",
        //方法所在页面和方法名      
        url: "/qyjx/WebServices.aspx/GetVendueType",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: "{'id':'" + id + "'}",
        success: function (data) {
            var type = data.d;
            if (type == 0) {
                AlertCharge();
            }
            else if (type == 1) {

            }
            else {

            }
        },
        error: function (err) {
        }
    });
}

var bankData = new Array(41);
bankData[0] = new Array("1", "中国工商银行");
bankData[1] = new Array("2", "中国农业银行");
bankData[2] = new Array("3", "中国建设银行");
bankData[3] = new Array("4", "招商银行");
bankData[4] = new Array("5", "中信银行");
bankData[5] = new Array("6", "交通银行");
bankData[6] = new Array("7", "广东发展银行");
bankData[7] = new Array("8", "上海浦东发展银行");
bankData[8] = new Array("9", "中国银行");
bankData[9] = new Array("10", "城市商业银行");
bankData[10] = new Array("11", "中国农业发展银行");
bankData[11] = new Array("12", "光大银行");
bankData[12] = new Array("13", "华夏银行");
bankData[13] = new Array("14", "中国民生银行");
bankData[14] = new Array("15", "深圳发展银行");
bankData[15] = new Array("16", "兴业银行");
bankData[16] = new Array("17", "农村信用合作社");
bankData[17] = new Array("18", "珠海南通银行");
bankData[18] = new Array("19", "宁波国际银行");
bankData[19] = new Array("20", "福建亚洲银行");
bankData[20] = new Array("21", "恒丰银行");
bankData[21] = new Array("22", "浙商银行");
bankData[22] = new Array("23", "农村商业银行");
bankData[23] = new Array("24", "城市信用合作社");
bankData[24] = new Array("25", "农村合作银");
bankData[25] = new Array("26", "浙江商业银行");
bankData[26] = new Array("27", "徽商银行");
bankData[27] = new Array("28", "渤海银行");
bankData[28] = new Array("29", "中国邮政储蓄银行");
bankData[29] = new Array("30", "北京银行");
bankData[30] = new Array("31", "平安银行股份有限公司");
bankData[31] = new Array("32", "南京银行");
bankData[32] = new Array("33", "江苏银行");
bankData[33] = new Array("34", "宁波银行");
bankData[34] = new Array("35", "上海银行");
bankData[35] = new Array("36", "杭州银行");
bankData[36] = new Array("37", "东莞农村商业银行");
bankData[37] = new Array("38", "东亚银行");
bankData[38] = new Array("39", "新韩银行");
bankData[39] = new Array("40", "青岛银行");
bankData[40] = new Array("41", "广州银行");

var china = [
//直辖市
 ['北京市'],
 ['上海市'],
 ['天津市'],
 ['重庆市'],
//华北地区
 ['河北省', '石家庄', '唐山', '秦皇岛', '邯郸', '邢台', '保定', '张家口', '承德', '沧州', '廊坊', '衡水'],
 ['山西省', '太原', '大同', '阳泉', '长治', '晋城', '朔州', '晋中', '运城', '忻州', '临汾', '吕梁'],
 ['内蒙古自治区', '呼和浩特', '包头', '乌海', '赤峰', '通辽', '鄂尔多斯', '呼伦贝尔', '巴彦淖尔', '乌兰察布', '兴安', '锡林郭勒', '阿拉善'],
//东北地区
 ['辽宁省', '沈阳', '大连', '鞍山', '抚顺', '本溪', '丹东', '锦州', '营口', '阜新', '辽阳', '盘锦', '铁岭', '朝阳', '葫芦岛'],
 ['吉林省', '长春', '吉林', '四平', '辽源', '通化', '白山', '松原', '白城', '延边'],
 ['黑龙江', '哈尔滨', '齐齐哈尔', '鸡西', '鹤岗', '双鸭山', '大庆', '伊春', '佳木斯', '七台河', '牡丹江', '黑河', '绥化', '大兴安岭'],
//华东地区
 ['江苏省', '南京', '无锡', '徐州', '常州', '苏州', '南通', '连云港', '淮安', '盐城', '扬州', '镇江', '泰州', '宿迁'],
 ['浙江省', '杭州', '宁波', '温州', '嘉兴', '湖州', '绍兴', '金华', '衢州', '舟山', '台州', '丽水'],
 ['安徽省', '合肥', '芜湖', '蚌埠', '淮南', '马鞍山', '淮北', '铜陵', '安庆', '黄山', '滁州', '阜阳', '宿州', '巢湖', '六安', '亳州', '池州', '宣城'],
 ['福建省', '福州', '厦门', '莆田', '三明', '泉州', '漳州', '南平', '龙岩', '宁德'],
 ['江西省', '南昌', '景德镇', '萍乡', '九江', '新余', '鹰潭', '赣州', '吉安', '宜春', '抚州', '上饶'],
 ['山东省', '济南', '青岛', '淄博', '枣庄', '东营', '烟台', '潍坊', '威海', '济宁', '泰安', '日照', '莱芜', '临沂', '德州', '聊城', '滨州', '菏泽'],
//中南地区
 ['河南省', '郑州', '开封', '洛阳', '平顶山', '焦作', '鹤壁', '新乡', '安阳', '濮阳', '许昌', '漯河', '三门峡', '南阳', '商丘', '信阳', '周口', '驻马店'],
 ['湖北省', '武汉', '黄石', '襄樊', '十堰', '荆州', '宜昌', '荆门', '鄂州', '孝感', '咸宁', '随州', '恩施'],
 ['湖南省', '长沙', '株洲', '湘潭', '衡阳', '邵阳', '岳阳', '常德', '张家界', '益阳', '郴州', '永州', '怀化', '娄底', '湘西'],
 ['广东省', '广州', '深圳', '珠海', '汕头', '韶关', '佛山', '江门', '湛江', '茂名', '肇庆', '惠州', '梅州', '汕尾', '河源', '阳江', '清远', '东莞', '中山', '潮州', '揭阳', '云浮'],
 ['广西自治区', '南宁', '柳州', '桂林', '梧州', '北海', '防城港', '钦州', '贵港', '玉林', '百色', '贺州', '河池', '来宾', '崇左'],
 ['海南省', '海口', '三亚'],
//西南地区
 ['四川省', '成都', '自贡', '攀枝花', '泸州', '德阳', '绵阳', '广元', '遂宁', '内江', '乐山', '南充', '宜宾', '广安', '达州', '眉山', '雅安', '巴中', '资阳', "阿坝", "甘孜", "凉山"],
 ['贵州省', '贵阳', "六盘水", "遵义", "安顺", "铜仁", "毕节", "黔西南", "黔东南", "黔南"],
 ['云南省', '昆明', '曲靖', '玉溪', "保山", "昭通", "丽江", "普洱", "临沧", "文山", "红河", "西双版纳", "楚雄", "大理", "德宏", "怒江", "迪庆"],
 ['西藏自治区', "拉萨", "昌都", "山南", "日喀则", "那曲", "阿里", "林芝"],
//西北地区
 ['陕西省', '西安', '铜川', '宝鸡', '咸阳', '渭南', '延安', '汉中', '榆林', '安康', '商洛'],
 ['甘肃省', "兰州", "嘉峪关", "金昌", "白银", "天水", "武威", "张掖", "平凉", "酒泉", "庆阳", "定西", "陇南", "临夏", "甘南"],
 ['青海省', "西宁", "海东", "海北", "黄南", "海南", "果洛", "玉树", "海西"],
 ['宁夏自治区', '银川', "石嘴山", "吴忠", "固原", "中卫"],
 ['新疆自治区', '乌鲁木齐', "克拉玛依", "吐鲁番", "哈密", "和田", "阿克苏", "喀什", "克孜勒苏柯尔克孜", "巴音郭楞蒙古", "昌吉", "博尔塔拉蒙古", "伊犁哈萨克", "塔城", "阿勒泰"],
//港澳台
 ['香港特别行政区'],
 ['澳门特别行政区'],
 ['台湾省', "台北", "高雄", "基隆", "台中", "台南", "新竹", "嘉义"]
 ];

function BindProvince() {
    var opt0 = "省份";
    var ProvinceCount = china.length;
    var ddlProvince = document.getElementById("ddlProvince");
    ddlProvince.innerHTML = "";
    ddlProvince.options[0] = new Option(opt0, "");
    for (var i = 0; i < ProvinceCount; i++) {
        ddlProvince.options[i + 1] = new Option(china[i][0], china[i][0]);
    }
}

function BindCity(City) {
    var opt0 = "省份";
    var ProvinceCount = china.length;
    var ddlProvince = document.getElementById("ddlProvince");
    ddlProvince.innerHTML = "";
    ddlProvince.options[0] = new Option(opt0, "");

    var opt0City = "城市";
    var ddlCity = document.getElementById("ddlCity");
    ddlCity.innerHTML = "";
    ddlCity.options[0] = new Option(opt0City, "");

    var flag = false;
    var chose = true;
    var selectProvinceIndex = 0;
    for (var i = 0; i < ProvinceCount; i++) {
        if (!flag) {
            var cityCount = china[i].length;
            for (var j = 1; j < cityCount; j++) {
                if (china[i][j] == City) {
                    flag = true;
                    selectProvinceIndex = i;
                    break;
                }
            }
        }
        ddlProvince.options[i + 1] = new Option(china[i][0], china[i][0]);
        if (flag && chose) {
            ddlProvince.options[i + 1].selected = true;
            chose = false;
        }
    }
    var cityCount = china[selectProvinceIndex].length;
    for (var i = 0; i < cityCount; i++) {
        if (cityCount == 1 && i == 0) {
            ddlCity.options[i + 1] = new Option(china[selectProvinceIndex][i], china[selectProvinceIndex][i]);
            i = 1;
        }
        else if (cityCount > 1 && i == 0) {
            i = 1;
            ddlCity.options[i] = new Option(china[selectProvinceIndex][i], china[selectProvinceIndex][i]);
        }
        else {
            ddlCity.options[i] = new Option(china[selectProvinceIndex][i], china[selectProvinceIndex][i]);
        }
        if (china[selectProvinceIndex][i] == City) {
            ddlCity.options[i].selected = true;
        }


    }

}

function selectMoreCity(sbj) {
    var opt0 = "城市";
    if (sbj.selectedIndex == 0) {
        var ddlCity = document.getElementById("ddlCity");
        ddlCity.innerHTML = "";
        ddlCity.options[0] = new Option(opt0, "");
        return;
    }

    var selectProvince = sbj.options[sbj.selectedIndex].value;
    var ProvinceCount = china.length;
    for (var i = 0; i < ProvinceCount; i++) {
        if (china[i][0] == selectProvince) {
            var cityCount = china[i].length;
            var ddlCity = document.getElementById("ddlCity");
            ddlCity.innerHTML = "";
            ddlCity.options[0] = new Option(opt0, "");
            for (var j = 0; j < cityCount; j++) {
                if (cityCount == 1 && j == 0) {
                    ddlCity.options[j + 1] = new Option(china[i][j], china[i][j]);
                    j = 1;
                }
                else if (cityCount > 1 && j == 0) {
                    j = 1;
                    ddlCity.options[j] = new Option(china[i][j], china[i][j]);
                }
                else {
                    ddlCity.options[j] = new Option(china[i][j], china[i][j]);
                }
                if (j == 1) {
                    ddlCity.options[1].selected = true;
                }
            }
            break;
        }
    }
}
