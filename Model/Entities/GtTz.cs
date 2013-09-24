using System;
using System.Collections.Generic;

namespace Model.Entities
{
	public class GtTz
	{
	    public GtTz()
		{
			this.HfTzs = new List<HfTz>();
		}

		public string GtTzID { get; set; }
		public string TzID { get; set; }
		public string HuiYuanID { get; set; }
		public string Ip { get; set; }
		public string Contents { get; set; }
		public Nullable<System.DateTime> CreateTime { get; set; }
		public virtual HuiYuan HuiYuan { get; set; }
		public virtual Tz Tz { get; set; }
		public virtual ICollection<HfTz> HfTzs { get; set; }
	}
}

