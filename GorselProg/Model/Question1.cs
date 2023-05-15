namespace GorselProg
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Questions")]
    public partial class Question1
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Question1()
        {
            Games = new HashSet<Game>();
        }

        [Key]
        public Guid Id { get; set; }

        public Guid? QuestionId { get; set; }

        public Guid? GameId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Game> Games { get; set; }

        public virtual Game Game { get; set; }

        public virtual Question Question { get; set; }
    }
}
