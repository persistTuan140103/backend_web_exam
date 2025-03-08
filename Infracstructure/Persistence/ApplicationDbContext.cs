using Core.Entities;
using Infracstructure.Identities;
using Infrastructure.Identities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Infracstructure.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser,ApplicationRole, int>
    {
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<ExamRoom> ExamRooms { get; set; }
        public DbSet<UserExam> UserExams { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                var tableName = entityType.GetTableName();
                if (tableName.StartsWith("AspNet"))
                {
                    entityType.SetTableName(tableName.Substring(6));
                }
            };


            builder.Entity<ApplicationUser>()
                .HasMany(u => u.Wallets)
                .WithOne()
                .HasForeignKey(w => w.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<UserExam>()
                .HasKey(ue => new {ue.UserId, ue.ExamRoomId });

            builder.Entity<UserExam>()
                .HasOne<ApplicationUser>()
                .WithMany(u => u.UserExams)
                .HasForeignKey("UserId");

            builder.Entity<UserExam>()
                .HasOne(ue => ue.ExamRoom)
                .WithMany(er => er.UserExams)
                .HasForeignKey(ue => ue.ExamRoomId);

        }
    }
}
