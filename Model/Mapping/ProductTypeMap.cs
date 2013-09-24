using System;
using System.Data.Entity.ModelConfiguration;
using System.Data.Common;
using System.Data.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Model.Entities;

namespace Model.Mapping
{
	public class ProductTypeMap : EntityTypeConfiguration<ProductType>
	{
		public ProductTypeMap()
		{
			// Primary Key
			this.HasKey(t => t.ProductTypeID);

			// Properties
			this.Property(t => t.ProductTypeID)
				.IsRequired()
				.HasMaxLength(50);
				
			this.Property(t => t.Fid)
				.HasMaxLength(50);
				
			this.Property(t => t.TypeName)
				.IsRequired()
				.HasMaxLength(100);
				
			// Table & Column Mappings
			this.ToTable("ProductType");
			this.Property(t => t.ProductTypeID).HasColumnName("ProductTypeID");
			this.Property(t => t.Fid).HasColumnName("Fid");
			this.Property(t => t.TypeName).HasColumnName("TypeName");
			this.Property(t => t.remark).HasColumnName("remark");
		}
	}
}

