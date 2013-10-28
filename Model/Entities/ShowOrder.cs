using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Entities
{
   public class ShowOrder
    {
       public string ShowOrderID { get; set; }
       public string OrderID { get; set; }
       public  Nullable<int> Points { get; set; }
       public string Title { get; set; }
       public string Detail { get; set; }
       public string Reply { get; set; }
       public DateTime LoadTime { get; set; }
       public int IsCheck { get; set; }
       public int IsRead { get; set; }
       public int IsShow { get; set; }
       public ShowOrderImg showOrderImg { get; set; }
       public string ImgUrl { get; set; }
    }
}
