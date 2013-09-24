using System;
using System.Collections.Generic;

namespace Model.Entities
{
	public class RenYuan
	{
	    public RenYuan()
		{
			this.Ggs = new List<Gg>();
			this.YongHus = new List<YongHu>();
		}

		public string RenYuanId { get; set; }
		public string PersonName { get; set; }
		public string Sex { get; set; }
		public string ZhiWei { get; set; }
		public string SFZ { get; set; }
		public string Email { get; set; }
		public string Mobile { get; set; }
		public string QQ { get; set; }
		public string Remark { get; set; }
		public virtual ICollection<Gg> Ggs { get; set; }
		public virtual ICollection<YongHu> YongHus { get; set; }
	}
}

