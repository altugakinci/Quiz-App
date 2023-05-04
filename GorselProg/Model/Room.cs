using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GorselProg.Model
{
    class Room
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }

        public virtual Game CurrentGame { get; set; }

        public virtual User Admin { get; set; }

        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Game> Games { get; set; }
        public virtual ICollection<Chat> Chats { get; set; }

        public virtual ICollection<User> BannedUsers { get; set; }
    }
}
