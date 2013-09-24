using System;
using System.Data.Entity.ModelConfiguration;
using System.Data.Common;
using System.Data.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Model.Entities;

namespace Model.Mapping
{
	public class HfTzMap : EntityTypeConfiguration<HfTz>
	{
		public HfTzMap()
		{
			// Primary Key
			this.HasKey(t => t.HfTzId);

			// Properties
			this.Property(t => t.HfTzId)
				.IsRequired()
				.HasMaxLength(50);
				
			this.Property(t => t.GtTzID)
				.HasMaxLength(50);
				
			this.Property(t => t.HuiYuanID)
				.HasMaxLength(50);
				
			this.Property(t => t.Ip)
				.HasMaxLength(50);
				
			// Table & Column Mappings
			this.ToTable("HfTz");
			this.Property(t => t.HfTzId).HasColumnName("HfTzId");
			this.Property(t => t.GtTzID).HasColumnName("GtTzID");
			this.Property(t => t.HuiYuanID).HasColumnName("HuiYuanID");
			this.Property(t => t.Ip).HasColumnName("Ip");
			this.Property(t => t.creats).HasColumnName("creats");
			this.Property(t => t.CreateTime).HasColumnName("CreateTime");

			// Relationships
			this.HasOptional(t => t.GtTz)
				.WithMany(t => t.HfTzs)
				.HasForeignKey(d => d.GtTzID);
				
			this.HasOptional(t => t.HuiYuan)
				.WithMany(t => t.HfTzs)
				.HasForeignKey(d => d.HuiYuanID);
				
		}
	}
}

