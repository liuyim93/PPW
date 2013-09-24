using System;
using System.Collections.Generic;

namespace Model.Entities
{
	public class QuanXian
	{
		public string Id { get; set; }
		public int CDId { get; set; }
		public string HYID { get; set; }
		public string JSID { get; set; }
		public string GNDID { get; set; }
		public virtual CaiDan CaiDan { get; set; }
		public virtual GongNengDian GongNengDian { get; set; }
		public virtual JueSe JueSe { get; set; }
		public virtual YongHu YongHu { get; set; }
	}
}

