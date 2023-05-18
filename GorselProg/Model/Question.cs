namespace GorselProg.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Question")]
    public partial class Question
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Question()
        {
            Answers = new HashSet<Answer>();
        }

        public Guid Id { get; set; }

        [Required]
        
        public string QuestionText { get; set; }

        [Required]
        
        public string OptionsText { get; set; }

        [Required]
        public int  CorrectAnswerIndex { get; set; }

        public Guid? CategoryId { get; set; }

        public Guid? GameId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Answer> Answers { get; set; }

        public virtual Category Category { get; set; }

        public virtual Game Game { get; set; }
    }
}
