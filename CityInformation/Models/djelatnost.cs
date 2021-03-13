namespace CityInformation.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("djelatnost")]
    public partial class djelatnost
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public djelatnost()
        {
            poduzeće = new HashSet<poduzeće>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int idDjelatnost { get; set; }

        [Required(ErrorMessage = "Polje je obavezno.")]
        [StringLength(100)]
        public string imeDjelatnost { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<poduzeće> poduzeće { get; set; }
    }
}
