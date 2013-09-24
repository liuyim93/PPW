using System;
using System.Collections.Generic;

namespace Model.Entities
{
	public class ErrorLog
	{
		public string id { get; set; }
		public string uid { get; set; }
		public Nullable<System.DateTime> logTime { get; set; }
		public string ip { get; set; }
		public string url { get; set; }
		public string errorDetails { get; set; }
		public string remark { get; set; }
	}
}

