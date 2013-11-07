﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Entities;
using Data;
using Data.SystemSeting;

namespace BLL
{
    public class AuctionBll
    {
        AuctionData auctionDat = new AuctionData();
        ChuJiaJiLuDat recordDat = new ChuJiaJiLuDat();

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
            string sql = "select * from Auction where ProductID="+proId;
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
    }
}