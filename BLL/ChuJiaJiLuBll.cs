using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data;
using Model.Entities;

namespace BLL
{
    public class ChuJiaJiLuBll
    {
        ChuJiaJiLuDat cd = new ChuJiaJiLuDat();

        /// <summary>
        /// 添加出价记录
        /// </summary>
        /// <param name="cj"></param>
        /// <returns></returns>
        public int AddChuJiaJiLu(ChuJiaJiLu cj) 
        {
            return cd.AddChuJiaJiLu(cj);
        }

        //根据id查询出价记录
        public List<ChuJiaJiLu> GetChuJiaJiLubyId(string id) 
        {
            string sql = "select * from ChuJiaJiLu where ChuJiaJiLuID='"+id+"'";
            return cd.GetChuJiaJiLu(sql);
        }

        //根据商品ID查询出价记录
        public List<ChuJiaJiLu> GetChuJiaJiLubyProId(string proId) 
        {
            string sql = "select * from ChuJiaJiLu where ProductID='"+proId+"'";
            return cd.GetChuJiaJiLu(sql);
        }

        //根据会员ID查询出价记录
        public List<ChuJiaJiLu> GetChuJiaJiLubyhyId(string hyId)
        {
            string sql = "select * from ChuJiaJiLu where HuiYuanID='"+hyId+"'";
            return cd.GetChuJiaJiLu(sql);
        }

        //查询最新的10条出价记录
        public List<ChuJiaJiLu> GetChuJiaJiLubyProId_Top10(string ProId) 
        {
            string sql = "select top 10 * from ChuJiaJiLu where ProductID='"+ProId+"' order by AuctionTime desc";
            return cd.GetChuJiaJiLu(sql);
        }
    }
}
