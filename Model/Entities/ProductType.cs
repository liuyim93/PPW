using System;
using System.Collections.Generic;

namespace Model.Entities
{
	public class ProductType
	{
	    public ProductType()
		{
			this.Products = new List<Product>();
		}

		public string ProductTypeID { get; set; }
		public string Fid { get; set; }
		public string TypeName { get; set; }
		public string remark { get; set; }
		public virtual ICollection<Product> Products { get; set; }
	}
}

