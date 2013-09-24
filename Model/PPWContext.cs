using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Model.Entities;
using Model.Mapping;

namespace Model
{
	public class PPWContext : DbContext
	{
		static PPWContext()
		{ 
			Database.SetInitializer<PPWContext>(null);
		}

		public DbSet<CaiDan> CaiDans { get; set; }
		public DbSet<CaiDan_GongNengDian> CaiDan_GongNengDian { get; set; }
		public DbSet<ChuJiaJiLu> ChuJiaJiLus { get; set; }
		public DbSet<DingDan> DingDans { get; set; }
		public DbSet<ErrorLog> ErrorLogs { get; set; }
		public DbSet<Gg> Ggs { get; set; }
		public DbSet<GgType> GgTypes { get; set; }
		public DbSet<GongNengDian> GongNengDians { get; set; }
		public DbSet<GsXX> GsXXes { get; set; }
		public DbSet<GtTz> GtTzs { get; set; }
		public DbSet<HfTz> HfTzs { get; set; }
		public DbSet<HuiYuan> HuiYuans { get; set; }
		public DbSet<JueSe> JueSes { get; set; }
		public DbSet<PaiDianJl> PaiDianJls { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<ProductImeg> ProductImegs { get; set; }
		public DbSet<ProductType> ProductTypes { get; set; }
		public DbSet<QuanXian> QuanXians { get; set; }
		public DbSet<RenYuan> RenYuans { get; set; }
		public DbSet<ShouHuoDZ> ShouHuoDZs { get; set; }
		public DbSet<Tz> Tzs { get; set; }
		public DbSet<YongHu> YongHus { get; set; }
		public DbSet<YouQinLianJi> YouQinLianJis { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
            modelBuilder.Conventions.Remove<IncludeMetadataConvention>();
			modelBuilder.Configurations.Add(new CaiDanMap());
			modelBuilder.Configurations.Add(new CaiDan_GongNengDianMap());
			modelBuilder.Configurations.Add(new ChuJiaJiLuMap());
			modelBuilder.Configurations.Add(new DingDanMap());
			modelBuilder.Configurations.Add(new ErrorLogMap());
			modelBuilder.Configurations.Add(new GgMap());
			modelBuilder.Configurations.Add(new GgTypeMap());
			modelBuilder.Configurations.Add(new GongNengDianMap());
			modelBuilder.Configurations.Add(new GsXXMap());
			modelBuilder.Configurations.Add(new GtTzMap());
			modelBuilder.Configurations.Add(new HfTzMap());
			modelBuilder.Configurations.Add(new HuiYuanMap());
			modelBuilder.Configurations.Add(new JueSeMap());
			modelBuilder.Configurations.Add(new PaiDianJlMap());
			modelBuilder.Configurations.Add(new ProductMap());
			modelBuilder.Configurations.Add(new ProductImegMap());
			modelBuilder.Configurations.Add(new ProductTypeMap());
			modelBuilder.Configurations.Add(new QuanXianMap());
			modelBuilder.Configurations.Add(new RenYuanMap());
			modelBuilder.Configurations.Add(new ShouHuoDZMap());
			modelBuilder.Configurations.Add(new TzMap());
			modelBuilder.Configurations.Add(new YongHuMap());
			modelBuilder.Configurations.Add(new YouQinLianJiMap());
		}
	}
}

