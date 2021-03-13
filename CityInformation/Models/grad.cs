namespace CityInformation.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("grad")]
    public partial class grad
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public grad()
        {
            poduzeće = new HashSet<poduzeće>();
            ulicas = new HashSet<ulica>();
        }

        [Required(ErrorMessage = "Polje je obavezno.")]
        [StringLength(128)]
        public string imeGrad { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int idGrad { get; set; }

        [Required(ErrorMessage = "Polje je obavezno.")]
        public int idŽupanija { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<poduzeće> poduzeće { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ulica> ulicas { get; set; }

        public virtual županija županija { get; set; }
    }
}
