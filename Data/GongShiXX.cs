using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Entities;
using DAL;
using System.Data;

namespace Data
{
    //公司信息
    public class GongShiXX
    {
        SQLHelper BLLdat = new SQLHelper();
        /// <summary>
        /// 跟据Sql语句得到公司信息
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public GsXX GetGsxx(string sql)
        {
            DataSet dat = BLLdat.GetDataSet(sql);
             GsXX xx=null;
            if (dat.Tables.Count>0)
            {
                if (dat.Tables[0].Rows.Count>0)
	            {
                    DataRow row=dat.Tables[0].Rows[0]; 
                    xx = new GsXX();
                    xx.GsXXId = row["GsXXId"].ToString();
                    xx.SgName = row["SgName"].ToString();
                    xx.Mode = row["Mode"].ToString();
                    xx.Dz = row["Dz"].ToString();
                    xx.WZ = row["WZ"].ToString();
                    xx.banQuanSY = row["banQuanSY"].ToString();
	            }
            }
            return xx;
        }

        /// <summary>
        /// 执行增删改操作
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public int SetSql(string sql) 
        {
            return BLLdat.ExecuteNonQuery(null,CommandType.Text, sql, null);
        }
    }
}
