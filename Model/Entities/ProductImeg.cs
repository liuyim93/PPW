using System;
using System.Collections.Generic;

namespace Model.Entities
{
	public class ProductImeg
	{
		public string ProductImegId { get; set; }
		public string ProductID { get; set; }
		public string img { get; set; }
		public int xh { get; set; }
		public virtual Product Product { get; set; }
	}
}

