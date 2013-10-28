using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tools;
using Data.SystemSeting;
using Model.Entities;

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

       /// <summary>
       /// 生成订单
       /// </summary>
       /// <param name="dd"></param>
       /// <returns></returns>
       public int AddOrder(DingDan dd) 
       {
           return ddDat.AddOrder(dd);
       }

       /// <summary>
       /// 根据类型名查询订单类型ID
       /// </summary>
       /// <param name="typeName"></param>
       /// <returns></returns>
       public OrderType GetbyName(string typeName) 
       {
           string sql = "select * from OrderType where TypeName='"+typeName+"'";
           return ddDat.GetOrderType(sql);
       }

       /// <summary>
       /// 根据ID查询订单类型名
       /// </summary>
       /// <param name="typeId"></param>
       /// <returns></returns>
       public OrderType GetbyID(string typeId) 
       {
           string sql = "select * from OrderType where OrderTypeID='"+typeId+"'";
           return ddDat.GetOrderType(sql);
       }

       /// <summary>
       /// 根据会员ID和订单状态查询订单
       /// </summary>
       /// <param name="hyId"></param>
       /// <param name="status"></param>
       /// <returns></returns>
       public List<DingDan> GetDingDanbyhyId(string hyId,int status) 
       {
           string sql = "select * from DingDan where HuiYuanID='"+hyId+"'and Status="+status+"";
           return ddDat.GetDingDan(sql);
       }

       /// <summary>
       /// 根据订单ID查询订单
       /// </summary>
       /// <param name="orderId"></param>
       /// <returns></returns>
       public List<DingDan> GetDingDan(string orderId) 
       {
           string sql = "select * from DingDan where DingDanID='"+orderId+"'";
           return ddDat.GetDingDan(sql);
       }
    }
}
