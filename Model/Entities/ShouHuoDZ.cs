using System;
using System.Collections.Generic;

namespace Model.Entities
{
	public class ShouHuoDZ
	{
		public string ShouHuoDZID { get; set; }
		public string HuiYuanID { get; set; }
		public string ShouHuoName { get; set; }
		public string Mode { get; set; }
		public string DZ { get; set; }
		public string YouBian { get; set; }
		public virtual HuiYuan HuiYuan { get; set; }
	}
}

