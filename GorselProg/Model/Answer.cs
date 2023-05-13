using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GorselProg.Model
{
    class Answer
    {
        [Key]
        public int Id { get; set; }

        public string AnswerText { get; set; }

        public virtual User User { get; set; }

        public virtual Question Question { get; set; }

        public virtual Game Game { get; set; }

        public virtual Room Room { get; set; }
    }
}
