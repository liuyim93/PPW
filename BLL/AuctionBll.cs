using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Entities;
using Data;
using Data.SystemSeting;
using System.Data;

namespace BLL
{
    public class AuctionBll
    {
        AuctionData auctionDat = new AuctionData();
        ChuJiaJiLuDat recordDat = new ChuJiaJiLuDat();

        #region 竞拍类型
        /// <summary>
        /// 查询所有的竞拍类型
        /// </summary>
        /// <returns></returns>
        public List<AuctionType> GetAllAuctionType() 
        {
            string sql = "select * from AuctionType";
            return auctionDat.GetAuctionType(sql);
        }
        #endregion

        /// <summary>
        /// 添加竞拍商品
        /// </summary>
        /// <param name="auction"></param>
        /// <returns></returns>
        public int AddAuction(auction auction) 
        {
            return auctionDat.AddAuction(auction);
        }

        /// <summary>
        /// 根据产品ID查询竞拍商品
        /// </summary>
        /// <param name="proId"></param>
        /// <returns></returns>
        public List<auction> GetAuctionbyProId(string proId) 
        {
            string sql = "select * from Auction where ProductID='"+proId+"'";
            return auctionDat.GetAuction(sql);
        }

        /// <summary>
        /// 根据ID查询竞拍商品
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<auction> GetAuction(string id) 
        {
            string sql = "select * from Auction where AuctionID='"+id+"'";
            return auctionDat.GetAuction(sql);
        }

        /// <summary>
        /// 查询正在竞拍的25件商品
        /// </summary>
        /// <returns></returns>
        public List<auction> GetAuction_Top25() 
        {
            string sql = "select top 25 * from Auction where Status=4 order by AuctionTime ASC";
            return auctionDat.GetAuction(sql);
        }

        /// <summary>
        /// 查询最近成交的5件商品
        /// </summary>
        /// <returns></returns>
        public List<auction> GetAuctioned_Top5() 
        {
            string sql = "select top 5 * from Auction where Status=3 order by EndTime DESC";
            return auctionDat.GetAuction(sql);
        }

        /// <summary>
        /// 修改倒计时
        /// </summary>
        /// <param name="auctionId"></param>
        /// <returns></returns>
        public int UpdateTimePoint(string auctionId) 
        {
            return auctionDat.UpdateTimePoint(auctionId);
        }

        /// <summary>
        /// 修改拍品状态
        /// </summary>
        /// <param name="auction"></param>
        /// <returns></returns>
        public int UpdateStatus(auction auction) 
        {
            return auctionDat.UpdateStatus(auction);
        }

        /// <summary>
        /// 出价
        /// </summary>
        /// <param name="auction"></param>
        /// <returns></returns>
        public int UpdateAuctionPrice(auction auction) 
        {
            return auctionDat.UpdateAuctionPrice(auction);
        }

        /// <summary>
        /// 查询所有已成交的竞拍
        /// </summary>
        /// <returns></returns>
        public List<auction> GetAllAuctioned() 
        {
            string sql = "select * from Auction where Status=3 order by EndTime desc";
            return auctionDat.GetAuction(sql);
        }

        /// <summary>
        /// 查询推荐的5件拍品
        /// </summary>
        /// <returns></returns>
        public List<auction> GetRecommendAuction_Top5() 
        {
            string sql = "select top 5 * from Auction where IsRecommend=1 and Status=4 order by AuctionTime asc";
            return auctionDat.GetAuction(sql);
        }

