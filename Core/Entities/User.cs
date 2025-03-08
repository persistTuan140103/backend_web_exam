using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class User : IUser<int>
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool Gener { get; set; }
        public string? EducationLevel { get; set; }

        //public ICollection<ExamRoom> ExamRooms { get; } = new List<ExamRoom>();

        //public ICollection<UserExam> UserExams { get; } = new List<UserExam>();
        //public ICollection<Wallet> Wallets { get; set; } = new List<Wallet>();
    }
}
