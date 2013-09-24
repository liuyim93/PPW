using System;
using System.Collections.Generic;

namespace Model.Entities
{
	public class Gg
	{
		public string GgId { get; set; }
		public string Tile { get; set; }
		public string Contents { get; set; }
		public string GgTypeID { get; set; }
		public string RenYuanId { get; set; }
		public Nullable<System.DateTime> CreatTime { get; set; }
		public virtual GgType GgType { get; set; }
		public virtual RenYuan RenYuan { get; set; }
	}
}