        /// <summary>
        /// 根据会员ID和状态查询拍品
        /// </summary>
        /// <param name="hyId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public List<auction> GetAuctionbyStatus(string hyId,int status) 
        {
            List<ChuJiaJiLu> list_record = recordDat.GetdifAuctionIDbyhyID(hyId);
            List<auction> list = new List<auction>();
            if (list_record.Count>0)
            {
                for (int i = 0; i < list_record.Count; i++)
                {
                    string auctionId=list_record[i].AuctionID;
                    string sql = "select * from Auction where AuctionID='"+auctionId+"' and Status="+status+"";
                    if (auctionDat.GetAuction(sql).Count>0)
                    {
                        list.Add(auctionDat.GetAuction(sql)[0]);
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// 查询所有正在竞拍的拍品
        /// </summary>
        /// <returns></returns>
        public List<auction> GetAllAuctioning() 
        {
            string sql = "select * from Auction where Status=4 order by AuctionTime asc";
            return auctionDat.GetAuction(sql);
        }

        /// <summary>
        /// 根据拍品名称、竞拍类型、状态查询竞拍信息
        /// </summary>
        /// <param name="proName">拍品名称</param>
        /// <param name="auctionType">竞拍类型</param>
        /// <param name="status">状态</param>
        /// <returns></returns>
        public List<auction> GetAuction(string proName,string auctionType,string status) 
        {
            string sql = "select * from Auction where ProductID in(select ProductID from Product where productName like'%" + proName + "%')";
            if (auctionType != "")
            {
                sql += "and AuctionTypeID=(select AuctionTypeID from AuctionType where TypeName='" + auctionType + "')";
            }
            else 
            {
                if (status!="")
                {
                    sql += "and Status="+status;
                }
            }
            return auctionDat.GetAuction(sql);
        }

        /// <summary>
        /// 根据竞拍类型ID查询竞拍信息
        /// </summary>
        /// <param name="typeId"></param>
        /// <returns></returns>
        public List<AuctionType> GetAuctionTypebyId(string typeId) 
        {
            string sql = "select * from AuctionType where AuctionTypeID='"+typeId+"'";
            return auctionDat.GetAuctionType(sql);
        }

        /// <summary>
        /// 根据类型名查询竞拍信息
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public List<AuctionType> GetAuctionTypebyName(string typeName) 
        {
            string sql = "select * from AuctionType where TypeName='"+typeName+"'";
            return auctionDat.GetAuctionType(sql);
        }

        /// <summary>
        /// 查询所有的竞拍类型
        /// </summary>
        /// <returns></returns>
        public List<AuctionType> GetAuctionType() 
        {
            string sql = "select * from AuctionType";
            return auctionDat.GetAuctionType(sql);
        }

        /// <summary>
        /// 修改竞拍信息
        /// </summary>
        /// <param name="act"></param>
        /// <returns></returns>
        public int UpdateAuction(auction act) 
        {
            return auctionDat.UpdateAuction(act);
        }

        /// <summary>
        /// 删除竞拍信息
        /// </summary>
        /// <returns></returns>
        public int DeleteAuction(string auctionId) 
        {
            return auctionDat.DeleteAuction(auctionId);
        }

        /// <summary>
        /// 查询即将竞拍的拍品
        /// </summary>
        /// <returns></returns>
        public DataTable GetAuction_Future() 
        {
            return auctionDat.GetAuction_Future();
        }

        /// <summary>
        /// 查询历史竞拍
        /// </summary>
        /// <param name="auctionTypeId"></param>
        /// <param name="proTypeId"></param>
        /// <param name="minProPrice"></param>
        /// <param name="maxProPrice"></param>
        /// <param name="minAuctionPrice"></param>
        /// <param name="maxAuctionPrice"></param>
        /// <returns></returns>
        public DataTable GetAuction_History(string auctionTypeId,string proTypeId,string minProPrice,string maxProPrice,string minAuctionPrice,string maxAuctionPrice) 
        {
            return auctionDat.GetAuction_History(auctionTypeId,proTypeId,minProPrice,maxProPrice,minAuctionPrice,minAuctionPrice);
        }

        /// <summary>
        /// 查询当天正在竞拍的常规竞拍拍品
        /// </summary>
        /// <returns></returns>
        public DataTable getAllAuctioning() {
            return auctionDat.getAllAuctioning();
        }

        /// <summary>
        /// 查询当天未成交的常规竞拍 拍品
        /// </summary>
        /// <returns></returns>
        public DataTable getNormalAuctioning() {
            return auctionDat.getNormalAuctioning();
        }

        /// <summary>
        /// 查询25件 常规竞拍
        /// </summary>
        /// <returns></returns>
        public DataTable getAuctioning_Top25() {
            return auctionDat.getAuctioning_Top25();
        }

        /// <summary>
        /// 查询5条免费竞拍
        /// </summary>
        /// <returns></returns>
        public DataTable getFreeAuction_Top5() {
            return auctionDat.getFreeAuction_Top5();
        }

        /// <summary>
        /// 查询当天所有未成交的免费竞拍 拍品
        /// </summary>
        /// <returns></returns>
        public DataTable getFreeAuctioning() {
            return auctionDat.getFreeAuctioning();
        }
    }
}
