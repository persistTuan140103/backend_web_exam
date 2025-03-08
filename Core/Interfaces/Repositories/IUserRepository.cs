using Core.Abstraction.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Repositories
{
    public interface IUserRepository<TUser> where TUser : class
    {
        Task<string> LoginAsync(TUser user, string password);
        Task<string> LoginByGoogleAsync(string credential);
        Task<bool> RegisterAsync(TUser user,string password, string userRole);
        Task<bool> RegisterByGoogleAsync(string credential);
        Task<string> GenerateJwtToken(TUser user, string roleUser);

    }
}
