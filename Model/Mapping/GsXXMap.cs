using System;
using System.Data.Entity.ModelConfiguration;
using System.Data.Common;
using System.Data.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Model.Entities;

namespace Model.Mapping
{
	public class GsXXMap : EntityTypeConfiguration<GsXX>
	{
		public GsXXMap()
		{
			// Primary Key
			this.HasKey(t => t.GsXXId);

			// Properties
			this.Property(t => t.GsXXId)
				.IsRequired()
				.HasMaxLength(50);
				
			this.Property(t => t.SgName)
				.IsRequired()
				.HasMaxLength(100);
				
			this.Property(t => t.Mode)
				.HasMaxLength(50);
				
			this.Property(t => t.Dz)
				.IsRequired()
				.HasMaxLength(500);
				
			this.Property(t => t.WZ)
				.HasMaxLength(500);
				
			this.Property(t => t.banQuanSY)
				.HasMaxLength(500);
				
			// Table & Column Mappings
			this.ToTable("GsXX");
			this.Property(t => t.GsXXId).HasColumnName("GsXXId");
			this.Property(t => t.SgName).HasColumnName("SgName");
			this.Property(t => t.Mode).HasColumnName("Mode");
			this.Property(t => t.Dz).HasColumnName("Dz");
			this.Property(t => t.WZ).HasColumnName("WZ");
			this.Property(t => t.banQuanSY).HasColumnName("banQuanSY");
		}
	}
}

