using System;
using System.Data.Entity.ModelConfiguration;
using System.Data.Common;
using System.Data.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Model.Entities;

namespace Model.Mapping
{
	public class JueSeMap : EntityTypeConfiguration<JueSe>
	{
		public JueSeMap()
		{
			// Primary Key
			this.HasKey(t => t.JueSeId);

			// Properties
			this.Property(t => t.JueSeId)
				.IsRequired()
				.HasMaxLength(50);
				
			this.Property(t => t.JSMC)
				.IsRequired()
				.HasMaxLength(50);
				
			// Table & Column Mappings
			this.ToTable("JueSe");
			this.Property(t => t.JueSeId).HasColumnName("JueSeId");
			this.Property(t => t.JSMC).HasColumnName("JSMC");
			this.Property(t => t.Remark).HasColumnName("Remark");
			this.Property(t => t.status).HasColumnName("status");
		}
	}
}

