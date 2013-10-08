using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Entities
{
    public class AuctionType
    {
        //竞拍类型ID
        public string AuctionTypeID { get; set; }
        //类型名：常规竞拍、免费竞拍
        public string TypeName { get; set; }
    }
}
