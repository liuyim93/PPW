using System;
using System.Collections.Generic;

namespace Model.Entities
{
	public class HuiYuan
	{
	    public HuiYuan()
		{
			this.ChuJiaJiLus = new List<ChuJiaJiLu>();
			this.DingDans = new List<DingDan>();
			this.GtTzs = new List<GtTz>();
			this.HfTzs = new List<HfTz>();
			this.PaiDianJls = new List<PaiDianJl>();
			this.Products = new List<Product>();
			this.ShouHuoDZs = new List<ShouHuoDZ>();
			this.Tzs = new List<Tz>();
		}

		public string HuiYuanID { get; set; }
		public string HuiYuanName { get; set; }
		public string MM { get; set; }
		public string email { get; set; }
		public string prName { get; set; }
		public string sex { get; set; }
		public string sfz { get; set; }
		public string sjh { get; set; }
		public Nullable<int> PaiDian { get; set; }
		public Nullable<int> JiFen { get; set; }
		public string DJ { get; set; }
		public Nullable<System.DateTime> CreatTime { get; set; }
		public Nullable<System.DateTime> LoginTime { get; set; }
		public virtual ICollection<ChuJiaJiLu> ChuJiaJiLus { get; set; }
		public virtual ICollection<DingDan> DingDans { get; set; }
		public virtual ICollection<GtTz> GtTzs { get; set; }
		public virtual ICollection<HfTz> HfTzs { get; set; }
		public virtual ICollection<PaiDianJl> PaiDianJls { get; set; }
		public virtual ICollection<Product> Products { get; set; }
		public virtual ICollection<ShouHuoDZ> ShouHuoDZs { get; set; }
		public virtual ICollection<Tz> Tzs { get; set; }
        public Nullable<int> FreePoint { get; set; }//������
        public Nullable<int> ContinueLogins { get; set; }//������¼����
        public Nullable<int> IsEmailVerify { get; set; }//�����Ƿ���֤
        public Nullable<int> IsPhoneVerify { get; set; }//�ֻ������Ƿ���֤
        public string Adress { get; set; }//��ַ
        public int Points { get; set; }//���û���
	}
}

