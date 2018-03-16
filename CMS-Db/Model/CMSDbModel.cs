namespace CMS_Entity.Db
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class CMSDbModel : DbContext
    {
        public CMSDbModel()
            : base("name=CMSDbModel")
        {
        }

        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<FavoriteUserCountry> FavoriteUserCountries { get; set; }
        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserClaim> UserClaims { get; set; }
        public virtual DbSet<UserLogin> UserLogins { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>()
                .HasMany(e => e.FavoriteUserCountries)
                .WithRequired(e => e.Country)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.FavoriteUserCountries)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.UserClaims)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.IdentityUser_Id);

            modelBuilder.Entity<User>()
                .HasMany(e => e.UserLogins)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.IdentityUser_Id);

            modelBuilder.Entity<User>()
                .HasMany(e => e.UserRoles)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.IdentityUser_Id);
        }
    }
}
