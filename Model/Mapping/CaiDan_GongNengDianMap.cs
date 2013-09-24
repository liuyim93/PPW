using System;
using System.Data.Entity.ModelConfiguration;
using System.Data.Common;
using System.Data.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Model.Entities;

namespace Model.Mapping
{
	public class CaiDan_GongNengDianMap : EntityTypeConfiguration<CaiDan_GongNengDian>
	{
		public CaiDan_GongNengDianMap()
		{
			// Primary Key
			this.HasKey(t => t.Id);

			// Properties
			this.Property(t => t.Id)
				.IsRequired()
				.HasMaxLength(50);
				
			this.Property(t => t.GNID)
				.IsRequired()
				.HasMaxLength(50);
				
			// Table & Column Mappings
			this.ToTable("CaiDan_GongNengDian");
			this.Property(t => t.Id).HasColumnName("Id");
			this.Property(t => t.CDID).HasColumnName("CDID");
			this.Property(t => t.GNID).HasColumnName("GNID");

			// Relationships
			this.HasRequired(t => t.CaiDan)
				.WithMany(t => t.CaiDan_GongNengDian)
				.HasForeignKey(d => d.CDID);
				
			this.HasRequired(t => t.GongNengDian)
				.WithMany(t => t.CaiDan_GongNengDian)
				.HasForeignKey(d => d.GNID);
				
		}
	}
}

