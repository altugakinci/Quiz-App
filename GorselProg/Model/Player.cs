namespace GorselProg.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Player
    {
        public Guid Id { get; set; }

        public Guid? RoomId { get; set; }

        public Guid? UserId { get; set; }

        public virtual Room Room { get; set; }

        public virtual User User { get; set; }
    }
}
