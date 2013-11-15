using System;
using System.Collections.Generic;

namespace Model.Entities
{
	public class DingDan
	{
		public string DingDanID { get; set; }
		public string DingDanBH { get; set; }
		public string HuiYuanID { get; set; }
		public string ProductID { get; set; }
		public Nullable<System.DateTime> DingDanTime { get; set; }
		public int Status { get; set; }
		public virtual HuiYuan HuiYuan { get; set; }
		public virtual Product Product { get; set; }
        //��������
        public string OrderTypeID { get; set; }
        //��Ʒ�۸�
        public decimal ProductPrice { get; set; }
        //������
        public decimal Fee { get; set; }
        //�˷�
        public decimal ShipFee { get; set; }
        //�����ܽ��
        public decimal TotalPrice { get; set; }
        //�ջ���ַ
        public string ShouHuoDZID { get; set; }
        //�����ֹ����
        public DateTime InvalidTime { get; set; }
        //����ID
        public string AuctionID { get; set; }
        //��Ա��
        public string HuiYuanName { get; set; }
        //�ջ���
        public string ShouHuoName { get; set; }
        //��ϵ�绰
        public string Mode { get; set; }
        //��ַ
        public string DZ { get; set; }
        //�ʱ�
        public string YouBian { get; set; }
        //��ע
        public string Remark { get; set; }
        //��Ʒ����
        public string ProductName { get; set; }
        //��������
        public string OrderType { get; set; }
	}
}

