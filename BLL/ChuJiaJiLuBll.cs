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
    }
}
