using System;
using System.Data.Entity.ModelConfiguration;
using System.Data.Common;
using System.Data.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Model.Entities;

namespace Model.Mapping
{
	public class YouQinLianJiMap : EntityTypeConfiguration<YouQinLianJi>
	{
		public YouQinLianJiMap()
		{
			// Primary Key
			this.HasKey(t => t.YouQinLianJiId);

			// Properties
			this.Property(t => t.YouQinLianJiId)
				.IsRequired()
				.HasMaxLength(50);
				
			this.Property(t => t.GsName)
				.HasMaxLength(500);
				
			this.Property(t => t.img)
				.HasMaxLength(500);
				
			this.Property(t => t.Url)
				.HasMaxLength(100);
				
			this.Property(t => t.DispType)
				.IsRequired()
				.HasMaxLength(50);
				
			// Table & Column Mappings
			this.ToTable("YouQinLianJi");
			this.Property(t => t.YouQinLianJiId).HasColumnName("YouQinLianJiId");
			this.Property(t => t.GsName).HasColumnName("GsName");
			this.Property(t => t.img).HasColumnName("img");
			this.Property(t => t.Url).HasColumnName("Url");
			this.Property(t => t.DispType).HasColumnName("DispType");
			this.Property(t => t.xh).HasColumnName("xh");
		}
	}
}

