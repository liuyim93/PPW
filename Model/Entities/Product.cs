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
		public string ProductTypeID { get; set; }
		public string productName { get; set; }
		public string productBrand { get; set; }
		public int isshouYei { get; set; }
		public Nullable<decimal> productPrice { get; set; }
		public Nullable<System.DateTime> CreateTime { get; set; }
		public string ProductDetails { get; set; }
		public virtual ICollection<ChuJiaJiLu> ChuJiaJiLus { get; set; }
		public virtual ICollection<DingDan> DingDans { get; set; }
		public virtual ProductType ProductType { get; set; }
		public virtual ICollection<ProductImeg> ProductImegs { get; set; }
        public decimal Fee { get; set; }//手续费
        public decimal ShipFee { get; set; }//运费
        public string Intro { get; set; }//产品简介
	}
}

