using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GorselProg.Model
{
    class Question
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string QuestionText { get; set; }
        [Required]
        public string OptionsText { get; set; }
        [Required]
        public string AnswerText { get; set; }
        [Required]
        public virtual ICollection<Category> Categories { get; set; }
    }
}
