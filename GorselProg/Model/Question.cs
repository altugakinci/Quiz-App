namespace GorselProg
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
            Answers = new HashSet<Answer1>();
            Questions = new HashSet<Question1>();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        public string QuestionText { get; set; }

        [Required]
        public string OptionsText { get; set; }

        [Required]
        public int CorrectAnswerIndex { get; set; }

        public Guid? CategoryId { get; set; }

        public Guid? AnswersId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Answer1> Answers { get; set; }

        public virtual Answer1 Answer { get; set; }

        public virtual Category Category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Question1> Questions { get; set; }
    }
}
