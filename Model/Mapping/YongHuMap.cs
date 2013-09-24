using System;
using System.Data.Entity.ModelConfiguration;
using System.Data.Common;
using System.Data.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Model.Entities;

namespace Model.Mapping
{
	public class YongHuMap : EntityTypeConfiguration<YongHu>
	{
		public YongHuMap()
		{
			// Primary Key
			this.HasKey(t => t.YongHuId);

			// Properties
			this.Property(t => t.YongHuId)
				.IsRequired()
				.HasMaxLength(50);
				
			this.Property(t => t.RenYuanId)
				.IsRequired()
				.HasMaxLength(50);
				
			this.Property(t => t.YHM)
				.IsRequired()
				.HasMaxLength(50);
				
			this.Property(t => t.MM)
				.IsRequired()
				.HasMaxLength(200);
				
			this.Property(t => t.JSID)
				.HasMaxLength(50);
				
			// Table & Column Mappings
			this.ToTable("YongHu");
			this.Property(t => t.YongHuId).HasColumnName("YongHuId");
			this.Property(t => t.RenYuanId).HasColumnName("RenYuanId");
			this.Property(t => t.YHM).HasColumnName("YHM");
			this.Property(t => t.MM).HasColumnName("MM");
			this.Property(t => t.JSID).HasColumnName("JSID");
			this.Property(t => t.BZ).HasColumnName("BZ");
			this.Property(t => t.status).HasColumnName("status");

			// Relationships
			this.HasOptional(t => t.JueSe)
				.WithMany(t => t.YongHus)
				.HasForeignKey(d => d.JSID);
				
			this.HasRequired(t => t.RenYuan)
				.WithMany(t => t.YongHus)
				.HasForeignKey(d => d.RenYuanId);
				
		}
	}
}

