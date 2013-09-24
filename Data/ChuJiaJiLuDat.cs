using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Entities;
using DAL;
using System.Data;
using System.Data.SqlClient;

namespace Data
{
   public class ChuJiaJiLuDat
    {
        SQLHelper sh = new SQLHelper();

       /// <summary>
       /// 添加出价记录
       /// </summary>
       /// <param name="cj"></param>
       /// <returns></returns>
        public int AddChuJiaJiLu(ChuJiaJiLu cj) 
        {
            string sql = "insert into ChuJiaJiLu ProductID=@ProductID,HuiYuanID=@HuiYuanID,Price=@Price,IPAdress=@IPAdress,AuctionPoint=@AuctionPoint,AuctionTime=@AuctionTime,Status=@Status,FreePoint=@FreePoint";
            try
            {
                return sh.ExecuteNonQuery(null,CommandType.Text,sql,new SqlParameter("@ProductID",cj.ProductID),
                    new SqlParameter("@HuiYuanID", cj.HuiYuanID),
                    new SqlParameter("@Price",cj.Price),
                    new SqlParameter("@IPAdress",cj.IPAdress),
                    new SqlParameter("@AuctionPoint",cj.AcutionPoint),
                    new SqlParameter("@AuctionTime",cj.AcutionTime),
                    new SqlParameter("@Status",cj.Status),
                    new SqlParameter("@FreePoint",cj.FreePoint));
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}
