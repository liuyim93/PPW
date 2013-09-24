using System;
using System.Data.Entity.ModelConfiguration;
using System.Data.Common;
using System.Data.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Model.Entities;

namespace Model.Mapping
{
	public class GgMap : EntityTypeConfiguration<Gg>
	{
		public GgMap()
		{
			// Primary Key
			this.HasKey(t => t.GgId);

			// Properties
			this.Property(t => t.GgId)
				.IsRequired()
				.HasMaxLength(50);
				
			this.Property(t => t.Tile)
				.HasMaxLength(500);
				
			this.Property(t => t.GgTypeID)
				.HasMaxLength(50);
				
			this.Property(t => t.RenYuanId)
				.IsRequired()
				.HasMaxLength(50);
				
			// Table & Column Mappings
			this.ToTable("Gg");
			this.Property(t => t.GgId).HasColumnName("GgId");
			this.Property(t => t.Tile).HasColumnName("Tile");
			this.Property(t => t.Contents).HasColumnName("Contents");
			this.Property(t => t.GgTypeID).HasColumnName("GgTypeID");
			this.Property(t => t.RenYuanId).HasColumnName("RenYuanId");
			this.Property(t => t.CreatTime).HasColumnName("CreatTime");

			// Relationships
			this.HasOptional(t => t.GgType)
				.WithMany(t => t.Ggs)
				.HasForeignKey(d => d.GgTypeID);
				
			this.HasRequired(t => t.RenYuan)
				.WithMany(t => t.Ggs)
				.HasForeignKey(d => d.RenYuanId);
				
		}
	}
}

