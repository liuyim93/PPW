using System;
using System.Collections.Generic;

namespace Model.Entities
{
	public class PaiDianJl
	{
		public string PaiDianJlId { get; set; }
		public int ChongZhi { get; set; }
		public string HuiYuanID { get; set; }
		public Nullable<System.DateTime> CreateTime { get; set; }
		public virtual HuiYuan HuiYuan { get; set; }
	}
}

