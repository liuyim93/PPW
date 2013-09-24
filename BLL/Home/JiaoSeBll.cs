using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using Model.Entities;
using Tools;
namespace BLL.Home
{
  public  class JiaoSeBll:BaseBll
    {
        /// <summary>
        /// 获取所有非删除角色的信息
        /// </summary>
        /// <returns></returns>
        public List<JueSe> GetJueSe() 
        {
             List<JueSe> list=db.JueSes.ToList();
             return list;
        }

        /// <summary>
        /// 根据ID删除角色信息
        /// </summary>
        /// <param name="id"></param>
        public int Delect(string id)
        {
            string sql = "delete from JueSe where JueSeId='"+id+"'";
            return setSql(sql);
        }
        /// <summary>
        /// 按ID更新角色的基本信息
        /// </summary>
        /// <param name="obj"></param>
        public void Update(JueSe obj) 
        {
            JueSe JueShe = db.JueSes.Find(obj.JueSeId);
            JueShe.JSMC = obj.JSMC;
            JueShe.Remark = obj.Remark;
            JueShe.status = obj.status;
            db.SaveChanges();  
        }
        /// <summary>
        /// 增加角色信息
        /// </summary>
        /// <param name="obj"></param>
        public void Add(JueSe obj) 
        {
            db.JueSes.Add(obj);
            db.SaveChanges();
        }

        /// <summary>
        /// 按条件查角色
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public List<JueSe> SelectBy(string name, string stat)
        {
            IEnumerable<JueSe> lists = db.JueSes;
            if (name != "")
            {
                lists = lists.Where(J => J.JSMC == name);
            }
            if (stat != "" && stat != null)
            {
                lists = lists.Where(k => k.status == Convert.ToInt32(stat));
            }
            return lists.ToList();
        }
      /// <summary>
      /// 根据角色ID查角色
      /// </summary>
      /// <param name="id"></param>
      /// <returns></returns>
        public JueSe SelectById(string id) 
        {
           JueSe js=db.JueSes.Find(id);
           return js;
        }
      
    }
}
