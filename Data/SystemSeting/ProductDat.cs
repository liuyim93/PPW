using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Entities;
using DAL;
using System.Data;
using System.Data.SqlClient;

namespace Data.SystemSeting
{
   public class ProductDat
    {
       SQLHelper BLLdat = new SQLHelper();

       #region 产品类型
            
       /// <summary>
       /// 查询产品类型
       /// </summary>
       /// <param name="sql"></param>
       /// <returns></returns>
       public List<ProductType> GetProducType(string sql) 
       {
           DataSet dat = BLLdat.GetDataSet(sql);
           List<ProductType> list = new List<ProductType>();
           if (dat.Tables.Count > 0)
           {
               foreach (DataRow item in dat.Tables[0].Rows)
               {
                   ProductType typ = new ProductType();
                   typ.Fid = item["Fid"] == null ? "" : item["Fid"].ToString();
                   typ.ProductTypeID = item["ProductTypeID"].ToString();
                   typ.remark = item["remark"] == null ? "" : item["remark"].ToString();
                   typ.TypeName = item["TypeName"].ToString();
                   list.Add(typ);
               }
           }
           return list;
       }
       /// <summary>
       /// 执行SQL语句
       /// </summary>
       /// <param name="sql"></param>
       /// <returns></returns>
       public int setSQL(string sql)
       {
           return BLLdat.RunSQL(sql);
       }
       #endregion

        #region 产品
       /// <summary>
       /// 修改产品信息
       /// </summary>
       /// <param name="sql"></param>
       /// <returns></returns>
       public int Eid(string sql) 
       {
           return BLLdat.RunSQL(sql);
       }

       public int Dele(string sql) 
       {
           return BLLdat.RunSQL(sql);
       }

       /// <summary>
       /// 查询产品信息
       /// </summary>
       /// <param name="sql"></param>
       /// <returns></returns>
       public List<Product> GetProduct(string sql) 
       {
           List<Product> list = new List<Product>();
           DataSet dataset = BLLdat.GetDataSet(sql);
           if (dataset.Tables.Count > 0)
           {
               foreach (DataRow item in dataset.Tables[0].Rows)
               {
                   Product obj = new Product();
                   obj.ProductID = item["ProductID"].ToString();
                   obj.coding=item["coding"]==null?"":item["coding"].ToString();
                   obj.ProductTypeID = item["ProductTypeID"].ToString();
                   obj.productName = item["productName"]==null ? "" : item["productName"].ToString();
                   obj.productBrand = item["productBrand"] == null ? "" : item["productBrand"].ToString();
                   obj.productPrice = Convert.ToDecimal(item["productPrice"]);
                   obj.PmJGproduct = Convert.ToDecimal(item["PmJGproduct"]);
                   obj.HuiYuanID = item["HuiYuanID"].ToString();
                   obj.AuctionTime = item["AuctionTime"] == DBNull.Value ? null:item["AuctionTime"] as DateTime?;
                   obj.TimePoint = item["TimePoint"] == null ? null :item["TimePoint"] as int?;
                   obj.CreateTime = Convert.ToDateTime(item["CreateTime"]);
                   obj.Status = (int)item["Status"];
                   obj.isshouYei =Convert.ToInt32(item["isshouYei"]);
                   obj.AuctionPoint = Convert.ToInt32(item["AuctionPoint"]);
                   obj.PriceAdd = Convert.ToDecimal(item["PriceAdd"]);
                   obj.Fee = Convert.ToDecimal(item["Fee"]);
                   obj.ShipFee = Convert.ToDecimal(item["ShipFee"]);
                   obj.EndTime =item["EndTime"]==DBNull.Value ? null:item["EndTime"]as DateTime?;
                   obj.Intro = item["Intro"].ToString();
                   list.Add(obj);
               }
           }
           return list;
       }

       /// <summary>
       /// 查找产品图片
       /// </summary>
       /// <param name="sql"></param>
       /// <returns></returns>
       public List<ProductImeg> GetProducImeg(string sql) 
       {
           List<ProductImeg> list = new List<ProductImeg>();
            DataSet dataset = BLLdat.GetDataSet(sql);
            if (dataset.Tables.Count > 0)
            {
                foreach (DataRow item in dataset.Tables[0].Rows)
                {
                    ProductImeg img = new ProductImeg();
                    img.ProductImegId = item["ProductImegId"].ToString();
                    img.ProductID = item["ProductID"].ToString();
                    img.xh = item["xh"]==null?0:Convert.ToInt32(item["xh"]);
                    img.img = item["img"].ToString();
                    list.Add(img);
                }
            }
            return list;
       }

