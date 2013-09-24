using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Entities;
using Model;
using System.Web;

namespace BLL.Home
{
    public class QuanXianBll
    {
        PPWContext db = new PPWContext();
        /// <summary>
        /// 修改菜单功能点
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public string UpdateCaiDan_GongNengDian(List<CaiDan_GongNengDian> list)
        {
            try
            {
                foreach (CaiDan_GongNengDian c in db.CaiDan_GongNengDian)
                {
                    db.CaiDan_GongNengDian.Remove(c);
                }
                foreach (CaiDan_GongNengDian c in list)
                {
                    db.CaiDan_GongNengDian.Add(c);
                }
                db.SaveChanges();
                return "ok";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        /// <summary>
        /// 判断菜单否拥有指定功能
        /// </summary>
        /// <param name="cdid">菜单id</param>
        /// <param name="gndid">功能点id</param>
        /// <returns></returns>
        public bool IsCaiDanQuanXian(int cdid,string gndid)
        {
            int i = db.CaiDan_GongNengDian.Count(c=>c.CDID == cdid&&c.GNID == gndid);
            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 判断当前用户是否拥有指定菜单和功能点权限
        /// </summary>
        /// <param name="cdid">菜单id</param>
        /// <param name="gntag">功能点标记</param>
        /// <returns></returns>
        public bool IsHaveQx(int cdid, string gntag)
        {
            string userid = HttpContext.Current.Session["userid"].ToString();
            YongHu yh = db.YongHus.Find(userid);
            int i = yh.QuanXians.Count(q=>q.CDId == cdid&&q.GongNengDian.Tag == gntag);
            int j = yh.JueSe==null||yh.JueSe.status!=(int)Tools.Status.正常?0:yh.JueSe.QuanXians.Count(q => q.CDId == cdid && q.GongNengDian.Tag == gntag);
            if ((i + j) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        /// <summary>
        /// 判断用户是否拥有指定菜单下的指定功能全新
        /// </summary>
        /// <param name="userid">指定的用户Id</param>
        /// <param name="cdid">指定的菜单id</param>
        /// <param name="gntag">指定的功能标记</param>
        /// <returns></returns>
        public bool IsHaveUserQx(string userid,int cdid, string gnid)
        {
            YongHu yh = db.YongHus.Find(userid);
            int i = yh.QuanXians==null?0:yh.QuanXians.Count(q => q.CDId == cdid && q.GNDID == gnid);
            if (i >0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 判断指定角色是否拥有指定菜单下的指定功能权限
        /// </summary>
        /// <param name="jsid">指定的角色id</param>
        /// <param name="cdid">指定的菜单id</param>
        /// <param name="gnid">指定的功能点id</param>
        /// <returns></returns>
        public bool IsHaveJueSeQx(string jsid, int cdid, string gnid)
        {
            JueSe js = db.JueSes.Find(jsid);
            int i = js.QuanXians.Count(q=>q.CDId == cdid&&q.GNDID == gnid);
            if (i>0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 根据菜单id获取菜单信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CaiDan GetCaiDanById(int id)
        {
            return db.CaiDans.Find(id);
        }
        /// <summary>
        /// 修改用户/角色权限（修改用时角色Id为Null，修改角色时用户id为Null）
        /// </summary>
        /// <param name="yhid">用户id</param>
        /// <param name="jsid">角色id</param>
        /// <param name="list">权限列表</param>
        /// <returns></returns>
        public string UpdateQx(string yhid, string jsid, List<QuanXian> list)
        {
            try
            {
                db = new PPWContext();
                List<QuanXian> tmp;
                if (yhid != null)
                {
                    tmp = db.QuanXians.Where(q => q.HYID == yhid).ToList();
                }
                else
                {
                    tmp = db.QuanXians.Where(q => q.JSID == jsid).ToList();
                }

                foreach (QuanXian qx in tmp)
                {
                    db.QuanXians.Remove(qx);
                }

                foreach (QuanXian qx in list)
                {
                    db.QuanXians.Add(qx);
                }
                db.SaveChanges();
                return "ok";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
