using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Entities;
using Data;

namespace BLL.SystemSeting
{
   public class YouQingBll
    {
       YouQingDat dat = new YouQingDat();
       public List<YouQinLianJi> GetYouQin(string id,string name,string type) 
       {
           string sql = "select * from YouQinLianJi where DispType  like '%"+type+"%'";
           if (name!="")
           {
               sql += " and GsName='"+name+"'";
           }
           if (id!="")
           {
               sql += " and YouQinLianJiId='"+id+"'";
           }
           return dat.GetYQ(sql);
       }


       /// <summary>
       /// 添加友情信息
       /// </summary>
       /// <param name="obj"></param>
       /// <returns></returns>
       public int Add(YouQinLianJi obj)
       {
           string sql = "insert into YouQinLianJi(YouQinLianJiId,GsName,img,Url,DispType,xh) values('"+obj.YouQinLianJiId+"','"+obj.GsName+"','"+obj.img+"','"+obj.Url+"','"+obj.DispType+"',"+obj.xh+")";
           return dat.setSql(sql);
       }

       /// <summary>
       /// 删除友情连接
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
       public int Dele(string id) 
       {
           string sql = "delete from YouQinLianJi where YouQinLianJiId='"+id+"'";
           return dat.setSql(sql);
       }

       /// <summary>
       /// 修改友情连接
       /// </summary>
       /// <param name="obj"></param>
       /// <returns></returns>
       public int Eid(YouQinLianJi obj)
       {
           string sql = "update YouQinLianJi set GsName='"+obj.GsName+"',img='"+obj.img+"',Url='"+obj.Url+"',DispType='"+obj.DispType+"',xh="+obj.xh+" where YouQinLianJiId='"+obj.YouQinLianJiId+"'";
           return dat.setSql(sql);
       }
    }
}
