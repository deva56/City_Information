namespace CityInformation.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("korisnik")]
    public partial class korisnik
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public korisnik()
        {
            recenzijas = new HashSet<recenzija>();
        }

        [Key]
        public string idKorisnik { get; set; }

        [Required(ErrorMessage = "Polje je obavezno.")]
        [StringLength(128)]
        public string registracijskiEmailKorisnik { get; set; }

        [StringLength(200)]
        public string putanjaDoProfilneSlike { get; set; }

        [Required(ErrorMessage = "Polje je obavezno.")]
        [StringLength(128)]
        public string korisničkoImeKorisnik { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<recenzija> recenzijas { get; set; }
    }
}
