using System;
using System.Collections.Generic;
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
        public string userName { get; set; }

        [Required]
        [MaxLength(100)]
        public string email { get; set; }

        [Required]
        public string password { get; set; }

        [Required]
        public string salt { get; set; }
    }
}
