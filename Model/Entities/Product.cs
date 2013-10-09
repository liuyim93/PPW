using System;
using System.Collections.Generic;

namespace Model.Entities
{
	public class Product
	{
	    public Product()
		{
			this.ChuJiaJiLus = new List<ChuJiaJiLu>();
			this.DingDans = new List<DingDan>();
			this.ProductImegs = new List<ProductImeg>();
		}

		public string ProductID { get; set; }
		public string coding { get; set; }
		public string ProductTypeID { get; set; }
		public string productName { get; set; }
		public string productBrand { get; set; }
		public int isshouYei { get; set; }
		public Nullable<decimal> productPrice { get; set; }
		public Nullable<decimal> PmJGproduct { get; set; }
		public Nullable<decimal> ChenJiaoJiaGe { get; set; }
		public string HuiYuanID { get; set; }
		public Nullable<System.DateTime> AuctionTime { get; set; }
		public Nullable<int> TimePoint { get; set; }
		public Nullable<System.DateTime> CreateTime { get; set; }
		public string ProductDetails { get; set; }
		public int Status { get; set; }
		public virtual ICollection<ChuJiaJiLu> ChuJiaJiLus { get; set; }
		public virtual ICollection<DingDan> DingDans { get; set; }
		public virtual HuiYuan HuiYuan { get; set; }
		public virtual ProductType ProductType { get; set; }
		public virtual ICollection<ProductImeg> ProductImegs { get; set; }
        public Nullable<decimal> PriceAdd { get; set; }//价格涨幅
        public int AuctionPoint { get; set; }//消耗的拍点数
        public decimal Fee { get; set; }//手续费
        public decimal ShipFee { get; set; }//运费
        public Nullable<System.DateTime> EndTime { get; set; }//竞拍结束时间
        public string Intro { get; set; }//产品简介
        public int FreePoint { get; set; }//每次竞价需要的返点数
        public string AuctionTypeID { get; set; }//竞拍类型
	}
}

