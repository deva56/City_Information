namespace CityInformation.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("slika")]
    public partial class slika
    {
        [Key]
        [StringLength(200)]
        public string idSlika { get; set; }

        [Required(ErrorMessage = "Polje je obavezno.")]
        [StringLength(200)]
        public string putanjaSlike { get; set; }

        [Required(ErrorMessage = "Polje je obavezno.")]
        [StringLength(128)]
        public string idPoduzeća { get; set; }

        public virtual poduzeće poduzeće { get; set; }
    }
}