       /// <summary>
       /// 查找产品
       /// </summary>
       /// <param name="sql"></param>
       /// <returns></returns>
       public List<Product> GetProductById(string sql)
       {
           List<Product> list = new List<Product>();
           DataSet dataset = BLLdat.GetDataSet(sql);
           if (dataset.Tables.Count > 0)
           {
               foreach (DataRow item in dataset.Tables[0].Rows)
               {
                   Product obj = new Product();
                   obj.ProductID = item["ProductID"].ToString();
                   obj.coding = item["coding"] == null ? "" : item["coding"].ToString();
                   obj.ProductTypeID = item["ProductTypeID"].ToString();
                   obj.productName = item["productName"] == null ? "" : item["productName"].ToString();
                   obj.productBrand = item["productBrand"] == null ? "" : item["productBrand"].ToString();
                   obj.productPrice = Convert.ToDecimal(item["productPrice"]);
                   obj.PmJGproduct = item["PmJGproduct"]==null?0:Convert.ToDecimal(item["PmJGproduct"]);
                   obj.HuiYuanID = item["HuiYuanID"]==null?"":item["HuiYuanID"].ToString();
                   obj.AuctionTime = item["AuctionTime"] == DBNull.Value ? null : item["AuctionTime"] as DateTime?;
                   obj.TimePoint = item["TimePoint"] == null ? null : item["TimePoint"] as int?;
                   obj.CreateTime = Convert.ToDateTime(item["CreateTime"]);
                   obj.ProductDetails = item["ProductDetails"] == null ? "" : item["ProductDetails"].ToString();
                   obj.Status = (int)item["Status"];
                   obj.PriceAdd = Convert.ToDecimal(item["PriceAdd"]);
                   obj.AuctionPoint = Convert.ToInt32(item["AuctionPoint"]);
                   obj.Intro = item["Intro"].ToString();
                   obj.Fee = Convert.ToDecimal(item["Fee"]);
                   obj.ShipFee = Convert.ToDecimal(item["ShipFee"]);
                   obj.FreePoint =Convert.ToInt32(item["FreePoint"]);
                   list.Add(obj);
               }
           }
           return list;
       }

       /// <summary>
       /// 会员出价
       /// </summary>
       /// <param name="pro"></param>
       /// <returns></returns>
       public int UpdatePrice(Product pro) 
       {
           try
           {
               string sql = "update product set PmJGproduct=PmJGproduct+PriceAdd,HuiYuanID=@HuiYuanID,TimePoint=@TimePoint where ProductID=@ProductID";
               return BLLdat.ExecuteNonQuery(null,CommandType.Text,sql,new SqlParameter("@HuiYuanID",pro.HuiYuanID),
                   new SqlParameter("@TimePoint",pro.TimePoint),
                   new SqlParameter("@ProductID",pro.ProductID));
           }
           catch (Exception)
           {
               
               throw;
           }
       }

       /// <summary>
       /// 修改产品状态（成交）
       /// </summary>
       /// <param name="pro"></param>
       /// <returns></returns>
       public int UpdateStatus(Product pro)
       {
           try
           {
               string sql = "update Product set Status=@Status,EndTime=@EndTime where ProductID=@ProductID";
               return BLLdat.ExecuteNonQuery(null,CommandType.Text,sql,new SqlParameter("@EndTime",DateTime.Now),
                   new SqlParameter("@ProductID",pro.ProductID),
                   new SqlParameter("@Status",pro.Status));
           }
           catch (Exception)
           {
               
               throw;
           }
       }

       /// <summary>
       /// 出价倒计时
       /// </summary>
       /// <param name="productId"></param>
       /// <returns></returns>
       public int UpdateTimePoint(string productId) 
       {
           try
           {
               string sql = "update Product set TimePoint=TimePoint-1 where ProductID=@ProductID";
               return BLLdat.ExecuteNonQuery(null,CommandType.Text,sql,new SqlParameter("@ProductID",productId));
           }
           catch (Exception)
           {
               
               throw;
           }
       }
        #endregion
    }
}
