using System;
using System.Data.Entity.ModelConfiguration;
using System.Data.Common;
using System.Data.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Model.Entities;

namespace Model.Mapping
{
	public class DingDanMap : EntityTypeConfiguration<DingDan>
	{
		public DingDanMap()
		{
			// Primary Key
			this.HasKey(t => t.DingDanID);

			// Properties
			this.Property(t => t.DingDanID)
				.IsRequired()
				.HasMaxLength(50);
				
			this.Property(t => t.DingDanBH)
				.IsRequired()
				.HasMaxLength(50);
				
			this.Property(t => t.HuiYuanID)
				.IsRequired()
				.HasMaxLength(50);
				
			this.Property(t => t.ProductID)
				.IsRequired()
				.HasMaxLength(50);
				
			// Table & Column Mappings
			this.ToTable("DingDan");
			this.Property(t => t.DingDanID).HasColumnName("DingDanID");
			this.Property(t => t.DingDanBH).HasColumnName("DingDanBH");
			this.Property(t => t.HuiYuanID).HasColumnName("HuiYuanID");
			this.Property(t => t.ProductID).HasColumnName("ProductID");
			this.Property(t => t.DingDanTime).HasColumnName("DingDanTime");
			this.Property(t => t.Status).HasColumnName("Status");

			// Relationships
			this.HasRequired(t => t.HuiYuan)
				.WithMany(t => t.DingDans)
				.HasForeignKey(d => d.HuiYuanID);
				
			this.HasRequired(t => t.Product)
				.WithMany(t => t.DingDans)
				.HasForeignKey(d => d.ProductID);
				
		}
	}
}

