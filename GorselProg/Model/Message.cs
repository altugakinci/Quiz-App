namespace GorselProg.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Message")]
    public partial class Message
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(255)]
        public string MessageText { get; set; }

        public DateTime SentTime { get; set; }

        public Guid? UserId { get; set; }

        public Guid? RoomId { get; set; }

        public virtual Room Room { get; set; }

        public virtual User User { get; set; }
    }
}
