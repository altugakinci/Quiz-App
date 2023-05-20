namespace GorselProg.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Answer")]
    public partial class Answer
    {
        public Guid Id { get; set; }

        [Required]
        public string AnswerText { get; set; }

        public int GainedXp { get; set; }

        public Guid? UserId { get; set; }

        public Guid? QuestionId { get; set; }

        public Guid? GameId { get; set; }

        public virtual Game Game { get; set; }

        public virtual Question Question { get; set; }

        public virtual User User { get; set; }
    }
}
