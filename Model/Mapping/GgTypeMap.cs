using System;
using System.Data.Entity.ModelConfiguration;
using System.Data.Common;
using System.Data.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Model.Entities;

namespace Model.Mapping
{
	public class GgTypeMap : EntityTypeConfiguration<GgType>
	{
		public GgTypeMap()
		{
			// Primary Key
			this.HasKey(t => t.GgTypeID);

			// Properties
			this.Property(t => t.GgTypeID)
				.IsRequired()
				.HasMaxLength(50);
				
			this.Property(t => t.TypeName)
				.IsRequired()
				.HasMaxLength(200);
				
			// Table & Column Mappings
			this.ToTable("GgType");
			this.Property(t => t.GgTypeID).HasColumnName("GgTypeID");
			this.Property(t => t.TypeName).HasColumnName("TypeName");
		}
	}
}

