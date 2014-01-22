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
                   ds.TotalPrice = Convert.ToDecimal(item["TotalPrice"]);
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
       public List<OrderType> GetOrderType(string sql) 
       {
           List<OrderType> list = new List<OrderType>();
           DataSet ds = BLLdat.GetDataSet(sql);
           if (ds.Tables[0].Rows.Count > 0)
           {               
               foreach (DataRow item in ds.Tables[0].Rows)
               {
                   OrderType type = new OrderType();
                   type.OrderTypeID = item["OrderTypeID"].ToString();
                   type.TypeName = item["TypeName"].ToString();
                   list.Add(type);
               }
               return list;
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

       /// <summary>
       /// 根据订单编号、下单时间、状态查询订单信息
       /// </summary>
       /// <param name="orderNum"></param>
       /// <param name="start"></param>
       /// <param name="end"></param>
       /// <param name="status"></param>
       /// <returns></returns>
       public List<DingDan> GetOrder(string orderNum,DateTime?start,DateTime?end,string status,string orderTypeId) 
       {
           string sql = "select dd.*,hh.HuiYuanName,pro.ProductName,ot.TypeName from DingDan as dd,HuiYuan as hh,Product as pro,OrderType as ot where hh.HuiYuanID=dd.HuiYuanID and pro.ProductID=dd.ProductID and ot.OrderTypeID=dd.OrderTypeID and dd.DingDanBH like '%"+orderNum+"%'";
           if (start != null)
           {
               string begin = start.Value.ToString("yyyy-MM-dd");
               sql += "and DingDanTime>'" + begin + "'";
           }
           else 
           {
               if (end != null)
               {
                   string maxTime = end.Value.ToString("yyyy-MM-dd");
                   sql += "and DingDanTime<'" + end + "'";
               }
               else 
               {
                   if (orderTypeId!=null)
                   {
                       sql += "and dd.OrderTypeID='" + orderTypeId + "'";
                   }
                   else 
                   {
                       if (status != null)
                       {
                           sql += "and Status=" + status;
                       }
                   }                   
               }
           }
           DataSet ds = BLLdat.GetDataSet(sql);
           List<DingDan> list = new List<DingDan>();
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
                   order.ProductPrice = Convert.ToDecimal(row["ProductPrice"]);
                   order.Fee = Convert.ToDecimal(row["Fee"]);
                   order.ShipFee = Convert.ToDecimal(row["ShipFee"]);
                   order.ShouHuoDZID = row["ShouHuoDZID"] ==null ? "" : row["ShouHuoDZID"].ToString();
                   order.Status = Convert.ToInt32(row["Status"]);
                   order.TotalPrice = Convert.ToDecimal(row["TotalPrice"]);
                   order.DingDanTime = Convert.ToDateTime(row["DingDanTime"]);
                   order.InvalidTime = Convert.ToDateTime(row["InvalidTime"]);
                   order.AuctionID = row["AuctionID"].ToString();
                   order.HuiYuanName=row["HuiYuanName"].ToString();
                   order.ProductName=row["ProductName"].ToString();
                   order.OrderType=row["TypeName"].ToString();
                   list.Add(order);
               }
           }
           return list;
       }

       /// <summary>
       /// 根据会员ID和状态查询订单
       /// </summary>
       /// <param name="hyId">会员ID</param>
       /// <param name="status">状态</param>
       /// <returns></returns>
       public DataTable getDingDanbyhyId(string hyId,int status,string orderType) {
           string sql = "select * from DingDan where HuiYuanID='"+hyId+"' and Status="+status+" and OrderTypeID=(select OrderTypeID from OrderType where TypeName='"+orderType+"') order by DingDanTime desc";
           DataSet ds = BLLdat.GetDataSet(sql);
           if (ds.Tables.Count > 0)
           {
               return ds.Tables[0];
           }
           else {
               return null;
           }
       }

       /// <summary>
       /// 根据会员id和订单类型查询订单
       /// </summary>
       /// <param name="hyId">会员ID</param>
       /// <param name="orderType">订单类型</param>
       /// <returns></returns>
       public DataTable getDingDanbyType(string hyId,string orderType) {
           string sql = "select * from DingDan where HuiYuanID='" + hyId + "' and OrderTypeID=(select OrderTypeID from OrderType where TypeName='" + orderType + "') order by DingDanTime desc";
           DataSet ds = BLLdat.GetDataSet(sql);
           if (ds.Tables.Count > 0)
           {
               return ds.Tables[0];
           }
           else {
               return null;
           }
       }
    }
}
