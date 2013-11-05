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
                   obj.ProductTypeID = item["ProductTypeID"].ToString();
                   obj.productName = item["productName"]==null ? "" : item["productName"].ToString();
                   obj.productBrand = item["productBrand"] == null ? "" : item["productBrand"].ToString();
                   obj.productPrice = Convert.ToDecimal(item["productPrice"]);
                   obj.CreateTime = Convert.ToDateTime(item["CreateTime"]);
                   obj.isshouYei =Convert.ToInt32(item["isshouYei"]);
                   obj.Fee = Convert.ToDecimal(item["Fee"]);
                   obj.ShipFee = Convert.ToDecimal(item["ShipFee"]);
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
                   obj.ProductTypeID = item["ProductTypeID"].ToString();
                   obj.productName = item["productName"] == null ? "" : item["productName"].ToString();
                   obj.productBrand = item["productBrand"] == null ? "" : item["productBrand"].ToString();
                   obj.productPrice = Convert.ToDecimal(item["productPrice"]);
                   obj.CreateTime = Convert.ToDateTime(item["CreateTime"]);
                   obj.ProductDetails = item["ProductDetails"] == null ? "" : item["ProductDetails"].ToString();
                   obj.Intro = item["Intro"].ToString();
                   obj.Fee = Convert.ToDecimal(item["Fee"]);
                   obj.ShipFee = Convert.ToDecimal(item["ShipFee"]);
                   list.Add(obj);
               }
           }
           return list;
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
                   new SqlParameter("@ProductID",pro.ProductID)
                   //new SqlParameter("@Status",pro.Status)
                   );
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

       /// <summary>
       /// 查询竞拍类型
       /// </summary>
       /// <param name="sql"></param>
       /// <returns></returns>
       public List<AuctionType> GetAuctionType(string sql) 
       {
           List<AuctionType> list = new List<AuctionType>();
           DataSet ds = BLLdat.GetDataSet(sql);
           if (ds.Tables[0].Rows.Count>0)
           {
               foreach (DataRow row in ds.Tables[0].Rows)
               {
                   AuctionType type = new AuctionType();
                   type.AuctionTypeID=row["AuctionTypeID"].ToString();
                   type.TypeName=row["TypeName"].ToString();
                   list.Add(type);
               }
           }
           return list;
       }

        #endregion
    }
}
