using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tools;
using DAL;
using System.Data;

namespace Data.SystemSeting
{
    //订单
   public class DingDanData
    {
       SQLHelper BLLdat = new SQLHelper();

       public List<DinDans> GetDinDans(string sql) 
       {
           DataSet dat = BLLdat.GetDataSet(sql);
           List<DinDans> list = new List<DinDans>();
           if (dat.Tables.Count>0)
           {
               foreach (DataRow item in dat.Tables[0].Rows)
               {
                   DinDans ds = new DinDans();
                   ds.DingDanID = item["DingDanID"].ToString();
                   ds.DingDanBH = item["DingDanBH"].ToString();
                   ds.HuiYuanID = item["HuiYuanName"].ToString();
                   ds.ProductID = item["productName"].ToString();
                   ds.DingDanTime = item["DingDanTime"] == DBNull.Value ? null : item["DingDanTime"] as DateTime?;
                   ds.Status =Convert.ToInt32(item["Status"]);
                   ds.ShouHuoName = item["ShouHuoName"].ToString();
                   ds.Mode = item["Mode"].ToString();
                   ds.DZ = item["DZ"].ToString();
                   ds.YouBian = item["YouBian"].ToString();
                   ds.OrderTypeID=item["TypeName"].ToString();
                   list.Add(ds);
               }
           }
           return list;
       }

       /// <summary>
       /// 执行增删改操作
       /// </summary>
       /// <param name="sql"></param>
       /// <returns></returns>
       public int Setsql(string sql) 
       {
           return BLLdat.RunSQL(sql);
       }
    }
}
