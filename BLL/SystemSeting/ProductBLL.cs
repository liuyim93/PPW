﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Entities;
using Data.SystemSeting;
using Tools;

namespace BLL.SystemSeting
{
   public class ProductBLL:BaseBll
    {
       ProductDat dal = new ProductDat();
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
       public int Eid(Product obj)
       {
           string dat = obj.AuctionTime == null ? "null" :"'"+obj.AuctionTime.Value.ToString("yyyy-MM-dd HH:mm:ss")+"'";
           string sql = "update Product set coding='"+obj.coding+"',ProductTypeID='"+obj.ProductTypeID+"',productName='"+obj.productName+"',productBrand='"+obj.productBrand+"',productPrice="+obj.productPrice+",PmJGproduct="+obj.PmJGproduct.Value+",AuctionTime="+dat+",TimePoint="+obj.TimePoint+",ProductDetails='"+obj.ProductDetails+"',PriceAdd="+obj.PriceAdd+",AuctionPoint="+obj.AuctionPoint+",Intro="+obj.Intro+",Fee="+obj.Fee+",ShipFee="+obj.ShipFee+" where ProductID='"+obj.ProductID+"'";
           return setSql(sql);
       }

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
       public List<Product> GetAuctioningProduct() 
       {
           string sql = "select top 25 * from Product where status=4 order by AuctionTime asc";
           return dal.GetProduct(sql);
       }

       /// <summary>
       /// 会员出价
       /// </summary>
       /// <param name="pro"></param>
       /// <returns></returns>
       public int UpdateProductPrice(Product pro) 
       {
           return dal.UpdatePrice(pro);
       }

       /// <summary>
       /// 产品成交
       /// </summary>
       /// <param name="pro"></param>
       /// <returns></returns>
       public int UpdateProductStatus(Product pro) 
       {
           return dal.UpdateStatus(pro);
       }
    }
}