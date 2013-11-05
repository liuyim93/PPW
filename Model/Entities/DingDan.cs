using System;
using System.Collections.Generic;

namespace Model.Entities
{
	public class DingDan
	{
		public string DingDanID { get; set; }
		public string DingDanBH { get; set; }
		public string HuiYuanID { get; set; }
		public string ProductID { get; set; }
		public Nullable<System.DateTime> DingDanTime { get; set; }
		public int Status { get; set; }
		public virtual HuiYuan HuiYuan { get; set; }
		public virtual Product Product { get; set; }
        //订单类型
        public string OrderTypeID { get; set; }
        //拍品价格
        public decimal ProductPrice { get; set; }
        //手续费
        public decimal Fee { get; set; }
        //运费
        public decimal ShipFee { get; set; }
        //订单总金额
        public decimal TotalPrice { get; set; }
        //收货地址
        public string ShouHuoDZID { get; set; }
        //付款截止日期
        public DateTime InvalidTime { get; set; }
        //竞拍ID
        public string AuctionID { get; set; }
	}
}

