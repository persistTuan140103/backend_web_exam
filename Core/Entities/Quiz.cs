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
    [Table("Quizzes")]
    public class Quiz : EntityAuditBase<int>
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public int ExamDuration { get; set; } = 60;
        public ICollection<Question> Questions { get; } = new List<Question>();
        public ICollection<ExamRoom> ExamRooms { get; } = new List<ExamRoom>();
    }
}
