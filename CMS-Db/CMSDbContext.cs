using CMS_Db.Entity;
//using CMS_Entity.Db;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Db
{
	public class CMSDbContext : IdentityDbContext<CMS_Db.Entity.User>
	{
		public CMSDbContext()
			: base("CMS", throwIfV1Schema: false)
		{
		}

		public static CMSDbContext Create()
		{
			return new CMSDbContext();
		}

		public virtual DbSet<Country> Countries { get; set; }
		public virtual DbSet<FavoriteUserCountry> FavoriteUserCountries { get; set; }
		public virtual DbSet<Log> Logs { get; set; }

		protected override void OnModelCreating(System.Data.Entity.DbModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<IdentityUser>().ToTable("User");
			modelBuilder.Entity<CMS_Db.Entity.User>().ToTable("User");

			modelBuilder.Entity<IdentityRole>().ToTable("Role");
			modelBuilder.Entity<IdentityUserRole>().ToTable("UserRole");
			modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaim");
			modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogin");

			modelBuilder.Entity<Country>()
				.HasMany(e => e.FavoriteUserCountries)
				.WithRequired(e => e.Country)
				.WillCascadeOnDelete(false);

			//modelBuilder.Entity<User>()
			//    .HasMany(e => e.FavoriteUserCountries)
			//    .WithRequired(e => e.User)
			//    .WillCascadeOnDelete(false);

		}
	}
}
