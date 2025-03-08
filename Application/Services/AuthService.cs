using Application.DTOs;
using Core.Abstraction.UnitOfWork;
using Core.Interfaces.Repositories;
using Core.ValueObjects;
using Infracstructure.Identities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository<ApplicationUser> _userRepository;

        public AuthService(IUnitOfWork unitOfWork, IUserRepository<ApplicationUser> userRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }

        public async Task<string> LoginAsync(LoginRequest request)
        {
            var user = new ApplicationUser
            {
                Email = request.Email,
            };
            try
            {
                return await _userRepository.LoginAsync(user, request.Password);
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
                return await _userRepository.LoginByGoogleAsync(credential);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    
        public async Task<bool> RegisterAsync(RegisterRequest request)
        {
            var user = new ApplicationUser
            {
                Email = request.Email
            };
            try
            {
                return await _userRepository.RegisterAsync(user, request.Password, request.Role);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    
        public async Task<bool> RegisterByGoogleAsync(string credential)
        {
            try
            {
                return await _userRepository.RegisterByGoogleAsync(credential);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
