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
    public class AuctionData
    {
        SQLHelper sh = new SQLHelper();

        /// <summary>
        /// 添加竞拍
        /// </summary>
        /// <param name="auction"></param>
        /// <returns></returns>
        public int AddAuction(auction auction) 
        {
            string sql = "insert into Auction (ProductID,AuctionPrice,AuctionTime,TimePoint,Status,PriceAdd,AuctionPoint,FreePoint,AuctionTypeID,IsReCommend)values(@ProductID,@AuctionPrice,@AuctionTime,@TimePoint,@Status,@PriceAdd,@AuctionPoint,@FreePoint,@AuctionTypeID,@IsRecommend)";
            return sh.ExecuteNonQuery(null,CommandType.Text,sql,new SqlParameter("@ProductID",auction.ProductID),
                new SqlParameter("@AuctionTime", auction.AuctionTime),
                new SqlParameter("@AuctionPrice", auction.AuctionPrice),
                new SqlParameter("@TimePoint", auction.TimePoint),
                new SqlParameter("@Status", auction.Status),
                new SqlParameter("@PriceAdd", auction.PriceAdd),
                new SqlParameter("@AuctionPoint", auction.AuctionPoint),
                new SqlParameter("@FreePoint", auction.FreePoint),
                new SqlParameter("@AuctionTypeID", auction.AuctionTypeID),
                new SqlParameter("@IsRecommend", auction.IsRecommend));
        }

        /// <summary>
        /// 查询竞拍商品
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public List<auction> GetAuction(string sql) 
        {
            List<auction> list = new List<auction>();
            DataSet ds = sh.GetDataSet(sql);
            if (ds.Tables[0].Rows.Count>0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    auction auction = new auction();
                    auction.AuctionID=row["AuctionID"].ToString();
                    auction.ProductID=row["ProductID"].ToString();
                    auction.AuctionTime = Convert.ToDateTime(row["AuctionTime"]);
                    auction.AuctionPrice=Convert.ToDecimal(row["AuctionPrice"]);
                    auction.TimePoint = Convert.ToInt32(row["TimePoint"]);
                    auction.Status = Convert.ToInt32(row["Status"]);
                    auction.PriceAdd = Convert.ToDecimal(row["PriceAdd"]);
                    auction.AuctionPoint = Convert.ToInt32(row["AuctionPoint"]);
                    auction.FreePoint = Convert.ToInt32(row["FreePoint"]);
                    auction.AuctionTypeID=row["AuctionTypeID"].ToString();
                    auction.IsRecommend = Convert.ToInt32(row["IsRecommend"]);
                    auction.Coding = Convert.ToInt32(row["Coding"]);
                    auction.HuiYuanID=row["HuiYuanID"]==DBNull.Value?"":row["HuiYuanID"].ToString();
                    auction.EndTime =row["EndTime"]as DateTime?;
                    auction.CreateTime = Convert.ToDateTime(row["CreateTime"]);
                    list.Add(auction);
                }
            }
            return list;
        }

        /// <summary>
        ///修改倒计时
        /// </summary>
        /// <param name="auctionId"></param>
        /// <returns></returns>
        public int UpdateTimePoint(string auctionId) 
        {
            string sql = "update Auction set TimePoint=TimePoint-1 where AuctionID=@AuctionID";
            return sh.ExecuteNonQuery(null,CommandType.Text,sql,new SqlParameter("@AuctionID",auctionId));
        }

        /// <summary>
        /// 修改拍品状态
        /// </summary>
        /// <param name="auction"></param>
        /// <returns></returns>
        public int UpdateStatus(auction auction) 
        {
            string sql = "update Auction set Status=@Status,EndTime=@EndTime where AuctionID=@AuctionID";
            return sh.ExecuteNonQuery(null, CommandType.Text, sql, new SqlParameter("@AuctionID", auction.AuctionID),
                new SqlParameter("@Status", auction.Status),
                new SqlParameter("@EndTime", auction.EndTime));
        }

        /// <summary>
        /// 出价
        /// </summary>
        /// <param name="auction"></param>
        /// <returns></returns>
        public int UpdateAuctionPrice(auction auction) 
        {
            string sql = "update Auction set AuctionPrice=AuctionPrice+PriceAdd,HuiYuanID=@HuiYuanID,TimePoint=@TimePoint where AuctionID=@AuctionID";
            return sh.ExecuteNonQuery(null, CommandType.Text, sql, new SqlParameter("@HuiYuanID", auction.HuiYuanID),
                new SqlParameter("@TimePoint", auction.TimePoint),
                new SqlParameter("@AuctionID", auction.AuctionID));
        }
    }
}
