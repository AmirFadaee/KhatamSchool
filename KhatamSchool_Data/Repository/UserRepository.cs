using KhatamSchool_Application;
using KhatamSchool_Application.Features.Commands;
using KhatamSchool_Application.Interfaces;
using KhatamSchool_Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhatamSchool_Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMediator _mediator;
        public UserRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IMediator mediator)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _mediator = mediator;
        }

        public async Task<Response<string>> AddUserAsync(string Fullname, string Username, string Password)
        {
            var existingUser = await _userManager.FindByNameAsync(Username);
            if (existingUser != null)
            {
                return new Response<string>
                {
                    Succeded = false,
                    Message = "این نام کاربری قبلاً ثبت شده است."
                };
            }
            var user = new ApplicationUser
            {
                UserName = Username,
                FullName = Fullname
            };
            var result = await _userManager.CreateAsync(user, Password);
            if (!result.Succeeded)
            {
                var errors = string.Join(" | ", result.Errors.Select(e => e.Description));
                return new Response<string>
                {
                    Succeded = false,
                    Message = errors
                };
            }

            return new Response<string>
            {
                Succeded = true,
                Message = "کاربر با موفقیت ثبت شد.",
                Data = user.Id
            };
        }

        public async Task<Response<string>> DeleteUserAsync(DeleteUserCommand command)
        {
            await _mediator.Send(command);
            return new Response<string> { Succeded = true, Message = "کاربر با موفقیت حذف شد."};
        }

        public async Task<ApplicationUser> GetUserByIdAsync(string userId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);
            return user;
        }

        public async Task<Response<string>> LoginAsync(string Username, string Password)
        {
            var user = await _userManager.FindByNameAsync(Username);
            if (user == null)
            {
                return new Response<string>
                {
                    Succeded = false,
                    Message = "نام کاربری یا رمز عبور اشتباه است."
                };
            }

            var result = await _signInManager.PasswordSignInAsync(user, Password, isPersistent: true, lockoutOnFailure: true);

            if (result.IsLockedOut)
            {
                return new Response<string>
                {
                    Succeded = false,
                    Message = "حساب شما به‌دلیل تلاش‌های ناموفق مکرر، موقتاً قفل شده است."
                };
            }

            if (!result.Succeeded)
            {
                return new Response<string>
                {
                    Succeded = false,
                    Message = "نام کاربری یا رمز عبور اشتباه است."
                };
            }

            return new Response<string>
            {
                Succeded = true,
                Message = "ورود با موفقیت انجام شد.",
                Data = user.Id
            };
        }
    }
}
