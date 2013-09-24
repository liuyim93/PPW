using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tools;
using Data.SystemSeting;

namespace BLL.SystemSeting
{
    //订单
   public class DingDanBll
    {
       DingDanData ddDat = new DingDanData();
       /// <summary>
       /// 跟据条件查找订单
       /// </summary>
       /// <param name="bh"></param>
       /// <param name="beg"></param>
       /// <param name="end"></param>
       /// <param name="zt"></param>
       /// <returns></returns>
       public List<DinDans> GetDinDan(string bh,DateTime?beg,DateTime? end,string zt)
       {
           string sql = "select DingDanID,DingDanBH,TypeName,HuiYuanName,productName,DingDanTime,dd.Status,ShouHuoName,Mode,DZ,YouBian from DingDan as dd,ShouHuoDZ as dz,HuiYuan as hy, Product as cp,OrderType as ot where dz.ShouHuoDZID=dd.ShouHuoDZID and hy.HuiYuanID=dd.HuiYuanID and cp.ProductID=dd.ProductID and ot.OrderTypeID=dd.OrderTypeID";
           if (bh!="")
           {
               sql += " and dd.DingDanBH='"+bh+"'";
           }
           if (beg!=null)
           {
               string begs = beg.Value.ToString("yyyy-MM-dd");
               sql += " and dd.DingDanTime>='"+begs+"'";
           }
           if (end!=null)
           {
               string ends = end.Value.ToString("yyyy-MM-dd");
               sql += " and dd.DingDanTime<='"+ends+"'";
           }
           if (zt!=""&&zt!=null)
           {
               sql += " and dd.Status="+zt;
           }
           sql += " order by dd.DingDanTime desc";
           return ddDat.GetDinDans(sql);
       }


       /// <summary>
       /// 修改订单状态
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
       public int Eid(string id,string st) 
       {
           string sql = "update DingDan set Status="+st+" where DingDanID='"+id+"'";
           return ddDat.Setsql(sql);
       }
    }
}
