using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Entities;
using Tools;
using Data.SystemSeting;
namespace BLL.SystemSeting
{
   public class ProductTypeBll:BaseBll
    {
       ProductDat bll = new ProductDat();
       /// <summary>
       /// 查找产品类型
       /// </summary>
       /// <param name="name"></param>
       /// <param name="id"></param>
       /// <returns></returns>
       public List<ProductType> GetAll(string name,string id,string fid) 
       {
           string sql = "select * from ProductType where  TypeName like'%"+name+"%'";;
           if (id!="")
           {
               sql += "and ProductTypeID='"+id+"'";
           }
           if (fid!="")
           {
               sql += "and Fid='"+fid+"'";
           }
           return bll.GetProducType(sql);
       }

       /// <summary>
       /// 添加
       /// </summary>
       /// <param name="obj"></param>
       public void Add(ProductType obj) 
       {
           db.ProductTypes.Add(obj);
           db.SaveChanges();
       }

       /// <summary>
       /// 删除数据
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
       public int Dele(string id) 
       {
           string sql = "delete from ProductType where ProductTypeID='" + id + "'";
           return bll.setSQL(sql);
       }

       /// <summary>
       /// 更新
       /// </summary>
       /// <param name="obj"></param>
       /// <returns></returns>
       public int Upudate(ProductType obj) 
       {
           string sql = "update ProductType set Fid='"+obj.Fid+"',TypeName='"+obj.TypeName+"',remark='"+obj.remark+"' where ProductTypeID='"+obj.ProductTypeID+"'";
           return bll.setSQL(sql);
       }
    }
}
