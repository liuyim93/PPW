using System;
using System.Data.Entity.ModelConfiguration;
using System.Data.Common;
using System.Data.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Model.Entities;

namespace Model.Mapping
{
	public class ShouHuoDZMap : EntityTypeConfiguration<ShouHuoDZ>
	{
		public ShouHuoDZMap()
		{
			// Primary Key
			this.HasKey(t => t.ShouHuoDZID);

			// Properties
			this.Property(t => t.ShouHuoDZID)
				.IsRequired()
				.HasMaxLength(50);
				
			this.Property(t => t.HuiYuanID)
				.IsRequired()
				.HasMaxLength(50);
				
			this.Property(t => t.ShouHuoName)
				.HasMaxLength(50);
				
			this.Property(t => t.Mode)
				.HasMaxLength(50);
				
			this.Property(t => t.DZ)
				.HasMaxLength(300);
				
			this.Property(t => t.YouBian)
				.HasMaxLength(10);
				
			// Table & Column Mappings
			this.ToTable("ShouHuoDZ");
			this.Property(t => t.ShouHuoDZID).HasColumnName("ShouHuoDZID");
			this.Property(t => t.HuiYuanID).HasColumnName("HuiYuanID");
			this.Property(t => t.ShouHuoName).HasColumnName("ShouHuoName");
			this.Property(t => t.Mode).HasColumnName("Mode");
			this.Property(t => t.DZ).HasColumnName("DZ");
			this.Property(t => t.YouBian).HasColumnName("YouBian");

			// Relationships
			this.HasRequired(t => t.HuiYuan)
				.WithMany(t => t.ShouHuoDZs)
				.HasForeignKey(d => d.HuiYuanID);
				
		}
	}
}

