using System;
using System.Data.Entity.ModelConfiguration;
using System.Data.Common;
using System.Data.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Model.Entities;

namespace Model.Mapping
{
	public class QuanXianMap : EntityTypeConfiguration<QuanXian>
	{
		public QuanXianMap()
		{
			// Primary Key
			this.HasKey(t => t.Id);

			// Properties
			this.Property(t => t.Id)
				.IsRequired()
				.HasMaxLength(50);
				
			this.Property(t => t.HYID)
				.IsRequired()
				.HasMaxLength(50);
				
			this.Property(t => t.JSID)
				.HasMaxLength(50);
				
			this.Property(t => t.GNDID)
				.IsRequired()
				.HasMaxLength(50);
				
			// Table & Column Mappings
			this.ToTable("QuanXian");
			this.Property(t => t.Id).HasColumnName("Id");
			this.Property(t => t.CDId).HasColumnName("CDId");
			this.Property(t => t.HYID).HasColumnName("HYID");
			this.Property(t => t.JSID).HasColumnName("JSID");
			this.Property(t => t.GNDID).HasColumnName("GNDID");

			// Relationships
			this.HasRequired(t => t.CaiDan)
				.WithMany(t => t.QuanXians)
				.HasForeignKey(d => d.CDId);
				
			this.HasRequired(t => t.GongNengDian)
				.WithMany(t => t.QuanXians)
				.HasForeignKey(d => d.GNDID);
				
			this.HasOptional(t => t.JueSe)
				.WithMany(t => t.QuanXians)
				.HasForeignKey(d => d.JSID);
				
			this.HasRequired(t => t.YongHu)
				.WithMany(t => t.QuanXians)
				.HasForeignKey(d => d.HYID);
				
		}
	}
}

