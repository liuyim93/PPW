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
        HuiYuanDat hyDal = new HuiYuanDat();

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
                    auction.HuiYuanName = row["HuiYuanID"] == null ? "" : hyDal.GetHuiYuanNamebyId(row["HuiYuanID"].ToString());
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

        /// <summary>
        /// 修改竞拍信息
        /// </summary>
        /// <param name="act"></param>
        /// <returns></returns>
        public int UpdateAuction(auction act)
        {
            string sql = "update Auction set ProductID=@ProductID,AuctionPrice=@AuctionPrice,AuctionTime=@AuctionTime,PriceAdd=@PriceAdd,AuctionPoint=@AuctionPoint,FreePoint=@FreePoint,IsRecommend=@IsRecommend,AuctionTypeID=@AuctionTypeID where AuctionID=@AuctionID";
            return sh.ExecuteNonQuery(null, CommandType.Text, sql, new SqlParameter("@ProductID", act.ProductID),
                new SqlParameter("@AuctionPrice", act.AuctionPrice),
                new SqlParameter("@AuctionTime", act.AuctionTime),
                new SqlParameter("@PriceAdd", act.PriceAdd),
                new SqlParameter("@AuctionPoint", act.AuctionPoint),
                new SqlParameter("@FreePoint", act.FreePoint),
                new SqlParameter("@IsRecommend", act.IsRecommend),
                new SqlParameter("@AuctionTypeID", act.AuctionTypeID),
                new SqlParameter("@AuctionID", act.AuctionID));
        }

        /// <summary>
        /// 删除竞拍信息
        /// </summary>
        /// <param name="auctionId"></param>
        /// <returns></returns>
        public int DeleteAuction(string auctionId) 
        {
            string sql = "delete from Auction where AuctionID=@AuctionID";
            return sh.ExecuteNonQuery(null,CommandType.Text,sql,new SqlParameter("@AuctionID",auctionId));
        }

        /// <summary>
        /// 查询即将竞拍的拍品
        /// </summary>
        /// <returns></returns>
        public DataTable GetAuction_Future() 
        {
            DateTime dt = DateTime.Now.Date.AddDays(1);
            string sql = "select * from Auction where Status=4 and AuctionTime>'"+DateTime.Now+"' and AuctionTime<'"+dt+"'";
            return sh.GetDataSet(sql).Tables[0];
        }

        /// <summary>
        /// 查询历史竞拍
        /// </summary>
        /// <param name="auctionTypeId">竞拍类型</param>
        /// <param name="proTypeId">拍品类型</param>
        /// <param name="minProPrice">拍品最低价格</param>
        /// <param name="maxProPrice">拍品最高价格</param>
        /// <param name="minAuctionPrice">竞拍价格</param>
        /// <param name="maxAuctionPrice">竞拍价格</param>
        /// <returns></returns>
        public DataTable GetAuction_History(string auctionTypeId,string proTypeId,string minProPrice,string maxProPrice,string minAuctionPrice,string maxAuctionPrice) 
        {
            string sql = "select * from Auction where Status=3 and AuctionTypeID='"+auctionTypeId+"'";
            if (proTypeId!="不限")
            {
                sql += "and ProductID in(select ProductID from Product where ProductTypeID='"+proTypeId+"')";
            }

            if (minProPrice != "" && maxProPrice != "")
            {                
                sql += " and ProductID in(select ProductID from Product where productPrice>" + minProPrice + " and productPrice<" + maxProPrice + ")";
            }
            else 
            {
                if (maxProPrice == ""&&minProPrice!="")
                {
                    sql += "and ProductID in(select ProductID from Product where productPrice<" + maxProPrice + ")";
                }
            }

            if (minAuctionPrice != "" && maxAuctionPrice != "")
            {
                sql += "and AuctionPrice>" + minAuctionPrice + " and AuctionPrice<" + maxAuctionPrice + "";
            }
            else 
            {
                if (maxAuctionPrice==""&&minAuctionPrice!="")
                {
                    sql += "and AuctionPrice<"+maxAuctionPrice+"";
                }
            }
            sql += "order by EndTime desc";
            return sh.GetDataSet(sql).Tables[0];
        }

        /// <summary>
        /// 查询当天正在竞拍的拍品
        /// </summary>
        /// <returns></returns>
        public DataTable getAllAuctioning() {
            DateTime date = DateTime.Now.Date.AddDays(1);
            string sql = "select a.*,p.productName,p.productPrice,p.Intro,i.img from Auction as a, Product as p,ProductImeg as i where p.ProductID=a.ProductID and i.ProductID=a.ProductID and i.xh=1 and a.Status!=3 and a.AuctionTime<'"+date+"'";
            return  sh.GetDataSet(sql).Tables[0];            
        }

        /// <summary>
        /// 查询当天未成交的常规竞拍 拍品
        /// </summary>
        /// <returns></returns>
        public DataTable getNormalAuctioning() {
            DateTime dt = DateTime.Now.Date.AddDays(1);
            string sql = "select a.*,p.productName,p.productPrice,p.Intro,i.img from Auction as a, Product as p,ProductImeg as i where p.ProductID=a.ProductID and i.ProductID=a.ProductID and i.xh=1 and a.Status!=3 and a.AuctionTime<'" + dt + "' and a.AuctionTypeID=(select AuctionTypeID from AuctionType where TypeName='常规竞拍') order by a.AuctionTime asc";
            return sh.GetDataSet(sql).Tables[0];
        }

        /// <summary>
        /// 查询25件常规竞拍拍品
        /// </summary>
        /// <returns></returns>
        public DataTable getAuctioning_Top25() {
            string sql = "select top 25 a.*,p.productName,p.productPrice,p.Intro,i.img from Auction as a, Product as p,ProductImeg as i where p.ProductID=a.ProductID and i.ProductID=a.ProductID and i.xh=1 and a.Status!=3 and a.AuctionTypeID=(select AuctionTypeID from AuctionType where TypeName='常规竞拍') order by a.AuctionTime asc";
            return sh.GetDataSet(sql).Tables[0];
        }

        /// <summary>
        /// 查询5件 免费竞拍拍品
        /// </summary>
        /// <returns></returns>
        public DataTable getFreeAuction_Top5() {
            string sql = "select top 5 a.*,p.productName,p.productPrice,p.Intro,i.img from Auction as a, Product as p,ProductImeg as i where p.ProductID=a.ProductID and i.ProductID=a.ProductID and i.xh=1 and a.Status!=3 and a.AuctionTypeID=(select AuctionTypeID from AuctionType where TypeName='免费竞拍') order by a.AuctionTime asc";
            return sh.GetDataSet(sql).Tables[0];
        }

        /// <summary>
        /// 查询当天所有未成交的免费竞拍 拍品
        /// </summary>
        /// <returns></returns>
        public DataTable getFreeAuctioning() {
            DateTime dt = DateTime.Now.Date;
            string sql = "select a.*,p.productName,p.productPrice,p.Intro,i.img from Auction as a,Product as p,ProductImeg as i where p.ProductID=a.ProductID and i.ProductID=a.ProductID and i.xh=1 and a.AuctionTypeID=(select AuctionTypeID from AuctionType where TypeName='免费竞拍') and a.Status!=3 and a.AuctionTime<'"+dt.AddDays(1)+"' order by a.AuctionTime asc";
            return sh.GetDataSet(sql).Tables[0];
        }

        #region 竞拍类型

        /// <summary>
        /// 查询竞拍类型
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public List<AuctionType> GetAuctionType(string sql) 
        {
            List<AuctionType> list = new List<AuctionType>();
            DataSet ds = sh.GetDataSet(sql);
            if (ds.Tables[0].Rows.Count>0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    AuctionType auctionType = new AuctionType();
                    auctionType.AuctionTypeID=row["AuctionTypeID"].ToString();
                    auctionType.TypeName=row["TypeName"].ToString();
                    list.Add(auctionType);
                }
            }
            return list;
        }
        # endregion
    }
}
