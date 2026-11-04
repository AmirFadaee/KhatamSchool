using KhatamSchool_Application.Features.Commands;
using KhatamSchool_Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhatamSchool_Application.Interfaces
{
    public interface IUserRepository
    {
        public Task<ApplicationUser> GetUserByIdAsync(string userId);
        public Task<Response<string>> AddUserAsync(string Fullname,string Username,string Password);
        public Task<Response<string>> LoginAsync(string Username, string Password);
        public Task<Response<string>> DeleteUserAsync(DeleteUserCommand command);
    }
}
