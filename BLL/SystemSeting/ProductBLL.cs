using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Entities;
using Data.SystemSeting;
using Tools;
using Data;

namespace BLL.SystemSeting
{
   public class ProductBLL:BaseBll
    {
       ProductDat dal = new ProductDat();
       ChuJiaJiLuDat record = new ChuJiaJiLuDat();
       /// <summary>
       /// 添加
       /// </summary>
       /// <param name="obj"></param>
       public void Add(Product obj) 
       {
           db.Products.Add(obj);
           db.SaveChanges();
       }

       /// <summary>
       /// 跟据条件查找产品
       /// </summary>
       /// <param name="name"></param>
       /// <param name="PinPai"></param>
       /// <param name="typeid"></param>
       /// <param name="bh"></param>
       /// <returns></returns>
       public List<Product> GetPort(string name,string PinPai,string typeid,string bh="")
       {
           string sql = "select * from Product where productName like '%"+name+"%'";
           if (PinPai!="")
           {
               sql += " and productBrand='"+PinPai+"'";
           }
           if (typeid!=null&&typeid!="")
           {
               sql += " and ProductTypeID='"+typeid+"'";
           }
           if (bh!="")
           {
               sql += " and coding='"+bh+"'";
           }
           return dal.GetProduct(sql);
       }

       /// <summary>
       /// 跟据ID查找数据
       /// </summary>
       /// <param name="cpId"></param>
       /// <returns></returns>
       public List<Product> GetById(string cpId)
       {
           string sql = "select * from Product where  ProductID='" + cpId + "'";
           return dal.GetProductById(sql);
       }

       /// <summary>
       /// 修改产品信息
       /// </summary>
       /// <param name="obj"></param>
       /// <returns></returns>
       //public int Eid(Product obj)
       //{
       //    string dat = obj.AuctionTime == null ? "null" :"'"+obj.AuctionTime.Value.ToString("yyyy-MM-dd HH:mm:ss")+"'";
       //    string sql = "update Product set coding='"+obj.coding+"',ProductTypeID='"+obj.ProductTypeID+"',productName='"+obj.productName+"',productBrand='"+obj.productBrand+"',productPrice="+obj.productPrice+",PmJGproduct="+obj.PmJGproduct.Value+",AuctionTime="+dat+",TimePoint="+obj.TimePoint+",ProductDetails='"+obj.ProductDetails+"',PriceAdd="+obj.PriceAdd+",AuctionPoint="+obj.AuctionPoint+",Intro="+obj.Intro+",Fee="+obj.Fee+",ShipFee="+obj.ShipFee+" where ProductID='"+obj.ProductID+"'";
       //    return setSql(sql);
       //}

       /// <summary>
       /// 删除数据
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
       public string Dele(string id) 
       {
           try
           {
               string sql = "delete from  Product where ProductID='" + id + "'";
               dal.Dele(sql);
               return "ok";
           }
           catch (Exception)
           {
               return "请检查相关联的系统是否删完！";
           }
       }

       /// <summary>
       /// 添加产品图片
       /// </summary>
       /// <param name="id"></param>
       /// <param name="cpid"></param>
       /// <param name="img"></param>
       /// <returns></returns>
       public int AddCpImge(string id,string cpid,string img,string xh)
       {
           string sql = "insert into ProductImeg (ProductImegId,ProductID,img,xh)values('"+id+"','"+cpid+"','"+img+"','"+xh+"')";
           return setSql(sql);
       }

       /// <summary>
       /// 按条件查询产品图片
       /// </summary>
       /// <param name="id"></param>
       /// <param name="cpid"></param>
       /// <returns></returns>
       public List<ProductImeg> GetProtductImeg(string id,string cpid) 
       {
           string sql = "select * from ProductImeg";
           if (cpid!=""&&cpid!=null)
           {
               sql = "select * from ProductImeg where ProductID='" + cpid + "'";
           }
           if (id!=null&&id!="")
           {
               sql = "select * from ProductImeg where ProductImegId='" + id + "'";
           }
           sql += " order by xh asc";
           return dal.GetProducImeg(sql);
       }

