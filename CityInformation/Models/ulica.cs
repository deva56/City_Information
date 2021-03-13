namespace CityInformation.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ulica")]
    public partial class ulica
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ulica()
        {
            poduzeće = new HashSet<poduzeće>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int idUlica { get; set; }

        [Required(ErrorMessage = "Polje je obavezno.")]
        [StringLength(256)]
        public string imeUlica { get; set; }

        [Required(ErrorMessage = "Polje je obavezno.")]
        public int idGrad { get; set; }

        public virtual grad grad { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<poduzeće> poduzeće { get; set; }
    }
}
