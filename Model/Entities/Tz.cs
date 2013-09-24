using System;
using System.Collections.Generic;

namespace Model.Entities
{
	public class Tz
	{
	    public Tz()
		{
			this.GtTzs = new List<GtTz>();
		}

		public string TzID { get; set; }
		public string Tile { get; set; }
		public string Creatr { get; set; }
		public string HuiYuanID { get; set; }
		public string Ip { get; set; }
		public Nullable<System.DateTime> CreateTime { get; set; }
		public virtual ICollection<GtTz> GtTzs { get; set; }
		public virtual HuiYuan HuiYuan { get; set; }
	}
}

