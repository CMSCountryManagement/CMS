namespace CMS_Db.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FavoriteUserCountry")]
    public partial class FavoriteUserCountry
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Cid { get; set; }

        public Guid Id { get; set; }

        [Required]
        [StringLength(128)]
        public string UserId { get; set; }

        public Guid CountryId { get; set; }

        public bool IsFavorite { get; set; }

        public string CreatedBy { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreatedOn { get; set; }

        public string ModifiedBy { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? ModifiedOn { get; set; }

        public string DeletedBy { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? DeletedOn { get; set; }

        public virtual Country Country { get; set; }

        public virtual User User { get; set; }
    }
}
