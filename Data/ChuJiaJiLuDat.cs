﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Entities;
using DAL;
using System.Data;
using System.Data.SqlClient;
using Data.SystemSeting;

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
            string sql = "insert into ChuJiaJiLu (AuctionID,HuiYuanID,Price,IPAdress,AuctionPoint,AuctionTime,Status,FreePoint) values(@AuctionID,@HuiYuanID,@Price,@IPAdress,@AuctionPoint,@AuctionTime,@Status,@FreePoint)";
            try
            {
                return sh.ExecuteNonQuery(null, CommandType.Text, sql, new SqlParameter("@AuctionID", cj.AuctionID),
                    new SqlParameter("@HuiYuanID", cj.HuiYuanID),
                    new SqlParameter("@Price",cj.Price),
                    new SqlParameter("@IPAdress",cj.IPAdress),
                    new SqlParameter("@AuctionPoint",cj.AuctionPoint),
                    new SqlParameter("@AuctionTime",cj.AuctionTime),
                    new SqlParameter("@Status",cj.Status),
                    new SqlParameter("@FreePoint",cj.FreePoint));
            }
            catch (Exception)
            {
                
                throw;
            }
        }

       /// <summary>
       /// 查询出价记录
       /// </summary>
       /// <param name="sql"></param>
       /// <returns></returns>
        public List<ChuJiaJiLu> GetChuJiaJiLu(string sql) 
        {
            List<ChuJiaJiLu> list = new List<ChuJiaJiLu>();
            DataSet ds = sh.GetDataSet(sql);
            if (ds.Tables[0].Rows.Count>0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    ChuJiaJiLu record = new ChuJiaJiLu();
                    record.HuiYuanID=row["HuiYuanID"].ToString();
                    record.IPAdress=row["IPAdress"].ToString();
                    record.Price = Convert.ToDecimal(row["Price"]);
                    record.AuctionID = row["AuctionID"].ToString();
                    record.Status = Convert.ToInt32(row["Status"]);
                    record.AuctionPoint = Convert.ToInt32(row["AuctionPoint"]);
                    record.AuctionTime = Convert.ToDateTime(row["AuctionTime"]);
                    record.FreePoint = Convert.ToInt32(row["FreePoint"]);
                    record.ChuJiaJiLuID=row["ChuJiaJiLuID"].ToString();
                    list.Add(record);
                }
            }
            return list;
        }

       /// <summary>
       ///根据会员ID查询参与过的竞拍ID 
       /// </summary>
       /// <param name="hyId"></param>
       /// <returns></returns>
        public List<ChuJiaJiLu> GetdifAuctionIDbyhyID(string hyId) 
        {
            List<ChuJiaJiLu> list = new List<ChuJiaJiLu>();
            string sql = "select distinct AuctionID from ChuJiaJiLu where HuiYuanID=@HuiYuanID";
            try
            {
                using (SqlDataReader reader=sh.ExecuteReader(CommandType.Text,sql,new SqlParameter("@HuiYuanID",hyId)))
                {
                    while (reader.Read())
                    {
                        ChuJiaJiLu record = new ChuJiaJiLu();
                        record.AuctionID = reader["AuctionID"].ToString();
                        list.Add(record);
                    }
                }
                return list;
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        
       /// <summary>
        /// 根据竞拍ID查询参与竞拍的会员ID
       /// </summary>
       /// <param name="proId"></param>
       /// <returns></returns>
        public List<ChuJiaJiLu> GetHuiYuanIDbyauctionId(string auctionId)
        {
            List<ChuJiaJiLu> list=new List<ChuJiaJiLu>();
            string sql = "select distinct HuiYuanID from ChuJiaJiLu where AuctionID=@AuctionID";
            try
            {
                using (SqlDataReader reader = sh.ExecuteReader(CommandType.Text, sql, new SqlParameter("@AuctionID", auctionId)))
                {
                    while (reader.Read())
                    {
                        ChuJiaJiLu record = new ChuJiaJiLu();
                        record.HuiYuanID = reader["HuiYuanID"].ToString();
                        list.Add(record);
                    }
                    return list;
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

       /// <summary>
       /// 查询出价记录、出价人会员名和手机号
       /// </summary>
       /// <param name="sql"></param>
       /// <returns></returns>
        public List<ChuJiaJiLu> GetchuJiaJiLu(string sql) {
            List<ChuJiaJiLu> list = new List<ChuJiaJiLu>();
            DataSet ds = sh.GetDataSet(sql);
            if(ds.Tables[0].Rows.Count>0){
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    ChuJiaJiLu record = new ChuJiaJiLu();
                    record.HuiYuanID = row["HuiYuanID"].ToString();
                    record.IPAdress = row["IPAdress"].ToString();
                    record.Price = Convert.ToDecimal(row["Price"]);
                    record.AuctionID = row["AuctionID"].ToString();
                    record.Status = Convert.ToInt32(row["Status"]);
                    record.AuctionPoint = Convert.ToInt32(row["AuctionPoint"]);
                    record.AuctionTime = Convert.ToDateTime(row["AuctionTime"]);
                    record.FreePoint = Convert.ToInt32(row["FreePoint"]);
                    record.ChuJiaJiLuID = row["ChuJiaJiLuID"].ToString();
                    record.HuiYuanName=row["HuiYuanName"].ToString();
                    record.sjh=row["sjh"].ToString();
                    list.Add(record);
                }
            }
            return list;
        }
    }
}
