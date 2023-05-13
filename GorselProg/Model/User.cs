using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GorselProg.Model
{
    class User
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Salt { get; set; }
        
        [DefaultValue(1)]
        public int Level { get; set; }

        [DefaultValue(0)]
        public int Xp { get; set; }
    }
}
