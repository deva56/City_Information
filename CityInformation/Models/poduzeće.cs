namespace CityInformation.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("poduzeće")]
    public partial class poduzeće
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public poduzeće()
        {
            slikas = new HashSet<slika>();
            recenzijas = new HashSet<recenzija>();
        }

        [Key]
        public string idPoduzeće { get; set; }

        [Required(ErrorMessage = "Polje je obavezno.")]
        [StringLength(128)]
        public string imePoduzeće { get; set; }

        [StringLength(5000)]
        public string opisPoduzeće { get; set; }

        [StringLength(200)]
        public string kontaktTelefon { get; set; }

        [StringLength(200)]
        public string kontaktEmail { get; set; }

        public int? tipPoslovnogObjekta { get; set; }

        public int? gradPoduzeće { get; set; }

        public int? županijaPoduzeće { get; set; }

        public int? ulicaPoduzeće { get; set; }

        [Required(ErrorMessage = "Polje je obavezno.")]
        [StringLength(128)]
        public string korisničkoImePoduzeće { get; set; }

        public bool javanPoduzeće { get; set; }

        public virtual djelatnost djelatnost { get; set; }

        public virtual grad grad { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<slika> slikas { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<recenzija> recenzijas { get; set; }

        public virtual ulica ulica { get; set; }

        public virtual županija županija { get; set; }
    }
}
