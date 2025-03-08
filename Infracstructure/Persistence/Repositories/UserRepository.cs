using Core.Interfaces.Repositories;
using Core.ValueObjects;
using Google.Apis.Auth;
using Infracstructure.Identities;
using Infracstructure.Persistence;
using Infracstructure.Persistence.Repositories;
using Infrastructure.Identities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.WebSockets;
using System.Security.AccessControl;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class UserRepository : GenericRepository<ApplicationUser>, IUserRepository<ApplicationUser>
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IConfiguration _configuration;
        public UserRepository(ApplicationDbContext context, UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager, 
            IConfiguration configuration)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        public async Task<string> GenerateJwtToken(ApplicationUser user, string roleUser)
        {
            var privateKey = _configuration["Jwt:PrivateKey"];
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];
            var expireHours = Convert.ToInt32(_configuration["Jwt:ExpireHours"]);

            var rsa = RSA.Create();
            rsa.ImportFromPem(privateKey.ToCharArray());
            var securityKey = new RsaSecurityKey(rsa);

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.RsaSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, roleUser)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = issuer,
                Audience = audience,
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = credentials,
                Expires = DateTime.UtcNow.AddHours(expireHours)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var jwt = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(jwt);

            return token;
        }

        public async Task<string> LoginAsync(ApplicationUser user, string password)
        {
            var userLogin = await _userManager.FindByEmailAsync(user.Email);
            if (userLogin == null)
            {
                throw new Exception("Email is not exit");
            }
            else
            {
                var roles = await _userManager.GetRolesAsync(user);
                var result = await _signInManager.PasswordSignInAsync(user.Email, password, true, false);
                if (result.Succeeded)
                {
                    return await GenerateJwtToken(user, roles.First());
                }
                else
                {
                    throw new Exception("Login failed!");
                }
        
            }
        }

        public async Task<bool> RegisterAsync(ApplicationUser user, string password, string userRole)
        {
            var userEmail = await _userManager.FindByEmailAsync(user.Email);
            if (userEmail != null)
            {
                throw new Exception("Email is already exit");
            }
            else
            {
                var result = await _userManager.CreateAsync(user, password);
                if(!result.Succeeded)
                {
                    throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
                }
                var resultRole = await _userManager.AddToRoleAsync(user, userRole);
                if(!resultRole.Succeeded)
                {
                    throw new Exception(string.Join(", ", resultRole.Errors.Select(e => e.Description)));
                }
                return true;
            }
        }
        
        public async Task<bool> RegisterByGoogleAsync(string credential)
        {
            try
            {
                GoogleJsonWebSignature.Payload payload = await GoogleJsonWebSignature.ValidateAsync(credential);
                var userEmail = await _userManager.FindByEmailAsync(payload.Email);
                if (userEmail != null)
                {
                    throw new Exception("Email is already exit");
                }
                else
                {
                    var user = new ApplicationUser
                    {
                        Email = payload.Email,
                        UserName = payload.Email,
                        EmailConfirmed = true
                    };
                    var result = await _userManager.CreateAsync(user);
                    if (result.Succeeded)
                    {
                        var r = _userManager.AddLoginAsync(user, new UserLoginInfo("Google", payload.Subject, "Google"));
                        return r.IsCompletedSuccessfully;
                    }
                    throw new Exception("Register failed");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            
        }
    
        public async Task<string> LoginByGoogleAsync(string credential)
        {
            try
            {
                GoogleJsonWebSignature.Payload payload = await GoogleJsonWebSignature.ValidateAsync(credential);
                var user = await _userManager.FindByEmailAsync(payload.Email);
                if (user == null)
                {
                    throw new Exception("Email is not exit. Please sign up");
                }
                else
                {
                    var result = await _signInManager.PasswordSignInAsync(user, payload.Subject, true, false);
                    if (result.Succeeded)
                    {
                        var roles = await _userManager.GetRolesAsync(user);
                        return await GenerateJwtToken(user, roles.First());
                    }
                    else
                    {
                        throw new Exception("Login faild with Google");
                    }
                }
            }
            catch (InvalidJwtException ex)
            {
                throw new Exception($"Error token Google: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
