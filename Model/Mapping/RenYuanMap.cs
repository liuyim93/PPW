using System;
using System.Data.Entity.ModelConfiguration;
using System.Data.Common;
using System.Data.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Model.Entities;

namespace Model.Mapping
{
	public class RenYuanMap : EntityTypeConfiguration<RenYuan>
	{
		public RenYuanMap()
		{
			// Primary Key
			this.HasKey(t => t.RenYuanId);

			// Properties
			this.Property(t => t.RenYuanId)
				.IsRequired()
				.HasMaxLength(50);
				
			this.Property(t => t.PersonName)
				.IsRequired()
				.HasMaxLength(100);
				
			this.Property(t => t.Sex)
				.IsRequired()
				.HasMaxLength(10);
				
			this.Property(t => t.ZhiWei)
				.HasMaxLength(50);
				
			this.Property(t => t.SFZ)
				.HasMaxLength(20);
				
			this.Property(t => t.Email)
				.HasMaxLength(100);
				
			this.Property(t => t.Mobile)
				.HasMaxLength(20);
				
			this.Property(t => t.QQ)
				.HasMaxLength(20);
				
			// Table & Column Mappings
			this.ToTable("RenYuan");
			this.Property(t => t.RenYuanId).HasColumnName("RenYuanId");
			this.Property(t => t.PersonName).HasColumnName("PersonName");
			this.Property(t => t.Sex).HasColumnName("Sex");
			this.Property(t => t.ZhiWei).HasColumnName("ZhiWei");
			this.Property(t => t.SFZ).HasColumnName("SFZ");
			this.Property(t => t.Email).HasColumnName("Email");
			this.Property(t => t.Mobile).HasColumnName("Mobile");
			this.Property(t => t.QQ).HasColumnName("QQ");
			this.Property(t => t.Remark).HasColumnName("Remark");
		}
	}
}

