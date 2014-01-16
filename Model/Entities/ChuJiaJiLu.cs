using System;
using System.Collections.Generic;

namespace Model.Entities
{
	public class ChuJiaJiLu
	{
		public string ChuJiaJiLuID { get; set; }
		public string AuctionID { get; set; }
		public string HuiYuanID { get; set; }
		public int Status { get; set; }
		public virtual HuiYuan HuiYuan { get; set; }
		public virtual Product Product { get; set; }
        public DateTime AuctionTime { get; set; }//出价时间
        public decimal Price { get; set; }//产品当前价格
        public string IPAdress { get; set; }//IP地址
        public int AuctionPoint { get; set; }//使用的拍点数
        public int FreePoint { get; set; }//使用的返点数
        public string HuiYuanName { get; set; }//会员名
        public string sjh { get; set; }//手机号
	}
}

