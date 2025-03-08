using Core.Abstraction;
using Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    [Table("Questions")]
    public class Question : EntityBase<int>
    {
        [Required]
        public string QuestionText { get; set; }
        public QuestionType Type{ get; set; }
        public LevelQuestion DifficultLevel { get; set; }
        public int QuizId { get; set; }
        public Quiz Quiz { get; set; }
        public ICollection<Answer> Answers { get; } = new List<Answer>();
    }
}
