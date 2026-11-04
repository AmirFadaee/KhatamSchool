using KhatamSchool_Application;
using KhatamSchool_Application.Interfaces;
using KhatamSchool_Application.Interfaces.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhatamSchool_Data.Repository.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository _userRepository;
        public AccountService(IUserRepository userRepository) => _userRepository = userRepository;

        public async Task<Response<string>> CreateUserAsync(string Fullname,string Username, string Password)
        {
            var result =await _userRepository.AddUserAsync(Fullname, Username, Password);
            return result;  
        }
        public async Task<Response<string>> LoginAsync(string Username, string Password)
        {
            var result = await _userRepository.LoginAsync(Username,Password);
            return result;  
        }
    }
}
