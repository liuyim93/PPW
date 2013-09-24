using System;
using System.Collections.Generic;

namespace Model.Entities
{
	public class GgType
	{
	    public GgType()
		{
			this.Ggs = new List<Gg>();
		}
		public string GgTypeID { get; set; }
		public string TypeName { get; set; }
		public virtual ICollection<Gg> Ggs { get; set; }
	}
}

