using System;
using System.Collections.Generic;

namespace Model.Entities
{
	public class CaiDan_GongNengDian
	{
		public string Id { get; set; }
		public int CDID { get; set; }
		public string GNID { get; set; }
		public virtual CaiDan CaiDan { get; set; }
		public virtual GongNengDian GongNengDian { get; set; }
	}
}

