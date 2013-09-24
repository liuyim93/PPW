using System;
using System.Data.Entity.ModelConfiguration;
using System.Data.Common;
using System.Data.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Model.Entities;

namespace Model.Mapping
{
	public class ProductImegMap : EntityTypeConfiguration<ProductImeg>
	{
		public ProductImegMap()
		{
			// Primary Key
			this.HasKey(t => t.ProductImegId);

			// Properties
			this.Property(t => t.ProductImegId)
				.IsRequired()
				.HasMaxLength(50);
				
			this.Property(t => t.ProductID)
				.IsRequired()
				.HasMaxLength(50);
				
			this.Property(t => t.img)
				.IsRequired()
				.HasMaxLength(200);
				
			// Table & Column Mappings
			this.ToTable("ProductImeg");
			this.Property(t => t.ProductImegId).HasColumnName("ProductImegId");
			this.Property(t => t.ProductID).HasColumnName("ProductID");
			this.Property(t => t.img).HasColumnName("img");
			this.Property(t => t.xh).HasColumnName("xh");

			// Relationships
			this.HasRequired(t => t.Product)
				.WithMany(t => t.ProductImegs)
				.HasForeignKey(d => d.ProductID);
				
		}
	}
}

