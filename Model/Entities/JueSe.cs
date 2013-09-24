using System;
using System.Collections.Generic;

namespace Model.Entities
{
	public class JueSe
	{
	    public JueSe()
		{
			this.QuanXians = new List<QuanXian>();
			this.YongHus = new List<YongHu>();
		}

		public string JueSeId { get; set; }
		public string JSMC { get; set; }
		public string Remark { get; set; }
		public int status { get; set; }
		public virtual ICollection<QuanXian> QuanXians { get; set; }
		public virtual ICollection<YongHu> YongHus { get; set; }
	}
}

