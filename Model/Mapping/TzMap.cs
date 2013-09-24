using System;
using System.Data.Entity.ModelConfiguration;
using System.Data.Common;
using System.Data.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Model.Entities;

namespace Model.Mapping
{
	public class TzMap : EntityTypeConfiguration<Tz>
	{
		public TzMap()
		{
			// Primary Key
			this.HasKey(t => t.TzID);

			// Properties
			this.Property(t => t.TzID)
				.IsRequired()
				.HasMaxLength(50);
				
			this.Property(t => t.HuiYuanID)
				.HasMaxLength(50);
				
			this.Property(t => t.Ip)
				.HasMaxLength(50);
				
			// Table & Column Mappings
			this.ToTable("Tz");
			this.Property(t => t.TzID).HasColumnName("TzID");
			this.Property(t => t.Tile).HasColumnName("Tile");
			this.Property(t => t.Creatr).HasColumnName("Creatr");
			this.Property(t => t.HuiYuanID).HasColumnName("HuiYuanID");
			this.Property(t => t.Ip).HasColumnName("Ip");
			this.Property(t => t.CreateTime).HasColumnName("CreateTime");

			// Relationships
			this.HasOptional(t => t.HuiYuan)
				.WithMany(t => t.Tzs)
				.HasForeignKey(d => d.HuiYuanID);
				
		}
	}
}

