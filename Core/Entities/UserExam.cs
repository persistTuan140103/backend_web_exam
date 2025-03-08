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
    [Table("UserExams")]
    public class UserExam : EntityAuditBase<int>
    {
        [Required]
        public float Score { get; set; } = -1;
        public DateTime CompletedAt { get; set; }
        // navigation properties
        public int UserId { get; set; }
        public int ExamRoomId { get; set; }
        public ExamRoom ExamRoom { get; set; }
        
    }
}
