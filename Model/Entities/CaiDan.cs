using System;
using System.Collections.Generic;

namespace Model.Entities
{
	public class CaiDan
	{
	    public CaiDan()
		{
			this.CaiDan_GongNengDian = new List<CaiDan_GongNengDian>();
			this.QuanXians = new List<QuanXian>();
		}

		public int Id { get; set; }
		public int Parent_Id { get; set; }
		public string Name { get; set; }
		public string Url { get; set; }
		public int Is_Show { get; set; }
		public int OrderNum { get; set; }
		public virtual ICollection<CaiDan_GongNengDian> CaiDan_GongNengDian { get; set; }
		public virtual ICollection<QuanXian> QuanXians { get; set; }
	}
}

