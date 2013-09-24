using System;
using System.Data.Entity.ModelConfiguration;
using System.Data.Common;
using System.Data.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Model.Entities;

namespace Model.Mapping
{
	public class GongNengDianMap : EntityTypeConfiguration<GongNengDian>
	{
		public GongNengDianMap()
		{
			// Primary Key
			this.HasKey(t => t.Id);

			// Properties
			this.Property(t => t.Id)
				.IsRequired()
				.HasMaxLength(50);
				
			this.Property(t => t.Tag)
				.IsRequired()
				.HasMaxLength(50);
				
			this.Property(t => t.name)
				.IsRequired()
				.HasMaxLength(20);
				
			// Table & Column Mappings
			this.ToTable("GongNengDian");
			this.Property(t => t.Id).HasColumnName("Id");
			this.Property(t => t.Tag).HasColumnName("Tag");
			this.Property(t => t.name).HasColumnName("name");
		}
	}
}

