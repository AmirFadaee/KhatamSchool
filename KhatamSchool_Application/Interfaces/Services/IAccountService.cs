using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhatamSchool_Application.Interfaces.Services
{
    public interface IAccountService
    {
        public Task<Response<string>> CreateUserAsync(string Fullname,string Username,string Password);
        public Task<Response<string>> LoginAsync(string Username, string Password);
    }
}