       /// <summary>
       /// 删除产品图片
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
       public int DeletImg(string id)
       {
           string sql = "delete from ProductImeg where ProductImegId='"+id+"'";
           return setSql(sql);
       }

       /// <summary>
       /// 正在热拍的25件商品
       /// </summary>
       /// <returns></returns>
       public List<Product> GetAuctioningProduct_Top25() 
       {
           string sql = "select top 25 * from Product where Status=4 order by AuctionTime asc";
           return dal.GetProduct(sql);
       }

       /// <summary>
       /// 查询所有的常规竞拍商品
       /// </summary>
       /// <returns></returns>
       public List<Product> GetAuctioningProduct() 
       {
           string sql = "select * from AuctionType where TypeName='常规竞拍'";
           string auctionTypeId = dal.GetAuctionType(sql)[0].AuctionTypeID;
            if (auctionTypeId != "")
            {
                string sql1 = "select * from Product where Status=4 and AuctionTypeID='" + auctionTypeId + "' order by AuctionTime asc";
                return dal.GetProduct(sql1);
            }
            else 
            {
                return null;
            }         
       }

       /// <summary>
       /// 根据ID查询竞拍类型
       /// </summary>
       /// <returns></returns>
       public List<AuctionType> GetAuctionTypebyID(string id) 
       {
           string sql = "select * from AuctionType where AuctionTypeID='"+id+"'";
           return dal.GetAuctionType(sql);
       }

       /// <summary>
       /// 根据名称查询竞拍类型
       /// </summary>
       /// <returns></returns>
       public List<AuctionType> GetAuctionTypebyName(string name) 
       {
           string sql = "select * from AuctionType where TypeName='"+name+"'";
           return dal.GetAuctionType(sql);
       }

       /// <summary>
       /// 查询5条推荐商品信息
       /// </summary>
       /// <returns></returns>
       public List<Product> GetRecomdProduct_Top5() 
       {
           string sql = "select top 5 * from Product where IsRecommend=1 order by AuctionTime asc";
           return dal.GetProduct(sql);
       }

       /// <summary>
       /// 查询5条已成交商品
       /// </summary>
       /// <returns></returns>
       public List<Product> GetDoneProduct_Top5() 
       {
           string sql = "select top 5 * from Product where Status=3 and AuctionTypeID=(select AuctionTypeID from AuctionType where TypeName='常规竞拍') order by EndTime desc";
           return dal.GetProduct(sql);
       }

       /// <summary>
       /// 查询会员参与过竞拍的商品
       /// </summary>
       /// <param name="hyId"></param>
       /// <param name="status"></param>
       /// <returns></returns>
       //public List<Product> GetProductbyStatus(string hyId, int status)
       //{
       //    List<ChuJiaJiLu> list = record.GetdifProductIDbyhyID(hyId);
       //    List<Product> list1 = new List<Product>();
       //    if (list != null)
       //    {
       //        for (int i = 0; i < list.Count; i++)
       //        {
       //            string proId = list[i].ProductID;
       //            string sql = "select * from Product where ProductID='" + proId + "' and Status='" + status + "'";
       //            if (dal.GetProduct(sql).Count > 0)
       //            {
       //                list1.Add(dal.GetProduct(sql)[0]);
       //            }
       //        }
       //        return list1;
       //    }
       //    else
       //    {
       //        return null;
       //    }
       //}

       /// <summary>
       /// 查询可以积分兑换的商品
       /// </summary>
       /// <returns></returns>
       public List<Product> GetAllExchangeProduct() 
       {
           string sql = "select * from Product where IsExchange=1 order by Points desc";
           return dal.GetProduct(sql);
       }
    }
}
