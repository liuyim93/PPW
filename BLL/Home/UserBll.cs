using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using Model.Entities;
using Tools;
using Data;
using DAL;

namespace BLL.Home
{
   
    public class UserBll:BaseBll
    {
        LoginData LongDat = new LoginData();//执行Sql语句
        /// <summary>
        /// 获取所有非删除状态的用户信息
        /// </summary>
        /// <returns></returns>
        public List<YongHu> GetUsers(string xm,string name,string st,string jsid=null)
        {
            IEnumerable<YongHu> list = db.YongHus.Where(y => y.RenYuan.PersonName.Contains(xm) && y.YHM.Contains(name));
            if (st!=""&&st!=null)
            {
                list = list.Where(x=>x.status==int.Parse(st));
            }
            return list.ToList();
        }

        public object GetByJsID(string jsId) 
        {
            SQLHelper dbHep = new SQLHelper();
            string sql = "select JSMC from YongHu yh,JueSe js where yh.JSID=js.JueSeId and yh.JSID='"+jsId+"'";
            return dbHep.ExecuteScalar(System.Data.CommandType.Text,sql);
        }

        /// <summary>
        /// 根据用户id删除用户信息
        /// </summary>
        /// <param name="id">用户id</param>
        public int DeleteUser(string id)
        {
            string sql = "delete from YongHu where YongHuId='"+id+"'";
           return setSql(sql);
        }
        /// <summary>
        /// 根据用户名获取用户信息
        /// </summary>
        /// <param name="hym"></param>
        /// <returns></returns>
        public YongHu GetUser(string hym)
        {
            List<YongHu> list = db.YongHus.Where(y=>y.YHM==hym).ToList();
            if (list.Count>0)
            {
                return list[0];
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 获取正常状态的角色
        /// </summary>
        /// <returns></returns>
        public List<JueSe> GetJueSe()
        {
            return db.JueSes.Where(j=>j.status == (int)Status.正常).ToList();
        }
        /// <summary>
        /// 添加一条用户信息
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <returns></returns>
        public string AddUser(YongHu user)
        {
            try
            {
                db.YongHus.Add(user);
                db.SaveChanges();
                return "ok";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        /// <summary>
        /// 根据id获取用户信息
        /// </summary>
        /// <param name="id">用户id</param>
        /// <returns></returns>
        public YongHu GetUserById(string id)
        {
            return db.YongHus.Find(id);
        }
        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="user">修改后的用户信息</param>
        /// <returns></returns>
        public string UpdateUser(YongHu user)
        {
            try
            {
                YongHu y = db.YongHus.Find(user.YongHuId);
                y.BZ = user.BZ;
                y.JSID = user.JSID;
                y.MM = user.MM;
                y.RenYuanId = user.RenYuanId;
                y.YHM = user.YHM;
                y.status = user.status;
                db.SaveChanges();
                return "ok";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// 修改用户密码和用户名
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userName"></param>
        /// <param name="mm"></param>
        /// <returns></returns>
        public int UpdateUser(string id, string userName, string mm) 
        {
            string sql = "update YongHu set YHM='"+userName+"',MM='"+mm+"' where YongHuId='"+id+"'";
            return setSql(sql);
        }

        /// <summary>
        /// 根据用户id获取其姓名
        /// </summary>
        /// <param name="id">用户id</param>
        /// <returns></returns>
        public string GetUserNameById(string id)
        {
            db = new PPWContext();
            YongHu y = db.YongHus.Find(id);
            return y.RenYuan.PersonName;
         
        }
        /// <summary>
        /// 根据人员类型和id获取其姓名
        /// </summary>
        /// <param name="lx"></param>
        /// <param name="ryid"></param>
        /// <returns></returns>
        public string GetNameByIdAndLx(string lx, string ryid)
        {
            return db.RenYuans.Find(ryid).Remark;
        }
        /// <summary>
        /// 根据人员类型和人员id获取用户信息
        /// </summary>
        /// <param name="lx">人员类型（教师、学生、校外人员）</param>
        /// <param name="ryid">人员id</param>
        /// <returns></returns>
        public YongHu GetUserByRYIDAndLX(string lx, string ryid)
        {
            List<YongHu> list = db.YongHus.Where(u => u.RenYuanId == ryid).ToList();
            if (list.Count >0)
            {
                return list[0];
            }
            else
            {
                return null;
            }
        }

        public RenYuan GetRenYuanById(string id)
        {
            return db.RenYuans.Find(id);
        }
    }
}
