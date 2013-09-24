using System;
using System.Collections.Generic;

namespace Model.Entities
{
	public class YongHu
	{
	    public YongHu()
		{
			this.QuanXians = new List<QuanXian>();
		}

		public string YongHuId { get; set; }
		public string RenYuanId { get; set; }
		public string YHM { get; set; }
		public string MM { get; set; }
		public string JSID { get; set; }
		public string BZ { get; set; }
		public int status { get; set; }
		public virtual JueSe JueSe { get; set; }
		public virtual ICollection<QuanXian> QuanXians { get; set; }
		public virtual RenYuan RenYuan { get; set; }
	}
}

