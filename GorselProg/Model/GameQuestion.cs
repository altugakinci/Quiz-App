namespace GorselProg.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GameQuestion")]
    public partial class GameQuestion
    {
        public Guid Id { get; set; }

        public Guid? GameId { get; set; }

        public Guid? QuestionId { get; set; }

        public virtual Game Game { get; set; }

        public virtual Question Question { get; set; }
    }
}
