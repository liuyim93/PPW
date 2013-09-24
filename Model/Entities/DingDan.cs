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
	}
}

