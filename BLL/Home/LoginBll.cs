using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Entities;
using Model;
using System.Data;
using Data;


namespace BLL.Home
{
    public class LoginBll
    {
        PPWContext db = new PPWContext();
        /// <summary>
        /// 根据用户名和密码获取正常状态的用户信息
        /// </summary>
        /// <param name="yhm"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public object GetYongHu(string yhm, string pwd)
        {
            LoginData datLogin = new LoginData();
            object obj = datLogin.SelectByMM(yhm, pwd);
            return obj;

        }
        /// <summary>
        /// 根据用户id获取用户信息
        /// </summary>
        /// <param name="yongHuId"></param>
        /// <returns></returns>
        public YongHu GetYongHu(string yongHuId)
        {
            return db.YongHus.Find(yongHuId);
           // YongHu bl = new YongHu();
        }

    }
}
