using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Entities
{
    public class auction
    {
        public string AuctionID { get; set; }
        public string ProductID { get; set; }
        public int Coding {get;set; }
        public string HuiYuanID { get; set; }
        public decimal AuctionPrice { get; set; }
        public DateTime AuctionTime { get; set; }
        public int TimePoint { get; set; }
        public DateTime CreateTime { get; set; }
        public int Status { get; set; }
        public decimal PriceAdd { get; set; }
        public int AuctionPoint { get; set; }
        public int FreePoint { get; set; }
        public Nullable<DateTime> EndTime { get; set; }
        public string AuctionTypeID { get; set; }
        public int IsRecommend { get; set; }

    }
}
