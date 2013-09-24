using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DAL;
using System.Data.SqlClient;
namespace Data
{
    public class LoginData
    {
     
        /// <summary>
        /// 更跟用户名和密码查找数据
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public object SelectByMM(string hhm,string mm) 
        {
            SQLHelper BLLdat = new SQLHelper();
           string sql = "select * from YongHu where YHM=@yhm and MM=@mm and [status]=1";
           CommandType CmTyp=new CommandType();
           SqlParameter[] spr={new SqlParameter("@yhm",hhm),new SqlParameter("@mm",mm)};
           return BLLdat.ExecuteScalar(CmTyp, sql, spr);
        }     
    }
}
