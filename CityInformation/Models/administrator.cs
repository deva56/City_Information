namespace CityInformation.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("administrator")]
    public partial class administrator
    {
        [Key]
        public string idAdministratora { get; set; }

        [Required(ErrorMessage = "Polje je obavezno.")]
        [StringLength(128)]
        public string imeAdministratora { get; set; }
    }
}
