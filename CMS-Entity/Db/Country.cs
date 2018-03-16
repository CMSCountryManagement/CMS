namespace CMS_Entity.Db
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Country")]
    public partial class Country
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Country()
        {
            FavoriteUserCountries = new HashSet<FavoriteUserCountry>();
            Logs = new HashSet<Log>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Cid { get; set; }

        public Guid Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        public string Alpha2Code { get; set; }

        public string Alpha3Code { get; set; }

        [StringLength(150)]
        public string Capital { get; set; }

        [StringLength(150)]
        public string Region { get; set; }

        [StringLength(150)]
        public string Subregion { get; set; }

        public long? Population { get; set; }

        public string Demonym { get; set; }

        public decimal? Area { get; set; }

        public decimal? Gini { get; set; }

        [StringLength(150)]
        public string NativeName { get; set; }

        [StringLength(150)]
        public string NumericCode { get; set; }

        public string Flag { get; set; }

        public string Cioc { get; set; }

        public string Latlng { get; set; }

        public string TopLevelDomain { get; set; }

        public string CallingCodes { get; set; }

        public string AltSpellings { get; set; }

        public string Timezones { get; set; }

        public string Borders { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreatedOn { get; set; }

        public string ModifiedBy { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? ModifiedOn { get; set; }

        public string DeletedBy { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? DeletedOn { get; set; }

        public long? SearchCount { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FavoriteUserCountry> FavoriteUserCountries { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Log> Logs { get; set; }
    }
}
