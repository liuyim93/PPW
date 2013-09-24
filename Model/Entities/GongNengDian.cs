using System;
using System.Collections.Generic;

namespace Model.Entities
{
	public class GongNengDian
	{
	    public GongNengDian()
		{
			this.CaiDan_GongNengDian = new List<CaiDan_GongNengDian>();
			this.QuanXians = new List<QuanXian>();
		}

		public string Id { get; set; }
		public string Tag { get; set; }
		public string name { get; set; }
		public virtual ICollection<CaiDan_GongNengDian> CaiDan_GongNengDian { get; set; }
		public virtual ICollection<QuanXian> QuanXians { get; set; }
	}
}

