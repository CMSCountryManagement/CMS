namespace CMS_Entity.Db
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Log")]
    public partial class Log
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Cid { get; set; }

        public Guid Id { get; set; }

        [Required]
        [StringLength(128)]
        public string ControllerName { get; set; }

        [Required]
        [StringLength(128)]
        public string ActionName { get; set; }

        [Required]
        public string Parameter { get; set; }

        [Required]
        public string RequestUrl { get; set; }

        [Required]
        [StringLength(128)]
        public string Ip { get; set; }

        [Required]
        [StringLength(128)]
        public string Activity { get; set; }

        [Required]
        [StringLength(128)]
        public string Description { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? LogInOn { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? LogOutOn { get; set; }

        [StringLength(128)]
        public string UserId { get; set; }

        public Guid? CountryId { get; set; }

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
