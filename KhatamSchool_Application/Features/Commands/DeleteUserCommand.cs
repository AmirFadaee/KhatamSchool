using KhatamSchool_Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhatamSchool_Application.Features.Commands
{
    public class DeleteUserCommand : IRequest<Response<string>>
    {
        public string UserId { get; set; }
    }
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Response<string>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public DeleteUserCommandHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<Response<string>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user =await _userManager.Users.FirstOrDefaultAsync(x=>x.Id == request.UserId);
            if(user == null)
            {
                return new Response<string> { Succeded = false, Message = "کاربر مورد نظر یافت نشد!" };
            }
            else
            {
                await _userManager.DeleteAsync(user);
                return new Response<string> { Succeded = true, Message = "کاربر با موفقیت حذف شد.",Data=user.Id };
            }
        }
    }
}
