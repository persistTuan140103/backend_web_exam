using Core.Abstraction;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    [Table("ExamRooms")]
    public class ExamRoom : EntityAuditBase<int>
    {
        public string RooName { get; set; }
        public DateTime StartTime{ get; set; }
        public DateTime EndTime { get; set; }
        public int QuizId { get; set; }
        public Quiz Quizzes { get; set; }
        public ICollection<UserExam> UserExams { get; } = new List<UserExam>();

    }
}
