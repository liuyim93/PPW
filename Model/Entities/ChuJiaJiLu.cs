using System;
using System.Collections.Generic;

namespace Model.Entities
{
	public class ChuJiaJiLu
	{
		public string ChuJiaJiLuID { get; set; }
		public string ProductID { get; set; }
		public string HuiYuanID { get; set; }
		public int Status { get; set; }
		public virtual HuiYuan HuiYuan { get; set; }
		public virtual Product Product { get; set; }
        public DateTime AcutionTime { get; set; }//����ʱ��
        public decimal Price { get; set; }//��Ʒ��ǰ�۸�
        public string IPAdress { get; set; }//IP��ַ
        public int AcutionPoint { get; set; }//ʹ�õ��ĵ���
        public int FreePoint { get; set; }//ʹ�õķ�����
	}
}

