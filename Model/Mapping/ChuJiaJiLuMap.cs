using System;
using System.Data.Entity.ModelConfiguration;
using System.Data.Common;
using System.Data.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Model.Entities;

namespace Model.Mapping
{
	public class ChuJiaJiLuMap : EntityTypeConfiguration<ChuJiaJiLu>
	{
		public ChuJiaJiLuMap()
		{
			// Primary Key
			this.HasKey(t => t.ChuJiaJiLuID);

			// Properties
			this.Property(t => t.ChuJiaJiLuID)
				.IsRequired()
				.HasMaxLength(50);
				
            //this.Property(t => t.ProductID)
            //    .IsRequired()
            //    .HasMaxLength(50);
				
			this.Property(t => t.HuiYuanID)
				.IsRequired()
				.HasMaxLength(50);
				
			// Table & Column Mappings
			this.ToTable("ChuJiaJiLu");
			this.Property(t => t.ChuJiaJiLuID).HasColumnName("ChuJiaJiLuID");
            //this.Property(t => t.ProductID).HasColumnName("ProductID");
			this.Property(t => t.HuiYuanID).HasColumnName("HuiYuanID");
			this.Property(t => t.Status).HasColumnName("Status");

			// Relationships
			this.HasRequired(t => t.HuiYuan)
				.WithMany(t => t.ChuJiaJiLus)
				.HasForeignKey(d => d.HuiYuanID);
				
            //this.HasRequired(t => t.Product)
            //    .WithMany(t => t.ChuJiaJiLus)
            //    .HasForeignKey(d => d.ProductID);
				
		}
	}
}

