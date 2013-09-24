using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Entities;
using Model;
using System.Web;
using Tools;

namespace BLL.Home
{
    public class IndexBll
    {
         PPWContext db = new PPWContext();

        /// <summary>
        /// 获取有相关权限的以及菜单
        /// </summary>
        /// <returns></returns>
        public List<CaiDan> GetOneCaidan()
        {
            //用户id
            string userid = HttpContext.Current.Session["userid"].ToString();
            //用户信息
            YongHu yh = new LoginBll().GetYongHu(userid);
            //用户权限（权限标记为browser）
            var hyqx = yh.QuanXians.Where(q => q.GongNengDian.Tag == "browser");
            //角色权限（权限标记为browser）
            var jsqx = yh.JueSe != null && yh.JueSe.status == (int)Status.正常 ? yh.JueSe.QuanXians.Where(q => q.GongNengDian.Tag == "browser") : null;
            //用户和角色合并后的权限
            var qx =jsqx ==null?hyqx:hyqx.Union(jsqx);
            //返回显示的菜单并按ordernum排序的一级菜单列表
            return qx.Select(q=>q.CaiDan).Distinct().Where(c=>c.Is_Show ==1&&c.Parent_Id==0).OrderBy(c=>c.OrderNum).ToList();
        }

        /// <summary>
        /// 获取相关权限的二级及其以下的菜单
        /// </summary>
        /// <param name="pid">上级id</param>
        /// <returns></returns>
        public List<CaiDan> GetChildCaidan()
        {
            //用户id
            string userid = HttpContext.Current.Session["userid"].ToString();
            //用户信息
            YongHu yh = new LoginBll().GetYongHu(userid);
            //用户权限（权限标记为browser）
            var hyqx = yh.QuanXians.Where(q => q.GongNengDian.Tag == "browser");
            //角色权限（权限标记为browser）
            var jsqx = yh.JueSe != null&&yh.JueSe.status==(int)Status.正常 ? yh.JueSe.QuanXians.Where(q => q.GongNengDian.Tag == "browser"):null;
            //用户和角色合并后的权限
            var qx = jsqx == null ? hyqx : hyqx.Union(jsqx);
            //返回显示的菜单并按ordernum排序的一级菜单列表
            return qx.Select(q => q.CaiDan).Distinct().Where(c => c.Is_Show == 1 && c.Parent_Id != 0).OrderBy(c => c.OrderNum).ToList();
        }

        /// <summary>
        /// 获取所有显示的菜单
        /// </summary>
        /// <returns></returns>
        public List<CaiDan> GetCaiDan()
        {
            return db.CaiDans.Where(c=>c.Is_Show == 1).OrderBy(c=>c.OrderNum).ToList();
        }
        /// <summary>
        /// 获取所有的功能点
        /// </summary>
        /// <returns></returns>
        public List<GongNengDian> GetAllGongNengDian()
        {
            return db.GongNengDians.ToList().OrderBy(g => Convert.ToInt32(g.Id)).ToList();
        }
        /// <summary>
        /// 根据用户id获取其含有相关权限的报表菜单
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <returns></returns>
        public List<CaiDan> GetAllBaoBiaoMenu(string param)
        {
            //用户id
            string userid = HttpContext.Current.Session["userid"].ToString();
            //用户信息
            YongHu yh = new LoginBll().GetYongHu(userid);
            //用户权限（权限标记为browser）
            var hyqx = yh.QuanXians.Where(q => q.GongNengDian.Tag == "browser");
            //角色权限（权限标记为browser）
            var jsqx = yh.JueSe != null && yh.JueSe.status == (int)Status.正常 ? yh.JueSe.QuanXians.Where(q => q.GongNengDian.Tag == "browser") : null;
            //用户和角色合并后的权限
            var qx = jsqx == null ? hyqx : hyqx.Union(jsqx);
            //返回显示的菜单并按ordernum排序的一级菜单列表
            return qx.Select(q => q.CaiDan).Distinct().Where(c => c.Is_Show == 1).OrderBy(c => c.OrderNum).ToList();
        }
    }
}
