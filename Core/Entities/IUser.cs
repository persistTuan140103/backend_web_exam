using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public interface IUser<TId>
    {
        TId Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool Gener { get; set; }
        public string? EducationLevel { get; set; }
        //public ICollection<ExamRoom> ExamRooms { get; }
        //public ICollection<UserExam> UserExams { get; }
        //public ICollection<Wallet> Wallets { get; set; }
    }
}
