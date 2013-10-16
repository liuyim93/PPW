using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data;
using Model.Entities;

namespace BLL
{
    public class ShouHuoDZBll
    {
        ShouHuoDZDat adressDat = new ShouHuoDZDat();

        /// <summary>
        /// 添加收货地址
        /// </summary>
        /// <param name="adress"></param>
        /// <returns></returns>
        public int AddShouHuoDZ(ShouHuoDZ adress) 
        {
            return adressDat.AddShouHuoDZ(adress);
        }

        /// <summary>
        /// 根据收货地址ID查询收货地址
        /// </summary>
        /// <param name="adrid"></param>
        /// <returns></returns>
        public List<ShouHuoDZ> GetShouHuoDZ(string adrid) 
        {
            string sql = "select * from ShouHuoDZ where ShouHuoDZID='"+adrid+"'";
            return adressDat.GetShouHuoDZ(sql);
        }

        /// <summary>
        /// 根据会员ID查询收货地址
        /// </summary>
        /// <param name="hyId"></param>
        /// <returns></returns>
        public List<ShouHuoDZ> GetShouHuoDZbyhyId(string hyId) 
        {
            string sql = "select * from ShouHuoDZ where HuiYuanID='"+hyId+"'";
            return adressDat.GetShouHuoDZ(sql);
        }
    }
}
