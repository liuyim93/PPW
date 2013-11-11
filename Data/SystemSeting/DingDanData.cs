using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tools;
using DAL;
using System.Data;
using Model.Entities;
using System.Data.SqlClient;

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

       /// <summary>
       /// 生成订单
       /// </summary>
       /// <param name="dd"></param>
       /// <returns></returns>
       public int AddOrder(DingDan dd)
       {
           string sql = "insert into DingDan (DingDanBH,HuiYuanID,ProductID,Status,OrderTypeID,ProductPrice,Fee,ShipFee,TotalPrice,DingDanTime,InvalidTime,AuctionID,ShouHuoDZID)values(@DingDanBH,@HuiYuanID,@ProductID,@Status,@OrderTypeID,@ProductPrice,@Fee,@ShipFee,@TotalPrice,@DingDanTime,@InvalidTime,@AuctionID,@ShouHuoDZID)";
           return BLLdat.ExecuteNonQuery(null,CommandType.Text,sql,new SqlParameter("@DingDanBH",dd.DingDanBH),
               new SqlParameter("@HuiYuanID",dd.HuiYuanID),
               new SqlParameter("@ProductID",dd.ProductID),
               new SqlParameter("@Status",dd.Status),
               new SqlParameter("@OrderTypeID",dd.OrderTypeID),
               new SqlParameter("@ProductPrice",dd.ProductPrice),
               new SqlParameter("@Fee",dd.Fee),
               new SqlParameter("@ShipFee",dd.ShipFee),
               new SqlParameter("@TotalPrice",dd.TotalPrice),
               new SqlParameter("@DingDanTime",DateTime.Now),
               new SqlParameter("@InvalidTime",dd.InvalidTime),
               new SqlParameter("@AuctionID",dd.AuctionID),
               new SqlParameter("@ShouHuoDZID",dd.ShouHuoDZID));
       }

       /// <summary>
       /// 查询订单类型
       /// </summary>
       /// <param name="sql"></param>
       /// <returns></returns>
       public OrderType GetOrderType(string sql) 
       {
           DataSet ds = BLLdat.GetDataSet(sql);
           if (ds.Tables[0].Rows.Count > 0)
           {
               OrderType type = new OrderType();
               foreach (DataRow item in ds.Tables[0].Rows)
               {
                   type.OrderTypeID = item["OrderTypeID"].ToString();
                   type.TypeName = item["TypeName"].ToString();
               }
               return type;
           }
           else 
           {
               return null;
           }                      
       }

       /// <summary>
       /// 查询订单
       /// </summary>
       /// <param name="sql"></param>
       /// <returns></returns>
       public List<DingDan> GetDingDan(string sql) 
       {
           List<DingDan> list = new List<DingDan>();
           DataSet ds = BLLdat.GetDataSet(sql);
           if (ds.Tables[0].Rows.Count>0)
           {               
               foreach (DataRow row in ds.Tables[0].Rows)
               {
                   DingDan order = new DingDan();
                   order.DingDanID = row["DingDanID"].ToString();
                   order.DingDanBH = row["DingDanBH"].ToString();
                   order.HuiYuanID = row["HuiYuanID"].ToString();
                   order.ProductID = row["ProductID"].ToString();
                   order.OrderTypeID = row["OrderTypeID"].ToString();
                   order.ProductPrice=Convert.ToDecimal(row["ProductPrice"]);
                   order.Fee=Convert.ToDecimal(row["Fee"]);
                   order.ShipFee=Convert.ToDecimal(row["ShipFee"]);
                   order.ShouHuoDZID=row["ShouHuoDZID"]==DBNull.Value?"":row["ShouHuoDZID"].ToString();
                   order.Status=Convert.ToInt32(row["Status"]);
                   order.TotalPrice=Convert.ToDecimal(row["TotalPrice"]);
                   order.DingDanTime=Convert.ToDateTime(row["DingDanTime"]);
                   order.InvalidTime=Convert.ToDateTime(row["InvalidTime"]);
                   order.AuctionID=row["AuctionID"].ToString();
                   list.Add(order);
               }
           }
           return list;
       }
    }
}
