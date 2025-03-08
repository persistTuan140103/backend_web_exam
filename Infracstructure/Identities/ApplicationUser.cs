using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infracstructure.Identities
{
    [Table("Users")]
    public class ApplicationUser : IdentityUser<int>
    {
        public ICollection<UserExam> UserExams { get; } = new List<UserExam>();
        public bool Gener { get; set; }
        public string? EducationLevel { get; set; }
        public ICollection<Wallet> Wallets { get; set; } = new List<Wallet>();
    }
}
