using System;
using System.Collections.Generic;

namespace Model.Entities
{
	public class HfTz
	{
		public string HfTzId { get; set; }
		public string GtTzID { get; set; }
		public string HuiYuanID { get; set; }
		public string Ip { get; set; }
		public string creats { get; set; }
		public Nullable<System.DateTime> CreateTime { get; set; }
		public virtual GtTz GtTz { get; set; }
		public virtual HuiYuan HuiYuan { get; set; }
	}
}

