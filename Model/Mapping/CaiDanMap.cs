using System;
using System.Data.Entity.ModelConfiguration;
using System.Data.Common;
using System.Data.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Model.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Mapping
{
	public class CaiDanMap : EntityTypeConfiguration<CaiDan>
	{
		public CaiDanMap()
		{
			// Primary Key
			this.HasKey(t => t.Id);

			// Properties
			this.Property(t => t.Id)
				.HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
				
			this.Property(t => t.Name)
				.IsRequired()
				.HasMaxLength(50);
				
			this.Property(t => t.Url)
				.HasMaxLength(500);
				
			// Table & Column Mappings
			this.ToTable("CaiDan");
			this.Property(t => t.Id).HasColumnName("Id");
			this.Property(t => t.Parent_Id).HasColumnName("Parent_Id");
			this.Property(t => t.Name).HasColumnName("Name");
			this.Property(t => t.Url).HasColumnName("Url");
			this.Property(t => t.Is_Show).HasColumnName("Is_Show");
			this.Property(t => t.OrderNum).HasColumnName("OrderNum");
		}
	}
}

