using System;
using System.Data.Entity.ModelConfiguration;
using System.Data.Common;
using System.Data.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Model.Entities;

namespace Model.Mapping
{
	public class PaiDianJlMap : EntityTypeConfiguration<PaiDianJl>
	{
		public PaiDianJlMap()
		{
			// Primary Key
			this.HasKey(t => t.PaiDianJlId);

			// Properties
			this.Property(t => t.PaiDianJlId)
				.IsRequired()
				.HasMaxLength(50);
				
			this.Property(t => t.HuiYuanID)
				.IsRequired()
				.HasMaxLength(50);
				
			// Table & Column Mappings
			this.ToTable("PaiDianJl");
			this.Property(t => t.PaiDianJlId).HasColumnName("PaiDianJlId");
			this.Property(t => t.ChongZhi).HasColumnName("ChongZhi");
			this.Property(t => t.HuiYuanID).HasColumnName("HuiYuanID");
			this.Property(t => t.CreateTime).HasColumnName("CreateTime");

			// Relationships
			this.HasRequired(t => t.HuiYuan)
				.WithMany(t => t.PaiDianJls)
				.HasForeignKey(d => d.HuiYuanID);
				
		}
	}
}

