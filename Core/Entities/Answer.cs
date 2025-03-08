using Core.Abstraction;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    [Table("Answers")]
    public class Answer : EntityAuditBase<int>
    {
        [Required]
        public string AnswerText { get; set; }
        [Required]
        public bool IsCorrect { get; set; } = false;
        [Required]
        public int QuestionId { get; set; }
        public Question Question { get; set; }
    }
}
