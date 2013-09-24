using System;
using System.Data.Entity.ModelConfiguration;
using System.Data.Common;
using System.Data.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Model.Entities;

namespace Model.Mapping
{
	public class GtTzMap : EntityTypeConfiguration<GtTz>
	{
		public GtTzMap()
		{
			// Primary Key
			this.HasKey(t => t.GtTzID);

			// Properties
			this.Property(t => t.GtTzID)
				.IsRequired()
				.HasMaxLength(50);
				
			this.Property(t => t.TzID)
				.HasMaxLength(50);
				
			this.Property(t => t.HuiYuanID)
				.HasMaxLength(50);
				
			this.Property(t => t.Ip)
				.HasMaxLength(50);
				
			// Table & Column Mappings
			this.ToTable("GtTz");
			this.Property(t => t.GtTzID).HasColumnName("GtTzID");
			this.Property(t => t.TzID).HasColumnName("TzID");
			this.Property(t => t.HuiYuanID).HasColumnName("HuiYuanID");
			this.Property(t => t.Ip).HasColumnName("Ip");
			this.Property(t => t.Contents).HasColumnName("Contents");
			this.Property(t => t.CreateTime).HasColumnName("CreateTime");

			// Relationships
			this.HasOptional(t => t.HuiYuan)
				.WithMany(t => t.GtTzs)
				.HasForeignKey(d => d.HuiYuanID);
				
			this.HasOptional(t => t.Tz)
				.WithMany(t => t.GtTzs)
				.HasForeignKey(d => d.TzID);
				
		}
	}
}

