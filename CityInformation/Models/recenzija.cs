namespace CityInformation.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("recenzija")]
    public partial class recenzija
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int idRecenzija { get; set; }

        [Required(ErrorMessage = "Polje je obavezno.")]
        [StringLength(500)]
        public string tekstRecenzija { get; set; }

        [Required(ErrorMessage = "Polje je obavezno.")]
        [StringLength(128)]
        public string vlasnikRecenzijaID { get; set; }

        public bool odobrenoRecenzija { get; set; }

        [Required(ErrorMessage = "Polje je obavezno.")]
        [StringLength(100)]
        public string datumRecenzija { get; set; }

        [Required(ErrorMessage = "Polje je obavezno.")]
        [StringLength(128)]
        public string poduzećeRecenzijaID { get; set; }

        public virtual korisnik korisnik { get; set; }

        public virtual poduzeće poduzeće { get; set; }
    }
}
