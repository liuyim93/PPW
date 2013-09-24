using System;
using System.Data.Entity.ModelConfiguration;
using System.Data.Common;
using System.Data.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Model.Entities;

namespace Model.Mapping
{
	public class ProductMap : EntityTypeConfiguration<Product>
	{
		public ProductMap()
		{
			// Primary Key
			this.HasKey(t => t.ProductID);

			// Properties
			this.Property(t => t.ProductID)
				.IsRequired()
				.HasMaxLength(50);
				
			this.Property(t => t.coding)
				.HasMaxLength(50);
				
			this.Property(t => t.ProductTypeID)
				.IsRequired()
				.HasMaxLength(50);
				
			this.Property(t => t.productName)
				.IsRequired()
				.HasMaxLength(500);
				
			this.Property(t => t.productBrand)
				.HasMaxLength(350);
				
			this.Property(t => t.HuiYuanID)
				.HasMaxLength(50);
				
			// Table & Column Mappings
			this.ToTable("Product");
			this.Property(t => t.ProductID).HasColumnName("ProductID");
			this.Property(t => t.coding).HasColumnName("coding");
			this.Property(t => t.ProductTypeID).HasColumnName("ProductTypeID");
			this.Property(t => t.productName).HasColumnName("productName");
			this.Property(t => t.productBrand).HasColumnName("productBrand");
			this.Property(t => t.isshouYei).HasColumnName("isshouYei");
			this.Property(t => t.productPrice).HasColumnName("productPrice");
			this.Property(t => t.PmJGproduct).HasColumnName("PmJGproduct");
			this.Property(t => t.ChenJiaoJiaGe).HasColumnName("ChenJiaoJiaGe");
			this.Property(t => t.HuiYuanID).HasColumnName("HuiYuanID");
			this.Property(t => t.AuctionTime).HasColumnName("AuctionTime");
			this.Property(t => t.TimePoint).HasColumnName("TimePoint");
			this.Property(t => t.CreateTime).HasColumnName("CreateTime");
			this.Property(t => t.ProductDetails).HasColumnName("ProductDetails");
			this.Property(t => t.Status).HasColumnName("Status");

			// Relationships
			this.HasOptional(t => t.HuiYuan)
				.WithMany(t => t.Products)
				.HasForeignKey(d => d.HuiYuanID);
				
			this.HasRequired(t => t.ProductType)
				.WithMany(t => t.Products)
				.HasForeignKey(d => d.ProductTypeID);
				
		}
	}
}

