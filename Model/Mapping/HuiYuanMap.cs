using System;
using System.Data.Entity.ModelConfiguration;
using System.Data.Common;
using System.Data.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Model.Entities;

namespace Model.Mapping
{
	public class HuiYuanMap : EntityTypeConfiguration<HuiYuan>
	{
		public HuiYuanMap()
		{
			// Primary Key
			this.HasKey(t => t.HuiYuanID);

			// Properties
			this.Property(t => t.HuiYuanID)
				.IsRequired()
				.HasMaxLength(50);
				
			this.Property(t => t.HuiYuanName)
				.HasMaxLength(50);
				
			this.Property(t => t.MM)
				.HasMaxLength(50);
				
			this.Property(t => t.email)
				.HasMaxLength(100);
				
			this.Property(t => t.prName)
				.HasMaxLength(50);
				
			this.Property(t => t.sex)
				.HasMaxLength(20);
				
			this.Property(t => t.sfz)
				.HasMaxLength(50);
				
			this.Property(t => t.sjh)
				.HasMaxLength(50);
				
			this.Property(t => t.DJ)
				.HasMaxLength(50);
				
			// Table & Column Mappings
			this.ToTable("HuiYuan");
			this.Property(t => t.HuiYuanID).HasColumnName("HuiYuanID");
			this.Property(t => t.HuiYuanName).HasColumnName("HuiYuanName");
			this.Property(t => t.MM).HasColumnName("MM");
			this.Property(t => t.email).HasColumnName("email");
			this.Property(t => t.prName).HasColumnName("prName");
			this.Property(t => t.sex).HasColumnName("sex");
			this.Property(t => t.sfz).HasColumnName("sfz");
			this.Property(t => t.sjh).HasColumnName("sjh");
			this.Property(t => t.PaiDian).HasColumnName("PaiDian");
			this.Property(t => t.JiFen).HasColumnName("JiFen");
			this.Property(t => t.DJ).HasColumnName("DJ");
			this.Property(t => t.CreatTime).HasColumnName("CreatTime");
			this.Property(t => t.LoginTime).HasColumnName("LoginTime");
		}
	}
}

