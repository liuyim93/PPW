using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tools
{
    //订单
    public class DinDans
    {
       public string DingDanID { get; set; }
       public string DingDanBH { get; set; }
       public string HuiYuanID { get; set; }
       public string ProductID { get; set; }
       public DateTime? DingDanTime { get; set; }
       public int Status { get; set; }
       public string ShouHuoName { get; set; }
       public string Mode { get; set; }
       public string DZ { get; set; }
       public string YouBian { get; set; }
       public string OrderTypeID { get; set; }
       public string ShouHuoDZID { get; set; }
       public decimal TotalPrice { get; set; }
    }

    [Serializable]
    public class EditQuanxXian
    {
        public string LX { get; set; }
        public string Id { get; set; }
        public DateTime LastTime { get; set; }
    }
}
