using System;
using System.Data.Entity.ModelConfiguration;
using System.Data.Common;
using System.Data.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Model.Entities;

namespace Model.Mapping
{
	public class ErrorLogMap : EntityTypeConfiguration<ErrorLog>
	{
		public ErrorLogMap()
		{
			// Primary Key
			this.HasKey(t => t.id);

			// Properties
			this.Property(t => t.id)
				.IsRequired()
				.HasMaxLength(50);
				
			this.Property(t => t.uid)
				.HasMaxLength(50);
				
			this.Property(t => t.ip)
				.HasMaxLength(50);
				
			this.Property(t => t.url)
				.HasMaxLength(200);
				
			// Table & Column Mappings
			this.ToTable("ErrorLog");
			this.Property(t => t.id).HasColumnName("id");
			this.Property(t => t.uid).HasColumnName("uid");
			this.Property(t => t.logTime).HasColumnName("logTime");
			this.Property(t => t.ip).HasColumnName("ip");
			this.Property(t => t.url).HasColumnName("url");
			this.Property(t => t.errorDetails).HasColumnName("errorDetails");
			this.Property(t => t.remark).HasColumnName("remark");
		}
	}
}

